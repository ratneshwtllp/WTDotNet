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
    public class HSNController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public HSNController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ViewHSNDetails> Get()
        {
            return _context.ViewHSNDetails.AsQueryable(); 
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewHSNDetails.Where(x => x.HSNId  == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTaxList")]
        public IActionResult GetTaxList()
        {
            try
            {
                object tax;
                tax = _context.TaxDetails 
                                        .OrderBy(c => c.TaxFullName )
                                        .Select(x => new { Id = x.TaxId , Value = x.TaxFullName  }).ToList();
                return Ok(tax);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("HSNList")]
        public IActionResult HSNList()
        {
            try
            {
                object tax;
                tax = _context.HSNDetails
                                        .OrderBy(c => c.HSNCode)
                                        .Select(x => new { Id = x.HSNId, Value = x.HSNCode  + " "  + x.Remark }).ToList();
                return Ok(tax);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetHSNList")]
        public IActionResult GetHSNList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewHSNDetails.OrderByDescending(x => x.HSNId ));
                else
                    return Ok(_context.ViewHSNDetails
                        .Where(x => x.HSNCode .Contains(search) || x.TaxFullName .Contains(search) || x.TaxName .Contains(search))
                        .OrderByDescending(x => x.HSNId ));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewHSNId")]
        public IActionResult GetNewHSNId()
        {
            try
            {
                long HSNMapId = 0;
                var lastHSNMap = _context.HSNDetails.OrderBy(x => x.HSNId ).LastOrDefault();
                HSNMapId = (lastHSNMap == null ? 0 : lastHSNMap.HSNId) + 1;
                return Ok(HSNMapId);
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
                var existingHSN = _context.HSNDetails.Where(x => x.HSNCode  == value).FirstOrDefault<HSNDetails  >();
                if (existingHSN != null)
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
        public IActionResult Post([FromBody]HSNDetails value)
        {
            try
            {
                _context.HSNDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HSNDetails value)
        {
            try
            {
                var existingHSN = _context.HSNDetails.Where(x => x.HSNId == value.HSNId ).FirstOrDefault<HSNDetails>();
                if (existingHSN == null)
                {
                    return Ok("Not Found");
                }
                existingHSN.TaxId  = value.TaxId ;
                existingHSN.HSNCode  = value.HSNCode ;
                existingHSN.Remark  = value.Remark;
                _context.HSNDetails.Update(existingHSN);
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
                var existingHSN = _context.HSNDetails.Where(x => x.HSNId  == id).FirstOrDefault<HSNDetails>();
                if (existingHSN != null)
                {
                    _context.HSNDetails.Remove(existingHSN);
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
