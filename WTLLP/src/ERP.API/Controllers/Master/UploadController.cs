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
    public class UploadController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public UploadController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        
       [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.UploadDetails.FirstOrDefault());
        }
    }
}
