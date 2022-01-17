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
    public class OpeningStockController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public OpeningStockController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ViewOpStockDetails> Get()
        {
            return _context.ViewOpStockDetails.OrderByDescending(x=> x.OpeningStockID).AsQueryable();
            //return _context.OpeningStockDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.OpeningStockDetails.Where(x => x.OpeningStockID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewOpStockDetails")]
        public IActionResult GetViewOpStockDetails(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewOpStockDetails.OrderByDescending(x => x.OpeningStockID));
                else
                    return Ok(_context.ViewOpStockDetails
                        .Where(x => x.CategoryName.Contains(search) || x.SubCategoryName.Contains(search) || x.Full_Name.Contains(search) || x.SupplierName.Contains(search) || x.RackNo.Contains(search))
                        .OrderByDescending(x => x.OpeningStockID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        } 

        [Route("GetNewOpStockId")]
        public IActionResult GetNewOpStockId()
        {
            try
            {
                long OpStockId = 0;
                var lastOpStock = _context.OpeningStockDetails.OrderBy(x => x.OpeningStockID).LastOrDefault();
                OpStockId = (lastOpStock == null ? 0 : lastOpStock.OpeningStockID) + 1;
                return Ok(OpStockId);
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
                var suppid = _context.RItemSupp.Where(x => x.RItem_Id == ritemid).Select(x => x.SupplierId ).ToArray();

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
          
        [Route("GetOpStockDetails/{ritemid}")]
        public IActionResult GetOpStockDetails(long ritemid)
        {
            try
            { 
                return Ok(_context.ViewOpStockDetails.Where(x => x.RitemID == ritemid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        
        [Route("CheckDuplicate/{ritemid}/{suppid}/{rackid}")]
        public IActionResult CheckDuplicate(long ritemid, long suppid, long rackid)
        {
            try
            {
                var existingRack = _context.OpeningStockDetails.Where(x => x.RitemID == ritemid && x.SupplierID == suppid && x.RackID == rackid).FirstOrDefault<OpeningStockDetails>();
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
        public IActionResult Post([FromBody]OpeningStockDetails value)
        {
            try
            {
                _context.OpeningStockDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]OpeningStockDetails value)
        {
            try
            {
                var existingOpStock = _context.OpeningStockDetails.Where(x => x.OpeningStockID == value.OpeningStockID).FirstOrDefault<OpeningStockDetails>();
                if (existingOpStock == null)
                {
                    return Ok("Not Found");
                }
                //existingOpStock.Remark = value.Remark;
                _context.OpeningStockDetails.Update(existingOpStock);
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
                var existingOpStock = _context.OpeningStockDetails.Where(x => x.OpeningStockID == id).FirstOrDefault<OpeningStockDetails>();
                if (existingOpStock != null)
                {
                    _context.OpeningStockDetails.Remove(existingOpStock);
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
