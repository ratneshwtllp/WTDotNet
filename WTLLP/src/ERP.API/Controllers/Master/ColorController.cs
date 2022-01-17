using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ColorController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ColorDetails> Get()
        {
            return _context.ColorDetails.AsQueryable().OrderBy(x => x.Clname);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ColorDetails.Where(x => x.Clid == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetColorList")]
        public IActionResult GetColorList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.ColorDetails 
                        .OrderByDescending(x => x.Clid));
                }
                else
                {
                    return Ok(_context.ColorDetails
                        .Where(x => x.Clname.Contains(search))
                        .OrderByDescending(x => x.Clid));
                }
                    
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewColorId")]
        public IActionResult GetNewColorId()
        {
            try
            {
                long colorid = 0;
                var lastcolor = _context.ColorDetails.OrderBy(x => x.Clid).LastOrDefault();
                colorid = (lastcolor == null ? 0 : lastcolor.Clid) + 1;
                return Ok(colorid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ColorDetails value)
        {
            try
            {
                _context.ColorDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ColorDetails value)
        {
            try
            {
                var existingColor = _context.ColorDetails.Find(value.Clid);
                if (existingColor == null)
                {
                    return Ok("Not Found");
                }
                existingColor.Clname = value.Clname;
                existingColor.Description = value.Description;
                existingColor.Online_Transfer = 0;
                _context.ColorDetails.Update(existingColor);
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
                var existingColor = _context.ColorDetails.Where(x => x.Clid == id).FirstOrDefault<ColorDetails>();
                if (existingColor != null)
                {
                    _context.ColorDetails.Remove(existingColor);
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

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingColor = _context.ColorDetails.Where(x => x.Clname == value).FirstOrDefault<ColorDetails>();
                if (existingColor != null)
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

        [Route("ColorList")]
        public IActionResult ColorList()
        {
            try
            {
                object Color;
                Color = _context.ColorDetails
                                    .OrderBy(c => c.Clname)
                                    .Select(x => new { Id = x.Clid, Value = x.Clname }).ToList();
                return Ok(Color);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetLiningColorList")]
        //public IActionResult GetLiningColorList()
        //{
        //    try
        //    {
        //        object LiningColor;
        //        LiningColor = _context.ColorDetails
        //                            .OrderBy(c => c.Clname)
        //                            .Select(x => new { Id = x.Clid, Value = x.Clname }).ToList();
        //        return Ok(LiningColor);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}
    }
}
