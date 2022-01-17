using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
namespace ERP.API
{
    [Route("api/[controller]")]
    public class CareLabelController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;
        public CareLabelController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable<ViewCarelabel> Get()
        {
            return _context.ViewCarelabel.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CareLabelDetails.Where(x => x.CareLabelID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("BuyerList")]
        public IActionResult BuyerList()
        {
            try
            {
                object supplier;
                supplier = _context.BuyerDetails
                                    .OrderBy(c => c.BuyerCode)
                                    .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCareLabelDetailsList")]
        public IActionResult GetCareLabelDetailsList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewCarelabel.OrderByDescending(x => x.CareLabelID));
                else
                    return Ok(_context.ViewCarelabel
                        .Where(x => x.BuyerName.Contains(search) || x.CareLabelLName.Contains(search))
                        .OrderByDescending(x => x.CareLabelID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCarelabelId")]
        public IActionResult GetNewCarelabelId()
        {
            try
            {
                long CareLabelID = 0;
                var lastSubCategory = _context.CareLabelDetails.OrderBy(x => x.CareLabelID).LastOrDefault();
                CareLabelID = (lastSubCategory == null ? 0 : lastSubCategory.CareLabelID) + 1;
                return Ok(CareLabelID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]CareLabelDetails value)
        {
            try
            {
                _context.CareLabelDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]CareLabelDetails value)
        {
            try
            {
                var existingCareLabel = _context.CareLabelDetails.Find(value.CareLabelID);
                if (existingCareLabel == null)
                {
                    return Ok("Not Found");
                }
                existingCareLabel.CareLabelLName = value.CareLabelLName;
                existingCareLabel.Description = value.Description;
                existingCareLabel.BuyerId = value.BuyerId;
                existingCareLabel.PhotoPath = value.PhotoPath;
                _context.CareLabelDetails.Update(existingCareLabel);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value, int BuyerId)
        {
            try
            {
                var existingSticker = _context.CareLabelDetails.Where(x => x.CareLabelLName == value && x.BuyerId == BuyerId).FirstOrDefault<CareLabelDetails>();
                if (existingSticker != null)
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

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingSticker = _context.CareLabelDetails.Where(x => x.CareLabelID == id).FirstOrDefault<CareLabelDetails>();
                if (existingSticker != null)
                {
                    _context.CareLabelDetails.Remove(existingSticker);
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

        [Route("GetCareLabelList")]
        public IActionResult GetCareLabelList()
        {
            try
            {
                object CareLabel;
                CareLabel = _context.CareLabelDetails
                                    .OrderBy(c => c.CareLabelLName)
                                    .Select(x => new { Id = x.CareLabelID, Value = x.CareLabelLName }).ToList();
                return Ok(CareLabel);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
