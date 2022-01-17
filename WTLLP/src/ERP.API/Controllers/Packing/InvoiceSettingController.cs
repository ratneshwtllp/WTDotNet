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
    public class InvoiceSettingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public InvoiceSettingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("GetInvoiceSettingDetails")]
        public IActionResult GetInvoiceSettingDetails()
        {
            try
            {
                return Ok(_context.InvoiceSettingDetails.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]InvoiceSettingDetails value)
        {
            try
            {
                _context.InvoiceSettingDetails.Add(value);
                _context.SaveChanges();
                return Ok("Invoice Setting Details Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]InvoiceSettingDetails value)
        {
            try
            {
                var existinginvoicesetting = _context.InvoiceSettingDetails.Where(x => x.InvoiceSettingID == value.InvoiceSettingID).FirstOrDefault<InvoiceSettingDetails>();
                if (existinginvoicesetting == null)
                {
                    return Ok("Not Found");
                }
                existinginvoicesetting.Declaration1 = value.Declaration1;
                existinginvoicesetting.Declaration2 = value.Declaration2;
                existinginvoicesetting.Declaration3 = value.Declaration3;
                existinginvoicesetting.InvoicePrefix = value.InvoicePrefix;
                existinginvoicesetting.Prop_authorize = value.Prop_authorize;
                existinginvoicesetting.BankDetails = value.BankDetails;
                _context.InvoiceSettingDetails.Update(existinginvoicesetting);
                _context.SaveChanges();
                return Ok("Invoice Setting Details Updated.");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
