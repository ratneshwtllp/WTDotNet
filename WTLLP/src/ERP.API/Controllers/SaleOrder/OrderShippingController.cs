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
    public class OrderShippingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public OrderShippingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IQueryable<OrderShippingDetails> Get()
        {
            return _context.OrderShippingDetails.AsQueryable();
        }
      

        [HttpPost]
        public IActionResult Post([FromBody]OrderShippingDetails value)
        {
            try
            {
                _context.OrderShippingDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]OrderShippingDetails value)
        {
            try
            {
                var existingOrder = _context.OrderShippingDetails.Where(x => x.OrderShippingID == value.OrderShippingID).FirstOrDefault<OrderShippingDetails>();
                if (existingOrder == null)
                {
                    return Ok("Not Found");
                }
                existingOrder.OrderShippingID = value.OrderShippingID;
                existingOrder.Order_ID = value.Order_ID;
                existingOrder.TransportName = value.TransportName;
                existingOrder.ShippingDate = value.ShippingDate;
                existingOrder.TrackingNo = value.TrackingNo;
                existingOrder.Remark = value.Remark;
                _context.OrderShippingDetails.Update(existingOrder);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
