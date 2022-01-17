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
    public class SampleRoomController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public SampleRoomController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("GetSampleRoomTypeList")]
        public IActionResult GetSampleRoomTypeList()
        {
            try
            {
                object sampletype;
                sampletype = _context.SampleType
                                        .OrderBy(c => c.SampleName)
                                        .Select(x => new { Id = x.SampleTypeId, Value = x.SampleName }).ToList();
                return Ok(sampletype);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
       [HttpGet]
        public IQueryable<SampleRoomDetails> Get()
        {
            return _context.SampleRoomDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewSampleRoom.Where(x => x.SampleRoomId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSampleRoomList")]
        public IActionResult GetSampleRoomList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSampleRoom.OrderByDescending(x => x.SampleRoomId));
                else
                    return Ok(_context.ViewSampleRoom
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.SampleName.Contains(search))
                        .OrderByDescending(x => x.SampleRoomId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSampleRoomId")]
        public IActionResult GetNewSampleRoomId()
        {
            try
            {
                long SampleRoomId = 0;
                var lastSample = _context.SampleRoomDetails.OrderBy(x => x.SampleRoomId).LastOrDefault();
                SampleRoomId = (lastSample == null ? 0 : lastSample.SampleRoomId) + 1;
                return Ok(SampleRoomId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]SampleRoomDetails value)
        {
            try
            {
                _context.SampleRoomDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]SampleRoomDetails value)
        {
            try
            {
                var existingSample = _context.SampleRoomDetails.Where(x => x.SampleRoomId == value.SampleRoomId).FirstOrDefault<SampleRoomDetails>();
                if (existingSample == null)
                {
                    return Ok("Not Found");
                }
                existingSample.FItemId = value.FItemId;
                existingSample.StoreQty = value.StoreQty;
                existingSample.StoreDate = value.StoreDate;
                existingSample.RackNo = value.RackNo;
                existingSample.Session_Year = value.Session_Year;
                existingSample.UserId = value.UserId;
                existingSample.SampleTypeId = value.SampleTypeId;
                _context.SampleRoomDetails.Update(existingSample);
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
                var existingSample = _context.SampleRoomDetails.Where(x => x.SampleRoomId == id).FirstOrDefault<SampleRoomDetails>();
                if (existingSample != null)
                {
                    _context.SampleRoomDetails.Remove(existingSample);
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
