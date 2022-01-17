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
    public class POPTController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public POPTController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<POPaymentTerms> Get()
        {
            return _context.POPaymentTerms.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.POPaymentTerms.Where(x => x.PTId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPOPTList")]
        public IActionResult GetPOPTList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.POPaymentTerms.OrderByDescending(x => x.PTId));
                else
                    return Ok(_context.POPaymentTerms
                        .Where(x => x.PT.Contains(search))
                        .OrderByDescending(x => x.PTId));

                //return Ok(_context.POPaymentTerms
                //    .Where(x => x.PT.Contains(search) || x.Usname.Contains(search))
                //    .OrderByDescending(x => x.Uid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPOPTId")]
        public IActionResult GetNewPOPTId()
        {
            try
            {
                long PTId = 0;
                var lastPT = _context.POPaymentTerms.OrderBy(x => x.PTId).LastOrDefault();
                PTId = (lastPT == null ? 0 : lastPT.PTId) + 1;
                return Ok(PTId);
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
                var existingPT = _context.POPaymentTerms.Where(x => x.PT == value).FirstOrDefault<POPaymentTerms>();
                if (existingPT != null)
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
        public IActionResult Post([FromBody]POPaymentTerms value)
        {
            try
            {
                _context.POPaymentTerms.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]POPaymentTerms value)
        {
            try
            {
                var existingPT = _context.POPaymentTerms.Where(x => x.PTId == value.PTId).FirstOrDefault<POPaymentTerms>();
                if (existingPT == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingPT.PT = value.PT;
                existingPT.PTDesc = value.PTDesc; 
                _context.POPaymentTerms.Update(existingPT);
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
                var existingPT = _context.POPaymentTerms.Where(x => x.PTId == id).FirstOrDefault<POPaymentTerms>();
                if (existingPT != null)
                {
                    _context.POPaymentTerms.Remove(existingPT);
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

        [Route("GetListPOPT")]
        public IActionResult GetListPOPT()
        {
            try
            {
                object popt;
                popt = _context.POPaymentTerms
                                    .OrderBy(c => c.PTId)
                                    .Select(x => new { Id = x.PTId, Value = x.PT }).ToList();
                return Ok(popt);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
