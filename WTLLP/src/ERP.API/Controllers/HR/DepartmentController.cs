using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
namespace ERP.Api
{
    [Route("api/[controller]")]
    public class HRDepartmentController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public HRDepartmentController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<HR_DepartmentDetails> Get()
        {
            return _context.HR_DepartmentDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HR_DepartmentDetails.Where(x => x.DepartmentID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetDepartmentList")]
        public IActionResult GetDepartmentList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.HR_DepartmentDetails.OrderByDescending(x => x.DepartmentID));
                else
                    return Ok(_context.HR_DepartmentDetails
                        .Where(x => x.DepartmentName.Contains(search) || x.DepartmentCode.Contains(search))
                        .OrderByDescending(x => x.DepartmentID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("DepartmentList")]
        public IActionResult DepartmentList()
        {
            try
            {
                object Department;
                Department = _context.HR_DepartmentDetails
                                        .OrderBy(c => c.DepartmentName)
                                        .Select(x => new { Id = x.DepartmentID, Value = x.DepartmentName }).ToList();
                return Ok(Department);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetNewDepartmentId")]
        public IActionResult GetNewDepartmentId()
        {
            try
            {
                long DepartmentId = 0;
                var lastDepartment =_context.HR_DepartmentDetails.OrderBy(x => x.DepartmentID).LastOrDefault();
                DepartmentId = (lastDepartment == null ? 0 : lastDepartment.DepartmentID) + 1;
                return Ok(DepartmentId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HR_DepartmentDetails value)
        {
            try
            {
               _context.HR_DepartmentDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]HR_DepartmentDetails value)
        {
            try
            {
                var existingDepartment =_context.HR_DepartmentDetails.Where(x => x.DepartmentID == value.DepartmentID).FirstOrDefault<HR_DepartmentDetails>();
                if (existingDepartment == null)
                {
                    return Ok("Not Found");
                }
                existingDepartment.DepartmentName = value.DepartmentName;
                existingDepartment.DepartmentCode = value.DepartmentCode;
                existingDepartment.Description = value.Description;
               _context.HR_DepartmentDetails.Update(existingDepartment);
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
                var existingDepartment =_context.HR_DepartmentDetails.Where(x => x.DepartmentID == id).FirstOrDefault<HR_DepartmentDetails>();
                if (existingDepartment != null)
                {
                   _context.HR_DepartmentDetails.Remove(existingDepartment);
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

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingWax =_context.HR_DepartmentDetails.Where(x => x.DepartmentName == value).FirstOrDefault<HR_DepartmentDetails>();
                if (existingWax != null)
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
    }
}
