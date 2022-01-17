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
    public class SizeController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public SizeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <SizeDetails> Get()
        {
            return _context.SizeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.SizeDetails.Where(x => x.SizeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetSizeList")]
        public IActionResult GetSizeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.SizeDetails.OrderByDescending(x => x.SizeId));
                else
                    return Ok(_context.SizeDetails.Where(x => x.SizeName.Contains(search)).OrderByDescending(x => x.SizeId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSizeId")]
        public IActionResult GetNewSizeId()
        {
            try
            {
                long sizeid = 0;
                var lastSize = _context.SizeDetails.OrderBy(x => x.SizeId).LastOrDefault();
                sizeid = (lastSize == null ? 0 : lastSize.SizeId) + 1;
                return Ok(sizeid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]SizeDetails value)
        {
            try
            {
                _context.SizeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]SizeDetails value)
        {
            try
            {
                var existingSize = _context.SizeDetails.Find(value.SizeId);
                if (existingSize == null)
                {
                    return Ok("Not Found");
                }
                existingSize.SizeName = value.SizeName;
                existingSize.SizeDesc = value.SizeDesc;
                _context.SizeDetails.Update(existingSize);
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
                var existingSize = _context.SizeDetails.Where(x => x.SizeName == value).FirstOrDefault<SizeDetails>();
                if (existingSize != null)
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
                var existingSize = _context.SizeDetails.Where(x => x.SizeId == id).FirstOrDefault<SizeDetails>();
                if (existingSize != null)
                {
                    _context.SizeDetails.Remove(existingSize);
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
