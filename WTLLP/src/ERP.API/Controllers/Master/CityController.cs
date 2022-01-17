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
    public class CityController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public CityController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ViewCityDetails> Get()
        {
            return _context.ViewCityDetails.AsQueryable(); 
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewCityDetails.Where(x => x.CityId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCountryList")]
        public IActionResult GetCountryList()
        {
            try
            {
                object countries;
                countries = _context.CountryDetails
                                        .OrderBy(c => c.CountryName)
                                        .Select(x => new { Id = x.CountryId, Value = x.CountryName }).ToList();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStateList/{CountryId}")]
        public IActionResult GetStateList(int Countryid)
        {
            try
            {
                object states;
                states = _context.StateDetails
                                        .Where(x => x.CountryId == Countryid)
                                        .OrderBy(c => c.StateName)
                                        .Select(x => new { Id = x.StateId, Value = x.StateName }).ToList();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCityList")]
        public IActionResult GetCityList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewCityDetails.OrderByDescending(x => x.CityId));
                else
                    return Ok(_context.ViewCityDetails
                        .Where(x => x.CountryName.Contains(search) || x.StateName.Contains(search) || x.CityName.Contains(search))
                        .OrderByDescending(x => x.CityId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCityId")]
        public IActionResult GetNewCityId()
        {
            try
            {
                long CityId = 0;
                var lastCity = _context.CityDetails.OrderBy(x => x.CityId).LastOrDefault();
                CityId = (lastCity == null ? 0 : lastCity.CityId) + 1;
                return Ok(CityId);
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
                var existingCity = _context.CityDetails.Where(x => x.CityName == value).FirstOrDefault<CityDetails>();
                if (existingCity != null)
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
        public IActionResult Post([FromBody]CityDetails value)
        {
            try
            {
                _context.CityDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]CityDetails value)
        {
            try
            {
                var existingCity = _context.CityDetails.Where(x => x.CityId == value.CityId).FirstOrDefault<CityDetails>();
                if (existingCity == null)
                {
                    return Ok("Not Found");
                }
                existingCity.CityName = value.CityName;
                existingCity.StateId = value.StateId;
                _context.CityDetails.Update(existingCity);
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
                var existingCity = _context.CityDetails.Where(x => x.CityId == id).FirstOrDefault<CityDetails>();
                if (existingCity != null)
                {
                    _context.CityDetails.Remove(existingCity);
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

        [Route("GetListCity/{stateid}")]
        public IActionResult GetListCity(int stateid)
        {
            try
            {
                object cities;
                cities = _context.CityDetails
                                    .Where(x => x.StateId == stateid)
                                    .OrderBy(c => c.CityName)
                                    .Select(x => new { Id = x.CityId, Value = x.CityName }).ToList();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
