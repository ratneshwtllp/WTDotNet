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
    public class EmployeeController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public EmployeeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<HR_EmployeeDetails> Get()
        {
            return _context.HR_EmployeeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_ViewEmployeeDetails.Where(x => x.EmployeeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmployeeList")]
        public IActionResult GetEmployeeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_ViewEmployeeDetails.OrderBy(x => x.EmpType).ThenBy(x => x.DepartmentName).ThenBy(x => x.EmployeeName));
                else
                    return Ok(_context.HR_ViewEmployeeDetails
                        .Where(x => x.EmployeeCode.Contains(search) || x.TitleName.Contains(search) || x.EmployeeName.Contains(search) ||
                        x.DesignationName.Contains(search) || x.EmployeeACNo.Contains(search) || x.EmployeePFNo.Contains(search)
                        || x.PhoneNo.Contains(search) || x.MobileNo.Contains(search) || x.FatherHusband.Contains(search) ||
                        x.GenderName.Contains(search) || x.RAddress.Contains(search) || x.PAddress.Contains(search) || x.DepartmentName.Contains(search))
                        .OrderBy(x => x.EmpType).ThenBy(x => x.DepartmentName).ThenBy(x => x.EmployeeName));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewEmployeeId")]
        public IActionResult GetNewEmployeeId()
        {
            try
            {
                long EmployeeId = 0;
                var lastEmp = _context.HR_EmployeeDetails.OrderBy(x => x.EmployeeId).LastOrDefault();
                EmployeeId = (lastEmp == null ? 0 : lastEmp.EmployeeId) + 1;
                return Ok(EmployeeId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_EmployeeDetails value)
        {
            try
            {
                _context.HR_EmployeeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_EmployeeDetails value)
        {
            try
            {
                var existingEmp = _context.HR_EmployeeDetails.Where(x => x.EmployeeId == value.EmployeeId).FirstOrDefault<HR_EmployeeDetails>();
                if (existingEmp == null)
                {
                    return Ok("Not Found");
                }
                existingEmp.EmployeeCode = value.EmployeeCode;
                existingEmp.TitleId = value.TitleId;
                existingEmp.EmployeeName = value.EmployeeName;
                existingEmp.DepartmentID = value.DepartmentID;
                existingEmp.DesignationId = value.DesignationId;
                existingEmp.GradeId = value.GradeId;
                existingEmp.DateOfJoin = value.DateOfJoin;
                existingEmp.EmployeeACNo = value.EmployeeACNo;
                existingEmp.EmployeePFNo = value.EmployeePFNo;
                existingEmp.EmployeeESINo = value.EmployeeESINo;
                existingEmp.Dispensary = value.Dispensary;
                existingEmp.FatherHusband = value.FatherHusband;
                existingEmp.DateOfBirth = value.DateOfBirth;
                existingEmp.GenderId = value.GenderId;
                existingEmp.MaritalStatusId = value.MaritalStatusId;
                existingEmp.PhoneNo = value.PhoneNo;
                existingEmp.MobileNo = value.MobileNo;
                existingEmp.PANNo = value.PANNo;
                existingEmp.RAddress = value.RAddress;
                existingEmp.PAddress = value.PAddress;
                existingEmp.PhotoPath = value.PhotoPath;
                existingEmp.EntryDate = value.EntryDate;
                existingEmp.SessionYear = value.SessionYear;
                existingEmp.UserId = value.UserId;

                existingEmp.EmployeeTypeId = value.EmployeeTypeId;
                existingEmp.AdharNo = value.AdharNo;
                existingEmp.VoteridNo = value.VoteridNo;
                existingEmp.BankId = value.BankId;
                existingEmp.IFSCcode = value.IFSCcode;
                existingEmp.BankAcNo = value.BankAcNo;

                _context.HR_EmployeeDetails.Update(existingEmp);
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
                var existingEmp = _context.HR_EmployeeDetails.Where(x => x.EmployeeId == id).FirstOrDefault<HR_EmployeeDetails>();
                if (existingEmp != null)
                {
                    _context.HR_EmployeeDetails.Remove(existingEmp);
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

        [Route("GetGenderList")]
        public IActionResult GetGenderList()
        {
            try
            {
                object Gender;
                Gender = _context.HR_GenderDetails
                                        .OrderBy(c => c.GenderName)
                                        .Select(x => new { Id = x.GenderId, Value = x.GenderName }).ToList();
                return Ok(Gender);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMaritalStatusList")]
        public IActionResult GetMaritalStatusList()
        {
            try
            {
                object Marital;
                Marital = _context.HR_MaritalStatusDetails
                                        .OrderBy(c => c.MaritalStatusName)
                                        .Select(x => new { Id = x.MaritalStatusId, Value = x.MaritalStatusName }).ToList();
                return Ok(Marital);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmployeeImagePath/{EmployeeId}")]
        public IActionResult GetEmployeeImagePath(long EmployeeId)
        {
            try
            {
                string imgpath = "";
                var femp = _context.HR_EmployeeDetails.Find(EmployeeId);
                imgpath = femp.PhotoPath;
                return Ok(imgpath);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmployeeFileRecord/{id}")]
        public ActionResult GetEmployeeFileRecord(long id)
        {
            try
            {
                var empfile = _context.HR_EmployeeFile
                    .Where(x => x.EmployeeId == id)
                    .OrderBy(x => x.EmployeeFileId);
                return Ok(empfile);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewEMPFileId")]
        public IActionResult GetNewEMPFileId()
        {
            try
            {
                long EmployeeFileId = 0;
                var lastEmployeeFile = _context.HR_EmployeeFile.OrderBy(x => x.EmployeeFileId).LastOrDefault();
                EmployeeFileId = (lastEmployeeFile == null ? 0 : lastEmployeeFile.EmployeeFileId) + 1;
                return Ok(EmployeeFileId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostEmployeeFile")]
        public IActionResult PostEmployeeFile([FromBody]List<HR_EmployeeFile> value)
        {
            try
            {
                for (int i = 0; i < value.Count; i++)
                {
                    _context.HR_EmployeeFile.Add(value[i]);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingThread = _context.HR_EmployeeDetails .Where(x => x.EmployeeCode  == value).FirstOrDefault<HR_EmployeeDetails >();
                if (existingThread != null)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetEmployeeType")]
        public IActionResult GetEmployeeType()
        {
            try
            {
                object type;
                type = _context.HR_EmployeeType 
                                        .OrderBy(c => c.EmpType )
                                        .Select(x => new { Id = x.EmployeeTypeId , Value = x.EmpType }).ToList();
                return Ok(type);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("EmployeeList")]
        public IActionResult EmployeeList()
        {
            try
            {
                object employee;
                employee = _context.HR_EmployeeDetails
                                        .OrderBy(c => c.EmployeeName)
                                        .Select(x => new { Id = x.EmployeeId, Value = x.EmployeeName }).ToList();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
