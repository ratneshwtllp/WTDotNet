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
    public class MoveRMStockController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public MoveRMStockController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ViewMoveRMStock.Where(x => x.MoveRMStockID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetViewMoveRMStock")]
        public IActionResult GetViewMoveRMStock([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewMoveRMStock> query = _context.ViewMoveRMStock;
                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID_From == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID_From == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.RawMaterial_From.Contains(obj.RawMaterial)); // || x.Code.Contains(obj.RawMaterial));
                if (!string.IsNullOrEmpty(obj.Rack))
                    query = query.Where(x => x.RackNo_From.Contains(obj.Rack));
                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierID_From == obj.SupplierID);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.EntryDate >= (obj.DateFrom) && x.EntryDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.EntryDate == (obj.DateFrom));
                if (obj.MonthId > 0)
                    query = query.Where(x => x.EntryDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.EntryDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);
                query = query.OrderBy(x => x.MoveRMStockID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewMoveRMStockID")]
        public IActionResult GetNewMoveRMStockID()
        {
            try
            {
                long MoveRMStockID = 0;
                var lastmoveRMStock = _context.Move_RMStock.OrderBy(x => x.MoveRMStockID).LastOrDefault();
                MoveRMStockID = (lastmoveRMStock == null ? 0 : lastmoveRMStock.MoveRMStockID) + 1;
                return Ok(MoveRMStockID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierList/{ritemid}")]
        public IActionResult GetSupplierList(long ritemid)
        {
            try
            {
                var suppid = _context.RItemSupp.Where(x => x.RItem_Id == ritemid).Select(x => x.SupplierId).ToArray();

                object suppList;
                suppList = _context.SupplierDetails.Where(x => suppid.Contains(x.SupplierId))
                                         .OrderBy(c => c.SupplierName)
                                         .Select(x => new { Id = x.SupplierId, Value = x.SupplierName }).ToList();
                return Ok(suppList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        [Route("CheckDuplicate/{ritemid}/{suppid}/{rackid}")]
        public IActionResult CheckDuplicate(long ritemid, long suppid, long rackid)
        {
            try
            {
                var existingRack = _context.OpeningStockDetails.Where(x => x.RitemID == ritemid && x.SupplierID == suppid && x.RackID == rackid).FirstOrDefault<OpeningStockDetails>();
                if (existingRack != null)
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
        public IActionResult Post([FromBody]Move_RMStock value)
        {
            try
            {
                _context.Move_RMStock.Add(value);
                _context.SaveChanges();
                return Ok("Record Move");
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
                var record = _context.Move_RMStock.Where(x => x.MoveRMStockID == id).FirstOrDefault<Move_RMStock>();
                if (record != null)
                {
                    _context.Move_RMStock.Remove(record);
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


        [Route("GetRackNoList/{suppid}/{rmid}")]
        public IActionResult GetRackNoList(long suppid, long rmid)
        {
            try
            {
                object rackNoList;
                rackNoList = _context.ViewGetRack.Where(x => x.SupplierID == suppid && x.RitemID == rmid)
                                         .OrderBy(x => x.RackId)
                                         .Select(x => new { Id = x.RackId, Value = x.RackNo }).ToList();
                return Ok(rackNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
