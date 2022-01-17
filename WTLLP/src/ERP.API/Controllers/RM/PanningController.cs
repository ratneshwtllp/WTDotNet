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
    public class PanningController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public PanningController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<PanningDetails> Get()
        {
            return _context.PanningDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.PanningDetails.Where(x => x.PanningId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPanningList")]
        public IActionResult GetPanningList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.PanningDetails.OrderByDescending(x => x.PanningId));
                }
                else
                {
                    return Ok(_context.PanningDetails
                        .Where(x => x.PanningName.Contains(search))
                        .OrderByDescending(x => x.PanningId));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPanningId")]
        public IActionResult GetNewPanningId()
        {
            try
            {
                long PanningId = 0;
                var lastPanning = _context.PanningDetails.OrderBy(x => x.PanningId).LastOrDefault();
                PanningId = (lastPanning == null ? 0 : lastPanning.PanningId) + 1;
                return Ok(PanningId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PanningDetails value)
        {
            try
            {
                _context.PanningDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]PanningDetails value)
        {
            try
            {
                var existingPanning = _context.PanningDetails.Find(value.PanningId);
                if (existingPanning == null)
                {
                    return Ok("Not Found");
                }
                existingPanning.PanningName = value.PanningName;
                existingPanning.PanningDesc = value.PanningDesc;
                _context.PanningDetails.Update(existingPanning);
                _context.SaveChanges();
                return Ok("Record Updated");
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
                var existingPanning = _context.PanningDetails.Where(x => x.PanningName == value).FirstOrDefault<PanningDetails>();
                if (existingPanning != null)
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

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingPanning = _context.PanningDetails.Find(id);
                if (existingPanning != null)
                {
                    _context.PanningDetails.Remove(existingPanning);
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
