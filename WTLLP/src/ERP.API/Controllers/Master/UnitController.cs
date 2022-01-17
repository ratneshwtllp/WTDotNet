using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;

namespace ERP.Api
{
    //Unit API Change on 24/Oct/2021 for Git
    //Line 2 Add after Line First
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public UnitController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<UnitDetails> Get()
        {
            return _context.UnitDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.UnitDetails.Where(x => x.Uid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetUnitList")]
        public IActionResult GetUnitList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.UnitDetails.OrderByDescending(x => x.Uid));
                else
                    return Ok(_context.UnitDetails
                        .Where(x => x.Uname.Contains(search) || x.Usname.Contains(search))
                        .OrderByDescending(x => x.Uid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewUnitId")]
        public IActionResult GetNewUnitId()
        {
            try
            {
                long UnitId = 0;
                var lastUnit = _context.UnitDetails.OrderBy(x => x.Uid).LastOrDefault();
                UnitId = (lastUnit == null ? 0 : lastUnit.Uid) + 1;
                return Ok(UnitId);
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
                var existingThread = _context.UnitDetails.Where(x => x.Uname == value).FirstOrDefault<UnitDetails>();
                if (existingThread != null)
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
        public IActionResult Post([FromBody]UnitDetails value)
        {
            try
            {
                _context.UnitDetails.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]UnitDetails value)
        {
            try
            {
                var existingUnit = _context.UnitDetails.Where(x => x.Uid == value.Uid).FirstOrDefault<UnitDetails>();
                if (existingUnit == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingUnit.Usname = value.Usname;
                existingUnit.Uname = value.Uname;
                existingUnit.Udesc = value.Udesc;
                _context.UnitDetails.Update(existingUnit);
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
                var existingUnit = _context.UnitDetails.Where(x => x.Uid == id).FirstOrDefault<UnitDetails>();
                if (existingUnit != null)
                {
                    _context.UnitDetails.Remove(existingUnit);
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

        [Route("UnitList")]
        public IActionResult UnitList()
        {
            try
            {
                object unit;
                unit = _context.UnitDetails
                                    .OrderBy(c => c.Usname)
                                    .Select(x => new { Id = x.Uid, Value = x.Usname }).ToList();

                return Ok(unit);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
