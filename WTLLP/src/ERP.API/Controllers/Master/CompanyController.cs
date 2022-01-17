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
    public class CompanyController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public CompanyController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<CompanyDetails> Get()
        {
            return _context.CompanyDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CompanyDetails.Where(x => x.Id == id).FirstOrDefault());
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
                    return Ok(_context.CompanyDetails.OrderBy(x => x.CName));
                else
                    return Ok(_context.CompanyDetails
                        .Where(x => x.CName.Contains(search))
                        .OrderBy(x => x.CName));
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
                int ComapnyId = 0;
                var lastCompany = _context.CompanyDetails.OrderBy(x => x.Id).LastOrDefault();
                ComapnyId = (lastCompany == null ? 0 : lastCompany.Id) + 1;
                return Ok(ComapnyId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost] 
        public IActionResult Post([FromBody]CompanyDetails value)  
        {
            try
            {
                _context.CompanyDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]CompanyDetails value)
        {
            try
            {
                var existingcompany = _context.CompanyDetails.Where(x => x.Id == value.Id).FirstOrDefault<CompanyDetails>();
                if (existingcompany == null)
                {
                    return Ok("Not Found");
                }
                existingcompany.CName = value.CName;
                existingcompany.AddressOffice = value.AddressOffice;
                existingcompany.AddressWork = value.AddressWork;
                existingcompany.Phone = value.Phone;
                existingcompany.Fax = value.Fax;
                existingcompany.Email = value.Email;
                existingcompany.Web = value.Web;
                existingcompany.CSTT = value.CSTT;
                existingcompany.UPTT = value.UPTT;
                existingcompany.TIN = value.TIN;
                existingcompany.IECode = "";
                existingcompany.EmpEsiCode1 = "1";
                existingcompany.EmpEsiCode2 = "1";
                existingcompany.EmpEsiCode3 = "1";
                existingcompany.EmpPfEsttCode = "1";
                existingcompany.Excise = 0;
                existingcompany.CustomTeriffNo = "1";
                existingcompany.PANNo = value.PANNo;         
                existingcompany.GSTN = value.GSTN;
                existingcompany.POFooter1  = value.POFooter1;
                existingcompany.POFooter2  = value.POFooter2;
                existingcompany.LogoPath = value.LogoPath;
                _context.CompanyDetails.Update(existingcompany);
                _context.SaveChanges();
                return Ok("Company Details Updated.");
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
                var existingcompany = _context.CompanyDetails.Where(x => x.Id == id).FirstOrDefault<CompanyDetails>();
                if (existingcompany != null)
                {
                    _context.CompanyDetails.Remove(existingcompany);
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

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingcompany = _context.CompanyDetails.Where(x => x.CName == value).FirstOrDefault<CompanyDetails>();
                if (existingcompany != null)
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

        [Route("GetCompanyList")]
        public IActionResult GetCompanyList()
        {
            try
            {
                object company;
                company = _context.CompanyDetails
                                        .OrderBy(c => c.CName)
                                        .Select(x => new { Id = x.Id, Value = x.CName }).ToList();
                return Ok(company);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

    }
}
