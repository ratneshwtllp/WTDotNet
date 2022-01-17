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
    public class ConsigneeController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ConsigneeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ConsigneeDetails> Get()
        {
            return _context.ConsigneeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ConsigneeDetails.Where(x => x.ConsigneeId == id).FirstOrDefault());

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

        [Route("GetCityList/{StateId}")]
        public IActionResult GetCityList(int Stateid)
        {
            try
            {
                object cities;
                cities = _context.CityDetails
                                        .Where(x => x.StateId == Stateid)
                                        .OrderBy(c => c.CityName)
                                        .Select(x => new { Id = x.CityId, Value = x.CityName }).ToList();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetConsigneeList")]
        public IActionResult GetConsigneeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ConsigneeDetails.OrderByDescending(x => x.ConsigneeId));
                else
                    return Ok(_context.ConsigneeDetails
                        .Where(x => x.ConsigneeCode.Contains(search) || x.ConsigneeName.Contains(search) || x.ConsigneeAddress.Contains(search) || x.ConsigneePin.Contains(search) || x.ConsigneeEmail.Contains(search) || x.ConsigneeMobile.Contains(search) || x.ConsigneePhone.Contains(search))
                        .OrderByDescending(x => x.ConsigneeId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetConsigneeNameList")]
        public IActionResult GetConsigneeNameList()
        {
            try
            {
                var consigneeList = _context.ConsigneeDetails.OrderByDescending(x => x.ConsigneeId).OrderBy(x => x.ConsigneeName)
                    .Select(x => new { Id = x.ConsigneeId, Value = x.ConsigneeName }).ToList();
                return Ok(consigneeList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewConsigneeId")]
        public IActionResult GetNewConsigneeId()
        {
            try
            {
                long ConsigneeId = 0;
                var lastConsignee = _context.ConsigneeDetails.OrderBy(x => x.ConsigneeId).LastOrDefault();
                ConsigneeId = (lastConsignee == null ? 0 : lastConsignee.ConsigneeId) + 1;
                return Ok(ConsigneeId);
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
                var existingConsignee = _context.ConsigneeDetails.Where(x => x.ConsigneeName == value).FirstOrDefault<ConsigneeDetails>();
                if (existingConsignee != null)
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
        public IActionResult Post([FromBody]ConsigneeDetails value)
        {
            try
            {
                _context.ConsigneeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]ConsigneeDetails value)
        {
            try
            {
                var existingConsignee = _context.ConsigneeDetails.Where(x => x.ConsigneeId == value.ConsigneeId).FirstOrDefault<ConsigneeDetails>();
                if (existingConsignee == null)
                {
                    return Ok("Not Found");
                }
                existingConsignee.ConsigneeCode = value.ConsigneeCode;
                existingConsignee.ConsigneeName = value.ConsigneeName;
                existingConsignee.ConsigneeAddress = value.ConsigneeAddress;
                existingConsignee.CountryId = value.CountryId;
                existingConsignee.StateId = value.StateId;
                existingConsignee.CityId = value.CityId;
                existingConsignee.ConsigneePin = value.ConsigneePin;
                existingConsignee.ConsigneeEmail = value.ConsigneeEmail;
                existingConsignee.ConsigneeMobile = value.ConsigneeMobile;
                existingConsignee.ConsigneePhone = value.ConsigneePhone;

                _context.ConsigneeDetails.Update(existingConsignee);
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
                var existingConsignee = _context.ConsigneeDetails.Where(x => x.ConsigneeId == id).FirstOrDefault<ConsigneeDetails>();
                if (existingConsignee != null)
                {
                    _context.ConsigneeDetails.Remove(existingConsignee);
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

        [Route("GetConsigneeName")]
        public ActionResult GetConsigneeName()
        {
            try
            {
                var consigneelist = _context.BuyerConsigneeDetails.Select(x => x.ConsigneeId).ToList();
                var obj = _context.ConsigneeDetails.Where(x => !consigneelist.Contains(x.ConsigneeId))
                    .OrderBy(x => x.ConsigneeName).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetConsigneeList/{buyerid}")]
        public ActionResult GetConsigneeList(int buyerid)
        {
            try
            {
                var consigneelist = _context.BuyerConsigneeDetails.Where(x => x.BuyerId != buyerid).Select(x => x.ConsigneeId).ToList();
                var obj = _context.ConsigneeDetails.Where(x => !consigneelist.Contains(x.ConsigneeId))
                    .OrderBy(x => x.ConsigneeName).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
