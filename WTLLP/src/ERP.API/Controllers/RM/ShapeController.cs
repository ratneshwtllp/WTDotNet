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
    public class ShapeController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public ShapeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ShapeDetails> Get()
        {
            return _context.ShapeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ShapeDetails.Where(x => x.ShapeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetShapeList")]
        public IActionResult GetShapeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ShapeDetails.OrderByDescending(x => x.ShapeId));
                else
                    return Ok(_context.ShapeDetails.Where(x => x.ShapeName.Contains(search)).OrderByDescending(x => x.ShapeId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewShapeId")]
        public IActionResult GetNewShapeId()
        {
            try
            {
                long ShapeId = 0;
                var lastShape = _context.ShapeDetails.OrderBy(x => x.ShapeId).LastOrDefault();
                ShapeId = (lastShape == null ? 0 : lastShape.ShapeId) + 1;
                return Ok(ShapeId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ShapeDetails value)
        {
            try
            {
                _context.ShapeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ShapeDetails value)
        {
            try
            {
                var existingShape = _context.ShapeDetails.Find(value.ShapeId);
                if (existingShape == null)
                {
                    return Ok("Not Found");
                }
                existingShape.ShapeName = value.ShapeName;
                existingShape.ShapeDesc = value.ShapeDesc;
                _context.ShapeDetails.Update(existingShape);
                _context.SaveChanges();
                return Ok("Record Updated");
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
                var existingShape = _context.ShapeDetails.Where(x => x.ShapeName == value).FirstOrDefault<ShapeDetails>();
                if (existingShape != null)
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
                var existingShape = _context.ShapeDetails.Find(id);
                if (existingShape != null)
                {
                    _context.ShapeDetails.Remove(existingShape);
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
