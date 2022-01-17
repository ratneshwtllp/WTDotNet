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
    public class BrandController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public BrandController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<BrandDetails> Get()
        {
            return _context.BrandDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.BrandDetails.Where(x => x.BrandId == id).FirstOrDefault());
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

        [Route("GetBrandList")]
        public IActionResult GetBrandList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewBrandDetails.OrderByDescending(x => x.BrandId));
                else
                    return Ok(_context.ViewBrandDetails
                        .Where(x => x.BrandName.Contains(search) || x.BuyerName.Contains(search))
                        .OrderByDescending(x => x.BrandId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBrandId")]
        public IActionResult GetNewBrandId()
        {
            try
            {
                long BrandId = 0;
                var lastBrand = _context.BrandDetails.OrderBy(x => x.BrandId).LastOrDefault();
                BrandId = (lastBrand == null ? 0 : lastBrand.BrandId) + 1;
                return Ok(BrandId);
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
                var existingState = _context.BrandDetails.Where(x => x.BrandName == value).FirstOrDefault<BrandDetails>();
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
        public IActionResult Post([FromBody]BrandDetails value)
        {
            try
            {
                _context.BrandDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        public IActionResult Put([FromBody]BrandDetails value)
        {
            try
            {
                var existingBrand = _context.BrandDetails.Where(x => x.BrandId == value.BrandId).FirstOrDefault<BrandDetails>();
                if (existingBrand == null)
                {
                    return Ok("Not Found");
                }
                existingBrand.BrandName = value.BrandName;
                existingBrand.BrandDesc = value.BrandDesc;
                existingBrand.BuyerId = value.BuyerId;
                _context.BrandDetails.Update(existingBrand);
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
                var existingBrand = _context.BrandDetails.Where(x => x.BrandId == id).FirstOrDefault<BrandDetails>();
                if (existingBrand != null)
                {
                    _context.BrandDetails.Remove(existingBrand);
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
