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
    public class CountryController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public CountryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<CountryDetails> Get()
        {
            return _context.CountryDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CountryDetails.Where(x => x.CountryId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCountryList")]
        public IActionResult GetCountryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.CountryDetails.OrderByDescending(x => x.CountryId));
                else
                    return Ok(_context.CountryDetails
                        .Where(x => x.CountryName.Contains(search))
                        .OrderByDescending(x => x.CountryId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCountryId")]
        public IActionResult GetNewCountryId()
        {
            try
            {
                long CountryId = 0;
                var lastCountry = _context.CountryDetails.OrderBy(x => x.CountryId).LastOrDefault();
                CountryId = (lastCountry == null ? 0 : lastCountry.CountryId) + 1;
                return Ok(CountryId);
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
                var existingCountry = _context.CountryDetails.Where(x => x.CountryName == value).FirstOrDefault<CountryDetails>();
                if (existingCountry != null)
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
        public IActionResult Post([FromBody]CountryDetails value)
        {
            try
            {
                _context.CountryDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]CountryDetails value)
        {
            try
            {
                var existingCountry = _context.CountryDetails.Where(x => x.CountryId == value.CountryId).FirstOrDefault<CountryDetails>();
                if (existingCountry == null)
                {
                    return Ok("Not Found");
                }
                existingCountry.CountryName = value.CountryName;
                _context.CountryDetails.Update(existingCountry);
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
                var existingCountry = _context.CountryDetails.Where(x => x.CountryId == id).FirstOrDefault<CountryDetails>();
                if (existingCountry != null)
                {
                    _context.CountryDetails.Remove(existingCountry);
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
