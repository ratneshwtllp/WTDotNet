using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ERP.Api
{
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public SalaryController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        [HttpPost]
        [Route("SalaryList")]
        public IActionResult SalaryList([FromBody] AttandanceSearchModel Obj)
        {
            try
            {
                IQueryable<HR_ViewCalculateMonthlySalary> query = _context.HR_ViewCalculateMonthlySalary;
                if(Obj.DepartmentID !=0)
                 query = query.Where(x => x.DepartmentID == Obj.DepartmentID && x.AttMonth == Obj.MonthId && x.AttYear == Obj.YearName);
                else
                query = query.Where(x => x.AttMonth == Obj.MonthId && x.AttYear  == Obj.YearName);
                query = query.OrderBy(x => x.DepartmentName);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        [Route("EmployeeSalaryList")]
        public IActionResult EmployeeSalaryList([FromBody] AttandanceSearchModel Obj)
        {
            try
            {
                IQueryable<HR_ViewEmployeeMonthlySalary> query = _context.HR_ViewEmployeeMonthlySalary;
                if (Obj.DepartmentID != 0)
                    query = query.Where(x => x.DepartmentID == Obj.DepartmentID && x.AttMonth == Obj.MonthId && x.AttYear == Obj.YearName);
                else
                    query = query.Where(x => x.AttMonth == Obj.MonthId && x.AttYear == Obj.YearName);
                query = query.OrderBy(x => x.DepartmentName);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
