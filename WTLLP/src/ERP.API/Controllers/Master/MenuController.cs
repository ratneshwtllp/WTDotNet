using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
namespace ERP.API
{
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public MenuController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ViewMenu> Get()
        {
            return _context.ViewMenu.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.MenuDetails.Where(x => x.MenuID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("MenuCategoryNameList")]
        public IActionResult MenuCategoryNameList()
        {
            try
            {
                object menucategory;
                menucategory = _context.MenuCategory
                                        .OrderBy(c => c.MenuCategoryID)
                                        .Select(x => new { Id = x.MenuCategoryID, Value = x.MenuCategoryName }).ToList();
                return Ok(menucategory);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetMenuDetailsList")]
        public IActionResult GetMenuDetailsList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewMenu.OrderBy(x => x.MenuCategoryID).ThenBy(x=> x.MenuName));
                else
                    return Ok(_context.ViewMenu.Where(x => x.MenuName.Contains(search)).OrderBy(x => x.MenuCategoryID).ThenBy(x => x.MenuName));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList")]
        public IActionResult GetList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewMenu.OrderBy(x => x.MenuCategoryName).ThenBy (x=> x.MenuName));
                else
                    return Ok(_context.ViewMenu.Where(x => x.MenuName.Contains(search)).OrderByDescending(x => x.MenuID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                long id = 0;
                var record = _context.MenuDetails.OrderBy(x => x.MenuID).LastOrDefault();
                id = (record == null ? 0 : record.MenuID) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]MenuDetails value)
        {
            try
            {
                var record = _context.MenuDetails.Find(value.MenuID);
                if (record == null)
                {
                    return Ok("Not Found");
                }
                record.MenuName = value.MenuName;
                record.MenuURL = "-";
                record.MenuCategoryID = value.MenuCategoryID;
                record.MenuArea = value.MenuArea;
                record.MenuController = value.MenuController;
                record.MenuAction = value.MenuAction;
                _context.MenuDetails.Update(record);
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
                var record = _context.MenuDetails.Where(x => x.MenuName == value).FirstOrDefault<MenuDetails>();
                if (record != null)
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
                var record = _context.MenuDetails.Where(x => x.MenuID == id).FirstOrDefault<MenuDetails>();
                if (record != null)
                {
                    _context.MenuDetails.Remove(record);
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

        [HttpPost]
        public IActionResult Post([FromBody]MenuDetails value)
        {
            try
            {
                _context.MenuDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
