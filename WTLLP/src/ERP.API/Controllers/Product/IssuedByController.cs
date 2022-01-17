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
    public class IssuedByController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public IssuedByController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<IssuedByDetails> Get()
        {
            return _context.IssuedByDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.IssuedByDetails.Where(x => x.IssuedById == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerList")]
        public IActionResult GetBuyerList()
        {
            try
            {
                object Buyers;
                Buyers = _context.BuyerDetails
                                        .OrderBy(c => c.BuyerCode)
                                        .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(Buyers);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetIssuedByList")]
        public IActionResult GetIssuedByList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewIssuedByDetails.OrderByDescending(x => x.IssuedById));
                else
                    return Ok(_context.ViewIssuedByDetails
                        .Where(x => x.IssuedByName.Contains(search) || x.BuyerName.Contains(search))
                        .OrderByDescending(x => x.IssuedById));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssuedById")]
        public IActionResult GetNewIssuedById()
        {
            try
            {
                long IssuedById = 0;
                var lastIssuedBy = _context.IssuedByDetails.OrderBy(x => x.IssuedById).LastOrDefault();
                IssuedById = (lastIssuedBy == null ? 0 : lastIssuedBy.IssuedById) + 1;
                return Ok(IssuedById);
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
                var existingIssuedBy = _context.IssuedByDetails.Where(x => x.IssuedByName == value).FirstOrDefault<IssuedByDetails>();

                if (existingIssuedBy != null)
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
        public IActionResult Post([FromBody]IssuedByDetails value)
        {
            try
            {
                _context.IssuedByDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        public IActionResult Put([FromBody]IssuedByDetails value)
        {
            try
            {
                var existingIssuedBy = _context.IssuedByDetails.Where(x => x.IssuedById == value.IssuedById).FirstOrDefault<IssuedByDetails>();
                if (existingIssuedBy == null)
                {
                    return Ok("Not Found");
                }
                existingIssuedBy.IssuedByName = value.IssuedByName;
                existingIssuedBy.IssuedByDesc = value.IssuedByDesc;
                existingIssuedBy.BuyerId = value.BuyerId;
                _context.IssuedByDetails.Update(existingIssuedBy);
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
                var existingIssuedBy = _context.IssuedByDetails.Where(x => x.IssuedById == id).FirstOrDefault<IssuedByDetails>();
                if (existingIssuedBy != null)
                {
                    _context.IssuedByDetails.Remove(existingIssuedBy);
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

        [Route("GetIssuedByList/{buyerId}")]
        public IActionResult GetIssuedByList(int buyerId)
        {
            try
            {
                object IssuedBy;
                IssuedBy = _context.IssuedByDetails
                                    .Where(x => x.BuyerId == buyerId)
                                    .OrderBy(c => c.IssuedByName)
                                    .Select(x => new { Id = x.IssuedById, Value = x.IssuedByName }).ToList();
                return Ok(IssuedBy);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
