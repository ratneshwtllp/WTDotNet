using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace ERP.API
{
    [Route("api/[controller]")]
    public class ProductSubCategoryController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        public ProductSubCategoryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable<ProductSubCategoryDetails> Get()
        {
            return _context.ProductSubCategoryDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductCategory")]
        public IActionResult ProductCategoryList()
        {
            try
            {
                object categories;
                categories = _context.ProductCategoryDetails
                                    .OrderBy(c => c.ProductCategoryName)
                                    .Select(x => new { Id = x.ProductCategoryID, Value = x.ProductCategoryName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductSubCategoryList")]
        public IActionResult GetProductSubCategoryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewProduct.OrderByDescending(x => x.ProductSubCategoryID));
                else
                    return Ok(_context.ViewProduct
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.ProductShortCode.Contains(search))
                        .OrderByDescending(x => x.ProductSubCategoryID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductSubCategoryId")]
        public IActionResult GetNewProductSubCategoryId()
        {
            try
            {
                long SubCategoryID = 0;
                var lastSubCategory = _context.ProductSubCategoryDetails.OrderBy(x => x.ProductSubCategoryID).LastOrDefault();
                SubCategoryID = (lastSubCategory == null ? 0 : lastSubCategory.ProductSubCategoryID) + 1;
                return Ok(SubCategoryID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductSubCategoryDetails value)
        {
            try
            {
                _context.ProductSubCategoryDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductSubCategoryDetails value)
        {
            try
            {
                var existingProSubCategory = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == value.ProductSubCategoryID).FirstOrDefault<ProductSubCategoryDetails>();
                if (existingProSubCategory == null)
                    return Ok("Not Found");

                existingProSubCategory.ProductSubCategoryName = value.ProductSubCategoryName;
                existingProSubCategory.Description = value.Description;
                existingProSubCategory.ProductCategoryID = value.ProductCategoryID;
                existingProSubCategory.ProductShortCode = value.ProductShortCode;
                existingProSubCategory.ProductStartWith = value.ProductStartWith;
                existingProSubCategory.Belongto = value.Belongto;
                existingProSubCategory.CatLevel = value.CatLevel;
                existingProSubCategory.Online_Transfer = value.Online_Transfer;
                existingProSubCategory.SubCatImagePath = value.SubCatImagePath;
                _context.ProductSubCategoryDetails.Update(existingProSubCategory);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingProSubCategory = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryName == value).FirstOrDefault<ProductSubCategoryDetails>();
                if (existingProSubCategory != null)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingProSubCategory = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == id).FirstOrDefault<ProductSubCategoryDetails>();
                if (existingProSubCategory != null)
                {
                    _context.ProductSubCategoryDetails.Remove(existingProSubCategory);
                    _context.SaveChanges();
                    return Ok("Record Deleted");
                }
                else
                {
                    return Ok("Not Found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        [Route("GetProSubCategoryList/{pcategoryId}")]
        public IActionResult GetProSubCategoryList(long pcategoryId)
        {
            try
            {
                object subcategories;
                subcategories = _context.ProductSubCategoryDetails
                                    .Where(x => x.ProductCategoryID == pcategoryId)
                                    .OrderBy(c => c.ProductShortCode)
                                    .Select(x => new { Id = x.ProductSubCategoryID, Value = x.ProductShortCode }).ToList();
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }






        /// <summary>
        /// Multi Level
        /// </summary>
        /// <returns></returns>

        [Route("GetMultiLevelSubCat")]
        public IActionResult GetMultiLevelSubCat()
        {
            try
            {
                _context.Database.ExecuteSqlCommand("EXEC SP_SubCategoryList");
                object obj;
                obj = _context.MultiLevelSubCategory
                                    .OrderBy(c => c.TableID)
                                    .Select(x => new { Id = x.ID, Value = x.Name }).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMultiLevelSubCat/{catid}")]
        public IActionResult GetMultiLevelSubCat(long catid)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("EXEC SP_SubCategoryList");
                object obj;
                obj = _context.MultiLevelSubCategory
                                    .Where(x => x.CategoryID == catid)
                                    .OrderBy(c => c.TableID)
                                    .Select(x => new { Id = x.ID, Value = x.Name }).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }        

        [HttpPost]
        [Route("MPost")]
        public IActionResult MPost([FromBody]ProductSubCategoryDetails value)
        {
            try
            {
                var obj1 = _context.MultiLevelSubCategory.Where(x => x.ID == value.Belongto).FirstOrDefault();
                if (obj1 == null)
                    return Ok("Not Found");
                if (obj1.Belongto == 0)
                {
                    value.ProductCategoryID = obj1.CategoryID;
                    value.CatLevel = obj1.CatLevel + 1;
                }
                else
                {
                    var obj = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == value.Belongto).FirstOrDefault();
                    if (obj == null)
                        return Ok("Not Found");
                    value.ProductCategoryID = obj.ProductCategoryID;
                    value.CatLevel = obj.CatLevel + 1;
                }

                _context.ProductSubCategoryDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("MPut")]
        public IActionResult MPut([FromBody]ProductSubCategoryDetails value)
        {
            try
            {
                var existingProSubCategory = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == value.ProductSubCategoryID).FirstOrDefault<ProductSubCategoryDetails>();
                if (existingProSubCategory == null)
                    return Ok("Not Found");

                var obj1 = _context.MultiLevelSubCategory.Where(x => x.ID == value.Belongto).FirstOrDefault();
                if (obj1 == null)
                    return Ok("Not Found");

                if (obj1.Belongto == 0)
                {
                    value.ProductCategoryID = obj1.CategoryID;
                    value.CatLevel = obj1.CatLevel + 1;
                }
                else
                {
                    var obj = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == value.Belongto).FirstOrDefault();
                    if (obj == null)
                        return Ok("Not Found");
                    value.ProductCategoryID = obj.ProductCategoryID;
                    value.CatLevel = obj.CatLevel + 1;
                }

                existingProSubCategory.ProductSubCategoryName = value.ProductSubCategoryName;
                existingProSubCategory.Description = value.Description;
                existingProSubCategory.ProductCategoryID = value.ProductCategoryID;
                existingProSubCategory.ProductShortCode = value.ProductShortCode;
                existingProSubCategory.ProductStartWith = value.ProductStartWith;
                existingProSubCategory.Belongto = value.Belongto;
                existingProSubCategory.CatLevel = value.CatLevel;
                existingProSubCategory.Online_Transfer = value.Online_Transfer;
                existingProSubCategory.SubCatImagePath = value.SubCatImagePath;
                _context.ProductSubCategoryDetails.Update(existingProSubCategory);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
