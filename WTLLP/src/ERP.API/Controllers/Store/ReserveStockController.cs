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
    public class ReserveStockController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ReserveStockController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ReserveStockMaster> Get()
        {
            return _context.ReserveStockMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ReserveStockMaster.Where(x => x.ReserveId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewReserveStock.Where(x => x.ReserveStatus == 0)
                        .OrderByDescending(x => x.ReserveId));
                else
                    return Ok(_context.ViewReserveStock.Where(x => (x.ReserveStatus == 0) && (x.OrderNo.Contains(search) || x.PartyCode.Contains(search) || x.ReserveNo.Contains(search) || x.Name.Contains(search) || x.Code.Contains(search)))
                        .OrderByDescending(x => x.ReserveId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReserveStockId")]
        public IActionResult GetNewReserveStockId()
        {
            try
            {
                long ReserveId = 0;
                var lastReserve = _context.ReserveStockMaster.OrderBy(x => x.ReserveId).LastOrDefault();
                ReserveId = (lastReserve == null ? 0 : lastReserve.ReserveId) + 1;
                return Ok(ReserveId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        } 

        [Route("PutReserveStatus/{reserveid}")]
        public IActionResult PutReserveStatus(long reserveid)
        {
            try
            {
                var existingreservestock = _context.ReserveStockMaster.Where(x => x.ReserveId == reserveid).FirstOrDefault();
                existingreservestock.ReserveStatus = 1;
                _context.ReserveStockMaster.Update(existingreservestock);
                _context.SaveChanges();
                return Ok("Status Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ReserveStockMaster value)
        {
            try
            {
                _context.ReserveStockMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //public IActionResult Put([FromBody]ReserveStockMaster value)
        //{
        //    try
        //    {
        //        var existingRack = _context.ReserveStockMaster.Where(x => x.RackId == value.RackId).FirstOrDefault<RackMaster>();
        //        if (existingRack == null)
        //        {
        //            return Ok("Not Found");
        //        }
        //        existingRack.LineCode = value.LineCode;
        //        existingRack.LineSNo = value.LineSNo;
        //        existingRack.LineNo = value.LineNo;
        //        existingRack.RackSNo = value.RackSNo;
        //        existingRack.RackNo = value.RackNo;
        //        existingRack.Remark = value.Remark;
        //       // existingRack.StoreId = value.StoreId;
        //        _context.RackMaster.Update(existingRack);
        //        _context.SaveChanges();
        //        return Ok("Record Updated");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingRack = _context.ReserveStockMaster.Where(x => x.ReserveId == id).FirstOrDefault();
                if (existingRack != null)
                {
                    _context.ReserveStockMaster.Remove(existingRack);
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
