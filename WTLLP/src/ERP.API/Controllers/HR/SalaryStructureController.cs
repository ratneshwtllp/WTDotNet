using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class SalaryStructureController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public SalaryStructureController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("GetNewSalStrId")]
        public IActionResult GetNewSalStrId()
        {
            try
            {
                long SalStrId = 0;
                var lastSalStrId = _context.HR_SalaryStructureMaster.OrderBy(x => x.SalStrId).LastOrDefault();
                SalStrId = (lastSalStrId == null ? 0 : lastSalStrId.SalStrId) + 1;
                return Ok(SalStrId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSalStrChildId")]
        public IActionResult GetNewSalStrChildId()
        {
            try
            {
                long SalStrChildId = 0;
                var lastSalStrChild = _context.HR_SalaryStructureChild.OrderBy(x => x.SalStrChildId).LastOrDefault();
                SalStrChildId = (lastSalStrChild == null ? 0 : lastSalStrChild.SalStrChildId) + 1;
                return Ok(SalStrChildId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] HR_SalaryStructureMaster value)
        {
            try
            {
                var existingsalary = _context.HR_SalaryStructureMaster
                                          .Where(x => x.SalStrId == value.SalStrId)
                                          .Include(x => x.HR_SalaryStructureChild)
                                          .SingleOrDefault();

                if (existingsalary != null)
                {
                    _context.Entry(existingsalary).CurrentValues.SetValues(value);

                    foreach (var existingsalarychild in existingsalary.HR_SalaryStructureChild.ToList())
                    {
                        _context.HR_SalaryStructureChild.Remove(existingsalarychild);
                    }

                    foreach (var childsalary in value.HR_SalaryStructureChild)
                    {
                        HR_SalaryStructureChild newChild = new HR_SalaryStructureChild();
                        newChild.SalStrChildId = childsalary.SalStrChildId;
                        newChild.SalStrId = childsalary.SalStrId;
                        newChild.AllowanceId = childsalary.AllowanceId;
                        newChild.AllowanceAmount = childsalary.AllowanceAmount;

                        existingsalary.HR_SalaryStructureChild.Add(newChild);
                    }
                    _context.SaveChanges();
                }
                else
                {
                    _context.HR_SalaryStructureMaster.Add(value);
                    _context.SaveChanges();
                }
                return Ok("Record Save");
            }


            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetDepartmentofEmployee")]
        public IActionResult GetDepartmentofEmployee()
        {
            try
            {
                object department;
                department = _context.HR_DepartmentDetails
                                    .OrderBy(c => c.DepartmentName)
                                    .Select(x => new { Id = x.DepartmentID, Value = x.DepartmentName }).ToList();
                return Ok(department);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmployeeList/{departmentid}")]
        public IActionResult GetEmployeeList(long departmentid)
        {
            try
            {
                var empid = _context.HR_EmployeeDetails
                    .Where(x => x.DepartmentID == departmentid)
                    .OrderBy(c => c.EmployeeName)
                   .Select(x => new { Id = x.EmployeeId, Value = x.EmployeeName }).ToList();
                return Ok(empid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetAllowncesList")]
        public IActionResult GetAllowncesList()
        {
            try
            {
                object allownces;
                allownces = _context.HR_AllowanceDetails
                                    .OrderBy(c => c.AllowanceType)
                                    .Select(x => new { Id = x.AllowanceId, Value = x.AllowanceType }).ToList();
                return Ok(allownces);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmpDetails/{id}")]
        public IActionResult GetEmpDetails(long id)
        {
            try
            {
                return Ok(_context.HR_ViewSalaryStructure.Where(x => x.EmployeeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetChildDetails/{id}")]
        public IActionResult GetChildDetails(long id)
        {
            try
            {
                return Ok(_context.HR_ViewSalaryStructure.Where(x => x.EmployeeId == id && x.AllowanceAmount > 0).ToList());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
