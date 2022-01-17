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
    public class MetalPart : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public MetalPart(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<MetalPartDetails> Get()
        {
            return _context.MetalPartDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.MetalPartDetails.Where(x => x.MepId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMetalPartList")]
        public IActionResult GetMetalPartList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.MetalPartDetails.OrderByDescending(x => x.MepId));
                }
                else
                {
                    return Ok(_context.MetalPartDetails
                        .Where(x => x.MepName.Contains(search))
                        .OrderByDescending(x => x.MepId));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewMetalPartId")]
        public IActionResult GetNewMetalPartId()
        {
            try
            {
                long MepId = 0;
                var lastMetalPart = _context.MetalPartDetails.OrderBy(x => x.MepId).LastOrDefault();
                MepId = (lastMetalPart == null ? 0 : lastMetalPart.MepId) + 1;
                return Ok(MepId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MetalPartDetails value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                var existingMetalPart = _context.MetalPartDetails.Where(s => s.MepId == value.MepId).FirstOrDefault<MetalPartDetails>();

                if (existingMetalPart != null)
                {
                    existingMetalPart.MepName = value.MepName;
                    existingMetalPart.MepDesc = value.MepDesc;
                    _context.SaveChanges();
                }
                else
                {
                    _context.MetalPartDetails.Add(value);
                    _context.SaveChanges();
                }
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]MetalPartDetails value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                var existingMetalPart = _context.MetalPartDetails.Where(x => x.MepId == value.MepId).FirstOrDefault<MetalPartDetails>();
                if (existingMetalPart != null)
                {
                    existingMetalPart.MepName = value.MepName;
                    existingMetalPart.MepDesc = value.MepDesc;
                    _context.MetalPartDetails.Update(existingMetalPart);
                    _context.SaveChanges();
                    return Ok("Record Updated");
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
                var existingMetalPart = _context.MetalPartDetails.Where(x => x.MepName == value).FirstOrDefault<MetalPartDetails>();
                if (existingMetalPart != null)
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

        //[HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingMetalPart = _context.MetalPartDetails.Where(x => x.MepId == id).FirstOrDefault<MetalPartDetails>();
                if (existingMetalPart != null)
                {
                    _context.MetalPartDetails.Remove(existingMetalPart);
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
