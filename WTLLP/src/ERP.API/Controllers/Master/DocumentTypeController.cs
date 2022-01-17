using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using ERP.Domain.Entities;
using System;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public DocumentTypeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<DocumentTypeDetails> Get()
        {
            return _context.DocumentTypeDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.DocumentTypeDetails .Where(x => x.DocumentTypeId == id).FirstOrDefault());
        }

        [Route("GetDocumentTypeList")]
        public IActionResult GetDocumentTypeList(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(_context.DocumentTypeDetails.OrderBy(x => x.DocumentTypeName));
            }
            else
            {
                return Ok(_context.DocumentTypeDetails
                    .Where(x => x.DocumentTypeName.Contains(search))
                    .OrderBy(x => x.DocumentTypeName));
            }
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            long id = 0;
            var obj = _context.DocumentTypeDetails.OrderBy(x => x.DocumentTypeId  ).LastOrDefault();
            id = (obj == null ? 0 : obj.DocumentTypeId ) + 1;
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]DocumentTypeDetails value)
        {
            _context.DocumentTypeDetails.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody]DocumentTypeDetails value)
        {
            var existing = _context.DocumentTypeDetails.Find(value.DocumentTypeId );
            if (existing == null)
            {
                return NotFound();
            }
            existing.DocumentTypeName  = value.DocumentTypeName;
            existing.Remark  = value.Remark ;
            _context.DocumentTypeDetails.Update(existing);
            _context.SaveChanges();
            return Ok();
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            var existing = _context.DocumentTypeDetails.Where(x => x.DocumentTypeName  == value).FirstOrDefault<DocumentTypeDetails>();

            if (existing != null)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.DocumentTypeDetails.Find(id);
            if (existing != null)
            {
                _context.DocumentTypeDetails.Remove(existing);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("DocumentTypeList")]
        public IActionResult DocumentTypeList()
        {
            try
            {
                var list = _context.DocumentTypeDetails.OrderBy(x => x.DocumentTypeName  )
                    .Select(x => new { id = x.DocumentTypeId  , value = x.DocumentTypeName  }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProductDocumentTypeList/{fitemid}")]
        public IActionResult ProductDocumentTypeList(long fitemid)
        {
            try
            {
                var TypeId = _context.ProductFile.Where(x => x.FitemId == fitemid).Select(x => x.DocumentTypeId).ToList();
                var list = _context.DocumentTypeDetails.Where(x => !TypeId.Contains(x.DocumentTypeId))
                    .OrderBy(x => x.DocumentTypeName)
                    .Select(x => new { id = x.DocumentTypeId, value = x.DocumentTypeName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMDocumentTypeList/{ritemid}")]
        public IActionResult RMDocumentTypeList(long ritemid)
        {
            try
            {
                var TypeId = _context.RMFile.Where(x => x.RItem_Id == ritemid).Select(x => x.DocumentTypeId).ToList();
                var list = _context.DocumentTypeDetails.Where(x => !TypeId.Contains(x.DocumentTypeId))
                    .OrderBy(x => x.DocumentTypeName)
                    .Select(x => new { id = x.DocumentTypeId, value = x.DocumentTypeName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GRNDocumentTypeList/{id}")]
        public IActionResult GRNDocumentTypeList(long id)
        {
            try
            {
                var TypeId = _context.GRNFile.Where(x => x.GRNId == id).Select(x => x.DocumentTypeId).ToList();
                var list = _context.DocumentTypeDetails.Where(x => !TypeId.Contains(x.DocumentTypeId))
                    .OrderBy(x => x.DocumentTypeName)
                    .Select(x => new { id = x.DocumentTypeId, value = x.DocumentTypeName }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
