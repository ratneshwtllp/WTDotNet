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
    public class PODTController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PODTController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<PODeliveryTerms> Get()
        {
            return _context.PODeliveryTerms.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.PODeliveryTerms.Where(x => x.DTId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPODTList")]
        public IActionResult GetPODTList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.PODeliveryTerms.OrderByDescending(x => x.DTId));
                else
                    return Ok(_context.PODeliveryTerms
                        .Where(x => x.DT.Contains(search))
                        .OrderByDescending(x => x.DTId));

                //return Ok(_context.PODeliveryTerms
                //    .Where(x => x.PT.Contains(search) || x.Usname.Contains(search))
                //    .OrderByDescending(x => x.Uid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPODTId")]
        public IActionResult GetNewPODTId()
        {
            try
            {
                long DTId = 0;
                var lastDT = _context.PODeliveryTerms.OrderBy(x => x.DTId).LastOrDefault();
                DTId = (lastDT == null ? 0 : lastDT.DTId) + 1;
                return Ok(DTId);
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
                var existingDT = _context.PODeliveryTerms.Where(x => x.DT == value).FirstOrDefault<PODeliveryTerms>();
                if (existingDT  != null)
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
        public IActionResult Post([FromBody]PODeliveryTerms value)
        {
            try
            {
                _context.PODeliveryTerms.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]PODeliveryTerms value)
        {
            try
            {
                var existingDT  = _context.PODeliveryTerms.Where(x => x.DTId == value.DTId).FirstOrDefault<PODeliveryTerms>();
                if (existingDT  == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingDT.DT = value.DT;
                existingDT.DTDesc = value.DTDesc; 
                _context.PODeliveryTerms.Update(existingDT);
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
                var existingDT  = _context.PODeliveryTerms.Where(x => x.DTId == id).FirstOrDefault<PODeliveryTerms>();
                if (existingDT  != null)
                {
                    _context.PODeliveryTerms.Remove(existingDT);
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

        [Route("GetListPODT")]
        public IActionResult GetListPODT()
        {
            try
            {
                object podt;
                podt = _context.PODeliveryTerms
                                    .OrderBy(c => c.DTId)
                                    .Select(x => new { Id = x.DTId, Value = x.DT }).ToList();
                return Ok(podt);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
