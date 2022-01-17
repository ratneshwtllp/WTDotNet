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
    public class GroupController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public GroupController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<GroupDetails> Get()
        {
            return _context.GroupDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.GroupDetails.Where(x => x.GroupId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetGroupList")]
        public IActionResult GetGroupList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.GroupDetails.OrderByDescending(x => x.GroupId));
                else
                    return Ok(_context.GroupDetails.Where(x => x.GroupName.Contains(search)).OrderByDescending(x => x.GroupId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewGroupId")]
        public IActionResult GetNewGroupId()
        {
            try
            {
                long GroupId = 0;
                var lastGroup = _context.GroupDetails.OrderBy(x => x.GroupId).LastOrDefault();
                GroupId = (lastGroup == null ? 0 : lastGroup.GroupId) + 1;
                return Ok(GroupId);
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
                var existingGroup = _context.GroupDetails.Where(x => x.GroupName == value).FirstOrDefault<GroupDetails>();
                if (existingGroup != null)
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
        public IActionResult Post([FromBody]GroupDetails value)
        {
            try
            {
                _context.GroupDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]GroupDetails value)
        {
            try
            {
                var existingGroup = _context.GroupDetails.Where(x => x.GroupId == value.GroupId).FirstOrDefault<GroupDetails>();
                if (existingGroup == null)
                {
                    return Ok("Not Found");
                }
                existingGroup.GroupName = value.GroupName;
                existingGroup.GroupDesc = value.GroupDesc;
                _context.GroupDetails.Update(existingGroup);
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
                var existingGroup = _context.GroupDetails.Where(x => x.GroupId == id).FirstOrDefault<GroupDetails>();
                if (existingGroup != null)
                {
                    _context.GroupDetails.Remove(existingGroup);
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
        [Route("GroupList")]
        public IActionResult GroupList()
        {
            try
            {
                object Group;
                Group = _context.GroupDetails
                                    .OrderBy(c => c.GroupName)
                                    .Select(x => new { Id = x.GroupId, Value = x.GroupName }).ToList();
                return Ok(Group);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
