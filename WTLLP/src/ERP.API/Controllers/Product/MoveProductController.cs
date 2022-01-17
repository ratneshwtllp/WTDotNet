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
    public class MoveProductController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public MoveProductController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductDetails value)
        {
            try
            {
                var existingProduct = _context.ProductDetails.Find(value.FitemId);
                if (existingProduct == null)
                {
                    return Ok("Not Found");
                }
                existingProduct.BelongTo = value.BelongTo;
                existingProduct.MasterBelongTo = value.MasterBelongTo;
                _context.ProductDetails.Update(existingProduct);
                _context.SaveChanges();
                return Ok("Item Moved");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
