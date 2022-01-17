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
    public class TransportController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public TransportController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<TransportDetails> Get()
        {
            return _context.TransportDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.TransportDetails.Where(x => x.TransportId == id).FirstOrDefault());
        }

        [Route("GetTransportList")]
        public IActionResult GetTransportList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.TransportDetails.OrderByDescending(x => x.TransportId));
            else
                return Ok(_context.TransportDetails.Where(x => x.TranportName.Contains(search)).OrderByDescending(x => x.TransportId));
        }

        [Route("GetNewTransportId")]
        public IActionResult GetNewTransportId()
        {
            long TransportId = 0;
            var lastTransport = _context.TransportDetails.OrderBy(x => x.TransportId).LastOrDefault();
            TransportId = (lastTransport == null ? 0 : lastTransport.TransportId) + 1;
            return Ok(TransportId);
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingTransport = _context.TransportDetails.Where(x => x.TranportName == value).FirstOrDefault<TransportDetails>();

            if (existingTransport != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]TransportDetails value)
        {
            _context.TransportDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Put([FromBody]TransportDetails value)
        {
            var existingTransport = _context.TransportDetails.Where(x => x.TransportId == value.TransportId).FirstOrDefault<TransportDetails>();
            if (existingTransport == null)
            {
                return NotFound();
            }
            existingTransport.TranportName = value.TranportName;
            existingTransport.TransportDesc = value.TransportDesc;
            _context.TransportDetails.Update(existingTransport);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingTransport = _context.TransportDetails.Where(x => x.TransportId == id).FirstOrDefault<TransportDetails>();

            if (existingTransport != null)
            {
                _context.TransportDetails.Remove(existingTransport);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("TransportList")]
        public IActionResult TransportList()
        {
            try
            {
                object ProCategory;
                ProCategory = _context.TransportDetails
                                    .OrderBy(c => c.TranportName)
                                    .Select(x => new { Id = x.TransportId, Value = x.TranportName }).ToList();
                return Ok(ProCategory);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("FOBCIFList")]
        public IActionResult FOBCIFList()
        {
            try
            {
                object fobcif;
                fobcif = _context.FOBCIFDetails
                                    .OrderBy(c => c.FOBCIF)
                                    .Select(x => new { Id = x.FobCifID, Value = x.FOBCIF }).ToList();
                return Ok(fobcif);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
