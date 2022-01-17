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
    public class AdvanceController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public AdvanceController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<HR_ViewAdvanceDetails> Get()
        {
            return _context.HR_ViewAdvanceDetails.AsQueryable(); 
        }
        [Route("GetAdvanceList")]
        public IActionResult GetAdvanceList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_ViewAdvanceDetails .OrderByDescending(x => x.AdvanceId ));
                else
                    return Ok(_context.HR_ViewAdvanceDetails
                        .Where(x => x.EmployeeName .Contains(search)||x.Remark .Contains(search))
                        .OrderByDescending(x => x.AdvanceId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
      
      

        [HttpPost]
        public IActionResult Post([FromBody]HR_AdvanceDetails  value)
        {
            try
            {
                long AdvanceId = 0;
                var lastadvance = _context.HR_AdvanceDetails.OrderBy(x => x.AdvanceId ).LastOrDefault();
                 AdvanceId = (lastadvance == null ? 0 : lastadvance.AdvanceId ) + 1;
                value.AdvanceId = AdvanceId;
                _context.HR_AdvanceDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_AdvanceDetails value)
        {
            try
            {
                var existingadvance = _context.HR_AdvanceDetails.Where(x => x.AdvanceId  == value.AdvanceId).FirstOrDefault<HR_AdvanceDetails>();
                if (existingadvance == null)
                {
                    return Ok("Not Found");
                }
                existingadvance.AdvanceDate  = value.AdvanceDate;
                existingadvance.AdvanceAmount  = value.AdvanceAmount;
                _context.HR_AdvanceDetails .Update(existingadvance);
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
                var existingadvance = _context.HR_AdvanceDetails .Where(x => x.AdvanceId  == id).FirstOrDefault<HR_AdvanceDetails >();
                if (existingadvance != null)
                {
                    _context.HR_AdvanceDetails.Remove(existingadvance);
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
