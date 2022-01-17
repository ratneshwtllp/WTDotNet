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
    public class IssueRMForChangeController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        string session_year = "";

        public IssueRMForChangeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            session_year = API.Helper.Create_Sort_Session();
        }

        [Route("GetViewIssueRmForChange/{IssueRMChangeID}")]
        public IQueryable<ViewIssueRMForChange> GetViewIssueRmForChange(long IssueRMChangeID)
        {
            if(IssueRMChangeID >0)
                return _context.ViewIssueRmForChange.Where(x => x.IssueRMChangeID == IssueRMChangeID).AsQueryable().OrderByDescending(x => x.IssueRMChangeID);
            else
                return _context.ViewIssueRmForChange.AsQueryable().OrderByDescending(x=> x.IssueRMChangeID);
        }

        [Route("GetViewIssueRMForChangeMaster/{IssueRMChangeID}")]
        public IQueryable<ViewIssueRMForChangeMaster> ViewIssueRMForChangeMaster(long IssueRMChangeID)
        {
            return _context.ViewIssueRMForChangeMaster.Where(x => x.IssueRMChangeID == IssueRMChangeID).AsQueryable();
        }

        [Route("GetRecvJW/{IssueRMChangeID}")]
        public IQueryable<RecvJW> GetRecvJW(long IssueRMChangeID)
        {
            return _context.RecvJW.Where(x => x.IssueRMChangeItemID == IssueRMChangeID).AsQueryable();
        }

        public IQueryable<ViewIssueRMForChange> Get()
        {
            return _context.ViewIssueRmForChange.OrderByDescending(x => x.IssueRMChangeID).AsQueryable();
        }

        [Route("GetMaster")]
        public IQueryable<ViewIssueRMForChangeMaster> GetMaster()
        {
            return _context.ViewIssueRMForChangeMaster.Where(x => x.IsComplete == 0).OrderByDescending(x => x.IssueRMChangeID).AsQueryable();
        }

        [Route("GetMasterCompleted")]
        public IQueryable<ViewIssueRMForChangeMaster> GetMasterCompleted()
        {
            return _context.ViewIssueRMForChangeMaster.Where(x => x.IsComplete == 1).OrderByDescending(x => x.IssueRMChangeID).AsQueryable();
        }

        [Route("GetBalanceList")]
        public IQueryable<ViewIssueRMForChangeMaster> GetBalanceList()
        {
            //return _context.ViewIssueRMForChangeMaster.Where(x => x.Balance > 0 && x.IsComplete == 0).OrderByDescending(x => x.IssueRMChangeID).AsQueryable();
            return _context.ViewIssueRMForChangeMaster.Where(x => x.IsComplete == 0).OrderByDescending(x => x.IssueRMChangeID).AsQueryable();
        }

        [Route("GetNewIssueRMChangeID")]

        public IActionResult GetNewIssueRMChangeID()
        {
            try
            {
                long IssueRMChangeID = 0;
                var lastIssue = _context.IssueRMforChangeMaster.OrderBy(x => x.IssueRMChangeID).LastOrDefault();
                IssueRMChangeID = (lastIssue == null ? 0 : lastIssue.IssueRMChangeID) + 1;
                return Ok(IssueRMChangeID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRecvJWID")]
        public IActionResult GetNewRecvJWID()
        {
            try
            {
                long id = 0;
                var record = _context.RecvJW.OrderBy(x => x.RecvJWID).LastOrDefault();
                id = (record == null ? 0 : record.RecvJWID) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetNewIssueRMChangeChildID")]
        public IActionResult GetNewIssueRMChangeChildID()
        {
            try
            {
                long IssueRMChangeChildID = 0;
                var lastIssueRMChangeChildID = _context.IssueRMForChangeChild.OrderBy(x => x.IssueRMChangeChildID).LastOrDefault();
                IssueRMChangeChildID = (lastIssueRMChangeChildID == null ? 0 : lastIssueRMChangeChildID.IssueRMChangeChildID) + 1;
                return Ok(IssueRMChangeChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssueRMChangeItemID")]
        public IActionResult GetNewIssueRMChangeItemID()
        {
            try
            {
                long IssueRMChangeItemID = 0;
                var record = _context.IssueRmforChangeItem.OrderBy(x => x.IssueRMChangeItemID).LastOrDefault();
                IssueRMChangeItemID = (record == null ? 0 : record.IssueRMChangeItemID) + 1;
                return Ok(IssueRMChangeItemID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //For Issue RM Change
        [Route("GetIssueRMChangeSerial/{jwtype}")]
        public IActionResult GetIssueRMChangeSerial(int jwtype)
        {
            try
            {
                long SerialNo = 0;
                var lastrecord = _context.IssueRMforChangeMaster.OrderBy(x => x.IssueRMChangeSNo).Where(x => x.SessionYear == session_year && x.InHouse_Outdoor == jwtype).LastOrDefault();
                SerialNo = (lastrecord == null ? 0 : lastrecord.IssueRMChangeSNo) + 1;
                return Ok(SerialNo);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetIssueRMChangeNo/{jwtype}")]
        public IActionResult GetIssueRMChangeNo(int jwtype)
        {
            try
            {
                long SerialNo = 0;
                var lastrecord = _context.IssueRMforChangeMaster.OrderBy(x => x.IssueRMChangeSNo).Where(x => x.SessionYear == session_year && x.InHouse_Outdoor == jwtype).LastOrDefault();
                SerialNo = (lastrecord == null ? 0 : lastrecord.IssueRMChangeSNo) + 1;

                string str = "";
                if (jwtype == 1)
                { str = "INH/JW/" + session_year + "/" + string.Format("{0:000}", SerialNo) + ""; }
                else { str = "SBI/JW/" + session_year + "/" + string.Format("{0:000}", SerialNo) + ""; }
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]IssueRMforChangeMaster value)
        {
            try
            {
                _context.IssueRMforChangeMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostJW")]
        public IActionResult PostJW([FromBody]RecvJW value)
        {
            try
            {
                _context.RecvJW.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{issuechangeid}")]
        public IActionResult Delete(long issuechangeid)
        {
            try
            {
                var existingIssueRm = _context.IssueRMforChangeMaster.Where(x => x.IssueRMChangeID == issuechangeid)
                      .Include(x => x.IssueRMForChangeChild)
                      .Include(x => x.IssueRMforChangeItem)
                      .SingleOrDefault();

                if (existingIssueRm != null)
                {
                    _context.IssueRMforChangeMaster.Remove(existingIssueRm);
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

        [Route("UpdateIssueRM/{IssueRMChangeChildID}")]
        public IActionResult UpdateIssueRM(long IssueRMChangeChildID)
        {
            try
            {
                var obj = _context.IssueRMForChangeChild.Where(x => x.IssueRMChangeChildID == IssueRMChangeChildID)
                      .SingleOrDefault();

                if (obj != null)
                {
                    if (obj.IsAddIn_JWOutBal == 0)
                        obj.IsAddIn_JWOutBal = 1;
                    else
                        obj.IsAddIn_JWOutBal = 0;
                    _context.IssueRMForChangeChild.Update(obj);
                    _context.SaveChanges();
                    return Ok("Update Complete");
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

        [Route("RItemCurrentStockShow/{id}")]
        public IActionResult RItemCurrentStockShow(long id)
        {
            try
            {
                object RitemDetails;
                RitemDetails = _context.ViewRMStock.Where(x => x.RItem_Id == id).FirstOrDefault();
                return Ok(RitemDetails);
                //return Ok(_context.ViewProductShow .Where(x => x.FitemId  == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetQtyOnRack/{ritemid}/{supplierid}/{rackid}")]
        public IActionResult GetQtyOnRack(long ritemid, long supplierid, long rackid)
        {
            try
            {
                object RitemDetails = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == supplierid && x.RackId == rackid).FirstOrDefault();

                return Ok(RitemDetails);
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

        [Route("IsComplete/{planid}")]
        public IActionResult IsComplete(long planid)
        {
            try
            {
                var existing = _context.IssueRMforChangeMaster.Find(planid);
                existing.IsComplete = 1;
                _context.IssueRMforChangeMaster.Update(existing);
                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewIssueRmForChange")]
        public IActionResult ViewIssueRmForChange([FromBody]SearchDetails sd)
        {
            try
            {
                var result = _context.ViewIssueRmForChange
                                    .Where(x => sd.IssueRMChangeID.Contains(x.IssueRMChangeID))
                                    .OrderBy(x => x.IssueRMChangeID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetJWNoList/{suppid}")]
        public IActionResult GetJWNoList(long suppid)
        {
            try
            {
                object PoNoList;
                PoNoList = _context.IssueRMforChangeMaster.Where(x => x.SupplierId == suppid)
                                         .OrderBy(x => x.IssueRMChangeID)
                                         .Select(x => new { Id = x.IssueRMChangeID, Value = x.IssueRMChangeNo }).Distinct().ToList();
                return Ok(PoNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        #region ReturnToSuppler RTS
        [HttpPost]
        [Route("PostRTS")]
        public IActionResult PostRTS([FromBody]ReturnToSupplier value)
        {
            try
            {
                long Id = 0;
                var lastrecord = _context.ReturnToSupplier.OrderBy(x => x.RTSID).LastOrDefault();
                Id = (lastrecord == null ? 0 : lastrecord.RTSID) + 1;

                value.RTSID = Id;

                long SerialNo = 0;
                var session_lastrecord = _context.ReturnToSupplier.OrderBy(x => x.RTSID).Where(x => x.SessionYear == session_year).LastOrDefault();
                SerialNo = (session_lastrecord == null ? 0 : session_lastrecord.RTSSNo) + 1;
                string str = "";
                str = "SBI/RTS/" + session_year + "/" + string.Format("{0:000}", SerialNo) + "";

                value.RTSSNo = SerialNo;
                value.RTSNo = str;

                _context.ReturnToSupplier.Add(value);
                _context.SaveChanges();

                //if (value.POID > 0)
                //{
                //    var obj = _context.Pomaster.Where(x => x.Poid == value.POID && x.Pono == value.PONO).FirstOrDefault();
                //    obj.Complete = 0;
                //    _context.Pomaster.Update(obj);
                //    _context.SaveChanges();
                //}
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("ViewRTS")]
        //public IActionResult ViewRTS(string search)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(search))
        //            return Ok(_context.ViewRTS.OrderByDescending(x => x.RTSID));
        //        else
        //            return Ok(_context.ViewRTS
        //                .Where(x => x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search) || x.SupplierName.Contains(search) || x.RackNo.Contains(search) || x.RTSRemark.Contains(search))
        //                .OrderByDescending(x => x.RTSID));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        //[HttpPost]
        //[Route("ViewRTS")]
        //public IActionResult ViewRTS([FromBody]StoreSearch obj)
        //{
        //    try
        //    {
        //        IQueryable<ViewRTS> query = _context.ViewRTS;

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

        //        if (!string.IsNullOrEmpty(obj.RTSNo))
        //            query = query.Where(x => x.RTSNo.Contains(obj.RTSNo));
        //        if (!string.IsNullOrEmpty(obj.PONO))
        //            query = query.Where(x => x.PONO.Contains(obj.PONO));
        //        //if (!string.IsNullOrEmpty(obj.RMFor))
        //        //    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

        //        if (obj.SupplierID > 0)
        //            query = query.Where(x => x.SupplierId == obj.SupplierID);
        //        //if (obj.ContractorId > 0)
        //        //    query = query.Where(x => x.ContractorID == obj.ContractorId);

        //        if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
        //            query = query.Where(x => x.RTSDate >= (obj.DateFrom) && x.RTSDate <= obj.DateTo);
        //        if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
        //            query = query.Where(x => x.RTSDate == (obj.DateFrom));

        //        if (obj.MonthId > 0)
        //            query = query.Where(x => x.RTSDate.Month == obj.MonthId);
        //        if (obj.YearName > 0)
        //            query = query.Where(x => x.RTSDate.Year == obj.YearName);
        //        //if (obj.Session_Year != "-- Select --")
        //        //    query = query.Where(x => x.SessionYear == obj.Session_Year);

        //        query = query.OrderBy(x => x.RTSID);
        //        var record = query.ToList();
        //        return Ok(record);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("ViewRTS")]
        public IActionResult ViewRTS([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewRTS> query = _context.ViewRTS;

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

                if (!string.IsNullOrEmpty(obj.RTSNo))
                    query = query.Where(x => x.RTSNo.Contains(obj.RTSNo));
                //if (!string.IsNullOrEmpty(obj.PONO))
                //    query = query.Where(x => x.PONO.Contains(obj.PONO));
                //if (!string.IsNullOrEmpty(obj.RMFor))
                //    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);
                //if (obj.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == obj.ContractorId);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.RTSDate >= (obj.DateFrom) && x.RTSDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.RTSDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.RTSDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.RTSDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderBy(x => x.RTSID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetRTSNo")]
        public IActionResult GetRTSNo()
        {
            try
            {
                long SerialNo = 0;
                var lastrecord = _context.ReturnToSupplier.OrderBy(x => x.RTSID).Where(x => x.SessionYear == session_year).LastOrDefault();
                SerialNo = (lastrecord == null ? 0 : lastrecord.RTSSNo) + 1;

                string str = "";
                str = "SBI/RTS/" + session_year + "/" + string.Format("{0:000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRTSApproved/{RTSID}")]
        public IActionResult PutRTSApproved(long RTSID)
        {
            try
            {
                var existingIssue = _context.ReturnToSupplier.Find(RTSID);
                existingIssue.RTSStatus = 1;
                _context.ReturnToSupplier.Update(existingIssue);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRTSDecline/{RTSID}")]
        public IActionResult PutRTSDecline(long RTSID)
        {
            try
            {
                var existingIssue = _context.ReturnToSupplier.Find(RTSID);
                existingIssue.RTSStatus = 2;
                _context.ReturnToSupplier.Update(existingIssue);

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
    }
}
