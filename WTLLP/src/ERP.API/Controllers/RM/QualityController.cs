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
    public class QualityController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public QualityController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<QualityDetails> Get()
        {
            return _context.QualityDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.QualityDetails.Where(x => x.QualityId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetQualityList")]
        public IActionResult GetQualityList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.QualityDetails.OrderByDescending(x => x.QualityId));
                }
                else
                {
                    return Ok(_context.QualityDetails
                        .Where(x => x.QualityName.Contains(search))
                        .OrderByDescending(x => x.QualityId));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewQualityId")]
        public IActionResult GetNewQualityId()
        {
            try
            {
                long QliId = 0;
                var lastQuality = _context.QualityDetails.OrderBy(x => x.QualityId).LastOrDefault();
                QliId = (lastQuality == null ? 0 : lastQuality.QualityId) + 1;
                return Ok(QliId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]QualityDetails value)
        {
            try
            {
                _context.QualityDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]QualityDetails value)
        {
            try
            {
                var existingQuality = _context.QualityDetails.Find(value.QualityId);
                if (existingQuality == null)
                {
                    return Ok("Not Found");
                }
                existingQuality.QualityName = value.QualityName;
                existingQuality.QualityDesc = value.QualityDesc;
                _context.QualityDetails.Update(existingQuality);
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
                var existingQuality = _context.QualityDetails.Where(x => x.QualityName == value).FirstOrDefault<QualityDetails>();
                if (existingQuality != null)
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
                var existingQuality = _context.QualityDetails.Find(id);
                if (existingQuality != null)
                {
                    _context.QualityDetails.Remove(existingQuality);
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
