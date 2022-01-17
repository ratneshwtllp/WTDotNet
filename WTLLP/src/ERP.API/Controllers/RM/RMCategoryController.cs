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
    public class RMCategoryController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public RMCategoryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <RMCategory> Get()
        {
            return _context.RMCategory.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.RMCategory.Where(x => x.CategoryID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [Route("GetRMCategoryList")]
        public IActionResult GetRMCategoryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.RMCategory.OrderByDescending(x => x.CategoryID));
                else
                    return Ok(_context.RMCategory
                        .Where(x => x.CategoryName.Contains(search))
                        .OrderByDescending(x => x.CategoryID));

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList()
        {
            try
            {
                object rmcatlist = _context.RMCategory
                                         .OrderBy(x => x.CategoryName)
                                         .Select(x => new { Id = x.CategoryID, Value = x.CategoryName });
                return Ok(rmcatlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMCategoryId")]
        public IActionResult GetNewRMCategoryId()
        {
            try
            {
                long CategoryID = 0;
                var lastCategory = _context.RMCategory.OrderBy(x => x.CategoryID).LastOrDefault();
                CategoryID = (lastCategory == null ? 0 : lastCategory.CategoryID) + 1;
                return Ok(CategoryID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]RMCategory value)
        {
            try
            {
                _context.RMCategory.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]RMCategory value)
        {
            try
            {
                var existingRMCategory = _context.RMCategory.Find(value.CategoryID);
                if (existingRMCategory == null)
                {
                    return Ok("Not Found");
                }
                existingRMCategory.CategoryName = value.CategoryName;
                existingRMCategory.Description = value.Description;
                existingRMCategory.DisplayOrder = value.DisplayOrder;
                _context.RMCategory.Update(existingRMCategory);
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
                var existingRMCategory = _context.RMCategory.Where(x => x.CategoryName == value).FirstOrDefault<RMCategory>();
                if (existingRMCategory != null)
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
                var existingRMCategory = _context.RMCategory.Where(x => x.CategoryID == id).FirstOrDefault<RMCategory>();
                if (existingRMCategory != null)
                {
                    _context.RMCategory.Remove(existingRMCategory);
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
