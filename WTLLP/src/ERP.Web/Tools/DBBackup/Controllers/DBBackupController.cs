using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;

namespace ERP.Web.Areas.DBBackup.Controllers
{
    [Area("DBBackup")]
    public class DBBackupController : Controller
    {
        IHostingEnvironment _env;
        IDBBackup _ibackup;

        public DBBackupController(IHostingEnvironment env, IDBBackup ibackup)
        {
            _env = env;
            _ibackup = ibackup;
        }

        public ActionResult Create()
        {
            try
            {
                DBBackupEntry db = new DBBackupEntry();
                db.name = "test";
                return View("Create", db);
            }
            catch (Exception ex)
            {
                return Json("Create " + ex.Message);
            }
        }        

        [HttpPost]
        public ActionResult Create(DBBackupEntry DB)
        {
            try
            {
                string result = _ibackup.Create().Result;
                ModelState.Clear();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Json("Create " + ex.Message);
            }
        }
    }
}
