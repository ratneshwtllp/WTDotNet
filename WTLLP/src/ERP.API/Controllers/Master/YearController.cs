using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class YearController : ControllerBase
    {
        DBContext _context;
        public YearController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<YearDetails> Get()
        {
            return _context.YearDetails.AsQueryable();
        }

        [Route("GetYearList")]
        public IActionResult GetYearList()
        {
            try
            {
                object Year;
                Year = _context.YearDetails
                       .OrderBy(c => c.YearName)
                       .Select(x => new { Value = x.YearName }).ToList();
                return Ok(Year);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
