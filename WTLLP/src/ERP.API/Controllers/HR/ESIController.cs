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
    public class ESIController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ESIController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_ESIDetails> Get()
        {
            return _context.HR_ESIDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_ESIDetails.Where(x => x.ESIID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetESIList")]
        public IActionResult GetESIList()
        {
            try
            {
                    return Ok(_context.HR_ESIDetails.OrderByDescending(x => x.ESIID));
               
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewESIId")]
        public IActionResult GetNewESIId()
        {
            try
            {
                long ESIID = 0;
                var lastESI = _context.HR_ESIDetails.OrderBy(x => x.ESIID).LastOrDefault();
                ESIID = (lastESI == null ? 0 : lastESI.ESIID) + 1;
                return Ok(ESIID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_ESIDetails value)
        {
            try
            {
                _context.HR_ESIDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_ESIDetails value)
        {
            try
            {
                var existingESI = _context.HR_ESIDetails.Where(x => x.ESIID == value.ESIID).FirstOrDefault<HR_ESIDetails>();
                if (existingESI == null)
                {
                    return Ok("Not Found");
                }
                existingESI.ESILimt = value.ESILimt;
                existingESI.ESIEmployee = value.ESIEmployee;
                existingESI.ESIEmployer = value.ESIEmployer;
                existingESI.ApplyDate = value.ApplyDate;
                _context.HR_ESIDetails.Update(existingESI);
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
                var existingESI = _context.HR_ESIDetails.Where(x => x.ESIID == id).FirstOrDefault<HR_ESIDetails>();
                if (existingESI != null)
                {
                    _context.HR_ESIDetails.Remove(existingESI);
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
