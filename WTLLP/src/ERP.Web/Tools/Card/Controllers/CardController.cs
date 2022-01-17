using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Card.Models;

namespace ERP.Web.Areas.Card.Controllers
{
    [AuthLogin]
    [Area("Card")]
    public class CardController : Controller
    {
        CardModel _cardmodel = new CardModel();
        IHostingEnvironment _env;
        ICardContract _icard;

        public CardController(IHostingEnvironment env, ICardContract card)
        {
            _env = env;
            _icard = card;
        }
        
        public ActionResult CardList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CardList " + ex.Message);
            }
        }

        public ActionResult CardList_Partial(String search)
        {
            try
            {
                return PartialView("_CardList", _icard.GetCardList(search).Result);

            }
            catch (Exception ex)
            {
                return Json("CardList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateCard()
        {
            try
            {
                _cardmodel = new CardModel();
                _cardmodel.CardId = _icard.GetNewCardId().Result;
                return View(_cardmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateCard " + ex.Message);
            }
        }

        public ActionResult EditCard(int id)
        {
            try
            {
                CardsFolder_Details _card = new CardsFolder_Details();
                _card = _icard.Get(id).Result;

                _cardmodel = new CardModel();
                _cardmodel.CardId = _card.CardId;
                _cardmodel.RepresentetiveName = _card.Representetive_Name;
                _cardmodel.FirmName = _card.Firm_Name;
                _cardmodel.CardAddress = _card.Address;
                _cardmodel.Website = _card.Website;
                _cardmodel.CardEmail = _card.Email;
                _cardmodel.CradDescription = _card.Description;
                _cardmodel.CradDesignetion = _card.Designetion;
                _cardmodel.CardPhoneNo1 = _card.Phone1;
                _cardmodel.CardPhoneNo2 = _card.Phone2;
                _cardmodel.CardPhoneNo3 = _card.Phone3;
                _cardmodel.CardMobileNo1 = _card.Mobile1;
                _cardmodel.CardMobileNo2 = _card.Mobile2;
                _cardmodel.CardMobileNo3 = _card.Mobile3;

                return View(_cardmodel);
            }
            catch (Exception ex)
            {
                return Json("EditCard " + ex.Message);
            }
        }

        public ActionResult DeleteCard(int id)
        {
            try
            {
                _icard.Delete(id);
                return RedirectToAction("CardList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCard " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateCard(CardModel cardm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                   // duplicate = _icard.CheckDuplicate(cardm.BuyerName).Result;
                    if (duplicate == 0)
                    {
                        CardsFolder_Details _card = new CardsFolder_Details();
                        _card.CardId = _icard.GetNewCardId().Result;
                        _card.Representetive_Name = cardm.RepresentetiveName;
                        _card.Firm_Name = cardm.FirmName;
                        _card.Address = cardm.CardAddress;
                        _card.Website = cardm.Website;
                        _card.Email = cardm.CardEmail;
                        _card.Description = cardm.CradDescription;
                        _card.Designetion = cardm.CradDesignetion;
                        _card.Phone1 = cardm.CardPhoneNo1;
                        _card.Phone2 = cardm.CardPhoneNo2;
                        _card.Phone3 = cardm.CardPhoneNo3;
                        _card.Mobile1 = cardm.CardMobileNo1;
                        _card.Mobile2 = cardm.CardMobileNo2;
                        _card.Mobile3 = cardm.CardMobileNo3;
                        string result = _icard.Post(_card).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(cardm);
            }
            catch (Exception ex)
            {
                return Json("CreateCard " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCard")]
        public IActionResult EditCard(CardModel cardm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CardsFolder_Details _card = new CardsFolder_Details();
                    _card.CardId = cardm.CardId;
                    _card.Representetive_Name = cardm.RepresentetiveName;
                    _card.Firm_Name = cardm.FirmName;
                    _card.Address = cardm.CardAddress;
                    _card.Website = cardm.Website;
                    _card.Email = cardm.CardEmail;
                    _card.Description = cardm.CradDescription;
                    _card.Designetion = cardm.CradDesignetion;
                    _card.Phone1 = cardm.CardPhoneNo1;
                    _card.Phone2 = cardm.CardPhoneNo2;
                    _card.Phone3 = cardm.CardPhoneNo3;
                    _card.Mobile1 = cardm.CardMobileNo1;
                    _card.Mobile2 = cardm.CardMobileNo2;
                    _card.Mobile3 = cardm.CardMobileNo3;
                    
                    string result = _icard.Put(_card).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(cardm);
            }
            catch (Exception ex)
            {
                return Json("EditCard " + ex.Message);
            }
        }

    }
}
