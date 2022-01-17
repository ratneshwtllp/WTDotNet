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
    public class LiningController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        //private object existingQuality;

        public LiningController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<LiningDetails > Get()
        {
            return _context.LiningDetails .AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.LiningDetails.Where(x => x.LiningID   == id).FirstOrDefault());
        }

        [Route("GetLiningList")]
        public IActionResult GetLiningList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.LiningDetails.OrderByDescending(x => x.LiningID ));
            }
            else
            {
                return Ok(_context.LiningDetails
                    .Where(x => x.LiningName  .Contains(search))
                    .OrderByDescending(x => x.LiningID ));
            }
        }

        [Route("FillQilting")]
        public IActionResult FillQilting(string search)
        {
            try
            {
                object liniing;
                if (string.IsNullOrEmpty(search))
                {
                    liniing = _context.LiningDetails
                                        .OrderBy(c => c.LiningName)
                                        .Select(x => new { Id = x.LiningID, Value = x.LiningName });
                }
                else
                {
                    liniing = _context.LiningDetails
                                        .Where(x => x.LiningName.Contains(search))
                                        .OrderBy(c => c.LiningName)
                                        .Select(x => new { Id = x.LiningID, Value = x.LiningName });
                }
                return Ok(liniing);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewLiningId")]
        public IActionResult GetNewLiningId()
        {
            long LiningID = 0;
            var lastLining = _context.LiningDetails.OrderBy(x => x.LiningID ).LastOrDefault();
            LiningID = (lastLining == null ? 0 : lastLining.LiningID) + 1;
            return Ok(LiningID);
        }

        [HttpPost]
        public IActionResult Post([FromBody]LiningDetails  value)
        {
            _context.LiningDetails .Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]LiningDetails  value)
        {
            var existingLining = _context.LiningDetails .Find(value.LiningID );
            if (existingLining == null)
            {
                return NotFound();
            }
            existingLining.LiningName   = value.LiningName  ;
            existingLining.LiningDesc   = value.LiningDesc  ;
            _context.LiningDetails .Update(existingLining);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingLining = _context.LiningDetails .Where(x => x.LiningName   == value).FirstOrDefault<LiningDetails >();

            if (existingLining != null)
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
            var existingLining = _context.LiningDetails .Find(id);
            if (existingLining != null)
            {
                _context.LiningDetails .Remove(existingLining);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        [Route("LiningList")]
        public IActionResult LiningList()
        {
            try
            {
                object Lining;
                Lining = _context.LiningDetails
                                    .OrderBy(c => c.LiningName)
                                    .Select(x => new { Id = x.LiningID, Value = x.LiningName }).ToList();
                return Ok(Lining);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
