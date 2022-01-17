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
    public class SampleIssueController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public SampleIssueController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("GetSampleTypeList")]
        public IActionResult GetSampleTypeList()
        {
            try
            {
                object sampletype;
                sampletype = _context.SampleType
                                        .OrderBy(c => c.SampleName)
                                        .Select(x => new { Id = x.SampleTypeId, Value = x.SampleName }).ToList();
                return Ok(sampletype);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        public IQueryable<SampleIssueDetails> Get()
        {
            return _context.SampleIssueDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewSampleIssue.Where(x => x.SIID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSampleIssueList")]
        public IActionResult GetSampleRoomList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSampleIssue.OrderByDescending(x => x.SIID));
                else
                    return Ok(_context.ViewSampleIssue
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.SampleName.Contains(search))
                        .OrderByDescending(x => x.SIID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSampleIssueId")]
        public IActionResult GetNewSampleIssueId()
        {
            try
            {
                long SIID = 0;
                var lastSample = _context.SampleIssueDetails.OrderBy(x => x.SIID).LastOrDefault();
                SIID = (lastSample == null ? 0 : lastSample.SIID) + 1;
                return Ok(SIID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssueNo")]
        public IActionResult GetNewIssueNo()
        {
            try
            {
                int IssueNo = 0;
                var lastSample = _context.SampleIssueDetails.OrderBy(x => x.IssueNo).LastOrDefault();
                IssueNo = (lastSample == null ? 0 : lastSample.IssueNo) + 1;
                return Ok(IssueNo);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]SampleIssueDetails value)
        {
            try
            {
                _context.SampleIssueDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]SampleIssueDetails value)
        {
            try
            {
                var existingSample = _context.SampleIssueDetails.Where(x => x.SIID == value.SIID).FirstOrDefault<SampleIssueDetails>();
                if (existingSample == null)
                {
                    return Ok("Not Found");
                }
                existingSample.FItemId = value.FItemId;
                existingSample.IssueQty = value.IssueQty;
                existingSample.IssueDate = value.IssueDate;
                existingSample.IssueNo = value.IssueNo;
                existingSample.Session_Year = value.Session_Year;
                existingSample.UserId = value.UserId;
                existingSample.SampleType = value.SampleType;
                existingSample.IssueTo = value.IssueTo;
                _context.SampleIssueDetails.Update(existingSample);
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
                var existingSample = _context.SampleIssueDetails.Where(x => x.SIID == id).FirstOrDefault<SampleIssueDetails>();
                if (existingSample != null)
                {
                    _context.SampleIssueDetails.Remove(existingSample);
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

        [Route("GetSampleRoomDetails/{id}")]
        public IActionResult GetSampleRoomDetails(long id)
        {
            try
            {
                return Ok(_context.ViewSampleRoom.Where(x => x.FItemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
