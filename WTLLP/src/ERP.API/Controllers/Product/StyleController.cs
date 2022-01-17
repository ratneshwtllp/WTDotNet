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
    public class StyleController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public StyleController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<StyleDetails> Get()
        {
            return _context.StyleDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.StyleDetails.Where(x => x.StyleID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStyleList")]
        public IActionResult GetStyleList()
        {
            try
            {
                object stylelist;
                stylelist = _context.StyleDetails
                                        .OrderBy(c => c.StyleName)
                                        .Select(x => new { Id = x.StyleID, Value = x.StyleName }).ToList();
                return Ok(stylelist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStyleRecord")]
        public IActionResult GetStyleRecord(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.StyleDetails.OrderByDescending(x => x.StyleID));
                else
                    return Ok(_context.StyleDetails
                        .Where(x => x.StyleName.Contains(search))
                        .OrderByDescending(x => x.StyleID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStyleId")]
        public IActionResult GetNewStyleId()
        {
            try
            {
                long StyleId = 0;
                var lastStyle = _context.StyleDetails.OrderBy(x => x.StyleID).LastOrDefault();
                StyleId = (lastStyle == null ? 0 : lastStyle.StyleID) + 1;
                return Ok(StyleId);
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
                var existingThread = _context.StyleDetails.Where(x => x.StyleName == value).FirstOrDefault<StyleDetails>();
                if (existingThread != null)
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
        public IActionResult Post([FromBody]StyleDetails value)
        {
            try
            {
                _context.StyleDetails.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]StyleDetails value)
        {
            try
            {
                var existingStyle = _context.StyleDetails.Where(x => x.StyleID == value.StyleID).FirstOrDefault<StyleDetails>();
                if (existingStyle == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingStyle.StyleName = value.StyleName; 
                existingStyle.Description = value.Description;
                _context.StyleDetails.Update(existingStyle);
                _context.SaveChanges();
                //return new NoContentResult();
                //return Ok("1");
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
                var existingStyle = _context.StyleDetails.Where(x => x.StyleID == id).FirstOrDefault<StyleDetails>();
                if (existingStyle != null)
                {
                    _context.StyleDetails.Remove(existingStyle);
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
