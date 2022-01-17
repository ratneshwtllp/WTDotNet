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
    public class HRGradeController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public HRGradeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_GradeDetails> Get()
        {
            return _context.HR_GradeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_GradeDetails.Where(x => x.GradeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetGradeList")]
        public IActionResult GetGradeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_GradeDetails.OrderByDescending(x => x.GradeId));
                else
                    return Ok(_context.HR_GradeDetails
                        .Where(x => x.GradeCode.Contains(search) || x.GradeName.Contains(search))
                        .OrderByDescending(x => x.GradeId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GradeList")]
        public IActionResult GradeList()
        {
            try
            {
                object Grade;
                Grade = _context.HR_GradeDetails
                                        .OrderBy(c => c.GradeName)
                                        .Select(x => new { Id = x.GradeId, Value = x.GradeName }).ToList();
                return Ok(Grade);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGradeId")]
        public IActionResult GetNewGradeId()
        {
            try
            {
                int GradeId = 0;
                var lastSample = _context.HR_GradeDetails.OrderBy(x => x.GradeId).LastOrDefault();
                GradeId = (lastSample == null ? 0 : lastSample.GradeId) + 1;
                return Ok(GradeId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_GradeDetails value)
        {
            try
            {
                _context.HR_GradeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_GradeDetails value)
        {
            try
            {
                var existingGrade = _context.HR_GradeDetails.Where(x => x.GradeId == value.GradeId).FirstOrDefault<HR_GradeDetails>();
                if (existingGrade == null)
                {
                    return Ok("Not Found");
                }
                existingGrade.GradeCode = value.GradeCode;
                existingGrade.GradeName = value.GradeName;
                existingGrade.SessionYear = value.SessionYear;
                existingGrade.UserId = value.UserId;
                _context.HR_GradeDetails.Update(existingGrade);
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
                var existingGrade = _context.HR_GradeDetails.Where(x => x.GradeId == id).FirstOrDefault<HR_GradeDetails>();
                if (existingGrade != null)
                {
                    _context.HR_GradeDetails.Remove(existingGrade);
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
