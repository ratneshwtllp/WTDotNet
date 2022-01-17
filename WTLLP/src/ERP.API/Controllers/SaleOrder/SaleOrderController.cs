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
    public class SaleOrderController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public SaleOrderController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ViewSaleOrder> Get()
        {
            return _context.ViewSaleOrder.Where(x => x.CancelStatus == 0)    //list of Not canceled po
               .OrderByDescending(x => x.OrderMasterID).AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.ViewSaleOrder.Where(x => x.OrderMasterID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        //[Route("GetPOForPrint/{id}")]
        //public IQueryable<ViewPOPrint> GetPOForPrint(long id)
        //{
        //    return _context.ViewPOPrint.Where(x => x.Poid == id).AsQueryable();
        //}
        [Route("GetSaleOrderChild/{id}")]
        public IQueryable<ViewSaleOrderDetails> GetSaleOrderChild(long id)

        {
            return _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == id).AsQueryable();
        }

        [Route("NewOrderMasterID")]
        public IActionResult GetNewOrderMasterID()
        {
            try
            {
                long orderid = 0;
                var lastOrder = _context.SaleOrderMaster.OrderBy(x => x.OrderMasterID).LastOrDefault();
                orderid = (lastOrder == null ? 0 : lastOrder.OrderMasterID) + 1;
                return Ok(orderid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("NewOrderChildID")]
        public IActionResult GetNewOrderChildID()
        {
            try
            {
                long OrderChildID = 0;
                var lastOrderChild = _context.SaleOrderChild.OrderBy(x => x.OrderChildID).LastOrDefault();
                OrderChildID = (lastOrderChild == null ? 0 : lastOrderChild.OrderChildID) + 1;
                return Ok(OrderChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("Fillbuyer")]
        public IActionResult fillBuyer()
        {
            try
            {
                object buyerDetails;
                buyerDetails = _context.BuyerDetails
                                         .OrderBy(c => c.BuyerName)
                                         .Select(x => new { Id = x.BuyerId, Value = x.BuyerName });
                return Ok(buyerDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("FItemShow/{id}")]
        public IActionResult FItemShow(int id)
        {
            try
            {
                object FitemDetails;
                FitemDetails = _context.ViewProductShow.Where(x => x.FitemId == id).FirstOrDefault();
                return Ok(FitemDetails);
                //return Ok(_context.ViewProductShow .Where(x => x.FitemId  == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicateOrder/{orderNo}")]
        public IActionResult CheckDuplicateOrder(string orderno)
        {
            try
            {
                object OrderNo;
                OrderNo = _context.SaleOrderMaster.Where(x => x.OrderNo == orderno).FirstOrDefault();
                return Ok(OrderNo);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]SaleOrderMaster value)
        {
            try
            {
                _context.SaleOrderMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]SaleOrderMaster value)
        {
            try
            {
                var existingOrder = _context.SaleOrderMaster
                        .Where(x => x.OrderMasterID == value.OrderMasterID)
                        .Include(x => x.SaleOrderChild)
                        .SingleOrDefault();

                if (existingOrder != null)
                {
                    // Update parent
                    _context.Entry(existingOrder).CurrentValues.SetValues(value);
                    //existingOrder.OrderNo = value.OrderNo;
                    //existingOrder.RefrenceNo = value.RefrenceNo;
                    //existingOrder.BuyerId = value.BuyerId;
                    //existingOrder.IssuedById = value.IssuedById;
                    //existingOrder.OrderDate = value.OrderDate;
                    //existingOrder.DeliveryDate = value.DeliveryDate;
                    //existingOrder.MyDeliveryDate = value.MyDeliveryDate;
                    //existingOrder.OrderEntryDate = value.OrderEntryDate;
                    //existingOrder.PRDApprovalDate = value.PRDApprovalDate;
                    //existingOrder.PRDStartDate = value.PRDStartDate;
                    //existingOrder.ShipmentSampleDate = value.ShipmentSampleDate;
                    //existingOrder.FOBCIF = value.FOBCIF;
                    //existingOrder.TransportId = value.TransportId;
                    //existingOrder.TransportCommments = value.TransportCommments;
                    //existingOrder.Remark = value.Remark; 

                    //existingOrder.TotalQty = value.TotalQty;
                    //existingOrder.TotalAmount = value.TotalAmount;

                    //existingOrder.MultipleDeliveryDate = value.MultipleDeliveryDate;
                    //existingOrder.MultipleTransport = value.MultipleTransport;

                    //existingOrder.UserID = value.UserID;

                    // Delete children
                    foreach (var ExistingOrderChild in existingOrder.SaleOrderChild.ToList())
                    {
                        _context.SaleOrderChild.Remove(ExistingOrderChild);
                    }

                    // Update and Insert children
                    foreach (var childSaleOrder in value.SaleOrderChild)
                    {
                        // Insert child 1
                        SaleOrderChild newChild = new SaleOrderChild();
                        newChild.OrderChildID = childSaleOrder.OrderChildID;
                        newChild.OrderMasterID = childSaleOrder.OrderMasterID;
                        newChild.FitemId = childSaleOrder.FitemId;
                        newChild.Qty = childSaleOrder.Qty;
                        newChild.Size = childSaleOrder.Size;
                        newChild.Price = childSaleOrder.Price;
                        newChild.Amount = childSaleOrder.Amount;
                        newChild.OrderChildRemark = childSaleOrder.OrderChildRemark;
                        newChild.ChildDeliveryDate = childSaleOrder.ChildDeliveryDate;
                        newChild.ChildMyDeliveryDate = childSaleOrder.ChildMyDeliveryDate;
                        newChild.TransportId = childSaleOrder.TransportId;
                        newChild.SNo = childSaleOrder.SNo;
                        existingOrder.SaleOrderChild.Add(newChild);
                    }
                    _context.SaveChanges();

                }
                return Ok("Record Updated");
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
                var existingorderno = _context.SaleOrderMaster.Where(x => x.OrderNo == value).FirstOrDefault<SaleOrderMaster>();
                if (existingorderno != null)
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

        [Route("UpdateOrder/{id}")]
        public IActionResult UpdateOrder(int id)
        {
            try
            {
                var existingorder = _context.SaleOrderMaster.Where(x => x.OrderMasterID == id).FirstOrDefault<SaleOrderMaster>();
                if (existingorder != null)
                {
                    existingorder.CancelStatus = 1;
                    _context.SaveChanges();
                    return Ok("Record updated");
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
        [Route("PutOrderStatus/{OrderMasterID}")]
        public IActionResult PutOrderStatus(long OrderMasterID)
        {
            try
            {
                var existingorder = _context.SaleOrderMaster.Find(OrderMasterID);
                existingorder.CancelStatus = 1;
                _context.SaleOrderMaster.Update(existingorder);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderNoList")]
        public IActionResult GetOrderNoList()
        {
            try
            {
                object ordernolist;
                ordernolist = _context.SaleOrderMaster
                                        .OrderBy(c => c.OrderMasterID)
                                        .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo }).ToList();
                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        } 
         
        [Route("GetOrderNoListForPI/{buyerid}")]
        public IActionResult GetOrderNoListForPI(long buyerid)
        {
            try
            {
                var orderidlist = _context.PIMaster.Select(x => x.OrderId).ToList();
                object ordernolist;
                ordernolist = _context.SaleOrderMaster.Where(x=> x.BuyerId == buyerid && !orderidlist.Contains(x.OrderMasterID))
                                        .OrderBy(c => c.OrderMasterID)
                                        .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo }).ToList();
                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderNoList/{buyerid}")]
        public IActionResult GetOrderNoList(long buyerid)
        {
            try
            {
                var ordernolist = _context.SaleOrderMaster.Where(x => x.BuyerId == buyerid && x.CancelStatus == 0)
                    .OrderBy(x => x.OrderNo)
                    .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo })
                    .ToList();

                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewSaleOrder")]
        public IActionResult GetViewSaleOrder(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    var SO = _context.ViewSaleOrder.Where(x => x.CancelStatus == 0)    //list of Not canceled po
                                            .OrderByDescending(x => x.OrderMasterID);
                    return Ok(SO);
                }
                else
                {
                    var SO = _context.ViewSaleOrder
                                    .Where(x => (x.CancelStatus == 0) && (x.BuyerName.Contains(search) || x.IssuedByName.Contains(search) || x.OrderNo.Contains(search) || x.RefrenceNo.Contains(search) || x.FOBCIF.Contains(search)))
                                    .OrderByDescending(x => x.OrderMasterID);
                    return Ok(SO);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderItems/{orderid}")]
        public IActionResult GetOrderItems(long orderid)
        {
            try
            {
                //if (string.IsNullOrEmpty(search))
                //{ 
                    var SO = _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == orderid )    //list of Not canceled po
                                            .OrderBy(x => x.Code);
                    return Ok(SO);
                //}
                //else
                //{
                //    var SO = _context.ViewSaleOrder
                //                    .Where(x => (x.CancelStatus == 0) && (x.BuyerName.Contains(search) || x.IssuedByName.Contains(search) || x.OrderNo.Contains(search) || x.RefrenceNo.Contains(search) || x.FOBCIF.Contains(search)))
                //                    .OrderByDescending(x => x.OrderMasterID);
                //    return Ok(SO);
                //}
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetItems/{id}")]
        public IActionResult GetItems(long id)
        {
            try
            {
                var SO = _context.ViewOrderItemsForPacking.Where(x => x.OrderMasterID == id)    //list of Not canceled po
                                        .OrderByDescending(x => x.OrderMasterID);
                return Ok(SO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSingleOrderNo/{id}")]
        public IActionResult GetSingleOrderNo(long id)
        {
            try
            {
                object ordernolist;
                ordernolist = _context.SaleOrderMaster.Where(x => x.OrderMasterID == id)
                                        .OrderBy(c => c.OrderMasterID)
                                        .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo }).ToList();
                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewOrderShippingId")]
        public IActionResult GetNewOrderShippingId()
        {
            try
            {
                long OrderShippingID = 0;
                var lastOrderShipping = _context.OrderShippingDetails.OrderBy(x => x.OrderShippingID).LastOrDefault();
                OrderShippingID = (lastOrderShipping == null ? 0 : lastOrderShipping.OrderShippingID) + 1;
                return Ok(OrderShippingID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("{orderid}")]
        public IActionResult GetShippingDetails(long id)
        {
            try
            {
                return Ok(_context.ViewSaleOrder.Where(x => x.OrderMasterID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpPost]
        [Route("SaleOrderList")]
        public IActionResult SaleOrderList([FromBody]OrderSearch SO)
        {
            try
            {
                IQueryable<ViewSaleOrderDetails> query = _context.ViewSaleOrderDetails;
                if (!string.IsNullOrEmpty(SO.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(SO.OrderNo));

                if (SO.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == SO.BuyerId);

                if (SO.IsShoesOrder == 0 )
                    query = query.Where(x => x.IsShoesOrder == SO.IsShoesOrder);

                if (!string.IsNullOrEmpty(SO.Code))
                    query = query.Where(x => x.Code.Contains(SO.Code));

                if (SO.ChkOrderDateFromInt == 1 && SO.ChkOrderDateToInt == 1)
                    query = query.Where(x => x.OrderDate >= (SO.OrderdateFrom) && x.OrderDate <= SO.OrderDateTo);
                if (SO.ChkOrderDateFromInt == 1 && SO.ChkOrderDateToInt == 0)
                    query = query.Where(x => x.OrderDate == (SO.OrderdateFrom));

                if (SO.ChkDeliveryDateFromInt == 1 && SO.ChkDeliveryDateToInt == 1)
                    query = query.Where(x => x.DeliveryDate >= (SO.DeliveryDateFrom) && x.DeliveryDate <= SO.DeliveryDateTo);
                if (SO.ChkDeliveryDateFromInt == 1 && SO.ChkDeliveryDateToInt == 0)
                    query = query.Where(x => x.DeliveryDate == (SO.DeliveryDateFrom));

                if (SO.ChkCancelStatusInt == 1)
                    query = query.Where(x => x.CancelStatus == SO.ChkCancelStatusInt);
                //else
                //    query = query.Where(x => x.CancelStatus == SO.ChkCancelStatusInt);

                //query = query.OrderByDescending(x => x.OrderMasterID).ThenBy(x=> x.SNo);
                query = query.OrderBy(x => x.MyDeliveryDate).ThenBy(x => x.OrderMasterID).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSizeList/{fitemid}")]
        public IActionResult GetSizeList(long fitemid)
        {
            try
            {
                object List;
                List = _context.ViewProductSize
                                    .Where(x => x.FitemId == fitemid)
                                    .OrderBy(c => c.SizeId)
                                    .Select(x => new { Id = x.SizeId, Value = x.SizeName }).ToList();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSaleOrderPrint/{id}")]
        public IQueryable<ViewSaleOrderDetails> GetSaleOrderPrint(long id)
        {
            return _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == id).AsQueryable();
        }

        [HttpPut]
        [Route("PutRequestCancelStatus")]
        public IActionResult PutRequestCancelStatus([FromBody]SaleOrderMaster SO)
        {
            try
            {
                var existing = _context.SaleOrderMaster.Find(SO.OrderMasterID);
                existing.CancelStatus = SO.CancelStatus;
                existing.CancelDate = SO.CancelDate;
                existing.CancelByUserId = SO.CancelByUserId;
                existing.CancelRemark = SO.CancelRemark;
                _context.SaleOrderMaster.Update(existing);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [HttpPut]
        [Route("PutRequestUndoStatus")]
        public IActionResult PutRequestUndoStatus([FromBody]SaleOrderMaster SO)
        {
            try
            {
                var existing = _context.SaleOrderMaster.Find(SO.OrderMasterID);
                existing.CancelStatus = SO.CancelStatus;
                //existing.CancelDate = SO.CancelDate;
                //existing.CancelByUserId = SO.CancelByUserId;
                //existing.CancelRemark = SO.CancelRemark;
                _context.SaleOrderMaster.Update(existing);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("PostShoesSale")]
        public IActionResult PostShoesSale([FromBody]SaleOrderMaster value)
        {
            try
            {
                _context.SaleOrderMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetZOrderItems")]
        public IActionResult GetZOrderItems()
        {
            try
            {
                var SOid_in_wplist = _context.WorkPlanChild.Select(x => x.OrderChildId).ToList();
                var SO = _context.ViewSaleOrderDetails.Where(x => !SOid_in_wplist.Contains(x.OrderChildID))    //list of Not canceled po
                                        .OrderByDescending(x => x.OrderMasterID);
                return Ok(SO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutShoesSale")]
        public IActionResult PutShoesSale([FromBody]SaleOrderMaster value)
        {
            try
            {
                var existingOrder = _context.SaleOrderMaster
                        .Where(x => x.OrderMasterID == value.OrderMasterID)
                        .Include(x => x.SaleOrderChild)
                        .SingleOrDefault();

                if (existingOrder != null)
                {
                    // Update parent
                    _context.Entry(existingOrder).CurrentValues.SetValues(value);
                    //existingOrder.OrderNo = value.OrderNo;
                    //existingOrder.RefrenceNo = value.RefrenceNo;
                    //existingOrder.BuyerId = value.BuyerId;
                    //existingOrder.IssuedById = value.IssuedById;
                    //existingOrder.OrderDate = value.OrderDate;
                    //existingOrder.DeliveryDate = value.DeliveryDate;
                    //existingOrder.MyDeliveryDate = value.MyDeliveryDate;
                    //existingOrder.OrderEntryDate = value.OrderEntryDate;
                    //existingOrder.PRDApprovalDate = value.PRDApprovalDate;
                    //existingOrder.PRDStartDate = value.PRDStartDate;
                    //existingOrder.ShipmentSampleDate = value.ShipmentSampleDate;
                    //existingOrder.FOBCIF = value.FOBCIF;
                    //existingOrder.TransportId = value.TransportId;
                    //existingOrder.TransportCommments = value.TransportCommments;
                    //existingOrder.Remark = value.Remark; 

                    //existingOrder.TotalQty = value.TotalQty;
                    //existingOrder.TotalAmount = value.TotalAmount;

                    //existingOrder.MultipleDeliveryDate = value.MultipleDeliveryDate;
                    //existingOrder.MultipleTransport = value.MultipleTransport;

                    //existingOrder.UserID = value.UserID;

                    // Delete children
                    foreach (var ExistingOrderChild in existingOrder.SaleOrderChild.ToList())
                    {
                        _context.SaleOrderChild.Remove(ExistingOrderChild);
                    }

                    // Update and Insert children
                    foreach (var childSaleOrder in value.SaleOrderChild)
                    {
                        // Insert child 1
                        SaleOrderChild newChild = new SaleOrderChild();
                        newChild.OrderChildID = childSaleOrder.OrderChildID;
                        newChild.OrderMasterID = childSaleOrder.OrderMasterID;
                        newChild.FitemId = childSaleOrder.FitemId;
                        newChild.Qty = childSaleOrder.Qty;
                        newChild.Size = childSaleOrder.Size;
                        newChild.Price = childSaleOrder.Price;
                        newChild.Amount = childSaleOrder.Amount;
                        newChild.OrderChildRemark = childSaleOrder.OrderChildRemark;
                        newChild.ChildDeliveryDate = childSaleOrder.ChildDeliveryDate;
                        newChild.ChildMyDeliveryDate = childSaleOrder.ChildMyDeliveryDate;
                        newChild.TransportId = childSaleOrder.TransportId;
                        newChild.SNo = childSaleOrder.SNo;
                        existingOrder.SaleOrderChild.Add(newChild);
                    }

                    _context.SaveChanges();

                }
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderProductList/{ordermasterid}")]
        public IActionResult GetOrderProductList(long ordermasterid)
        {
            try
            {
                var data = _context.SaleOrderChild.Where(x => x.OrderMasterID == ordermasterid)
                                        .OrderBy(x => x.OrderChildID)
                                        .Select(x => new { FitemId = x.FitemId, OrderMasterId = x.OrderMasterID })
                                        .Distinct().ToList();

                //var orderproductlist = data.Select(x => new { FitemId = x.FitemId, Code = x.Code }).Distinct().ToList();
                //return Ok(orderproductlist);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetOrderItemsDetail/{ordermasterid}/{fitemid}")]
        public IActionResult GetOrderItemsDetail(long ordermasterid, long fitemid)
        {
            try
            {
                var data = _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == ordermasterid && x.FitemId == fitemid)
                                        .OrderBy(x => x.OrderChildID).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductSizeList/{fitemid}")]
        public IActionResult GetProductSizeList(long fitemid)
        {
            try
            {
                object List;
                List = _context.ViewProductSize
                                    .Where(x => x.FitemId == fitemid)
                                    .OrderBy(c => c.SizeId);//;
                                    //.Select(x => new { SizeId = x.SizeId, SizeName = x.SizeName }).ToList();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetFitemSizePrice/{fitemid}/{sizeid}")]
        public IActionResult GetFitemSizePrice(long fitemid, int sizeid)
        {
            try
            {
                double price = 0;
                var sizeprice = _context.ProductMultipleSize.Where(x => x.FitemId == fitemid && x.SizeId == sizeid).FirstOrDefault();
                price = (sizeprice == null ? 0 : sizeprice.SizePrice);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #region OrderStatus

        [Route("GetOrderItemsDetails/{id}")]
        public IActionResult GetOrderItemsDetails(long id)
        {
            try
            {
                var SO = _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == id)    //list of Not canceled po
                                        .OrderByDescending(x => x.OrderMasterID);
                return Ok(SO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetOrderStatus/{orderid}/{orderchildid}")]
        public IActionResult GetOrderStatus(long orderid, long orderchildid)
        {
            try
            {
                IQueryable<ViewSaleOrderStatus> query = _context.ViewSaleOrderStatus.Where(x => x.OrderMasterID == orderid && x.OrderChildID == orderchildid);

                query = query.OrderBy(x => x.MyDeliveryDate).ThenBy(x => x.PlanChildId);

                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #endregion

        #region SaleOrderCloseFile

        [Route("GetBuyerOrderNoList/{buyerid}")]
        public IActionResult GetBuyerOrderNoList(long buyerid)
        {
            try
            {
                var orderidlist = _context.OrderSheetClose.Select(x => x.OrderMasterID).ToList();
                object ordernolist;
                ordernolist = _context.SaleOrderMaster.Where(x => x.BuyerId == buyerid && !orderidlist.Contains(x.OrderMasterID))
                                        .OrderBy(c => c.OrderMasterID)
                                        .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo }).ToList();

                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderNoListForClose/{buyerid}")]
        public IActionResult GetOrderNoListForClose(long buyerid)
        {
            try
            {
                var orderidlist = _context.OrderSheetClose.Select(x => x.OrderMasterID).ToList();
                object ordernolist;
                ordernolist = _context.SaleOrderMaster.Where(x => x.BuyerId == buyerid && !orderidlist.Contains(x.OrderMasterID))
                                        .OrderBy(c => c.OrderMasterID)
                                        .Select(x => new { Id = x.OrderMasterID, Value = x.OrderNo }).ToList();

                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSaleOrderDetails/{id}")]
        public IActionResult GetSaleOrderDetails(long id)
        {
            try
            {
                var SO = _context.ViewSaleOrderDetails.Where(x => x.OrderMasterID == id)
                                        .OrderByDescending(x => x.OrderMasterID);
                return Ok(SO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostOrdeSheetClose")]
        public IActionResult PostOrdeSheetClose([FromBody]OrderSheetClose value)
        {
            try
            {
                long id = 0;
                var last = _context.OrderSheetClose.OrderBy(x => x.OrderSheetCloseId).LastOrDefault();
                id = (last == null ? 0 : last.OrderSheetCloseId) + 1;
                value.OrderSheetCloseId = id;
                _context.OrderSheetClose.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewSaleOrderSheetClose")]
        public IActionResult GetViewSaleOrderSheetClose([FromBody]OrderSearch SO)
        {
            try
            {
                IQueryable<ViewSaleOrderSheetClose> query = _context.ViewSaleOrderSheetClose;
                if (!string.IsNullOrEmpty(SO.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(SO.OrderNo));

                if (SO.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == SO.BuyerId);

                if (SO.ChkOrderDateFromInt == 1 && SO.ChkOrderDateToInt == 1)
                    query = query.Where(x => x.CloseDate >= (SO.OrderdateFrom) && x.CloseDate <= SO.OrderDateTo);
                if (SO.ChkOrderDateFromInt == 1 && SO.ChkOrderDateToInt == 0)
                    query = query.Where(x => x.CloseDate == (SO.OrderdateFrom));

                query = query.OrderByDescending(x => x.OrderSheetCloseId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteOrderSheetClose/{id}")]
        public IActionResult DeleteOrderSheetClose(long id)
        {
            try
            {
                var existing = _context.OrderSheetClose
                     .Where(x => x.OrderSheetCloseId == id)
                     .SingleOrDefault();
                if (existing != null)
                {
                    _context.OrderSheetClose.Remove(existing);
                    _context.SaveChanges();
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

        #endregion
    }
}
