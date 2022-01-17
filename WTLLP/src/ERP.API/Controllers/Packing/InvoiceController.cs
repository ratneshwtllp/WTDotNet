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
    public class InvoiceController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public InvoiceController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<InvoiceMaster> Get()
        {
            return _context.InvoiceMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.ViewInvoice.Where(x => x.InvoiceID == id).FirstOrDefault()); 
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
                    return Ok(_context.ViewInvoice
                        .OrderByDescending(x => x.InvoiceNo));
                else
                    return Ok(_context.ViewInvoice
                        .Where(x => x.InvoiceNo.Contains(search) || x.PackingNo.Contains(search) || x.BuyerName.Contains(search) || x.FOBCIF.Contains(search))   //|| x.Code.Contains(search) || x.Name.Contains(search)
                        .OrderByDescending(x => x.InvoiceNo));
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
                var child = _context.ViewInvoiceChild.Where(x => x.InvoiceID == id)
                                .OrderBy(x => x.SNo);
                return Ok(child);
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
                long INVID = 0;
                var lastInovice = _context.InvoiceMaster.OrderBy(x => x.InvoiceID).LastOrDefault();
                INVID = (lastInovice == null ? 0 : lastInovice.InvoiceID) + 1;
                return Ok(INVID);
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
                long InvChildID = 0;
                var lastInvoiceChild = _context.InvoiceChild.OrderBy(x => x.InvoiceChildID).LastOrDefault();
                InvChildID = (lastInvoiceChild == null ? 0 : lastInvoiceChild.InvoiceChildID) + 1;
                return Ok(InvChildID);
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
        //        long InvoiceSerial = 0;
        //        var lastInvoice = _context.InvoiceMaster.OrderBy(x => x.InvoiceSNo).LastOrDefault();
        //        InvoiceSerial = (lastInvoice == null ? 0 : lastInvoice.InvoiceSNo) + 1;
        //        return Ok(InvoiceSerial);
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
                var FirstRecord = _context.InvoiceMaster.OrderBy(x => x.InvoiceID).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Invoice").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.InvoiceMaster.Where(x => x.SessionYear == session).OrderBy(x => x.InvoiceID).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.InvoiceID) + 1;
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
                var FirstRecord = _context.InvoiceMaster.OrderBy(x => x.PackingID).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Invoice").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.InvoiceMaster.Where(x => x.SessionYear == session).OrderBy(x => x.InvoiceSNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.InvoiceSNo) + 1;
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
        //        long InvoiceSerial = 0;
        //        var lastInvoice = _context.InvoiceMaster.OrderBy(x => x.InvoiceSNo).LastOrDefault();
        //        InvoiceSerial = (lastInvoice == null ? 0 : lastInvoice.InvoiceSNo) + 1;

        //        String InvoiceNO = "SB/INV/17-18/" + string.Format("{0:0000}", InvoiceSerial) + "";
        //        return Ok(InvoiceNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        public IActionResult Post([FromBody]InvoiceMaster value)
        {
            try
            {
                _context.InvoiceMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]InvoiceMaster value)
        {
            try
            {
                var existingInvoice = _context.InvoiceMaster
                        .Where(x => x.InvoiceID == value.InvoiceID)
                        .Include(x => x.InvoiceChild)
                        .SingleOrDefault();

                if (existingInvoice != null)
                {
                    // Update parent
                    _context.Entry(existingInvoice).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var ExistingChild in existingInvoice.InvoiceChild.ToList())
                    {
                        _context.InvoiceChild.Remove(ExistingChild);
                    }

                    // Update and Insert children
                    foreach (var child in value.InvoiceChild)
                    {
                        // Insert child 1
                        InvoiceChild newChild = new InvoiceChild();
                        newChild.InvoiceChildID = child.InvoiceChildID;
                        newChild.InvoiceID = child.InvoiceID;
                        newChild.FitemID = child.FitemID;
                        newChild.Qty = child.Qty;
                        newChild.Price = child.Price;
                        newChild.Amount = child.Amount;
                        newChild.SNo = child.SNo;
                        existingInvoice.InvoiceChild.Add(newChild);
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

        [Route("Cancel/{id}")]
        public IActionResult Cancel(long id)
        {
            try
            {
                var existingInvoice = _context.InvoiceMaster.Find(id);
                //existingInvoice.CancelStatus = 1;
                _context.InvoiceMaster.Update(existingInvoice);

                _context.SaveChanges();
                var result = "Record Canceled";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var existingInvoice = _context.InvoiceMaster.Where(x => x.InvoiceID == id)
                                                .Include(x => x.InvoiceChild)
                                                .FirstOrDefault();

                if (existingInvoice != null)
                {
                    _context.InvoiceMaster.Remove(existingInvoice);
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

        [Route("GetInvoicePrint/{id}")]
        public IActionResult GetInvoicePrint(long id)
        {
            var recordlist = _context.ViewInvoicePrint.Where(x => x.InvoiceID == id)
                                        .OrderBy(x => x.InvoiceID)
                                        .ThenBy(x => x.Code)
                                        .ToList();
            return Ok(recordlist);
        }

        [Route("GetCurrencyList")]
        public IActionResult GetCurrencyList()
        {
            try
            {
                object Currency;
                Currency = _context.CurrencyDetails
                                        .OrderBy(c => c.Cid)
                                        .Select(x => new { Id = x.Cid, Value = x.Csname }).ToList();
                return Ok(Currency);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetFinalDestinationList")]
        public IActionResult GetFinalDestinationList()
        {
            try
            {
                object FinalDestination;
                FinalDestination = _context.InvoiceMaster
                                        .Select(x => new { Value = x.FinalDestination }).ToList();
                return Ok(FinalDestination);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ViewInvoice")]
        public IActionResult ViewInvoice([FromBody]InvoiceSearch IS)
        {
            try
            {
                IQueryable<ViewInvoice> query = _context.ViewInvoice;
                if (IS.ChkDateFromInt == 1 && IS.ChkDateToInt == 1)
                    query = query.Where(x => x.InvoiceDate >= (IS.DateFrom) && x.InvoiceDate <= IS.DateTo);
                if (IS.ChkDateFromInt == 1 && IS.ChkDateToInt == 0)
                    query = query.Where(x => x.InvoiceDate == (IS.DateFrom));
                if (IS.BuyerID > 0)
                    query = query.Where(x => x.BuyerID == IS.BuyerID);
                if (!string.IsNullOrEmpty(IS.InvoiceNo))
                    query = query.Where(x => x.InvoiceNo.Contains(IS.InvoiceNo));
                if (IS.MonthId > 0)
                    query = query.Where(x => x.InvoiceDate.Month == IS.MonthId);
                if (IS.YearName > 0)
                    query = query.Where(x => x.InvoiceDate.Year == IS.YearName);
                if (IS.CID > 0)
                    query = query.Where(x => x.CID == IS.CID);
                if (IS.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == IS.Session_Year);
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
