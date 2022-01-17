using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class SessionYearController : ControllerBase
    {
        DBContext _context;
        public SessionYearController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<SessionYear> Get()
        {
            return _context.SessionYear.AsQueryable();
        }

        [Route("GetSessionYearList")]
        public IActionResult GetSessionYearList()
        {
            try
            {
                object sessionyear;
                sessionyear = _context.SessionYear
                              .OrderBy(c => c.SessionYearId)
                              .Select(x => new { Value = x.Session_Year }).ToList();
                return Ok(sessionyear);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetShortSessionYearList")]
        public IActionResult GetShortSessionYearList()
        {
            try
            {
                object sessionyear;
                sessionyear = _context.SessionYear
                              .OrderBy(c => c.SessionYearId)
                              .Select(x => new { Value = x.ShortSessionYear }).ToList();
                return Ok(sessionyear);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
