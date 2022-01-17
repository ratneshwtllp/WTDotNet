using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;
using System.Collections.Generic;
namespace ERP.Api
{
    [Route("api/[controller]")]
    public class UserAsSupervisorController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public UserAsSupervisorController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

      
        [Route("GetUserList")]
        public IActionResult GetUserList()
        {
            try
            {
                object record;
                record = _context.UserDetails
                                        .OrderBy(c => c.LoginName)
                                        .Select(x => new { Id = x.UserId, Value = x.LoginName }).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
  
        [Route("GetContractorList")]
        public ActionResult GetContractorList()
        {
            object record;
            record = _context.ContractorDetails.OrderBy(x=> x.ContractorName).ToList();
            return Ok(record);
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                long id = 0;
                var record = _context.UserAsSupervisorDetails.OrderBy(x => x.UserAsSupervisorId).LastOrDefault();
                id = (record == null ? 0 : record.UserAsSupervisorId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]List<UserAsSupervisorDetails> value)
        {
            try
            {
               
                var existinguserdetails = _context.UserAsSupervisorDetails
                             .Where(x => x.UserId == value[0].UserId).ToList();
                foreach (var existing in existinguserdetails.ToList())
                {
                    _context.UserAsSupervisorDetails.Remove(existing);
                }

                foreach (var record in value.ToList())
                {
                    _context.UserAsSupervisorDetails.Add(record);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetUserSupervisor/{userid}")]
        public IQueryable<UserAsSupervisorDetails> GetUserSupervisor(int userid)
        {
            return _context.UserAsSupervisorDetails.Where(x => x.UserId == userid).AsQueryable();
        }

    }
}
