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
    public class SampleChangeRequestController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public SampleChangeRequestController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<Sample_Request> Get()
        {
            return _context.Sample_Request.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewSample_Request.Where(x => x.Request_Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetChangeRequestStatus/{Request_Id}")]
        public IActionResult GetChangeRequestStatus(long Request_Id)
        {
            try
            {
                var existingChangeRequest = _context.Sample_Request.Find(Request_Id);
                existingChangeRequest.Status = 1;
                existingChangeRequest.Change_On = DateTime.Now.Date;
                _context.Sample_Request.Update(existingChangeRequest);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetChangesCompleteList")]
        public IActionResult GetChangesCompleteList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSample_Request.Where(x => (x.Status == 1)).OrderByDescending(x => x.Change_On));
                else
                    return Ok(_context.ViewSample_Request.Where(x => (x.Status == 1) && (x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) ||
                    x.Code.Contains(search) || x.ChangeMessage.Contains(search) || x.MessageFrom.Contains(search))).OrderByDescending( x => x.Change_On));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSampleChangeRequestList")]
        public IActionResult GetSampleChangeRequestList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSample_Request.Where(x => (x.Status == 0))
                        .OrderByDescending(x => x.RequestDate));
                else
                    return Ok(_context.ViewSample_Request
                        .Where(x => (x.Status == 0 )  && (x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.ChangeMessage.Contains(search) || x.MessageFrom.Contains(search)))
                        .OrderByDescending(x => x.RequestDate));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSampleChangeRequestId")]
        public IActionResult GetNewSampleChangeRequestId()
        {
            try
            {
                long Request_Id = 0;
                var lastSample = _context.Sample_Request.OrderBy(x => x.Request_Id).LastOrDefault();
                Request_Id = (lastSample == null ? 0 : lastSample.Request_Id) + 1;
                return Ok(Request_Id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Sample_Request value)
        {
            try
            {
                _context.Sample_Request.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]Sample_Request value)
        {
            try
            {
                var existingSampleCR = _context.Sample_Request.Where(x => x.Request_Id == value.Request_Id).FirstOrDefault<Sample_Request>();
                if (existingSampleCR == null)
                {
                    return Ok("Not Found");
                }
                existingSampleCR.FItem_Id = value.FItem_Id;
                existingSampleCR.ChangeMessage = value.ChangeMessage;
                existingSampleCR.MessageFrom = value.MessageFrom;
                existingSampleCR.RequestDate = value.RequestDate;
                existingSampleCR.ChageUpTo = value.ChageUpTo;
                existingSampleCR.Status = value.Status;
                existingSampleCR.session_year = value.session_year;
                existingSampleCR.UserID = value.UserID;
                existingSampleCR.SMS_Bulk = value.SMS_Bulk;
                existingSampleCR.Change_On = value.Change_On;
                existingSampleCR.Change_Read_On = value.Change_Read_On;
                _context.Sample_Request.Update(existingSampleCR);
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
                var existingSample = _context.Sample_Request.Where(x => x.Request_Id == id).FirstOrDefault<Sample_Request>();
                if (existingSample != null)
                {
                    _context.Sample_Request.Remove(existingSample);
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
