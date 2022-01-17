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
    public class ClothController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ClothController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ClothDetails> Get()
        {
            return _context.ClothDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ClothDetails.Where(x => x.Chid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetClothList")]
        public IActionResult GetClothList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ClothDetails.OrderByDescending(x => x.Chid));
                else
                    return Ok(_context.ClothDetails
                        .Where(x => x.Chname.Contains(search))
                        .OrderByDescending(x => x.Chid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewClothID")]
        public IActionResult GetNewClothID()
        {
            try
            {
                long Chid = 0;
                var lastCloth = _context.ClothDetails.OrderBy(x => x.Chid).LastOrDefault();
                Chid = (lastCloth == null ? 0 : lastCloth.Chid) + 1;
                return Ok(Chid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ClothDetails value)
        {
            try
            {
                _context.ClothDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ClothDetails value)
        {
            try
            {
                var existingCloth = _context.ClothDetails.Where(x => x.Chid == value.Chid).FirstOrDefault<ClothDetails>();
                if (existingCloth == null)
                {
                    return Ok("Not Found");
                }
                existingCloth.Chname = value.Chname;
                existingCloth.Description = value.Description;
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
                var existingCloth = _context.ClothDetails.Where(x => x.Chname == value).FirstOrDefault<ClothDetails>();
                if (existingCloth != null)
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
                var existingCloth = _context.ClothDetails.Where(x => x.Chid == id).FirstOrDefault<ClothDetails>();
                if (existingCloth != null)
                {
                    _context.ClothDetails.Remove(existingCloth);
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
