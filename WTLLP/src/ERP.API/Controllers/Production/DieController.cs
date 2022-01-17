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
    public class DieController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public DieController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<DieDetails> Get()
        {
            return _context.DieDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.DieDetails.Where(x => x.DieId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetDieList")]
        public IActionResult GetDieList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.DieDetails.OrderByDescending(x => x.DieId));
                else
                    return Ok(_context.DieDetails
                        .Where(x => x.DieName.Contains(search) || x.DieNo.Contains(search))
                        .OrderByDescending(x => x.DieId));

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetFitemList")]
        public IQueryable<ProductDetails> GetFitemList()
        {
            return _context.ProductDetails.AsQueryable();
        }

        [Route("GetDieFitemList/{Dieid}")]
        public IQueryable<DieFitem> GetDieFitemList(long Dieid)
        {
            //return _context.RitemMaster.AsQueryable();
            return _context.DieFitem.Where(x => x.DieId == Dieid).AsQueryable();
        }
         
        [Route("GetViewDieFitemList/{id}")]
        public IQueryable<ViewDieFitemDetails> GetViewDieFitemList(long id)
        { 
            return _context.ViewDieFitemDetails.Where(x => x.DieId == id).AsQueryable();
        }

        [Route("GetNewDieId")]
        public IActionResult GetNewDieId()
        {
            try
            {
                long DieId = 0;
                var lastDie = _context.DieDetails.OrderBy(x => x.DieId).LastOrDefault();
                DieId = (lastDie == null ? 0 : lastDie.DieId) + 1;
                return Ok(DieId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewDieFitemId")]
        public IActionResult GetNewDieFitemId()
        {
            try
            {
                long DieFitem_Id = 0;
                var lastDieFitem = _context.DieFitem.OrderBy(x => x.DieFitemId).LastOrDefault();
                DieFitem_Id = (lastDieFitem == null ? 0 : lastDieFitem.DieFitemId) + 1;
                return Ok(DieFitem_Id);
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
                var existingDie = _context.DieDetails.Where(x => x.DieNo == value).FirstOrDefault<DieDetails>();
                if (existingDie != null)
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

        //[HttpPost]
        //public IActionResult Post([FromBody]DieDetails value)
        //{
        //    var existingDieDetails = _context.DieDetails
        //                                 .Where(x => x.DieId == value.DieId)
        //                                 .Include(x => x.DieFitem)
        //                                 .SingleOrDefault();

        //    if (existingDieDetails != null)
        //    {
        //        // Update parent
        //        _context.Entry(existingDieDetails).CurrentValues.SetValues(value);

        //        // Delete children
        //        foreach (var existingDieFitem in existingDieDetails.DieFitem.ToList())
        //        {
        //            //************ usefull for delete existing entry only --- do not delete all
        //            //if (value.RItemSupp.Any(c => c.SID == existingRItemSupp.SID))
        //            _context.DieFitem.Remove(existingDieFitem);
        //        }

        //        // Update and Insert children
        //        foreach (var childModel in value.DieFitem)
        //        {
        //            // Insert child
        //            DieFitem newChild = new DieFitem();
        //            newChild.DieFitemId = childModel.DieFitemId;
        //            newChild.DieId = childModel.DieId;
        //            newChild.FitemId = childModel.FitemId;
        //            existingDieDetails.DieFitem.Add(newChild);
        //        }
        //        _context.SaveChanges();
        //    }
        //    else
        //    {
        //        _context.DieDetails.Add(value);
        //        _context.SaveChanges();
        //    }
        //    return Ok("Record Save");
        //}

        [HttpPost]
        public IActionResult Post([FromBody]DieDetails value)
        {
            try
            {
                _context.DieDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        //public IActionResult Put([FromBody]DieDetails value)
        //{
        //    try
        //    {
        //        //var existingDie = _context.DieDetails.Where(x => x.DieId == value.DieId).FirstOrDefault<DieDetails>();
        //        //if (existingDieDetails == null)
        //        //{
        //        //    return NotFound();
        //        //}
        //        //existingDieDetails.DieName = value.DieName;
        //        //existingDieDetails.DieNo = value.DieNo;
        //        //existingDieDetails.DieDesc = value.DieDesc;
        //        //_context.DieDetails.Update(existingDieDetails);
        //        //_context.SaveChanges();
        //        //return new NoContentResult();

        //        var existingDieDetails = _context.DieDetails
        //                                         .Where(x => x.DieId == value.DieId)
        //                                         .Include(x => x.DieFitem)
        //                                         .SingleOrDefault();
        //        if (existingDieDetails != null)
        //        {
        //            // Update parent
        //            //_context.Entry(existingDieDetails).CurrentValues.SetValues(value);
        //            existingDieDetails.DieName = value.DieName;
        //            existingDieDetails.DieNo = value.DieNo;
        //            existingDieDetails.DieDesc = value.DieDesc;

        //            // Delete children
        //            foreach (var existingDieFitem in existingDieDetails.DieFitem.ToList())
        //            {
        //                //************ usefull for delete existing entry only --- do not delete all
        //                //if (value.RItemSupp.Any(c => c.SID == existingRItemSupp.SID))
        //                _context.DieFitem.Remove(existingDieFitem);
        //            }

        //            // Update and Insert children
        //            foreach (var childModel in value.DieFitem)
        //            {
        //                // Insert child
        //                DieFitem newChild = new DieFitem();
        //                newChild.DieFitemId = childModel.DieFitemId;
        //                newChild.DieId = childModel.DieId;
        //                newChild.FitemId = childModel.FitemId;
        //                existingDieDetails.DieFitem.Add(newChild);
        //            }

        //            _context.DieDetails.Update(existingDieDetails);
        //            _context.SaveChanges();
        //        }
        //        else
        //        {
        //            _context.DieDetails.Update(existingDieDetails);
        //            _context.SaveChanges();
        //        }
        //        return Ok("Record Updated");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.InnerException);
        //    }
        //}

        [HttpPut]
        public IActionResult Put([FromBody]DieDetails value)
        {
            try
            {
                var existingDieDetails = _context.DieDetails
                                                 .Where(x => x.DieId == value.DieId)
                                                 .SingleOrDefault();

                existingDieDetails.DieName = value.DieName;
                existingDieDetails.DieNo = value.DieNo;
                existingDieDetails.DieDesc = value.DieDesc;

                _context.DieDetails.Update(existingDieDetails);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutDieFitem")]
        public IActionResult PutDieFitem([FromBody]List<DieFitem> value)
        {
            try
            {
                var existingDieDetails = _context.DieFitem
                                                 .Where(x => x.DieId == value[0].DieId)
                                                 .ToList();

                if (existingDieDetails != null)
                {
                    //Delete children
                    foreach (var existingDieFitem in existingDieDetails)
                    {
                        foreach (var childModel in value)
                        {
                            if (existingDieFitem.FitemId == childModel.FitemId && existingDieFitem.DieId == childModel.DieId)
                            {
                                _context.DieFitem.Remove(existingDieFitem);
                            }
                        }
                    }                     
                    foreach (var diefitem in value)
                    {
                        _context.DieFitem.Add(diefitem);
                        _context.SaveChanges();
                    }
                    //return Ok("Record Updated");
                    return Ok("Product Assigned");
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

        [Route("RemoveDieFitem")]
        public IActionResult RemoveDieFitem([FromBody]List<DieFitem> value)
        {
            try
            {
                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        var existingDieFitemDetails = _context.DieFitem
                                                 .Where(x => x.DieId == value[i].DieId && x.FitemId == value[i].FitemId)
                                                 .SingleOrDefault();

                        if (existingDieFitemDetails != null)
                        {
                            // Delete children
                            _context.DieFitem.Remove(existingDieFitemDetails);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return Ok("Not Found");
                        }
                    }
                    return Ok("Removed Assigned Product");
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

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //var existingDie = _context.DieDetails.Where(x => x.DieId == id).FirstOrDefault<DieDetails>();
                //if (existingDie != null)
                //{
                //    _context.DieDetails.Remove(existingDie);
                //    _context.SaveChanges();
                //}
                //else
                //{
                //    return NotFound();
                //}

                var existingDieDetails = _context.DieDetails
                                                 .Where(x => x.DieId == id)
                                                 .Include(x => x.DieFitem)
                                                 .SingleOrDefault();
                if (existingDieDetails != null)
                {
                    // Delete children
                    foreach (var existingDieFitem in existingDieDetails.DieFitem.ToList())
                    {
                        //************ usefull for delete existing entry only --- do not delete all
                        //if (value.RItemSupp.Any(c => c.SID == existingRItemSupp.SID))
                        _context.DieFitem.Remove(existingDieFitem);
                    }

                    _context.DieDetails.Remove(existingDieDetails);
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
