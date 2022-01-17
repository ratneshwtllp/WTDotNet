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
    public class StateController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public StateController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<StateDetails> Get()
        {
            return _context.StateDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.StateDetails.Where(x => x.StateId == id).FirstOrDefault());
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

        [Route("GetStateList")]
        public IActionResult GetStateList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewStateDetails.OrderByDescending(x => x.StateId));
                else
                    return Ok(_context.ViewStateDetails
                        .Where(x => x.CountryName.Contains(search) || x.StateName.Contains(search) || x.StateCode.Contains(search))
                        .OrderByDescending(x => x.StateId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStateId")]
        public IActionResult GetNewStateId()
        {
            try
            {
                long StateId = 0;
                var lastState = _context.StateDetails.OrderBy(x => x.StateId).LastOrDefault();
                StateId = (lastState == null ? 0 : lastState.StateId) + 1;
                return Ok(StateId);
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
                var existingState = _context.StateDetails.Where(x => x.StateName == value).FirstOrDefault<StateDetails>();
                if (existingState != null)
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
        public IActionResult Post([FromBody]StateDetails value)
        {
            try
            {
                _context.StateDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        public IActionResult Put([FromBody]StateDetails value)
        {
            try
            {
                var existingState = _context.StateDetails.Where(x => x.StateId == value.StateId).FirstOrDefault<StateDetails>();
                if (existingState == null)
                {
                    return Ok("Not Found");
                }
                existingState.StateName = value.StateName;
                existingState.StateCode = value.StateCode;
                existingState.CountryId = value.CountryId;
                _context.StateDetails.Update(existingState);
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
                var existingState = _context.StateDetails.Where(x => x.StateId == id).FirstOrDefault<StateDetails>();
                if (existingState != null)
                {
                    _context.StateDetails.Remove(existingState);
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

        [Route("GetListState")]
        public IActionResult GetListState()
        {
            try
            {
                object states;
                states = _context.StateDetails
                                        .OrderBy(c => c.StateName)
                                        .Select(x => new { Id = x.StateId, Value = x.StateName }).ToList();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
