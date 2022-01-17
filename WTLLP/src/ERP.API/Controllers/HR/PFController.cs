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
    public class PFController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PFController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_PFDetails> Get()
        {
            return _context.HR_PFDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_PFDetails.Where(x => x.PFID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPFList")]
        public IActionResult GetPFList()
        {
            try
            {
                    return Ok(_context.HR_PFDetails.OrderByDescending(x => x.PFID));
               
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPFId")]
        public IActionResult GetNewPFId()
        {
            try
            {
                long PFID = 0;
                var lastPF = _context.HR_PFDetails.OrderBy(x => x.PFID).LastOrDefault();
                PFID = (lastPF == null ? 0 : lastPF.PFID) + 1;
                return Ok(PFID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_PFDetails value)
        {
            try
            {
                _context.HR_PFDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_PFDetails value)
        {
            try
            {
                var existingPF = _context.HR_PFDetails.Where(x => x.PFID == value.PFID).FirstOrDefault<HR_PFDetails>();
                if (existingPF == null)
                {
                    return Ok("Not Found");
                }
                existingPF.PFLimit = value.PFLimit;
                existingPF.EPFEmployee = value.EPFEmployee;
                existingPF.FPFEmployer = value.FPFEmployer;
                existingPF.EmployerContribution = value.EmployerContribution;
                existingPF.ApplyDate = value.ApplyDate;
                _context.HR_PFDetails.Update(existingPF);
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
                var existingPF = _context.HR_PFDetails.Where(x => x.PFID == id).FirstOrDefault<HR_PFDetails>();
                if (existingPF != null)
                {
                    _context.HR_PFDetails.Remove(existingPF);
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
