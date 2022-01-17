using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class ContractorController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ContractorController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ContractorDetails> Get()
        {
            return _context.ContractorDetails.AsQueryable();
        }

        //public IQueryable<ViewRMSubCategory> Get()
        //{
        //    return _context.ViewRMSubCategory.AsQueryable();
        //}

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.ContractorDetails.Where(x => x.ContractorID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetContractorRecord")]
        public IActionResult GetContractorRecord(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewContractorDetails.OrderByDescending(x => x.ContractorID));
                else
                    return Ok(_context.ViewContractorDetails.Where(x => x.ContractorName.Contains(search)).OrderByDescending(x => x.ContractorID));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetContractorRecord/{id}")]
        public IActionResult GetContractorRecord(long id)
        {
            try
            {
                return Ok(_context.ViewContractorDetails.Where(x => x.ContractorID == id).FirstOrDefault());
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



        //[Route("GetContractorList")]
        //public IActionResult GetContractorList()
        //{
        //    try
        //    {
        //        var list = _context.ContractorDetails.OrderByDescending(x => x.ContractorID)
        //            .Select(x => new { Id = x.ContractorID , Value = x.ContractorName}).ToList().OrderBy(x => x.Value);
        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetContractorList")]
        public IActionResult GetContractorList()
        {
            try
            {
                var list = _context.ContractorDetails.OrderByDescending(x => x.ContractorID)
                    .Select(x => new { Id = x.ContractorID, Value = x.ContractorName }).ToList().OrderBy(x => x.Value);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetActiveContractorList")]
        public IActionResult GetActiveContractorList()
        {
            try
            {
                var list = _context.ContractorDetails.OrderByDescending(x => x.ContractorID).Where (x=> x.Active_Deactive == 1)
                    .Select(x => new { Id = x.ContractorID, Value = x.ContractorName }).ToList().OrderBy(x => x.Value);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPayableContractorList")]
        public IActionResult GetPayableContractorList()
        {
            try
            {
                var list = _context.ContractorDetails.OrderByDescending(x => x.ContractorID).Where(x=> x.IsPayable == 1 && x.Active_Deactive == 1)
                    .Select(x => new { Id = x.ContractorID, Value = x.ContractorName }).ToList().OrderBy(x => x.Value);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewContractorId")]
        public IActionResult GetNewContractorId()
        {
            try
            {
                long ContractorId = 0;
                var lastContractor = _context.ContractorDetails.OrderBy(x => x.ContractorID).LastOrDefault();
                ContractorId = (lastContractor == null ? 0 : lastContractor.ContractorID) + 1;
                return Ok(ContractorId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ContractorDetails value)
        {
            try
            {
                _context.ContractorDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ContractorDetails value)
        {
            try
            {
                var existingContractor = _context.ContractorDetails.Find(value.ContractorID);
                if (existingContractor == null)
                {
                    return Ok("Not Found");
                }
                existingContractor.ContractorName = value.ContractorName;
                existingContractor.CompanyID = value.CompanyID;
                existingContractor.Phone1 = value.Phone1;
                existingContractor.Mobile1 = value.Mobile1;
                existingContractor.Address = value.Address;
                existingContractor.OpeningBalance = value.OpeningBalance;
                existingContractor.OpeningDate = value.OpeningDate;
                existingContractor.IsPayable = value.IsPayable;
                existingContractor.Active_Deactive = value.Active_Deactive;
                _context.ContractorDetails.Update(existingContractor);
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
                var existingContractor = _context.ContractorDetails.Where(x => x.ContractorName == value).FirstOrDefault<ContractorDetails>();
                if (existingContractor != null)
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
        public IActionResult Delete(long id)
        {
            try
            {
                var existingContractor = _context.ContractorDetails.Where(x => x.ContractorID == id).FirstOrDefault<ContractorDetails>();
                if (existingContractor != null)
                {
                    _context.ContractorDetails.Remove(existingContractor);
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
