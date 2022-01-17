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
    public class FinishController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public FinishController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<FinishDetails> Get()
        {
            return _context.FinishDetails.AsQueryable();
        }

        [Route("GetFinishList")]
        public IActionResult GetFinishList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.FinishDetails.OrderByDescending(x => x.Fid));
            else
                return Ok(_context.FinishDetails.Where(x => x.Fname.Contains(search)).OrderByDescending(x => x.Fid));
        }

        [Route("GetNewFinishId")]
        public IActionResult GetNewFinishId()
        {
            long FinishId = 0;
            var lastFinish = _context.FinishDetails.OrderBy(x => x.Fid).LastOrDefault();
            FinishId = (lastFinish == null ? 0 : lastFinish.Fid) + 1;
            return Ok(FinishId);
        }

        [HttpPost]
        public IActionResult Post([FromBody]FinishDetails value)
        {
            _context.FinishDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Put([FromBody]FinishDetails value)
        {
            var existingFinish = _context.FinishDetails.Where(x => x.Fid == value.Fid).FirstOrDefault<FinishDetails>();
            if (existingFinish == null)
            {
                return NotFound();              
            }
            existingFinish.Fname = value.Fname;
            existingFinish.Fdesc = value.Fdesc;
            _context.FinishDetails.Update(existingFinish);
            _context.SaveChanges();
            return Ok();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.FinishDetails.Where(x => x.Fid == id).FirstOrDefault<FinishDetails>();
            if (existing != null)
            {
                _context.FinishDetails.Remove(existing);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
