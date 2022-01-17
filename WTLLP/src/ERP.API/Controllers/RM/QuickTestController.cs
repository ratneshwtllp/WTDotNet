using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using ERP.Domain.Entities;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class QuickTestController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public QuickTestController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<QuickTestDetails> Get()
        {
            return _context.QuickTestDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.QuickTestDetails.Where(x => x.ID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetQuickTestList")]
        public IActionResult GetQuickTestList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.QuickTestDetails.OrderByDescending(x => x.ID));
                else
                    return Ok(_context.QuickTestDetails
                        .Where(x => x.TestName.Contains(search))
                        .OrderByDescending(x => x.ID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewQuickTestId")]
        public IActionResult GetNewQuickTestId()
        {
            try
            {
                long Quick_Test_Id = 0;
                var Quick_Id = _context.QuickTestDetails.OrderBy(x => x.ID).LastOrDefault();
                Quick_Test_Id = (Quick_Id == null ? 0 : Quick_Id.ID) + 1;
                return Ok(Quick_Test_Id);
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
                var existingQuicktest = _context.QuickTestDetails.Where(x => x.TestName == value).FirstOrDefault<QuickTestDetails>();
                if (existingQuicktest != null)
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
        public IActionResult Post([FromBody]QuickTestDetails value)
        {
            try
            {
                _context.QuickTestDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]QuickTestDetails value)
        {
            try
            {
                var existingQuicktest = _context.QuickTestDetails.Where(x => x.ID == value.ID).FirstOrDefault<QuickTestDetails>();
                if (existingQuicktest == null)
                {
                    return Ok("Not Found");
                }
                existingQuicktest.TestName = value.TestName;
                existingQuicktest.MasterBelongTo = value.MasterBelongTo;
                _context.QuickTestDetails.Update(existingQuicktest);
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
                var existingQuicktest = _context.QuickTestDetails.Where(x => x.ID == id).FirstOrDefault<QuickTestDetails>();
                if (existingQuicktest != null)
                {
                    _context.QuickTestDetails.Remove(existingQuicktest);
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


        [Route("GetQuickrTestList")]
        public IQueryable<QuickTestDetails> GetQuickrTestList()
        {
            return _context.QuickTestDetails.OrderBy(x => x.TestName).AsQueryable();
        }

        [Route("GetNewRItemQuickTestId")]
        public IActionResult GetNewRItemQuickTestId()
        {
            try
            {
                long ID = 0;
                var LastID = _context.RItemQuickTest.OrderBy(x => x.RItemQuickTestId).LastOrDefault();
                ID = (LastID == null ? 0 : LastID.RItemQuickTestId) + 1;
                return Ok(ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetExQuickTestList/{RItemId}")]
        public IQueryable<RItemQuickTest> GetExQuickTestList(long RItemId)
        {
            var data = _context.RItemQuickTest.Where(x => x.RItem_Id == RItemId).AsQueryable();
            return data;
        }

    }
}
