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
    public class StoreMasterController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public StoreMasterController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<StoreMasterDetails> Get()
        {
            return _context.StoreMasterDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.StoreMasterDetails.Where(x => x.StoreId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStoreMasterList")]
        public IActionResult GetStoreMasterList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.StoreMasterDetails.OrderByDescending(x => x.StoreId));
                else
                    return Ok(_context.StoreMasterDetails
                        .Where(x => x.StoreName.Contains(search))
                        .OrderByDescending(x => x.StoreId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStoreMasterId")]
        public IActionResult GetNewStoreMasterId()
        {
            try
            {
                int StoreId = 0;
                var lastSample = _context.StoreMasterDetails.OrderBy(x => x.StoreId).LastOrDefault();
                StoreId = (lastSample == null ? 0 : lastSample.StoreId) + 1;
                return Ok(StoreId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]StoreMasterDetails value)
        {
            try
            {
                _context.StoreMasterDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]StoreMasterDetails value)
        {
            try
            {
                var existingStore = _context.StoreMasterDetails.Where(x => x.StoreId == value.StoreId).FirstOrDefault<StoreMasterDetails>();
                if (existingStore == null)
                {
                    return Ok("Not Found");
                }
                existingStore.StoreName = value.StoreName;
                existingStore.EntryDate = value.EntryDate;
                existingStore.SessionYear = value.SessionYear;
                existingStore.UserId = value.UserId;
                _context.StoreMasterDetails.Update(existingStore);
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
                var existingStore = _context.StoreMasterDetails.Where(x => x.StoreId == id).FirstOrDefault<StoreMasterDetails>();
                if (existingStore != null)
                {
                    _context.StoreMasterDetails.Remove(existingStore);
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

        [Route("GetStoreList")]
        public IActionResult GetStoreList()
        {
            try
            {
                object obj;
                obj = _context.StoreMasterDetails 
                                    .OrderBy(c => c.StoreName )
                                    .Select(x => new { Id = x.StoreId , Value = x.StoreName  }).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
