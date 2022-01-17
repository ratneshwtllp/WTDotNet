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
    public class LabelController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public LabelController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<LabelDetails> Get()
        {
            return _context.LabelDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.LabelDetails.Where(x => x.LabelId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerList")]
        public IActionResult GetBuyerList()
        {
            try
            {
                object Buyers;
                Buyers = _context.BuyerDetails
                                    .OrderBy(c => c.BuyerCode)
                                    .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(Buyers);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetLabelList")]
        public IActionResult GetLabelList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewLabelDetails.OrderByDescending(x => x.LabelId));
                else
                    return Ok(_context.ViewLabelDetails.Where(x => x.LabelName.Contains(search)).OrderByDescending(x => x.LabelId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewLabelId")]
        public IActionResult GetNewLabelId()
        {
            try
            {
                long LabelId = 0;
                var lastLabel = _context.LabelDetails.OrderBy(x => x.LabelId).LastOrDefault();
                LabelId = (lastLabel == null ? 0 : lastLabel.LabelId) + 1;
                return Ok(LabelId);
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
                var existingLabel = _context.LabelDetails.Where(x => x.LabelName == value).FirstOrDefault<LabelDetails>();

                if (existingLabel != null)
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
        public IActionResult Post([FromBody]LabelDetails value)
        {
            try
            {
                _context.LabelDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]LabelDetails value)
        {
            try
            {
                var existingLabel = _context.LabelDetails.Where(x => x.LabelId == value.LabelId).FirstOrDefault<LabelDetails>();
                if (existingLabel == null)
                {
                    return Ok("Not Found");
                }
                existingLabel.LabelName = value.LabelName;
                existingLabel.LabelDesc = value.LabelDesc;
                existingLabel.BuyerId = value.BuyerId;
                _context.LabelDetails.Update(existingLabel);
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
                var existingLabel = _context.LabelDetails.Where(x => x.LabelId == id).FirstOrDefault<LabelDetails>();

                if (existingLabel != null)
                {
                    _context.LabelDetails.Remove(existingLabel);
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

        [Route("LabelList")]
        public IActionResult LabelList()
        {
            try
            {
                object Label;
                Label = _context.LabelDetails
                                    .OrderBy(c => c.LabelName)
                                    .Select(x => new { Id = x.LabelId, Value = x.LabelName }).ToList();
                return Ok(Label);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
