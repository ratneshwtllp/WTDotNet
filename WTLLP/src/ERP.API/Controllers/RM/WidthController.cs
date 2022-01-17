using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class WidthController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public WidthController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<WidthDetails> Get()
        {
            return _context.WidthDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.WidthDetails.Where(x => x.WidthId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetWidthList")]
        public IActionResult GetWidthList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.WidthDetails.OrderByDescending(x => x.WidthId));
                else
                    return Ok(_context.WidthDetails
                        .Where(x => x.WidthName.Contains(search))
                        .OrderByDescending(x => x.WidthId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewWidthId")]
        public IActionResult GetNewWidthId()
        {
            try
            {
                long WidthId = 0;
                var lastWidth = _context.WidthDetails.OrderBy(x => x.WidthId).LastOrDefault();
                WidthId = (lastWidth == null ? 0 : lastWidth.WidthId) + 1;
                return Ok(WidthId);
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
                var existingWidth = _context.WidthDetails.Where(x => x.WidthName == value).FirstOrDefault<WidthDetails>();
                if (existingWidth != null)
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

        [HttpPost]
        public IActionResult Post([FromBody]WidthDetails value)
        {
            try
            {
                _context.WidthDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]WidthDetails value)
        {
            try
            {
                var existingWidth = _context.WidthDetails.Where(x => x.WidthId == value.WidthId).FirstOrDefault<WidthDetails>();
                if (existingWidth == null)
                {
                    return Ok("Not Found");
                }
                existingWidth.WidthName = value.WidthName;
                existingWidth.WidthDesc = value.WidthDesc;
                _context.WidthDetails.Update(existingWidth);
                _context.SaveChanges();
                return Ok("Record Updated");
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
                var existingWidth = _context.WidthDetails.Where(x => x.WidthId == id).FirstOrDefault<WidthDetails>();
                if (existingWidth != null)
                {
                    _context.WidthDetails.Remove(existingWidth);
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

        [Route("GetWidthDetails")]
        public IActionResult GetWidthDetails()
        {
            try
            {
                var WidthList = _context.WidthDetails
                    .OrderBy(x => x.WidthName)
                    .Select(x => new { Id = x.WidthId, Value = x.WidthName })
                    .ToList();

                return Ok(WidthList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
