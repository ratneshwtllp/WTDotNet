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
    public class CardController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public CardController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<CardsFolder_Details> Get()
        {
            return _context.CardsFolder_Details.AsQueryable();   
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.CardsFolder_Details.Where(x => x.CardId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCardList")]
        public IActionResult GetCardList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.CardsFolder_Details.OrderByDescending(x => x.CardId));
                else
                    return Ok(_context.CardsFolder_Details
                        .Where(x => x.Representetive_Name.Contains(search) || x.Firm_Name.Contains(search) || x.Address.Contains(search) || x.Website.Contains(search) || x.Email.Contains(search) || x.Description.Contains(search) || x.Designetion.Contains(search) || x.Phone1.Contains(search) || x.Mobile1.Contains(search))
                        .OrderByDescending(x => x.CardId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewCardId")]
        public IActionResult GetNewCardId()
        {
            try
            {
                int CardId = 0;
                var lastCard = _context.CardsFolder_Details.OrderBy(x => x.CardId).LastOrDefault();
                CardId = (lastCard == null ? 0 : lastCard.CardId) + 1;
                return Ok(CardId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("CheckDuplicate")]
        //public IActionResult CheckDuplicate(string value)
        //{
        //    try
        //    {
        //        var existingCard = _context.CardsFolder_Details.Where(x => x.BuyerName == value).FirstOrDefault<BuyerDetails>();
        //        if (existingCard != null)
        //        {
        //            return Ok(1);
        //        }
        //        else
        //        {
        //            return Ok(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPost]
        public IActionResult Post([FromBody]CardsFolder_Details value)
        {
            try
            {
                _context.CardsFolder_Details.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        
        public IActionResult Put([FromBody]CardsFolder_Details value)
        {
            try
            {
                var existingCard = _context.CardsFolder_Details.Where(x => x.CardId == value.CardId).FirstOrDefault<CardsFolder_Details>();
                if (existingCard == null)
                {
                    return Ok("Not Found");
                }
                existingCard.Representetive_Name = value.Representetive_Name;
                existingCard.Firm_Name = value.Firm_Name;
                existingCard.Address = value.Address;
                existingCard.Website = value.Website;
                existingCard.Email = value.Email;
                existingCard.Description = value.Description;
                existingCard.Designetion = value.Designetion;
                existingCard.Phone1 = value.Phone1;
                existingCard.Phone2 = value.Phone2;
                existingCard.Phone3 = value.Phone3;
                existingCard.Mobile1 = value.Mobile1;
                existingCard.Mobile2 = value.Mobile2;
                existingCard.Mobile3 = value.Mobile3;
                _context.CardsFolder_Details.Update(existingCard);
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
                var existingCard = _context.CardsFolder_Details.Where(x => x.CardId == id).FirstOrDefault<CardsFolder_Details>();
                if (existingCard != null)
                {
                    _context.CardsFolder_Details.Remove(existingCard);
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
