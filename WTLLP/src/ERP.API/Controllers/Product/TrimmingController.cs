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
    public class TrimmingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public TrimmingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<TrimmingDetails> Get()
        {
            return _context.TrimmingDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.TrimmingDetails.Where(x => x.TrimmingId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTrimmingList")]
        public IActionResult GetTrimmingList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.TrimmingDetails.OrderByDescending(x => x.TrimmingId));
                else
                    return Ok(_context.TrimmingDetails
                        .Where(x => x.TrimmingName.Contains(search))
                        .OrderByDescending(x => x.TrimmingId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewTrimmingId")]
        public IActionResult GetNewTrimmingId()
        {
            try
            {
                long TrimmingId = 0;
                var lastTrimming = _context.TrimmingDetails.OrderBy(x => x.TrimmingId).LastOrDefault();
                TrimmingId = (lastTrimming == null ? 0 : lastTrimming.TrimmingId) + 1;
                return Ok(TrimmingId);
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
                var existingTrimming = _context.TrimmingDetails.Where(x => x.TrimmingName == value).FirstOrDefault<TrimmingDetails>();
                if (existingTrimming != null)
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
        public IActionResult Post([FromBody]TrimmingDetails value)
        {
            try
            {
                _context.TrimmingDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]TrimmingDetails value)
        {
            try
            {
                var existingTrimming = _context.TrimmingDetails.Where(x => x.TrimmingId == value.TrimmingId).FirstOrDefault<TrimmingDetails>();
                if (existingTrimming == null)
                {
                    return Ok("Not Found");
                }
                existingTrimming.TrimmingName = value.TrimmingName;
                existingTrimming.TrimmingDesc = value.TrimmingDesc;
                _context.TrimmingDetails.Update(existingTrimming);
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
                var existingTrimming = _context.TrimmingDetails.Where(x => x.TrimmingId == id).FirstOrDefault<TrimmingDetails>();
                if (existingTrimming != null)
                {
                    _context.TrimmingDetails.Remove(existingTrimming);
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



        [Route("TrimmingList")]
        public IActionResult TrimmingList()
        {
            try
            {
                object Trimming;
                Trimming = _context.TrimmingDetails
                                    .OrderBy(c => c.TrimmingName)
                                    .Select(x => new { Id = x.TrimmingId, Value = x.TrimmingName }).ToList();
                return Ok(Trimming);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
