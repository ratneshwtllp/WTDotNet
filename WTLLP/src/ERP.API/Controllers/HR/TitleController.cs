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
    public class TitleController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public TitleController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_TitleDetails> Get()
        {
            return _context.HR_TitleDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_TitleDetails.Where(x => x.TitleId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTitleList")]
        public IActionResult GetTitleList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_TitleDetails.OrderByDescending(x => x.TitleId));
                else
                    return Ok(_context.HR_TitleDetails
                        .Where(x => x.TitleName.Contains(search))
                        .OrderByDescending(x => x.TitleId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("TitleList")]
        public IActionResult TitleList()
        {
            try
            {
                object Title;
                Title = _context.HR_TitleDetails
                                        .OrderBy(c => c.TitleName)
                                        .Select(x => new { Id = x.TitleId, Value = x.TitleName }).ToList();
                return Ok(Title);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewTitleId")]
        public IActionResult GetNewTitleId()
        {
            try
            {
                int TitleId = 0;
                var lastTitle = _context.HR_TitleDetails.OrderBy(x => x.TitleId).LastOrDefault();
                TitleId = (lastTitle == null ? 0 : lastTitle.TitleId) + 1;
                return Ok(TitleId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_TitleDetails value)
        {
            try
            {
                _context.HR_TitleDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_TitleDetails value)
        {
            try
            {
                var existingTitle = _context.HR_TitleDetails.Where(x => x.TitleId == value.TitleId).FirstOrDefault<HR_TitleDetails>();
                if (existingTitle == null)
                {
                    return Ok("Not Found");
                }
                existingTitle.TitleName = value.TitleName;
                existingTitle.SessionYear = value.SessionYear;
                existingTitle.UserId = value.UserId;
                _context.HR_TitleDetails.Update(existingTitle);
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
                var existingTitle = _context.HR_TitleDetails.Where(x => x.TitleId == id).FirstOrDefault<HR_TitleDetails>();
                if (existingTitle != null)
                {
                    _context.HR_TitleDetails.Remove(existingTitle);
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
