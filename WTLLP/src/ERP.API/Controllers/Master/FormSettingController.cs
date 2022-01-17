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
    public class FormSettingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public FormSettingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<FormNumberSettingsDetails> Get()
        {
            return _context.FormNumberSettingsDetails.AsQueryable();
        }

        public IActionResult Put([FromBody]FormNumberSettingsDetails value)
        {
            try
            {
                var existingForm = _context.FormNumberSettingsDetails.Where(x => x.SNo == value.SNo).FirstOrDefault<FormNumberSettingsDetails>();
                if (existingForm == null)
                {
                    return Ok("Not Found");
                } 
                existingForm.Prefix = value.Prefix; 
                existingForm.StartingNumber = value.StartingNumber;
                existingForm.NoOfDigits = value.NoOfDigits;
                existingForm.DispalyNumber = value.DispalyNumber;
                existingForm.IsBatchOfRM = value.IsBatchOfRM;
                _context.FormNumberSettingsDetails.Update(existingForm);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetIsBatchOfRM")]
        public IActionResult GetIsBatchOfRM()
        {
            try
            {
                var obj = _context.FormNumberSettingsDetails.Where(x => x.FormName == "GRN").LastOrDefault();
                int IsBatchOfRM = obj.IsBatchOfRM;
                return Ok(IsBatchOfRM);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
