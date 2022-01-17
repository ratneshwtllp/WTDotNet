using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ERP.DataAccess;
using ERP.Domain.Entities;

namespace ERP.Api.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public CurrencyController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<CurrencyDetails> Get()
        {
            return _context.CurrencyDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.CurrencyDetails.Where(x => x.Cid == id).FirstOrDefault());
        }

        [Route("GetCurrencyList")]
        public IActionResult GetCurrencyList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.CurrencyDetails.OrderByDescending(x => x.Cid));
            else
                return Ok(_context.CurrencyDetails
                    .Where(x => x.Cname.Contains(search) || x.Csname.Contains(search))
                    .OrderByDescending(x => x.Cid));
        }

        [Route("GetCurrencyNameList")]
        public IActionResult GetCurrencyNameList()
        {
            try
            {
                var currencylist = _context.CurrencyDetails
                            .OrderBy(x => x.Cname)
                            .Select(x => new { Id = x.Cid, Value = x.Cname }).ToList();
                return Ok(currencylist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCurrencyId")]
        public IActionResult GetNewCurrencyId()
        {
            long CurrencyId = 0;
            var lastCurrency = _context.CurrencyDetails.OrderBy(x => x.Cid).LastOrDefault();
            CurrencyId = (lastCurrency == null ? 0 : lastCurrency.Cid) + 1;
            return Ok(CurrencyId);
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existingCurrency = _context.CurrencyDetails.Where(x => x.Cname == value).FirstOrDefault<CurrencyDetails>();

            if (existingCurrency != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]CurrencyDetails value)
        {
            _context.CurrencyDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Put([FromBody]CurrencyDetails value)
        {
            var existingCurrency = _context.CurrencyDetails
                         .Where(x => x.Cid == value.Cid).SingleOrDefault();
            //.Include(x => x.CurrencyHistoryDetails).SingleOrDefault();


            //var existingCurrency = _context.CurrencyDetails
            //    .Where(x => x.Cid == value.Cid).SingleOrDefault();

            if (existingCurrency == null)
            {
                return NotFound();
            }
            existingCurrency.Csname = value.Csname;
            existingCurrency.Cname = value.Cname;
            existingCurrency.Crate = value.Crate;
            _context.CurrencyDetails.Update(existingCurrency);
            // insert Currency History
            foreach (var CurrHistory in value.CurrencyHistoryDetails)
            {
                CurrencyHistoryDetails CH = new CurrencyHistoryDetails();
                CH.CurrencyHistoryId = CurrHistory.CurrencyHistoryId;
                CH.CID = value.Cid;
                CH.ChangeDate = CurrHistory.ChangeDate;
                CH.PreviousPrice = CurrHistory.PreviousPrice;
                CH.ChangePrice = CurrHistory.ChangePrice;
                CH.UserId = CurrHistory.UserId;
                // existingCurrency.CurrencyHistoryDetails.Add(CH);

                _context.CurrencyHistoryDetails.Add(CH);
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingCurrency = _context.CurrencyDetails.Where(x => x.Cid == id).FirstOrDefault<CurrencyDetails>();

            if (existingCurrency != null)
            {
                _context.CurrencyDetails.Remove(existingCurrency);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        // for Currency History
        [HttpGet]
        public IQueryable<CurrencyHistoryDetails> GetCurrencyHistory()
        {
            return _context.CurrencyHistoryDetails.AsQueryable();
        }

        [Route("GetNewCurrencyHistoryId")]
        public IActionResult GetNewCurrencyHistoryId()
        {
            long CurrencyHistoryId = 0;
            var lastCurrency = _context.CurrencyHistoryDetails.OrderBy(x => x.CurrencyHistoryId).LastOrDefault();
            CurrencyHistoryId = (lastCurrency == null ? 0 : lastCurrency.CurrencyHistoryId) + 1;
            return Ok(CurrencyHistoryId);
        }
        [Route("GetPreviuosPrice/{cid}")]
        public IActionResult GetPreviuosPrice(int cid)
        {
            decimal PreviousPrice = 0;
            var Currency = _context.CurrencyDetails.Where(x => x.Cid == cid).LastOrDefault();
            PreviousPrice = Currency.Crate;
            return Ok(PreviousPrice);
            // return Ok(_context.CurrencyDetails.Where(x => x.Cid == cid).Select(x => x.Crate));
        }

        [Route("GetCurrencyHistoryList/{cid}")]
        public IActionResult GetCurrencyHistoryList(int cid)
        {
            return Ok(_context.ViewCurrencyHistory
                .Where(x => x.Cid == cid)
                    .OrderByDescending(x => x.CurrencyHistoryId).ToList ());
        }
    }
}
