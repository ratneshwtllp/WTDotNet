using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class WorkPlanController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        string sort_session = "";

        public WorkPlanController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            sort_session = API.Helper.Create_Sort_Session();
            _context.Database.SetCommandTimeout(360);
        }

        [HttpGet]
        public IQueryable<ViewWorkPlan> Get()
        {
            //return _context.WorkPlanMaster
            //    .OrderByDescending(x => x.PlanId).AsQueryable();

            return _context.ViewWorkPlan.Where(x => x.WorkplanCancelStatus == 0)
                .OrderByDescending(x => x.PlanId).AsQueryable();
        }

        [Route("{planid}")]
        public IActionResult Get(long planid)
        {
            try
            {
                return Ok(_context.ViewWorkPlan.Where(x => x.PlanId == planid).FirstOrDefault()); //WorkPlanMaster
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPlanID")]
        public IActionResult GetNewPlanID()
        {
            try
            {
                long PlanID = 0;
                var lastPlan = _context.WorkPlanMaster.OrderBy(x => x.PlanId).LastOrDefault();
                PlanID = (lastPlan == null ? 0 : lastPlan.PlanId) + 1;
                return Ok(PlanID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPlanChildID")]
        public IActionResult GetNewPlanChildID()
        {
            try
            {
                long PlanChildID = 0;
                var lastPlanChild = _context.WorkPlanChild.OrderBy(x => x.PlanChildId).LastOrDefault();
                PlanChildID = (lastPlanChild == null ? 0 : lastPlanChild.PlanChildId) + 1;
                return Ok(PlanChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPlanSerial")]
        public IActionResult GetNewPlanSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.WorkPlanMaster.OrderBy(x => x.PlanId).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "WorkPlan").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.WorkPlanMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PlanSNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PlanSNo) + 1;
                }
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPlanNO")]
        public IActionResult GetNewPlanNO()
        {
            try
            {
                string session = API.Helper.Create_Session();
                string sort_session = API.Helper.Create_Sort_Session();
                long Serial = 0;
                var FirstRecord = _context.WorkPlanMaster.OrderBy(x => x.PlanId).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "WorkPlan").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.WorkPlanMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PlanSNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PlanSNo) + 1;
                }

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
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]WorkPlanMaster value)
        {
            try
            {
                _context.WorkPlanMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutWPStatus/{planid}")]
        public IActionResult PutWPStatus(long planid)
        {
            try
            {
                var existingWP = _context.WorkPlanMaster.Find(planid);
                existingWP.CancelStatus = 1;
                _context.WorkPlanMaster.Update(existingWP);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutWPCompleteStatus/{planid}")]
        public IActionResult PutWPCompleteStatus(long planid)
        {
            try
            {
                var existingWP = _context.WorkPlanMaster.Find(planid);
                existingWP.IsPending = 1;
                _context.WorkPlanMaster.Update(existingWP);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetWorkPlanDetails/{planid}")]
        public IActionResult GetWorkPlanDetails(long planid)
        {
            try
            {
                var details = _context.ViewWorkPlanChild.Where(x => x.PlanId == planid)
                    .OrderBy(x => x.Code);

                return Ok(details);
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
                var ordernolist = _context.ViewSaleOrder.Where(x => x.BuyerId == buyerid && x.CancelStatus == 0)
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

        [Route("GetWorkPlanRMDetails/{planid}")]
        public IActionResult GetWorkPlanRMDetails(long planid)
        {
            //try
            //{
            //    var details = _context.ViewWorkPlanBOM.Where(x => x.PlanId == planid)
            //        .OrderBy(x => x.RItemID); 

            //    return Ok(details);
            //}
            //catch (Exception ex)
            //{
            //    return Ok(ex.InnerException);
            //}

            try
            {
                var result = from c in _context.ViewWorkPlanBOM
                             where (c.PlanId == planid)
                             group c by new { c.CategoryName, c.SubCategoryName, c.Code, c.Full_Name, c.CostUnit, c.RItemID, c.CostUnitSName, c.Title, c.IssueQty, c.ReserveQty, c.RMStock } into g     //, c.Title
                             orderby g.Key.CategoryName, g.Key.SubCategoryName, g.Key.Full_Name
                             select new
                             {
                                 CategoryName = g.Key.CategoryName,
                                 SubCategoryName = g.Key.SubCategoryName,
                                 Code = g.Key.Code,
                                 Required = g.Sum(x => x.Required),
                                 Full_Name = g.Key.Full_Name,
                                 CostUnit = g.Key.CostUnit,
                                 RItemID = g.Key.RItemID,
                                 CostUnitSName = g.Key.CostUnitSName,
                                 Title = g.Key.Title,
                                 IssueQty = g.Key.IssueQty,
                                 ReserveQty = g.Key.ReserveQty,
                                 RMStock = g.Key.RMStock,
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRequiredQtyDetail/{planid}/{ritemid}")]
        public IActionResult GetRequiredQtyDetail(long planid, long ritemid)
        {
            try
            {
                object Details;
                Details = _context.ViewWorkPlanBOM.Where(x => x.PlanId == planid && x.RItemID == ritemid)
                                         .OrderBy(x => x.PlanId);
                return Ok(Details);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderItemsForBalance/{orderid}")]
        public IActionResult GetOrderItemsForBalance(long orderid)
        {
            try
            {
                var result = from c in _context.ViewWorkPlanChild
                             where (c.OrderId == orderid)
                             group c by new { c.FitemId } into g     //, c.Title
                             orderby g.Key.FitemId
                             select new
                             {
                                 PlanQty = g.Sum(x => x.PlanQty),
                                 FitemId = g.Key.FitemId,
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPlanNoList")]
        public IActionResult GetPlanNoList()
        {
            try
            {
                var planlist = _context.WorkPlanMaster.Where(x => x.IsPending == 0).OrderByDescending(x => x.PlanNo)
                    .Select(x => new { id = x.PlanId, value = x.PlanNo }).ToList();
                return Ok(planlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProcessNoList")]
        public IActionResult GetProcessNoList()
        {
            try
            {
                var prnolist = _context.ProcessExecutionMaster.Where(x => x.IsRead == 0).OrderByDescending(x => x.PRNo)
                    .Select(x => new { id = x.PRMasterId, value = x.PRNo }).ToList();
                return Ok(prnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetProcessNoListForProcessRecv")]
        public IActionResult GetProcessNoListForProcessRecv()
        {
            try
            {
                //var prmasterid = _context.ViewProcessRecv.Select(x => x.PRMasterId).ToList();
                //var prnolist = _context.ProcessExecutionMaster.Where(x => !prmasterid.Contains(x.PRMasterId)).OrderByDescending(x => x.PRNo)
                //    .Select(x => new { id = x.PRMasterId, value = x.PRNo }).ToList();
                var prnolist = _context.ProcessExecutionMaster.Where(x => x.IsProcessReceive == 0).OrderByDescending(x => x.PRMasterId)
                    .Select(x => new { id = x.PRMasterId, value = x.PRNo }).ToList();

                return Ok(prnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetViewPRNoForIssueRM")]
        public IActionResult GetViewPRNoForIssueRM()
        {
            try
            {
                var prnolist = _context.ViewPRNoForIssueRM.OrderByDescending(x => x.PRMasterId)
                    .Select(x => new { id = x.PRMasterId, value = x.PRNo }).ToList();
                return Ok(prnolist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetComponentList")]
        public IActionResult GetComponentList()
        {
            try
            {
                var list = _context.ComponentDetails.OrderBy(x => x.CompName)
                    .Select(x => new { id = x.CompId, value = x.CompName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetComponentList/{FitemId}")]
        public IActionResult GetComponentList(long FitemId)
        {
            try
            {
                var list = _context.ViewProductComponent.Where(x => x.FitemId == FitemId)
                    .Select(x => new { id = x.Comp_Id, value = x.Comp_Name }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPRNo")]
        public IActionResult GetNewPRNo()
        {
            try
            {
                string session = API.Helper.Create_Session();
                string sort_session = API.Helper.Create_Sort_Session();
                long Serial = 0;
                var FirstRecord = _context.ProcessExecutionMaster.OrderBy(x => x.PRMasterId).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Process").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.ProcessExecutionMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PRSerialNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PRSerialNo) + 1;
                }

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
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostProcessExecution")]
        [HttpPost]
        public IActionResult PostProcessExecution([FromBody]ProcessExecutionMaster value)
        {
            try
            {
                var obj = _context.ViewProcessRecvMaster.Where(x => x.PlanChildId == value.PlanChildId && x.ProcessID == value.ProcessFromId).Max(x => x.PRRecvDate);
                if (value.PRDate < obj)
                {
                    return BadRequest("Please check process date, It should be grator than process receive date.");
                }

                //var lastPR = _context.ProcessExecutionMaster.Max(x => x.PRDate);
                //if (value.PRDate < lastPR)
                //{
                //    return BadRequest("Please check process date, It should be grator or equal to previos PR date enterd in system .");
                //}

                string session = API.Helper.Create_Session();
                string sort_session = API.Helper.Create_Sort_Session();

                long Serial = 0;
                long PRMasterId = 0;

                var FirstRecord = _context.ProcessExecutionMaster.OrderBy(x => x.PRMasterId).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "Process").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                    PRMasterId = 1;
                }
                else
                {
                    var LastRecord_session = _context.ProcessExecutionMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PRSerialNo).LastOrDefault();
                    var LastRecord = _context.ProcessExecutionMaster.OrderBy(x => x.PRMasterId).LastOrDefault();

                    Serial = (LastRecord_session == null ? 0 : LastRecord_session.PRSerialNo) + 1;
                    PRMasterId = (LastRecord == null ? 0 : LastRecord.PRMasterId) + 1;
                }

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

                _context.ProcessExecutionMaster.Add(value);
                value.PRMasterId = PRMasterId;
                value.PRSerialNo = Serial;
                value.PRNo = DisplayString;

                var ChildRecord = _context.ProcessExecutionChild.OrderBy(x => x.PRChildId).LastOrDefault();
                long PRChildId = 0;
                PRChildId = (ChildRecord == null ? 0 : ChildRecord.PRChildId) + 1;
                foreach (ProcessExecutionChild child in value.ProcessExecutionChild)
                {
                    child.PRChildId = PRChildId;
                    child.PRMasterId = PRMasterId;
                    PRChildId++;
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ProcessList")]
        public IActionResult ProcessList([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessExecutionMaster> query = _context.ViewProcessExecutionMaster;

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                if (PS.ChkProcessDateInt == 1)
                    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                //if (!string.IsNullOrEmpty(PS.Comp_Name))
                //    query = query.Where(x => x.Comp_Name.Contains(PS.Comp_Name));

                if (PS.PRMasterId > 0)
                    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                if (PS.PlanChildId > 0)
                    query = query.Where(x => x.PlanChildId == PS.PlanChildId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);

                // IsPending For workplan and IsProcessReceive for process
                if (PS.IsFloorReport == 1)
                {
                    var list = _context.ProcessExecutionMaster.GroupBy(x => x.PlanChildId, (key, g) => g.OrderByDescending(e => e.PRMasterId).First()).Select(x => x.PRMasterId).ToArray();
                    query = query.Where(x => x.IsPending == 0 && list.Contains(x.PRMasterId));
                    //query = query.Where(x => x.IsProcessReceive == 0 && x.IsPending == 0); 
                }
                query = query.OrderByDescending(x => x.PRMasterId);
                var record = query.ToList();

                if (PS.IsFloorReport == 1)
                {
                    foreach (var v in record)
                    {
                        v.NoOfProcess = _context.ProductProcess.Where(x => x.FitemId == v.FitemId).Count();
                        v.NoOfProcess--;// for start WP
                        v.CompletedProcess = _context.ProcessExecutionMaster.Where(x => x.PlanChildId == v.PlanChildId).Select(x => x.ProcessID).Distinct().Count();

                        v.BalProcess = v.NoOfProcess - v.CompletedProcess;
                        //if(v.BalProcess == 1)
                        //{
                        //    v.CompletedProcess += 1;
                        //    v.BalProcess = 0;
                        //}
                        v.CompletedPer = v.CompletedProcess * 100 / v.NoOfProcess;
                        v.BalPer = 100- v.CompletedPer;
                        var dispatch_qty =  _context.ProcessExecutionMaster.Where(x => x.PlanChildId == v.PlanChildId && x.ProcessID == 50 ).Sum(x=> x.ProcessToQty);
                        v.IsPayable = dispatch_qty;
                        //v.BalPer = v.BalProcess * 100 / v.NoOfProcess;
                    }
                }
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetProcessListForRecv")]
        public IActionResult GetProcessListForRecv([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessExecution> query = _context.ViewProcessExecution.Where(x => x.PRMasterId == PS.PRMasterId);

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                if (PS.ChkProcessDateInt == 1)
                    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                //if (!string.IsNullOrEmpty(PS.Comp_Name))
                //    query = query.Where(x => x.Comp_Name.Contains(PS.Comp_Name));

                if (PS.PRMasterId > 0)
                    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                //if (PS.PlanId > 0)
                //    query = query.Where(x => x.PlanId == PS.PlanId);

                query = query.OrderByDescending(x => x.PRMasterId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetProcessListForContractorPayment")]
        public IActionResult GetProcessListForContractorPayment([FromBody]ProcessSearch PS)
        {
            try
            {
                //var prmasterid = _context.ContractorPaymentChild.Select(x => x.PRMasterId).ToList();

                IQueryable<ViewProcessExecutionMaster> query = _context.ViewProcessExecutionMaster.FromSql("SELECT * FROM ViewProcessExecutionMaster WHERE PRMasterId not in (Select PRMasterId from ContractorPaymentChild) AND IsProcessReceive = 1 AND ProcessAmount > 0 AND IsPayable  = 1 ");

                //IQueryable<ViewProcessExecutionMaster> query = _context.ViewProcessExecutionMaster.Where(x => x.IsProcessReceive == 1 && x.ProcessAmount > 0);

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                //if (PS.ChkProcessDateInt == 1)
                //    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                //if (!string.IsNullOrEmpty(PS.Comp_Name))
                //    query = query.Where(x => x.Comp_Name.Contains(PS.Comp_Name));

                if (PS.IsRead > 0)
                    query = query.Where(x => x.IsRead == PS.IsRead);

                if (PS.PRMasterId > 0)
                    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                //if (PS.PlanId > 0)
                //    query = query.Where(x => x.PlanId == PS.PlanId);

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.PRDate >= (PS.DateFrom) && x.PRDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.PRDate == (PS.DateFrom));

                //if (prmasterid.Count > 0)
                //    query = query.Where(x => !prmasterid.Contains(x.PRMasterId));
                //if(PS.OrderBySNo ==4)
                //    query = query.OrderBy(x => x.PRMasterId);
                //if(PS.OrderBy == 5)
                //    query = query.OrderBy(x => x.PRDate).ThenBy(x=> x.PRMasterId);
                //else if (PS.OrderBy == 7)
                //    query = query.OrderBy(x => x.EstimatedRecieveDate);
                //else
                //    query = query.OrderBy(x => x.PRMasterId);


                var record = query.ToList();

                foreach (var obj in record)
                {
                    var recvdetails = _context.ProcessRecvMaster.Where(x => x.PRMasterId == obj.PRMasterId).OrderBy(x => x.PRRecvId).LastOrDefault();
                    obj.EstimatedRecieveDate = recvdetails.PRRecvDate;
                    obj.Through = recvdetails.PRRecvNo;
                }

                if (PS.OrderBy == 5)
                    record = record.OrderBy(x => x.PRDate).ThenBy(x => x.PRMasterId).ToList();
                else if (PS.OrderBy == 7)
                    record = record.OrderBy(x => x.EstimatedRecieveDate).ToList();
                else
                    record = record.OrderBy(x => x.PRMasterId).ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostProcessRecv")]
        [HttpPost]
        public IActionResult PostProcessRecv([FromBody]ProcessRecvMaster value)
        {
            try
            {
                string session = API.Helper.Create_Session();
                string sort_session = API.Helper.Create_Sort_Session();

                long Serial = 0;
                long MasterId = 0;

                var FirstRecord = _context.ProcessRecvMaster.OrderBy(x => x.PRRecvId).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "ProcessRecv").FirstOrDefault();
                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                    MasterId = 1;
                }
                else
                {
                    var LastRecord_session = _context.ProcessRecvMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PRRecvSerialNo).LastOrDefault();
                    var LastRecord = _context.ProcessRecvMaster.OrderBy(x => x.PRRecvId).LastOrDefault();
                    Serial = (LastRecord_session == null ? 0 : LastRecord_session.PRRecvSerialNo) + 1;
                    MasterId = (LastRecord == null ? 0 : LastRecord.PRRecvId) + 1;
                }

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
                _context.ProcessRecvMaster.Add(value);
                value.PRRecvId = MasterId;
                value.PRRecvSerialNo = Serial;
                value.PRRecvNo = DisplayString;

                var ChildRecord = _context.ProcessRecvChild.OrderBy(x => x.PRRecvChildId).LastOrDefault();
                long ChildId = 0;
                ChildId = (ChildRecord == null ? 0 : ChildRecord.PRRecvChildId) + 1;
                foreach (ProcessRecvChild child in value.ProcessRecvChild)
                {
                    child.PRRecvChildId = ChildId;
                    child.PRRecvId = MasterId;
                    ChildId++;
                }
                _context.SaveChanges();

                var totalrecv = _context.ProcessRecvMaster.Where(x => x.PRMasterId == value.PRMasterId).Sum(x => x.PRRecvQty);
                var existing = _context.ProcessExecutionMaster.Find(value.PRMasterId);
                if (totalrecv >= existing.ProcessToQty)
                {
                    existing.IsProcessReceive = 1;
                    _context.ProcessExecutionMaster.Update(existing);
                }

                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ProcessRecvList")]
        public IActionResult ProcessRecvList([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessRecvMaster> query = _context.ViewProcessRecvMaster;
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));


                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.PRRecvDate >= PS.DateFrom && x.PRRecvDate <= PS.DateTo);
                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.PRRecvDate == PS.DateFrom);

                query = query.OrderByDescending(x => x.PRRecvDate).ThenByDescending(x => x.PRRecvId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetViewWIP")]
        public IActionResult GetViewWIP([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewWIP> query = _context.ViewWIP; //.Where(x=> (x.InQty-x.OutQty ) >0);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                query = query.OrderBy(x => x.MyDeliveryDate).ThenBy(x => x.PlanChildId);

                //query = query.OrderBy(x => x.BuyerId).ThenBy(x=> x.OrderMasterID).ThenBy(x=> x.OrderChildID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetProcessCharge")]
        //public IActionResult GetProcessCharge([FromBody]SearchModel sm)
        //{
        //    try
        //    {
        //        double processcharge = 0;
        //        if (sm.ProcessName == "Knotting")
        //        {
        //            processcharge = _context.ProcessChargeDetails.Where(x => x.FitemId == sm.FitemId && x.ProductSizeId == sm.SizeId).Select(x => x.KnottingCharge).FirstOrDefault();
        //        }
        //        else if (sm.ProcessName == "Tooling")
        //        {
        //            processcharge = _context.ProcessChargeDetails.Where(x => x.FitemId == sm.FitemId && x.ProductSizeId == sm.SizeId).Select(x => x.ToolingCharge).FirstOrDefault();
        //        }
        //        else
        //        {
        //            var costingobj = _context.CostingMaster.Where(x => x.FitemID == sm.FitemId && x.SizeId == sm.SizeId).LastOrDefault();
        //            if (costingobj == null) { return Ok(-1); }
        //            var costingelobj = _context.ViewCostingEL.Where(x => x.CostingID == costingobj.CostingID && x.CostingElementName == sm.ProcessName).LastOrDefault();
        //            processcharge = (costingelobj == null ? 0 : costingelobj.ElementAmount);
        //        }
        //        return Ok(processcharge);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetProcessCharge")]
        public IActionResult GetProcessCharge([FromBody]SearchModel sm)
        {
            try
            {
                double processcharge = 0;
                processcharge = _context.ProductProcessCharge.Where(x => x.FitemId == sm.FitemId && x.ProductSizeId == sm.SizeId && x.ProcessId == sm.ProcessId && x.ComponentId == sm.ComponentId).Select(x => x.ProcessCharge).FirstOrDefault();
                return Ok(processcharge);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetWPQtyForNextProcess")]
        public IActionResult GetWPQtyForNextProcess([FromBody]SearchModel sm)
        {
            try
            {
                int qty = 0;
                qty = _context.ViewWPQtyForNextProcess.Where(x => x.PlanChildId == sm.PlanChildId && x.ProcessID == sm.ProcessId).Select(x => x.QtyForNextProcess).FirstOrDefault();
                return Ok(qty);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("IsStartWPDone")]
        public IActionResult IsStartWPDone([FromBody]SearchModel sm)
        {
            try
            {
                int StartWPStaus = 0;
                var obj = _context.ProcessExecutionMaster.Where(x => x.PlanChildId == sm.PlanChildId).FirstOrDefault();
                if (obj == null)
                    StartWPStaus = 0;
                else
                    StartWPStaus = 1;
                return Ok(StartWPStaus);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProcessBalQty")]
        public IActionResult GetProcessBalQty([FromBody]SearchModel sm)
        {
            try
            {
                //var ProcessBalQty = _context.Database.ExecuteSqlCommand("SELECT dbo.FuncProcessBalQty("+sm.PlanChildId+","+sm.ComponentId+","+sm.FitemId+","+sm.SizeId+") AS BalanceQty");
                //return Ok(ProcessBalQty);
                var ProcessBalQty = _context.ViewProcessExecution
                                    .Where(x => x.PlanChildId == sm.PlanChildId && x.Comp_Id == sm.ComponentId && x.FitemId == sm.FitemId && x.SizeId == sm.SizeId)
                                    .FirstOrDefault();
                return Ok(ProcessBalQty);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetContractorBalance/{contractorid}")]
        public IActionResult GetContractorBalance(long contractorid)
        {
            try
            {
                var data = _context.ViewContractorBalance.Where(x => x.ContractorID == contractorid).SingleOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutReview/{PRMasterId}")]
        public IActionResult PutReview(long PRMasterId)
        {
            try
            {
                var existing = _context.ProcessExecutionMaster.Find(PRMasterId);
                existing.IsRead = 1;
                _context.ProcessExecutionMaster.Update(existing);

                _context.SaveChanges();
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutWPProcessCharge/{PRMasterId}/{ProcessCharge}")]
        public IActionResult PutWPProcessCharge(long PRMasterId, double ProcessCharge)
        {
            try
            {
                var existing = _context.ProcessExecutionMaster.Find(PRMasterId);
                if (existing != null)
                {
                    //var obj = _context.WorkPlanChild.Find(existing.PlanChildId);
                    //if (obj != null)
                    //{
                    //    existing.ProcessAmount = ProcessCharge * obj.PlanQty;
                    //}
                    existing.ProcessAmount = ProcessCharge * existing.ProcessToQty;
                    existing.ProcessCharge = ProcessCharge;
                    _context.ProcessExecutionMaster.Update(existing);

                    _context.SaveChanges();
                    var result = "Record Update";
                    return Ok(result);
                }
                else
                {
                    return Ok("PutWPProcessCharge fail.");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewContractorPaymentId")]
        public IActionResult GetNewContractorPaymentId()
        {
            try
            {
                long id = 0;
                var last = _context.ContractorPayment.OrderBy(x => x.ContractorPaymentId).LastOrDefault();
                id = (last == null ? 0 : last.ContractorPaymentId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewContractorPaymentChildId")]
        public IActionResult GetNewContractorPaymentChildId()
        {
            try
            {
                long id = 0;
                var last = _context.ContractorPaymentChild.OrderBy(x => x.ContractorPaymentChildId).LastOrDefault();
                id = (last == null ? 0 : last.ContractorPaymentChildId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewContractorAdvanceId")]
        public IActionResult GetNewContractorAdvanceId()
        {
            try
            {
                long id = 0;
                var last = _context.ContractorAdvanceDetails.OrderBy(x => x.ContractorAdvanceId).LastOrDefault();
                id = (last == null ? 0 : last.ContractorAdvanceId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostContractorPayment")]
        public IActionResult PostContractorPayment([FromBody]ContractorPayment value)
        {
            try
            {
                int cpsno = _context.ContractorPayment.Where(x => x.SessionYear == sort_session).Max(x => x.CPSNo);
                cpsno++;
                string cpno = "CP/" + sort_session + "/" + cpsno;
                value.CPSNo = cpsno;
                value.CPNo = cpno;
                _context.ContractorPayment.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostContractorAdvance")]
        public IActionResult PostContractorAdvance([FromBody]ContractorAdvanceDetails value)
        {
            try
            {
                _context.ContractorAdvanceDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetContractorPaymentList")]
        public IActionResult GetContractorPaymentList([FromBody]WorkPlanSearch WS)
        {
            try
            {
                IQueryable<ViewContractorPayment> query = _context.ViewContractorPayment;

                //if (!string.IsNullOrEmpty(PS.PlanNo))
                //    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (WS.ProcessId > 0)
                    query = query.Where(x => x.ProcessId == WS.ProcessId);
                //if (PS.BuyerId > 0)
                //    query = query.Where(x => x.BuyerId == PS.BuyerId);
                //if (!string.IsNullOrEmpty(PS.Code))
                //    query = query.Where(x => x.Code.Contains(PS.Code));

                if (WS.ContractorId > 0)
                    query = query.Where(x => x.ContractorId == WS.ContractorId);

                if (WS.ChkDateFromInt == 1 && WS.ChkDateToInt == 1)
                    query = query.Where(x => x.PaidDate >= (WS.DateFrom) && x.PaidDate <= WS.DateTo);
                if (WS.ChkDateFromInt == 1 && WS.ChkDateToInt == 0)
                    query = query.Where(x => x.PaidDate == (WS.DateFrom));

                query = query.OrderByDescending(x => x.ContractorPaymentId).ThenBy(x => x.SNo);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetContractorAdvanceList")]
        public IActionResult GetContractorAdvanceList([FromBody]WorkPlanSearch WS)
        {
            try
            {
                IQueryable<ViewContractorAdvanceDetails> query = _context.ViewContractorAdvanceDetails;

                //if (!string.IsNullOrEmpty(PS.PlanNo))
                //    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo)); 

                if (WS.ContractorId > 0)
                    query = query.Where(x => x.ContractorId == WS.ContractorId);
                if (WS.ChkDateFromInt == 1 && WS.ChkDateToInt == 1)
                    query = query.Where(x => x.ContractorAdvanceDate >= (WS.DateFrom) && x.ContractorAdvanceDate <= WS.DateTo);
                if (WS.ChkDateFromInt == 1 && WS.ChkDateToInt == 0)
                    query = query.Where(x => x.ContractorAdvanceDate == (WS.DateFrom));

                query = query.OrderByDescending(x => x.ContractorAdvanceId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetContractorPaymentPrint/{contractorpaymentid}")]
        public IQueryable<ViewContractorPayment> GetContractorPaymentPrint(long contractorpaymentid)
        {
            return _context.ViewContractorPayment.Where(x => x.ContractorPaymentId == contractorpaymentid).OrderBy(x => x.BuyerCode).ThenBy(x => x.OrderNo).AsQueryable();
        }

        [HttpGet]
        [Route("GetContractorAdvancePrint/{contractoradvanceid}")]
        public IQueryable<ViewContractorAdvanceDetails> GetContractorAdvancePrint(long contractoradvanceid)
        {
            return _context.ViewContractorAdvanceDetails.Where(x => x.ContractorAdvanceId == contractoradvanceid).AsQueryable();
        }

        [HttpPost]
        [Route("GetViewWorPlan")]
        public IActionResult GetViewWorPlan([FromBody]WorkPlanSearch WP)
        {
            try
            {
                IQueryable<ViewWorkPlanChild> query = _context.ViewWorkPlanChild;
                if (WP.WorkPlanStatusId > 0)
                {
                    //if (WP.WorkPlanStatusId == 1)
                    //    query = query.Where(x => x.CurrentStock == WP.RMCategory_ID); //All
                    //else 
                    if (WP.WorkPlanStatusId == 2)
                        query = query.Where(x => x.IsPending == 0); // Pending 
                    else if (WP.WorkPlanStatusId == 3)
                        query = query.Where(x => x.IsPending == 1); // Complete
                }

                if (!string.IsNullOrEmpty(WP.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(WP.PlanNo));
                if (!string.IsNullOrEmpty(WP.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(WP.OrderNo));
                if (!string.IsNullOrEmpty(WP.Code))
                    query = query.Where(x => x.Code.Contains(WP.Code));
                if (WP.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == WP.ContractorId);
                if (WP.PlanId > 0)
                    query = query.Where(x => x.PlanId == WP.PlanId);
                if (WP.ChkDateFromInt == 1 && WP.ChkDateToInt == 1)
                    query = query.Where(x => x.PlanDate >= (WP.DateFrom) && x.PlanDate <= WP.DateTo);
                if (WP.ChkDateFromInt == 1 && WP.ChkDateToInt == 0)
                    query = query.Where(x => x.PlanDate == (WP.DateFrom));

                query = query.OrderByDescending(x => x.PlanId).ThenBy(x => x.Code);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewTableId")]
        public IActionResult GetNewTableId()
        {
            try
            {
                long id = 0;
                var last = _context.TempTableDetails.OrderBy(x => x.TableId).LastOrDefault();
                id = (last == null ? 0 : last.TableId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostTempTableDetails")]
        public IActionResult PostTempTableDetails([FromBody]List<TempTableDetails> value)
        {
            try
            {
                var existing = _context.TempTableDetails.AsQueryable();
                foreach (var item in existing)
                {
                    _context.TempTableDetails.Remove(item);
                }
                foreach (var item in value)
                {
                    _context.TempTableDetails.Add(item);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpGet]
        [Route("GetProductionProcessPrint")]
        public IQueryable<ViewProcessExecution> GetProductionProcessPrint()
        {
            var existing = _context.TempTableDetails.Select(x => x.AnyId).AsQueryable();
            //return _context.ViewContractorAdvanceDetails.Where(x => existing.Contains(x.ContractorAdvanceId)).AsQueryable();
            return _context.ViewProcessExecution.Where(x => existing.Contains(x.PRMasterId)).AsQueryable();
        }

        [HttpPost]
        [Route("ProductListForProcessCharge")]
        public IActionResult ProductListForProcessCharge([FromBody]WorkPlanSearch PS)
        {
            try
            {
                IQueryable<ViewProductListForProcessCharge> query = _context.ViewProductListForProcessCharge.Where(x => x.Code.Contains(PS.Code)).OrderBy(x => x.Code);
                //if (!string.IsNullOrEmpty(PS.Code))
                //    query = query.Where(x => x.Code.Contains(PS.Code));
                //query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostProcessCharge")]
        public IActionResult PostProcessCharge([FromBody]List<ProcessChargeDetails> value)
        {
            try
            {
                long id = 0;
                var last = _context.ProcessChargeDetails.OrderBy(x => x.ProcessChargeId).LastOrDefault();
                id = (last == null ? 0 : last.ProcessChargeId) + 1;

                foreach (var item in value)
                {
                    if (item.ProcessChargeId <= 0)
                    {
                        item.ProcessChargeId = id;
                        _context.ProcessChargeDetails.Add(item);
                        id++;
                    }
                    else
                    {
                        var existing = _context.ProcessChargeDetails.Find(item.ProcessChargeId);
                        existing.KnottingCharge = item.KnottingCharge;
                        existing.ToolingCharge = item.ToolingCharge;
                        _context.ProcessChargeDetails.Update(existing);
                    }
                }

                ProcessChargeHistoryDetails _newchild;
                long ProcessChargeHistoryId = 0;
                var last_pchistory = _context.ProcessChargeHistoryDetails.OrderBy(x => x.ProcessChargeHistoryId).LastOrDefault();
                ProcessChargeHistoryId = (last_pchistory == null ? 0 : last_pchistory.ProcessChargeHistoryId) + 1;

                foreach (var item in value)
                {
                    var existinglastfitem = _context.ProcessChargeHistoryDetails.Where(x => x.FitemId == item.FitemId && x.ProductSizeId == item.ProductSizeId).LastOrDefault();

                    _newchild = new ProcessChargeHistoryDetails();
                    _newchild.ProcessChargeHistoryId = ProcessChargeHistoryId;
                    _newchild.FitemId = item.FitemId;
                    _newchild.ProductSizeId = item.ProductSizeId;
                    if (existinglastfitem == null) // for first time entry
                    {
                        _newchild.PreKnottingCharge = 0;
                        _newchild.PreToolingCharge = 0;
                    }
                    else
                    {
                        _newchild.PreKnottingCharge = existinglastfitem.NewKnottingCharge;
                        _newchild.PreToolingCharge = existinglastfitem.NewToolingCharge;
                    }
                    _newchild.NewKnottingCharge = item.KnottingCharge;
                    _newchild.NewToolingCharge = item.ToolingCharge;
                    _newchild.EntryDate = item.EntryDate;
                    _newchild.SessionYear = item.SessionYear;
                    _newchild.UserId = item.UserId;
                    _context.ProcessChargeHistoryDetails.Add(_newchild);
                    ProcessChargeHistoryId++;
                }

                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteWorkPlan/{planid}")]
        public IActionResult DeleteWorkPlan(long planid)
        {
            try
            {
                var existinginProcess = _context.ProcessExecutionMaster.Where(x => x.PlanId == planid).LastOrDefault();
                if (existinginProcess == null)
                {
                    var existing = _context.WorkPlanMaster
                              .Where(x => x.PlanId == planid)
                              .Include(x => x.WorkPlanChild)
                              .SingleOrDefault();

                    if (existing != null)
                    {
                        _context.WorkPlanMaster.Remove(existing);
                        _context.SaveChanges();
                        return Ok("Record Deleted");
                    }
                    else
                    {
                        return Ok("Not Found");
                    }
                }
                else
                {
                    return Ok("WorkPlan is in Process, Cannot Delete");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductDetail/{fitemid}")]
        public IActionResult GetProductDetail(int fitemid)
        {
            try
            {
                return Ok(_context.WorkPlanChild.Where(x => x.FitemId == fitemid).LastOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("FinishGoodsStockList")]
        public IActionResult FinishGoodsStockList([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessFinishGoodsStock> query = _context.ViewProcessFinishGoodsStock;

                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));

                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                query = query.OrderByDescending(x => x.PRMasterId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpPost]
        //[Route("GetProductionFlowStatus")]
        //public IActionResult GetProductionFlowStatus([FromBody]ProcessSearch PS)
        //{
        //    try
        //    {
        //        IQueryable<ViewWIP> query = _context.ViewWIP.Where(x => x.ProcessBal > 0);
        //        //IQueryable<ViewWIP> query = _context.ViewWIP;

        //        if (!string.IsNullOrEmpty(PS.Code))
        //            query = query.Where(x => x.Code.Contains(PS.Code));
        //        if (!string.IsNullOrEmpty(PS.OrderNo))
        //            query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
        //        if (PS.BuyerId > 0)
        //            query = query.Where(x => x.BuyerId == PS.BuyerId);
        //        if (PS.PlanId > 0)
        //            query = query.Where(x => x.PlanId == PS.PlanId);
        //        if (PS.ProcessID > 0)
        //            query = query.Where(x => x.ProcessID == PS.ProcessID);
        //        query = query.OrderBy(x => x.MyDeliveryDate).ThenBy(x => x.PlanChildId);

        //        //query = query.OrderBy(x => x.BuyerId).ThenBy(x=> x.OrderMasterID).ThenBy(x=> x.OrderChildID);
        //        var record = query.ToList();
        //        return Ok(record);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetProductionFlowStatus")]
        public IActionResult GetProductionFlowStatus([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewWIP> query = _context.ViewWIP.Where(x => (x.Qty) > 0);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);

                //if (PS.PRMasterId > 0)
                //    query = query.Where(x => x.PRMasterId == PS.PRMasterId);

                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);

                query = query.OrderBy(x => x.MyDeliveryDate).ThenBy(x => x.PlanChildId);

                //query = query.OrderBy(x => x.BuyerId).ThenBy(x=> x.OrderMasterID).ThenBy(x=> x.OrderChildID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetViewProcessBOM/{prmasterid}")]
        public IActionResult GetViewProcessBOM(long prmasterid)
        {
            try
            {
                var details = _context.ViewProcessBOM.Where(x => x.PRMasterId == prmasterid)
                    .OrderBy(x => x.Name);

                return Ok(details);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }

            //try
            //{
            //    var result = from c in _context.ViewWorkPlanBOM
            //                 where (c.PlanId == planid)
            //                 group c by new { c.CategoryName, c.SubCategoryName, c.Code, c.Full_Name, c.CostUnit, c.RItemID, c.CostUnitSName, c.Title, c.IssueQty, c.ReserveQty, c.RMStock } into g     //, c.Title
            //                 orderby g.Key.CategoryName, g.Key.SubCategoryName, g.Key.Full_Name
            //                 select new
            //                 {
            //                     CategoryName = g.Key.CategoryName,
            //                     SubCategoryName = g.Key.SubCategoryName,
            //                     Code = g.Key.Code,
            //                     Required = g.Sum(x => x.Required),
            //                     Full_Name = g.Key.Full_Name,
            //                     CostUnit = g.Key.CostUnit,
            //                     RItemID = g.Key.RItemID,
            //                     CostUnitSName = g.Key.CostUnitSName,
            //                     Title = g.Key.Title,
            //                     IssueQty = g.Key.IssueQty,
            //                     ReserveQty = g.Key.ReserveQty,
            //                     RMStock = g.Key.RMStock,
            //                 };
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return Ok(ex.InnerException);
            //}
        }

        [Route("GetProcessDetails/{prmasterid}")]
        public IActionResult GetProcessDetails(long prmasterid)
        {
            try
            {
                var details = _context.ViewProcessExecutionMaster
                        .Where(x => x.PRMasterId == prmasterid)
                        .FirstOrDefault();
                return Ok(details);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProcessItemList/{prmasterid}")]
        public IActionResult GetProcessItemList(long prmasterid)
        {
            try
            {
                var details = _context.ViewProcessExecutionMaster.Where(x => x.PRMasterId == prmasterid)
                    .OrderBy(x => x.Code);

                return Ok(details);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostProductProcessCharge")]
        public IActionResult PostProductProcessCharge([FromBody]List<ProductProcessCharge> value)
        {
            try
            {
                var obj = _context.ProductProcessCharge.Where(x => x.FitemId == value.First().FitemId).ToList();
                foreach (var v in obj)
                {
                    _context.Remove(v);
                    _context.SaveChanges();
                }

                long id = _context.ProductProcessCharge.Max(x => x.ProductProcessChargeId);
                id++;
                foreach (ProductProcessCharge onerecord in value)
                {
                    onerecord.ProductProcessChargeId = id;
                    _context.ProductProcessCharge.Add(onerecord);
                    _context.SaveChanges();
                    id++;
                }

                //ProcessChargeHistoryDetails _newchild;
                //long ProcessChargeHistoryId = 0;
                //var last_pchistory = _context.ProcessChargeHistoryDetails.OrderBy(x => x.ProcessChargeHistoryId).LastOrDefault();
                //ProcessChargeHistoryId = (last_pchistory == null ? 0 : last_pchistory.ProcessChargeHistoryId) + 1;

                //foreach (var item in value)
                //{
                //    var existinglastfitem = _context.ProcessChargeHistoryDetails.Where(x => x.FitemId == item.FitemId && x.ProductSizeId == item.ProductSizeId).LastOrDefault();

                //    _newchild = new ProcessChargeHistoryDetails();
                //    _newchild.ProcessChargeHistoryId = ProcessChargeHistoryId;
                //    _newchild.FitemId = item.FitemId;
                //    _newchild.ProductSizeId = item.ProductSizeId;
                //    if (existinglastfitem == null) // for first time entry
                //    {
                //        _newchild.PreKnottingCharge = 0;
                //        _newchild.PreToolingCharge = 0;
                //    }
                //    else
                //    {
                //        _newchild.PreKnottingCharge = existinglastfitem.NewKnottingCharge;
                //        _newchild.PreToolingCharge = existinglastfitem.NewToolingCharge;
                //    }
                //    _newchild.NewKnottingCharge = item.KnottingCharge;
                //    _newchild.NewToolingCharge = item.ToolingCharge;
                //    _newchild.EntryDate = item.EntryDate;
                //    _newchild.SessionYear = item.SessionYear;
                //    _newchild.UserId = item.UserId;
                //    _context.ProcessChargeHistoryDetails.Add(_newchild);
                //    ProcessChargeHistoryId++;
                //}

                //_context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductProcessCharge/{fitemid}")]
        public IActionResult GetProductProcessCharge(long fitemid)
        {
            try
            {
                var details = _context.ViewProductProcessCharge.Where(x => x.FitemId == fitemid).OrderBy(x => x.ProductSizeId).ThenBy(x => x.ProcessName);
                return Ok(details);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductionProcessDetails/{processid}")]
        public IActionResult GetProductionProcessDetails(int processid)
        {
            try
            {
                return Ok(_context.ProductionProcessDetails.Where(x => x.ProcessID == processid).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPost]
        [Route("GetSaleOrderWPStatus")]
        public IActionResult GetSaleOrderWPStatus([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewSaleOrderWPStatus> query = _context.ViewSaleOrderWPStatus;

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.WorkPlanStatusId == 2) // pending WP
                    query = query.Where(x => x.PlanId == 0);
                if (PS.WorkPlanStatusId == 3) // WP created
                    query = query.Where(x => x.PlanId > 0);
                query = query.OrderBy(x => x.MyDeliveryDate);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetWeeklyProduction")]
        public IActionResult GetWeeklyProduction([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessRecvMaster> query = _context.ViewProcessRecvMaster;
                DateTime StartDate, EndDate;
                if (PS.ReportType == 1)
                {
                    StartDate = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);
                    EndDate = DateTime.Now;
                }
                else
                {
                    DateTime date = DateTime.Now;
                    StartDate = date.AddDays(-(7 + ((int)date.DayOfWeek - 1 == -1 ? 6 : (int)date.DayOfWeek - 1)));
                    EndDate = date.AddDays(-(2 + ((int)date.DayOfWeek - 1 == -1 ? 6 : (int)date.DayOfWeek - 1)));
                }
                query = query.OrderByDescending(x => x.PRRecvDate).ThenBy(x => x.OrderNo).ThenBy(x => x.Code)
                .Where(x => x.PRRecvDate >= StartDate && x.PRRecvDate <= EndDate);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetMonthlyProduction")]
        public IActionResult GetMonthlyProduction([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProcessRecvMaster> query = _context.ViewProcessRecvMaster;
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);

                if (PS.MonthId > 0)
                    query = query.Where(x => x.PRRecvDate.Month == PS.MonthId);
                if (PS.YearName > 0)
                    query = query.Where(x => x.PRRecvDate.Year == PS.YearName);

                query = query.OrderByDescending(x => x.PRRecvDate).ThenBy(x => x.OrderNo).ThenBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetRunningProcessList")]
        public IActionResult GetRunningProcessList([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewRunningProcessReport> query = _context.ViewRunningProcessReport;

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));
                if (PS.ContractorId > 0)
                    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                if (PS.ChkProcessDateInt == 1)
                    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                if (PS.PRMasterId > 0)
                    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                if (PS.PlanChildId > 0)
                    query = query.Where(x => x.PlanChildId == PS.PlanChildId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);

                query = query.OrderByDescending(x => x.PRMasterId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetOrderStatus")]
        public IActionResult GetOrderStatus([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewOrderStatus> query = _context.ViewOrderStatus;

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (!string.IsNullOrEmpty(PS.SizePartyCode))
                    query = query.Where(x => x.SizePartyCode.Contains(PS.SizePartyCode));
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));
                //if (PS.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                //if (!string.IsNullOrEmpty(PS.ProcessNo))
                //    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                //if (PS.ChkProcessDateInt == 1)
                //    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                //if (PS.PRMasterId > 0)
                //    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                if (PS.PlanChildId > 0)
                    query = query.Where(x => x.PlanChildId == PS.PlanChildId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);

                if (PS.DeliveryDays > 0)
                    query = query.Where(x => x.MyDeliveryDate <= DateTime.Now.Date.AddDays(PS.DeliveryDays));

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 1)
                    query = query.Where(x => x.MyDeliveryDate >= (PS.DateFrom) && x.MyDeliveryDate <= PS.DateTo);

                if (PS.ChkDateFromInt == 1 && PS.ChkDateToInt == 0)
                    query = query.Where(x => x.MyDeliveryDate == (PS.DateFrom));

                query = query.OrderBy(x => x.OrderDate);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetViewProductProcess")]
        public IActionResult GetViewProductProcess([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ViewProductProcess> query = _context.ViewProductProcess;
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetProcessExecutionMaster")]
        public IActionResult GetProcessExecutionMaster([FromBody]ProcessSearch PS)
        {
            try
            {
                IQueryable<ProcessExecutionMaster> query = _context.ProcessExecutionMaster;
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeletePR/{PRMasterId}")]
        public IActionResult DeletePR(long PRMasterId)
        {
            try
            {
                var existinginProcess = _context.ProcessExecutionMaster.Where(x => x.PRMasterId == PRMasterId)
                                        .Include(x=> x.ProcessExecutionChild).ToList();

                if (existinginProcess != null)
                {
                    var existingRecv = _context.ProcessRecvMaster.Where(x => x.PRMasterId == PRMasterId).Include(x => x.ProcessRecvChild).ToList();                              
                    if (existingRecv.Count > 0)
                    {
                        return Ok("Process Recv , You can't delete this entry");                       
                    }
                    else
                    {
                        _context.ProcessExecutionMaster.RemoveRange(existinginProcess);
                        _context.SaveChanges();
                        return Ok("Record Deleted");
                    }
                }
                else
                {
                    return Ok("No record found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("BalProcessForSupervisorList")]
        public IActionResult BalProcessForSupervisorList([FromBody]ProcessSearch PS)
        {
            try
            {
                var contactorid = _context.UserAsSupervisorDetails.Where(x => x.UserId == PS.UserId).Select(x => x.ContractorID).ToList();
                IQueryable<ViewProcessExecutionMaster> query = _context.ViewProcessExecutionMaster.Where(x => x.IsProcessReceive == 0 && contactorid.Contains(x.ContractorID));

                query = query.Where(x => x.IsPending == 0); // Balance WP Only
                query = query.Where(x => x.ProcessID != 50); // not in dispatch

                if (!string.IsNullOrEmpty(PS.PlanNo))
                    query = query.Where(x => x.PlanNo.Contains(PS.PlanNo));
                if (PS.ProcessID > 0)
                    query = query.Where(x => x.ProcessID == PS.ProcessID);
                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                //if (PS.ContractorId > 0)
                //    query = query.Where(x => x.ContractorID == PS.ContractorId);
                if (!string.IsNullOrEmpty(PS.OrderNo))
                    query = query.Where(x => x.OrderNo.Contains(PS.OrderNo));
                if (!string.IsNullOrEmpty(PS.ProcessNo))
                    query = query.Where(x => x.PRNo.Contains(PS.ProcessNo));
                if (PS.ChkProcessDateInt == 1)
                    query = query.Where(x => x.PRDate == (PS.ProcessDate));
                //if (!string.IsNullOrEmpty(PS.Comp_Name))
                //    query = query.Where(x => x.Comp_Name.Contains(PS.Comp_Name));

                if (PS.PRMasterId > 0)
                    query = query.Where(x => x.PRMasterId == PS.PRMasterId);
                if (PS.PlanChildId > 0)
                    query = query.Where(x => x.PlanChildId == PS.PlanChildId);
                if (PS.PlanId > 0)
                    query = query.Where(x => x.PlanId == PS.PlanId);

                query = query.OrderByDescending(x => x.NoofDays);
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
