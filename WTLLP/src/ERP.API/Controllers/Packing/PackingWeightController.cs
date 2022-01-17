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
    public class PackingWeightController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PackingWeightController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("Get/{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var packingweight = _context.PackingWeightDetails.Where(x => x.PackingID == id)
                     .OrderBy(x => x.CartonNo);

                return Ok(packingweight);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewid")]
        public IActionResult GetNewid()
        {
            try
            {
                long PackingWtID = 0;
                var lastpackingWt = _context.PackingWeightDetails.OrderBy(x => x.PackingWeightID).LastOrDefault();
                PackingWtID = (lastpackingWt == null ? 0 : lastpackingWt.PackingWeightID) + 1;
                return Ok(PackingWtID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNoofCartonsByDimension/{id}")]
        public IActionResult GetNoofCartonsByDimension(long id)
        {
            try
            {
                var NoofCartonsByDimension = _context.ViewPackingWeight.Where(x => x.PackingID == id);
                return Ok(NoofCartonsByDimension);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("IsPackingWeightExist/{id}")]
        public IActionResult IsPackingWeightExist(long id)
        {
            var existingpacking = _context.PackingWeightDetails.Where(x => x.PackingID == id).FirstOrDefault();
            if (existingpacking != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [Route("GetPackingWeightDetails/{id}")]
        public IActionResult GetPackingWeightDetails(long id)
        {
            var packwtDetails = _context.PackingWeightDetails
                                        .Where(x => x.PackingID == id).ToList();
            return Ok(packwtDetails);
        }

        [HttpPost]
        public IActionResult Post([FromBody]List<PackingWeightDetails> value)
        {
            try
            {
                for (int i = 0; i < value.Count; i++)
                {
                    _context.PackingWeightDetails.Add(value[i]);
                }
                _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]List<PackingWeightDetails> value)
        {
            try
            {
                var existingPackWeight = _context.PackingWeightDetails
                        .Where(x => x.PackingID == value[0].PackingID)
                        .ToList();

                if (existingPackWeight != null)
                {
                    // Update Parent
                    //_context.Entry(existingPackWeight).CurrentValues.SetValues(value);

                    // Delete children of Master
                    foreach (var ExistingChild in existingPackWeight)
                    {
                        _context.PackingWeightDetails.Remove(ExistingChild);
                    }

                    for (int i = 0; i < value.Count; i++)
                    {
                        _context.PackingWeightDetails.Add(value[i]);
                    }
                    _context.SaveChanges();
                    return Ok("Record Updated");
                }
                else { return Ok("Error"); }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }

        }


    }
}
