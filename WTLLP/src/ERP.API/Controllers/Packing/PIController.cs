using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;
using System.Collections.Generic;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class PIController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public PIController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<PIMaster> Get()
        {
            return _context.PIMaster.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(_context.ViewPI.Where(x => x.PIMasterId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Get")]
        public IActionResult Get(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewPI
                        .OrderByDescending(x => x.PIMasterId));
                else
                    return Ok(_context.ViewPI
                        .Where(x => x.PINo.Contains(search) || x.BuyerName.Contains(search) || x.OrderNo.Contains(search))   //|| x.Code.Contains(search) || x.Name.Contains(search)
                        .OrderByDescending(x => x.PIMasterId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetChild/{id}")]
        public IActionResult GetChild(long id)
        {
            try
            {
                var child = _context.ViewPIChild.Where(x => x.PIMasterId == id)
                                .OrderBy(x=>x.SNo);
                return Ok(child);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewid")]
        public IActionResult GetNewid()
        {
            try
            {
                long ProformaID = 0;
                var lastproforma = _context.PIMaster.OrderBy(x => x.PIMasterId).LastOrDefault();
                ProformaID = (lastproforma == null ? 0 : lastproforma.PIMasterId) + 1;
                return Ok(ProformaID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewChildid")]
        public IActionResult GetNewChildid()
        {
            try
            {
                long PIChildID = 0;
                var lastPIChild = _context.PIChild.OrderBy(x => x.PIChildId).LastOrDefault();
                PIChildID = (lastPIChild == null ? 0 : lastPIChild.PIChildId) + 1;
                return Ok(PIChildID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetNewSerial")]
        //public IActionResult GetNewSerial()
        //{
        //    try
        //    {
        //        long PISerial = 0;
        //        var lastPI = _context.PIMaster.OrderBy(x => x.PISNo).LastOrDefault();
        //        PISerial = (lastPI == null ? 0 : lastPI.PISNo) + 1;
        //        return Ok(PISerial);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetNewSerial")]
        public IActionResult GetNewSerial()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.PIMaster.OrderBy(x => x.PIMasterId).FirstOrDefault();
                if (FirstRecord == null)
                {
                    var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "PI").FirstOrDefault();
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.PIMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PISNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PISNo) + 1;
                }
                return Ok(Serial);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewNo")]
        public IActionResult GetNewNo()
        {
            try
            {
                string session = API.Helper.Create_Session();
                long Serial = 0;
                var FirstRecord = _context.PIMaster.OrderBy(x => x.PIMasterId).FirstOrDefault();
                var FormSettings = _context.FormNumberSettingsDetails.Where(x => x.FormName == "PI").FirstOrDefault();

                if (FirstRecord == null)
                {
                    Serial = FormSettings.StartingNumber;
                }
                else
                {
                    var LastRecord = _context.PIMaster.Where(x => x.SessionYear == session).OrderBy(x => x.PISNo).LastOrDefault();
                    Serial = (LastRecord == null ? 0 : LastRecord.PISNo) + 1;
                }

                String DisplayString = FormSettings.Prefix + "/" + session + "/";
                switch (FormSettings.NoOfDigits)
                {
                    case 3:
                        DisplayString += string.Format("{0:000}", Serial);
                        break;
                    case 4:
                        DisplayString += string.Format("{0:0000}", Serial);
                        break;
                    case 5:
                        DisplayString += string.Format("{0:00000}", Serial);
                        break;
                    default:
                        break;
                }
                return Ok(DisplayString);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //[Route("GetNewNo")]
        //public IActionResult GetNewNo()
        //{
        //    try
        //    {
        //        long PISerial = 0;
        //        var lastPI = _context.PIMaster.OrderBy(x => x.PISNo).LastOrDefault();
        //        PISerial = (lastPI == null ? 0 : lastPI.PISNo) + 1;

        //        String PINO = "SB/PI/17-18/" + string.Format("{0:0000}", PISerial) + "";
        //        return Ok(PINO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [Route("GetPIPrint/{id}")]
        public IActionResult GetPIPrint(long id)
        {
            var recordlist = _context.ViewPIPrint.Where(x => x.PIMasterId == id)
                                        .OrderBy(x => x.PIMasterId)
                                        .ThenBy(x => x.Code)
                                        .ToList();
            return Ok(recordlist);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PIMaster value)
        {
            try
            {
                _context.PIMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]PIMaster value)
        {
            try
            {
                var existingPI = _context.PIMaster
                        .Where(x => x.PIMasterId == value.PIMasterId)
                        .Include(x => x.PIChild)
                        .SingleOrDefault();

                if (existingPI != null)
                {
                    // Update parent
                    _context.Entry(existingPI).CurrentValues.SetValues(value);

                    // Delete children
                    foreach (var ExistingPackingChild in existingPI.PIChild.ToList())
                    {
                        _context.PIChild.Remove(ExistingPackingChild);
                    }

                    // Update and Insert children
                    foreach (var childPacking in value.PIChild)
                    {
                        // Insert child 1
                        PIChild newChild = new PIChild();
                        newChild.PIChildId = childPacking.PIChildId;
                        newChild.PIMasterId = childPacking.PIMasterId;
                        newChild.FitemID = childPacking.FitemID;
                        newChild.Qty = childPacking.Qty;
                        newChild.Price = childPacking.Price;
                        newChild.Amount = childPacking.Amount;
                        newChild.SNo = childPacking.SNo;
                        existingPI.PIChild.Add(newChild);
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

        [Route("Cancel/{id}")]
        public IActionResult Cancel(long id)
        {
            try
            {
                var existingPI = _context.PIMaster.Find(id);
                //existingIssue.CancelStatus = 1;
                _context.PIMaster.Update(existingPI);

                _context.SaveChanges();
                var result = "Record Canceled";
                return Ok(result);

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
                var existingPI = _context.PIMaster.Where(x => x.PIMasterId == id)
                                                .Include(x => x.PIChild)
                                                .FirstOrDefault();

                if (existingPI != null)
                {
                    _context.PIMaster.Remove(existingPI);
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
