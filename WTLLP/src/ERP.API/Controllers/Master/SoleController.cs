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
    public class SoleController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public SoleController(IMemoryCache memoryCache, DBContext context)
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
                return Ok(_context.SoleMaster.Where(x => x.SoleId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSoleList")]
        public IActionResult GetSoleList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.SoleMaster.OrderByDescending(x => x.SoleId));
                else
                    return Ok(_context.SoleMaster
                        .Where(x => x.SoleName.Contains(search) || x.Remark.Contains(search))
                        .OrderByDescending(x => x.SoleId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSoleId")]
        public IActionResult GetNewSoleId()
        {
            try
            {
                int SoleId = 0;
                var lastSole = _context.SoleMaster.OrderBy(x => x.SoleId).LastOrDefault();
                SoleId = (lastSole == null ? 0 : lastSole.SoleId) + 1;
                return Ok(SoleId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]SoleMaster value)
        {
            try
            {
                _context.SoleMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]SoleMaster value)
        {
            try
            {
                var existingSole = _context.SoleMaster.Where(x => x.SoleId == value.SoleId).FirstOrDefault<SoleMaster>();
                if (existingSole == null)
                {
                    return Ok("Not Found");
                }
                existingSole.SoleName = value.SoleName;
                existingSole.Remark = value.Remark;
                existingSole.SessionYear = value.SessionYear;
                existingSole.UserId = value.UserId;
                _context.SoleMaster.Update(existingSole);
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
                var existingSole = _context.SoleMaster.Where(x => x.SoleId == id).FirstOrDefault<SoleMaster>();
                if (existingSole != null)
                {
                    _context.SoleMaster.Remove(existingSole);
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
                var existingSole = _context.SoleMaster.Where(x => x.SoleName == value).FirstOrDefault<SoleMaster>();
                if (existingSole != null)
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

        [Route("SoleList")]
        public IActionResult SoleList()
        {
            try
            {
                object Sole;
                Sole = _context.SoleMaster
                                    .OrderBy(c => c.SoleName)
                                    .Select(x => new { Id = x.SoleId, Value = x.SoleName }).ToList();
                return Ok(Sole);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
