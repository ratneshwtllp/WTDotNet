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
    public class CostingElementController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public CostingElementController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<CostingElementDetails> Get()
        {
            return _context.CostingElementDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CostingElementDetails.Where(x => x.CostingElementId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingElementList")]
        public IActionResult GetCostingElementList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.CostingElementDetails.OrderByDescending(x => x.CostingElementId));
                else
                    return Ok(_context.CostingElementDetails.Where(x => x.CostingElementName.Contains(search)).OrderByDescending(x => x.CostingElementId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CostingElementCat")]
        public IActionResult CostingElementCat()
        {
            try
            {
                object categories;
                categories = _context.CostingElementDetails
                                    .OrderBy(c => c.CostingElementId)
                                    .Select(x => new { Id = x.CostingElementId, Value = x.CostingElementName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCostingElementId")]
        public IActionResult GetNewCostingElementId()
        {
            try
            {
                long CostingElementId = 0;
                var lastcostingElement = _context.CostingElementDetails.OrderBy(x => x.CostingElementId).LastOrDefault();
                CostingElementId = (lastcostingElement == null ? 0 : lastcostingElement.CostingElementId) + 1;
                return Ok(CostingElementId);
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
                var existingCostingElement = _context.CostingElementDetails.Where(x => x.CostingElementName == value).FirstOrDefault<CostingElementDetails>();
                if (existingCostingElement != null)
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
        public IActionResult Post([FromBody]CostingElementDetails value)
        {
            try
            {
                _context.CostingElementDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]CostingElementDetails value)
        {
            try
            {
                var existingCostingElement = _context.CostingElementDetails.Where(x => x.CostingElementId == value.CostingElementId).FirstOrDefault<CostingElementDetails>();
                if (existingCostingElement == null)
                {
                    return Ok("Not Found");
                }
                existingCostingElement.CostingElementName = value.CostingElementName;
                existingCostingElement.CostingElementDesc = value.CostingElementDesc;
                _context.CostingElementDetails.Update(existingCostingElement);
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
                var existingCostingElement = _context.CostingElementDetails.Where(x => x.CostingElementId == id).FirstOrDefault<CostingElementDetails>();
                if (existingCostingElement != null)
                {
                    _context.CostingElementDetails.Remove(existingCostingElement);
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
