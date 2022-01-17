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
    public class WashController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        //private object existingQuality;

        public WashController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<WashDetails  > Get()
        {
            return _context.WashDetails .AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.WashDetails .Where(x => x.WashID == id).FirstOrDefault());
        }

        [Route("GetWashList")]
        public IActionResult GetWashList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.WashDetails .OrderByDescending(x => x.WashID  ));
            }
            else
            {
                return Ok(_context.WashDetails 
                    .Where(x => x.Wash .Contains(search))
                    .OrderByDescending(x => x.WashID  ));
            }
        }

        [Route("GetNewWashId")]
        public IActionResult GetNewWashId()
        {
            long WashID = 0;
            var lastwash = _context.WashDetails .OrderBy(x => x.WashID  ).LastOrDefault();
            WashID = (lastwash == null ? 0 : lastwash.WashID ) + 1;
            return Ok(WashID);
        }

        [HttpPost]
        public IActionResult Post([FromBody]WashDetails   value)
        {
            _context.WashDetails  .Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]WashDetails   value)
        {
            var existingwash = _context.WashDetails  .Find(value.WashID  );
            if (existingwash == null)
            {
                return NotFound();
            }
            existingwash.Wash     = value.Wash   ;
            existingwash.Description    = value.Description   ;
            _context.WashDetails  .Update(existingwash);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingwash = _context.WashDetails  .Where(x => x.Wash    == value).FirstOrDefault<WashDetails  >();

            if (existingwash != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingwash = _context.WashDetails  .Find(id);
            if (existingwash != null)
            {
                _context.WashDetails  .Remove(existingwash);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("WashList")]
        public IActionResult WashList()
        {
            try
            {
                object Wash;
                Wash = _context.WashDetails
                                    .OrderBy(c => c.Wash)
                                    .Select(x => new { Id = x.WashID, Value = x.Wash }).ToList();
                return Ok(Wash);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
