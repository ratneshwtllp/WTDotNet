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
    public class BankController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public BankController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<BankDetails> Get()
        {
            return _context.BankDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.BankDetails.Where(x => x.BankId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBankList")]
        public IActionResult GetBankList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.BankDetails.OrderByDescending(x => x.BankId));
                else
                    return Ok(_context.BankDetails
                        .Where(x => x.BankName.Contains(search))
                        .OrderByDescending(x => x.BankId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBankId")]
        public IActionResult GetNewBankId()
        {
            try
            {
                int BankId = 0;
                var lastBank = _context.BankDetails.OrderBy(x => x.BankId).LastOrDefault();
                BankId = (lastBank == null ? 0 : lastBank.BankId) + 1;
                return Ok(BankId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]BankDetails value)
        {
            try
            {
                _context.BankDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]BankDetails value)
        {
            try
            {
                var existingBank = _context.BankDetails.Where(x => x.BankId == value.BankId).FirstOrDefault<BankDetails>();
                if (existingBank == null)
                {
                    return Ok("Not Found");
                }
                existingBank.BankName = value.BankName;
                existingBank.SessionYear = value.SessionYear;
                existingBank.UserId = value.UserId;
                _context.BankDetails.Update(existingBank);
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
                var existingBank = _context.BankDetails.Where(x => x.BankId == id).FirstOrDefault<BankDetails>();
                if (existingBank != null)
                {
                    _context.BankDetails.Remove(existingBank);
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

        [Route("GetBankNameList")]
        public IActionResult GetBankNameList()
        {
            try
            {
                object bank;
                bank = _context.BankDetails
                                        .OrderBy(c => c.BankName)
                                        .Select(x => new { Id = x.BankId, Value = x.BankName }).ToList();
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
