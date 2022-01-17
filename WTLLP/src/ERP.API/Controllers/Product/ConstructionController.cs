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
    public class ConstructionController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ConstructionController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ConstructionDetails> Get()
        {
            return _context.ConstructionDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ConstructionDetails.Where(x => x.ConstrnId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetBuyerList")]
        //public IActionResult GetBuyerList()
        //{
        //    try
        //    {
        //        object Buyers;
        //        Buyers = _context.BuyerDetails
        //                                .OrderBy(c => c.BuyerCode)
        //                                .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
        //        return Ok(Buyers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetConstructionList")]
        public IActionResult GetConstructionList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ConstructionDetails.OrderByDescending(x => x.ConstrnId));
                else
                    return Ok(_context.ConstructionDetails
                        .Where(x => x.ConstrnName.Contains(search)) // || x.BuyerName.Contains(search))
                        .OrderByDescending(x => x.ConstrnId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetListConstruction")]
        public IActionResult GetListConstruction()
        {
            try
            {
                object Constrns;
                Constrns = _context.ConstructionDetails
                                        .OrderBy(c => c.ConstrnName)
                                        .Select(x => new { Id = x.ConstrnId, Value = x.ConstrnName }).ToList();
                return Ok(Constrns);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewConstructionId")]
        public IActionResult GetNewConstructionId()
        {
            try
            {
                long ConstrnId = 0;
                var lastconstrn = _context.ConstructionDetails.OrderBy(x => x.ConstrnId).LastOrDefault();
                ConstrnId = (lastconstrn == null ? 0 : lastconstrn.ConstrnId) + 1;
                return Ok(ConstrnId);
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
                var existingConstrn = _context.ConstructionDetails.Where(x => x.ConstrnName == value).FirstOrDefault<ConstructionDetails>();
                if (existingConstrn != null)
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
        public IActionResult Post([FromBody]ConstructionDetails value)
        {
            try
            {
                _context.ConstructionDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]ConstructionDetails value)
        {
            try
            {
                var existingConstrn = _context.ConstructionDetails.Where(x => x.ConstrnId == value.ConstrnId).FirstOrDefault<ConstructionDetails>();
                if (existingConstrn == null)
                {
                    return Ok("Not Found");
                }
                existingConstrn.ConstrnName = value.ConstrnName;
                existingConstrn.ConstrnDesc = value.ConstrnDesc;
                _context.ConstructionDetails.Update(existingConstrn);
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
                var existingConstrn = _context.ConstructionDetails.Where(x => x.ConstrnId == id).FirstOrDefault<ConstructionDetails>();
                if (existingConstrn != null)
                {
                    _context.ConstructionDetails.Remove(existingConstrn);
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
    }
}
