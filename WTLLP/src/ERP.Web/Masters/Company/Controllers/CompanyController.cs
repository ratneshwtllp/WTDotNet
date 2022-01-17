using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ERP.Web.Areas.Company.Controllers
{
    [AuthLogin]
    [Area("Company")]
    public class CompanyController : Controller
    {
        CompanyDetails _companydetails = new CompanyDetails();

        IHostingEnvironment _env;
        ICompanyContract _icompany;

        public CompanyController(IHostingEnvironment env, ICompanyContract company)
        {
            _env = env;
            _icompany = company;
        }

        public ActionResult CompanyList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CompanyList " + ex.Message);
            }
        }

        public ActionResult CompanyList_Partial(string search)
        {
            try
            {
                return PartialView("_CompanyList", _icompany.GetList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("CompanyList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateCompany()
        {
            try
            {
                CompanyDetails _company = new CompanyDetails();
                return View(_company);
            }
            catch (Exception ex)
            {
                return Json("CreateCompany " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateCompany(CompanyDetails Com)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icompany.CheckDuplicate(Com.CName).Result;
                    if (duplicate == 0)
                    {
                        Com.Id = _icompany.GetNewId().Result;
                        string result = _icompany.Post(Com).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(1);
                        //return BadRequest("1");
                    }
                }
                return View(Com);
            }
            catch (Exception ex)
            {
                return Json("CreateCompany " + ex.Message);
            }
        }
        public ActionResult EditCompany(int id)
        {
            try
            {
                _companydetails = new CompanyDetails();
                _companydetails = _icompany.Get(id).Result;
                return View(_companydetails);
            }
            catch (Exception ex)
            {
                return Json("EditCompany " + ex.Message);
            }
        }

        public ActionResult DeleteCompany(int id)
        {
            try
            {
                _icompany.Delete(id);
                return RedirectToAction("CompanyList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCompany " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCompany")]
        public async Task<IActionResult> EditCompany(CompanyDetails _companydetails, IFormFile file)
        {
            string result;
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var webRoot = _env.WebRootPath;
                        string filename = file.FileName;
                        int pos = file.FileName.LastIndexOf(".");
                        filename = file.FileName.Substring(0, pos);
                        filename = file.FileName.Replace(filename, _companydetails.Id.ToString());
                        var uploads = Path.Combine("uploads/logo/", filename);
                        var path = Path.Combine(_env.WebRootPath, uploads);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        _companydetails.LogoPath = uploads;
                    }
                    result = _icompany.Put(_companydetails).Result;
                    ModelState.Clear();
                    return RedirectToAction("CompanyList");
                }
                return View(_companydetails);
            }
            catch (Exception ex)
            {
                return Json("EditCompany " + ex.Message);
            }
        }



    }
}
