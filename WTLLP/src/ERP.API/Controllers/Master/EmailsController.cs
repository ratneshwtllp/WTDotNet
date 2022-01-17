using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore; 

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public EmailsController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<EmailSettings> Get()
        { 
            return _context.EmailSettings
                        .OrderByDescending(x => x.EmailSettingsID)
                        .AsQueryable();                                       
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.EmailSettings.Where(x => x.EmailSettingsID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmailSetting/{emailtypeid}")]
        public IActionResult GetEmailSetting(int emailtypeid)
        {
            try
            {
                return Ok(_context.EmailSettings.Where(x => x.EmailTypeId == emailtypeid).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        [Route("GetViewEmailSettings")]
        public IQueryable<ViewEmailSettings> GetViewEmailSettings()
        { 
            return _context.ViewEmailSettings     
                .OrderByDescending(x => x.EmailTypeId).AsQueryable(); 
        }

        [Route("GetViewEmailSettingsSearch")]
        public IQueryable<ViewEmailSettings> GetViewEmailSettingsSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _context.ViewEmailSettings
                  .OrderByDescending(x => x.EmailTypeId).AsQueryable();
            }
            else
            {
                return _context.ViewEmailSettings.Where(x => x.EmailTypeName.Contains(search) || x.EmailAddress.Contains(search) || x.EmailSubject.Contains(search) || x.EmailHeader.Contains(search) || x.EmailFooter.Contains(search) || x.EmailTO.Contains(search))
                  .OrderByDescending(x => x.EmailTypeId).AsQueryable();
            }
        }

        [Route("GetEmailsList")]
        public IActionResult GetEmailsList()
        {
            try
            {
                var emList = _context.EmailType.OrderBy(x => x.EmailTypeId)
                    .Select(x => new { Id = x.EmailTypeId, Value = x.EmailTypeName })
                                    .ToList();
                return Ok(emList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewEmailID")]
        public IActionResult GetNewEmailID()
        {
            try
            {
                long emailID = 0;
                var lastemail = _context.EmailSettings.OrderBy(x => x.EmailSettingsID).LastOrDefault();
                emailID = (lastemail == null ? 0 : lastemail.EmailSettingsID) + 1;
                return Ok(emailID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [HttpPost]
        public IActionResult Post([FromBody]EmailSettings value)
        {
            try
            {
                _context.EmailSettings.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]EmailSettings value)
        {
            try
            {
                var existingEmail = _context.EmailSettings
                        .Where(x => x.EmailSettingsID == value.EmailSettingsID)
                        .SingleOrDefault();

                existingEmail.EmailAddress = value.EmailAddress;
                existingEmail.EmailPassword = value.EmailPassword;
                existingEmail.BCC = value.BCC;
                existingEmail.CC = value.CC;
                existingEmail.EmailHeader = value.EmailHeader;
                existingEmail.EmailFooter = value.EmailFooter; 
                existingEmail.EmailSubject = value.EmailSubject;
                existingEmail.OutGoingServer = value.OutGoingServer;
                existingEmail.Port = value.Port;
                existingEmail.EmailTO = value.EmailTO;
                _context.EmailSettings.Update(existingEmail);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingEmail = _context.EmailSettings.Where(x => x.EmailSettingsID == id).FirstOrDefault();
                if (existingEmail != null)
                {
                    _context.EmailSettings.Remove(existingEmail);
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

        [Route("GetNewEmailSendID")]
        public IActionResult GetNewEmailSendID()
        {
            try
            {
                long emailsendid = 0;
                var lastemailsend = _context.EmailSendDetails.OrderBy(x => x.EmailSendID).LastOrDefault();
                emailsendid = (lastemailsend == null ? 0 : lastemailsend.EmailSendID) + 1;
                return Ok(emailsendid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostEmailSend")]
        public IActionResult PostEmailSend([FromBody]EmailSendDetails value)
        {
            try
            {
                _context.EmailSendDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewSendEmail/{EmailSendID}")]
        public IActionResult GetViewSendEmail(long EmailSendID)
        {
            try
            {
                return Ok(_context.ViewSendEmail.Where(x => x.EmailSendID == EmailSendID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        

        //[Route("CheckDuplicate")]
        //public IActionResult CheckDuplicate(string value)
        //{
        //    try
        //    {
        //        var existingRMSubCategory = _context.RMSubCategory.Where(x => x.SubCategoryName == value).FirstOrDefault<RMSubCategory>();
        //        if (existingRMSubCategory != null)
        //        {
        //            return Ok(1);
        //        }
        //        else
        //        {
        //            return Ok(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}


        //[Route("GetNewPOSerial")]
        //public IActionResult GetNewPOSerial()
        //{
        //    try
        //    {
        //        int POSerial = 0;
        //        var lastPO = _context.Pomaster.OrderBy(x => x.POSerial).LastOrDefault();
        //        POSerial = (lastPO == null ? 0 : lastPO.POSerial) + 1;
        //        return Ok(POSerial);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}



    }
}
