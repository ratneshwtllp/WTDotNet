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
    public class StitchingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public StitchingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<StitchingDetails> Get()
        {
            return _context.StitchingDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.StitchingDetails.Where(x => x.StitchingId == id).FirstOrDefault());
        }

        [Route("GetStitchingList")]
        public IActionResult GetStitchingList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.StitchingDetails.OrderByDescending(x => x.StitchingId));
            else
                return Ok(_context.StitchingDetails
                    .Where(x => x.StitchingName.Contains(search))
                    .OrderByDescending(x => x.StitchingId));
        }

        [Route("GetNewStitchingId")]
        public IActionResult GetNewStitchingId()
        {
            long StitchingId = 0;
            var lastStitching = _context.StitchingDetails.OrderBy(x => x.StitchingId).LastOrDefault();
            StitchingId = (lastStitching == null ? 0 : lastStitching.StitchingId) + 1;
            return Ok(StitchingId);
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingStitching = _context.StitchingDetails.Where(x => x.StitchingName == value).FirstOrDefault<StitchingDetails>();

            if (existingStitching != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]StitchingDetails value)
        {
            _context.StitchingDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Put([FromBody]StitchingDetails value)
        {
            var existingStitching = _context.StitchingDetails.Where(x => x.StitchingId == value.StitchingId).FirstOrDefault<StitchingDetails>();
            if (existingStitching == null)
            {
                return NotFound();
            }
            existingStitching.StitchingName = value.StitchingName;
            existingStitching.StitchingDesc = value.StitchingDesc;
            _context.StitchingDetails.Update(existingStitching);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingStitching = _context.StitchingDetails.Where(x => x.StitchingId == id).FirstOrDefault<StitchingDetails>();

            if (existingStitching != null)
            {
                _context.StitchingDetails.Remove(existingStitching);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("StitchingList")]
        public IActionResult StitchingList()
        {
            try
            {
                object Stitching;
                Stitching = _context.StitchingDetails
                                    .OrderBy(c => c.StitchingName)
                                    .Select(x => new { Id = x.StitchingId, Value = x.StitchingName }).ToList();
                return Ok(Stitching);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
