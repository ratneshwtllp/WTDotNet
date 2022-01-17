using CrystalDecisions.CrystalReports.Engine;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Emails.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Areas.Emails.Controllers
{
    [AuthLogin]
    [Area("Emails")]
    public class EmailsController : Controller
    {
        EmailsModel EM = new EmailsModel();
        IHostingEnvironment _env;
        IEmailsContract _iemail;

        public EmailsController(IHostingEnvironment env, IEmailsContract Em)
        {
            _env = env;
            _iemail = Em;
        }

        public ActionResult EmailsList()
        {
            return View();            
        }
         
        public ActionResult EmailsList_Partial(string search)
        {
            return PartialView("_EmailsList", _iemail.GetViewEmailSettingsSearch(search).Result); 
        }

        public ActionResult DeleteEmail(long emid)
        {
            _iemail.Delete(emid);
            return RedirectToAction("EmailsList"); 
        }
        
        [HttpGet]
        public ActionResult CreateEmails()
        {
            EM = new EmailsModel();
            EM.EditMode = false;
            EM.EmailTypeList = Helper.ConvertObjectToSelectList(_iemail.GetEmailsList().Result);  
            return View(EM); 
        }

        [HttpGet]
        public ActionResult EditEmails(long id)
        {
            EmailSettings Ems = new EmailSettings();
            Ems = _iemail.Get(id).Result;

            EM = new EmailsModel();
            EM.EmailTypeList = Helper.ConvertObjectToSelectList(_iemail.GetEmailsList().Result);
            EM.EditMode = true;
            EM.EmailSettingsID = id;
            EM.EmailTypeId = Ems.EmailTypeId;
            EM.EmailAddress = Ems.EmailAddress;
            EM.EmailPassword = Ems.EmailPassword;
            EM.OutGoingServer = Ems.OutGoingServer;
            EM.Port = Ems.Port;
            EM.EmailSubject = Ems.EmailSubject;
            EM.EmailHeader = Ems.EmailHeader;
            EM.EmailFooter = Ems.EmailFooter;
            EM.BCC = "-";
            EM.EmailTO = Ems.EmailTO;
            EM.CC = Ems.CC;
            //EM.BCC = Ems.BCC;

            return View(EM);
        } 

        [HttpPost]
        public IActionResult CreateEmails(EmailsModel EM)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                EmailSettings EmS = new EmailSettings();
                //if (EM.EditMode == true)
                //{
                //    EmS.EmailSettingsID = EM.EmailSettingsID;
                //}
                //else
                //{
                    EmS.EmailSettingsID = _iemail.GetNewEmailID().Result;
                //}
                EmS.EmailTypeId = EM.EmailTypeId;
                EmS.EmailAddress = EM.EmailAddress;
                EmS.EmailPassword = EM.EmailPassword;
                EmS.OutGoingServer = EM.OutGoingServer;
                EmS.Port = EM.Port;
                EmS.EmailSubject = EM.EmailSubject;
                EmS.EmailHeader = EM.EmailHeader;
                EmS.EmailFooter = EM.EmailFooter;
                EmS.BCC = "-";
                EmS.EmailTO = EM.EmailTO;
                EmS.CC = EM.CC;
                //EmS.BCC = EM.BCC;

                //if (EM.EditMode == true)
                //{
                //    result = _iemail.Put(EmS).Result;
                //}
                //else
                //{
                result = _iemail.Post(EmS).Result;
                //}
            }
            ModelState.Clear();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult EditEmails(EmailsModel EM)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                EmailSettings EmS = new EmailSettings();
                EmS.EmailSettingsID = EM.EmailSettingsID;
                EmS.EmailTypeId = EM.EmailTypeId;
                EmS.EmailAddress = EM.EmailAddress;
                EmS.EmailPassword = EM.EmailPassword;
                EmS.OutGoingServer = EM.OutGoingServer;
                EmS.Port = EM.Port;
                EmS.EmailSubject = EM.EmailSubject;
                EmS.EmailHeader = EM.EmailHeader;
                EmS.EmailFooter = EM.EmailFooter;
                EmS.EmailTO = EM.EmailTO;
                EmS.CC = EM.CC;
                EmS.BCC = EM.BCC;
                //EmS.BCC = EM.BCC;

                result = _iemail.Put(EmS).Result;
            }
            ModelState.Clear();
            return Ok(result);
        }
        //public ActionResult POPrint(int id)
        //{
        //    ReportClass rptH = new ReportClass();
        //    IEnumerable<ViewPOPrint> data = _iemail.GetPOForPrint(id).Result;
        //    DataTable table = new DataTable();
        //    table.TableName = "ViewPOPrint";
        //    using (var reader = ObjectReader.Create(data))
        //    {
        //        table.Load(reader);
        //    }

        //    table.WriteXml(@"C:\Print\ViewPOPrint.xml", XmlWriteMode.WriteSchema);

        //    rptH.FileName = Path.Combine(_env.WebRootPath.Replace("wwwroot", ""), "Areas/PO/Views/PO/CrystalReport1.rpt");// Server.MapPath("/Views/PO/CrystalReport1.rpt");rptH.FileName = Path.Combine(_env.ContentRootPath, "Areas/PO/Views/PO/CrystalReport1.rpt");// Server.MapPath("/Views/PO/CrystalReport1.rpt");
        //    rptH.Load();
        //    rptH.SetDataSource(table);
        //    Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
        //    return File(stream, "application/pdf");
        //}
    }
}

