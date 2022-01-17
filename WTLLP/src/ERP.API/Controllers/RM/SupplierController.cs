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
    public class SupplierController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public SupplierController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<SupplierDetails> Get()
        {
            return _context.SupplierDetails.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.SupplierDetails.Where(x => x.SupplierId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCountryList")]
        public IActionResult GetCountryList()
        {
            try
            {
                object countries;
                countries = _context.CountryDetails
                                        .OrderBy(c => c.CountryName)
                                        .Select(x => new { Id = x.CountryId, Value = x.CountryName }).ToList();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetStateList/{CountryId}")]
        public IActionResult GetStateList(int Countryid)
        {
            try
            {
                object states;
                states = _context.StateDetails
                                        .Where(x => x.CountryId == Countryid)
                                        .OrderBy(c => c.StateName)
                                        .Select(x => new { Id = x.StateId, Value = x.StateName }).ToList();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCityList/{StateId}")]
        public IActionResult GetCityList(int Stateid)
        {
            try
            {
                object cities;
                cities = _context.CityDetails
                                        .Where(x => x.StateId == Stateid)
                                        .OrderBy(c => c.CityName)
                                        .Select(x => new { Id = x.CityId, Value = x.CityName }).ToList();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierList")]
        public IActionResult GetSupplierList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewSupplierDetails.OrderByDescending(x => x.SupplierId));
                else
                    return Ok(_context.ViewSupplierDetails.Where(x => x.SupplierName.Contains(search)).OrderByDescending(x => x.SupplierId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplier")]
        public IActionResult GetSupplier()
        {
            try
            {
                object suppList;
                suppList = _context.SupplierDetails
                                         .Where(x => x.BlackListed == 0)
                                         .OrderBy(x => x.SupplierName)
                                         .Select(x => new { Id = x.SupplierId, Value = x.SupplierName }).ToList();
                return Ok(suppList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSupplierId")]
        public IActionResult GetNewSupplierId()
        {
            try
            {
                long SupplierId = 0;
                var lastSupplier = _context.SupplierDetails.OrderBy(x => x.SupplierId).LastOrDefault();
                SupplierId = (lastSupplier == null ? 0 : lastSupplier.SupplierId) + 1;
                return Ok(SupplierId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCurrencyList")]
        public IActionResult GetCurrencyList()
        {
            try
            {
                object currency;
                currency = _context.CurrencyDetails 
                                        .OrderBy(c => c.Cid)
                                        .Select(x => new { Id = x.Cid, Value = x.Csname }).ToList();
                return Ok(currency);
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
                var existingSupplier = _context.SupplierDetails.Where(x => x.SupplierName == value).FirstOrDefault<SupplierDetails>();
                if (existingSupplier != null)
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
        public IActionResult Post([FromBody]SupplierDetails value)
        {
            try
            {
                _context.SupplierDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        public IActionResult Put([FromBody]SupplierDetails value)
        {
            try
            {
                var existingSupplier = _context.SupplierDetails.Where(x => x.SupplierId == value.SupplierId).FirstOrDefault<SupplierDetails>();
                if (existingSupplier == null)
                {
                    return Ok("Not Found");
                }
                existingSupplier.SupplierName = value.SupplierName;
                existingSupplier.SupplierCode = value.SupplierCode;
                existingSupplier.ContactPerson = value.ContactPerson;
                existingSupplier.SupplierAddress = value.SupplierAddress;
                existingSupplier.CountryId = value.CountryId;
                existingSupplier.StateId = value.StateId;
                existingSupplier.CityId = value.CityId;
                existingSupplier.Pincode = value.Pincode;
                existingSupplier.SupplierPhoneNo = value.SupplierPhoneNo;
                existingSupplier.SupplierMobileNo = value.SupplierMobileNo;
                existingSupplier.SupplierFaxNo = value.SupplierFaxNo;
                existingSupplier.SupplierEmail = value.SupplierEmail;
                existingSupplier.SupplierWeb = value.SupplierWeb;
                existingSupplier.SupplierPANNo = value.SupplierPANNo;
                existingSupplier.SupplierCSTNo = value.SupplierCSTNo;
                existingSupplier.SupplierUPTTNo = value.SupplierUPTTNo;
                existingSupplier.SupplierTINNo = value.SupplierTINNo;
                existingSupplier.SupplierLocalTAX = value.SupplierLocalTAX;
                existingSupplier.SupplierTerms = value.SupplierTerms;
                existingSupplier.SupplierDesc = value.SupplierDesc;
                existingSupplier.PType = value.PType;
                existingSupplier.BlackListed = value.BlackListed;
                existingSupplier.CurrencyID = value.CurrencyID;
                existingSupplier.GSTN = value.GSTN; 
                _context.SupplierDetails.Update(existingSupplier);
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
                var existingSupplier = _context.SupplierDetails.Where(x => x.SupplierId == id).FirstOrDefault<SupplierDetails>();
                if (existingSupplier != null)
                {
                    _context.SupplierDetails.Remove(existingSupplier);
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

        [Route("GetSupplierNameList")]
        public IActionResult GetSupplierNameList()
        {
            try
            {
                object suppList;
                suppList = _context.SupplierDetails
                                         .OrderBy(c => c.SupplierName)
                                         .Select(x => new { Id = x.SupplierId, Value = x.SupplierName }).ToList();
                return Ok(suppList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierDetails/{id}")]
        public IActionResult GetSupplierDetails(int id)
        {
            try
            {
                return Ok(_context.ViewSupplierDetails.Where(x => x.SupplierId == id).FirstOrDefault());
                //return Ok(_context.ViewSupplierDetails.Where(x => x.SupplierId == id));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierForPrint/{id}")]
        public IActionResult GetSupplierForPrint(int id)
        {
            try
            {
                return Ok(_context.ViewSupplierDetails.Where(x => x.SupplierId == id).ToList());
                //return Ok(_context.ViewSupplierDetails.Where(x => x.SupplierId == id));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplier/{id}")]
        public IActionResult GetSupplier(long id)
        {
            try
            {
                var SupplierListofSingleRM = _context.RItemSupp.Where(x => x.RItem_Id == id)
                                                .Select(x => x.SupplierId).ToList();
                object suppList;
                suppList = _context.SupplierDetails
                                         .Where(x => SupplierListofSingleRM.Contains(x.SupplierId))
                                         .OrderBy(x => x.SupplierName)
                                         .Select(x => new { Id = x.SupplierId, Value = x.SupplierName })
                                         .ToList();

                return Ok(suppList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierBank/{id}")]
        public IActionResult GetSupplierBank(long id)
        {
            try
            {
                var supplierbank = _context.ViewSupplierBank
                                        .Where(x => x.SupplierId == id)
                                        .OrderBy(x => x.SupplierBankId);
                return Ok(supplierbank);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostSupplierBank")]
        public IActionResult PostSupplierBank([FromBody]SupplierBankDetails value)
        {
            try
            {
                _context.SupplierBankDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetNewSupplierBankId")]
        public IActionResult GetNewSupplierBankId()
        {
            try
            {
                long supplierbankId = 0;
                var lastBuyer = _context.SupplierBankDetails.OrderBy(x => x.SupplierBankId).LastOrDefault();
                supplierbankId = (lastBuyer == null ? 0 : lastBuyer.SupplierBankId) + 1;
                return Ok(supplierbankId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteSupplierBank/{id}")]
        public IActionResult DeleteSupplierBank(int id)
        {
            try
            {
                var existingsupplierbank = _context.SupplierBankDetails.Where(x => x.SupplierBankId == id).FirstOrDefault<SupplierBankDetails>();
                if (existingsupplierbank != null)
                {
                    _context.SupplierBankDetails.Remove(existingsupplierbank);
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

        //[Route("GetSupplierDetails/{id}")]
        //public IQueryable<ViewSupplierDetails> GetSupplierDetails(long id)
        //{
        //    return _context.ViewSupplierDetails.Where(x => x.SupplierId == id).AsQueryable();
        //}
    }
}
