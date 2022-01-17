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
    public class PrintController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PrintController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<PrintDetails> Get()
        {
            return _context.PrintDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.PrintDetails.Where(x => x.PrintId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPrintList")]
        public IActionResult GetPrintList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.PrintDetails.OrderByDescending(x => x.PrintId));
                else
                    return Ok(_context.PrintDetails
                        .Where(x => x.Print.Contains(search))
                        .OrderByDescending(x => x.PrintId));

                //return Ok(_context.PrintDetails
                //    .Where(x => x.PT.Contains(search) || x.Usname.Contains(search))
                //    .OrderByDescending(x => x.Uid));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewPrintId")]
        public IActionResult GetNewPrintId()
        {
            try
            {
                long PrintId = 0;
                var lastprint = _context.PrintDetails.OrderBy(x => x.PrintId).LastOrDefault();
                PrintId = (lastprint == null ? 0 : lastprint.PrintId) + 1;
                return Ok(PrintId);
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
                var existingPrint = _context.PrintDetails.Where(x => x.Print == value).FirstOrDefault<PrintDetails>();
                if (existingPrint  != null)
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
        public IActionResult Post([FromBody]PrintDetails value)
        {
            try
            {
                _context.PrintDetails.Add(value);
                _context.SaveChanges();
                //return Ok("1");
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]PrintDetails value)
        {
            try
            {
                var existingPrint  = _context.PrintDetails.Where(x => x.PrintId == value.PrintId).FirstOrDefault<PrintDetails>();
                if (existingPrint  == null)
                {
                    //return NotFound();
                    //return Ok("0");
                    return Ok("Not Found");
                }
                existingPrint.Print = value.Print;
                existingPrint.PrintDesc = value.PrintDesc; 
                _context.PrintDetails.Update(existingPrint);
                _context.SaveChanges();
                //return new NoContentResult();
                //return Ok("1");
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
                var existingPrint  = _context.PrintDetails.Where(x => x.PrintId == id).FirstOrDefault<PrintDetails>();
                if (existingPrint  != null)
                {
                    _context.PrintDetails.Remove(existingPrint);
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

        [Route("GetListPrint")]
        public IActionResult GetListPrint()
        {
            try
            {
                object print;
                print = _context.PrintDetails
                                    .OrderBy(c => c.PrintId)
                                    .Select(x => new { Id = x.PrintId, Value = x.Print }).ToList();
                return Ok(print);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
