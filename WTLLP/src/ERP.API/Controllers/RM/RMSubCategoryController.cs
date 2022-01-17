using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class RMSubCategoryController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public RMSubCategoryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<RMSubCategory> Get()
        {
            return _context.RMSubCategory.AsQueryable();
        }

        //public IQueryable<ViewRMSubCategory> Get()
        //{
        //    return _context.ViewRMSubCategory.AsQueryable();
        //}

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.RMSubCategory.Where(x => x.SubCategoryID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }



        [Route("RMCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                object categories;
                categories = _context.RMCategory
                                    .OrderBy(c => c.CategoryName)
                                    .Select(x => new { Id = x.CategoryID, Value = x.CategoryName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMSubCategoryList")]
        public IActionResult GetRMSubCategoryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewRMSubCategory.OrderByDescending(x => x.SubCategoryID));
                else
                    return Ok(_context.ViewRMSubCategory
                        .Where(x => x.CategoryName.Contains(search) || x.SubCategoryName.Contains(search) || x.ShortCode.Contains(search))
                        .OrderByDescending(x => x.SubCategoryID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSubCategoryDetails/{subcatid}")]
        public IActionResult GetSubCategoryDetails(int subcatid)
        {
            try
            {
                var rmsubcatdetails = _context.ViewRMSubCategory.Where(x => x.SubCategoryID== subcatid)
                        .OrderByDescending(x => x.SubCategoryID)
                        .SingleOrDefault();

                return Ok(rmsubcatdetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList/{id}")]
        public IActionResult GetList(long id)
        {
            try
            {
                object rmscatlist = _context.RMSubCategory.Where(x => x.CategoryID == id)
                                         .OrderBy(x => x.SubCategoryName)
                                         .Select(x => new { Id = x.SubCategoryID, Value = x.SubCategoryName });
                return Ok(rmscatlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMSubCategoryId")]
        public IActionResult GetNewRMSubCategoryId()
        {
            try
            {
                long SubCategoryID = 0;
                var lastSubCategory = _context.RMSubCategory.OrderBy(x => x.SubCategoryID).LastOrDefault();
                SubCategoryID = (lastSubCategory == null ? 0 : lastSubCategory.SubCategoryID) + 1;
                return Ok(SubCategoryID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]RMSubCategory value)
        {
            try
            {
                _context.RMSubCategory.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]RMSubCategory value)
        {
            try
            {
                var existingRMSubCategory = _context.RMSubCategory.Find(value.SubCategoryID);
                if (existingRMSubCategory == null)
                {
                    return Ok("Not Found");
                }
                existingRMSubCategory.SubCategoryName = value.SubCategoryName;
                existingRMSubCategory.Description = value.Description;
                existingRMSubCategory.CategoryID = value.CategoryID;
                existingRMSubCategory.HSNId = value.HSNId;
                _context.RMSubCategory.Update(existingRMSubCategory);
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
                var existingRMSubCategory = _context.RMSubCategory.Where(x => x.SubCategoryName == value).FirstOrDefault<RMSubCategory>();
                if (existingRMSubCategory != null)
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
                var existingRMSubCategory = _context.RMSubCategory.Where(x => x.SubCategoryID == id).FirstOrDefault<RMSubCategory>();
                if (existingRMSubCategory != null)
                {
                    _context.RMSubCategory.Remove(existingRMSubCategory);
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


    }
}
