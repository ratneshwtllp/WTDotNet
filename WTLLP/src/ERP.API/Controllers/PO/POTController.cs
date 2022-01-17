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
    public class POTController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public POTController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<POTransportDetails> Get()
        {
            return _context.POTransportDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.POTransportDetails.Where(x => x.POTransportId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPOTList")]
        public IActionResult GetPOTList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.POTransportDetails.OrderByDescending(x => x.POTransportId));
                else
                    return Ok(_context.POTransportDetails
                        .Where(x => x.POTransport.Contains(search))
                        .OrderByDescending(x => x.POTransportId)); 
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPOTId")]
        public IActionResult GetNewPOTId()
        {
            try
            {
                long POTransportId = 0;
                var lastPOT = _context.POTransportDetails.OrderBy(x => x.POTransportId).LastOrDefault();
                POTransportId = (lastPOT == null ? 0 : lastPOT.POTransportId) + 1;
                return Ok(POTransportId);
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
                var existingPOT = _context.POTransportDetails.Where(x => x.POTransport == value).FirstOrDefault<POTransportDetails>();
                if (existingPOT != null)
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
        public IActionResult Post([FromBody]POTransportDetails value)
        {
            try
            {
                _context.POTransportDetails.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]POTransportDetails value)
        {
            try
            {
                var existingPOT = _context.POTransportDetails.Where(x => x.POTransportId == value.POTransportId).FirstOrDefault<POTransportDetails>();
                if (existingPOT == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingPOT.POTransport = value.POTransport;
                existingPOT.POTransportDesc = value.POTransportDesc; 
                _context.POTransportDetails.Update(existingPOT);
                _context.SaveChanges();
                //return new NoContentResult();
                //return Ok("1");
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
                var existingPOT = _context.POTransportDetails.Where(x => x.POTransportId == id).FirstOrDefault<POTransportDetails>();
                if (existingPOT != null)
                {
                    _context.POTransportDetails.Remove(existingPOT);
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

        [Route("GetListPOT")]
        public IActionResult GetListPOT()
        {
            try
            {
                object pot;
                pot = _context.POTransportDetails
                                    .OrderBy(c => c.POTransportId)
                                    .Select(x => new { Id = x.POTransportId, Value = x.POTransport }).ToList();
                return Ok(pot);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
