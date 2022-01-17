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
    public class HeelController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public HeelController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<HeelDetails> Get()
        {
            return _context.HeelDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HeelDetails.Where(x => x.HeelId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [Route("GetHeelList")]
        public IActionResult GetHeelList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HeelDetails.OrderByDescending(x => x.HeelId));
                else
                    return Ok(_context.HeelDetails
                        .Where(x => x.HeelName.Contains(search)) 
                        .OrderByDescending(x => x.HeelId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetListHeel")]
        public IActionResult GetListHeel()
        {
            try
            {
                object Heels;
                Heels = _context.HeelDetails
                                        .OrderBy(c => c.HeelName)
                                        .Select(x => new { Id = x.HeelId, Value = x.HeelName }).ToList();
                return Ok(Heels);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewHeelId")]
        public IActionResult GetNewHeelId()
        {
            try
            {
                long HeelId = 0;
                var lastHeel = _context.HeelDetails.OrderBy(x => x.HeelId).LastOrDefault();
                HeelId = (lastHeel == null ? 0 : lastHeel.HeelId) + 1;
                return Ok(HeelId);
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
                var existingHeel = _context.HeelDetails.Where(x => x.HeelName == value).FirstOrDefault<HeelDetails>();
                if (existingHeel != null)
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
        public IActionResult Post([FromBody]HeelDetails value)
        {
            try
            {
                _context.HeelDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HeelDetails value)
        {
            try
            {
                var existingHeel = _context.HeelDetails.Where(x => x.HeelId == value.HeelId).FirstOrDefault<HeelDetails>();
                if (existingHeel == null)
                {
                    return Ok("Not Found");
                }
                existingHeel.HeelName = value.HeelName;
                existingHeel.HeelDesc = value.HeelDesc;
                _context.HeelDetails.Update(existingHeel);
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
                var existingHeel = _context.HeelDetails.Where(x => x.HeelId == id).FirstOrDefault<HeelDetails>();
                if (existingHeel != null)
                {
                    _context.HeelDetails.Remove(existingHeel);
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
