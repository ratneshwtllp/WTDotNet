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
    public class RMStockController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public RMStockController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            _context.Database.SetCommandTimeout(360);
        }

        //GET api/values
        [HttpGet]
        public IQueryable<ViewRMStock> Get()
        {
            return _context.ViewRMStock
                .OrderByDescending(x => x.RItem_Id)
                .AsQueryable();

            //return _context.ViewOpStockDetails.OrderByDescending(x=> x.OpeningStockID).AsQueryable();
            //return _context.OpeningStockDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.ViewRMStock.Where(x => x.RItem_Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewRMCurrentStock/{rmcatid}/{rmscatid}/{ritemid}")]
        public IActionResult GetViewRMCurrentStock(long rmcatid, long rmscatid, long ritemid)
        {
            try
            {
                if (ritemid == 0 && rmscatid == 0)
                {
                    return Ok(_context.ViewRMStock.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.CategoryID == rmcatid));

                }
                else if (ritemid == 0 && rmscatid != 0)
                {
                    return Ok(_context.ViewRMStock.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.SubCategoryID == rmscatid));
                }
                else
                {
                    return Ok(_context.ViewRMStock.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.RItem_Id == ritemid));
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewRMStockRackWise/{ritemid}")]
        public IActionResult GetViewRMStockRackWise(long ritemid)
        {
            try
            {
                return Ok(_context.ViewRMStockRackWise.OrderBy(x => x.SupplierName).ThenBy(x => x.RackNo)
                    .Where(x => x.RItem_Id == ritemid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewRMCurrentStock")]
        public IActionResult GetViewRMCurrentStock([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewRMStock> query = _context.ViewRMStock;
                if (RMS.StockFilterId > 0)
                {
                    //if (RMS.StockFilterId == 1)
                    //    query = query.Where(x => x.CurrentStock == RMS.RMCategory_ID); //All
                    //else 
                    if (RMS.StockFilterId == 2)
                        query = query.Where(x => x.CurrentStock > 0); //> 0
                    else if (RMS.StockFilterId == 3)
                        query = query.Where(x => x.CurrentStock == 0); //= 0
                }
                if (RMS.RMCategory_ID > 0)
                    query = query.Where(x => x.CategoryID == RMS.RMCategory_ID);
                if (RMS.RMSubCategory_ID > 0)
                    query = query.Where(x => x.SubCategoryID == RMS.RMSubCategory_ID);
                if (!string.IsNullOrEmpty(RMS.RMCode))
                    query = query.Where(x => x.Code.Contains(RMS.RMCode));
                if (!string.IsNullOrEmpty(RMS.RMName))
                    query = query.Where(x => x.Name.Contains(RMS.RMName));
                query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewRMStockRackWise")]
        public IActionResult GetViewRMStockRackWise([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewRMStockRackWise> query = _context.ViewRMStockRackWise;
                if (RMS.StockFilterId > 0)
                {
                    if (RMS.StockFilterId == 2)
                        query = query.Where(x => x.CurrentStock > 0);
                    else if (RMS.StockFilterId == 3)
                        query = query.Where(x => x.CurrentStock == 0);
                }
                if (!string.IsNullOrEmpty(RMS.RMCode))
                    query = query.Where(x => x.Code.Contains(RMS.RMCode));
                if (RMS.StoreId >0)
                    query = query.Where(x => x.StoreId == RMS.StoreId);
                if (!string.IsNullOrEmpty(RMS.RMName))
                    query = query.Where(x => x.Full_Name.Contains(RMS.RMName));
                if (!string.IsNullOrEmpty(RMS.RackNo))
                    query = query.Where(x => x.RackNo.Contains(RMS.RackNo));

                query = query.OrderBy(x => x.StoreName).ThenBy(x => x.RackNo).ThenBy(x=> x.Full_Name);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetLotRMRate/{rmid}/{suppid}/{lotno}")]
        public IActionResult GetLotRMRate(long rmid, long suppid, string lotno)
        {
            try
            {
                double rmlotrate;
                var record = _context.ViewRMLotRate.Where(x => x.RitemID == rmid && x.SupplierID == suppid && x.LotNo == lotno).FirstOrDefault();
                if (record != null) { rmlotrate = record.Rate; }
                else
                {
                    var obj = _context.RitemMaster.Where(x => x.RitemId == rmid).FirstOrDefault();
                    rmlotrate = obj.CostPrice;
                }
                return Ok(rmlotrate);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetRMRackStockFromBatchLotNo/{rowidlotid}")]
        //public IActionResult GetRMRackStockFromBatchLotNo(long rowidlotid)
        //{
        //    try
        //    {
        //        double stock;
        //        var record = _context.ViewRMStockRackWise.Where(x => x.RowID == rowidlotid).SingleOrDefault();
        //        stock = record.CurrentStock;
        //        return Ok(stock);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetRMRackStock/{ritemid}/{suppid}/{rackid}")]
        public IActionResult GetRMRackStock(long ritemid, long suppid, long rackid,string lotno )
        {
            try
            {
                double stock;
                var record = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == suppid && x.RackId == rackid && x.LotNo == lotno).SingleOrDefault();
                stock = record.CurrentStock;
                return Ok(stock);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMLotStock/{ritemid}/{suppid}/{rackid}")]
        public IActionResult GetRMLotStock(long ritemid, long suppid, long rackid, string lotno)
        {
            try
            {
                var record = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == suppid && x.RackId == rackid && x.LotNo == lotno).SingleOrDefault();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBatch_LotNoFromRM_Supp_Rack/{ritemid}/{suppid}/{rackid}")]
        public IActionResult GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid)
        {
            try
            {
                object batch_lotNoList;
                batch_lotNoList = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == suppid && x.RackId == rackid)
                                         .OrderBy(x => x.RowID)
                                         .Select(x => new { Id = x.RowID, Value = x.LotNo }).ToList();
                return Ok(batch_lotNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetRMMovement")]
        public IActionResult GetRMMovement([FromBody]RMSearch RMS)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("EXEC SPRMStockDateWise '" + string.Format("{0:dd/MMM/yyyy}", RMS.RMStockDate) + "' ");
                var record = _context.RMMovement.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("RMMovementPrint")]
        public IActionResult RMMovementPrint([FromBody]RMSearch RMS)
        {
            try
            {
                var record = _context.RMMovement.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewRMLedger")]
        public IActionResult GetViewRMLedger([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewRMLedger> query = _context.ViewRMLedger.Where(x => x.RitemId == RMS.RItemId);
                if (RMS.ChkCreateDateFromInt == 1)
                    query = query.Where(x => x.RecordDate >= RMS.CreateDateFrom);
                if (RMS.ChkCreateDateFromInt == 1 && RMS.ChkCreateDateToInt == 1)
                    query = query.Where(x => x.RecordDate >= RMS.CreateDateFrom && x.RecordDate <= RMS.CreateDateTo);
                query = query.OrderBy(x => x.RecordDate).ThenBy(x => x.RecordType).ThenBy(x => x.RecordMasterId);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewRMLedgerOPOnDate")]
        public IActionResult GetViewRMLedgerOPOnDate([FromBody]RMSearch RMS)
        {
            try
            {
                var totalinqty = _context.ViewRMLedger.Where(x => x.RitemId == RMS.RItemId).Where(x => x.RecordDate < RMS.CreateDateFrom).Sum(x => x.INQty);
                var totalout = _context.ViewRMLedger.Where(x => x.RitemId == RMS.RItemId).Where(x => x.RecordDate < RMS.CreateDateFrom).Sum(x => x.OUTQty);
                var balance = totalinqty - totalout;

                return Ok(balance);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewRMStatus")]
        public IActionResult GetViewRMStatus([FromBody]RMSearch RMS)
        {
            try
            {
                //int sp_res = _context.Database.ExecuteSqlCommand("exec SP_RMStatus");

                IQueryable<ViewRMStatus> query = _context.ViewRMStatus;
                if (RMS.RMCategory_ID > 0)
                    query = query.Where(x => x.MasterBelongTo == RMS.RMCategory_ID);
                if (RMS.RMSubCategory_ID > 0)
                    query = query.Where(x => x.BelongTo == RMS.RMSubCategory_ID);
                if (RMS.RItemId > 0)
                    query = query.Where(x => x.RitemID == RMS.RItemId);
                if (!string.IsNullOrEmpty(RMS.RMCode))
                    query = query.Where(x => x.Code.Contains(RMS.RMCode));
                if (!string.IsNullOrEmpty(RMS.RMName))
                    query = query.Where(x => x.Code.Contains(RMS.RMName));

                query = query.OrderBy(x => x.Full_Name);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("CalculateRMStatus")]
        public IActionResult CalculateRMStatus()
        {
            try
            {
                int sp_res = _context.Database.ExecuteSqlCommand("exec SP_RMStatus");
                return Ok("Calculation Complete.");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetViewWorkPlanBOMRuning")]
        public IActionResult GetViewWorkPlanBOMRuning([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewWorkPlanBOMRuning> query = _context.ViewWorkPlanBOMRuning.Where(x => x.RItemID == RMS.RItemId && x.IssueQty < x.Required).OrderBy(x => x.PlanId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewSaleOrderItemBalBOM")]
        public IActionResult GetViewSaleOrderItemBalBOM([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewSaleOrderItemBalBOM> query = _context.ViewSaleOrderItemBalBOM.Where(x => x.RItemID == RMS.RItemId ).OrderBy(x=> x.Code).ThenBy(x=> x.ProductSizeName);
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
