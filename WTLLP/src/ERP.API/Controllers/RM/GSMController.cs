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
    public class GSMController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public GSMController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<GSMDetails> Get()
        {
            return _context.GSMDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.GSMDetails.Where(x => x.GSMId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetGSMList")]
        public IActionResult GetGSMList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.GSMDetails.OrderByDescending(x => x.GSMId));
                else
                    return Ok(_context.GSMDetails
                        .Where(x => x.GSMName == Convert.ToInt16(search))
                        .OrderByDescending(x => x.GSMId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGSMId")]
        public IActionResult GetNewGSMId()
        {
            try
            {
                long GSMId = 0;
                var lastGSM = _context.GSMDetails.OrderBy(x => x.GSMId).LastOrDefault();
                GSMId = (lastGSM == null ? 0 : lastGSM.GSMId) + 1;
                return Ok(GSMId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("CheckDuplicate")]
        //public IActionResult CheckDuplicate(string value)
        //{
        //    try
        //    {
        //        var existingGSM = _context.GSMDetails.Where(x => x.GSMName == value).FirstOrDefault<GSMDetails>();
        //        if (existingGSM != null)
        //        {
        //            return Ok(1);
        //        }
        //        else
        //        {
        //            return Ok(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("CheckDuplicate/{search}")]
        public IActionResult CheckDuplicate(int search)
        {
            try
            {
                var existingGSM = _context.GSMDetails.Where(x => x.GSMName == search).FirstOrDefault<GSMDetails>();
                if (existingGSM != null)
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

        [HttpPost]
        public IActionResult Post([FromBody]GSMDetails value)
        {
            try
            {
                _context.GSMDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]GSMDetails value)
        {
            try
            {
                var existingGSM = _context.GSMDetails.Where(x => x.GSMId == value.GSMId).FirstOrDefault<GSMDetails>();
                if (existingGSM == null)
                {
                    return Ok("Not Found");
                }
                existingGSM.GSMName = value.GSMName;
                existingGSM.GSMDesc = value.GSMDesc;
                _context.GSMDetails.Update(existingGSM);
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
                var existingGSM = _context.GSMDetails.Where(x => x.GSMId == id).FirstOrDefault<GSMDetails>();
                if (existingGSM != null)
                {
                    _context.GSMDetails.Remove(existingGSM);
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

        [Route("GetListGSM")]
        public IActionResult GetListGSM()
        {
            try
            {
                object GSMs;
                GSMs = _context.GSMDetails
                                        .OrderBy(c => c.GSMName)
                                        .Select(x => new { Id = x.GSMId, Value = x.GSMName }).ToList();
                return Ok(GSMs);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
