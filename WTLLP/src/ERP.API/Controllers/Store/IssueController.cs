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
    public class IssueController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        string sort_session = "";
        public IssueController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            sort_session = API.Helper.Create_Sort_Session();
        }

        // GET api/values
        [HttpGet]
        public IQueryable<IssueMaster> Get()
        {
            return _context.IssueMaster.AsQueryable();
        }

        [Route("{issueid}")]
        public IActionResult Get(long issueid)
        {
            try
            {
                return Ok(_context.IssueMaster.Where(x => x.IssueID == issueid).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetList")]
        public IActionResult GetList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewIssueRMWP> query = _context.ViewIssueRMWP; //.Where(x => x.CancelStatus == 0)

                if (!string.IsNullOrEmpty(obj.IssueNo))
                    query = query.Where(x => x.IssueNo.Contains(obj.IssueNo));
                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) || x.Code.Contains(obj.RawMaterial));
                if (!string.IsNullOrEmpty(obj.Rack))
                    query = query.Where(x => x.RackNo.Contains(obj.Rack));
                if (!string.IsNullOrEmpty(obj.WorkPlan))
                    query = query.Where(x => x.PlanNo.Contains(obj.WorkPlan));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);
                if (obj.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == obj.ContractorId);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.IssueDate >= (obj.DateFrom) && x.IssueDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.IssueDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.IssueDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.IssueDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);

                if (!string.IsNullOrEmpty(obj.PRNo))
                    query = query.Where(x => x.PRNo.Contains(obj.PRNo));

                query = query.OrderByDescending(x => x.IssueID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssueMasterID")]
        public IActionResult GetNewIssueMasterID()
        {
            try
            {
                long IssueID = 0;
                var lastissue = _context.IssueMaster.OrderBy(x => x.IssueID).LastOrDefault();
                IssueID = (lastissue == null ? 0 : lastissue.IssueID) + 1;
                return Ok(IssueID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssueChildID")]
        public IActionResult GetNewIssueChildID()
        {
            try
            {
                long IssueChildID = 0;
                var lastIssueChild = _context.IssueChild.OrderBy(x => x.IssueChildID).LastOrDefault();
                IssueChildID = (lastIssueChild == null ? 0 : lastIssueChild.IssueChildID) + 1;
                return Ok(IssueChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetNewIssueSerial")]
        //public IActionResult GetNewIssueSerial()
        //{
        //    try
        //    {
        //        long IssueSerialNo = 0;
        //        var lastIssue = _context.IssueMaster.OrderBy(x => x.IssueSNo).LastOrDefault();
        //        IssueSerialNo = (lastIssue == null ? 0 : lastIssue.IssueSNo) + 1;
        //        return Ok(IssueSerialNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewIssueSerial")]
        public IActionResult GetNewIssueSerial()
        {
            try
            {               
                long Serial = NewIssueSerial();
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewIssueNO")]
        public IActionResult GetNewIssueNO()
        {
            try
            {
                string DisplayString = NewIssueNO();
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        private long NewIssueSerial()
        {
            long IssueSNo = _context.IssueMaster.Where(x => x.SessionYear == sort_session).Max(x => x.IssueSNo);
            IssueSNo++;
            return IssueSNo;
        }

        private string NewIssueNO()
        {
            string sort_session = API.Helper.Create_Sort_Session();
            long Serial = 0;
            var FirstRecord = _context.IssueMaster.OrderBy(x => x.IssueID).FirstOrDefault();
            var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "IssueRMWP").FirstOrDefault();

            if (FirstRecord == null)
            {
                Serial = FormSettings.StartingNumber;
            }
            else
            {
                var LastRecord = _context.IssueMaster.Where(x => x.SessionYear == sort_session).OrderBy(x => x.IssueSNo).LastOrDefault();
                Serial = (LastRecord == null ? 0 : LastRecord.IssueSNo) + 1;
            }

            String DisplayString = FormSettings.Prefix + "/" + sort_session + "/";
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

        [HttpPost]
        public IActionResult Post([FromBody]IssueMaster value)
        {
            try
            {
                value.IssueSNo = NewIssueSerial();
                value.IssueNo = NewIssueNO();
                
                _context.IssueMaster.Add(value);
                _context.SaveChanges();

                var obj = _context.ProcessExecutionMaster.Where(x => x.PRMasterId == value.PRMasterId).FirstOrDefault();
                if (obj != null)
                {
                    obj.IsRMIssue = 1;
                    _context.ProcessExecutionMaster.Update(obj);
                    _context.SaveChanges();
                }

                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRackListFromRitemNSupplierid/{ritemid}/{supplierid}")]
        public IActionResult GetRackListFromRitemNSupplierid(long ritemid, long supplierid)
        {
            try
            {
                //var racklist = _context.ViewOpStockDetails.Where(x => x.RitemID == ritemid && x.SupplierID == supplierid)
                //    .OrderBy(x => x.RackID).Select(x => new { Id = x.RackID, Value = x.RackNo });
                //return Ok(racklist);

                var racklist = _context.ViewRMStockRackWise.Where(x => x.RItem_Id == ritemid && x.SupplierId == supplierid)
                    .OrderBy(x =>x.RackId)             
                    .Select(x => new { Id = x.RackId, Value = x.RackNo + " " + x.Remark })
                    .Distinct();

                return Ok(racklist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutIssueStatus/{issueid}")]
        public IActionResult PutIssueStatus(long issueid)
        {
            try
            {
                var existingIssue = _context.IssueMaster.Find(issueid);
                existingIssue.CancelStatus = 1;
                _context.IssueMaster.Update(existingIssue);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMIssuePrint/{id}")]
        public IQueryable<ViewIssueRMWP> GetRMIssuePrint(long id)
        {
            return _context.ViewIssueRMWP.Where(x => x.IssueID == id).AsQueryable();
        }

        [HttpPost]
        [Route("PostReturn")]
        public IActionResult PostReturn([FromBody]ReturnRMWP value)
        {
            try
            {
                _context.ReturnRMWP.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReturnID")]
        public IActionResult GetNewReturnID()
        {
            try
            {
                long id = 0;
                var record = _context.ReturnRMWP.OrderBy(x => x.ReturnRMWPID).LastOrDefault();
                id = (record == null ? 0 : record.ReturnRMWPID) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetReturnRMWP/{PlanId}")]
        public IActionResult GetReturnRMWP(long PlanId)
        {
            try
            {
                return Ok(_context.ViewReturnRMWP.Where(x => x.PlanId == PlanId).OrderBy(x => x.ReturnRMWPID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetReturnRMWP")]
        //public IActionResult GetReturnRMWP(string search)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(search))
        //            return Ok(_context.ViewReturnRMWP.OrderBy(x => x.ReturnRMWPID));
        //        else
        //            return Ok(_context.ViewReturnRMWP.Where(x => x.PlanNo.Contains(search) || x.Full_Name.Contains(search) || x.SubCategoryName.Contains(search) || x.SupplierName.Contains(search)).OrderBy(x => x.ReturnRMWPID));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("GetReturnRMWP")]
        public IActionResult GetReturnRMWP([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewReturnRMWP> query = _context.ViewReturnRMWP;

                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) || x.Code.Contains(obj.RawMaterial));
                if (!string.IsNullOrEmpty(obj.Rack))
                    query = query.Where(x => x.RackNo.Contains(obj.Rack));
                if (!string.IsNullOrEmpty(obj.WorkPlan))
                    query = query.Where(x => x.PlanNo.Contains(obj.WorkPlan));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);
                //if (obj.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == obj.ContractorId);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.ReturnDate >= (obj.DateFrom) && x.ReturnDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.ReturnDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.ReturnDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.ReturnDate.Year == obj.YearName);
                //if (obj.Session_Year != "-- Select --")
                //    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderBy(x => x.ReturnRMWPID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        // request RM
        [Route("GetNewReqRMNo")]
        public IActionResult GetNewReqRMNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.RequestRM.OrderBy(x => x.ReqRMSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReqRMSNo) + 1;

                String str = "SBI/REQ/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostRequestRM")]
        public IActionResult PostRequestRM([FromBody]RequestRM value)
        {
            try
            {
                long masterid = 0;
                var record = _context.RequestRM.OrderBy(x => x.ReqRMId).LastOrDefault();
                masterid = (record == null ? 0 : record.ReqRMId) + 1;

                long SerialNo = 0;
                var lastIssue = _context.RequestRM.OrderBy(x => x.ReqRMSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (lastIssue == null ? 0 : lastIssue.ReqRMSNo) + 1;

                String ReqRMNo = "SBI/REQ/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";

                value.ReqRMId = masterid;
                value.ReqRMSNo = SerialNo;
                value.ReqRMNo = ReqRMNo;

                long childid = 0;
                var childrecord = _context.RequestRMChild.OrderBy(x => x.ReqRMChildId).LastOrDefault();
                childid = (childrecord == null ? 0 : childrecord.ReqRMChildId) + 1;
                foreach (RequestRMChild child in value.RequestRMChild)
                {
                    child.ReqRMId = masterid;
                    child.ReqRMChildId = childid;
                    childid++;
                }
                _context.RequestRM.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRequestRM")]
        public IActionResult PutRequestRM([FromBody]RequestRM value)
        {
            try
            {
                var existingreqrm = _context.RequestRM
                        .Where(x => x.ReqRMId == value.ReqRMId)
                        .Include(x => x.RequestRMChild)
                        .SingleOrDefault();

                if (existingreqrm != null)
                {
                    // Update parent
                    _context.Entry(existingreqrm).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var Existingreqchild in existingreqrm.RequestRMChild.ToList())
                    {
                        _context.RequestRMChild.Remove(Existingreqchild);
                    }

                    long childid = _context.RequestRMChild.Max(x=> x.ReqRMChildId);
                    childid++;

                    // Update and Insert children
                    foreach (var child in value.RequestRMChild)
                    {
                        child.ReqRMChildId = childid;
                        existingreqrm.RequestRMChild.Add(child);
                        childid++;
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

        [Route("GetReqRMNoList/{DepartmentId}")]
        public IActionResult GetReqRMNoList(long DepartmentId)
        {
            try
            {
                object obj;
                if (DepartmentId == 0)
                {
                    obj = _context.RequestRM.Where(x => x.ReqRMStatus == 0 || x.ReqRMStatus == 3)
                                        .OrderBy(c => c.ReqRMId)
                                        .Select(x => new { Id = x.ReqRMId, Value = x.ReqRMNo }).ToList();
                }
                else
                {
                    obj = _context.RequestRM.Where(x => x.ReqRMStatus == 0 || x.ReqRMStatus == 3)
                                        .Where(x=> x.DepartmentId == DepartmentId)
                                        .OrderBy(c => c.ReqRMId)
                                        .Select(x => new { Id = x.ReqRMId, Value = x.ReqRMNo }).ToList();
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetViewRequestRM")]
        public IActionResult GetViewRequestRM([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewRequestRM> query = _context.ViewRequestRM;

                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) || x.Code.Contains(obj.RawMaterial));

                if (obj.DepartmentId > 0)
                    query = query.Where(x => x.DepartmentId == obj.DepartmentId);

                if (!string.IsNullOrEmpty(obj.ReqRMNo))
                    query = query.Where(x => x.ReqRMNo.Contains(obj.ReqRMNo));
                if (!string.IsNullOrEmpty(obj.RMFor))
                    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.ReqRMDate >= (obj.DateFrom) && x.ReqRMDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.ReqRMDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.ReqRMDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.ReqRMDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderBy(x => x.ReqRMSNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetViewRequestRMPending")]
        public IActionResult GetViewRequestRMPending(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewRequestRM.Where(x => x.ReqRMStatus == 0 || x.ReqRMStatus == 3)
                        .OrderBy(x => x.ReqRMSNo));
                else
                    return Ok(_context.ViewRequestRM
                        .Where(x => x.ReqRMStatus == 0 || x.ReqRMStatus == 3 && (x.ReqRMNo.Contains(search) ||   x.Full_Name.Contains(search)))
                        .OrderBy(x => x.ReqRMSNo));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetReqRMRecord/{ReqRMId}")]
        public IActionResult GetReqRMRecord(long ReqRMId)
        {
            try
            {
                return Ok(_context.ViewRequestRM.Where(x => x.ReqRMID == ReqRMId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetReturnRMRecord/{RetRMId}")]
        public IActionResult GetReturnRMRecord(long RetRMId)
        {
            try
            {
                return Ok(_context.ViewRetRMList.Where(x => x.ReturnRMID == RetRMId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetViewReqRMList/{ReqRMID}")]
        public IActionResult GetViewReqRMList(long ReqRMID)
        {
            try
            {
                return Ok(_context.ViewReqRMList.Where(x => x.ReqRMID == ReqRMID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReqRMIssueNo")]
        public IActionResult GetNewReqRMIssueNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.RequestRMIssue.OrderBy(x => x.ReqRMIssueSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReqRMIssueSNo) + 1;

                String str = "SBI/RISU/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostRequestRMIssue")]
        public IActionResult PostRequestRMIssue([FromBody]RequestRMIssue value)
        {
            try
            {
                long masterid = 0;
                var record = _context.RequestRMIssue.OrderBy(x => x.ReqRMIssueId).LastOrDefault();
                masterid = (record == null ? 0 : record.ReqRMIssueId) + 1;

                long SerialNo = 0;
                var last = _context.RequestRMIssue.OrderBy(x => x.ReqRMIssueSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReqRMIssueSNo) + 1;
                String ReqRMIssueNo = "SBI/RISU/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";

                value.ReqRMIssueId = masterid;
                value.ReqRMIssueSNo = SerialNo;
                value.ReqRMIssueNo = ReqRMIssueNo;

                long childid = 0;
                var childrecord = _context.RequestRMIssueChild.OrderBy(x => x.ReqRMIssueChildId).LastOrDefault();
                childid = (childrecord == null ? 0 : childrecord.ReqRMIssueChildId) + 1;
                foreach (RequestRMIssueChild child in value.RequestRMIssueChild)
                {
                    child.ReqRMIssueId = masterid;
                    child.ReqRMIssueChildId = childid;
                    childid++;
                }
                _context.RequestRMIssue.Add(value);
                _context.SaveChanges();

                var obj = _context.RequestRM.Where(x => x.ReqRMId == value.ReqRMId).LastOrDefault();
                obj.ReqRMStatus = 3;
                _context.RequestRM.Update(obj);
                _context.SaveChanges();

                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpGet]
        //[Route("GetViewRequestRMIssue")]
        //public IActionResult GetViewRequestRMIssue(string search)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(search))
        //            return Ok(_context.ViewRequestRMIssue
        //                .OrderByDescending(x => x.ReqRMID));
        //        else
        //            return Ok(_context.ViewRequestRMIssue
        //                .Where(x => (x.ReqRMIssueNo.Contains(search) || x.CategoryName.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
        //               .OrderByDescending(x => x.ReqRMID));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("GetViewRequestRMIssue")]
        public IActionResult GetViewRequestRMIssue([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewRequestRMIssue> query = _context.ViewRequestRMIssue;

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

                if (obj.DepartmentId > 0)
                    query = query.Where(x => x.DepartmentId == obj.DepartmentId);

                if (!string.IsNullOrEmpty(obj.ReqRMIssueNo))
                    query = query.Where(x => x.ReqRMIssueNo.Contains(obj.ReqRMIssueNo));

                if (!string.IsNullOrEmpty(obj.ReqRMNo))
                    query = query.Where(x => x.ReqRMNo.Contains(obj.ReqRMNo));
                if (!string.IsNullOrEmpty(obj.RMFor))
                    query = query.Where(x => x.RMFor.Contains(obj.RMFor));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);
                //if (obj.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == obj.ContractorId);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.ReqRMIssueDate >= (obj.DateFrom) && x.ReqRMIssueDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.ReqRMIssueDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.ReqRMIssueDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.ReqRMIssueDate.Year == obj.YearName);
                //if (obj.Session_Year != "-- Select --")
                //    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderBy(x => x.ReqRMIssueID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("RequestRMPrint/{ReqRMID}")]
        public IActionResult RequestRMPrint(long ReqRMID)
        {
            try
            {
                return Ok(_context.ViewRequestRM.Where(x => x.ReqRMID == ReqRMID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRequestCompleteStatus/{ReqRMID}")]
        public IActionResult PutRequestCompleteStatus(long ReqRMID)
        {
            try
            {
                var existing = _context.RequestRM.Find(ReqRMID);
                existing.ReqRMStatus = 1;
                _context.RequestRM.Update(existing);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRequestCancelStatus/{ReqRMID}")]
        public IActionResult PutRequestCancelStatus(long ReqRMID)
        {
            try
            {
                var existing = _context.RequestRM.Find(ReqRMID);
                existing.ReqRMStatus = 2;
                _context.RequestRM.Update(existing);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReturnRMNo")]
        public IActionResult GetNewReturnRMNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.ReturnRM.OrderBy(x => x.ReturnRMSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReturnRMSNo) + 1;

                String str = "SBI/RTR/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostReturnRM")]
        public IActionResult PostReturnRM([FromBody]ReturnRM value)
        {
            try
            {
                long masterid = 0;
                var record = _context.ReturnRM.OrderBy(x => x.ReturnRMID).LastOrDefault();
                masterid = (record == null ? 0 : record.ReturnRMID) + 1;

                long SerialNo = 0;
                var lastIssue = _context.ReturnRM.OrderBy(x => x.ReturnRMSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (lastIssue == null ? 0 : lastIssue.ReturnRMSNo) + 1;

                String ReqRMNo = "SBI/RTR/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";

                value.ReturnRMID = masterid;
                value.ReturnRMSNo = SerialNo;
                value.ReturnRMNo = ReqRMNo;

                long childid = 0;
                var childrecord = _context.ReturnRMChild.OrderBy(x => x.ReturnRMChildID).LastOrDefault();
                childid = (childrecord == null ? 0 : childrecord.ReturnRMChildID) + 1;
                foreach (ReturnRMChild child in value.ReturnRMChild)
                {
                    child.ReturnRMID = masterid;
                    child.ReturnRMChildID = childid;
                    childid++;
                }
                _context.ReturnRM.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRetRMNoList")]
        public IActionResult GetRetRMNoList()
        {
            try
            {
                object obj;
                obj = _context.ReturnRM.Where(x => x.ReturnRMStatus == 0)
                                    .OrderBy(c => c.ReturnRMID)
                                    .Select(x => new { Id = x.ReturnRMID, Value = x.ReturnRMNo }).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetViewRetRMList/{RetRMID}")]
        public IActionResult GetViewRetRMList(long RetRMID)
        {
            try
            {
                return Ok(_context.ViewRetRMList.Where(x => x.ReturnRMID == RetRMID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewReturnRMRcvNo")]
        public IActionResult GetNewReturnRMRcvNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.ReturnRMRecv.OrderBy(x => x.ReturnRMRecvSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReturnRMRecvSNo) + 1;
                String str = "SBI/RCV/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostReturnRMRecv")]
        public IActionResult PostReturnRMRecv([FromBody]ReturnRMRecv value)
        {
            try
            {
                long masterid = 0;
                var record = _context.ReturnRMRecv.OrderBy(x => x.ReturnRMRecvID).LastOrDefault();
                masterid = (record == null ? 0 : record.ReturnRMRecvID) + 1;

                long SerialNo = 0;
                var last = _context.ReturnRMRecv.OrderBy(x => x.ReturnRMRecvSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.ReturnRMRecvSNo) + 1;
                String str = "SBI/RCV/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";

                value.ReturnRMRecvID = masterid;
                value.ReturnRMRecvSNo = SerialNo;
                value.ReturnRMRecvNo = str;


                long childid = 0;
                var childrecord = _context.ReturnRMRecvChild.OrderBy(x => x.ReturnRMRecvChildID).LastOrDefault();
                childid = (childrecord == null ? 0 : childrecord.ReturnRMRecvChildID) + 1;
                foreach (ReturnRMRecvChild child in value.ReturnRMRecvChild)
                {
                    child.ReturnRMRecvID = masterid;
                    child.ReturnRMRecvChildID = childid;
                    childid++;
                }
                _context.ReturnRMRecv.Add(value);

                var obj = _context.ReturnRM.Where(x => x.ReturnRMID == value.ReturnRMID).LastOrDefault();
                obj.ReturnRMStatus = 1;
                _context.ReturnRM.Update(obj);

                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpGet]
        //[Route("GetViewReturnRMRecv")]
        //public IActionResult GetViewReturnRMRecv(string search)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(search))
        //            return Ok(_context.ViewReturnRMRecv
        //                .OrderByDescending(x => x.ReturnRMRecvID));
        //        else
        //            return Ok(_context.ViewReturnRMRecv
        //                .Where(x => (x.ReturnRMRecvNo.Contains(search) || x.CategoryName.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search)))
        //               .OrderByDescending(x => x.ReturnRMRecvID));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("GetViewReturnRMRecv")]
        public IActionResult GetViewReturnRMRecv([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewReturnRMRecv> query = _context.ViewReturnRMRecv;

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

                if (obj.DepartmentId > 0)
                    query = query.Where(x => x.DepartmentId == obj.DepartmentId);

                if (!string.IsNullOrEmpty(obj.ReturnRMNo))
                    query = query.Where(x => x.ReturnRMNo.Contains(obj.ReturnRMNo));
                if (!string.IsNullOrEmpty(obj.ReturnRMRecvNo))
                    query = query.Where(x => x.ReturnRMRecvNo.Contains(obj.ReturnRMRecvNo));

                if (obj.SupplierID > 0)
                    query = query.Where(x => x.SupplierId == obj.SupplierID);

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.ReturnRMRecvDate >= (obj.DateFrom) && x.ReturnRMRecvDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.ReturnRMRecvDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.ReturnRMRecvDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.ReturnRMRecvDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderByDescending(x => x.ReturnRMRecvID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        #region StockRMRequest

        [Route("GetNewStockTransferRequestID")]
        public IActionResult GetNewStockTransferRequestID()
        {
            try
            {
                long ID = 0;
                var lastID = _context.StockTransferRequest.OrderBy(x => x.StockTransferRequestId).LastOrDefault();
                ID = (lastID == null ? 0 : lastID.StockTransferRequestId) + 1;
                return Ok(ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStockTransferRequestNo")]
        public IActionResult GetStockTransferRequestNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.StockTransferRequest.OrderBy(x => x.StockTransferRequestSNo).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.StockTransferRequestSNo) + 1;

                String str = "SBI/SREQ/" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStockTransferRequestSNo")]
        public IActionResult GetNewStockTransferRequestSNo()
        {
            try
            {
                long SNO = 0;
                var LastSNO = _context.StockTransferRequest.OrderBy(x => x.StockTransferRequestSNo).LastOrDefault();
                SNO = (LastSNO == null ? 0 : LastSNO.StockTransferRequestSNo) + 1;
                return Ok(SNO);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStockTransferRequestChildID")]
        public IActionResult GetNewStockTransferRequestChildID()
        {
            try
            {
                long ID = 0;
                var LastID = _context.StockTransferRequestChild.OrderBy(x => x.StockTransferRequestChildId).LastOrDefault();
                ID = (LastID == null ? 0 : LastID.StockTransferRequestChildId) + 1;
                return Ok(ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostStockTransferRequest")]
        public IActionResult PostStockTransferRequest([FromBody]StockTransferRequest value)
        {
            try
            {
                _context.StockTransferRequest.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutStockTransferRequest")]
        public IActionResult PutStockTransferRequest([FromBody]StockTransferRequest value)
        {
            try
            {
                var existingdata = _context.StockTransferRequest
                        .Where(x => x.StockTransferRequestId == value.StockTransferRequestId)
                        .Include(x => x.StockTransferRequestChild)
                        .SingleOrDefault();

                if (existingdata != null)
                {
                    // Update parent
                    _context.Entry(existingdata).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var Existingchild in existingdata.StockTransferRequestChild.ToList())
                    {
                        _context.StockTransferRequestChild.Remove(Existingchild);
                    }

                    long childid = _context.StockTransferRequestChild.Max(x => x.StockTransferRequestChildId);
                    childid++;

                    // Update and Insert children
                    foreach (var child in value.StockTransferRequestChild)
                    {
                        child.StockTransferRequestChildId = childid;
                        existingdata.StockTransferRequestChild.Add(child);
                        childid++;
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
        [Route("GetStockTransferRequestList")]
        public IActionResult GetStockTransferRequestList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewStockTransferRequest> query = _context.ViewStockTransferRequest;

                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial));

                if (obj.RequestBranchId > 0)
                    query = query.Where(x => x.RequestBranchId == obj.RequestBranchId);

                if (obj.RequestToBranchId > 0)
                    query = query.Where(x => x.RequestToBranchId == obj.RequestToBranchId);


                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.StockTransferRequestDate >= (obj.DateFrom) && x.StockTransferRequestDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.StockTransferRequestDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.StockTransferRequestDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.StockTransferRequestDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);


                query = query.OrderByDescending(x => x.StockTransferRequestId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStockTransferRequestDetails/{id}")]
        public IActionResult GetStockTransferRequestDetails(long id)
        {
            try
            {
                return Ok(_context.ViewStockTransferRequest.Where(x => x.StockTransferRequestId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetStockTransferRequestChild/{id}")]
        public IQueryable<ViewStockTransferRequest> GetStockTransferRequestChild(long id)

        {
            return _context.ViewStockTransferRequest.Where(x => x.StockTransferRequestId == id).AsQueryable();
        }

        [Route("DeleteStockTransferRequest/{id}")]
        public IActionResult DeleteStockTransferRequest(int id)
        {
            try
            {

                var existingData = _context.StockTransferRequest
                     .Where(x => x.StockTransferRequestId == id)
                     .Include(x => x.StockTransferRequestChild)
                     .SingleOrDefault();
                if (existingData != null)
                {
                    foreach (var ExistingchildData in existingData.StockTransferRequestChild.ToList())
                    {
                        _context.StockTransferRequestChild.Remove(ExistingchildData);
                    }
                    _context.StockTransferRequest.Remove(existingData);
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
        [Route("GetStockTransferRequestPrint/{id}")]
        public IQueryable<ViewStockTransferRequest> GetStockTransferRequestPrint(long id)
        {
            return _context.ViewStockTransferRequest.Where(x => x.StockTransferRequestId == id).AsQueryable();
        }



        [Route("GetNewStockTransferIssueID")]
        public IActionResult GetNewStockTransferIssueID()
        {
            try
            {
                long ID = 0;
                var lastID = _context.StockTransferIssue.OrderBy(x => x.StockTransferIssueId).LastOrDefault();
                ID = (lastID == null ? 0 : lastID.StockTransferIssueId) + 1;
                return Ok(ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetStockTransferRequestList")]
        public IActionResult GetStockTransferRequestList()
        {
            try
            {
                object list;
                list = _context.StockTransferRequest
                                    .OrderBy(c => c.StockTransferRequestNo)
                                    .Select(x => new { Id = x.StockTransferRequestId, Value = x.StockTransferRequestNo }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetStockTransferRequestForIssue/{StockTransferRequestId}")]
        public IActionResult GetStockTransferRequestForIssue(long StockTransferRequestId)
        {
            try
            {
                return Ok(_context.ViewStockTransferRequest.Where(x => x.StockTransferRequestId == StockTransferRequestId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStockTransferIssueNo")]
        public IActionResult GetStockTransferIssueNo()
        {
            try
            {
                long SerialNo = 0;
                var last = _context.StockTransferIssue.OrderBy(x => x.StockTransferIssueId).Where(x => x.SessionYear == sort_session).LastOrDefault();
                SerialNo = (last == null ? 0 : last.StockTransferIssueId) + 1;

                String str = "SBI/SISU" + sort_session + "/" + string.Format("{0:0000}", SerialNo) + "";
                return Ok(str);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewStockTransferIssueChildID")]
        public IActionResult GetNewStockTransferIssueChildID()
        {
            try
            {
                long ID = 0;
                var LastID = _context.StockTransferIssueChild.OrderBy(x => x.StockTransferIssueChildId).LastOrDefault();
                ID = (LastID == null ? 0 : LastID.StockTransferIssueChildId) + 1;
                return Ok(ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostStockTransferIssue")]
        public IActionResult PostStockTransferIssue([FromBody]StockTransferIssue value)
        {
            try
            {
                _context.StockTransferIssue.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutStockTransferIssue")]
        public IActionResult PutStockTransferIssue([FromBody]StockTransferIssue value)
        {
            try
            {
                var existingdata = _context.StockTransferIssue
                     .Where(x => x.StockTransferIssueId == value.StockTransferIssueId)
                     .Include(x => x.StockTransferIssueChild)
                     .SingleOrDefault();

                if (existingdata != null)
                {
                    // Update parent
                    _context.Entry(existingdata).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var Existingchild in existingdata.StockTransferIssueChild.ToList())
                    {
                        _context.StockTransferIssueChild.Remove(Existingchild);
                    }

                    long childid = _context.StockTransferIssueChild.Max(x => x.StockTransferIssueChildId);
                    childid++;

                    // Update and Insert children
                    foreach (var child in value.StockTransferIssueChild)
                    {
                        child.StockTransferRequestChildId = childid;
                        existingdata.StockTransferIssueChild.Add(child);
                        childid++;
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
        [Route("GetStockTransferIssueList")]
        public IActionResult GetStockTransferIssueList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewStockTransferIssue> query = _context.ViewStockTransferIssue;

                if (obj.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == obj.CategoryID);
                if (obj.SubCategoryID > 0)
                    query = query.Where(x => x.SubCategoryID == obj.SubCategoryID);
                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial));

                if (obj.RequestBranchId > 0)
                    query = query.Where(x => x.RequestBranchId == obj.RequestBranchId);

                if (obj.RequestToBranchId > 0)
                    query = query.Where(x => x.RequestToBranchId == obj.RequestToBranchId);

                if (!string.IsNullOrEmpty(obj.StockTransferIssueNo))
                    query = query.Where(x => x.StockTransferIssueNo.Contains(obj.StockTransferIssueNo));

                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.StockTransferIssueDate >= (obj.DateFrom) && x.StockTransferIssueDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.StockTransferIssueDate == (obj.DateFrom));

                if (obj.MonthId > 0)
                    query = query.Where(x => x.StockTransferIssueDate.Month == obj.MonthId);
                if (obj.YearName > 0)
                    query = query.Where(x => x.StockTransferIssueDate.Year == obj.YearName);
                if (obj.Session_Year != "-- Select --")
                    query = query.Where(x => x.SessionYear == obj.Session_Year);

                query = query.OrderByDescending(x => x.StockTransferIssueId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStockTransferIssueDetails/{id}")]
        public IActionResult GetStockTransferIssueDetails(long id)
        {
            try
            {
                return Ok(_context.ViewStockTransferIssue.Where(x => x.StockTransferIssueId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStockTransferIssuePrint/{id}")]
        public IQueryable<ViewStockTransferIssue> GetStockTransferIssuePrint(long id)
        {
            return _context.ViewStockTransferIssue.Where(x => x.StockTransferIssueId == id).AsQueryable();
        }
        [Route("GetStockTransferIssueChild/{id}")]
        public IQueryable<ViewStockTransferIssue> GetStockTransferIssueChild(long id)

        {
            return _context.ViewStockTransferIssue.Where(x => x.StockTransferIssueId == id).AsQueryable();
        }

        [Route("DeleteStockTransferIssue/{id}")]
        public IActionResult DeleteStockTransferIssue(int id)
        {
            try
            {
                var existingData = _context.StockTransferIssue
                     .Where(x => x.StockTransferIssueId == id)
                     .Include(x => x.StockTransferIssueChild)
                     .SingleOrDefault();
                if (existingData != null)
                {
                    foreach (var ExistingchildData in existingData.StockTransferIssueChild.ToList())
                    {
                        _context.StockTransferIssueChild.Remove(ExistingchildData);
                    }
                    _context.StockTransferIssue.Remove(existingData);
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
        #endregion

        #region DailyMaterialIssueNote

        [HttpPost]
        [Route("GetDailyMaterialIssueNote")]
        public IActionResult GetDailyMaterialIssueNote([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewIssueRMWP> query = _context.ViewIssueRMWP; //.Where(x => x.CancelStatus == 0)

                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) /*|| x.Code.Contains(obj.RawMaterial)*/);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.IssueDate >= (obj.DateFrom) && x.IssueDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.IssueDate == (obj.DateFrom));

                query = query.OrderByDescending(x => x.IssueID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetDailyMaterialIssueNoteList")]
        public IActionResult GetDailyMaterialIssueNoteList([FromBody]StoreSearch obj)
        {
            try
            {
                IQueryable<ViewRequestRMIssue> query = _context.ViewRequestRMIssue; //.Where(x => x.CancelStatus == 0)

                if (!string.IsNullOrEmpty(obj.RawMaterial))
                    query = query.Where(x => x.Full_Name.Contains(obj.RawMaterial) /*|| x.Code.Contains(obj.RawMaterial)*/);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 1)
                    query = query.Where(x => x.ReqRMIssueDate >= (obj.DateFrom) && x.ReqRMIssueDate <= obj.DateTo);
                if (obj.ChkDateFromInt == 1 && obj.ChkDateToInt == 0)
                    query = query.Where(x => x.ReqRMIssueDate == (obj.DateFrom));

                query = query.OrderByDescending(x => x.ReqRMID);
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
