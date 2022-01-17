using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ERP.Api
{
    [Route("api/[controller]")]
    public class AttandanceController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public AttandanceController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<HR_ViewAttandance> Get()
        {
            return _context.HR_ViewAttandance.AsQueryable();
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<HR_AttandanceDetails> value)
        {
            try
            {
                var oldrecord = _context.HR_AttandanceDetails.Where(x=> x.AttandanceDate==value[0].AttandanceDate).ToList();
                _context.HR_AttandanceDetails.RemoveRange(oldrecord);
                _context.SaveChanges();

                long id = 0;
                var record = _context.HR_AttandanceDetails.OrderBy(x => x.AttandanceID).LastOrDefault();
                id = (record == null ? 0 : record.AttandanceID) + 1;

                for (int i= 0;i < value.Count;i++)
                {
                    value[i].AttandanceID = id;
                    id += 1;
                    _context.HR_AttandanceDetails.Add(value[i]);
                    _context.SaveChanges();
                }
                //_context.HR_AttandanceDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("List")]
        public IActionResult List([FromBody] AttandanceSearchModel Obj)
        {
            try
            {
                IQueryable<HR_ViewAttandance> query = _context.HR_ViewAttandance;

                if (Obj.IsEmployee == true )
                    query = query.Where(x => x.EmployeeId == Obj.EmployeeId);

                if (Obj.IsDate == true)
                    query = query.Where(x => x.AttandanceDate >= Obj.AttandanceDateFrom && x.AttandanceDate <= Obj.AttandanceDateTo);

                if (Obj.IsMaxDate == true)
                {
                    DateTime dt;
                    dt = _context.HR_AttandanceDetails.Max(x => x.AttandanceDate);
                    if (dt == null)
                        dt = DateTime.Now.Date;
                    query = query.Where(x => x.AttandanceDate == dt);
                }

                query = query.OrderBy(x=> x.AttandanceID);
                var record = query.ToList();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [Route("AttandanceStatusList")]
        public IActionResult AttandanceStatusList()
        {
            try
            {
                object employee;
                employee = _context.HR_AttandanceStatus
                                        .OrderBy(c => c.AttandanceStatusId)
                                        .Select(x => new { Id = x.AttandanceStatusId, Value = x.AttandanceStatus }).ToList();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
