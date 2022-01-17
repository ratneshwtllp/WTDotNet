using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using ERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.API
{
    [Route("api/[controller]")]
    public class ProductSizeController : ControllerBase
    {
        DBContext _context;
        IMemoryCache _memoryCache;

        public ProductSizeController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IQueryable <ProductSizeDetails> Get()
        {
            return _context.ProductSizeDetails.AsQueryable().OrderBy(x=> x.ProductSizeName);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ProductSizeDetails.Where(x => x.ProductSizeId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductSizeList")]
        public IActionResult GetProductSizeList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ProductSizeDetails.OrderByDescending(x => x.ProductSizeId));
                else
                    return Ok(_context.ProductSizeDetails
                        .Where(x => x.ProductSizeName.Contains(search))
                        .OrderByDescending(x => x.ProductSizeId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductSizeId")]
        public IActionResult GetNewProductSizeId()
        {
            try
            {
                long ProductSizeid = 0;
                var lastProductSize = _context.ProductSizeDetails.OrderBy(x => x.ProductSizeId).LastOrDefault();
                ProductSizeid = (lastProductSize == null ? 0 : lastProductSize.ProductSizeId) + 1;
                return Ok(ProductSizeid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductSizeDetails value)
        {
            try
            {
                _context.ProductSizeDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductSizeDetails value)
        {
            try
            {
                var existingProductSize = _context.ProductSizeDetails.Find(value.ProductSizeId);
                if (existingProductSize == null)
                {
                    return Ok("Not Found");
                }
                existingProductSize.ProductSizeName = value.ProductSizeName;
                existingProductSize.ProductSizeDesc = value.ProductSizeDesc;
                existingProductSize.Online_Transfer = value.Online_Transfer;
                _context.ProductSizeDetails.Update(existingProductSize);
                _context.SaveChanges();
                return Ok("Record Updated");
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
                var existingProductSize = _context.ProductSizeDetails.Where(x => x.ProductSizeName == value).FirstOrDefault<ProductSizeDetails>();
                if (existingProductSize != null)
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

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingProductSize = _context.ProductSizeDetails.Where(x => x.ProductSizeId == id).FirstOrDefault<ProductSizeDetails>();
                if (existingProductSize != null)
                {
                    _context.ProductSizeDetails.Remove(existingProductSize);
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

        [Route("GetProductDetails/{barcode}")]
        public IActionResult GetProductDetails(string barcode)
        {
            try
            {
                var product = _context.ViewProductSize.Where(x => x.SizeBarcode == barcode)
                                    .OrderByDescending(x => x.FitemId).FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
