using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERP.Web.Areas.Quality.Controllers
{
    [AuthLogin]
    [Area("DocumentType")]
    public class DocumentTypeController : Controller
    {
        DocumentTypeDetails _documenttype = new DocumentTypeDetails();
        IHostingEnvironment _env;
        IDocumentType _idocument;

        public DocumentTypeController(IHostingEnvironment env, IDocumentType _icomp)
        {
            _env = env;
            _idocument = _icomp;
        }

        public ActionResult CreateDocumentType()
        {
            _documenttype = new DocumentTypeDetails();
            return View(_documenttype);
        }


        public ActionResult EditDocumentType(int id)
        {
            _documenttype = new DocumentTypeDetails();
            _documenttype = _idocument.Get(id).Result;
            return View(_documenttype);
        }
        public ActionResult DocumentTypeList()
        {
            return View();
        }

        public ActionResult DocumentTypeList_Partial(String search)
        {
            return PartialView("_DocumentTypeList", _idocument.GetDocumentTypeList(search).Result);
        }

        [HttpPost]
        public ActionResult CreateDocumentType(DocumentTypeDetails  Comp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _idocument.CheckDuplicate(Comp.DocumentTypeName).Result;
                    if (duplicate == 0)
                    {
                        Comp.EntryDate = DateTime.Now.Date;
                        Comp.SessionYear  = Helper.Create_Session();
                        Comp.UserId  = HttpContext.Session.GetInt32("userid").Value;
                        Comp.DocumentTypeId    = _idocument.GetNewId().Result;
                        string result = _idocument.Post(Comp).Result;
                        ModelState.Clear();
                        return Ok("Record Save");
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(Comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, ActionName("EditDocumentType")]
        public IActionResult EditComponent(DocumentTypeDetails Comp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _idocument.Put(Comp).Result;
                    ModelState.Clear();
                    return Ok("Record Edit.");
                }
                return View(Comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        public ActionResult DeleteDocumentType(int  id)
        {
            _idocument.Delete(id);
            return RedirectToAction("DocumentTypeList");
        }
    }
}
