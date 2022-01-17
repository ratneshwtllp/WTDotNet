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
    public class POController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public POController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ViewPOMaster> Get()
        {
            //return _context.ViewPOMaster
            //    .OrderByDescending(x => x.Poid).AsQueryable();     //list of all the po 

            return _context.ViewPOMaster.Where(x => x.CancelStatus == 0)    //list of Not canceled po
                .OrderByDescending(x => x.Poid).AsQueryable();

            //return _context.ViewPOMaster.Where(x => x.CancelStatus == 1)    //list of canceled po
            //    .OrderByDescending(x => x.Poid).AsQueryable();
        }

        [Route("GetPOForPrint/{id}")]
        public IQueryable<ViewPOPrint> GetPOForPrint(long id)
        {
            return _context.ViewPOPrint.Where(x => x.Poid == id).AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.Pomaster.Where(x => x.Poid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPOChild/{id}")]
        public IQueryable<ViewPOChild> GetPOChild(long id)
        {
            return _context.ViewPOChild.Where(x => x.POID == id).AsQueryable();
        }

        //[Route("GetNewPOID")]
        //public IActionResult GetNewPOID()
        //{
        //    try
        //    {
        //        long POID = 0;
        //        var lastPO = _context.Pomaster.OrderBy(x => x.Poid).LastOrDefault();
        //        POID = (lastPO == null ? 0 : lastPO.Poid) + 1;
        //        return Ok(POID);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewPOChildID")]
        public IActionResult GetNewPOChildID()
        {
            try
            {
                long POChildID = 0;
                var lastPOChild = _context.POChild.OrderBy(x => x.POChildID).LastOrDefault();
                POChildID = (lastPOChild == null ? 0 : lastPOChild.POChildID) + 1;
                return Ok(POChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPOSerial")]
        public IActionResult GetNewPOSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.Pomaster.OrderBy(x => x.Poid).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "PO").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.Pomaster.Where(x => x.SessionYear == session).OrderBy(x => x.POSerial).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.POSerial) + 1;
                }
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPONO")]
        public IActionResult GetNewPONO()
        {
            try
            {
                string session = API.Helper.Create_Sort_Session();
                long Serial = 0;
                var FirstRecord = _context.Pomaster.OrderBy(x => x.Poid).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "PO").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    //var LastRecord = _context.Pomaster.Where(x => x.SessionYear == "2016-17").OrderBy(x => x.POSerial).LastOrDefault();
                    var LastRecord = _context.Pomaster.OrderBy(x => x.Poid).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.Poid) + 1;
                }
                //String DisplayString = FormSettings.Prefix + "/" + "17-18" + "/";
                String DisplayString = FormSettings.Prefix + "/";
                switch (FormSettings.NoOfDigits)
                {
                    case 3:
                        DisplayString += string.Format("{0:000}", Serial);
                        break;
                    case 4:
                        DisplayString += string.Format("{0:0000}", Serial);
                        break;
                    case 5:
                        DisplayString += string.Format("{0:00000}", Serial);
                        break;
                    default:
                        break;
                }
                DisplayString += "/" + session;
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewPOMaster")]
        public IActionResult GetViewPOMaster(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewPOMaster.Where(x => x.CancelStatus == 0)
                        .OrderByDescending(x => x.Poid));
                else
                {
                    var PO = _context.ViewPOMaster
                                    .Where(x => (x.CancelStatus == 0) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search)))
                                    .OrderByDescending(x => x.Poid);
                    return Ok(PO);
                }

                //if (string.IsNullOrEmpty(search))
                //    return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 0)
                //        .OrderByDescending(x => x.Poid).Take(20));
                //else
                //{
                //    var PO = _context.ViewPOPrint
                //                    .Where(x => (x.CancelStatus == 0) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
                //                    .OrderByDescending(x => x.Poid)
                //                    .ThenBy(x => x.SNo);
                //    return Ok(PO);
                //}
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
                long POID = 0;
                var lastPO = _context.Pomaster.OrderBy(x => x.Poid).LastOrDefault();
                POID = (lastPO == null ? 0 : lastPO.Poid);

                if (string.IsNullOrEmpty(search))
                {
                    //if (POID > 20)
                    //{
                    //    return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 0 && x.Poid > POID - 20)
                    //   .OrderByDescending(x => x.Poid));
                    //}
                    //else
                    //{
                    //    return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 0)
                    //      .OrderByDescending(x => x.Poid));
                    //}
                    return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 0)
                          .OrderByDescending(x => x.Poid));
                }
                else
                {
                    var poid_list = _context.ViewPOPrint
                                    .Where(x => (x.CancelStatus == 0) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
                                   .Select(x => x.Poid).ToArray();

                    //var PO = _context.ViewPOPrint
                    //                .Where(x => (x.CancelStatus == 0) && (poid_list.Contains(x.Pono)) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
                    //                .OrderByDescending(x => x.Poid)
                    //                .ThenBy(x => x.SNo);

                    var PO = _context.ViewPOPrint
                                    .Where(x => poid_list.Contains(x.Poid))
                                    .OrderByDescending(x => x.Poid)
                                    .ThenBy(x => x.SNo);
                    return Ok(PO);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetListCancel")]
        public IActionResult GetListCancel(string search)
        {
            try
            {
                long POID = 0;
                var lastPO = _context.Pomaster.OrderBy(x => x.Poid).LastOrDefault();
                POID = (lastPO == null ? 0 : lastPO.Poid);

                if (string.IsNullOrEmpty(search))
                {
                    if (POID > 20)
                    {
                        return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 1 && x.Poid > POID - 20)
                        .OrderByDescending(x => x.Poid));
                    }
                    else
                    {
                        return Ok(_context.ViewPOPrint.Where(x => x.CancelStatus == 1)
                        .OrderByDescending(x => x.Poid));
                    }
                }
                else
                {
                    var poid_list = _context.ViewPOPrint
                                    .Where(x => (x.CancelStatus == 1) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
                                   .Select(x => x.Poid).ToArray();

                    //var PO = _context.ViewPOPrint
                    //                .Where(x => (x.CancelStatus == 0) && (poid_list.Contains(x.Pono)) && (x.Pono.Contains(search) || x.SupplierName.Contains(search) || x.SupplierEmail.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
                    //                .OrderByDescending(x => x.Poid)
                    //                .ThenBy(x => x.SNo);

                    var PO = _context.ViewPOPrint
                                    .Where(x => poid_list.Contains(x.Poid))
                                    .OrderByDescending(x => x.Poid)
                                    .ThenBy(x => x.SNo);
                    return Ok(PO);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]POMaster value)
        {
            try
            {
                long id = _context.POForSaleOrderDetails.Max(x=> x.POForSaleOrderId);
                id++;
                foreach (POForSaleOrderDetails child in value.POForSaleOrderDetails)
                {
                    child.POForSaleOrderId = id;
                    id++;
                }

                _context.Pomaster.Add(value);
                _context.SaveChanges();

                _context.Database.ExecuteSqlCommand("EXEC SP_UpdateMultipleOrdersInPO " + value.Poid  + " ");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]POMaster value)
        {
            try
            {
                var existingpo = _context.Pomaster
                        .Where(x => x.Poid == value.Poid)
                        .Include(x => x.POChild)
                        .Include(x=> x.POForSaleOrderDetails)
                        .SingleOrDefault();

                if (existingpo != null)
                {
                    // Update parent
                    _context.Entry(existingpo).CurrentValues.SetValues(value);
                   
                    foreach (var ExistingPoChild in existingpo.POChild.ToList())
                    {
                        bool isChildExist = false;
                        foreach (var childPO in value.POChild)
                        {
                            if (ExistingPoChild.POChildID == childPO.POChildID)
                            {
                                isChildExist = true;

                                // Update children
                                ExistingPoChild.POChildID = childPO.POChildID;
                                ExistingPoChild.POID = childPO.POID;
                                ExistingPoChild.RItem_Id = childPO.RItem_Id;
                                ExistingPoChild.Qty = childPO.Qty;
                                ExistingPoChild.Unit = childPO.Unit;
                                ExistingPoChild.FactoryQty = childPO.FactoryQty;
                                ExistingPoChild.FactoryUnit = childPO.FactoryUnit;
                                ExistingPoChild.Rate = childPO.Rate;
                                ExistingPoChild.Amount = childPO.Amount;
                                ExistingPoChild.SNo = childPO.SNo;
                                ExistingPoChild.RDesc = childPO.RDesc;

                                ExistingPoChild.HSNCode = childPO.HSNCode;
                                ExistingPoChild.TaxId = childPO.TaxId;
                                ExistingPoChild.IGSTPer = childPO.IGSTPer;
                                ExistingPoChild.IGSTAmt = childPO.IGSTAmt;
                                ExistingPoChild.CGSTPer = childPO.CGSTPer;
                                ExistingPoChild.CGSTAmt = childPO.CGSTAmt;
                                ExistingPoChild.SGSTPer = childPO.SGSTPer;
                                ExistingPoChild.SGSTAmt = childPO.SGSTAmt;
                                ExistingPoChild.AmtAfterTax = childPO.AmtAfterTax;
                                ExistingPoChild.ConversionFactor = childPO.ConversionFactor;
                                break;
                            }
                        }
                        if (isChildExist == false)
                        {
                            // Delete children
                            _context.POChild.Remove(ExistingPoChild);
                        }
                    }

                    // Update and Insert children
                    foreach (var childPO in value.POChild)
                    {
                        bool isChildExist = _context.POChild.Where(x => x.POChildID == childPO.POChildID).Any();
                        if (isChildExist == true)
                        {
                           
                        }
                        else
                        {
                            // add new child
                            existingpo.POChild.Add(childPO);
                        }
                    }

                    // Update and Insert children
                    foreach (var child in value.POForSaleOrderDetails)
                    {
                        existingpo.POForSaleOrderDetails.Remove(child);
                    }
                    long id = _context.POForSaleOrderDetails.Max(x => x.POForSaleOrderId);
                    id++;
                    foreach (var child in value.POForSaleOrderDetails)
                    {
                        child.POForSaleOrderId = id;
                        id++;
                        existingpo.POForSaleOrderDetails.Add(child);
                    }

                    _context.SaveChanges();
                }
                _context.Database.ExecuteSqlCommand("EXEC SP_UpdateMultipleOrdersInPO " + value.Poid + " ");

                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCurrencyofSupplier/{suppid}")]
        public IActionResult GetCurrencyofSupplier(long suppid)
        {
            try
            {
                //var CurrSName = _context.ViewSupplierDetails
                //    .Where(x => x.SupplierId == suppid).Select(x => new { Value = x.CSName }); 
                //return Ok(CurrSName);

                var CurrSName = "";
                var Curr = _context.ViewSupplierDetails
                   .Where(x => x.SupplierId == suppid).LastOrDefault();
                CurrSName = (Curr == null ? "-" : Curr.CSName);
                return Ok(CurrSName);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutPOStatus/{poid}")]
        public IActionResult PutPOStatus(long poid)
        {
            try
            {
                var existingPO = _context.Pomaster.Find(poid);
                existingPO.CancelStatus = 1;
                _context.Pomaster.Update(existingPO);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutPOEmailStatus/{poid}")]
        public IActionResult PutPOEmailStatus(long poid)
        {
            try
            {
                var existingPO = _context.Pomaster.Find(poid);
                existingPO.EmailStatus = 1;
                existingPO.EmailDate = DateTime.Now.Date;
                _context.Pomaster.Update(existingPO);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PODelayReport")]
        public IActionResult PODelayReport()
        {
            try
            {
                var PoIdlist = _context.ViewPODelay
                                         .OrderBy(x => x.SupplierId)
                                         .Select(x => x.SupplierId).ToList();
                var podelay = _context.ViewPODelay.Where(x => PoIdlist.Contains(x.SupplierId));
                return Ok(podelay);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PODelaySum")]
        public IActionResult PODelaySum()
        {
            try
            {
                var PoIdlist = _context.ViewPODelaySum
                                         .OrderBy(x => x.SupplierName)
                                         .Select(x => x.SupplierId).ToList();
                var podelaysum = _context.ViewPODelaySum.Where(x => PoIdlist.Contains(x.SupplierId));
                return Ok(podelaysum);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierPerformanceList")]
        public IActionResult GetSupplierPerformanceList(long supplierid)
        {
            try
            {
                return Ok(_context.ViewSupplierPerformance.Where(x => x.SupplierID == supplierid).OrderByDescending(x => x.POID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierAverageDelayList")]
        public IActionResult GetSupplierAverageDelayList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSupplierPerformanceSumarray.OrderByDescending(x => x.SupplierId));
                else
                    return Ok(_context.ViewSupplierPerformanceSumarray
                        .Where(x => x.SupplierName.Contains(search))
                        .OrderByDescending(x => x.SupplierId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ViewPO")]
        public IActionResult ViewPO([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewPOPrint> query = _context.ViewPOPrint;
                if (IS.RMCategoryID > 0)
                {
                    if (IS.RMSubCategoryID > 0)
                    {
                        var poidlist = _context.ViewPOPrint.Where(x => x.CategoryID == IS.RMCategoryID && x.SubCategoryID == IS.RMSubCategoryID).Select(x => x.Poid).Distinct().ToList();
                        query = query.Where(x => poidlist.Contains(x.Poid));
                    }
                    else
                    {
                        var poidlist = _context.ViewPOPrint.Where(x => x.CategoryID == IS.RMCategoryID).Select(x => x.Poid).Distinct().ToList();
                        query = query.Where(x => poidlist.Contains(x.Poid));
                    }
                }
                //if (!string.IsNullOrEmpty(IS.RMName))
                //{
                //    var poidlist = _context.ViewPOPrint.Where(x => x.Full_Name.Contains(IS.RMName) || x.Code.Contains(IS.RMName)).Select(x => x.Poid).Distinct().ToList();
                //    query = query.Where(x => poidlist.Contains(x.Poid));
                //}

                if (!string.IsNullOrEmpty(IS.RMName))
                {
                    var poidlist = _context.ViewPOPrint.Where(x => x.Full_Name.Contains(IS.RMName) || x.Code.Contains(IS.RMName)).Select(x => x.Poid).Distinct().ToList();
                    query = query.Where(x => poidlist.Contains(x.Poid));
                }
                
                if (IS.BranchID > 0)
                    query = query.Where(x => x.BranchID == IS.BranchID);

                if (IS.ChkCreationDateFromInt == 1 && IS.ChkCreationDateToInt == 1)
                    query = query.Where(x => x.EntryDate >= (IS.CreationDateFrom) && x.EntryDate <= IS.CreationDateTo);
                if (IS.ChkCreationDateFromInt == 1 && IS.ChkCreationDateToInt == 0)
                    query = query.Where(x => x.EntryDate == (IS.CreationDateFrom));


                if (IS.SupplierId > 0)
                    query = query.Where(x => x.Sid == IS.SupplierId);
                if (IS.MonthId > 0)
                    query = query.Where(x => x.Podate.Month == IS.MonthId);
                if (IS.YearName > 0)
                    query = query.Where(x => x.Podate.Year == IS.YearName);
                if (!string.IsNullOrEmpty(IS.PONO))
                    query = query.Where(x => x.Pono.Contains(IS.PONO));
                if (IS.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == IS.Session_Year);
                if (IS.CancelStatus != 2)
                    query = query.Where(x => x.CancelStatus == IS.CancelStatus);

                if (IS.CID > 0)
                    query = query.Where(x => x.CID == IS.CID);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 1)
                    query = query.Where(x => x.Podate >= (IS.PODateFrom) && x.Podate <= IS.PODateTo);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 0)
                    query = query.Where(x => x.Podate == (IS.PODateFrom));

                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 1)
                    query = query.Where(x => x.Dldate >= (IS.DeliveryDateFrom) && x.Dldate <= IS.DeliveryDateTo);
                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 0)
                    query = query.Where(x => x.Dldate == (IS.DeliveryDateFrom));

                query = query.OrderByDescending(x => x.Poid).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetPOBalanceList")]
        public IActionResult GetPOBalanceList([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewPOBalance> query = _context.ViewPOBalance.Where(x => x.GRNQtySuppUnit < x.Qty);
                if (IS.BranchID > 0)
                    query = query.Where(x => x.BranchID == IS.BranchID);
                if (IS.Session_Year != "-- Select --")
                    query = query.Where(x => x.Sessionyear.Contains(IS.Session_Year));
                if (IS.MonthId > 0)
                    query = query.Where(x => x.Podate.Month == IS.MonthId);
                if (IS.YearName > 0)
                    query = query.Where(x => x.Podate.Year == IS.YearName);
                if (!string.IsNullOrEmpty(IS.PONO))
                    query = query.Where(x => x.Pono.Contains(IS.PONO));



                if (IS.SupplierId > 0)
                    query = query.Where(x => x.Sid == IS.SupplierId);
                if (IS.RMCategoryID > 0)
                    query = query.Where(x => x.CategoryID == IS.RMCategoryID);
                if (IS.RMSubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == IS.RMSubCategoryID);
                if (!string.IsNullOrEmpty(IS.RMName))
                    query = query.Where(x => x.Name.Contains(IS.RMName) || x.Code.Contains(IS.RMName));
                if (IS.POBalaceTypeId == 2)
                    query = query.Where(x => x.GRNQtySuppUnit == 0);
                if (IS.POBalaceTypeId == 3)
                    query = query.Where(x => x.GRNQtySuppUnit > 0);


                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 1)
                    query = query.Where(x => x.Podate >= (IS.PODateFrom) && x.Podate <= IS.PODateTo);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 0)
                    query = query.Where(x => x.Podate == (IS.PODateFrom));

                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 1)
                    query = query.Where(x => x.Dldate >= (IS.DeliveryDateFrom) && x.Dldate <= IS.DeliveryDateTo);
                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 0)
                    query = query.Where(x => x.Dldate == (IS.DeliveryDateFrom));

                if (IS.ChkCreationDateFromInt == 1 && IS.ChkCreationDateToInt == 1)
                    query = query.Where(x => x.EntryDate >= (IS.CreationDateFrom) && x.EntryDate <= IS.CreationDateTo);
                if (IS.ChkCreationDateFromInt == 1 && IS.ChkCreationDateToInt == 0)
                    query = query.Where(x => x.EntryDate == (IS.CreationDateFrom));

                query = query.OrderByDescending(x => x.POID).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetPONO_Session")]
        public IActionResult GetPONO_Session([FromBody]POMaster _oPOMaster)
        {
            try
            {
                string session = API.Helper.Create_Sort_Session_FromDate(Convert.ToDateTime(_oPOMaster.Podate));
                POMaster _POMaster = new POMaster();
                DateTime LastDateOfPO_AgainstSession = _context.Pomaster.Where(x => x.SessionYear == session).Max(x => x.Podate);
                long Poid = _context.Pomaster.Max(x => x.Poid);
                Poid++;
                int POSerial = 0;
                var FirstRecord = _context.Pomaster.OrderBy(x => x.Poid).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "PO").FirstOrDefault();

                if (FirstRecord == null)
                {
                    POSerial = FormSettings.StartingNumber;
                }
                else
                {
                    POSerial = _context.Pomaster.Where(x => x.SessionYear == session).Max(x => x.POSerial);
                    POSerial++;
                }
                String Pono = FormSettings.Prefix + "/";
                switch (FormSettings.NoOfDigits)
                {
                    case 3:
                        Pono += string.Format("{0:000}", POSerial);
                        break;
                    case 4:
                        Pono += string.Format("{0:0000}", POSerial);
                        break;
                    case 5:
                        Pono += string.Format("{0:00000}", POSerial);
                        break;
                    default:
                        break;
                }
                Pono += "/" + session;

                _POMaster.Pono = Pono;
                _POMaster.POSerial = POSerial;
                _POMaster.SessionYear = session;
                _POMaster.Poid = Poid;
                _POMaster.NewDate = LastDateOfPO_AgainstSession;

                return Ok(_POMaster);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #region Running&CompletePOList

        [HttpPost]
        [Route("GetRunningPOList")]
        public IActionResult GetRunningPOList([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewRunningPOList> query = _context.ViewRunningPOList;


                if (IS.SupplierId > 0)
                    query = query.Where(x => x.Sid == IS.SupplierId);
                if (IS.MonthId > 0)
                    query = query.Where(x => x.Podate.Month == IS.MonthId);
                if (IS.YearName > 0)
                    query = query.Where(x => x.Podate.Year == IS.YearName);
                if (!string.IsNullOrEmpty(IS.PONO))
                    query = query.Where(x => x.Pono.Contains(IS.PONO));
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 1)
                    query = query.Where(x => x.Podate >= (IS.PODateFrom) && x.Podate <= IS.PODateTo);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 0)
                    query = query.Where(x => x.Podate == (IS.PODateFrom));


                query = query.OrderByDescending(x => x.Poid).ThenBy(x => x.Sid);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RunningPOListComplete/{poid}")]
        public IActionResult RunningPOListComplete(long poid)
        {
            try
            {
                var existingPO = _context.Pomaster.Find(poid);
                existingPO.Complete = 1;
                _context.Pomaster.Update(existingPO);

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
        [Route("PutPOCompleteStatus")]
        public IActionResult PutPOCompleteStatus([FromBody]List<POMaster> value)
        {
            try
            {


                foreach (var item in value)
                {
                    var existingpo = _context.Pomaster
                      .Where(x => x.Poid == item.Poid)
                      .SingleOrDefault();

                    //var existingPO = _context.Pomaster.Find(item);
                    existingpo.Complete = 1;
                    _context.Pomaster.Update(existingpo);
                }
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
        [Route("GetCompletedPOList")]
        public IActionResult GetCompletedPOList([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewPOPrint> query = _context.ViewPOPrint;
                if (IS.RMCategoryID > 0)
                {
                    if (IS.RMSubCategoryID > 0)
                    {
                        var poidlist = _context.ViewPOPrint.Where(x => x.CategoryID == IS.RMCategoryID && x.SubCategoryID == IS.RMSubCategoryID).Select(x => x.Poid).Distinct().ToList();
                        query = query.Where(x => poidlist.Contains(x.Poid) && x.Complete == 1);
                    }
                    else
                    {
                        var poidlist = _context.ViewPOPrint.Where(x => x.CategoryID == IS.RMCategoryID).Select(x => x.Poid).Distinct().ToList();
                        query = query.Where(x => poidlist.Contains(x.Poid) && x.Complete == 1);
                    }
                }
                if (!string.IsNullOrEmpty(IS.RMName))
                {
                    var poidlist = _context.ViewPOPrint.Where(x => x.Full_Name.Contains(IS.RMName) || x.Code.Contains(IS.RMName)).Select(x => x.Poid).Distinct().ToList();
                    query = query.Where(x => poidlist.Contains(x.Poid) && x.Complete == 1);
                }

                if (IS.SupplierId > 0)
                    query = query.Where(x => x.Sid == IS.SupplierId && x.Complete == 1);
                if (IS.MonthId > 0)
                    query = query.Where(x => x.Podate.Month == IS.MonthId && x.Complete == 1);
                if (IS.YearName > 0)
                    query = query.Where(x => x.Podate.Year == IS.YearName && x.Complete == 1);
                if (!string.IsNullOrEmpty(IS.PONO))
                    query = query.Where(x => x.Pono.Contains(IS.PONO) && x.Complete == 1);
                if (IS.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == IS.Session_Year && x.Complete == 1);
                if (IS.CancelStatus != 2)
                    query = query.Where(x => x.CancelStatus == IS.CancelStatus && x.Complete == 1);

                if (IS.CID > 0)
                    query = query.Where(x => x.CID == IS.CID && x.Complete == 1);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 1)
                    query = query.Where(x => x.Podate >= (IS.PODateFrom) && x.Podate <= IS.PODateTo && x.Complete == 1);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 0)
                    query = query.Where(x => x.Podate == (IS.PODateFrom) && x.Complete == 1);

                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 1)
                    query = query.Where(x => x.Dldate >= (IS.DeliveryDateFrom) && x.Dldate <= IS.DeliveryDateTo && x.Complete == 1);
                if (IS.ChkDeliveryDateFromInt == 1 && IS.ChkDeliveryDateToInt == 0)
                    query = query.Where(x => x.Dldate == (IS.DeliveryDateFrom) && x.Complete == 1);

                query = query.Where(x => x.Complete == 1);
                query = query.OrderByDescending(x => x.Poid).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("UndoCompletedPOList/{poid}")]
        public IActionResult UndoCompletedPOList(long poid)
        {
            try
            {
                var existingPO = _context.Pomaster.Find(poid);
                existingPO.Complete = 0;
                _context.Pomaster.Update(existingPO);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #endregion
        #region DalayedMeterialList
        [HttpPost]
        [Route("GetDelayedMaterialList")]
        public IActionResult GetDelayedMaterialList([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewDelayedMaterial> query = _context.ViewDelayedMaterial;


                if (IS.SupplierId > 0)
                    query = query.Where(x => x.SID == IS.SupplierId);
                if (IS.MonthId > 0)
                    query = query.Where(x => x.Podate.Month == IS.MonthId);
                if (IS.YearName > 0)
                    query = query.Where(x => x.Podate.Year == IS.YearName);
                if (!string.IsNullOrEmpty(IS.PONO))
                    query = query.Where(x => x.PONO.Contains(IS.PONO));
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 1)
                    query = query.Where(x => x.Podate >= (IS.PODateFrom) && x.Podate <= IS.PODateTo);
                if (IS.ChkPODateFromInt == 1 && IS.ChkPODateToInt == 0)
                    query = query.Where(x => x.Podate == (IS.PODateFrom));


                query = query.OrderByDescending(x => x.POID).ThenBy(x => x.SID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #endregion
    }
}
