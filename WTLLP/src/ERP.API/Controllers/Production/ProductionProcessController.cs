using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using ERP.Domain.Entities;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ProductionProcessController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ProductionProcessController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ProductionProcessDetails> Get()
        {
            return _context.ProductionProcessDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.ProductionProcessDetails.Where(x => x.ProcessID == id).FirstOrDefault());
        }

        [Route("GetProductionProcessList")]
        public IActionResult GetProductionProcessList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.ProductionProcessDetails.OrderBy(x => x.ProcessName));
            }
            else
            {
                return Ok(_context.ProductionProcessDetails
                    .Where(x => x.ProcessName.Contains(search))
                    .OrderBy(x => x.ProcessName));
            }
        }

        [Route("GetNewProductionProcessId")]
        public IActionResult GetNewProductionProcessId()
        {
            long ProductionProcessID = 0;
            var lastProductionProcess = _context.ProductionProcessDetails.OrderBy(x => x.ProcessID).LastOrDefault();
            ProductionProcessID = (lastProductionProcess == null ? 0 : lastProductionProcess.ProcessID) + 1;
            return Ok(ProductionProcessID);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductionProcessDetails value)
        {
            _context.ProductionProcessDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductionProcessDetails value)
        {
            var existingProductionProcess = _context.ProductionProcessDetails.Find(value.ProcessID);
            if (existingProductionProcess == null)
            {
                return NotFound();
            }
            existingProductionProcess.ProcessName = value.ProcessName;
            existingProductionProcess.Description = value.Description;
            existingProductionProcess.IsRMRequired = value.IsRMRequired;
            existingProductionProcess.IsChargeable = value.IsChargeable;
            existingProductionProcess.Charge_on_Item_Component = value.Charge_on_Item_Component;
            _context.ProductionProcessDetails.Update(existingProductionProcess);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingProductionProcess = _context.ProductionProcessDetails.Where(x => x.ProcessName == value).FirstOrDefault<ProductionProcessDetails>();

            if (existingProductionProcess != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingProductionProcess = _context.ProductionProcessDetails.Find(id);
            if (existingProductionProcess != null)
            {
                _context.ProductionProcessDetails.Remove(existingProductionProcess);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("ProcessList")]
        public IActionResult ProcessList()
        {
            try
            {
                object Obj;
                Obj = _context.ProductionProcessDetails.OrderBy(c => c.ProcessName).Select(x => new { Id = x.ProcessID, Value = x.ProcessName }).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProcessListForPayment")]
        public IActionResult ProcessListForPayment()
        {
            try
            {
                object Obj;
                Obj = _context.ProductionProcessDetails.Where(x=> x.IsChargeable == 1).OrderBy(c => c.ProcessName).Select(x => new { Id = x.ProcessID, Value = x.ProcessName }).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProcessListForAnlysis")]
        public IActionResult ProcessListForAnlysis()
        {
            try
            {
                object Obj;
                Obj = _context.ProductionProcessDetails.Where(x => x.IsRMRequired == 1)
                    .OrderBy(c => c.ProcessName)
                    .Select(x => new { Id = x.ProcessID, Value = x.ProcessName }).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProductProcessList/{FitemId}")]
        public IActionResult ProductProcessList(long FitemId)
        {
            try
            {
                object Obj;
                Obj = _context.ViewProductProcessDetails.Where(c => c.FitemId == FitemId).OrderBy(c => c.SNo).Select(x => new { Id = x.ProcessID, Value = x.ProcessName }).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProductProcessListForCharge/{FitemId}")]
        public IActionResult ProductProcessListForCharge(long FitemId)
        {
            try
            {
                object Obj;
                Obj = _context.ViewProductProcessDetails.Where(c => c.FitemId == FitemId && c.IsChargeable == 1).OrderBy(c => c.ProcessName).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProcessOfWP/{PlanId}")]
        public IActionResult ProcessOfWP(long PlanId)
        {
            try
            {
                var idlist = _context.ProcessExecutionMaster
                                   .Where(x => x.PlanId == PlanId)
                                   .Distinct()
                                   .Select(x => x.ProcessID).ToArray();

                object Obj;
                Obj = _context.ProductionProcessDetails.Where(x => idlist.Contains(x.ProcessID)).OrderBy(c => c.ProcessName)
                    .Select(x => new { Id = x.ProcessID, Value = x.ProcessName }).ToList();

                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
