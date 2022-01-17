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
    public class TaxController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public TaxController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<TaxDetails> Get()
        {
            return _context.TaxDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.TaxDetails.Where(x => x.TaxId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("FillTax")]
        public IActionResult FillTax()
        {
            try
            {
                object tax;
                tax = _context.TaxDetails
                                        .OrderBy(c => c.TaxFullName)
                                        .Select(x => new { Id = x.TaxId, Value = x.TaxFullName }).ToList();
                return Ok(tax);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTaxList")]
        public IActionResult GetTaxList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.TaxDetails.OrderByDescending(x => x.TaxId));
                else
                    return Ok(_context.TaxDetails
                        .Where(x => x.TaxName.Contains(search) || x.TaxFullName.Contains(search))
                        .OrderByDescending(x => x.TaxId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewTaxId")]
        public IActionResult GetNewTaxId()
        {
            try
            {
                long TaxId = 0;
                var lastTax = _context.TaxDetails.OrderBy(x => x.TaxId).LastOrDefault();
                TaxId = (lastTax == null ? 0 : lastTax.TaxId) + 1;
                return Ok(TaxId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTaxRate/{id}")]
        public IActionResult GetTaxRate(int id)
        {
            try
            {
                double taxrate = 0;
                var Tax = _context.TaxDetails.Where(x => x.TaxId==id).LastOrDefault();
                taxrate = (Tax == null ? 0 : Tax.TaxPer);
                return Ok(taxrate);
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
                var existingTax = _context.TaxDetails.Where(x => x.TaxFullName == value).FirstOrDefault<TaxDetails>();
                if (existingTax != null)
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
        public IActionResult Post([FromBody]TaxDetails value)
        {
            try
            {
                _context.TaxDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]TaxDetails value)
        {
            try
            {
                var existingTax = _context.TaxDetails.Where(x => x.TaxId == value.TaxId).FirstOrDefault<TaxDetails>();
                if (existingTax == null)
                {
                    return Ok("Not Found");
                }
                existingTax.TaxName = value.TaxName;
                existingTax.TaxFullName = value.TaxFullName;
                existingTax.TaxPer = value.TaxPer;
                _context.TaxDetails.Update(existingTax);
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
                var existingTax = _context.TaxDetails.Where(x => x.TaxId == id).FirstOrDefault<TaxDetails>();
                if (existingTax != null)
                {
                    _context.TaxDetails.Remove(existingTax);
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
