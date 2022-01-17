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
    public class SelectionController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public SelectionController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<SelectionDetails> Get()
        {
            return _context.SelectionDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.SelectionDetails.Where(x => x.SelectionID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSelectionList")]
        public IActionResult GetSelectionList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Ok(_context.SelectionDetails.OrderByDescending(x => x.SelectionID));
                }
                else
                {
                    return Ok(_context.SelectionDetails
                        .Where(x => x.Selection.Contains(search))
                        .OrderByDescending(x => x.SelectionID));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSelectionid")]
        public IActionResult GetNewSelectionid()
        {
            try
            {
                long SelectionID = 0;
                var lastSelection = _context.SelectionDetails.OrderBy(x => x.SelectionID).LastOrDefault();
                SelectionID = (lastSelection == null ? 0 : lastSelection.SelectionID) + 1;
                return Ok(SelectionID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]SelectionDetails value)
        {
            try
            {
                _context.SelectionDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]SelectionDetails value)
        {
            try
            {
                var existingSelection = _context.SelectionDetails.Find(value.SelectionID);
                if (existingSelection == null)
                {
                    return Ok("Not Found");
                }
                existingSelection.Selection = value.Selection;
                existingSelection.SelectionDesc = value.SelectionDesc;
                _context.SelectionDetails.Update(existingSelection);
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
                var existingSelection = _context.SelectionDetails.Where(x => x.Selection == value).FirstOrDefault<SelectionDetails>();
                if (existingSelection != null)
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

        //[HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingSelection = _context.SelectionDetails.Find(id);
                if (existingSelection != null)
                {
                    _context.SelectionDetails.Remove(existingSelection);
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
