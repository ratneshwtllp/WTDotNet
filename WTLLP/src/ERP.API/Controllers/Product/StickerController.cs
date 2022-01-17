using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
namespace ERP.Api
{
    [Route("api/[controller]")]
    public class StickerController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        //private object existingSticker;
        public StickerController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable<ViewSticker> Get()
        {
            return _context.ViewSticker.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.StickerDetials.Where(x => x.StickerID == id).FirstOrDefault());
        }

        [Route("BuyerList")]
        public IActionResult BuyerList()
        {
            object buyer;
            buyer = _context.BuyerDetails
                                .OrderBy(c => c.BuyerName)
                                .Select(x => new { Id = x.BuyerId, Value = x.BuyerName }).ToList();
            return Ok(buyer);
        }

        [Route("GetStickerDetialsList")]
        public IActionResult GetStickerDetialsList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.ViewSticker.OrderByDescending(x => x.StickerID));
            else
                return Ok(_context.ViewSticker
                    .Where(x => x.BuyerName.Contains(search) || x.StickerName.Contains(search))
                    .OrderByDescending(x => x.StickerID));
        }

        [Route("GetNewStickerId")]
        public IActionResult GetNewStickerId()
        {
            long StickerID = 0;
            var lastSubCategory = _context.StickerDetials.OrderBy(x => x.StickerID).LastOrDefault();
            StickerID = (lastSubCategory == null ? 0 : lastSubCategory.StickerID) + 1;
            return Ok(StickerID);
        }

        [HttpPost]
        public IActionResult Post([FromBody]StickerDetials value)
        {
            _context.StickerDetials.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]StickerDetials value)
        {
            var existingSticker = _context.StickerDetials.Find(value.StickerID);
            if (existingSticker == null)
            {
                return NotFound();
            }
            existingSticker.StickerName = value.StickerName;
            existingSticker.Description = value.Description;
            existingSticker.BuyerId = value.BuyerId;
            _context.StickerDetials.Update(existingSticker);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value, int BuyerId)
        {
            var existingSticker = _context.StickerDetials.Where(x => x.StickerName == value && x.BuyerId == BuyerId).FirstOrDefault<StickerDetials>();
            if (existingSticker != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingSticker = _context.StickerDetials.Where(x => x.StickerID == id).FirstOrDefault<StickerDetials>();
            if (existingSticker != null)
            {
                _context.StickerDetials.Remove(existingSticker);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        [Route("GetStickerList")]
        public IActionResult GetStickerList()
        {
            try
            {
                object Sticker;
                Sticker = _context.StickerDetials
                                    .OrderBy(c => c.StickerName)
                                    .Select(x => new { Id = x.StickerID, Value = x.StickerName }).ToList();
                return Ok(Sticker);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
