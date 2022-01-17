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
    public class ZipperController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ZipperController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<SoleMaster> Get()
        {
            return _context.SoleMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ZipperMaster.Where(x => x.ZipperId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetZipperList")]
        public IActionResult GetZipperList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ZipperMaster.OrderByDescending(x => x.ZipperId));
                else
                    return Ok(_context.ZipperMaster
                        .Where(x => x.ZipperName.Contains(search) || x.Remark.Contains(search))
                        .OrderByDescending(x => x.ZipperId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewZipperId")]
        public IActionResult GetNewZipperId()
        {
            try
            {
                int ZipperId = 0;
                var lastZipper = _context.ZipperMaster.OrderBy(x => x.ZipperId).LastOrDefault();
                ZipperId = (lastZipper == null ? 0 : lastZipper.ZipperId) + 1;
                return Ok(ZipperId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ZipperMaster value)
        {
            try
            {
                _context.ZipperMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]ZipperMaster value)
        {
            try
            {
                var existingZipper = _context.ZipperMaster.Where(x => x.ZipperId == value.ZipperId).FirstOrDefault<ZipperMaster>();
                if (existingZipper == null)
                {
                    return Ok("Not Found");
                }
                existingZipper.ZipperName = value.ZipperName;
                existingZipper.Remark = value.Remark;
                existingZipper.SessionYear = value.SessionYear;
                existingZipper.UserId = value.UserId;
                _context.ZipperMaster.Update(existingZipper);
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
                var existingZipper = _context.ZipperMaster.Where(x => x.ZipperId == id).FirstOrDefault<ZipperMaster>();
                if (existingZipper != null)
                {
                    _context.ZipperMaster.Remove(existingZipper);
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
                var existingZipper = _context.ZipperMaster.Where(x => x.ZipperName == value).FirstOrDefault<ZipperMaster>();
                if (existingZipper != null)
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

        [Route("ZipperList")]
        public IActionResult ZipperList()
        {
            try
            {
                object zipper;
                zipper = _context.ZipperMaster
                                    .OrderBy(c => c.ZipperName)
                                    .Select(x => new { Id = x.ZipperId, Value = x.ZipperName }).ToList();
                return Ok(zipper);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
