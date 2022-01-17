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
    public class DesignerController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public DesignerController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<DesignerDetails> Get()
        {
            return _context.DesignerDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.DesignerDetails.Where(x => x.DesignerId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetDesignerList")]
        public IActionResult GetDesignerList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.DesignerDetails.OrderByDescending(x => x.DesignerId));
                else
                    return Ok(_context.DesignerDetails
                        .Where(x => x.DesignerName.Contains(search))
                        .OrderByDescending(x => x.DesignerId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewDesignerId")]
        public IActionResult GetNewDesignerId()
        {
            try
            {
                long DesignerId = 0;
                var lastDesigner = _context.DesignerDetails.OrderBy(x => x.DesignerId).LastOrDefault();
                DesignerId = (lastDesigner == null ? 0 : lastDesigner.DesignerId) + 1;
                return Ok(DesignerId);
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
                var existingDesigner = _context.DesignerDetails.Where(x => x.DesignerName == value).FirstOrDefault<DesignerDetails>();
                if (existingDesigner != null)
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
        public IActionResult Post([FromBody]DesignerDetails value)
        {
            try
            {
                _context.DesignerDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]DesignerDetails value)
        {
            try
            {
                var existingDesigner = _context.DesignerDetails.Where(x => x.DesignerId == value.DesignerId).FirstOrDefault<DesignerDetails>();
                if (existingDesigner == null)
                {
                    return Ok("Not Found");
                }
                existingDesigner.DesignerName = value.DesignerName;
                existingDesigner.DesignerDesc = value.DesignerDesc;
                _context.DesignerDetails.Update(existingDesigner);
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
                var existingDesigner = _context.DesignerDetails.Where(x => x.DesignerId == id).FirstOrDefault<DesignerDetails>();
                if (existingDesigner != null)
                {
                    _context.DesignerDetails.Remove(existingDesigner);
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


        [Route("DesignerList")]
        public IActionResult DesignerList()
        {
            try
            {
                object Designer;
                Designer = _context.DesignerDetails
                                    .OrderBy(c => c.DesignerName)
                                    .Select(x => new { Id = x.DesignerId, Value = x.DesignerName }).ToList();
                return Ok(Designer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
