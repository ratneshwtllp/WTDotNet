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
    public class StructureController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public StructureController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<StructureDetails> Get()
        {
            return _context.StructureDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.StructureDetails.Where(x => x.Tsid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStructureList")]
        public IActionResult GetStructureList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.StructureDetails.OrderByDescending(x => x.Tsid));
                }
                else
                {
                    return Ok(_context.StructureDetails
                        .Where(x => x.Tsname.Contains(search))
                        .OrderByDescending(x => x.Tsid));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStructureid")]
        public IActionResult GetNewStructureid()
        {
            try
            {
                long Tsid = 0;
                var lastStructure = _context.StructureDetails.OrderBy(x => x.Tsid).LastOrDefault();
                Tsid = (lastStructure == null ? 0 : lastStructure.Tsid) + 1;
                return Ok(Tsid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]StructureDetails value)
        {
            try
            {
                _context.StructureDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]StructureDetails value)
        {
            try
            {
                var existingStructure = _context.StructureDetails.Find(value.Tsid);
                if (existingStructure == null)
                {
                    return Ok("Not Found");
                }
                existingStructure.Tsname = value.Tsname;
                existingStructure.Tsdesc = value.Tsdesc;
                _context.StructureDetails.Update(existingStructure);
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
                var existingStructure = _context.StructureDetails.Where(x => x.Tsname == value).FirstOrDefault<StructureDetails>();
                if (existingStructure != null)
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
                var existingStructure = _context.StructureDetails.Find(id);
                if (existingStructure != null)
                {
                    _context.StructureDetails.Remove(existingStructure);
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
