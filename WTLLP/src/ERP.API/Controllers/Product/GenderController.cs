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
    public class GenderController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public GenderController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<GenderDetails> Get()
        {
            return _context.GenderDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.GenderDetails.Where(x => x.GenderId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [Route("GetGenderList")]
        public IActionResult GetGenderList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.GenderDetails.OrderByDescending(x => x.GenderId));
                else
                    return Ok(_context.GenderDetails
                        .Where(x => x.GenderName.Contains(search)) 
                        .OrderByDescending(x => x.GenderId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetListGender")]
        public IActionResult GetListGender()
        {
            try
            {
                object genders;
                genders = _context.GenderDetails
                                        .OrderBy(c => c.GenderName)
                                        .Select(x => new { Id = x.GenderId, Value = x.GenderName }).ToList();
                return Ok(genders);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGenderId")]
        public IActionResult GetNewGenderId()
        {
            try
            {
                long GenderId = 0;
                var lastgender = _context.GenderDetails.OrderBy(x => x.GenderId).LastOrDefault();
                GenderId = (lastgender == null ? 0 : lastgender.GenderId) + 1;
                return Ok(GenderId);
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
                var existingGender = _context.GenderDetails.Where(x => x.GenderName == value).FirstOrDefault<GenderDetails>();
                if (existingGender != null)
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
        public IActionResult Post([FromBody]GenderDetails value)
        {
            try
            {
                _context.GenderDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]GenderDetails value)
        {
            try
            {
                var existingGender = _context.GenderDetails.Where(x => x.GenderId == value.GenderId).FirstOrDefault<GenderDetails>();
                if (existingGender == null)
                {
                    return Ok("Not Found");
                }
                existingGender.GenderName = value.GenderName;
                existingGender.GenderDesc = value.GenderDesc;
                _context.GenderDetails.Update(existingGender);
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
                var existingGender = _context.GenderDetails.Where(x => x.GenderId == id).FirstOrDefault<GenderDetails>();
                if (existingGender != null)
                {
                    _context.GenderDetails.Remove(existingGender);
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
