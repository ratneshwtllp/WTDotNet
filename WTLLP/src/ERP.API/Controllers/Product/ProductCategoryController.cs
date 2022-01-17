using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.API
{
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ProductCategoryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ProductCategoryDetails> Get()
        {
            return _context.ProductCategoryDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ProductCategoryDetails.Where(x => x.ProductCategoryID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetProductCategoryList")]
        public IActionResult GetProductCategoryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewProductCategory .OrderByDescending(x => x.ProductCategoryID));
                else
                    return Ok(_context.ViewProductCategory
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductShortCode.Contains(search))
                        .OrderByDescending(x => x.ProductCategoryID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductCategoryId")]
        public IActionResult GetNewProductCategoryId()
        {
            try
            {
                long CategoryID = 0;
                var lastCategory = _context.ProductCategoryDetails.OrderBy(x => x.ProductCategoryID).LastOrDefault();
                CategoryID = (lastCategory == null ? 0 : lastCategory.ProductCategoryID) + 1;
                return Ok(CategoryID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductCategoryDetails value)
        {
            try
            {
                _context.ProductCategoryDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductCategoryDetails value)
        {
            try
            {
                var existingProductCategory = _context.ProductCategoryDetails.Find(value.ProductCategoryID);
                if (existingProductCategory == null)
                {
                    return Ok("Not Found");
                }
                existingProductCategory.ProductCategoryName = value.ProductCategoryName;
                existingProductCategory.ProductShortCode = value.ProductShortCode;
                existingProductCategory.ProductStartWith = value.ProductStartWith;
                existingProductCategory.Description = value.Description;

                existingProductCategory.BackgroundImagePath = value.BackgroundImagePath;
                existingProductCategory.CategoryImagePath = value.CategoryImagePath;
                existingProductCategory.PDFCatelogPath = value.PDFCatelogPath;
                existingProductCategory.Position = value.Position;
                existingProductCategory.PDFCatelogStatus = value.PDFCatelogStatus;

                existingProductCategory.Active = value.Active;
                existingProductCategory.ShowInMenu = value.ShowInMenu;
                existingProductCategory.Online_Transfer = value.Online_Transfer;
                //existingProductCategory.BuyerId = value.BuyerId;
                _context.ProductCategoryDetails.Update(existingProductCategory);
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
                var existingProductCategory = _context.ProductCategoryDetails.Where(x => x.ProductCategoryName == value).FirstOrDefault<ProductCategoryDetails>();
                if (existingProductCategory != null)
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
                var existingProductCategory = _context.ProductCategoryDetails.Where(x => x.ProductCategoryID == id).FirstOrDefault<ProductCategoryDetails>();
                if (existingProductCategory != null)
                {
                    _context.ProductCategoryDetails.Remove(existingProductCategory);
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

        [Route("GetProCategoryList")]
        public IActionResult GetProCategoryList()
        {
            try
            {
                object ProCategory;
                ProCategory = _context.ProductCategoryDetails
                                    .OrderBy(c => c.ProductCategoryName)
                                    .Select(x => new { Id = x.ProductCategoryID, Value = x.ProductCategoryName }).ToList();
                return Ok(ProCategory);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetProCategoryIdByBuyerCode")]
        public IActionResult GetProCategoryIdByBuyerCode(string val)
        {
            try
            {
                var catid = _context.ProductCategoryDetails
                                        .Where(x => x.ProductShortCode == val)
                                        .Select(x => x.ProductCategoryID)
                                        .SingleOrDefault();
                return Ok(catid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetPositionList")]
        public IActionResult GetPositionList()
        {
            try
            {
               var  existingposition = _context.ProductCategoryDetails
                                  .OrderBy(x => x.Position)
                                  .Select(x => x.Position).ToList();

                object position;
                position = _context.DisplayPosition
                        .Where(x =>  !existingposition.Contains(x.Position ))
                                    .OrderBy(c => c.Position )
                                    .Select(x => new { Id = x.Position, Value = x.Position }).ToList();
                return Ok(position);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetModifyPositionList")]
        public IActionResult GetModifyPositionList()
        {
            try
            {
                object position;
                position = _context.DisplayPosition
                                    .OrderBy(c => c.Position)
                                    .Select(x => new { Id = x.Position, Value = x.Position }).ToList();
                return Ok(position);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
