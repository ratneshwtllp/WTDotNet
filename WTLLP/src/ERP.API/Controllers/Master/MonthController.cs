using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class MonthController : ControllerBase
    {
        DBContext _context;
        public MonthController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<MonthDetail> Get()
        {
            return _context.MonthDetail.AsQueryable();
        }

        [Route("GetMonthList")]
        public IActionResult GetMonthList()
        {
            try
            {
                object Month;
                Month = _context.MonthDetail
                        .OrderBy(c => c.MonthId)
                        .Select(x => new { Id = x.MonthId, Value = x.Monthname }).ToList();
                return Ok(Month);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
