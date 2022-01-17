using ERP.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class DBBackup : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public DBBackup(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [Route("Create")]
        public IActionResult Create()
        {
            try
            {
                _context.Database.ExecuteSqlCommand("EXEC SP_CreateBackup");
                return Ok("Bakup Complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

    }
}
