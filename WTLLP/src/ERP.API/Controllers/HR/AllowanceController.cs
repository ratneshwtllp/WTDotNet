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
    public class AllowanceController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public AllowanceController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_AllowanceDetails> Get()
        {
            return _context.HR_AllowanceDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_AllowanceDetails.Where(x => x.AllowanceId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_AllowanceDetails.OrderByDescending(x => x.AllowanceId));
                else
                    return Ok(_context.HR_AllowanceDetails
                        .Where(x => x.AllowanceType.Contains(search) || x.Description.Contains(search))
                        .OrderByDescending(x => x.AllowanceId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                int id = 0;
                var record = _context.HR_AllowanceDetails.OrderBy(x => x.AllowanceId).LastOrDefault();
                id = (record == null ? 0 : record.AllowanceId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_AllowanceDetails value)
        {
            try
            {
                _context.HR_AllowanceDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_AllowanceDetails value)
        {
            try
            {
                var record = _context.HR_AllowanceDetails.Where(x => x.AllowanceId == value.AllowanceId).FirstOrDefault<HR_AllowanceDetails>();
                if (record == null)
                {
                    return Ok("Not Found");
                }
                record.AllowanceType = value.AllowanceType;
                record.Description = value.Description;
                record.SessionYear = value.SessionYear;
                record.UserId = value.UserId;
                _context.HR_AllowanceDetails.Update(record);
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
                var record = _context.HR_AllowanceDetails.Where(x => x.AllowanceId == id).FirstOrDefault<HR_AllowanceDetails>();
                if (record != null)
                {
                    _context.HR_AllowanceDetails.Remove(record);
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

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var record = _context.HR_AllowanceDetails.Where(x => x.AllowanceType == value).FirstOrDefault<HR_AllowanceDetails>();
                if (record != null)
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

    }
}
