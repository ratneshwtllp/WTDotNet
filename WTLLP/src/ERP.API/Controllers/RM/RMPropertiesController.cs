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
    public class RMPropertiesController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public RMPropertiesController(IMemoryCache memoryCache, DBContext context)
        {
            _context = context;
        }
     
        [Route("GetNewRPId")]
        public IActionResult GetNewRPId()
        {
            try
            {
                long RMProId = 0;
                var lastRMPro = _context.RMProperties.OrderBy(x => x.RMPropertiesID).LastOrDefault();
                RMProId = (lastRMPro == null ? 0 : lastRMPro.RMPropertiesID) + 1;
                return Ok(RMProId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]RMProperties value)
        {
            try
            {               
                _context.RMProperties.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMPropertiesList")]
        public IActionResult RMPropertiesList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.RMProperties.OrderByDescending(x => x.RMPropertiesName ));
                else
                    return Ok(_context.RMProperties
                        .Where(x => x.RMPropertiesName.Contains(search))
                        .OrderByDescending(x => x.RMPropertiesName));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetValueRMPro/{fitemid}")]
        public IActionResult GetValueRMPro(long fitemid)
        {
            try
            {
                var RMPro = _context.RMProperties.Where(x => x.RMPropertiesID == fitemid)
                                    .OrderByDescending(x => x.RMPropertiesID).FirstOrDefault();
                return Ok(RMPro);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]RMProperties value)
        {
            try
            {
                var existingRMPro = _context.RMProperties.Where(x => x.RMPropertiesID == value.RMPropertiesID).FirstOrDefault<RMProperties>();
                if (existingRMPro == null)
                {
                    return Ok("Not Found");
                }
                existingRMPro.RMPropertiesName = value.RMPropertiesName;
                existingRMPro.RMPropertiesIsPrintOnPo = value.RMPropertiesIsPrintOnPo;
                existingRMPro.RMPropertiesIsMaster = value.RMPropertiesIsMaster;
                existingRMPro.RMPropertiesRemark = value.RMPropertiesRemark;
                
                _context.RMProperties.Update(existingRMPro);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingRMPro = _context.RMProperties.Where(x => x.RMPropertiesID == id).SingleOrDefault();

                var allreadyuse = _context.RMPropertiesValue .Where(x => x.RMPropertiesID == id).FirstOrDefault<RMPropertiesValue>();


                if (existingRMPro != null && allreadyuse==null)
                {                    
                    _context.RMProperties.Remove(existingRMPro);
                    _context.SaveChanges();
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
        //For RMPValue
        [Route("GetRMPValueList")]
        public IActionResult GetRMPValueList()
        {
            try
            {
                object RMPValue;
                RMPValue = _context.RMProperties.Where(x=>x.RMPropertiesIsMaster==1)
                                    .OrderBy(c => c.RMPropertiesName)
                                    .Select(x => new { Id = x.RMPropertiesID, Value = x.RMPropertiesName }).ToList();
                return Ok(RMPValue);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        //[HttpPost]
        [HttpPost, Route("PostValue")]
        public IActionResult PostValue([FromBody]RMPropertiesValue RMPV)
        {
            try
            {
                _context.RMPropertiesValue.Add(RMPV);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }              

        [Route("GetNewRPValueId")]
        public IActionResult GetNewRPValueId()
        {
            try
            {
                long RMPValueId = 0;
                var lastRPV = _context.RMPropertiesValue.OrderBy(x => x.RMPropertiesValueID).LastOrDefault();
                RMPValueId = (lastRPV == null ? 0 : lastRPV.RMPropertiesValueID) + 1;
                return Ok(RMPValueId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpPost]
        //[Route("RMProValueList")]
        //public IActionResult RMProValueList([FromBody]RMProSearch PS)
        //{
        //    try
        //    {
        //        IQueryable<ViewRMPropertiesDetails> query = _context.ViewRMPropertiesDetails;
        //        if (PS.RMPropertiesValId > 0)
        //            query = query.Where(x => x.RMPropertiesID== PS.RMPropertiesValId);
        //        if (!string.IsNullOrEmpty(PS.RMPropertiesValue))
        //            query = query.Where(x => x.RMPropertiesValueName.Contains(PS.RMPropertiesValue));
        //        query = query.OrderByDescending(x => x.RMPropertiesValueID);
        //        var record = query.ToList();
        //        return Ok(record);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("RMProValueList")]
        public IActionResult RMProValueList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewRMPropertiesDetails.OrderByDescending(x => x.RMPropertiesName));
                else
                    return Ok(_context.ViewRMPropertiesDetails
                        .Where(x => x.RMPropertiesName.Contains(search) || x.RMPropertiesValueName .Contains(search))
                        .OrderByDescending(x => x.RMPropertiesName));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetValueRPro/{fitemid}")]
        public IActionResult GetValueRPro(long fitemid)
        {
            try
            {
                var RMProValue = _context.RMPropertiesValue.Where(x => x.RMPropertiesValueID == fitemid)
                                    .OrderByDescending(x => x.RMPropertiesValueID).FirstOrDefault();
                return Ok(RMProValue);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
      
        [HttpPut, Route("PutValue")]
        public IActionResult PutValue([FromBody]RMPropertiesValue value)
        {
            try
            {
                var ExistingRMV = _context.RMPropertiesValue
                        .Where(x => x.RMPropertiesValueID == value.RMPropertiesValueID)
                        .SingleOrDefault();

                if (ExistingRMV != null)
                {                   
                    _context.Entry(ExistingRMV).CurrentValues.SetValues(value);
                    _context.SaveChanges();
                }
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("DeleteValue/{id}")]
        public IActionResult DeleteValue(int id)
        {
            try
            {
                var existingRMProValue = _context.RMPropertiesValue
                     .Where(x => x.RMPropertiesValueID == id)
                     .SingleOrDefault();
                if (existingRMProValue != null)
                {
                    _context.RMPropertiesValue.Remove(existingRMProValue);
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
        //For Mapping
        [Route("GetRMProList")]
        public IQueryable<RMProperties> GetRMProList()
        {
            return _context.RMProperties.OrderBy(x => x.RMPropertiesName).AsQueryable();
        }


        [Route("GetNewMapId")]
        public IActionResult GetNewMapId()
        {
            try
            {
                long RMPMapId = 0;
                var lastRMap = _context.RMPropertiesMapping.OrderBy(x => x.RMMappingID).LastOrDefault();
                RMPMapId = (lastRMap == null ? 0 : lastRMap.RMMappingID) + 1;
                return Ok(RMPMapId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMProId")]
        public IActionResult GetNewRMProId()
        {
            try
            {
                long RItemSupp_ID = 0;
                var lastRItemSupp = _context.RMProperties.OrderBy(x => x.RMPropertiesID).LastOrDefault();
                RItemSupp_ID = (lastRItemSupp == null ? 0 : lastRItemSupp.RMPropertiesID) + 1;
                return Ok(RItemSupp_ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]        
        [Route("PostMapping")]
        public IActionResult PostMapping([FromBody]RMPropertiesMapping value)
        {
            try
            {
                //var existingmapping = _context.RMPropertiesMapping
                //                .Where(x => x.CategoryID == value.CategoryID).ToList();
                //if (existingmapping != null)
                //{
                //    foreach (var ExistingRMP in existingmapping)
                //    {
                //        _context.RMPropertiesMapping.Remove(ExistingRMP);
                //    }

                //}
                long id = _context.RMPropertiesMapping.Max(x=> x.RMMappingID);
                  id++;
                  value.RMMappingID = id;
                 _context.RMPropertiesMapping.Add(value);
                  _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("RMMappingList")]
        public IActionResult RMMappingList([FromBody]RMProSearch RMS)
        {
            try
            {
                IQueryable<ViewRMPropertiesMapping> query = _context.ViewRMPropertiesMapping;
                if (RMS.CategoryID > 0)
                    query = query.Where(x => x.CategoryID == RMS.CategoryID);                        
                query = query.OrderBy(x => x.RMMappingID);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteMapping/{id}")]
        public IActionResult DeleteMapping(int id)
        {
            try
            {
                var existingRM = _context.RMPropertiesMapping
                                                     .Where(x => x.RMMappingID == id)                                                     
                                                     .SingleOrDefault();
                if (existingRM != null)
                {               
                    _context.RMPropertiesMapping.Remove(existingRM);
                    _context.SaveChanges();
                }
                else
                {
                    return Ok("Not Found");
                }
                return Ok("Record Deleted");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMapping/{fitemid}")]
        public IActionResult GetMapping(long fitemid)
        {
            try
            {
                var RMProMapp = _context.RMPropertiesMapping.Where(x => x.RMMappingID == fitemid)
                                    .OrderByDescending(x => x.RMMappingID).ToList();
                return Ok(RMProMapp);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMapList")]
        public IQueryable<RMPropertiesMapping> GetMapList()
        {
            return _context.RMPropertiesMapping.OrderBy(x => x.RMMappingID).AsQueryable();
        }

        [Route("GetRMMapProperties/{catid}")]
        public IQueryable<RMPropertiesMapping> GetRMMapProperties(long catid)
        {
            return _context.RMPropertiesMapping.Where(x => x.CategoryID   == catid).AsQueryable();
        }

        [Route("DeleteRMmapping/{id}")]
        public IActionResult DeleteRMmapping(long id)
        {
            try
            {
                var existingrmmaping = _context.RMPropertiesMapping.Where(x => x.CategoryID  == id).ToList();
                if (existingrmmaping != null)
                {

                    foreach (var existingrmproperties in existingrmmaping)
                    {
                        _context.RMPropertiesMapping.Remove(existingrmproperties);
                        _context.SaveChanges();

                    }
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

        [Route("GetCatMapProperties/{catid}")]
        public IQueryable<ViewRMPropertiesMapping> GetCatMapProperties(long catid)
        {
            return _context.ViewRMPropertiesMapping.Where(x => x.CategoryID == catid).AsQueryable();
        }

        [Route("GetRMPropertiesValueList/{propertiesid}")]
        public IActionResult GetRMPropertiesValueList(long propertiesid)
        {
            try
            {
                object RMPValue;
                RMPValue = _context.RMPropertiesValue .Where (x=>x.RMPropertiesID == propertiesid)
                                    .OrderBy(c => c.RMPropertiesValueName )
                                    .Select(x => new { Id = x.RMPropertiesValueID , Value = x.RMPropertiesValueName }).ToList();
                return Ok(RMPValue);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

      
        [Route("CheckRMPValue/{id}/{value}/{rmpvalueid}")]
        public IActionResult CheckBarcode(long id,string value,long rmpvalueid)
        {
            try
            {
                if (rmpvalueid > 0) // edit mode
                {
                    var existing = _context.RMPropertiesValue .Where(x => x.RMPropertiesID  == id && x.RMPropertiesValueName  == value && x.RMPropertiesValueID  != rmpvalueid).FirstOrDefault<RMPropertiesValue>();
                    if (existing != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
                else
                {
                    var existing = _context.RMPropertiesValue.Where(x => x.RMPropertiesID == id && x.RMPropertiesValueName == value).FirstOrDefault<RMPropertiesValue>();
                    if (existing != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckRMP/{value}/{rmpid}")]
        public IActionResult CheckRMP(string value, long rmpid)
        {
            try
            {
                if (rmpid > 0) // edit mode
                {
                    var existing = _context.RMProperties .Where(x => x.RMPropertiesName  == value && x.RMPropertiesID  != rmpid).FirstOrDefault<RMProperties>();
                    if (existing != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
                else
                {
                    var existing = _context.RMProperties.Where(x => x.RMPropertiesName == value).FirstOrDefault<RMProperties>();
                    if (existing != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
