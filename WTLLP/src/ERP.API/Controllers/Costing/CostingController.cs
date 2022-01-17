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
    public class CostingController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public CostingController(IMemoryCache memoryCache, DBContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(360);
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ViewShowCosting> Get()
        {
            return _context.ViewShowCosting.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {

                return Ok(_context.ViewShowCosting.Where(x => x.CostingID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("{id}")]
        //public IActionResult Get(long id)
        //{
        //    try
        //    {
        //        return Ok(_context.ViewShowCosting.Where(x => x.CostingID == id).FirstOrDefault());
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewCostingID")]
        public IActionResult GetNewCostingID()
        {
            try
            {
                long CostingID = 0;
                var existing = _context.CostingMaster.OrderBy(x => x.CostingID).LastOrDefault();
                CostingID = (existing == null ? 0 : existing.CostingID) + 1;
                return Ok(CostingID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCostingRMID")]
        public IActionResult GetNewCostingRMID()
        {
            try
            {
                long CostingRMID = 0;
                var existing = _context.CostingRM.OrderBy(x => x.CostingRMID).LastOrDefault();
                CostingRMID = (existing == null ? 0 : existing.CostingRMID) + 1;
                return Ok(CostingRMID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCostingElID")]
        public IActionResult GetNewCostingElID()
        {
            try
            {
                long CostingElID = 0;
                var existing = _context.CostingEl.OrderBy(x => x.CostingElID).LastOrDefault();
                CostingElID = (existing == null ? 0 : existing.CostingElID) + 1;
                return Ok(CostingElID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingRM/{id}")]
        public IQueryable<ViewCostingRM> GetCostingRM(long id)
        {
            return _context.ViewCostingRM.Where(x => x.CostingID == id)
                                    .OrderBy(x => x.SerialNo).AsQueryable();
        }

        [Route("GetViewCostingSizeRM/{costingid}")]
        public IQueryable<ViewCostingSizeRM> GetViewCostingSizeRM(long costingid)
        {
            return _context.ViewCostingSizeRM.Where(x => x.CostingID == costingid)
                                    .OrderBy(x => x.SerialNo).AsQueryable();
        }

        [Route("GetViewCostingSizeRM/{costingid}/{sizeid}")]
        public IQueryable<ViewCostingSizeRM> GetViewCostingSizeRM(long costingid, int sizeid)
        {
            return _context.ViewCostingSizeRM.Where(x => x.CostingID == costingid && x.SizeId == sizeid)
                                    .OrderBy(x => x.SerialNo).AsQueryable();
        }

        [Route("GetCostingEL/{id}")]
        public IQueryable<ViewCostingEL> GetCostingEL(long id)
        {
            return _context.ViewCostingEL.Where(x => x.CostingID == id)
                                    .OrderBy(x => x.SerialNo).AsQueryable();
        }

        [HttpPost]
        public IActionResult Post([FromBody]CostingMaster value)
        {
            try
            {
                var CostingSizeRMId = _context.CostingSizeRM.Max(x => x.CostingSizeRMId);
                CostingSizeRMId++;
                foreach (var record in value.CostingSizeRM)
                {
                    record.CostingSizeRMId = CostingSizeRMId;
                    CostingSizeRMId++;
                }
                _context.CostingMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]CostingMaster value)
        {
            try
            {
                var existingCosting = _context.CostingMaster
                        .Where(x => x.CostingID == value.CostingID)
                        .Include(x => x.CostingRM)
                        .Include(x => x.CostingEl)
                        .Include(x => x.CostingCurrencyDetails)
                        .Include(x => x.CostingSizeRM)
                        .SingleOrDefault();

                if (existingCosting != null)
                {
                    // Update parent
                    value.CreationDate = existingCosting.CreationDate;
                    _context.Entry(existingCosting).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var ExistingCostingRM in existingCosting.CostingRM.ToList())
                    {
                        _context.CostingRM.Remove(ExistingCostingRM);
                    }

                    foreach (var ExistingCostingEL in existingCosting.CostingEl.ToList())
                    {
                        _context.CostingEl.Remove(ExistingCostingEL);
                    }

                    foreach (var ExistingCostingCurrency in existingCosting.CostingCurrencyDetails.ToList())
                    {
                        _context.CostingCurrencyDetails.Remove(ExistingCostingCurrency);
                    }

                    // Update and Insert children
                    foreach (var childRM in value.CostingRM)
                    {
                        // Insert child 1
                        CostingRM newChild = new CostingRM();
                        newChild.CostingRMID = childRM.CostingRMID;
                        newChild.CostingID = childRM.CostingID;
                        newChild.RItemID = childRM.RItemID;
                        newChild.RMQty = childRM.RMQty;
                        newChild.CWAS = childRM.CWAS;
                        newChild.CAfterWAS = childRM.CAfterWAS;
                        newChild.RMPrice = childRM.RMPrice;
                        newChild.RMCAmount = childRM.RMCAmount;
                        newChild.BOMWAS = childRM.BOMWAS;
                        newChild.BOMAfterWas = childRM.BOMAfterWas;
                        newChild.RMBAmount = childRM.RMBAmount;
                        newChild.RMPrice = childRM.RMPrice;
                        newChild.SerialNo = childRM.SerialNo;
                        newChild.IsComponent = childRM.IsComponent;
                        newChild.SupplierId = childRM.SupplierId;
                        //newChild.ProcessID = childRM.ProcessID;

                        existingCosting.CostingRM.Add(newChild);
                    }

                    foreach (var childEL in value.CostingEl)
                    {
                        // Insert child 2
                        CostingEl newChild = new CostingEl();
                        newChild.CostingElID = childEL.CostingElID;
                        newChild.CostingID = childEL.CostingID;
                        newChild.CostingElementID = childEL.CostingElementID;
                        newChild.Percent = childEL.Percent;
                        newChild.ElementAmount = childEL.ElementAmount;
                        newChild.SerialNo = childEL.SerialNo;
                        existingCosting.CostingEl.Add(newChild);
                    }

                    foreach (var costingcurrency in value.CostingCurrencyDetails)
                    {
                        existingCosting.CostingCurrencyDetails.Add(costingcurrency);
                    }

                    #region CostingSizeRM
                    //update CostingSizeRM only few row match with item sizeid
                    //CostingSizeRMId recv from web, match with api then update rm values
                    foreach (var ExistingSizeRM in existingCosting.CostingSizeRM.ToList())
                    {
                        foreach (var sizerm in value.CostingSizeRM)
                        {
                            if (ExistingSizeRM.CostingSizeRMId == sizerm.CostingSizeRMId)
                            {
                                ExistingSizeRM.RMPrice = sizerm.RMPrice;
                                ExistingSizeRM.CWAS = sizerm.CWAS;
                                ExistingSizeRM.CAfterWAS = sizerm.CAfterWAS;
                                ExistingSizeRM.RMCAmount = sizerm.RMCAmount;
                                ExistingSizeRM.BOMWAS = sizerm.BOMWAS;
                                ExistingSizeRM.BOMAfterWas = sizerm.BOMAfterWas;
                                ExistingSizeRM.RMBAmount = sizerm.RMBAmount;
                            }
                        }
                    }
                    #endregion

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
        public IActionResult CheckDuplicate(long value)
        {
            try
            {
                var existing = _context.CostingMaster.Where(x => x.FitemID == value).FirstOrDefault<CostingMaster>();
                if (existing != null)
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

        [Route("GetCostingId/{fitemid}")]
        public IActionResult GetCostingId(long fitemid)
        {
            try
            {
                var id = _context.ViewShowCosting.Where(x => x.FitemID == fitemid).SingleOrDefault();
                if (id == null)
                {
                    return Ok(0);
                }
                else
                {
                    return Ok(id.CostingID);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewShowCostingSearch")]
        public IQueryable<ViewShowCosting> ViewShowCostingSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {

                var obj = _context.ViewShowCosting.Where(x => x.FormType == 2)
                                       .OrderByDescending(x => x.CostingID).AsQueryable();
                return obj;
            }
            else
            {
                var obj = _context.ViewShowCosting
                                .Where(x => (x.FormType == 2) && (x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search)))
                                .OrderByDescending(x => x.CostingID).AsQueryable();
                return obj;
            }
        }

        [HttpPost]
        [Route("CostingList")]
        public IActionResult CostingList([FromBody]CostingSearch CS)
        {
            try
            {
                IQueryable<ViewShowCosting> query = _context.ViewShowCosting.Where(x => x.FormType == 2);

                if (CS.BuyerID > 0)
                    query = query.Where(x => x.BuyerId == CS.BuyerID);
                if (CS.ProductCategoryID > 0)
                    query = query.Where(x => x.ProductCategoryID == CS.ProductCategoryID);
                if (CS.ProductSubCategoryID > 0)
                    query = query.Where(x => x.ProductSubCategoryID == CS.ProductSubCategoryID);
                if (!string.IsNullOrEmpty(CS.Code))
                    query = query.Where(x => x.Code.Contains(CS.Code));
                if (CS.ChkCreationDateInt == 1)
                    query = query.Where(x => x.CreationDate == (CS.CreationDate));

                if (CS.ChkUpdationDateFromInt == 1 && CS.ChkUpdationDateToInt == 1)
                    query = query.Where(x => x.UpdationDate >= (CS.UpdationDateFrom) && x.UpdationDate <= CS.UpdationDateTo);
                if (CS.ChkUpdationDateFromInt == 1 && CS.ChkUpdationDateToInt == 0)
                    query = query.Where(x => x.UpdationDate == (CS.UpdationDateFrom));

                query = query.OrderByDescending(x => x.CostingID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("CostingPendingReport")]
        public IActionResult CostingPendingReport([FromBody]CostingSearch CS)
        {
            try
            {
                IQueryable<ViewCostingPendingReport> query = _context.ViewCostingPendingReport;

                if (CS.BuyerID > 0)
                    query = query.Where(x => x.BuyerId == CS.BuyerID);
                if (CS.ProductCategoryID > 0)
                    query = query.Where(x => x.ProductCategoryID == CS.ProductCategoryID);
                if (CS.ProductSubCategoryID > 0)
                    query = query.Where(x => x.ProductSubCategoryID == CS.ProductSubCategoryID);
                if (!string.IsNullOrEmpty(CS.Code))
                    query = query.Where(x => x.Code.Contains(CS.Code));
                //if (CS.ChkCreationDateInt == 1)
                //    query = query.Where(x => x.CreationDate == (CS.CreationDate));

                query = query.OrderBy(x => x.ProductCategoryName)
                    .ThenBy(x => x.ProductSubCategoryName)
                    .ThenBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("ViewShowCosting")]
        public IQueryable<ViewShowCosting> ViewShowCosting()
        {
            return _context.ViewShowCosting.Where(x => x.FormType == 2)
                                    .OrderByDescending(x => x.CostingID).AsQueryable();
        }

        [Route("GetOrderBOM/{ordermasterid}")]
        public IActionResult GetOrderBOM(long ordermasterid)
        {
            try
            {
                var OrderBOM = _context.ViewOrderBOM.Where(x => x.OrderMasterID == ordermasterid)
                                        .OrderBy(x => x.Code).ToList();
                //if (OrderBOM == null)
                //{
                //    return Ok(0);
                //}
                //else
                //{
                return Ok(OrderBOM);
                //}
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetOrderBOMSum/{ordermasterid}")]
        public IActionResult GetOrderBOMSum(long ordermasterid)
        {
            try
            {
                var OrderBOM = _context.ViewOrderBOMSUM.Where(x => x.OrderMasterID == ordermasterid)
                                             .ToList();
                //if (OrderBOM == null)
                //{
                //    return Ok(0);
                //}
                //else
                //{
                return Ok(OrderBOM);
                //}
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteMaterial/{costingid}")]
        public IActionResult DeleteMaterial(long costingid)
        {
            try
            {
                var existingMaterial = _context.CostingMaster
                       .Where(x => x.CostingID == costingid)
                       .Include(x => x.CostingRM)
                       .Include(x => x.CostingEl)
                       .Include(x => x.CostingRMComponent)
                       .Include(x => x.CostingSizeRM)
                       .SingleOrDefault();
                if (existingMaterial != null)
                {
                    _context.CostingMaster.Remove(existingMaterial);
                    _context.SaveChanges();
                    return Ok("Record Deleted");
                }
                else
                {
                    return Ok("Not Found");
                }
                //var existingMaterial = _context.CostingMaster.Where(x => x.CostingID == costingid).FirstOrDefault();
                //var existingMaterialRM = _context.CostingRM.Where(x => x.CostingID == costingid).FirstOrDefault();
                //var existingMaterialEL = _context.CostingMaster.Where(x => x.CostingID == costingid && x.FormType == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetItemBOMDetails/{id}")]
        public IQueryable<ViewItemBOM> GetItemBOMDetails(long id)
        {
            var viewitembom = _context.ViewItemBOM.Where(x => x.CostingID == id)
                                    .OrderBy(x => x.ProductCategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                                    .AsQueryable();
            return viewitembom;
        }

        [Route("DeleteTempPOChild")]
        public IActionResult DeleteTempPOChild()
        {
            try
            {
                var existingtpochild = _context.TempPOChild.AsQueryable();
                if (existingtpochild != null)
                {
                    foreach (var tpochild in existingtpochild)
                    {
                        _context.TempPOChild.Remove(tpochild);
                    }
                    //_context.TempPOChild.Remove(existingtpochild);
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

        [Route("SaveTempPOChild")]
        public IActionResult SaveTempPOChild([FromBody]List<TempPOChild> value)
        {
            try
            {
                foreach (var tpochild in value)
                {
                    _context.TempPOChild.Add(tpochild);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetTempPOChild")]
        public IQueryable<TempPOChild> GetTempPOChild()
        {
            return _context.TempPOChild.AsQueryable();
        }

        //CostingCurrencyDetails
        [Route("GetNewCostingCurrencyId")]
        public IActionResult GetNewCostingCurrencyId()
        {
            try
            {
                long CostingCurrId = 0;
                var existingCostingCurr = _context.CostingCurrencyDetails.OrderBy(x => x.CostingCurrencyId).LastOrDefault();
                CostingCurrId = (existingCostingCurr == null ? 0 : existingCostingCurr.CostingCurrencyId) + 1;
                return Ok(CostingCurrId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostCurrDetails")]
        public IQueryable<ViewCostingCurrencyDetails> GetCostCurrDetails()
        {
            return _context.ViewCostingCurrencyDetails.OrderBy(x => x.RowID).AsQueryable();
        }

        [Route("GetCostCurrEdit/{id}")]
        public IActionResult GetCostCurrEdit(long id)
        {
            var costcurredit = _context.ViewCostingCurrencyEdit.Where(x => x.CostingId == id)
                                    .OrderBy(x => x.CostingId)
                                    .ThenBy(x => x.SerialNo)
                                    .ToList();
            return Ok(costcurredit);
        }

        //[Route("GetCostForPrint/{id}")]
        //public IQueryable<ViewCostingRM> GetCostForPrint(long id)
        //{
        //    return _context.ViewCostingRM.Where(x => x.CostingID == id).AsQueryable();
        //}

        [Route("GetCostForPrint/{id}")]
        public IQueryable<ViewCostingRMPrint> GetCostForPrint(long id)
        {
            return _context.ViewCostingRMPrint.Where(x => x.CostingID == id)
                                .OrderBy(x => x.DisplayOrder).AsQueryable();
        }

        [Route("GetViewCostingPendingReport")]
        public IActionResult GetViewCostingPendingReport(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewCostingPendingReport.OrderByDescending(x => x.FitemId));
                else
                    return Ok(_context.ViewCostingPendingReport
                        .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search))
                        .OrderByDescending(x => x.FitemId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingPending/{id}")]
        public IActionResult GetCostingPending(long id)
        {
            try
            {
                return Ok(_context.ViewCostingPendingReport.Where(x => x.FitemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSizeForCasting/{fitemid}")]
        public IActionResult GetSizeForCasting(long fitemid)
        {
            try
            {
                var SizeId = _context.CostingMaster.Where(x => x.FitemID == fitemid).Select(x => x.SizeId).ToList();
                object List;
                List = _context.ViewProductSize
                                    .Where(x => x.FitemId == fitemid && SizeId.Contains(x.SizeId))
                                    .OrderBy(c => c.SizeId)
                                    .Select(x => new { Id = x.SizeId, Value = x.SizeName }).ToList();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSizeForCopy/{fitemid}")]
        public IActionResult GetSizeListForCopy(long fitemid)
        {
            try
            {
                var SizeId = _context.CostingMaster.Where(x => x.FitemID == fitemid).Select(x => x.SizeId).ToList();
                object List;
                List = _context.ViewProductSize
                                    .Where(x => x.FitemId == fitemid && !SizeId.Contains(x.SizeId))
                                    .OrderBy(x => x.SizeId)
                                    .Select(x => new { Id = x.SizeId, Value = x.SizeName }).ToList();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSizeForAnlysis/{fitemid}")]
        public IActionResult GetSizeForAnlysis(long fitemid)
        {
            try
            {
                //var SizeId = _context.CostingMaster.Where(x => x.FitemID == fitemid && x.FormType == 2).Select(x => x.SizeId).ToList();
                //object List;
                //List = _context.ViewProductSize
                //                    .Where(x => x.FitemId == fitemid && !SizeId.Contains(x.SizeId))
                //                    .OrderBy(c => c.SizeId)
                //                    .Select(x => new { Id = x.Size1Id, Value = x.SizeName }).ToList();
                //return Ok(List);

                //var SizeId = _context.CostingMaster.Where(x => x.FitemID == fitemid && x.FormType == 2).Select(x => x.SizeId).ToList();
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

        [Route("GetCostingId/{FitemId}/{sizeid}")]
        public IActionResult GetCostingId(long FitemId, int sizeid)
        {
            try
            {
                long costingid = 0;
                var obj = _context.CostingMaster.Where(x => x.FitemID == FitemId && x.SizeId == sizeid).FirstOrDefault();
                costingid = (obj == null ? 0 : obj.CostingID);
                return Ok(costingid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingRMIsCompFilter/{id}/{ritemid}")]
        public IQueryable<ViewCostingRM> GetCostingRMIsCompFilter(long id, long ritemid)
        {
            if (ritemid > 0)
            {
                return _context.ViewCostingRM.Where(x => x.CostingID == id && x.IsComponent == 1 && x.RItemID == ritemid)
                                       .OrderBy(x => x.SerialNo).AsQueryable();
            }
            else
            {
                return _context.ViewCostingRM.Where(x => x.CostingID == id && x.IsComponent == 1)
                                       .OrderBy(x => x.SerialNo).AsQueryable();
            }
        }

        [Route("GetCompGroupList")]
        public IActionResult GetCompGroupList()
        {
            try
            {
                var list = _context.ComponentGroupDetails.OrderBy(x => x.CompGroupName)
                    .Select(x => new { id = x.CompGroupId, value = x.CompGroupName }).OrderBy(x => x.id).ToList();
                return Ok(list);
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

        [Route("GetComponentList/{id}")]
        public IActionResult GetComponentList(int id)
        {
            try
            {
                var list = _context.ComponentDetails.Where(x => x.CompGroupId == id)
                    .OrderBy(x => x.CompName)
                    .Select(x => new { id = x.CompId, value = x.CompName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCostingRMComponentID")]
        public IActionResult GetNewCostingRMComponentID()
        {
            try
            {
                long CostingRMComponentID = 0;
                var existing = _context.CostingRMComponent.OrderBy(x => x.CostingRMComponentID).LastOrDefault();
                CostingRMComponentID = (existing == null ? 0 : existing.CostingRMComponentID) + 1;
                return Ok(CostingRMComponentID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingRMComponent/{CostingId}/{RitemId}")]
        public IActionResult GetCostingRMComponent(long CostingId, long RitemId)
        {
            try
            {
                var obj2 = _context.ViewCostingRMComponent
                                 .Where(x => x.CostingID == CostingId && x.RitemId == RitemId)
                                 .OrderBy(x => x.SerialNo)
                                 .ToList();
                return Ok(obj2);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpPost]
        //[Route("SaveComponent")]
        //public IActionResult SaveComponent([FromBody]List<CostingRMComponent> value)
        //{
        //    try
        //    {
        //        double totalarea_sqinch = 0;
        //        double totalarea_sqft = 0;

        //        var existingCostingRMComp = _context.CostingRMComponent
        //                .Where(x => x.CostingID == value[0].CostingID && x.RitemId == value[0].RitemId)
        //                .ToList();

        //        if (existingCostingRMComp.Count > 0)
        //        {
        //            // Delete children 
        //            foreach (var ExistingCostingRMComponent in existingCostingRMComp)
        //            {
        //                _context.CostingRMComponent.Remove(ExistingCostingRMComponent);
        //            }
        //            _context.SaveChanges();
        //        }

        //        foreach (var childRMComponent in value)
        //        {
        //            // Insert child 2
        //            CostingRMComponent newChild = new CostingRMComponent();
        //            newChild.CostingRMComponentID = childRMComponent.CostingRMComponentID;
        //            newChild.CostingID = childRMComponent.CostingID;
        //            newChild.RitemId = childRMComponent.RitemId;
        //            newChild.CompID = childRMComponent.CompID;
        //            newChild.Length = childRMComponent.Length;
        //            newChild.Breadth = childRMComponent.Breadth;
        //            newChild.Height = childRMComponent.Height;
        //            newChild.CompQty = childRMComponent.CompQty;
        //            newChild.Area = childRMComponent.Area;
        //            newChild.SerialNo = childRMComponent.SerialNo;

        //            totalarea_sqinch += childRMComponent.Area;

        //            _context.CostingRMComponent.Add(newChild);
        //        } 

        //        totalarea_sqft = totalarea_sqinch / 144;
        //        var existingCostingRM = _context.CostingRM
        //             .Where(x => x.CostingID == value[0].CostingID && x.RItemID == value[0].RitemId)
        //             .SingleOrDefault();

        //        existingCostingRM.RMQty = Math.Round(totalarea_sqft, 3); 

        //        _context.SaveChanges();
        //        return Ok("Record Saved");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        [Route("SaveComponent")]
        public IActionResult SaveComponent([FromBody]CostingMaster value)
        {
            try
            {
                var ritemid = value.CostingRMComponent.FirstOrDefault().RitemId;
                var existingCosting = _context.CostingMaster
                        .Where(x => x.CostingID == value.CostingID)
                        .Include(x => x.CostingRMComponent)
                        .SingleOrDefault();

                if (existingCosting != null)
                {
                    // Update parent
                    //_context.Entry(existingCosting).CurrentValues.SetValues(value);
                    //existingCostingRM.RMQty = value.RMQty;

                    // Delete children 
                    foreach (var ExistingCostingRMComponent in existingCosting.CostingRMComponent.ToList())
                    {
                        if (ExistingCostingRMComponent.RitemId == ritemid)
                        {
                            _context.CostingRMComponent.Remove(ExistingCostingRMComponent);
                        }
                    }

                    long rid = 0;
                    // Update and Insert children
                    foreach (var childRMComponent in value.CostingRMComponent)
                    {
                        // Insert child 2
                        CostingRMComponent newChild = new CostingRMComponent();
                        newChild.CostingRMComponentID = childRMComponent.CostingRMComponentID;
                        newChild.CostingID = childRMComponent.CostingID;
                        newChild.RitemId = childRMComponent.RitemId;
                        newChild.CompID = childRMComponent.CompID;
                        newChild.CompGroupId = childRMComponent.CompGroupId;
                        newChild.Length = childRMComponent.Length;
                        newChild.Breadth = childRMComponent.Breadth;
                        newChild.Height = childRMComponent.Height;
                        newChild.CompQty = childRMComponent.CompQty;
                        newChild.Area = childRMComponent.Area;
                        newChild.SerialNo = childRMComponent.SerialNo;
                        newChild.Remark = childRMComponent.Remark;
                        newChild.Photo = childRMComponent.Photo;
                        rid = childRMComponent.RitemId;
                        existingCosting.CostingRMComponent.Add(newChild);
                    }

                    var existingCostingRM = _context.CostingRM
                     .Where(x => x.CostingID == value.CostingID && x.RItemID == rid)
                     .SingleOrDefault();

                    //existingCostingRM.RMQty = Math.Round(totalarea_sqft, 3);
                    existingCostingRM.RMQty = value.RMTotal;

                    _context.SaveChanges();
                }
                return Ok("Record Saved");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostNewMaterial")]
        public IActionResult PostNewMaterial([FromBody]CostingMaster value)
        {
            try
            {
                var CostingSizeRMId = _context.CostingSizeRM.Max(x => x.CostingSizeRMId);
                CostingSizeRMId++;
                foreach (var record in value.CostingSizeRM)
                {
                    record.CostingSizeRMId = CostingSizeRMId;
                    CostingSizeRMId++;
                }

                _context.CostingMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutNewMaterial")]
        public IActionResult PutNewMaterial([FromBody]CostingMaster value)
        {
            try
            {
                var existingCosting = _context.CostingMaster
                        .Where(x => x.CostingID == value.CostingID)
                        .Include(x => x.CostingRMComponent)
                        .Include(x => x.CostingRM)
                        .Include(x => x.CostingSizeRM)
                        .SingleOrDefault();

                if (existingCosting != null)
                {
                    //value.CreationDate = existingCosting.CreationDate;
                    //_context.Entry(existingCosting).CurrentValues.SetValues(value);
                    //existingCosting.FormType = 1;
                    existingCosting.UpdationDate = value.UpdationDate;
                    _context.CostingMaster.Update(existingCosting);

                    // CostingRMComponent
                    foreach (var existing in existingCosting.CostingRMComponent.ToList())
                    {
                        _context.CostingRMComponent.Remove(existing);
                    }

                    foreach (var child in value.CostingRMComponent)
                    {
                        existingCosting.CostingRMComponent.Add(child);
                    }

                    // CostingRM
                    foreach (var existing in existingCosting.CostingRM.ToList())
                    {
                        _context.CostingRM.Remove(existing);
                    }

                    foreach (var child in value.CostingRM)
                    {
                        existingCosting.CostingRM.Add(child);
                    }

                    // CostingSizeRM
                    foreach (var existing in existingCosting.CostingSizeRM.ToList())
                    {
                        _context.CostingSizeRM.Remove(existing);
                    }

                    var CostingSizeRMId = _context.CostingSizeRM.Max(x => x.CostingSizeRMId);
                    CostingSizeRMId++;
                    foreach (var child in value.CostingSizeRM)
                    {
                        child.CostingSizeRMId = CostingSizeRMId;
                        CostingSizeRMId++;
                        existingCosting.CostingSizeRM.Add(child);
                    }

                    _context.SaveChanges();

                    // insert costingid for update costing
                    //var existing_tempcostingid = _context.TempCostigIDsForUpdateCosting.ToList();
                    //_context.TempCostigIDsForUpdateCosting.RemoveRange(existing_tempcostingid);
                    //_context.SaveChanges();

                    //long temptableid = _context.TempCostigIDsForUpdateCosting.Max(x => x.TempTableID);
                    //temptableid++;
                    //TempCostigIDsForUpdateCosting _tempobj = new TempCostigIDsForUpdateCosting();
                    //_tempobj.TempTableID = temptableid;
                    //_tempobj.CostingID = value.CostingID;
                    //_context.TempCostigIDsForUpdateCosting.Add(_tempobj);
                    //_context.SaveChanges();

                    //_context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingRMPrice ");
                    //_context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingAfterRMPriceChanges ");
                    // close after update costing
                }
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingRMComponent/{CostingId}")]
        public IActionResult GetCostingRMComponent(long CostingId)
        {
            try
            {
                var obj2 = _context.ViewCostingRMComponent
                                 .Where(x => x.CostingID == CostingId)
                                 .OrderBy(x => x.SerialNo)
                                 .ToList();
                return Ok(obj2);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutCompPhoto")]
        public IActionResult PutCompPhoto([FromBody]CostingRMComponent value)
        {
            try
            {
                var existingCosting = _context.CostingRMComponent
                        .Where(x => x.CostingRMComponentID == value.CostingRMComponentID)
                        .SingleOrDefault();

                if (existingCosting != null)
                {
                    existingCosting.Photo = value.Photo;
                    _context.SaveChanges();
                }
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutMatReviewedStatus")]
        public IActionResult PutMatReviewedStatus([FromBody]CostingMaster value)
        {
            try
            {
                var existing = _context.CostingMaster.Find(value.CostingID);
                existing.MatReviewedStatus = value.MatReviewedStatus;
                existing.MatReviewedDate = value.MatReviewedDate;
                existing.MatReviewedUserId = value.MatReviewedUserId;

                _context.CostingMaster.Update(existing);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ViewShowMaterialSearch")]
        public IActionResult ViewShowMaterialSearch([FromBody]CostingSearch CS)
        {
            try
            {
                IQueryable<ViewShowCosting> query = _context.ViewShowCosting.Where(x => x.FormType == 1);

                if (CS.BuyerID > 0)
                    query = query.Where(x => x.BuyerId == CS.BuyerID);

                if (!string.IsNullOrEmpty(CS.Code))
                    query = query.Where(x => x.Code.Contains(CS.Code));
                if (CS.ChkCreationDateInt == 1)
                    query = query.Where(x => x.CreationDate == (CS.CreationDate));

                if (CS.ChkUpdationDateFromInt == 1 && CS.ChkUpdationDateToInt == 1)
                    query = query.Where(x => x.UpdationDate >= (CS.UpdationDateFrom) && x.UpdationDate <= CS.UpdationDateTo);
                if (CS.ChkUpdationDateFromInt == 1 && CS.ChkUpdationDateToInt == 0)
                    query = query.Where(x => x.UpdationDate == (CS.UpdationDateFrom));

                query = query.OrderByDescending(x => x.CostingID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("OrderItemInBOM")]
        public IActionResult OrderItemInBOM([FromBody]CostingSearch CS)
        {
            try
            {
                IQueryable<ViewOrderBOM> query = _context.ViewOrderBOM.Where(x => x.OrderMasterID == CS.OrderMasterId && x.RItemID == CS.RitemId);

                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("PutRMinProduct")]
        //public IActionResult PutRMinProduct([FromBody]CostingSearch value)
        //{
        //    try
        //    {
        //        var supplier = _context.RItemSupp.Where(x => x.RItem_Id == value.UpdateRItemID).FirstOrDefault();
        //        long suppid = 0;
        //        if (supplier != null)
        //        {
        //            suppid = supplier.SupplierId;
        //        }

        //        var existingCostingRM = _context.CostingRM.Where(x => x.CostingID == value.CostingID && x.RItemID == value.RitemId).FirstOrDefault();
        //        if (existingCostingRM != null)
        //        {
        //            existingCostingRM.RItemID = value.UpdateRItemID;
        //            existingCostingRM.SupplierId = suppid;
        //            _context.CostingRM.Update(existingCostingRM);

        //            var existingCostingRMComponent = _context.CostingRMComponent.Where(x => x.CostingID == value.CostingID && x.RitemId == value.RitemId).FirstOrDefault();
        //            existingCostingRMComponent.RitemId = value.UpdateRItemID;
        //            existingCostingRMComponent.SupplierId = suppid;
        //            _context.CostingRMComponent.Update(existingCostingRMComponent);
        //        }
        //        _context.SaveChanges();
        //        var result = "Record Update";
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("PutRMinProduct")]
        public IActionResult PutRMinProduct([FromBody]List<CostingSearch> value)
        {
            try
            {
                var existing = _context.TempCostigIDsForUpdateCosting.ToList();
                _context.TempCostigIDsForUpdateCosting.RemoveRange(existing);
                _context.SaveChanges();
                long temptableid = _context.TempCostigIDsForUpdateCosting.Max(x => x.TempTableID);

                foreach (var onerecord in value)
                {
                    //Insert costing ids
                    TempCostigIDsForUpdateCosting _tempobj = new TempCostigIDsForUpdateCosting();
                    temptableid++;
                    _tempobj.TempTableID = temptableid;
                    _tempobj.CostingID = onerecord.CostingID;
                    _context.TempCostigIDsForUpdateCosting.Add(_tempobj);
                    // 

                    var supplier = _context.RItemSupp.Where(x => x.RItem_Id == onerecord.UpdateRItemID).FirstOrDefault();
                    long suppid = 0;
                    if (supplier != null)
                    {
                        suppid = supplier.SupplierId;
                    }

                    var existingCostingRM = _context.CostingRM.Where(x => x.CostingID == onerecord.CostingID && x.RItemID == onerecord.RitemId).FirstOrDefault();
                    if (existingCostingRM != null)
                    {
                        var objrm = _context.RitemMaster.Where(x => x.RitemId == onerecord.UpdateRItemID).FirstOrDefault();
                        existingCostingRM.RItemID = onerecord.UpdateRItemID;
                        existingCostingRM.SupplierId = suppid;

                        existingCostingRM.RMPrice = objrm.CostPrice;
                        existingCostingRM.RMCAmount = existingCostingRM.CAfterWAS * objrm.CostPrice;
                        existingCostingRM.RMBAmount = existingCostingRM.BOMAfterWas * objrm.CostPrice;
                        _context.CostingRM.Update(existingCostingRM);

                        var existingCostingRMComponent = _context.CostingRMComponent.Where(x => x.CostingID == onerecord.CostingID && x.RitemId == onerecord.RitemId).FirstOrDefault();
                        existingCostingRMComponent.RitemId = onerecord.UpdateRItemID;
                        existingCostingRMComponent.SupplierId = suppid;
                        _context.CostingRMComponent.Update(existingCostingRMComponent);
                    }
                }
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingAfterRMPriceChanges ");
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRMinProductSizeWise")]
        public IActionResult PutRMinProductSizeWise([FromBody]List<CostingSearch> value)
        {
            try
            {
                var existing = _context.TempCostigIDsForUpdateCosting.ToList();
                _context.TempCostigIDsForUpdateCosting.RemoveRange(existing);
                _context.SaveChanges();
                //long temptableid = _context.TempCostigIDsForUpdateCosting.Max(x => x.TempTableID);
                long temptableid = 1;

                var supplier = _context.RItemSupp.Where(x => x.RItem_Id == value.FirstOrDefault().UpdateRItemID).FirstOrDefault();
                long suppid = 0;
                if (supplier != null)
                {
                    suppid = supplier.SupplierId;
                }

                var objrm = _context.RitemMaster.Where(x => x.RitemId == value.FirstOrDefault().UpdateRItemID).FirstOrDefault();

                foreach (var onerecord in value)
                {
                    //Insert costing ids
                    TempCostigIDsForUpdateCosting _tempobj = new TempCostigIDsForUpdateCosting();
                    temptableid++;
                    _tempobj.TempTableID = temptableid;
                    _tempobj.CostingID = onerecord.CostingID;
                    _context.TempCostigIDsForUpdateCosting.Add(_tempobj);

                    var existingCostingSizeRM = _context.CostingSizeRM.Where(x => x.CostingID == onerecord.CostingID && x.SizeId == onerecord.SizeId && x.RItemID == onerecord.RitemId).FirstOrDefault();
                    if (existingCostingSizeRM != null)
                    {
                        existingCostingSizeRM.RItemID = onerecord.UpdateRItemID;
                        existingCostingSizeRM.SupplierId = suppid;
                        existingCostingSizeRM.RMPrice = objrm.CostPrice;
                        existingCostingSizeRM.RMCAmount = existingCostingSizeRM.CAfterWAS * objrm.CostPrice;
                        existingCostingSizeRM.RMBAmount = existingCostingSizeRM.BOMAfterWas * objrm.CostPrice;
                        _context.CostingSizeRM.Update(existingCostingSizeRM);
                    }
                }
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingAfterRMPriceChanges ");
                var result = "Record Update";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutCostingWithCurrentRMPrice")]
        public IActionResult PutCostingWithCurrentRMPrice([FromBody]CostingMaster value)
        {
            try
            {
                long costingid = 0;
                var obj = _context.CostingMaster.Where(x => x.FitemID == value.FitemID && x.SizeId == value.SizeId).FirstOrDefault();
                costingid = (obj == null ? 0 : obj.CostingID);

                if (costingid > 0)
                {
                    var existing = _context.TempCostigIDsForUpdateCosting.ToList();
                    _context.TempCostigIDsForUpdateCosting.RemoveRange(existing);
                    _context.SaveChanges();

                    long temptableid = _context.TempCostigIDsForUpdateCosting.Max(x => x.TempTableID);
                    temptableid++;
                    TempCostigIDsForUpdateCosting _tempobj = new TempCostigIDsForUpdateCosting();
                    _tempobj.TempTableID = temptableid;
                    _tempobj.CostingID = costingid;
                    _context.TempCostigIDsForUpdateCosting.Add(_tempobj);
                    _context.SaveChanges();

                    _context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingRMPrice ");
                    _context.Database.ExecuteSqlCommand("EXEC SP_UpdateCostingAfterRMPriceChanges ");
                    return Ok("Record Updated");
                }
                else
                {
                    return Ok("Costing Not Found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        #region RMCombo

        [Route("GetRMComboID")]
        public IActionResult GetRMComboID()
        {
            try
            {
                long RMComboID = 0;
                var lastRecord = _context.RMComboMaster.OrderBy(x => x.RMComboID).LastOrDefault();
                RMComboID = (lastRecord == null ? 0 : lastRecord.RMComboID) + 1;
                return Ok(RMComboID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMComboChildID")]
        public IActionResult GetNewRMComboChildID()
        {
            try
            {
                long RMComboChildID = 0;
                var lastRecord = _context.RMComboChild.OrderBy(x => x.RMComboChildID).LastOrDefault();
                RMComboChildID = (lastRecord == null ? 0 : lastRecord.RMComboChildID) + 1;
                return Ok(RMComboChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostRMCombo")]
        public IActionResult PostRMCombo([FromBody]RMComboMaster value)
        {
            try
            {
                _context.RMComboMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutRMCombo")]
        public IActionResult PutRMCombo([FromBody]RMComboMaster value)
        {
            try
            {
                var existingData = _context.RMComboMaster
                        .Where(x => x.RMComboID == value.RMComboID)
                        .Include(x => x.RMComboChild)
                        .SingleOrDefault();

                if (existingData != null)
                {
                    // Update parent
                    _context.Entry(existingData).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var ExistingRMComboChild in existingData.RMComboChild.ToList())
                    {
                        _context.RMComboChild.Remove(ExistingRMComboChild);
                    }

                    // Update and Insert children
                    foreach (var childRMC in value.RMComboChild)
                    {
                        // Insert child 1
                        RMComboChild newChild = new RMComboChild();
                        newChild.RMComboChildID = childRMC.RMComboChildID;
                        newChild.RMComboID = childRMC.RMComboID;
                        newChild.RitemId = childRMC.RitemId;
                        newChild.CompID = childRMC.CompID;
                        newChild.CompQty = childRMC.CompQty;
                        newChild.SerialNo = childRMC.SerialNo;
                        newChild.CompGroupId = childRMC.CompGroupId;
                        newChild.Remark = childRMC.Remark;
                        newChild.Photo = childRMC.Photo;
                        newChild.RMQty = childRMC.RMQty;
                        newChild.SupplierId = childRMC.SupplierId;
                        newChild.ProcessID = childRMC.ProcessID;

                        existingData.RMComboChild.Add(newChild);
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

        [Route("GetProductList")]
        public IActionResult GetProductList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ProductDetails.OrderByDescending(x => x.FitemId));
                else
                    return Ok(_context.ProductDetails.Where(x => x.Name.Contains(search)).OrderByDescending(x => x.FitemId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetComboList")]
        public IActionResult GetComboList()
        {
            try
            {
                object rmlist;
                rmlist = _context.RMComboMaster
                                    .OrderBy(c => c.ComboName)
                                    .Select(x => new { Id = x.RMComboID, Value = x.ComboName }).ToList();
                return Ok(rmlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMComboId/{ComboID}")]
        public IActionResult GetRMComboId(long ComboID)
        {
            try
            {
                long comboid = 0;
                var obj = _context.RMComboMaster.Where(x => x.RMComboID == ComboID).FirstOrDefault();
                comboid = (obj == null ? 0 : obj.RMComboID);
                return Ok(comboid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMCombo/{comboid}")]
        public IActionResult GetRMCombo(long comboid)
        {
            try
            {
                var obj2 = _context.ViewRMCombo
                                 .Where(x => x.RMComboID == comboid)
                                 .OrderBy(x => x.SerialNo)
                                 .ToList();
                return Ok(obj2);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #endregion

        #region UseCombo
        [Route("GetViewCostingProductForRMCombo/{BelongTo}")]
        public IActionResult GetViewCostingProductForRMCombo(long BelongTo)
        {
            try
            {
                var list = _context.ViewCostingProductForRMCombo.Where(x => x.BelongTo == BelongTo)
                    .OrderBy(x => x.Code).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetNewCostingFitemID/{Fitemid}")]
        public IActionResult GetNewCostingFitemID(long Fitemid)
        {
            try
            {
                long CostingRMComponentID = 0;
                var existing = _context.CostingMaster.Where(x => x.FitemID == Fitemid)
                    .Select(x => new { Id = x.CostingID })
                    .LastOrDefault();
                CostingRMComponentID = (existing == null ? 0 : existing.Id);
                return Ok(CostingRMComponentID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPut]
        [Route("SaveUseCombo")]
        public IActionResult SaveUseCombo([FromBody]List<CostingMaster> value)
        {
            try
            {
                var combo = _context.ViewRMCombo.Where(x => x.RMComboID == value.FirstOrDefault().CLID).ToList();
                long CostingRMID = _context.CostingRM.Max(x => x.CostingRMID);
                long CostingRMComponentID = _context.CostingRMComponent.Max(x => x.CostingRMComponentID);
                foreach (CostingMaster onerow in value)
                {
                    foreach (ViewRMCombo comborow in combo)
                    {
                        CostingRMID++;
                        CostingRM _rm = new CostingRM();
                        _rm.CostingRMID = CostingRMID;
                        _rm.CostingID = onerow.CostingID;
                        _rm.RItemID = comborow.RitemId;
                        _rm.RMQty = comborow.RMQty;
                        _rm.CWAS = 0;
                        _rm.CAfterWAS = 0;
                        _rm.RMPrice = 0;
                        _rm.RMCAmount = 0;
                        _rm.RMCAmount = 0;
                        _rm.BOMWAS = 0;
                        _rm.BOMAfterWas = 0;
                        _rm.SerialNo = 0;
                        _rm.IsComponent = 0;
                        _rm.SupplierId = comborow.SupplierId;
                        _context.CostingRM.Add(_rm);

                        CostingRMComponentID++;
                        CostingRMComponent _rmComponent = new CostingRMComponent();
                        _rmComponent.CostingRMComponentID = CostingRMComponentID++;
                        _rmComponent.CostingID = onerow.CostingID;
                        _rmComponent.RitemId = comborow.RitemId;
                        _rmComponent.CompID = comborow.CompID;
                        _rmComponent.Length = 0;
                        _rmComponent.Breadth = 0;
                        _rmComponent.Height = 0;
                        _rmComponent.CompQty     = comborow.CompQty;
                        _rmComponent.Area = 0;
                        _rmComponent.SerialNo = 0;
                        _rmComponent.CompGroupId = comborow.CompGroupId;
                        _rmComponent.Remark = comborow.Remark;
                        _rmComponent.Photo = comborow.Photo;
                        _rmComponent.RMQty = comborow.RMQty;
                        _rmComponent.SupplierId = comborow.SupplierId;
                        _rmComponent.ProcessID = comborow.ProcessID;
                        _context.CostingRMComponent.Add(_rmComponent);
                    }
                    _context.SaveChanges();
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }

            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        #endregion
    }
}
