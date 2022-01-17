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
    public class RackMasterController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public RackMasterController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<RackMaster> Get()
        {
            return _context.RackMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.RackMaster.Where(x => x.RackId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetRMCategoryList")]
        //public IActionResult GetRMCategoryList()
        //{
        //    try
        //    {
        //        object RMCategory;
        //        RMCategory = _context.RMCategory
        //                            .OrderBy(c => c.CategoryName)
        //                            .Select(x => new { Id = x.CategoryID, Value = x.CategoryName }).ToList();
        //        return Ok(RMCategory);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetRackMasterList")]
        public IActionResult GetRackMasterList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewRackMaster.OrderByDescending(x => x.RackId));
                else
                    return Ok(_context.ViewRackMaster.Where(x => x.LineNo.Contains(search) || x.RackNo.Contains(search) || x.StoreName.Contains(search) || x.Remark.Contains(search))
                        .OrderByDescending(x => x.RackId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRackMasterId")]
        public IActionResult GetNewRackMasterId()
        {
            try
            {
                long RackId = 0;
                var lastRack = _context.RackMaster.OrderBy(x => x.RackId).LastOrDefault();
                RackId = (lastRack == null ? 0 : lastRack.RackId) + 1;
                return Ok(RackId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList()
        {
            try
            {
                var racknolist = _context.RackMaster
                    .Select(x => new { Id = x.RackId, Value = x.RackNo + " " + x.Remark })
                    .ToList();
                return Ok(racknolist);
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
                var existingRack = _context.RackMaster.Where(x => x.RackNo == value).FirstOrDefault<RackMaster>();
                if (existingRack != null)
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
       
        [HttpPost]
        public IActionResult Post([FromBody]RackMaster value)
        {
            try
            {
                _context.RackMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]RackMaster value)
        {
            try
            {
                var existingRack = _context.RackMaster.Where(x => x.RackId == value.RackId).FirstOrDefault<RackMaster>();
                if (existingRack == null)
                {
                    return Ok("Not Found");
                }

                existingRack.StoreId = value.StoreId;
                existingRack.LineCode = value.LineCode;
                existingRack.LineSNo = value.LineSNo;
                existingRack.LineNo = value.LineNo;
                existingRack.RackSNo = value.RackSNo;
                existingRack.RackNo = value.RackNo;
                existingRack.Remark = value.Remark;
                _context.RackMaster.Update(existingRack);
                _context.SaveChanges();
                return Ok("Record Updated");
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
                var existingRack = _context.RackMaster.Where(x => x.RackId == id).FirstOrDefault<RackMaster>();
                if (existingRack != null)
                {
                    _context.RackMaster.Remove(existingRack);
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

        [Route("GetStoreList")]
        public IActionResult GetStoreList()
        {
            try
            {
                object store;
                store = _context.StoreMasterDetails
                                        .OrderBy(c => c.StoreName)
                                        .Select(x => new { Id = x.StoreId, Value = x.StoreName }).ToList();
                return Ok(store);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRackNoList/{suppid}/{rmid}")]
        public IActionResult GetRackNoList(long suppid, long rmid)
        {
            try
            {
                object rackNoList;
                rackNoList = _context.ViewGetRack.Where(x => x.SupplierID == suppid && x.RitemID == rmid)
                                         .OrderBy(x => x.RackId)
                                         .Select(x => new { Id = x.RackId, Value = x.RackNo + " " + x.Remark }).ToList();
                return Ok(rackNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStoreRackList/{storeid}")]
        public IActionResult GetStoreRackList(long storeid)
        {
            try
            {
                object rackNoList;
                rackNoList = _context.RackMaster.Where(x => x.StoreId == storeid)
                                         .OrderBy(x => x.RackNo)
                                         .Select(x => new { Id = x.RackId, Value = x.RackNo + " " + x.Remark  }).ToList();
                return Ok(rackNoList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
