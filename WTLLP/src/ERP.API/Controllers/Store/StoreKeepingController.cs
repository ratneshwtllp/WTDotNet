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
    public class StoreKeepingController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        string sort_session = "";
        string session = "";
        public StoreKeepingController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            sort_session = API.Helper.Create_Sort_Session();
            session = API.Helper.Create_Session();
        }

        [HttpGet]
        public IQueryable<StoreKeepingMaster> Get()
        {
            return _context.StoreKeepingMaster
                .OrderByDescending(x => x.GRNID).AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.StoreKeepingMaster.Where(x => x.GRNID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStoreKeepingID")]
        public IActionResult GetNewStoreKeepingID()
        {
            try
            {
                long StoreKeepingID = _context.StoreKeepingMaster.Max(x => x.StoreKeepingID);
                StoreKeepingID++;
                return Ok(StoreKeepingID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStoreKeepingChildID")]
        public IActionResult GetNewStoreKeepingChildID()
        {
            try
            {
                long StoreKeepingChildID = _context.StoreKeepingChild.Max(x => x.StoreKeepingChildID);
                StoreKeepingChildID++;
                return Ok(StoreKeepingChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStoreKeepingSerial")]
        public IActionResult GetNewStoreKeepingSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long SKSerial = 0;
                var lastsk = _context.StoreKeepingMaster.OrderBy(x => x.StoreKeepingSerial).Where(x => x.SessionYear == session).LastOrDefault();
                SKSerial = (lastsk == null ? 0 : lastsk.StoreKeepingSerial) + 1;
                return Ok(SKSerial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStoreKeepingNO")]
        public IActionResult GetNewStoreKeepingNO()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long SKSerial = 0;
                var lastsk = _context.StoreKeepingMaster.OrderBy(x => x.StoreKeepingSerial).Where(x => x.SessionYear == session).LastOrDefault();
                SKSerial = (lastsk == null ? 0 : lastsk.StoreKeepingSerial) + 1;

                String SKNO = "SB/SK/" + session + "/" + string.Format("{0:0000}", SKSerial) + "";
                return Ok(SKNO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetGRNList")]
        public IActionResult GetGRNList()
        {
            try
            {
                object GrnNoList;
                GrnNoList = _context.GRNMaster.OrderBy(x => x.GRNID)
                            .Where(x => x.Verify == 0 && x.CancelStatus == 0)
                            .Select(x => new { Id = x.GRNID, Value = x.GRNNO }).ToList();
                return Ok(GrnNoList);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]StoreKeepingMaster value)
        {
            try
            {
                _context.StoreKeepingMaster.Add(value);
                _context.SaveChanges();

                _context.Database.ExecuteSqlCommand("SP_UpdateGRNVerify");

                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]StoreKeepingMaster value)
        {
            try
            {
                var existingSK = _context.StoreKeepingMaster
                        .Where(x => x.StoreKeepingID == value.StoreKeepingID)
                        .Include(x => x.StoreKeepingChild)
                        .SingleOrDefault();

                if (existingSK != null)
                {
                    // Update parent
                    _context.Entry(existingSK).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var existingSKChild in existingSK.StoreKeepingChild.ToList())
                    {
                        _context.StoreKeepingChild.Remove(existingSKChild);
                    }

                    // Update and Insert children
                    foreach (var childsk in value.StoreKeepingChild)
                    {
                        // Insert child 1
                        StoreKeepingChild newChild = new StoreKeepingChild();
                        newChild.StoreKeepingChildID = childsk.StoreKeepingChildID;
                        newChild.StoreKeepingID = childsk.StoreKeepingID;
                        newChild.GRNChildID = childsk.GRNChildID;
                        newChild.RitemID = childsk.RitemID;
                        newChild.RackID = childsk.RackID;
                        newChild.StoreKeepingQty = childsk.StoreKeepingQty;
                        newChild.Side = childsk.Side;
                        existingSK.StoreKeepingChild.Add(newChild);
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

        [HttpPost]
        [Route("GetStoreKeepingList")]
        public IActionResult GetStoreKeepingList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewStoreKeeping> query = _context.ViewStoreKeeping;
                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierID == obj.SupplierID);
                if (!string.IsNullOrEmpty(obj.GRNNO))
                    query = query.Where(x => x.GRNNO.Contains(obj.GRNNO));
                if (!string.IsNullOrEmpty(obj.PONO))
                    query = query.Where(x => x.PONO.Contains(obj.PONO));
                if (!string.IsNullOrEmpty(obj.StoreKeepingNO))
                    query = query.Where(x => x.StoreKeepingNO.Contains(obj.StoreKeepingNO));
                if (!string.IsNullOrEmpty(obj.RMName))
                    query = query.Where(x => x.Name.Contains(obj.RMName) || x.Code.Contains(obj.RMName));
                if (!string.IsNullOrEmpty(obj.RackNo))
                    query = query.Where(x => x.RackNo.Contains(obj.RackNo));

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.StoreKeepingDate >= (obj.DateFrom) && x.StoreKeepingDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.StoreKeepingDate == (obj.DateFrom));
                if (obj.MonthId > 0)
                    query = query.Where(x => x.GRNDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.GRNDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);
                var record = query.OrderByDescending(x => x.StoreKeepingID).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewGRNForStoreKeeping/{grnid}")]
        public IActionResult GetViewGRNForStoreKeeping(long grnid)
        {
            try
            {
                object grndata;
                grndata = _context.ViewGRNForStoreKeeping.Where(x => x.GRNID == grnid && x.BalQty > 0).OrderBy(x => x.Code).ToList();
                return Ok(grndata);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        private long NewRTSGRNSNo()
        {
            long Serial = 0;

            var FirstRecord = _context.RTSGRNMaster.OrderBy(x => x.RTSGRNID).FirstOrDefault();
            var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "RTSG").FirstOrDefault();

            if (FirstRecord == null)
            {
                Serial = FormSettings.StartingNumber;
            }
            else
            {
                Serial = _context.RTSGRNMaster.Where(x => x.SessionYear == sort_session).Max(x => x.RTSGRNSNo);
                Serial++;
            }
            return Serial;
        }

        private string NewRTSGRNNo()
        {
            long Serial = NewRTSGRNSNo();

            var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "RTSG").FirstOrDefault();
            string DisplayString = FormSettings.Prefix + "/" + sort_session + "/";
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
            return DisplayString;
        }

        [Route("GetNewRTSGRNNo")]
        public IActionResult GetNewRTSGRNNo()
        {
            try
            {
                return Ok(NewRTSGRNNo());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostRTSGRN")]
        public IActionResult PostRTSGRN([FromBody]RTSGRNMaster value)
        {
            try
            {
                long RTSGRNID = _context.RTSGRNMaster.Max(x => x.RTSGRNID);
                RTSGRNID++;

                value.RTSGRNID = RTSGRNID;
                value.RTSGRNSNo = NewRTSGRNSNo();
                value.RTSGRNNo = NewRTSGRNNo();

                long RTSGRNChildID = _context.RTSGRNChild.Max(x => x.RTSGRNChildID);
                foreach (var v in value.RTSGRNChild)
                {
                    RTSGRNChildID++;
                    v.RTSGRNChildID = RTSGRNChildID;
                    v.RTSGRNID = RTSGRNID;
                }

                _context.RTSGRNMaster.Add(value);
                _context.SaveChanges();

                _context.Database.ExecuteSqlCommand("SP_UpdateGRNVerify");

                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetRTSGRNList")]
        public IActionResult GetRTSGRNList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewRTSGRN> query = _context.ViewRTSGRN;
                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);
                if (!string.IsNullOrEmpty(obj.RTSGRNNo))
                    query = query.Where(x => x.RTSGRNNo.Contains(obj.RTSGRNNo));
                if (!string.IsNullOrEmpty(obj.GRNNO))
                    query = query.Where(x => x.GRNNO.Contains(obj.GRNNO));
                if (!string.IsNullOrEmpty(obj.PONO))
                    query = query.Where(x => x.PONO.Contains(obj.PONO));

                if (!string.IsNullOrEmpty(obj.RMName))
                    query = query.Where(x => x.Full_Name.Contains(obj.RMName));

                if (!string.IsNullOrEmpty(obj.BillNo))
                    query = query.Where(x => x.BillNo.Contains(obj.BillNo));
                if (!string.IsNullOrEmpty(obj.ChallanNo))
                    query = query.Where(x => x.ChallanNo.Contains(obj.ChallanNo));

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.RTSGRNDate >= (obj.DateFrom) && x.RTSGRNDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.RTSGRNDate == (obj.DateFrom));
                if (obj.MonthId > 0)
                    query = query.Where(x => x.RTSGRNDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.RTSGRNDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);
                var record = query.OrderByDescending(x => x.RTSGRNID).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
