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
    public class WaxController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public WaxController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<WaxDetails> Get()
        {
            return _context.WaxDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.WaxDetails.Where(x => x.WaxID == id).FirstOrDefault());
        }

        [Route("GetWaxList")]
        public IActionResult GetWaxList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.WaxDetails.OrderByDescending(x => x.WaxID));
            else
                return Ok(_context.WaxDetails
                    .Where(x => x.WaxName.Contains(search))
                    .OrderByDescending(x => x.WaxID));
        }

        [Route("GetNewWaxId")]
        public IActionResult GetNewWaxId()
        {
            long WaxId = 0;
            var lastWax = _context.WaxDetails.OrderBy(x => x.WaxID).LastOrDefault();
            WaxId = (lastWax == null ? 0 : lastWax.WaxID) + 1;
            return Ok(WaxId);
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingWax = _context.WaxDetails.Where(x => x.WaxName == value).FirstOrDefault<WaxDetails>();

            if (existingWax != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]WaxDetails value)
        {
            _context.WaxDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Put([FromBody]WaxDetails value)
        {
            var existingWax = _context.WaxDetails.Where(x => x.WaxID == value.WaxID).FirstOrDefault<WaxDetails>();
            if (existingWax == null)
            {
                return NotFound();
            }
            existingWax.WaxName = value.WaxName;
            existingWax.WaxDesc = value.WaxDesc;
            _context.WaxDetails.Update(existingWax);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingWax = _context.WaxDetails.Where(x => x.WaxID == id).FirstOrDefault<WaxDetails>();

            if (existingWax != null)
            {
                _context.WaxDetails.Remove(existingWax);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("WaxList")]
        public IActionResult WaxList()
        {
            try
            {
                object Wax;
                Wax = _context.WaxDetails
                                    .OrderBy(c => c.WaxName)
                                    .Select(x => new { Id = x.WaxID, Value = x.WaxName }).ToList();
                return Ok(Wax);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
