using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using ERP.Domain.Entities;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ComponentController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ComponentController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ComponentDetails> Get()
        {
            return _context.ComponentDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.ViewComponentDetails.Where(x => x.Comp_Id  == id).FirstOrDefault());
        }

        [Route("GetComponentList")]
        public IActionResult GetComponentList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.ViewComponentDetails.OrderBy(x => x.Comp_Name ));
            }
            else
            {
                return Ok(_context.ViewComponentDetails
                    .Where(x => x.Comp_Name.Contains(search))
                    .OrderBy(x => x.Comp_Name));
            }
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            long id = 0;
            var obj = _context.ComponentDetails.OrderBy(x => x.CompId).LastOrDefault();
            id = (obj == null ? 0 : obj.CompId) + 1;
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ComponentDetails value)
        {
            _context.ComponentDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]ComponentDetails value)
        {
            var existing = _context.ComponentDetails.Find(value.CompId);
            if (existing == null)
            {
                return NotFound();
            }
            existing.CompName = value.CompName;
            existing.CompDescr = value.CompDescr;
            _context.ComponentDetails.Update(existing);
            existing.CompGroupId  = value.CompGroupId ;
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existing = _context.ComponentDetails.Where(x => x.CompName == value).FirstOrDefault<ComponentDetails>();

            if (existing != null)
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
            var existing = _context.ComponentDetails.Find(id);
            if (existing != null)
            {
                _context.ComponentDetails.Remove(existing);
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
                Obj = _context.ComponentDetails.OrderBy(c => c.CompName).Select(x => new { Id = x.CompId, Value = x.CompName }).ToList();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++
        [Route("SearchComponentList")]
        public ActionResult SearchComponentList([FromBody]SearchModel obj)
        {
            object searchcomponent;
            if (obj.searchvalue == null)
            {
                searchcomponent = _context.ViewComponentDetails
                                        .OrderBy(x => x.Comp_Name)
                                        .Select(x => new { Comp_Id = x.Comp_Id, Comp_Name = x.Comp_Name })
                                        .ToList();
            }
            else
            {
                searchcomponent = _context.ViewComponentDetails
                                        .Where(x => x.Comp_Name.Contains(obj.searchvalue))
                                         .Select(x => new { Comp_Id = x.Comp_Id, Comp_Name = x.Comp_Name })
                                        .ToList();
            }
            return Ok(searchcomponent);
        }
    }
}
