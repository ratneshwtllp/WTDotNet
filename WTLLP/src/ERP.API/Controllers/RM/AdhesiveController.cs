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
    public class AdhesiveController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public AdhesiveController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <AdhesiveDetails> Get()
        {
            return _context.AdhesiveDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.AdhesiveDetails.Where(x => x.ADID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetAdhesiveList")]
        public IActionResult GetAdhesiveList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.AdhesiveDetails.OrderByDescending(x => x.ADID));
                else
                    return Ok(_context.AdhesiveDetails
                        .Where(x => x.ADName.Contains(search))
                        .OrderByDescending(x => x.ADID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewAdhesiveID")]
        public IActionResult GetNewAdhesiveID()
        {
            try
            {
                long ADID = 0;
                var lastAdhesive = _context.AdhesiveDetails.OrderBy(x => x.ADID).LastOrDefault();
                ADID = (lastAdhesive == null ? 0 : lastAdhesive.ADID) + 1;
                return Ok(ADID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdhesiveDetails value)
        {
            try
            {
                _context.AdhesiveDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]AdhesiveDetails value)
        {
            try
            {
                var existingAdhesive = _context.AdhesiveDetails.Find(value.ADID);
                if (existingAdhesive == null)
                {
                    return Ok("Not Found");
                }
                existingAdhesive.ADName = value.ADName;
                existingAdhesive.Description = value.Description;
                _context.AdhesiveDetails.Update(existingAdhesive);
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
                var existingAdhesive = _context.AdhesiveDetails.Where(x => x.ADName == value).FirstOrDefault<AdhesiveDetails>();
                if (existingAdhesive != null)
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
                var existingAdhesive = _context.AdhesiveDetails.Where(x => x.ADID == id).FirstOrDefault<AdhesiveDetails>();
                if (existingAdhesive != null)
                {
                    _context.AdhesiveDetails.Remove(existingAdhesive);
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
