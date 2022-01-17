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
    public class ReceiveWPController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ReceiveWPController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ReceiveWPMaster> Get()
        {
            return _context.ReceiveWPMaster.AsQueryable();   
        }

        [Route("{receivewpid}")]
        public IActionResult Get(long receivewpid)
        {
            try
            {
                return Ok(_context.ReceiveWPMaster.Where(x => x.ReceiveWPID == receivewpid).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewReceiveWP
                        .OrderByDescending(x => x.ReceiveWPID));
                else
                    return Ok(_context.ViewReceiveWP
                        .Where(x => x.PlanNo.Contains(search) || x.ContractorName.Contains(search) || x.Code.Contains(search) || x.Name.Contains(search))
                        .OrderByDescending(x => x.ReceiveWPID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReceiveWPID")]
        public IActionResult GetNewReceiveWPID()
        {
            try
            {
                long ReceiveWPID = 0;
                var lastreceivewp = _context.ReceiveWPMaster.OrderBy(x => x.ReceiveWPID).LastOrDefault();
                ReceiveWPID = (lastreceivewp == null ? 0 : lastreceivewp.ReceiveWPID) + 1;
                return Ok(ReceiveWPID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ReceiveWPMaster value)
        {
            try
            {
                _context.ReceiveWPMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("PutIssueStatus/{issueid}")]
        //public IActionResult PutIssueStatus(long issueid)
        //{
        //    try
        //    {
        //        var existingIssue = _context.ReceiveWPMaster.Find(issueid);
        //        existingIssue.CancelStatus = 1;
        //        _context.ReceiveWPMaster.Update(existingIssue);

        //        _context.SaveChanges();
        //        var result = "Record Update";
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("Delete/{receivewpid}")]
        public IActionResult Delete(long receivewpid)
        {
            try
            {
                var existingReceiveWP = _context.ReceiveWPMaster.Where(x => x.ReceiveWPID == receivewpid)
                    .FirstOrDefault();

                if (existingReceiveWP != null)
                {
                    _context.ReceiveWPMaster.Remove(existingReceiveWP);
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
    }
}
