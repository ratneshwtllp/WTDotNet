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
    public class HolidayController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public HolidayController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_HolidayMaster> Get()
        {
            return _context.HR_HolidayMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_HolidayMaster.Where(x => x.HolidayId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetHolidayList")]
        public IActionResult GetHolidayList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_HolidayMaster.OrderBy(x => x.HolidayDate));
                else
                    return Ok(_context.HR_HolidayMaster
                        .Where(x => x.HolidayName.Contains(search))
                        .OrderBy(x => x.HolidayDate));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewHolidayId")]
        public IActionResult GetNewHolidayId()
        {
            try
            {
                int HolidayId = 0;
                var lastHoliday = _context.HR_HolidayMaster.OrderBy(x => x.HolidayId).LastOrDefault();
                HolidayId = (lastHoliday == null ? 0 : lastHoliday.HolidayId) + 1;
                return Ok(HolidayId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_HolidayMaster value)
        {
            try
            {
                _context.HR_HolidayMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_HolidayMaster value)
        {
            try
            {
                var existingHoliday = _context.HR_HolidayMaster.Where(x => x.HolidayId == value.HolidayId).FirstOrDefault<HR_HolidayMaster>();
                if (existingHoliday == null)
                {
                    return Ok("Not Found");
                }
                existingHoliday.HolidayName = value.HolidayName;
                existingHoliday.HolidayDate = value.HolidayDate;
                existingHoliday.SessionYear = value.SessionYear;
                existingHoliday.UserId = value.UserId;
                _context.HR_HolidayMaster.Update(existingHoliday);
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
                var existingHoliday = _context.HR_HolidayMaster.Where(x => x.HolidayId == id).FirstOrDefault<HR_HolidayMaster>();
                if (existingHoliday != null)
                {
                    _context.HR_HolidayMaster.Remove(existingHoliday);
                    _context.SaveChanges();
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

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(DateTime value)
        {
            try
            {
                var existingHoliday = _context.HR_HolidayMaster.Where(x => x.HolidayDate == value).FirstOrDefault<HR_HolidayMaster>();
                if (existingHoliday != null)
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
