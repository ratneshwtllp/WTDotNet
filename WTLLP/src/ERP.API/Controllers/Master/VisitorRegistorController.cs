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
    public class VisitorRegistorController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public VisitorRegistorController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<VisitorRegistorDetails> Get()
        {
            return _context.VisitorRegistorDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.VisitorRegistorDetails.Where(x => x.VisitorRegistorId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetVisitorRegistorList")]
        public IActionResult GetVisitorRegistorList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.VisitorRegistorDetails.OrderByDescending(x => x.VisitorRegistorId));
                else
                    return Ok(_context.VisitorRegistorDetails
                        .Where(x => x.VisitorsName.Contains(search) || x.VisitorFrom.Contains(search) || x.WhomToMeet.Contains(search) || x.Purpose.Contains(search) || x.MobileNo.Contains(search) || x.InTime.Contains(search) || x.OutTime.Contains(search) || x.InTimeAMPM.Contains(search) || x.RemarkIfAny.Contains(search))
                        .OrderByDescending(x => x.VisitorRegistorId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewVisitorRegistorId")]
        public IActionResult GetNewVisitorRegistorId()
        {
            try
            {
                long VisitorRegistorId = 0;
                var lastVisitor = _context.VisitorRegistorDetails.OrderBy(x => x.VisitorRegistorId).LastOrDefault();
                VisitorRegistorId = (lastVisitor == null ? 0 : lastVisitor.VisitorRegistorId) + 1;
                return Ok(VisitorRegistorId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]VisitorRegistorDetails value)
        {
            try
            {
                _context.VisitorRegistorDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        public IActionResult Put([FromBody]VisitorRegistorDetails value)
        {
            try
            {
                var existingvisit = _context.VisitorRegistorDetails.Where(x => x.VisitorRegistorId == value.VisitorRegistorId).FirstOrDefault<VisitorRegistorDetails>();
                if (existingvisit == null)
                {
                    return Ok("Not Found");
                }
                existingvisit.VisitDate = value.VisitDate;
                existingvisit.VisitorsName = value.VisitorsName;
                existingvisit.VisitorFrom = value.VisitorFrom;
                existingvisit.WhomToMeet = value.WhomToMeet;
                existingvisit.Purpose = value.Purpose;
                existingvisit.MobileNo = value.MobileNo;
                existingvisit.InTime = value.InTime;
                existingvisit.InTimeAMPM = value.InTimeAMPM;
                existingvisit.OutTime = value.OutTime;
                existingvisit.OutTimeAMPM = value.OutTimeAMPM;
                existingvisit.RemarkIfAny = value.RemarkIfAny;
                existingvisit.PhotoPath = value.PhotoPath;
                existingvisit.Session_Year = value.Session_Year;
                existingvisit.UserId = value.UserId;
                _context.VisitorRegistorDetails.Update(existingvisit);
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
                var existingVisit = _context.VisitorRegistorDetails.Where(x => x.VisitorRegistorId == id).FirstOrDefault<VisitorRegistorDetails>();
                if (existingVisit != null)
                {
                    _context.VisitorRegistorDetails.Remove(existingVisit);
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
