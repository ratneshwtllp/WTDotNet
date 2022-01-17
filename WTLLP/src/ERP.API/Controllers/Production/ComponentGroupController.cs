using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using ERP.Domain.Entities;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ComponentGroupController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ComponentGroupController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<ComponentGroupDetails> Get()
        {
            return _context.ComponentGroupDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.ComponentGroupDetails.Where(x => x.CompGroupId == id).FirstOrDefault());
        }

        [Route("GetComponentGroupList")]
        public IActionResult GetComponentGroupList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.ComponentGroupDetails.OrderBy(x => x.CompGroupName));
            }
            else
            {
                return Ok(_context.ComponentGroupDetails
                    .Where(x => x.CompGroupName.Contains(search))
                    .OrderBy(x => x.CompGroupName));
            }
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            long id = 0;
            var obj = _context.ComponentGroupDetails.OrderBy(x => x.CompGroupId ).LastOrDefault();
            id = (obj == null ? 0 : obj.CompGroupId) + 1;
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ComponentGroupDetails value)
        {
            _context.ComponentGroupDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]ComponentGroupDetails value)
        {
            var existing = _context.ComponentGroupDetails.Find(value.CompGroupId);
            if (existing == null)
            {
                return NotFound();
            }
            existing.CompGroupName = value.CompGroupName;
            existing.CompGroupRemark = value.CompGroupRemark;
            _context.ComponentGroupDetails.Update(existing);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existing = _context.ComponentGroupDetails.Where(x => x.CompGroupName == value).FirstOrDefault<ComponentGroupDetails>();

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
            var existing = _context.ComponentGroupDetails.Find(id);
            if (existing != null)
            {
                _context.ComponentGroupDetails.Remove(existing);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        [Route("ComponentGroupList")]
        public IActionResult ComponentGroupList()
        {
            try
            {
                var list = _context.ComponentGroupDetails.OrderBy(x => x.CompGroupName )
                    .Select(x => new { id = x.CompGroupId , value = x.CompGroupName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
