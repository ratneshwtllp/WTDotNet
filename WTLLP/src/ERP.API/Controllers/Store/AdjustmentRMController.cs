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
    public class AdjustmentRMController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public AdjustmentRMController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        //[HttpPost]
        //[Route("GetViewAdjustmentRM")]
        //public IActionResult GetViewAdjustmentRM([FromBody]StoreSearch obj)
        //{
        //    try
        //    {
        //        IQueryable<ViewAdjustmentRM> query = _context.ViewAdjustmentRM;

        //        if (obj.CategoryID > 0)
        //            query = query.Where(x => x.CategoryID == obj.CategoryID);
        //        if (obj.SubCategoryID > 0)
        //            query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
        //        if (!string.IsNullOrEmpty(obj.RawMaterial))
        //            query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) || x.Code.Contains(obj.RawMaterial));
        //        if (!string.IsNullOrEmpty(obj.Rack))
        //            query = query.Where(x => x.RackNo.Contains(obj.Rack));
        //        if (!string.IsNullOrEmpty(obj.LotNo))
        //            query = query.Where(x => x.LotNo.Contains(obj.LotNo));
        //        //if (!string.IsNullOrEmpty(obj.ReqRMIssueNo))
        //        //    query = query.Where(x => x.ReqRMIssueNo.Contains(obj.ReqRMIssueNo));
        //        //if (!string.IsNullOrEmpty(obj.ReqRMNo))
        //        //    query = query.Where(x => x.ReqRMNo.Contains(obj.ReqRMNo));
        //        //if (!string.IsNullOrEmpty(obj.RMFor))
        //        //    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

        //        if (obj.SupplierID > 0)
        //            query = query.Where(x => x.SupplierID == obj.SupplierID);
        //        //if (obj.ContractorId > 0)
        //        //    query = query.Where(x => x.ContractorID == obj.ContractorId);

        //        if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
        //            query = query.Where(x => x.AdjustmentDate >= (obj.DateFrom) && x.AdjustmentDate <= obj.DateTo);
        //        if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
        //            query = query.Where(x => x.AdjustmentDate == (obj.DateFrom));

        //        if (obj.MonthId > 0)
        //            query = query.Where(x => x.AdjustmentDate.Month == obj.MonthId);
        //        if (obj.YearName > 0)
        //            query = query.Where(x => x.AdjustmentDate.Year == obj.YearName);
        //        //if (obj.Session_Year != "-- Select --")
        //        //    query = query.Where(x => x.SessionYear == obj.Session_Year);

        //        query = query.OrderBy(x => x.AdjustmentRMStockID);
        //        var record = query.ToList();
        //        return Ok(record);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("GetViewAdjustmentRM")]
        public IActionResult GetViewAdjustmentRM([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewAdjustmentRM> query = _context.ViewAdjustmentRM;

                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) || x.Code.Contains(obj.RawMaterial));
                if (!string.IsNullOrEmpty(obj.Rack))
                    query = query.Where(x => x.RackNo.Contains(obj.Rack));
                if (!string.IsNullOrEmpty(obj.LotNo))
                    query = query.Where(x => x.LotNo.Contains(obj.LotNo));
                //if (!string.IsNullOrEmpty(obj.ReqRMIssueNo))
                //    query = query.Where(x => x.ReqRMIssueNo.Contains(obj.ReqRMIssueNo));
                //if (!string.IsNullOrEmpty(obj.ReqRMNo))
                //    query = query.Where(x => x.ReqRMNo.Contains(obj.ReqRMNo));
                //if (!string.IsNullOrEmpty(obj.RMFor))
                //    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierID == obj.SupplierID);
                //if (obj.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == obj.ContractorId);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.AdjustmentDate >= (obj.DateFrom) && x.AdjustmentDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.AdjustmentDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.AdjustmentDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.AdjustmentDate.Year == obj.YearName);
                if (obj.StockType > 0)
                    query = query.Where(x => x.StockType == obj.StockType);
                //if (obj.Session_Year != "-- Select --")
                //    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderBy(x => x.AdjustmentRMStockID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewAdjustmentRMID")]
        public ActionResult GetNewAdjustmentRMID()
        {
            try
            {
                long AdjustmentRMStockID = 0;
                var lastadjustmentRM = _context.AdjustmentRMDetails.OrderBy(x => x.AdjustmentRMStockID).LastOrDefault();
                AdjustmentRMStockID = (lastadjustmentRM == null ? 0 : lastadjustmentRM.AdjustmentRMStockID) + 1;
                return Ok(AdjustmentRMStockID);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
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

        [HttpPost]
        public IActionResult Post([FromBody]AdjustmentRMDetails value)
        {
            try
            {
                _context.AdjustmentRMDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
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
                var record = _context.AdjustmentRMDetails.Where(x => x.AdjustmentRMStockID == id).FirstOrDefault();

                long ritemid = record.RitemID;
                long supplierid = record.SupplierID;
                long rackid = record.RackID;
                string lotno = record.LotNo;
                int stocktype = record.StockType;
                double Qty = record.Quantity;

                var data = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == supplierid && x.RackId == rackid && x.LotNo == lotno).SingleOrDefault();
                double currentstock = data.CurrentStock;

                if (stocktype == 2)
                {
                    if (currentstock >= Qty)
                    {
                        _context.AdjustmentRMDetails.Remove(record);
                        _context.SaveChanges();
                        return Ok("Record Deleted");
                    }
                    else
                    {
                        return Ok("Can Not be Deleted");
                    }
                }
                if (record != null)
                {
                    _context.AdjustmentRMDetails.Remove(record);
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
                //object rackNoList;
                //rackNoList = _context.ViewGetRack.Where(x => x.SupplierID == suppid && x.RitemID == rmid)
                //                         .OrderBy(x => x.RackId)
                //                         .Select(x => new { Id = x.RackId, Value = x.RackNo }).ToList();

                var racklist = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == rmid && x.SupplierId == suppid)
                    .OrderBy(x => x.RackId)
                    .Select(x => new { Id = x.RackId, Value = x.RackNo + " " + x.Remark })
                    .Distinct();

                return Ok(racklist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
