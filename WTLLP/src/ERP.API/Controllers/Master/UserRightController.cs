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
    public class UserRightController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public UserRightController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("GetmenuList/{id}")]
        public IActionResult GetmenuList(int id)
        {
            try
            {
                var Existingmodel = _context.MenuDetails
                .Where(x => x.MenuCategoryID == id).OrderBy(x=> x.MenuName).ToList();
                return Ok(Existingmodel);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetUserList")]
        public IActionResult GetUserList()
        {
            try
            {
                object record;
                record = _context.UserDetails
                                        .OrderBy(c => c.LoginName)
                                        .Select(x => new { Id = x.UserId, Value = x.LoginName }).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMenuCatList")]
        public IActionResult GetMenuCatList()
        {
            try
            {
                object record;
                record = _context.MenuCategory
                                        .Where(x => x.MenuCategoryName !="Tools")
                                        .OrderBy(x => x.MenuCategoryID)
                                        .Select(x => new { Id = x.MenuCategoryID, Value = x.MenuCategoryName }).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("SearchMenuList/{id}")]
        public ActionResult SearchMenuList(int id)
        {
            object record;
            record = _context.MenuDetails.Where(x => x.MenuCategoryID == id).OrderBy(x=> x.MenuName).ToList();
            return Ok(record);
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                long id = 0;
                var record = _context.UserRights.OrderBy(x => x.UserRightsId).LastOrDefault();
                id = (record == null ? 0 : record.UserRightsId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult PostUserMenu([FromBody]UserRights value)
        {
            try
            {
                _context.UserRights.Add(value);

                var record = _context.UserRights.Where(x => x.MenuId == value.MenuId && x.UserId == value.UserId).ToList();

                foreach (var recordM in record.ToList())
                {
                    _context.UserRights.Remove(recordM);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RemoveMenu")]
        public IActionResult RemoveMenu([FromBody]UserRights value)
        {
            try
            {
                var record = _context.UserRights
                                         .Where(x => x.UserId == value.UserId && x.MenuId == value.MenuId)
                                         .SingleOrDefault();
                if (record != null)
                {
                    // Delete children
                    _context.UserRights.Remove(record);
                    _context.SaveChanges();
                }
                else
                {
                    return Ok("Not Found");
                }
                return Ok("Removed Assigned Menu");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewMenuList/{id}")]
        public IQueryable<ViewUserRights> GetViewMenuList(int id)
        {
            return _context.ViewUserRights.Where(x => x.UserId == id).OrderBy(x=> x.MenuCategoryID).ThenBy(x=> x.MenuName).AsQueryable();
        }

        [Route("GetCatList/{id}")]
        public IActionResult GetCatList(int id)
        {
            try
            {
                object record;
                record = _context.ViewUserRights.Where (x=> x.UserId==id)
                                        .OrderBy(x => x.MenuCategoryID)
                                        .Select(x => new { Id = x.MenuCategoryID, Value = x.MenuCategoryName }).ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
