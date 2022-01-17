using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.API
{
    [Route("api/[controller]")]
    public class FinishMetalController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public FinishMetalController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <FinishMetalDetails> Get()
        {
            return _context.FinishMetalDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.FinishMetalDetails.Where(x => x.Mfid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetFinishMetalList")]
        public IActionResult GetFinishMetalList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.FinishMetalDetails.OrderByDescending(x => x.Mfid));
                else
                    return Ok(_context.FinishMetalDetails
                        .Where(x => x.Mfname.Contains(search))
                        .OrderByDescending(x => x.Mfid));

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewFinishMetalId")]
        public IActionResult GetNewFinishMetalId()
        {
            try
            {
                long Mfid = 0;
                var lastrecord = _context.FinishMetalDetails.OrderBy(x => x.Mfid).LastOrDefault();
                Mfid = (lastrecord == null ? 0 : lastrecord.Mfid) + 1;
                return Ok(Mfid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]FinishMetalDetails value)
        {
            try
            {
                _context.FinishMetalDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]FinishMetalDetails value)
        {
            try
            {
                var existingFinish = _context.FinishMetalDetails.Find(value.Mfid);
                if (existingFinish == null)
                {
                    return Ok("Not Found");
                }
                existingFinish.Mfname = value.Mfname;
                existingFinish.Description = value.Description;
                _context.FinishMetalDetails.Update(existingFinish);
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
                var existingFinish = _context.FinishMetalDetails.Where(x => x.Mfname == value).FirstOrDefault<FinishMetalDetails>();
                if (existingFinish != null)
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
                var existingFinish = _context.FinishMetalDetails.Find(id);
                if (existingFinish != null)
                {
                    _context.FinishMetalDetails.Remove(existingFinish);
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
