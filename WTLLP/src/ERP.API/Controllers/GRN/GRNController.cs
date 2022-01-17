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
    public class GRNController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public GRNController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ViewGRNMaster> Get()
        {
            return _context.ViewGRNMaster.Where(x => x.CancelStatus == 0)       //list of Not canceled grn
                .OrderByDescending(x => x.GRNID).AsQueryable();
        }

        [Route("GetViewGRNMaster/{grnid}")]
        public IActionResult GetViewGRNMaster(long grnid)
        {
            return Ok(_context.ViewGRNMaster.Where(x => x.CancelStatus == 0 && x.GRNID == grnid)
                .OrderByDescending(x => x.GRNID).FirstOrDefault());
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.GRNMaster.Where(x => x.GRNID == id).FirstOrDefault());
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
                long GrnID = 0;
                var lastgrnId = _context.GRNMaster.OrderBy(x => x.GRNID).LastOrDefault();
                GrnID = (lastgrnId == null ? 0 : lastgrnId.GRNID);

                if (string.IsNullOrEmpty(search))
                {
                    if (GrnID > 20)
                    {
                        return Ok(_context.ViewGRN.Where(x => x.CancelStatus == 0 && x.GRNID > GrnID - 20)
                       .OrderByDescending(x => x.GRNID)
                       .ThenBy(x => x.SNo));
                    }
                    else
                    {
                        return Ok(_context.ViewGRN.Where(x => x.CancelStatus == 0)
                        .OrderByDescending(x => x.GRNID)
                        .ThenBy(x => x.SNo));
                    }
                }
                else
                {
                    var grnid_list = _context.ViewGRN
                                     .Where(x => (x.CancelStatus == 0) && (x.GRNNO.Contains(search) || x.PONO.Contains(search) || x.SupplierName.Contains(search) || x.BillNo.Contains(search) || x.ChallanNo.Contains(search)))
                                    .Select(x => x.GRNID).ToArray();

                    var GRN = _context.ViewGRN
                                    .Where(x => grnid_list.Contains(x.GRNID))
                                    .OrderByDescending(x => x.GRNID)
                                    .ThenBy(x => x.SNo);
                    return Ok(GRN);
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
                long GrnID = 0;
                var lastgrnId = _context.GRNMaster.OrderBy(x => x.GRNID).LastOrDefault();
                GrnID = (lastgrnId == null ? 0 : lastgrnId.GRNID);

                if (string.IsNullOrEmpty(search))
                {
                    if (GrnID > 20)
                    {
                        return Ok(_context.ViewGRN.Where(x => x.CancelStatus == 1 && x.GRNID > GrnID - 20)
                        .OrderByDescending(x => x.GRNID)
                        .ThenBy(x => x.SNo));
                    }
                    else
                    {
                        return Ok(_context.ViewGRN.Where(x => x.CancelStatus == 1)       //list of canceled grn
                                .OrderByDescending(x => x.GRNID)
                                .ThenBy(x => x.SNo));
                    }
                }
                else
                {
                    var grnid_list = _context.ViewGRN
                                    .Where(x => (x.CancelStatus == 1) && (x.GRNNO.Contains(search) || x.PONO.Contains(search) || x.SupplierName.Contains(search) || x.BillNo.Contains(search) || x.ChallanNo.Contains(search)))
                                   .Select(x => x.GRNID).ToArray();

                    var GRN = _context.ViewGRN
                                    .Where(x => grnid_list.Contains(x.GRNID))
                                    .OrderByDescending(x => x.GRNID)
                                    .ThenBy(x => x.SNo);
                    return Ok(GRN);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGRNID")]
        public IActionResult GetNewGRNID()
        {
            try
            {
                long GrnID = 0;
                var lastgrnId = _context.GRNMaster.OrderBy(x => x.GRNID).LastOrDefault();
                GrnID = (lastgrnId == null ? 0 : lastgrnId.GRNID) + 1;
                return Ok(GrnID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGRNChildID")]
        public IActionResult GetNewGRNChildID()
        {
            try
            {
                long grnChildId = 0;
                var lastGrnChild = _context.GRNChild.OrderBy(x => x.GRNChildID).LastOrDefault();
                grnChildId = (lastGrnChild == null ? 0 : lastGrnChild.GRNChildID) + 1;
                return Ok(grnChildId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGRNSerial")]
        public IActionResult GetNewGRNSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.GRNMaster.OrderBy(x => x.GRNID).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "GRN").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.GRNMaster.Where(x => x.SessionYear == session).OrderBy(x => x.GRNSerial).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.GRNSerial) + 1;
                }
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGRNNO")]
        public IActionResult GetNewGRNNO()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.GRNMaster.OrderBy(x => x.GRNID).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "GRN").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.GRNMaster.Where(x => x.SessionYear == session).OrderBy(x => x.GRNSerial).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.GRNSerial) + 1;
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

        [Route("GetPONoList/{suppid}")]
        public IActionResult GetPONoList(long suppid)
        {
            try
            {
                object PoNoList;
                PoNoList = _context.ViewPOForGRN.Where(x => x.SID == suppid && x.Balance > 0)
                                         .OrderBy(x => x.POID)
                                         .Select(x => new { Id = x.POID, Value = x.PONO }).Distinct().ToList();
                return Ok(PoNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        //[Route("GetPONoList/{suppid}")]
        //public IActionResult GetPONoList(long suppid)
        //{
        //    try
        //    {
        //        object PoNoList;
        //        PoNoList = _context.ViewPOMaster.Where(x => x.Sid == suppid && x.CancelStatus == 0)
        //                                 .OrderBy(x => x.Poid)
        //                                 .Select(x => new { Id = x.Poid, Value = x.Pono }).ToList();
        //        return Ok(PoNoList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetPoChildRecord/{poid}")]
        public IActionResult GetPoChildRecord(long poid)
        {
            try
            {   // earlier ViewPOChild
                var pochildrecord = _context.ViewPOForGRN.Where(x => x.POID == poid).OrderBy(x => x.POChildID).ToList();
                return Ok(pochildrecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMultiPOChildRecord")]
        public IActionResult GetMultiPOChildRecord(long[] poids)
        {
            try
            {
                var pochildrecord = _context.ViewPOForGRN.Where(x => poids.Contains(x.POID)).OrderBy(x => x.POChildID).ToList();
                return Ok(pochildrecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]GRNMaster value)
        {
            try
            {
                long id = _context.GRNJWRMComsumption.Max(x=> x.GRNJWRMComsumptionId);
                id++;
                for (int i= 0; i< value.GRNJWRMComsumption.Count; i++)
                {
                    value.GRNJWRMComsumption.ElementAt(i).GRNJWRMComsumptionId = id;
                    id++;
                }

                _context.GRNMaster.Add(value);

                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]GRNMaster value)
        {
            try
            {
                var existingGRN = _context.GRNMaster
                        .Where(x => x.GRNID == value.GRNID)
                        .Include(x => x.GRNChild)
                        .SingleOrDefault();

                if (existingGRN != null)
                {
                    // Update parent
                    _context.Entry(existingGRN).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var existingGRNChild in existingGRN.GRNChild.ToList())
                    {
                        _context.GRNChild.Remove(existingGRNChild);
                    }

                    // Update and Insert children
                    foreach (var childGrn in value.GRNChild)
                    {
                        // Insert child 1
                        //GRNChild newChild = new GRNChild();
                        //newChild.GRNChildID = childGrn.GRNChildID;
                        //newChild.GRNID = childGrn.GRNID;
                        //newChild.POChildID = childGrn.POChildID;
                        //newChild.GRNQty = childGrn.GRNQty;
                        //newChild.LotNo = childGrn.LotNo;
                        //newChild.SNo = childGrn.SNo;
                        existingGRN.GRNChild.Add(childGrn);
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

        [Route("PutGRNStatus/{grnid}")]
        public IActionResult PutGRNStatus(long grnid)
        {
            try
            {
                var existinggrn = _context.GRNMaster.Find(grnid);
                existinggrn.CancelStatus = 1;
                _context.GRNMaster.Update(existinggrn);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("PutStoreKeepingStatus/{grnid}")]
        //public IActionResult PutStoreKeepingStatus(long grnid)
        //{
        //    try
        //    {
        //        var existinggrn = _context.GRNMaster.Find(grnid);
        //        existinggrn.Verify = 1;
        //        _context.GRNMaster.Update(existinggrn);

        //        _context.SaveChanges();
        //        var result = "Record Save";
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewGRNFileId")]
        public IActionResult GetNewGRNFileId()
        {
            try
            {
                long GRNFileId = 0;
                var lastgrnFile = _context.GRNFile.OrderBy(x => x.GRNFileId).LastOrDefault();
                GRNFileId = (lastgrnFile == null ? 0 : lastgrnFile.GRNFileId) + 1;
                return Ok(GRNFileId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("PostGRNFile")]
        //public IActionResult PostGRNFile([FromBody]List<GRNFile> value)
        //{
        //    try
        //    {
        //        for (int i = 0; i < value.Count; i++)
        //        {
        //            _context.GRNFile.Add(value[i]);
        //        }
        //        _context.SaveChanges();
        //        return Ok("Record Save");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetGRNList")]
        public IActionResult GetGRNList([FromBody]GRNSearch GS)
        {
            try
            {
                IQueryable<ViewGRN> query = _context.ViewGRN;
                if (GS.SupplierID > 0)
                    query = query.Where(x => x.SupplierID == GS.SupplierID);
                if (!string.IsNullOrEmpty(GS.GRNNO))
                    query = query.Where(x => x.GRNNO.Contains(GS.GRNNO));
                if (GS.MonthId > 0)
                    query = query.Where(x => x.GRNDate.Month == GS.MonthId);
                if (GS.YearName > 0)
                    query = query.Where(x => x.GRNDate.Year == GS.YearName);
                if (GS.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == GS.Session_Year);
                if (!string.IsNullOrEmpty(GS.ChallanNo))
                    query = query.Where(x => x.ChallanNo.Contains(GS.ChallanNo));
                if (!string.IsNullOrEmpty(GS.BillNo))
                    query = query.Where(x => x.BillNo.Contains(GS.BillNo));
                if (!string.IsNullOrEmpty(GS.PONO))
                    query = query.Where(x => x.PONO.Contains(GS.PONO));
                if (!string.IsNullOrEmpty(GS.RMName))
                    query = query.Where(x => x.Name.Contains(GS.RMName));

                if (GS.ChkGRNDateFromInt == 1 && GS.ChkGRNDateToInt == 1)
                    query = query.Where(x => x.GRNDate >= (GS.GRNDateFrom) && x.GRNDate <= GS.GRNDateTo);
                if (GS.ChkGRNDateFromInt == 1 && GS.ChkGRNDateToInt == 0)
                    query = query.Where(x => x.GRNDate == (GS.GRNDateFrom));

                if (GS.PO_JW > 0)
                    query = query.Where(x => x.PO_JobWork == GS.PO_JW);

                query = query.OrderByDescending(x => x.GRNID).ThenBy(x => x.SNo);

                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckBillChallan")]
        public IActionResult CheckBillChallan([FromBody]GRNSearch GS)
        {
            try
            {
                IQueryable<GRNMaster> query = _context.GRNMaster.Where(x => x.SupplierID == GS.SupplierID && x.SessionYear == GS.Session_Year);
                if (GS.GRNId > 0 )
                    query = query.Where(x => x.GRNID != GS.GRNId);
                if (GS.BillNo != "-")
                    query = query.Where(x => x.BillNo == GS.BillNo);
                if (GS.ChallanNo != "-")
                    query = query.Where(x => x.ChallanNo == GS.ChallanNo);

                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetGRNForPrint/{id}")]
        public IQueryable<ViewGRN> GetGRNForPrint(long id)
        {
            return _context.ViewGRN.Where(x => x.GRNID == id).AsQueryable();
        }

        [Route("GetGRNFileRecord/{id}")]
        public IActionResult GetGRNFileRecord(long id)
        {
            try
            {
                var grnfile = _context.ViewGRNFile
                                        .Where(x => x.GRNId == id)
                                        .OrderBy(x => x.GRNFileId);
                return Ok(grnfile);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostGRNFile")]
        public IActionResult PostGRNFile([FromBody]GRNFile value)
        {
            try
            {
                _context.GRNFile.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteGRNFile/{id}")]
        public IActionResult DeleteGRNFile(long id)
        {
            var existing = _context.GRNFile.Find(id);
            if (existing != null)
            {
                _context.GRNFile.Remove(existing);
                _context.SaveChanges();
                return Ok("1");
            }
            else
            {
                return Ok("0");
            }

        }

        [Route("GRNFileLocation/{id}")]
        public IActionResult GRNFileLocation(long id)
        {
            try
            {
                var filename = _context.GRNFile.Where(x => x.GRNFileId == id).Select(x => x.FileLocation).FirstOrDefault(); ;
                return Ok(filename);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMultiJWChildRecord")]
        public IActionResult GetMultiJWChildRecord(long[] issuermchangeid)
        {
            try
            {
                var pochildrecord = _context.ViewJWForGRN.Where(x => issuermchangeid.Contains(x.IssueRMChangeID)).OrderBy(x => x.IssueRMChangeItemID).ToList();
                return Ok(pochildrecord);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMultiJWIssueRMList")]
        public IActionResult GetMultiJWIssueRMList(long[] issuermchangeid)
        {
            var record = _context.ViewIssueRmForChange.Where(x => issuermchangeid.Contains(x.IssueRMChangeID)).OrderBy(x => x.IssueRMChangeID).ToList();
            return Ok(record);
        }

        [HttpPost]
        [Route("GetPONoListAgainstDate")]
        public IActionResult GetPONoListAgainstDate([FromBody]GRNSearch search)
        {
            try
            {
                object PoNoList;
                PoNoList = _context.ViewPOForGRN.Where(x => x.SID == search.SupplierID && x.PODate <= search.GRNDateTo)
                                         .OrderBy(x => x.POID)
                                         .Select(x => new { Id = x.POID, Value = x.PONO }).Distinct().ToList();
                return Ok(PoNoList);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetJWNoListAgainstDate")]
        public IActionResult GetJWNoListAgainstDate([FromBody]GRNSearch search)
        {
            try
            {
                object PoNoList;
                PoNoList = _context.IssueRMforChangeMaster.Where(x => x.SupplierId == search.SupplierID && x.IssueRMChangeDate <= search.GRNDateTo)
                                         .OrderBy(x => x.IssueRMChangeID)
                                         .Select(x => new { Id = x.IssueRMChangeID, Value = x.IssueRMChangeNo }).ToList();
                return Ok(PoNoList);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetGRNNo_Session")]
        public IActionResult GetGRNNo_Session([FromBody]GRNMaster _oGRNMaster)
        {
            try
            {
                string session = API.Helper.Create_Session_FromDate(Convert.ToDateTime(_oGRNMaster.GRNDate));
                GRNMaster _GRNMaster = new GRNMaster();
                DateTime LastDateOfGRN_AgainstSession = _context.GRNMaster.Where(x => x.SessionYear == session).Max(x => x.GRNDate);
                
                long GRNSerial = 0;
                var FirstRecord = _context.GRNMaster.OrderBy(x => x.GRNID).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "GRN").FirstOrDefault();

                if (FirstRecord == null)
                {
                    GRNSerial = FormSettings.StartingNumber;
                }
                else
                {
                    GRNSerial = _context.GRNMaster.Where(x => x.SessionYear == session).Max(x => x.GRNSerial);
                    GRNSerial++;
                }
                String GRNNO = FormSettings.Prefix + "/"+ session + "/";
                switch (FormSettings.NoOfDigits)
                {
                    case 3:
                        GRNNO += string.Format("{0:000}", GRNSerial);
                        break;
                    case 4:
                        GRNNO += string.Format("{0:0000}", GRNSerial);
                        break;
                    case 5:
                        GRNNO += string.Format("{0:00000}", GRNSerial);
                        break;
                    default:
                        break;
                }

                long GRNID = _context.GRNMaster.Max(x => x.GRNID);
                GRNID++;

                _GRNMaster.GRNNO = GRNNO;
                _GRNMaster.GRNSerial = GRNSerial;
                _GRNMaster.SessionYear = session;
                _GRNMaster.GRNID = GRNID;
                _GRNMaster.EntryDate = LastDateOfGRN_AgainstSession;

                return Ok(_GRNMaster);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
