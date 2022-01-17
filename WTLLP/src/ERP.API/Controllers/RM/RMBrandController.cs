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
    public class RMBrandController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public RMBrandController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<RMBrandDetails> Get()
        {
            return _context.RMBrandDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.RMBrandDetails.Where(x => x.RMBrandId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetBuyerList")]
        //public IActionResult GetBuyerList()
        //{
        //    try
        //    {
        //        object Buyers;
        //        Buyers = _context.BuyerDetails
        //                                .OrderBy(c => c.BuyerCode)
        //                                .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
        //        return Ok(Buyers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("ListRMBrand")]
        public IActionResult ListRMBrand(string rmb)
        {
            try
            {
                object ListRMBrand; 
                if (string.IsNullOrEmpty(rmb))
                {
                    ListRMBrand = _context.RMBrandDetails
                                        .OrderBy(c => c.RMBrandCode)
                                        .Select(x => new { Id = x.RMBrandId, Value = x.RMBrandCode }).ToList();
                }
                else
                {
                    ListRMBrand = _context.RMBrandDetails
                                        .Where(x => x.RMBrandCode.Contains(rmb))
                                        .OrderBy(c => c.RMBrandCode)
                                        .Select(x => new { Id = x.RMBrandId, Value = x.RMBrandCode }).ToList();
                }
                return Ok(ListRMBrand);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMBrandList")]
        public IActionResult GetRMBrandList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.RMBrandDetails.OrderByDescending(x => x.RMBrandId));
                else
                    return Ok(_context.RMBrandDetails
                        .Where(x => x.RMBrandName.Contains(search))
                        .OrderByDescending(x => x.RMBrandId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMBrandId")]
        public IActionResult GetNewRMBrandId()
        {
            try
            {
                long RMBrandId = 0;
                var lastRMBrand = _context.RMBrandDetails.OrderBy(x => x.RMBrandId).LastOrDefault();
                RMBrandId = (lastRMBrand == null ? 0 : lastRMBrand.RMBrandId) + 1;
                return Ok(RMBrandId);
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
                var existingState = _context.RMBrandDetails.Where(x => x.RMBrandName == value).FirstOrDefault<RMBrandDetails>();
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
        public IActionResult Post([FromBody]RMBrandDetails value)
        {
            try
            {
                _context.RMBrandDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]RMBrandDetails value)
        {
            try
            {
                var existingRMBrand = _context.RMBrandDetails.Where(x => x.RMBrandId == value.RMBrandId).FirstOrDefault<RMBrandDetails>();
                if (existingRMBrand == null)
                {
                    return Ok("Not Found");
                }
                existingRMBrand.RMBrandName = value.RMBrandName;
                existingRMBrand.RMBrandCode = value.RMBrandCode;
                existingRMBrand.RMBrandDesc = value.RMBrandDesc;
                //existingBrand.RMBuyerId = value.RMBuyerId;
                _context.RMBrandDetails.Update(existingRMBrand);
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
                var existingRMBrand = _context.RMBrandDetails.Where(x => x.RMBrandId == id).FirstOrDefault<RMBrandDetails>();
                if (existingRMBrand != null)
                {
                    _context.RMBrandDetails.Remove(existingRMBrand);
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
