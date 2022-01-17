using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class DashBoardController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public DashBoardController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
        [HttpPost]
        [Route("GetViewDashBoardOrderToBeDeliver")]
        public IActionResult GetViewDashBoardOrderToBeDeliver([FromBody]OrderSearch Obj)
        {
            try
            {
                IQueryable<ViewDashBoardOrderToBeDeliver> query = _context.ViewDashBoardOrderToBeDeliver;
                if (!string.IsNullOrEmpty(Obj.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(Obj.OrderNo));               
                query = query.OrderBy(x => x.MyDeliveryDate);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewDashBoardBuyerLastOrder")]
        public IActionResult GetViewDashBoardBuyerLastOrder([FromBody]OrderSearch Obj)
        {
            try
            {
                IQueryable<ViewDashBoardBuyerLastOrder> query = _context.ViewDashBoardBuyerLastOrder;
                if (!string.IsNullOrEmpty(Obj.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(Obj.OrderNo));
                query = query.OrderBy(x => x.OrderDate);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
