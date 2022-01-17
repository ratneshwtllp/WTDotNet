using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public DesignationController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<HR_DesignationDetails> Get()
        {
            return _context.HR_DesignationDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_DesignationDetails.Where(x => x.DesignationId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetDesignationList")]
        public IActionResult GetDesignationList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_DesignationDetails.OrderByDescending(x => x.DesignationId));
                else
                    return Ok(_context.HR_DesignationDetails
                        .Where(x => x.DesignationName.Contains(search))
                        .OrderByDescending(x => x.DesignationId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("DesignationList")]
        public IActionResult DesignationList()
        {
            try
            {
                object Desig;
                Desig = _context.HR_DesignationDetails
                                        .OrderBy(c => c.DesignationName)
                                        .Select(x => new { Id = x.DesignationId, Value = x.DesignationName }).ToList();
                return Ok(Desig);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewDesignationId")]
        public IActionResult GetNewDesignationId()
        {
            try
            {
                int DesignationId = 0;
                var lastDesignation = _context.HR_DesignationDetails.OrderBy(x => x.DesignationId).LastOrDefault();
                DesignationId = (lastDesignation == null ? 0 : lastDesignation.DesignationId) + 1;
                return Ok(DesignationId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_DesignationDetails value)
        {
            try
            {
                _context.HR_DesignationDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_DesignationDetails value)
        {
            try
            {
                var existingDesignation = _context.HR_DesignationDetails.Where(x => x.DesignationId == value.DesignationId).FirstOrDefault<HR_DesignationDetails>();
                if (existingDesignation == null)
                {
                    return Ok("Not Found");
                }
                existingDesignation.DesignationName = value.DesignationName;
                existingDesignation.SessionYear = value.SessionYear;
                existingDesignation.UserId = value.UserId;
                _context.HR_DesignationDetails.Update(existingDesignation);
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
                var existingDesignation = _context.HR_DesignationDetails.Where(x => x.DesignationId == id).FirstOrDefault<HR_DesignationDetails>();
                if (existingDesignation != null)
                {
                    _context.HR_DesignationDetails.Remove(existingDesignation);
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
