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
    public class CartonController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public CartonController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<CartonDetails> Get()
        {
            return _context.CartonDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CartonDetails.Where(x => x.CartonId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("FillCartonDim")]
        public IActionResult FillCartonDim(string search)
        {
            try
            {
                object carton;
                if (string.IsNullOrEmpty(search))
                {
                    carton = _context.CartonDetails
                                        .OrderBy(c => c.Dimension)
                                        .Select(x => new { Id = x.CartonId, Value = x.Dimension });
                }
                else
                {
                    carton = _context.CartonDetails
                                        .Where(x => x.Dimension.Contains(search))
                                        .OrderBy(c => c.Dimension)
                                        .Select(x => new { Id = x.CartonId, Value = x.Dimension });
                }
                return Ok(carton);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCartonList")]
        public IActionResult GetCartonList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.CartonDetails.OrderByDescending(x => x.CartonId));
                }
                else
                {
                    return Ok(_context.CartonDetails
                        .Where(x => x.Dimension.Contains(search))
                        .OrderByDescending(x => x.CartonId));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCartonId")]
        public IActionResult GetNewCartonId()
        {
            try
            {
                long CartonId = 0;
                var lastShape = _context.CartonDetails.OrderBy(x => x.CartonId).LastOrDefault();
                CartonId = (lastShape == null ? 0 : lastShape.CartonId) + 1;
                return Ok(CartonId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CartonDetails value)
        {
            try
            {
                _context.CartonDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]CartonDetails value)
        {
            try
            {
                var existingCarton = _context.CartonDetails.Find(value.CartonId);
                if (existingCarton == null)
                {
                    return Ok("Not Found");
                }
                existingCarton.Length = value.Length;
                existingCarton.Height = value.Height;
                existingCarton.Width = value.Width;
                existingCarton.Dimension = value.Dimension;
                existingCarton.Weight = value.Weight;
                existingCarton.Description = value.Description;
                _context.CartonDetails.Update(existingCarton);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingCarton = _context.CartonDetails.Where(x => x.CartonId == id).FirstOrDefault<CartonDetails>();
                if (existingCarton != null)
                {
                    _context.CartonDetails.Remove(existingCarton);
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

        [Route("GetDimensionList")]
        public IActionResult GetDimensionList()
        {
            try
            {
                var dimensionlist = _context.CartonDetails.OrderBy(x => x.CartonId)
                                            .Select(x => new { Id = x.CartonId, Value = x.Dimension })
                                            .ToList();
                 
                return Ok(dimensionlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
