using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
namespace ERP.API
{
    [Route("api/[controller]")]
    public class HandTagController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        public HandTagController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable<ViewHandTag> Get()
        {
            return _context.ViewHandTag.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.HandTagDetails.Where(x => x.HandTagID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("BuyerList")]
        public IActionResult BuyerList()
        {
            try
            {
                object buyer;
                buyer = _context.BuyerDetails
                                    .OrderBy(c => c.BuyerCode)
                                    .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetHandTagDetailsList")]
        public IActionResult GetHandTagDetailsList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewHandTag.OrderByDescending(x => x.HandTagID));
                else
                    return Ok(_context.ViewHandTag
                        .Where(x => x.BuyerName.Contains(search) || x.HandTagName.Contains(search))
                        .OrderByDescending(x => x.HandTagID));

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewHandTagId")]
        public IActionResult GetNewHandTagId()
        {
            try
            {
                long HandTagID = 0;
                var lastHandTagID = _context.HandTagDetails.OrderBy(x => x.HandTagID).LastOrDefault();
                HandTagID = (lastHandTagID == null ? 0 : lastHandTagID.HandTagID) + 1;
                return Ok(HandTagID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]HandTagDetails value)
        {
            try
            {
                _context.HandTagDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]HandTagDetails value)
        {
            try
            {
                var existingHandTag = _context.HandTagDetails.Find(value.HandTagID);
                if (existingHandTag == null)
                {
                    return Ok("Not Found");
                }
                existingHandTag.HandTagName = value.HandTagName;
                existingHandTag.Description = value.Description;
                existingHandTag.BuyerId = value.BuyerId;
                _context.HandTagDetails.Update(existingHandTag);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value, int BuyerId)
        {
            try
            {
                var existingSticker = _context.HandTagDetails.Where(x => x.HandTagName == value && x.BuyerId == BuyerId).FirstOrDefault<HandTagDetails>();
                if (existingSticker != null)
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

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingSticker = _context.HandTagDetails.Where(x => x.HandTagID == id).FirstOrDefault<HandTagDetails>();
                if (existingSticker != null)
                {
                    _context.HandTagDetails.Remove(existingSticker);
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


        [Route("GetHangtagsList")]
        public IActionResult GetHangtagsList()
        {
            try
            {
                object Hangtags;
                Hangtags = _context.HandTagDetails
                                    .OrderBy(c => c.HandTagName)
                                    .Select(x => new { Id = x.HandTagID, Value = x.HandTagName }).ToList();
                return Ok(Hangtags);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
