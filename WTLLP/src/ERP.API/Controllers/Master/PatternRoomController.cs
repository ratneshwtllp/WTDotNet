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
    public class PatternRoomController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PatternRoomController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<PatternRoomDetails> Get()
        {
            return _context.PatternRoomDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewPatternRoomDetails.Where(x => x.PatternRoomId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPatternRoomList")]
        public IActionResult GetPatternRoomList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewPatternRoomDetails.OrderByDescending(x => x.PatternRoomId));
                else
                    return Ok(_context.ViewPatternRoomDetails
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search))
                        .OrderByDescending(x => x.PatternRoomId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPatternRoomId")]
        public IActionResult GetNewPatternRoomId()
        {
            try
            {
                long PatternRoomId = 0;
                var lastSample = _context.PatternRoomDetails.OrderBy(x => x.PatternRoomId).LastOrDefault();
                PatternRoomId = (lastSample == null ? 0 : lastSample.PatternRoomId) + 1;
                return Ok(PatternRoomId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PatternRoomDetails value)
        {
            try
            {
                _context.PatternRoomDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]PatternRoomDetails value)
        {
            try
            {
                var existingPattern = _context.PatternRoomDetails.Where(x => x.PatternRoomId == value.PatternRoomId).FirstOrDefault<PatternRoomDetails>();
                if (existingPattern == null)
                {
                    return Ok("Not Found");
                }
                existingPattern.FItemId = value.FItemId;
                existingPattern.StoreQty = value.StoreQty;
                existingPattern.StoreDate = value.StoreDate;
                existingPattern.RackNo = value.RackNo;
                existingPattern.Session_Year = value.Session_Year;
                existingPattern.UserId = value.UserId;
                existingPattern.ComponentQty = value.ComponentQty;
                _context.PatternRoomDetails.Update(existingPattern);
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
                var existingPattern = _context.PatternRoomDetails.Where(x => x.PatternRoomId == id).FirstOrDefault<PatternRoomDetails>();
                if (existingPattern != null)
                {
                    _context.PatternRoomDetails.Remove(existingPattern);
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
