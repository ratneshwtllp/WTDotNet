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
    public class DepartmentController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public DepartmentController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<DepartmentDetails> Get()
        {
            return _context.DepartmentDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.DepartmentDetails.Where(x => x.DepartmentID == id).FirstOrDefault());
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
                    return Ok(_context.DepartmentDetails.OrderByDescending(x => x.DepartmentID));
                else
                    return Ok(_context.DepartmentDetails
                        .Where(x => x.DepartmentName.Contains(search) || x.DepartmentCode.Contains(search))
                        .OrderByDescending(x => x.DepartmentID));
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
                var lastDepartment = _context.DepartmentDetails.OrderBy(x => x.DepartmentID).LastOrDefault();
                DepartmentId = (lastDepartment == null ? 0 : lastDepartment.DepartmentID) + 1;
                return Ok(DepartmentId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]DepartmentDetails value)
        {
            try
            {
                _context.DepartmentDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]DepartmentDetails value)
        {
            try
            {
                var existingDepartment = _context.DepartmentDetails.Where(x => x.DepartmentID == value.DepartmentID).FirstOrDefault<DepartmentDetails>();
                if (existingDepartment == null)
                {
                    return Ok("Not Found");
                }
                existingDepartment.DepartmentName = value.DepartmentName;
                existingDepartment.DepartmentCode = value.DepartmentCode;
                existingDepartment.Description = value.Description;
                _context.DepartmentDetails.Update(existingDepartment);
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
                var existingDepartment = _context.DepartmentDetails.Where(x => x.DepartmentID == id).FirstOrDefault<DepartmentDetails>();
                if (existingDepartment != null)
                {
                    _context.DepartmentDetails.Remove(existingDepartment);
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
                var existingWax = _context.DepartmentDetails.Where(x => x.DepartmentName == value).FirstOrDefault<DepartmentDetails>();
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

        [Route("GetListDepartment")]
        public IActionResult GetListDepartment()
        {
            try
            {
                object deptts;
                deptts = _context.DepartmentDetails
                                .OrderBy(x => x.DepartmentCode)
                                .Select(x => new { Id = x.DepartmentID, Value = x.DepartmentName })
                                .ToList();
                return Ok(deptts);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
