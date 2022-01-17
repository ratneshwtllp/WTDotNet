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
    public class BuyerController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public BuyerController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<BuyerDetails> Get()
        {
            return _context.BuyerDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.BuyerDetails.Where(x => x.BuyerId == id).FirstOrDefault());
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

        [Route("BuyerList")]
        public IActionResult BuyerList()
        {
            try
            {
                object Buyers;
                Buyers = _context.BuyerDetails
                                    .OrderBy(c => c.BuyerName)
                                    .Select(x => new { Id = x.BuyerId, Value = x.BuyerName }).ToList();
                return Ok(Buyers);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerList")]
        public IActionResult GetBuyerList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewBuyerDetails.OrderByDescending(x => x.BuyerId));
                else
                    return Ok(_context.ViewBuyerDetails
                        .Where(x => x.BuyerCode.Contains(search) || x.BuyerName.Contains(search) || x.ContactPerson.Contains(search) || x.BuyerAddress.Contains(search) || x.CityName.Contains(search) || x.BuyerEmail.Contains(search) || x.BuyerPhoneNo.Contains(search) || x.BuyerMobileNo.Contains(search) || x.CName.Contains(search))
                        .OrderByDescending(x => x.BuyerId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBuyerId")]
        public IActionResult GetNewBuyerId()
        {
            try
            {
                long BuyerId = 0;
                var lastBuyer = _context.BuyerDetails.OrderBy(x => x.BuyerId).LastOrDefault();
                BuyerId = (lastBuyer == null ? 0 : lastBuyer.BuyerId) + 1;
                return Ok(BuyerId);
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
                var existingBuyer = _context.BuyerDetails.Where(x => x.BuyerName == value).FirstOrDefault<BuyerDetails>();
                if (existingBuyer != null)
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
        public IActionResult Post([FromBody]BuyerDetails value)
        {
            try
            {
                _context.BuyerDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]BuyerDetails value)
        {
            try
            {
                var existingBuyer = _context.BuyerDetails
                       .Where(x => x.BuyerId == value.BuyerId)
                       .Include(x => x.BuyerConsigneeDetails)
                       .SingleOrDefault();

                if (existingBuyer != null)
                {
                    // Update parent
                    //_context.Entry(existingbuyerconsignee).CurrentValues.SetValues(value);
                    existingBuyer.BuyerName = value.BuyerName;
                    existingBuyer.BuyerCode = value.BuyerCode;
                    existingBuyer.ContactPerson = value.ContactPerson;
                    existingBuyer.BuyerCode = value.BuyerCode;
                    existingBuyer.CountryId = value.CountryId;
                    existingBuyer.StateId = value.StateId;
                    existingBuyer.CityId = value.CityId;
                    existingBuyer.Pincode = value.Pincode;
                    existingBuyer.BuyerPhoneNo = value.BuyerPhoneNo;
                    existingBuyer.BuyerMobileNo = value.BuyerMobileNo;
                    existingBuyer.BuyerFaxNo = value.BuyerFaxNo;
                    existingBuyer.BuyerEmail = value.BuyerEmail;
                    existingBuyer.BuyerWeb = value.BuyerWeb;
                    existingBuyer.BuyerPANNo = value.BuyerPANNo;
                    existingBuyer.BuyerCSTNo = value.BuyerCSTNo;
                    existingBuyer.BuyerUPTTNo = value.BuyerUPTTNo;
                    existingBuyer.BuyerTINNo = value.BuyerTINNo;
                    existingBuyer.BuyerLocalTAX = value.BuyerLocalTAX;
                    existingBuyer.BuyerTerms = value.BuyerTerms;
                    existingBuyer.BuyerDesc = value.BuyerDesc;
                    existingBuyer.PType = value.PType;
                    existingBuyer.BlackListed = value.BlackListed;
                    existingBuyer.CurrencyID = value.CurrencyID;

                    existingBuyer.BuyerUserID = value.BuyerUserID;
                    existingBuyer.BuyerPW = value.BuyerPW;
                    existingBuyer.FOB_CIF = value.FOB_CIF;
                    if (value.LogoPath != null)
                    {
                        existingBuyer.LogoPath = value.LogoPath;
                    }
                    existingBuyer.IsActive = value.IsActive;
                    existingBuyer.IsAllowLogin = value.IsAllowLogin;

                    existingBuyer.IsGeneralBuyer = value.IsGeneralBuyer;
                    existingBuyer.Online_Transfer = value.Online_Transfer;
                    _context.BuyerDetails.Update(existingBuyer);
                    _context.SaveChanges();

                    // Delete children
                    foreach (var Existingconsignee in existingBuyer.BuyerConsigneeDetails.ToList())
                    {
                        _context.BuyerConsigneeDetails.Remove(Existingconsignee);
                    }

                    // Update and Insert children
                    foreach (var consignee in value.BuyerConsigneeDetails)
                    {
                        // Insert child 1
                        BuyerConsigneeDetails newChild = new BuyerConsigneeDetails();
                        newChild.BuyerConsigneeId = consignee.BuyerConsigneeId;
                        newChild.BuyerId = consignee.BuyerId;
                        newChild.ConsigneeId = consignee.ConsigneeId;
                        newChild.EntryDate = consignee.EntryDate;
                        newChild.SessionYear = consignee.SessionYear;
                        newChild.UserId = consignee.UserId;
                        existingBuyer.BuyerConsigneeDetails.Add(newChild);
                    }
                    _context.SaveChanges();

                }
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
                var existingBuyer = _context.BuyerDetails.Where(x => x.BuyerId == id).FirstOrDefault<BuyerDetails>();
                if (existingBuyer != null)
                {
                    _context.BuyerDetails.Remove(existingBuyer);
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

        [Route("GetBuyerNameList")]
        public IActionResult GetBuyerNameList()
        {
            try
            {
                object buyer;
                buyer = _context.BuyerDetails
                                        .OrderBy(c => c.BuyerName)
                                        .Select(x => new { Id = x.BuyerId, Value = x.BuyerName }).ToList();
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerCodeList")]
        public IActionResult GetBuyerCodeList()
        {
            try
            {
                object buyer;
                buyer = _context.BuyerDetails
                                        .OrderBy(c => c.BuyerCode)
                                        .Select(x => new { Id = x.BuyerId, Value = x.BuyerCode }).ToList();
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerDetails/{buyerid}")]
        public IActionResult GetBuyerDetails(long buyerid)
        {
            try
            {
                object buyer;
                buyer = _context.ViewBuyerDetails.Where(x => x.BuyerId == buyerid).ToList();
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBuyerConsigneeId")]
        public IActionResult GetNewBuyerConsigneeId()
        {
            try
            {
                long BuyerConsigneeId = 0;
                var lastconsignee = _context.BuyerConsigneeDetails.OrderBy(x => x.BuyerConsigneeId).LastOrDefault();
                BuyerConsigneeId = (lastconsignee == null ? 0 : lastconsignee.BuyerConsigneeId) + 1;
                return Ok(BuyerConsigneeId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerConsignee/{buyerid}")]
        public ActionResult GetBuyerConsignee(long buyerid)
        {
            try
            {
                var obj = _context.BuyerConsigneeDetails.Where(x => x.BuyerId == buyerid).AsQueryable();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetBuyerBank/{id}")]
        public IActionResult GetBuyerBank(long id)
        {
            try
            {
                var buyerbank = _context.ViewBuyerBank
                                        .Where(x => x.BuyerId == id)
                                        .OrderBy(x => x.BuyerBankId);
                return Ok(buyerbank);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostBuyerBank")]
        public IActionResult PostBuyerBank([FromBody]BuyerBankDetails value)
        {
            try
            {
                _context.BuyerBankDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewBuyerBankId")]
        public IActionResult GetNewBuyerBankId()
        {
            try
            {
                long BuyerbankId = 0;
                var lastBuyer = _context.BuyerBankDetails.OrderBy(x => x.BuyerBankId).LastOrDefault();
                BuyerbankId = (lastBuyer == null ? 0 : lastBuyer.BuyerBankId) + 1;
                return Ok(BuyerbankId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteBuyerBank/{id}")]
        public IActionResult DeleteBuyerBank(int id)
        {
            try
            {
                var existingBuyerbank = _context.BuyerBankDetails.Where(x => x.BuyerBankId == id).FirstOrDefault<BuyerBankDetails>();
                if (existingBuyerbank != null)
                {
                    _context.BuyerBankDetails.Remove(existingBuyerbank);
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

        [Route("BuyerConsigneePrint/{id}")]
        public IActionResult BuyerConsigneePrint(long id)
        {
            try
            {
                var buyerbank = _context.ViewBuyerConsignee
                                        .Where(x => x.BuyerId == id)
                                        .OrderBy(x => x.BuyerConsigneeId);
                return Ok(buyerbank);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
