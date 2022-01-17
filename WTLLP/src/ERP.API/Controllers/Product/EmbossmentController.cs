using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;

namespace ERP.ApiEmbossmentList
{
    [Route("api/[controller]")]
    public class EmbossmentController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public EmbossmentController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<EmbossmentDetails> Get()
        {
            return _context.EmbossmentDetails.AsQueryable();
            
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.EmbossmentDetails.Where(x => x.EmbossmentId == id).FirstOrDefault());
        }

        [Route("GetEmbossmentList")]
        public IActionResult GetEmbossmentList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.EmbossmentDetails.OrderByDescending(x => x.EmbossmentId));
            else
                return Ok(_context.EmbossmentDetails
                    .Where(x => x.EmbossmentName.Contains(search))
                    .OrderByDescending(x => x.EmbossmentId));
        }

        [Route("GetNewEmbossmentId")]
        public IActionResult GetNewEmbossmentId()
        {
            long EmbossmentId = 0;
            var lastEmbossment = _context.EmbossmentDetails.OrderBy(x => x.EmbossmentId).LastOrDefault();
            EmbossmentId = (lastEmbossment == null ? 0 : lastEmbossment.EmbossmentId) + 1;
            return Ok(EmbossmentId);
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingEmbossment = _context.EmbossmentDetails.Where(x => x.EmbossmentName == value).FirstOrDefault<EmbossmentDetails>();

            if (existingEmbossment != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]EmbossmentDetails value)
        {
            _context.EmbossmentDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Put([FromBody]EmbossmentDetails value)
        {
            var existingEmbossment = _context.EmbossmentDetails.Where(x => x.EmbossmentId == value.EmbossmentId).FirstOrDefault<EmbossmentDetails>();
            if (existingEmbossment == null)
            {
                return NotFound();
            }
            existingEmbossment.EmbossmentName = value.EmbossmentName;
            existingEmbossment.EmbossmentDesc = value.EmbossmentDesc;
            _context.EmbossmentDetails.Update(existingEmbossment);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingEmbossment = _context.EmbossmentDetails.Where(x => x.EmbossmentId == id).FirstOrDefault<EmbossmentDetails>();

            if (existingEmbossment != null)
            {
                _context.EmbossmentDetails.Remove(existingEmbossment);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("EmbossmentList")]
        public IActionResult EmbossmentList()
        {
            try
            {
                object Embossment;
                Embossment = _context.EmbossmentDetails
                                    .OrderBy(c => c.EmbossmentName)
                                    .Select(x => new { Id = x.EmbossmentId, Value = x.EmbossmentName }).ToList();
                return Ok(Embossment);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
