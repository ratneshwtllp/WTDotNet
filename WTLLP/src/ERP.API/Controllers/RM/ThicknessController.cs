using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ThicknessController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ThicknessController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ThicknessDetails> Get()
        {
            return _context.ThicknessDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ThicknessDetails.Where(x => x.Thid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetThicknessList")]
        public IActionResult GetThicknessList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ThicknessDetails.OrderByDescending(x => x.Thid));
                else
                    return Ok(_context.ThicknessDetails
                        .Where(x => x.Thname.Contains(search))
                        .OrderByDescending(x => x.Thid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewThicknessId")]
        public IActionResult GetNewThicknessId()
        {
            try
            {
                long id = 0;
                var lastThickness = _context.ThicknessDetails.OrderBy(x => x.Thid).LastOrDefault();
                id = (lastThickness == null ? 0 : lastThickness.Thid) + 1;
                return Ok(id);
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
                var existingThickness = _context.ThicknessDetails.Where(x => x.Thname == value).FirstOrDefault<ThicknessDetails>();
                if (existingThickness != null)
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
        public IActionResult Post([FromBody]ThicknessDetails value)
        {
            try
            {
                _context.ThicknessDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ThicknessDetails value)
        {
            try
            {
                var existingThickness = _context.ThicknessDetails.Find(value.Thid);
                if (existingThickness == null)
                {
                    return Ok("Not Found");
                }
                existingThickness.Thname = value.Thname;
                existingThickness.Thdesc = value.Thdesc;
                _context.ThicknessDetails.Update(existingThickness);
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
                var existingThickness = _context.ThicknessDetails.Find(id);
                if (existingThickness != null)
                {
                    _context.ThicknessDetails.Remove(existingThickness);
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

        [Route("GetListThickness")]
        public IActionResult GetListThickness()
        {
            try
            {
                var thicknessDetails = _context.ThicknessDetails
                                            .OrderBy(c => c.Thname)
                                            .Select(x => new { Id = x.Thid, Value = x.Thname });
                return Ok(thicknessDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
