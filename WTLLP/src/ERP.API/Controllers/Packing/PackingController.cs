using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;
using System.Collections.Generic;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class PackingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PackingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<PackingMaster> Get()
        {
            return _context.PackingMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.PackingMaster.Where(x => x.PackingID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Get")]
        public IActionResult Get(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewPackingMaster
                        .OrderByDescending(x => x.PackingID));
                else
                    return Ok(_context.ViewPackingMaster
                        .Where(x => x.PackingNo.Contains(search) || x.BuyerName.Contains(search))   //|| x.Code.Contains(search) || x.Name.Contains(search)
                        .OrderByDescending(x => x.PackingID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewid")]
        public IActionResult GetNewid()
        {
            try
            {
                long PackingID = 0;
                var lastpacking = _context.PackingMaster.OrderBy(x => x.PackingID).LastOrDefault();
                PackingID = (lastpacking == null ? 0 : lastpacking.PackingID) + 1;
                return Ok(PackingID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewChildid")]
        public IActionResult GetNewChildid()
        {
            try
            {
                long PackingChildID = 0;
                var lastpackingChild = _context.PackingChild.OrderBy(x => x.PackingChildID).LastOrDefault();
                PackingChildID = (lastpackingChild == null ? 0 : lastpackingChild.PackingChildID) + 1;
                return Ok(PackingChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetNewSerial")]
        //public IActionResult GetNewSerial()
        //{
        //    try
        //    {
        //        long PackingSerialNo = 0;
        //        var lastPacking = _context.PackingMaster.OrderBy(x => x.PackingSerial).LastOrDefault();
        //        PackingSerialNo = (lastPacking == null ? 0 : lastPacking.PackingSerial) + 1;
        //        return Ok(PackingSerialNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewSerial")]
        public IActionResult GetNewSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.Pomaster.OrderBy(x => x.Poid).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Packing").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.PackingMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PackingID).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PackingID) + 1;
                }
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewNo")]
        public IActionResult GetNewNo()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.PackingMaster.OrderBy(x => x.PackingID).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Packing").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.PackingMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PackingSerial).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PackingSerial) + 1;
                }

                String DisplayString = FormSettings.Prefix + "/" + session + "/";
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
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetNewNo")]
        //public IActionResult GetNewNo()
        //{
        //    try
        //    {
        //        long PackingSerialNo = 0;
        //        var lastPacking = _context.PackingMaster.OrderBy(x => x.PackingSerial).LastOrDefault();
        //        PackingSerialNo = (lastPacking == null ? 0 : lastPacking.PackingSerial) + 1;

        //        String PackingNO = "SB/PCK/17-18/" + string.Format("{0:0000}", PackingSerialNo) + "";
        //        return Ok(PackingNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetList")]
        public IActionResult GetList()
        {
            try
            {
                var packingnolist = _context.PackingMaster    //list of Not canceled po
                                        .OrderByDescending(x => x.PackingID)
                                        .Select(x => new { Id = x.PackingID, Value = x.PackingNo })
                                        .ToList();
                return Ok(packingnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPendingList")]
        public IActionResult GetPendingList()
        {
            try
            {
                var packid = _context.InvoiceMaster.Select(x => x.PackingID).ToList();
                var packingnolist = _context.PackingMaster
                                        .Where(x => !packid.Contains(x.PackingID))
                                        .OrderByDescending(x => x.PackingID)
                                        .Select(x => new { Id = x.PackingID, Value = x.PackingNo })
                                        .ToList();
                return Ok(packingnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCompleteList")]
        public IActionResult GetCompleteList()
        {
            try
            {
                var packid = _context.InvoiceMaster.Select(x => x.PackingID).ToList();
                var packingnolist = _context.PackingMaster
                                        .Where(x => packid.Contains(x.PackingID))
                                        .OrderByDescending(x => x.PackingID)
                                        .Select(x => new { Id = x.PackingID, Value = x.PackingNo })
                                        .ToList();
                return Ok(packingnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSinglePackingNo/{id}")]
        public IActionResult GetSinglePackingNo(long id)
        {
            try
            {
                object ordernolist;
                ordernolist = _context.PackingMaster.Where(x => x.PackingID == id)
                                        .OrderBy(c => c.PackingID)
                                        .Select(x => new { Id = x.PackingID, Value = x.PackingNo }).ToList();
                return Ok(ordernolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetChild/{id}")]
        public IActionResult GetChild(long id)
        {
            try
            {
                var childrecord = _context.ViewPackingChild.Where(x => x.PackingID == id)
                                  .OrderBy(x => x.CartonNo).ThenBy(x=> x.Qty);
                return Ok(childrecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCartonList/{id}")]
        public IActionResult GetCartonList(long id)
        {
            try
            {
                var packingnolist = _context.ViewPackingCartons
                    .Where(x => x.PackingID == id)
                    .OrderBy(x => x.CartonNo);

                return Ok(packingnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerFromPacking/{id}")]
        public IActionResult GetBuyerFromPacking(long id)
        {
            try
            {
                var buyer = _context.ViewPackingMaster.Where(x => x.PackingID == id)
                    .FirstOrDefault();

                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRecordForInvoice/{id}")]
        public IActionResult GetRecordForInvoice(long id)
        {
            try
            {
                var packingnoRecord = from c in _context.ViewPackingChild.Where(x => x.PackingID == id)
                                      group c by new { c.PartyCode, c.Code, c.Description, c.CLName, c.Price, c.FitemId,c.Unit, c.ProductSizeName, c.ProductSizeId, c.CSName } into g   //, c.BuyerID, c.BuyerName
                                      orderby g.Key.Code
                                      select new
                                      {
                                          PartyCode = g.Key.PartyCode,
                                          Code = g.Key.Code,
                                          Description = g.Key.Description,
                                          CLName = g.Key.CLName,
                                          PackingQty = g.Sum(x => x.PackingQty),
                                          Price = g.Key.Price,
                                          FitemId = g.Key.FitemId,
                                          Unit = g.Key.Unit,
                                          ProductSizeId = g.Key.ProductSizeId,
                                          ProductSizeName = g.Key.ProductSizeName,
                                          //BuyerID = g.Key.BuyerID,
                                          //BuyerName = g.Key.BuyerName
                                          CSName = g.Key.CSName
                                      };

                return Ok(packingnoRecord);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRecord/{id}")]
        public IActionResult GetRecord(long id)
        {
            try
            {
                var packingnoRecord = _context.ViewPackingChild.Where(x => x.PackingID == id)
                                                   .OrderBy(x => x.Code).ThenBy(x => x.CartonNo);

                return Ok(packingnoRecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PackingMaster value)
        {
            try
            {
                _context.PackingMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]PackingMaster value)
        {
            try
            {

                var existingPacking = _context.PackingMaster
                        .Where(x => x.PackingID == value.PackingID)
                        .Include(x => x.PackingChild)
                        .SingleOrDefault();

                if (existingPacking != null)
                {
                    // Update parent
                    _context.Entry(existingPacking).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var ExistingPackingChild in existingPacking.PackingChild.ToList())
                    {
                        _context.PackingChild.Remove(ExistingPackingChild);
                    }

                    // Update and Insert children
                    foreach (var childPacking in value.PackingChild)
                    {
                        // Insert child 1
                        PackingChild newChild = new PackingChild();
                        newChild.PackingChildID = childPacking.PackingChildID;
                        newChild.PackingID = childPacking.PackingID;
                        newChild.OrderChildID = childPacking.OrderChildID;
                        newChild.CartonNo = childPacking.CartonNo;
                        newChild.CartonID = childPacking.CartonID;
                        newChild.PackingQty = childPacking.PackingQty;
                        newChild.SNo = childPacking.SNo;
                        newChild.Barcode = childPacking.Barcode;
                        newChild.FitemId = childPacking.FitemId;
                        newChild.SizeId  = childPacking.SizeId;
                        existingPacking.PackingChild.Add(newChild);
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

        [Route("ChangeStatus/{id}")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var existingPacking = _context.PackingMaster.Find(id);
                existingPacking.IsWeigh = 1;
                _context.PackingMaster.Update(existingPacking);

                _context.SaveChanges();
                var result = "Status Updated";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCartonNo")]
        public IActionResult GetNewCartonNo()
        {
            try
            {
                long cartonno_tbno = 0;
                var maxno = _context.ShoesCartonMaster.Max(x => x.CartonNo);
                cartonno_tbno = (maxno == 0 ? 0 : maxno) + 1;
                return Ok(cartonno_tbno);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetShoesPackingRecord/{id}")]
        public IActionResult GetShoesPackingRecord(long id)
        {
            try
            {
                var childrecord = _context.ViewShoesPacking.Where(x => x.PackingID == id)
                    .OrderBy(x => x.SNo);
                return Ok(childrecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBarcodeDetails/{orderid}/{barcode}")]
        public IActionResult GetBarcodeDetails(long orderid, string barcode)
        {
            try
            {
                //var product = _context.ViewProductSize.Where(x => x.SizeBarcode == barcode)
                //                       .OrderByDescending(x => x.FitemId).FirstOrDefault();

                //if (product == null) { return Ok("1"); }
                //else
                //{
                //    var record = _context.SaleOrderChild.Where(x => x.OrderMasterID == orderid && x.FitemId == product.FitemId && x.Size == product.SizeId)
                //                    .SingleOrDefault();
                //    if (record == null) { return Ok("2"); }
                //    else
                //    {

//                var text = '{"employees":[' +
//'{"firstName":"John","lastName":"Doe" },' +
//'{"firstName":"Anna","lastName":"Smith" },' +
//'{"firstName":"Peter","lastName":"Jones" }]}';

                //var data = " [{ "'fitemId'": + product.FitemId + " ,'code':'" + product.Code +
                //             "', 'sizeId':" + product.SizeId + " ,'sizeName':'" + product.SizeName +
                //             "', 'clName':" + product.CLName + " ,'orderChildId':" + record.OrderChildID + " }]";
                //        return Ok(data);
                //    }
                //}

                var product = _context.ViewSaleOrderChild.Where(x => x.OrderMasterID == orderid && x.SizeBarcode == barcode)
                                       .OrderByDescending(x => x.FitemId).FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetNewShoesCartonMasterId")]
        public IActionResult GetNewShoesCartonMasterId()
        {
            try
            {
                long masterid = 0;
                var last = _context.ShoesCartonMaster.OrderBy(x => x.ShoesCartonMasterId).LastOrDefault();
                masterid = (last == null ? 0 : last.ShoesCartonMasterId) + 1;
                return Ok(masterid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewShoesCartonChildId")]
        public IActionResult GetNewShoesCartonChildId()
        {
            try
            {
                long childid = 0;
                var last= _context.ShoesCartonChild.OrderBy(x => x.ShoesCartonChildId).LastOrDefault();
                childid = (last == null ? 0 : last.ShoesCartonChildId) + 1;
                return Ok(childid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostShoesCarton")]
        public IActionResult PostShoesCarton([FromBody]ShoesCartonMaster value)
        {
            try
            {
                _context.ShoesCartonMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetShoesCartonList")]
        public IActionResult GetShoesCartonList([FromBody]PackingSearch PS)
        {
            try
            {
                IQueryable<ViewShoesCartonChild> query = _context.ViewShoesCartonChild;

                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));

                if (!string.IsNullOrEmpty(PS.Barcode))
                    query = query.Where(x => x.Barcode.Contains(PS.Barcode));

                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);

                if (PS.CartonNo > 0)
                    query = query.Where(x => x.CartonNo == PS.CartonNo);

                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.ShoesCartonDate >= (PS.DateFrom) && x.ShoesCartonDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.ShoesCartonDate == (PS.DateFrom)); 

                query = query.OrderByDescending(x => x.ShoesCartonMasterId).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
         
        [HttpPost] 
        [Route("GetShoesCartonListForPacking")]
        public IActionResult GetShoesCartonListForPacking([FromBody]PackingSearch PS)
        {
            try
            {
                var listshoescartonmasterid = _context.PackingChild.Select(x => x.ShoesCartonMasterId).ToList();
                    
                IQueryable<ViewShoesCartonChild> query = _context.ViewShoesCartonChild.Where(x=> !listshoescartonmasterid.Contains(x.ShoesCartonMasterId) );

                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));

                if (!string.IsNullOrEmpty(PS.Barcode))
                    query = query.Where(x => x.Barcode.Contains(PS.Barcode));

                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);

                if (PS.CartonNo > 0)
                    query = query.Where(x => x.CartonNo == PS.CartonNo);

                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.ShoesCartonDate >= (PS.DateFrom) && x.ShoesCartonDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.ShoesCartonDate == (PS.DateFrom));

                query = query.OrderByDescending(x => x.ShoesCartonMasterId).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetShoesPackingList")]
        public IActionResult GetShoesPackingList([FromBody]PackingSearch PS)
        {
            try
            {
                IQueryable<ViewShoesPackingChild> query = _context.ViewShoesPackingChild;

                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));

                if (!string.IsNullOrEmpty(PS.Barcode))
                    query = query.Where(x => x.Barcode.Contains(PS.Barcode));

                if (!string.IsNullOrEmpty(PS.PackingNo))
                    query = query.Where(x => x.PackingNo.Contains(PS.PackingNo));

                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerID == PS.BuyerId);

                if (PS.CartonNo > 0)
                    query = query.Where(x => x.CartonNo == PS.CartonNo);

                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.PackingDate >= (PS.DateFrom) && x.PackingDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.PackingDate == (PS.DateFrom));

                query = query.OrderByDescending(x => x.ShoesCartonMasterId).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerCodeList")]
        public IActionResult GetBuyerCodeList()
        {
            try
            {
                var buyeridlist= _context.SaleOrderMaster.Select(x => x.BuyerId ).ToList();
                object buyer;
                buyer = _context.BuyerDetails.Where (x=> buyeridlist.Contains (x.BuyerId ))
                                        .OrderBy(c => c.BuyerCode)
                                        .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProductionPackingPrint/{id}")]
        public IQueryable<ViewProductionPacking > ProductionPackingPrint(long id)
        {
            return _context.ViewProductionPacking.Where(x => x.PackingID   == id).AsQueryable();
        }
        [Route("PackingSlipPrint/{id}")]
        public IQueryable<ViewPackingChild > PackingSlipPrint(long id)
        {
            return _context.ViewPackingChild.Where(x => x.PackingID == id).AsQueryable();
        }


        [HttpPost]
        [Route("Viewpacking")]
        public IActionResult Viewpacking([FromBody]PackingSearch PS) 
        {
            try
            {
                IQueryable<ViewPackingMaster> query = _context.ViewPackingMaster;
                if (!string.IsNullOrEmpty(PS.PackingNo))
                    query = query.Where(x => x.PackingNo.Contains(PS.PackingNo));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerID == PS.BuyerId);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.PackingDate >= (PS.DateFrom) && x.PackingDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.PackingDate == (PS.DateFrom));
                  var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #region CompanyBeneficiary

        [Route("GetNewBeneficiaryBankID")]
        public IActionResult GetNewBeneficiaryBankID()
        {
            try
            {
                long BeneficiaryBankID = 0;
                var lastid = _context.CompanyBeneficiaryBankDetails.OrderBy(x => x.BeneficiaryBankID).LastOrDefault();
                BeneficiaryBankID = (lastid == null ? 0 : lastid.BeneficiaryBankID) + 1;
                return Ok(BeneficiaryBankID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostCompanyBeneficiaryBankDetails")]
        public IActionResult PostCompanyBeneficiaryBankDetails([FromBody]CompanyBeneficiaryBankDetails value)
        {
            try
            {
                _context.CompanyBeneficiaryBankDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpPut]
        [Route("PutCompanyBeneficiaryBankDetails")]
        public IActionResult PutCompanyBeneficiaryBankDetails([FromBody]CompanyBeneficiaryBankDetails value)
        {
            try
            {
                var existingdata = _context.CompanyBeneficiaryBankDetails.Where(x => x.BeneficiaryBankID == value.BeneficiaryBankID).FirstOrDefault<CompanyBeneficiaryBankDetails>();
                if (existingdata == null)
                {
                    return Ok("Not Found");
                }

                existingdata.AccountNumber = value.AccountNumber;
                existingdata.BankAddress = value.BankAddress;
                existingdata.BankID = value.BankID;
                existingdata.CompanyID = value.CompanyID;
                existingdata.SwiftID = value.SwiftID;
                existingdata.AccountNumber = value.AccountNumber;
                existingdata.Remark = value.Remark;
                existingdata.UserID = value.UserID;
                _context.CompanyBeneficiaryBankDetails.Update(existingdata);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("DeleteCompanyBeneficiary/{id}")]
        public IActionResult DeleteCompanyBeneficiary(long id)
        {
            try
            {
                var existingdata = _context.CompanyBeneficiaryBankDetails.Where(x => x.BeneficiaryBankID == id)
                                                .FirstOrDefault();

                if (existingdata != null)
                {
                    _context.CompanyBeneficiaryBankDetails.Remove(existingdata);
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

        [Route("GetCompanyBeneficiaryBankDetails/{id}")]
        public IActionResult GetCompanyBeneficiaryBankDetails(int id)
        {
            try
            {
                return Ok(_context.CompanyBeneficiaryBankDetails.Where(x => x.BeneficiaryBankID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCompanyBeneficiaryList")]
        public IActionResult GetCompanyBeneficiaryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewCompanyBeneficiaryBankDetails.OrderByDescending(x => x.BeneficiaryBankID));
                else
                    return Ok(_context.ViewCompanyBeneficiaryBankDetails
                        .Where(x => x.BankName.Contains(search) || x.AccountNumber.Contains(search) || x.Remark.Contains(search))
                        .OrderByDescending(x => x.BeneficiaryBankID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CompanyBeneficiaryBankDetails/{id}")]
        public IActionResult CompanyBeneficiaryBankDetails(int id)
        {
            try
            {
                return Ok(_context.CompanyBeneficiaryBankDetails.Where(x => x.BankID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #endregion


        #region CompanyIntermediary

        [Route("GetNewIntermediaryBankID")]
        public IActionResult GetNewIntermediaryBankID()
        {
            try
            {
                long IntermediaryBankID = 0;
                var lastid = _context.CompanyIntermediaryBankDetails.OrderBy(x => x.IntermediaryBankID).LastOrDefault();
                IntermediaryBankID = (lastid == null ? 0 : lastid.IntermediaryBankID) + 1;
                return Ok(IntermediaryBankID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostCompanyIntermediaryBankDetails")]
        public IActionResult PostCompanyIntermediaryBankDetails([FromBody]CompanyIntermediaryBankDetails value)
        {
            try
            {
                _context.CompanyIntermediaryBankDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [HttpPut]
        [Route("PutCompanyIntermediaryBankDetails")]
        public IActionResult PutCompanyIntermediaryBankDetails([FromBody]CompanyIntermediaryBankDetails value)
        {
            try
            {
                var existingdata = _context.CompanyIntermediaryBankDetails.Where(x => x.IntermediaryBankID == value.IntermediaryBankID).FirstOrDefault<CompanyIntermediaryBankDetails>();
                if (existingdata == null)
                {
                    return Ok("Not Found");
                }

                existingdata.RoutingNumber = value.RoutingNumber;
                existingdata.BankAddress = value.BankAddress;
                existingdata.BankID = value.BankID;
                existingdata.CompanyID = value.CompanyID;
                existingdata.SwiftID = value.SwiftID;
                existingdata.Remark = value.Remark;
                existingdata.UserID = value.UserID;
                _context.CompanyIntermediaryBankDetails.Update(existingdata);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("DeleteCompanyIntermediary/{id}")]
        public IActionResult DeleteCompanyIntermediary(long id)
        {
            try
            {
                var existingdata = _context.CompanyIntermediaryBankDetails.Where(x => x.IntermediaryBankID == id)
                                                .FirstOrDefault();

                if (existingdata != null)
                {
                    _context.CompanyIntermediaryBankDetails.Remove(existingdata);
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

        [Route("GetCompanyIntermediaryBankDetails/{id}")]
        public IActionResult CompanyIntermediaryBankDetails(int id)
        {
            try
            {
                return Ok(_context.CompanyIntermediaryBankDetails.Where(x => x.IntermediaryBankID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCompanyIntermediaryList")]
        public IActionResult GetCompanyIntermediaryList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewCompanyIntermediaryBankDetails.OrderByDescending(x => x.IntermediaryBankID));
                else
                    return Ok(_context.ViewCompanyIntermediaryBankDetails
                        .Where(x => x.BankName.Contains(search) || x.RoutingNumber.Contains(search) || x.Remark.Contains(search) || x.SwiftID.Contains(search) || x.BankAddress.Contains(search))
                        .OrderByDescending(x => x.IntermediaryBankID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #endregion

        #region BuyerCartonStickerTemplate

        [HttpPost]
        [Route("PostBuyerCartonStickerTemplate")]
        public IActionResult PostBuyerCartonStickerTemplate([FromBody]BuyerCartonStickerTemplate value)
        {
            try
            {
                var existingdata = _context.BuyerCartonStickerTemplate.Where(x => x.BuyerId == value.BuyerId)
                                               .FirstOrDefault();
                if (existingdata != null)
                {
                    _context.BuyerCartonStickerTemplate.Remove(existingdata);
                    _context.SaveChanges();

                }

                var MaxBuyerCartonStickerTemplateID = _context.BuyerCartonStickerTemplate.Max(x => x.BuyerCartonStickerTemplateID);
                MaxBuyerCartonStickerTemplateID++;
                value.BuyerCartonStickerTemplateID = MaxBuyerCartonStickerTemplateID;

                _context.BuyerCartonStickerTemplate.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerCartonSticker/{id}")]
        public IActionResult GetBuyerCartonSticker(int id)
        {
            try
            {
                return Ok(_context.BuyerCartonStickerTemplate.Where(x => x.BuyerId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCompanyIntermediaryBankDetails/{bankid}")]
        public IActionResult GetCompanyIntermediaryBankDetails(int bankid)
        {
            try
            {
                return Ok(_context.CompanyIntermediaryBankDetails.Where(x => x.BankID == bankid).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #endregion
    }
}
