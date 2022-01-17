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
    public class BackingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public BackingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<BackingDetails> Get()
        {
            return _context.BackingDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.BackingDetails.Where(x => x.BackingId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBackingList")]
        public IActionResult GetBackingList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.BackingDetails.OrderByDescending(x => x.BackingId));
                else
                    return Ok(_context.BackingDetails.Where(x => x.BackingName.Contains(search)).OrderByDescending(x => x.BackingId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBackingId")]
        public IActionResult GetNewBackingId()
        {
            try
            {
                long BackingId = 0;
                var lastBacking = _context.BackingDetails.OrderBy(x => x.BackingId).LastOrDefault();
                BackingId = (lastBacking == null ? 0 : lastBacking.BackingId) + 1;
                return Ok(BackingId);
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
                var existingBacking = _context.BackingDetails.Where(x => x.BackingName == value).FirstOrDefault<BackingDetails>();
                if (existingBacking != null)
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
        public IActionResult Post([FromBody]BackingDetails value)
        {
            try
            {
                _context.BackingDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]BackingDetails value)
        {
            try
            {
                var existingBacking = _context.BackingDetails.Where(x => x.BackingId == value.BackingId).FirstOrDefault<BackingDetails>();
                if (existingBacking == null)
                {
                    return Ok("Not Found");
                }
                existingBacking.BackingName = value.BackingName;
                existingBacking.BackingDesc = value.BackingDesc;
                _context.BackingDetails.Update(existingBacking);
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
                var existingBacking = _context.BackingDetails.Where(x => x.BackingId == id).FirstOrDefault<BackingDetails>();
                if (existingBacking != null)
                {
                    _context.BackingDetails.Remove(existingBacking);
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
