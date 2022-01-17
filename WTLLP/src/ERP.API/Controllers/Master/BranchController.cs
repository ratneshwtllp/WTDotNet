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
    public class BranchController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public BranchController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<BranchDetails> Get()
        {
            return _context.BranchDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.BranchDetails.Where(x => x.BranchID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBranchDetails/{id}")]
        public IActionResult GetBranchDetails(long id)
        {
            try
            {
                return Ok(_context.ViewBranchDetails.Where(x => x.BranchID == id).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBranchList")]
        public IActionResult GetBranchList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.BranchDetails.OrderByDescending(x => x.BranchID));
                else
                    return Ok(_context.BranchDetails
                        .Where(x => x.BranchName.Contains(search) || x.Address.Contains(search) || x.PhoneNo.Contains(search))
                        .OrderByDescending(x => x.BranchID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBranchId")]
        public IActionResult GetNewBranchId()
        {
            try
            {
                long BranchId = 0;
                var lastBranch = _context.BranchDetails.OrderBy(x => x.BranchID).LastOrDefault();
                BranchId = (lastBranch == null ? 0 : lastBranch.BranchID) + 1;
                return Ok(BranchId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]BranchDetails value)
        {
            try
            {
                _context.BranchDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]BranchDetails value)
        {
            try
            {
                var existingBranch = _context.BranchDetails.Where(x => x.BranchID == value.BranchID).FirstOrDefault<BranchDetails>();
                if (existingBranch == null)
                {
                    return Ok("Not Found");
                }
                existingBranch.BranchName = value.BranchName;
                existingBranch.Address = value.Address;
                existingBranch.PhoneNo = value.PhoneNo;
                existingBranch.StateId = value.StateId;
                existingBranch.CityId = value.CityId;
                _context.BranchDetails.Update(existingBranch);
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
                var existingBranch = _context.BranchDetails.Where(x => x.BranchID == id).FirstOrDefault<BranchDetails>();
                if (existingBranch != null)
                {
                    _context.BranchDetails.Remove(existingBranch);
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
                var existingWax = _context.BranchDetails.Where(x => x.BranchName == value).FirstOrDefault<BranchDetails>();
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

        [Route("GetListBranch")]
        public IActionResult GetListBranch()
        {
            try
            {
                object branches;
                branches = _context.BranchDetails
                                .OrderBy(x => x.BranchName)
                                .Select(x => new { Id = x.BranchID, Value = x.BranchName })
                                .ToList();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
