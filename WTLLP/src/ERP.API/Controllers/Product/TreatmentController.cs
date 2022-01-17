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
    public class TreatmentController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public TreatmentController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<TreatmentDetails> Get()
        {
            return _context.TreatmentDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.TreatmentDetails.Where(x => x.TreatmentId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTreatmentList")]
        public IActionResult GetTreatmentList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.TreatmentDetails.OrderByDescending(x => x.TreatmentId));
                else
                    return Ok(_context.TreatmentDetails
                        .Where(x => x.TreatmentName.Contains(search))
                        .OrderByDescending(x => x.TreatmentId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewTreatmentId")]
        public IActionResult GetNewTreatmentId()
        {
            try
            {
                long TreatmentId = 0;
                var lastTreatment = _context.TreatmentDetails.OrderBy(x => x.TreatmentId).LastOrDefault();
                TreatmentId = (lastTreatment == null ? 0 : lastTreatment.TreatmentId) + 1;
                return Ok(TreatmentId);
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
                var existingTreatment = _context.TreatmentDetails.Where(x => x.TreatmentName == value).FirstOrDefault<TreatmentDetails>();
                if (existingTreatment != null)
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
        public IActionResult Post([FromBody]TreatmentDetails value)
        {
            try
            {
                _context.TreatmentDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]TreatmentDetails value)
        {
            try
            {
                var existingTreatment = _context.TreatmentDetails.Where(x => x.TreatmentId == value.TreatmentId).FirstOrDefault<TreatmentDetails>();
                if (existingTreatment == null)
                {
                    return Ok("Not Found");
                }
                existingTreatment.TreatmentName = value.TreatmentName;
                existingTreatment.TreatmentDesc = value.TreatmentDesc;
                _context.TreatmentDetails.Update(existingTreatment);
                _context.SaveChanges();
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
                var existingTreatment = _context.TreatmentDetails.Where(x => x.TreatmentId == id).FirstOrDefault<TreatmentDetails>();
                if (existingTreatment != null)
                {
                    _context.TreatmentDetails.Remove(existingTreatment);
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
    }
}
