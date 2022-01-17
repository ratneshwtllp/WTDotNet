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
    public class PatternIssueController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PatternIssueController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<PatternIssueDetails> Get()
        {
            return _context.PatternIssueDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewPatternIssueDetails.Where(x => x.PIID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPatternIssueList")]
        public IActionResult GetPatternIssueList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewPatternIssueDetails.OrderByDescending(x => x.PIID));
                else
                    return Ok(_context.ViewPatternIssueDetails
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.ContractorName.Contains(search))
                        .OrderByDescending(x => x.PIID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPatternIssueId")]
        public IActionResult GetNewPatternIssueId()
        {
            try
            {
                long PIID = 0;
                var lastSample = _context.PatternIssueDetails.OrderBy(x => x.PIID).LastOrDefault();
                PIID = (lastSample == null ? 0 : lastSample.PIID) + 1;
                return Ok(PIID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPatternIssueNo")]
        public IActionResult GetNewPatternIssueNo()
        {
            try
            {
                int IssueNo = 0;
                var lastSample = _context.PatternIssueDetails.OrderBy(x => x.IssueNo).LastOrDefault();
                IssueNo = (lastSample == null ? 0 : lastSample.IssueNo) + 1;
                return Ok(IssueNo);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PatternIssueDetails value)
        {
            try
            {
                _context.PatternIssueDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]PatternIssueDetails value)
        {
            try
            {
                var existingPattern = _context.PatternIssueDetails.Where(x => x.PIID == value.PIID).FirstOrDefault<PatternIssueDetails>();
                if (existingPattern == null)
                {
                    return Ok("Not Found");
                }
                existingPattern.FItemId = value.FItemId;
                existingPattern.IssueQty = value.IssueQty;
                existingPattern.IssueDate = value.IssueDate;
                existingPattern.IssueNo = value.IssueNo;
                existingPattern.Session_Year = value.Session_Year;
                existingPattern.UserId = value.UserId;
                existingPattern.IssueTo = value.IssueTo;
                _context.PatternIssueDetails.Update(existingPattern);
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
                var existingSample = _context.PatternIssueDetails.Where(x => x.PIID == id).FirstOrDefault<PatternIssueDetails>();
                if (existingSample != null)
                {
                    _context.PatternIssueDetails.Remove(existingSample);
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

        [Route("GetPatternRoomDetails/{id}")]
        public IActionResult GetPatternRoomDetails(long id)
        {
            try
            {
                return Ok(_context.ViewPatternRoomDetails.Where(x => x.FItemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
