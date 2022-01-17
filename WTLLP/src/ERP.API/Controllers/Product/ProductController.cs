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
    public class ProductController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public ProductController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        [HttpGet]
        public IQueryable<ProductDetails> Get()
        {
            return _context.ProductDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.ProductDetails.Where(x => x.FitemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewProduct/{fitemid}")]
        public IActionResult ViewProduct(long fitemid)
        {
            try
            {
                var product = _context.ViewProductShow.Where(x => x.FitemId == fitemid)
                                    .OrderByDescending(x => x.FitemId).FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewProduct")]
        public IActionResult GetViewProduct(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    var Product = _context.ViewProductShow.OrderByDescending(x => x.FitemId);
                    return Ok(Product);
                }
                else
                {
                    var Product = _context.ViewProductShow
                                    .Where(x => x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.Name.Contains(search))
                                    .OrderByDescending(x => x.FitemId);
                    return Ok(Product);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetViewProductSearch")]
        public IActionResult GetViewProductSearch(string search, long id)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    var Product = _context.ViewProductShow.Where(x => x.ProductCategoryID == id).OrderByDescending(x => x.FitemId);
                    return Ok(Product);
                }
                else
                {
                    var Product = _context.ViewProductShow
                                    .Where(x => x.ProductCategoryID == id && (x.ProductCategoryName.Contains(search) || x.ProductSubCategoryName.Contains(search) || x.Code.Contains(search) || x.Name.Contains(search)))
                                    .OrderByDescending(x => x.FitemId);
                    return Ok(Product);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProSubCategoryFromBuyer/{buyerid}")]
        public IActionResult GetProSubCategoryFromBuyer(long buyerid)
        {
            try
            {
                var buyercode = _context.BuyerDetails
                                    .Where(x => x.BuyerId == buyerid)
                                    .Select(x => x.BuyerCode);

                var categories = _context.ProductCategoryDetails
                                    .Where(x => x.ProductShortCode == buyercode.Single())
                                    .OrderBy(c => c.ProductShortCode)
                                    .Select(x => x.ProductCategoryID).ToList();

                var subcategories = _context.ProductSubCategoryDetails
                    .Where(x => categories.Contains(x.ProductCategoryID))
                    .OrderBy(c => c.ProductShortCode)
                    .Select(x => new { Id = x.ProductSubCategoryID, Value = x.ProductShortCode }).ToList();

                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetFItemCodeList/{id}")]
        public IActionResult Get4DigitCode(long id)
        {
            try
            {
                object Fdigitcode = _context.ViewProductShow
                                        .Where(c => c.ProductSubCategoryID == id)
                                        .Select(c => new { Id = c.FitemId, value = c.Code })
                                        .ToList();
                return Ok(Fdigitcode);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetPartyFItemList/{Partyid}")]
        public IActionResult GetPartyFItemList(long Partyid)
        {
            try
            {
                object obj = _context.ViewProductShow
                                        .Where(c => c.BuyerId == Partyid)
                                        .Select(c => new { Id = c.FitemId, value = c.Code })
                                        .OrderBy(c => c.value)
                                        .ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        /// <summary>
        /// API action GetProductList
        /// </summary>
        /// <param name="subcategoryid">when 0 then all product else only belog to</param>
        /// <returns></returns>
        [Route("GetProductList/{subcategoryid}")]
        public IActionResult GetProductList(int subcategoryid)
        {
            try
            {
                object ProductL;
                if (subcategoryid == 0)
                {
                    /// select All product
                    ProductL = _context.ProductDetails
                    .OrderBy(c => c.Code)
                    .Select(x => new { Id = x.FitemId, Value = x.Code }).ToList();
                }
                else
                {
                    /// select only belong to  product
                    ProductL = _context.ProductDetails
                    .Where(x => x.BelongTo == subcategoryid)
                    .OrderBy(c => c.Code)
                    .Select(x => new { Id = x.FitemId, Value = x.Code }).ToList();
                }
                return Ok(ProductL);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [Route("GetFinishList")]
        public IActionResult GetFinishList()
        {
            try
            {
                object Finish;
                Finish = _context.FinishDetails
                                    .OrderBy(c => c.Fname)
                                    .Select(x => new { Id = x.Fid, Value = x.Fname }).ToList();
                return Ok(Finish);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductSizeList")]
        public IActionResult GetProductSizeList()
        {
            try
            {
                object ProductSize;
                ProductSize = _context.ProductSizeDetails
                                    .OrderBy(c => c.ProductSizeName)
                                    .Select(x => new { Id = x.ProductSizeId, Value = x.ProductSizeName }).ToList();
                return Ok(ProductSize);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetbackingList")]
        public IActionResult GetbackingList()
        {
            try
            {
                object Backing;
                Backing = _context.BackingDetails
                                    .OrderBy(c => c.BackingName)
                                    .Select(x => new { Id = x.BackingId, Value = x.BackingName }).ToList();
                return Ok(Backing);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetQualityList")]
        public IActionResult GetQualityList()
        {
            try
            {
                object Quality;
                Quality = _context.QualityDetails
                                    .OrderBy(c => c.QualityName)
                                    .Select(x => new { Id = x.QualityId, Value = x.QualityName }).ToList();
                return Ok(Quality);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductList")]
        public IActionResult GetProductList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ProductDetails.OrderByDescending(x => x.FitemId));
                else
                    return Ok(_context.ProductDetails.Where(x => x.Name.Contains(search)).OrderByDescending(x => x.FitemId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductId")]
        public IActionResult GetNewProductId()
        {
            try
            {
                long ProductId = 0;
                var lastProduct = _context.ProductDetails.OrderBy(x => x.FitemId).LastOrDefault();
                ProductId = (lastProduct == null ? 0 : lastProduct.FitemId) + 1;
                return Ok(ProductId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductImagePath/{fitemid}")]
        public IActionResult GetProductImagePath(long fitemid)
        {
            try
            {
                string imgpath = "";
                var fproduct = _context.ProductDetails.Find(fitemid);
                imgpath = fproduct.PhotoPath;
                return Ok(imgpath);
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
                var existingProduct = _context.ProductDetails.Where(x => x.Name == value).FirstOrDefault<ProductDetails>();
                if (existingProduct != null)
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

        [HttpPost]
        public IActionResult Post([FromBody]ProductDetails value)
        {
            try
            {
                _context.ProductDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProductDetails value)
        {
            try
            {
                var existingproduct = _context.ProductDetails
                                     .Where(x => x.FitemId == value.FitemId)
                                     .Include(x => x.ProductMultipleSize)
                                     .SingleOrDefault();

                if (existingproduct != null)
                {
                    // Update parent 
                    existingproduct.MasterBelongTo = value.MasterBelongTo;
                    existingproduct.BelongTo = value.BelongTo;
                    existingproduct.Code = value.Code;
                    existingproduct.Name = value.Name;
                    existingproduct.ItOrCat = 1;
                    existingproduct.Description = value.Description;
                    existingproduct.ItemWeight = value.ItemWeight;
                    //existingProduct.Quantity = value.Quantity;
                    existingproduct.UpdatedBy = value.UpdatedBy;
                    existingproduct.CreatedDate = value.CreatedDate;
                    existingproduct.PartyDate = value.PartyDate;
                    //existingProduct.TypeOfSample = value.TypeOfSample;
                    existingproduct.OrderNo = value.OrderNo;

                    //existingproduct.Customer = value.Customer;
                    existingproduct.CreatedBy = value.CreatedBy;
                    //existingProduct.Finish = value.Finish;
                    existingproduct.Color = value.Color;
                    existingproduct.Designer = value.Designer;
                    //existingProduct.GroupName = value.GroupName;
                    //existingProduct.Size = value.Size;
                    existingproduct.SizeId = 0;
                    existingproduct.Sticker = value.Sticker;
                    //existingProduct.Trimming = value.Trimming;
                    //existingProduct.Backing = value.Backing;
                    existingproduct.Lining = value.Lining;
                    //existingProduct.LiningColour = value.LiningColour;
                    //existingProduct.Labels = value.Labels;
                    existingproduct.CareLabels = value.CareLabels;
                    existingproduct.Embossment = value.Embossment;
                    existingproduct.Wax = value.Wax;

                    //existingProduct.Wash = value.Wash;
                    existingproduct.Quality = value.Quality;
                    existingproduct.Hangtags = value.Hangtags;
                    existingproduct.Stitching = value.Stitching;
                    existingproduct.PhotoPath = value.PhotoPath;

                    existingproduct.NumericItemCode = value.NumericItemCode;
                    existingproduct.Suffix = value.Suffix;
                    existingproduct.CodeAlias = value.CodeAlias;
                    existingproduct.PartyCode = value.PartyCode;
                    existingproduct.Barcode = value.Barcode;
                    existingproduct.Logo = value.Logo;
                    existingproduct.Price = value.Price;
                    existingproduct.StyleId = value.StyleId;
                    existingproduct.RitemId = value.RitemId;
                    existingproduct.ProductTypeId = value.ProductTypeId;
                    existingproduct.SoleName = value.SoleName;
                    existingproduct.ZipperName = value.ZipperName;
                    existingproduct.BuyerId = value.BuyerId;
                    existingproduct.Online_Transfer = value.Online_Transfer;
                    existingproduct.Unit = value.Unit;
                    existingproduct.IsActive = value.IsActive;
                    existingproduct.IsOpenForAll = value.IsOpenForAll;

                    //existingproduct.Paper1 = value.Paper1;
                    //existingproduct.Paper2 = value.Paper2; 
                    //existingproduct.Color2 = value.Color2;
                    //existingproduct.Print = value.Print; 
                    //existingproduct.Thickness = value.Thickness;

                    //existingproduct.Fabric1 = value.Fabric1;
                    //existingproduct.Fabric2 = value.Fabric2;

                    //existingproduct.Width = value.Width;
                    existingproduct.PRDWidth = value.PRDWidth;
                    existingproduct.Connstruction = value.Connstruction;
                    existingproduct.GenderId = value.GenderId;
                    existingproduct.Heel = value.Heel;

                    existingproduct.IsSizeRun = value.IsSizeRun;

                    var existingChild = existingproduct.ProductMultipleSize
                        .Where(c => c.FitemId == value.FitemId).ToList();
                    _context.ProductMultipleSize.RemoveRange(existingChild);

                    // Update and Insert children 2
                    foreach (var childModel in value.ProductMultipleSize)
                    {
                        // Insert child
                        ProductMultipleSize newChild = new ProductMultipleSize();
                        newChild.ProductMultipleSizeId = childModel.ProductMultipleSizeId;
                        newChild.FitemId = childModel.FitemId;
                        newChild.SizeId = childModel.SizeId;
                        newChild.SizePrice = childModel.SizePrice;
                        newChild.SizeBarcode = childModel.SizeBarcode;
                        newChild.SizePartyCode = childModel.SizePartyCode;
                        existingproduct.ProductMultipleSize.Add(newChild);

                        //var existingChild = existingproduct.ProductMultipleSize
                        //    .Where(c => c.FitemId == childModel.FitemId && c.SizeId == childModel.SizeId)
                        //    .SingleOrDefault();

                        //if (existingChild == null)
                        //{
                        //    existingproduct.ProductMultipleSize.Add(newChild);
                        //}
                        //else
                        //{
                        //    existingChild.SizePrice = childModel.SizePrice;
                        //    existingChild.SizeBarcode = childModel.SizeBarcode;
                        //    existingChild.SizePartyCode = childModel.SizePartyCode;
                        //}
                    }
                    _context.ProductDetails.Update(existingproduct);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ProductDetails.Add(value);
                    _context.SaveChanges();
                }
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ChangeSubCat")]
        public IActionResult ChangeSubCat([FromBody]SearchModel value)
        {
            try
            {
                var obj = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == value.belongto).FirstOrDefault();

                var existingproduct = _context.ProductDetails.Where(x => value.fitemids.Contains(x.FitemId)).ToList();
                if (existingproduct != null)
                {
                    foreach (var onerecord in existingproduct)
                    {
                        onerecord.BelongTo = value.belongto;
                        onerecord.MasterBelongTo = obj.ProductCategoryID;
                        _context.ProductDetails.Update(onerecord);
                    }
                }
                _context.SaveChanges();
                var result = "Record update";
                return Ok(result);
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
                //var existingProduct = _context.ProductDetails.Where(x => x.FitemId == id).FirstOrDefault<ProductDetails>();
                var existingProduct = _context.ProductDetails
                     .Where(x => x.FitemId == id)
                     .Include(x => x.ProductMultipleSize)
                     .SingleOrDefault();
                if (existingProduct != null)
                {
                    foreach (var ExistingProductChild in existingProduct.ProductMultipleSize.ToList())
                    {
                        _context.ProductMultipleSize.Remove(ExistingProductChild);
                    }
                    _context.ProductDetails.Remove(existingProduct);
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

        [Route("GetProductCodeList/{subcatId}")]
        public IActionResult GetProductCodeList(long subcatId)
        {
            try
            {
                object CodeList;
                CodeList = _context.ProductDetails
                                    .Where(x => x.BelongTo == subcatId)
                                    .OrderBy(c => c.Code)
                                    .Select(x => new { Id = x.FitemId, Value = x.Code }).ToList();
                return Ok(CodeList);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNumericItemCode/{pcatid}/{pscatid}")]
        public IActionResult GetNumericItemCode(long pcatid, long pscatid)
        {
            try
            {
                //long numericitemcode = 0;
                //var lastnumericitemcode = _context.ProductDetails.Where(x => x.MasterBelongTo == pcatid && x.BelongTo == pscatid)
                //    .OrderBy(x => x.FitemId).LastOrDefault();
                //numericitemcode = (lastnumericitemcode == null ? 0 : lastnumericitemcode.NumericItemCode) + 1;
                //return Ok(numericitemcode);

                long numericitemcode = 0;
                var lastnumericitemcode = _context.ProductDetails.Where(x => x.MasterBelongTo == pcatid && x.BelongTo == pscatid)
                    .OrderBy(x => x.NumericItemCode).LastOrDefault();
                if (lastnumericitemcode == null)
                {
                    var numericcodefromProSubCat = _context.ProductSubCategoryDetails.Where(x => x.ProductSubCategoryID == pscatid)
                        .OrderBy(x => x.ProductSubCategoryID).SingleOrDefault();

                    numericitemcode = numericcodefromProSubCat.ProductStartWith + 1;
                }
                else
                {
                    numericitemcode = lastnumericitemcode.NumericItemCode + 1;
                }
                return Ok(numericitemcode);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckBarcode")]
        public IActionResult CheckBarcode(string value)
        {
            try
            {
                var existingProduct = _context.ProductDetails.Where(x => x.Barcode == value).FirstOrDefault<ProductDetails>();
                if (existingProduct != null)
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

        [Route("CheckProductCode/{FitemId}")]
        public IActionResult CheckProductCode(string Code, Int64 FitemId)
        {
            try
            {
                if (FitemId > 0) // edit mode
                {
                    var existingProduct = _context.ProductDetails.Where(x => x.Code == Code && x.FitemId != FitemId).FirstOrDefault<ProductDetails>();
                    if (existingProduct != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
                else
                {
                    var existingProduct = _context.ProductDetails.Where(x => x.Code == Code).FirstOrDefault<ProductDetails>();
                    if (existingProduct != null)
                        return Ok(1);
                    else
                        return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("SearchProductList")]
        public ActionResult SearchProductList([FromBody]SearchModel obj)
        {
            object searchproduct;
            searchproduct = _context.ProductDetails.Where(x => x.Code.Contains(obj.searchvalue)).Select(x => new { FitemId = x.FitemId, Code = x.Code }).ToList();
            return Ok(searchproduct);
        }

        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                long ProductFileId = 0;
                var lastProductFile = _context.ProductFile.OrderBy(x => x.ProductFileId).LastOrDefault();
                ProductFileId = (lastProductFile == null ? 0 : lastProductFile.ProductFileId) + 1;
                return Ok(ProductFileId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductProcessPicturesId")]
        public IActionResult GetNewProductProcessPicturesId()
        {
            try
            {
                long id = 0;
                var last = _context.ProductProcessPictures.OrderBy(x => x.ProductProcessPicturesId).LastOrDefault();
                id = (last == null ? 0 : last.ProductProcessPicturesId) + 1;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductFileRecord/{id}")]
        public IActionResult GetProductFileRecord(long id)
        {
            try
            {
                var productfile = _context.ViewProductFile
                                        .Where(x => x.FitemId == id)
                                        .OrderBy(x => x.ProductFileId);
                return Ok(productfile);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductProcessDetails/{fitemid}/{processid}")]
        public IActionResult GetProductProcessDetails(long fitemid, int processid)
        {
            try
            {
                var record = _context.ViewProductProcessDetails
                                        .Where(x => x.FitemId == fitemid && x.ProcessID == processid).SingleOrDefault();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetProductProcessPicsRecord/{fitemid}/{processid}")]
        public IActionResult GetProductProcessPicsRecord(long fitemid, int processid)
        {
            try
            {
                var record = _context.ViewProductProcessPictures
                                        .Where(x => x.FitemId == fitemid && x.ProcessId == processid)
                                        .OrderBy(x => x.ProductProcessPicturesId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductProcessPicsRecord/{productprocesspicturesid}")]
        public IActionResult GetProductProcessPicsRecord(long productprocesspicturesid)
        {
            try
            {
                var record = _context.ViewProductProcessPictures
                                        .Where(x => x.ProductProcessPicturesId == productprocesspicturesid)
                                        .SingleOrDefault();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("TypeList")]
        public IActionResult TypeList()
        {
            try
            {
                var typelist = _context.ProductType
                                       .OrderBy(x => x.ProductTypeName)
                                       .Select(x => new { Id = x.ProductTypeId, Value = x.ProductTypeName }).ToList();
                return Ok(typelist);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewSampleRMId")]
        public IActionResult GetNewSampleRMId()
        {
            try
            {
                long SampleRMId = 0;
                var lastsamolerm = _context.SampleRM.OrderBy(x => x.SampleRMId).LastOrDefault();
                SampleRMId = (lastsamolerm == null ? 0 : lastsamolerm.SampleRMId) + 1;
                return Ok(SampleRMId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostSampleRM")]
        public IActionResult PostSampleRM([FromBody]List<SampleRM> value)
        {
            try
            {
                for (int i = 0; i < value.Count; i++)
                {
                    _context.SampleRM.Add(value[i]);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutSampleRM")]
        public IActionResult PutSampleRM([FromBody]List<SampleRM> value)
        {
            try
            {
                var existingsamplerm = _context.SampleRM
                        .Where(x => x.FitemId == value[0].FitemId)
                        .ToList();

                if (existingsamplerm != null)
                {
                    // Update Parent
                    //_context.Entry(existingPackWeight).CurrentValues.SetValues(value);

                    // Delete children of Master
                    foreach (var ExistingChild in existingsamplerm)
                    {
                        _context.SampleRM.Remove(ExistingChild);
                    }

                    for (int i = 0; i < value.Count; i++)
                    {
                        _context.SampleRM.Add(value[i]);
                    }
                    _context.SaveChanges();
                    return Ok("Record Updated");
                }
                else { return Ok("Error"); }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSampleRMRecord/{id}")]
        public IActionResult GetSampleRMRecord(long id)
        {
            try
            {
                var samplermlist = _context.ViewSampleRM
                                        .Where(x => x.FitemId == id)
                                        .OrderBy(x => x.SampleRMId);
                return Ok(samplermlist);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductForPrint/{id}")]
        public IQueryable<ViewProductShow> GetProductForPrint(long id)
        {
            return _context.ViewProductShow.Where(x => x.FitemId == id).AsQueryable();
        }

        [Route("GetProductDiePrint/{id}")]
        public IQueryable<ViewDieFitemDetails> GetProductDiePrint(long id)
        {
            return _context.ViewDieFitemDetails.Where(x => x.FitemId == id).AsQueryable();
        }

        [Route("ProductWithoutPhoto")]
        public IActionResult ProductWithoutPhoto()
        {
            try
            {
                var ProductIdlist = _context.ViewProductWithoutPhoto
                                         .OrderBy(x => x.FitemId)
                                         .Select(x => x.FitemId).ToList();
                var productwithoutphoto = _context.ViewProductWithoutPhoto.Where(x => ProductIdlist.Contains(x.FitemId));
                return Ok(productwithoutphoto);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductMultipleColorId")]
        public IActionResult GetNewProductMultipleColorId()
        {
            try
            {
                long ProductMultipleColorId = 0;
                var lastProductmulticolor = _context.ProductMultipleColor.OrderBy(x => x.ProductMultipleColorId).LastOrDefault();
                ProductMultipleColorId = (lastProductmulticolor == null ? 0 : lastProductmulticolor.ProductMultipleColorId) + 1;
                return Ok(ProductMultipleColorId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductMultipleSizeId")]
        public IActionResult GetNewProductMultipleSizeId()
        {
            try
            {
                long ProductMultipleSizeId = 0;
                var lastProductmultisize = _context.ProductMultipleSize.OrderBy(x => x.ProductMultipleSizeId).LastOrDefault();
                ProductMultipleSizeId = (lastProductmultisize == null ? 0 : lastProductmultisize.ProductMultipleSizeId) + 1;
                return Ok(ProductMultipleSizeId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductMultiColorRecord/{fitemid}")]
        public IActionResult GetProductMultiColorRecord(long fitemid)
        {
            try
            {
                var pmulcolor = _context.ProductMultipleColor.Where(x => x.FitemId == fitemid);
                return Ok(pmulcolor);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductMultiSizeRecord/{fitemid}")]
        public IQueryable<ProductMultipleSize> GetProductMultiSizeRecord(long fitemid)
        {
            return _context.ProductMultipleSize.Where(x => x.FitemId == fitemid).AsQueryable();
        }

        [Route("GetNewProductProcessId")]
        public IActionResult GetNewProductProcessId()
        {
            try
            {
                long ProductProcessId = 0;
                var lastProductprocess = _context.ProductProcess.OrderBy(x => x.ProductProcessID).LastOrDefault();
                ProductProcessId = (lastProductprocess == null ? 0 : lastProductprocess.ProductProcessID) + 1;
                return Ok(ProductProcessId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductProcessSNo/{id}")]
        public IActionResult GetNewProductProcessSNo(long id)
        {
            try
            {
                int ProductProcessSNo = 0;
                var lastProductprocess = _context.ProductProcess.Where(x => x.FitemId == id)
                                    .OrderBy(x => x.SNo)
                                    .LastOrDefault();
                ProductProcessSNo = (lastProductprocess == null ? 0 : lastProductprocess.SNo) + 1;
                return Ok(ProductProcessSNo);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("SearchProcessList")]
        public ActionResult SearchProcessList([FromBody]SearchModel obj)
        {
            object searchprocess;
            if (obj.searchvalue == null)
            {
                searchprocess = _context.ProductionProcessDetails
                                        .OrderBy(x => x.ProcessName)
                                        .Select(x => new { ProcessID = x.ProcessID, ProcessName = x.ProcessName })
                                        .ToList();
            }
            else
            {
                searchprocess = _context.ProductionProcessDetails
                                        .Where(x => x.ProcessName.Contains(obj.searchvalue))
                                        .Select(x => new { ProcessID = x.ProcessID, ProcessName = x.ProcessName })
                                        .ToList();
            }
            return Ok(searchprocess);
        }

        [Route("GetViewProductProcessList/{id}")]
        public IQueryable<ViewProductProcessDetails> GetViewProductProcessList(long id)
        {
            return _context.ViewProductProcessDetails.Where(x => x.FitemId == id).AsQueryable();
        }

        [Route("PutProductProcess")]
        public IActionResult PutProductProcess([FromBody]List<ProductProcess> value)
        {
            try
            {
                var existingProductProcess = _context.ProductProcess
                                                 .Where(x => x.FitemId == value[0].FitemId)
                                                 .ToList();

                if (existingProductProcess != null)
                {
                    //Delete children
                    foreach (var existingpp in existingProductProcess)
                    {
                        foreach (var childModel in value)
                        {
                            if (existingpp.ProcessID == childModel.ProcessID && existingpp.FitemId == childModel.FitemId)
                            {
                                _context.ProductProcess.Remove(existingpp);
                            }
                        }
                    }

                    foreach (var pp in value)
                    {
                        _context.ProductProcess.Add(pp);
                        _context.SaveChanges();
                    }
                    //return Ok("Record Updated");
                    return Ok("Process Assigned");
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

        [Route("RemoveProductProcess")]
        public IActionResult RemoveProductProcess([FromBody]List<ProductProcess> value)
        {
            try
            {
                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        var existingProductProcess = _context.ProductProcess
                                                 .Where(x => x.FitemId == value[i].FitemId && x.ProcessID == value[i].ProcessID)
                                                 .SingleOrDefault();

                        if (existingProductProcess != null)
                        {
                            // Delete children
                            _context.ProductProcess.Remove(existingProductProcess);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return Ok("Not Found");
                        }
                    }
                    return Ok("Removed Assigned Process");
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

        [Route("GetSampleRMPrint/{id}")]
        public IQueryable<ViewSampleRM> GetSampleRMPrint(long id)
        {
            return _context.ViewSampleRM.Where(x => x.FitemId == id).AsQueryable();
        }

        [HttpPost]
        [Route("ProductList")]
        public IActionResult ProductList([FromBody]ProductSearch PS)
        {
            try
            {
                IQueryable<ViewProductShow> query = _context.ViewProductShow;
                if (PS.ProductTypeId > 0)
                    query = query.Where(x => x.ProductTypeId == PS.ProductTypeId);
                if (PS.FItemCategory_ID > 0)
                    query = query.Where(x => x.ProductCategoryID == PS.FItemCategory_ID);
                if (PS.FItemSubCategory_ID > 0)
                    query = query.Where(x => x.ProductSubCategoryID == PS.FItemSubCategory_ID);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                if (PS.BuyerId > 0)
                    query = query.Where(x => x.BuyerId == PS.BuyerId);
                if (PS.FItemId > 0)
                    query = query.Where(x => x.FitemId == PS.FItemId);
                if (!string.IsNullOrEmpty(PS.PartyCode))
                    query = query.Where(x => x.PartyCode.Contains(PS.PartyCode));

                if (PS.ChkCreateDateFromInt == 1 && PS.ChkCreateDateToInt == 1)
                    query = query.Where(x => x.CreatedDate >= (PS.CreateDateFrom) && x.CreatedDate <= PS.CreateDateTo);
                if (PS.ChkCreateDateFromInt == 1 && PS.ChkCreateDateToInt == 0)
                    query = query.Where(x => x.CreatedDate == (PS.CreateDateFrom));

                query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ProductProcessList")]
        public IActionResult ProductProcessList([FromBody]ProductSearch PS)
        {
            try
            {
                IQueryable<ViewProductProcessDetails> query = _context.ViewProductProcessDetails;
                //if (PS.ProductTypeId > 0)
                //    query = query.Where(x => x.ProductTypeId == PS.ProductTypeId);
                //if (PS.FItemCategory_ID > 0)
                //    query = query.Where(x => x.ProductCategoryID == PS.FItemCategory_ID);
                //if (PS.FItemSubCategory_ID > 0)
                //    query = query.Where(x => x.ProductSubCategoryID == PS.FItemSubCategory_ID);
                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));

                //if (PS.BuyerId > 0)
                //    query = query.Where(x => x.BuyerId == PS.BuyerId);
                //if (!string.IsNullOrEmpty(PS.PartyCode))
                //    query = query.Where(x => x.PartyCode.Contains(PS.PartyCode));

                //if (PS.ChkCreateDateFromInt == 1 && PS.ChkCreateDateToInt == 1)
                //    query = query.Where(x => x.CreatedDate >= (PS.CreateDateFrom) && x.CreatedDate <= PS.CreateDateTo);
                //if (PS.ChkCreateDateFromInt == 1 && PS.ChkCreateDateToInt == 0)
                //    query = query.Where(x => x.CreatedDate == (PS.CreateDateFrom));

                query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostProductFile")]
        public IActionResult PostProductFile([FromBody]ProductFile value)
        {
            try
            {
                _context.ProductFile.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostProductProcessPictures")]
        public IActionResult PostProductProcessPictures([FromBody]List<ProductProcessPictures> value)
        {
            try
            {
                foreach (var item in value)
                {
                    _context.ProductProcessPictures.Add(item);
                }
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteDocument/{id}")]
        public IActionResult DeleteDocument(long id)
        {
            var existing = _context.ProductFile.Find(id);
            if (existing != null)
            {
                _context.ProductFile.Remove(existing);
                _context.SaveChanges();
                return Ok("1");
            }
            else
            {
                return Ok("0");
            }
        }

        [Route("DeleteProductProcessPictures/{productprocesspicturesid}")]
        public IActionResult DeleteProductProcessPictures(long productprocesspicturesid)
        {
            try
            {
                var existing = _context.ProductProcessPictures.Find(productprocesspicturesid);
                if (existing != null)
                {
                    _context.ProductProcessPictures.Remove(existing);
                    _context.SaveChanges();
                    return Ok("1");
                }
                else
                {
                    return Ok("0");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ProductFileName/{id}")]
        public IActionResult ProductFileName(long id)
        {
            try
            {
                var filename = _context.ProductFile.Where(x => x.ProductFileId == id).Select(x => x.FileLocation).FirstOrDefault(); ;
                return Ok(filename);
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
                var product = _context.ViewProductShow.Where(x => x.Barcode == barcode)
                                    .OrderByDescending(x => x.FitemId).FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }

        }

        [Route("GetPartyFItemListWithPartyCode/{Partyid}")]
        public IActionResult GetPartyFItemListWithPartyCode(long Partyid)
        {
            try
            {
                object obj = _context.ViewProductShow
                                        .Where(c => c.BuyerId == Partyid)
                                        .Select(c => new { Id = c.FitemId, value = c.Code + " ## " + c.PartyCode })
                                        .OrderBy(c => c.value)
                                        .ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("PostCreateSubitems")]
        public IActionResult PostCreateSubitems([FromBody]ProductSearch value)
        {
            try
            {
                #region ProductSubItems

                var _productmodel = _context.ProductDetails.Where(x => x.FitemId == value.FItemId)
                                   .Include(x => x.ProductMultipleSize)
                                   .SingleOrDefault();

                var NewFitemId = _context.ProductDetails.Max(x => x.FitemId);
                NewFitemId++;

                ProductDetails product = new ProductDetails();
                product.FitemId = NewFitemId;
                product.SubItemsBelongTo = value.FItemId;

                product.Code = value.Code;// _productmodel.Code;
                product.ItOrCat = 2;// _productmodel.ItOrCat;
                product.SessionYear = _productmodel.SessionYear;
                product.UserId = _productmodel.UserId;

                product.PhotoPath = _productmodel.PhotoPath;
                product.Name = _productmodel.Name;
                product.MasterBelongTo = _productmodel.MasterBelongTo;
                product.BelongTo = _productmodel.BelongTo;
                product.Description = _productmodel.Description;
                product.ItemWeight = _productmodel.ItemWeight;
                product.Quantity = _productmodel.Quantity;
                product.UpdatedBy = _productmodel.UpdatedBy;
                product.CreatedDate = _productmodel.CreatedDate;
                product.PartyDate = _productmodel.PartyDate;
                product.TypeOfSample = _productmodel.TypeOfSample;
                product.OrderNo = _productmodel.OrderNo;
                product.Customer = _productmodel.Customer;
                product.CreatedBy = _productmodel.CreatedBy;
                product.Finish = _productmodel.Finish;
                product.Color = _productmodel.Color;
                product.Designer = _productmodel.Designer;
                product.GroupName = _productmodel.GroupName;
                product.SizeId = _productmodel.SizeId;
                product.Size = _productmodel.Size;
                product.Sticker = _productmodel.Sticker;
                product.Trimming = _productmodel.Trimming;
                product.Backing = _productmodel.Backing;
                product.Lining = _productmodel.Lining;
                product.LiningColour = _productmodel.LiningColour;
                product.Labels = _productmodel.Labels;
                product.CareLabels = _productmodel.CareLabels;
                product.Embossment = _productmodel.Embossment;
                product.Wax = _productmodel.Wax;
                product.Wash = _productmodel.Wash;
                product.Quality = _productmodel.Quality;
                product.Hangtags = _productmodel.Hangtags;
                product.Stitching = _productmodel.Stitching;

                product.NumericItemCode = _productmodel.NumericItemCode;
                product.Suffix = _productmodel.Suffix;
                product.CodeAlias = _productmodel.CodeAlias;
                product.PartyCode = _productmodel.PartyCode;
                product.Barcode = _productmodel.Barcode;
                product.Logo = _productmodel.Logo;
                product.Price = _productmodel.Price;
                product.StyleId = _productmodel.StyleId;
                product.RitemId = _productmodel.RitemId;
                product.ProductTypeId = _productmodel.ProductTypeId;

                product.ShortName = _productmodel.ShortName; ;
                product.StartWith = _productmodel.StartWith;
                product.SubCode = _productmodel.SubCode;
                product.ItemCode = _productmodel.ItemCode;
                product.UpdatedDate = _productmodel.UpdatedDate;
                product.Supplier = _productmodel.Supplier;
                product.SupplierContact = _productmodel.SupplierContact;
                product.CustomerContact = _productmodel.CustomerContact;
                product.ReqDelivery = _productmodel.ReqDelivery;
                product.DeliveryDate = _productmodel.DeliveryDate;
                product.Office = _productmodel.Office;
                product.Unit = _productmodel.Unit;
                product.DesignDesc = _productmodel.DesignDesc;
                product.ColorDesc = _productmodel.ColorDesc;
                product.Sketch = _productmodel.Sketch;
                product.Note = _productmodel.Note;
                product.OtherAttachment = _productmodel.OtherAttachment;
                product.FinishStatus = _productmodel.FinishStatus;
                product.ProductionTime = _productmodel.ProductionTime;
                product.Length = _productmodel.Length;
                product.Width = _productmodel.Width;
                product.Height = _productmodel.Height;
                product.ManDays = _productmodel.ManDays;
                product.ActualDispatchDate = _productmodel.ActualDispatchDate;
                product.MessageStatus = _productmodel.MessageStatus;
                product.Treatment = _productmodel.Treatment;
                product.TreatmentName = _productmodel.TreatmentName;
                product.ItemInvoiceName = _productmodel.ItemInvoiceName;
                product.ItemInvoiceCode = _productmodel.ItemInvoiceCode;
                product.ByRefrence = _productmodel.ByRefrence;
                product.SizeGroupId = _productmodel.SizeGroupId;
                product.SoleName = _productmodel.SoleName;
                product.ZipperName = _productmodel.ZipperName;
                product.BuyerId = _productmodel.BuyerId;
                product.IsActive = _productmodel.IsActive;
                product.IsOpenForAll = _productmodel.IsOpenForAll;
                product.Online_Transfer = _productmodel.Online_Transfer;
                product.Paper1 = _productmodel.Paper1;
                product.Paper2 = _productmodel.Paper2;
                product.Color2 = _productmodel.Color2;
                product.Print = _productmodel.Print;
                product.Thickness = _productmodel.Thickness;
                product.Fabric1 = _productmodel.Fabric1;
                product.Fabric2 = _productmodel.Fabric2;
                product.PRDWidth = _productmodel.PRDWidth;
                product.Connstruction = _productmodel.Connstruction;
                product.GenderId = _productmodel.GenderId;
                product.Heel = _productmodel.Heel;
                product.GSM = _productmodel.GSM;


                //---------Product Multiple Size--------------
                long ProductMultipleSizeId = _context.ProductMultipleSize.Max(x => x.ProductMultipleSizeId);

                List<ProductMultipleSize> _listProductMultipleSize = new List<ProductMultipleSize>();
                ProductMultipleSize _oProductMultipleSize;
                foreach (var child in _productmodel.ProductMultipleSize)
                {
                    _oProductMultipleSize = new ProductMultipleSize();
                    _oProductMultipleSize.FitemId = product.FitemId;
                    ProductMultipleSizeId++;
                    _oProductMultipleSize.ProductMultipleSizeId = ProductMultipleSizeId;

                    _oProductMultipleSize.SizeId = child.SizeId;
                    _oProductMultipleSize.SizePrice = child.SizePrice;
                    _oProductMultipleSize.SizeBarcode = child.SizeBarcode;
                    _oProductMultipleSize.SizePartyCode = child.SizePartyCode;
                    _listProductMultipleSize.Add(_oProductMultipleSize);
                }
                product.ProductMultipleSize = _listProductMultipleSize;

                _context.ProductDetails.Add(product);
                _context.SaveChanges();
                #endregion

                // copy costing
                //select* from CostingRM
                //select* from CostingRMComponent
                //select* from CostingMaster


                var existingCosting = _context.CostingMaster
                       .Where(x => x.FitemID == value.FItemId)
                       .Include(x => x.CostingRM)
                       .Include(x => x.CostingRMComponent)
                       .FirstOrDefault();

                if (existingCosting != null)
                {
                    long CostingID = _context.CostingMaster.Max(x => x.CostingID);
                    long CostingRMID = _context.CostingRM.Max(x => x.CostingRMID);
                    long CostingRMComponentID = _context.CostingRMComponent.Max(x => x.CostingRMComponentID);

                    CostingID++;
                    CostingRMID++;
                    CostingRMComponentID++;

                    CostingMaster _objCostingMaster = new CostingMaster();
                    //_objCostingMaster = existingCosting;

                    _objCostingMaster.CostingID = CostingID;
                    _objCostingMaster.FitemID = NewFitemId;

                    _objCostingMaster.CLID = existingCosting.CLID;
                    _objCostingMaster.RMTotal = existingCosting.RMTotal;
                    _objCostingMaster.ElementTotal = existingCosting.ElementTotal;
                    _objCostingMaster.Forwording = existingCosting.Forwording;
                    _objCostingMaster.Freight = existingCosting.Freight;
                    _objCostingMaster.Packing = existingCosting.Packing;
                    _objCostingMaster.Labour = existingCosting.Labour;
                    _objCostingMaster.GrandTotal = existingCosting.GrandTotal;
                    _objCostingMaster.OHPer = existingCosting.OHPer;
                    _objCostingMaster.OHPerAmt = existingCosting.OHPerAmt;
                    _objCostingMaster.OHAmt = existingCosting.OHAmt;
                    _objCostingMaster.ProfitPer = existingCosting.ProfitPer;
                    _objCostingMaster.ProfitPerAmt = existingCosting.ProfitPerAmt;
                    _objCostingMaster.ProfitAmt = existingCosting.ProfitAmt;
                    _objCostingMaster.ComPer = existingCosting.ComPer;
                    _objCostingMaster.ComPerAmt = existingCosting.ComPerAmt;
                    _objCostingMaster.ComAmt = existingCosting.ComAmt;
                    _objCostingMaster.CostingPrice = existingCosting.CostingPrice;
                    _objCostingMaster.SEAFreight = existingCosting.SEAFreight;
                    _objCostingMaster.AIRFreight = existingCosting.AIRFreight;
                    _objCostingMaster.CostingAfterLabour = existingCosting.CostingAfterLabour;
                    _objCostingMaster.CostingAfterTaxes = existingCosting.CostingAfterTaxes;
                    _objCostingMaster.ManDays = existingCosting.ManDays;
                    _objCostingMaster.Sessionyear = existingCosting.Sessionyear;
                    _objCostingMaster.UserId = existingCosting.UserId;
                    _objCostingMaster.Remark = existingCosting.Remark;
                    _objCostingMaster.FormType = 1;// existingCosting.FormType;
                    _objCostingMaster.CreationDate = existingCosting.CreationDate;
                    _objCostingMaster.IdentifiedBy = existingCosting.IdentifiedBy;
                    _objCostingMaster.BomRMTotal = existingCosting.BomRMTotal;
                    _objCostingMaster.BomGrandTotal = existingCosting.BomGrandTotal;
                    _objCostingMaster.BOMOHPerAmt = existingCosting.BOMOHPerAmt;
                    _objCostingMaster.BomOHAmt = existingCosting.BomOHAmt;
                    _objCostingMaster.BOMProfitPerAmt = existingCosting.BOMProfitPerAmt;
                    _objCostingMaster.BomProfitAmt = existingCosting.BomProfitAmt;
                    _objCostingMaster.BOMComPerAmt = existingCosting.BOMComPerAmt;
                    _objCostingMaster.BomComAmt = existingCosting.BomComAmt;
                    _objCostingMaster.BomCostingPrice = existingCosting.BomCostingPrice;
                    _objCostingMaster.BomAfterLabour = existingCosting.BomAfterLabour;
                    _objCostingMaster.BomAfterTax = existingCosting.BomAfterTax;
                    _objCostingMaster.HighPer = existingCosting.HighPer;
                    _objCostingMaster.MedPer = existingCosting.MedPer;
                    _objCostingMaster.Lowper = existingCosting.Lowper;
                    _objCostingMaster.Increaseper = existingCosting.Increaseper;
                    _objCostingMaster.FinishInHouse = existingCosting.FinishInHouse;
                    _objCostingMaster.Coments = existingCosting.Coments;
                    _objCostingMaster.SizeId = existingCosting.SizeId;
                    _objCostingMaster.CLID = existingCosting.CLID;
                    _objCostingMaster.UpdationDate = existingCosting.UpdationDate;
                    _objCostingMaster.MatReviewedStatus = existingCosting.MatReviewedStatus;
                    _objCostingMaster.MatReviewedDate = existingCosting.MatReviewedDate;
                    _objCostingMaster.MatReviewedUserId = existingCosting.MatReviewedUserId;
                    _objCostingMaster.PriceInUSD = existingCosting.PriceInUSD;



                    List<CostingRM> _listCostingRM = new List<CostingRM>();
                    CostingRM _objCostingRM;
                    foreach (var childRM in existingCosting.CostingRM)
                    {
                        _objCostingRM = new CostingRM();

                        _objCostingRM.CostingID = CostingID;
                        _objCostingRM.CostingRMID = CostingRMID;

                        _objCostingRM.RItemID = childRM.RItemID;
                        _objCostingRM.RMQty = childRM.RMQty;
                        _objCostingRM.CWAS = childRM.CWAS;
                        _objCostingRM.CAfterWAS = childRM.CAfterWAS;
                        _objCostingRM.RMPrice = childRM.RMPrice;
                        _objCostingRM.RMCAmount = childRM.RMCAmount;
                        _objCostingRM.BOMWAS = childRM.BOMWAS;
                        _objCostingRM.BOMAfterWas = childRM.BOMAfterWas;
                        _objCostingRM.RMBAmount = childRM.RMBAmount;
                        _objCostingRM.SerialNo = childRM.SerialNo;
                        _objCostingRM.IsComponent = childRM.IsComponent;
                        _objCostingRM.SupplierId = childRM.SupplierId;
                        CostingRMID++;
                        _listCostingRM.Add(_objCostingRM);
                    }
                    _objCostingMaster.CostingRM = _listCostingRM;

                    //////
                    List<CostingRMComponent> _listCostingRMComponent = new List<CostingRMComponent>();
                    CostingRMComponent _objCostingRMComponent;
                    foreach (var childRMComp in existingCosting.CostingRMComponent)
                    {
                        _objCostingRMComponent = new CostingRMComponent();

                        _objCostingRMComponent.CostingID = CostingID;
                        _objCostingRMComponent.CostingRMComponentID = CostingRMComponentID;

                        _objCostingRMComponent.RitemId = childRMComp.RitemId;
                        _objCostingRMComponent.CompID = childRMComp.CompID;
                        _objCostingRMComponent.Length = childRMComp.Length;
                        _objCostingRMComponent.Breadth = childRMComp.Breadth;
                        _objCostingRMComponent.Height = childRMComp.Height;
                        _objCostingRMComponent.CompQty = childRMComp.CompQty;
                        _objCostingRMComponent.Area = childRMComp.Area;
                        _objCostingRMComponent.SerialNo = childRMComp.SerialNo;
                        _objCostingRMComponent.CompGroupId = childRMComp.CompGroupId;
                        _objCostingRMComponent.Remark = childRMComp.Remark;
                        _objCostingRMComponent.Photo = childRMComp.Photo;
                        _objCostingRMComponent.RMQty = childRMComp.RMQty;
                        _objCostingRMComponent.SupplierId = childRMComp.SupplierId;
                        _objCostingRMComponent.ProcessID = childRMComp.ProcessID;
                        CostingRMComponentID++;
                        _listCostingRMComponent.Add(_objCostingRMComponent);

                    }
                    _objCostingMaster.CostingRMComponent = _listCostingRMComponent; ;


                    /////////////
                    List<CostingCurrencyDetails> _listCostingCurrencyDetails = new List<CostingCurrencyDetails>();
                    //CostingCurrencyDetails _objCostingCurrencyDetails;
                    //if (existingCosting.CostingCurrencyDetails != null)
                    //{
                    //    foreach (var costingcurrency in existingCosting.CostingCurrencyDetails)
                    //    {
                    //        _objCostingCurrencyDetails = new CostingCurrencyDetails();
                    //        _objCostingCurrencyDetails = costingcurrency;
                    //        _objCostingCurrencyDetails.CostingId = CostingID;
                    //        _objCostingCurrencyDetails.CostingCurrencyId = CostingCurrencyId;
                    //        _listCostingCurrencyDetails.Add(_objCostingCurrencyDetails);
                    //        CostingCurrencyId++;
                    //    }
                    //}
                    _objCostingMaster.CostingCurrencyDetails = _listCostingCurrencyDetails;


                    _context.CostingMaster.Add(_objCostingMaster);
                    _context.SaveChanges();

                }
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("SearchproductcodeList")]
        public ActionResult SearchproductcodeList([FromBody]SearchModel obj)
        {
            object searchproduct;
            if (obj.searchvalue == null)
            {
                searchproduct = _context.ViewProductDetails
                                        .OrderBy(x => x.Code)
                                        .Select(x => new { FitemId = x.FitemId, Code = x.Code })
                                         .OrderBy(x => x.Code)
                                        .ToList();
            }
            else
            {
                searchproduct = _context.ViewProductDetails
                                        .Where(x => x.Code.Contains(obj.searchvalue))
                                        .Select(x => new { FitemId = x.FitemId, Code = x.Code })
                                        .OrderBy(x => x.Code)
                                        .ToList();
            }
            return Ok(searchproduct);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Route("GetNewProductComponentId")]
        public IActionResult GetNewProductComponentId()
        {
            try
            {
                long ProductComponentid = 0;
                var lastProductComponent = _context.ProductComponent.OrderBy(x => x.ProductComponentId).LastOrDefault();
                ProductComponentid = (lastProductComponent == null ? 0 : lastProductComponent.ProductComponentId) + 1;
                return Ok(ProductComponentid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewProductComponentSNo/{id}")]
        public IActionResult GetNewProductComponentSNo(long id)
        {
            try
            {
                int ProductComponentSNo = 0;
                var lastProductComponent = _context.ProductComponent.Where(x => x.FitemId == id)
                                    .OrderBy(x => x.SNo)
                                    .LastOrDefault();
                ProductComponentSNo = (lastProductComponent == null ? 0 : lastProductComponent.SNo) + 1;
                return Ok(ProductComponentSNo);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutProductComponent")]
        public IActionResult PutProductComponent([FromBody]List<ProductComponent> value)
        {
            try
            {
                var existingProductcomponent = _context.ProductComponent
                                                 .Where(x => x.FitemId == value[0].FitemId)
                                                 .ToList();

                if (existingProductcomponent != null)
                {
                    //Delete children
                    foreach (var existingpp in existingProductcomponent)
                    {
                        foreach (var childModel in value)
                        {
                            if (existingpp.Comp_Id == childModel.Comp_Id && existingpp.FitemId == childModel.FitemId)
                            {
                                _context.ProductComponent.Remove(existingpp);
                            }
                        }
                    }

                    foreach (var pp in value)
                    {
                        _context.ProductComponent.Add(pp);
                        _context.SaveChanges();
                    }
                    return Ok("Component Saved");
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


        [Route("GetViewProductComponentList/{id}")]
        public IQueryable<ViewProductComponent> GetViewProductComponentList(long id)
        {
            return _context.ViewProductComponent.Where(x => x.FitemId == id).AsQueryable();
        }


        [Route("RemoveProductComponent")]
        public IActionResult RemoveProductComponent([FromBody]List<ProductComponent> value)
        {
            try
            {
                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        var existingProductComponent = _context.ProductComponent
                                                 .Where(x => x.FitemId == value[i].FitemId && x.Comp_Id == value[i].Comp_Id)
                                                 .SingleOrDefault();

                        if (existingProductComponent != null)
                        {
                            // Delete children
                            _context.ProductComponent.Remove(existingProductComponent);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return Ok("Not Found");
                        }
                    }
                    return Ok("Removed Saved Component");
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

        #region ProductQCPoint

        [Route("CheckDuplicateProductQCPoint")]
        public IActionResult CheckDuplicateProductQCPoint(string value)
        {
            try
            {
                var existingData = _context.ProductQCPointMaster.Where(x => x.QCPoint == value).FirstOrDefault<ProductQCPointMaster>();
                if (existingData != null)
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

        [Route("GetNewProductQCPointId")]
        public IActionResult GetNewProductQCPointId()
        {
            try
            {
                long QCPointID = 0;
                var lastProductQCPoint = _context.ProductQCPointMaster.OrderBy(x => x.QCPointID).LastOrDefault();
                QCPointID = (lastProductQCPoint == null ? 0 : lastProductQCPoint.QCPointID) + 1;
                return Ok(QCPointID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostProductQCPoint")]
        public IActionResult PostProductQCPoint([FromBody]ProductQCPointMaster value)
        {
            try
            {
                _context.ProductQCPointMaster.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductQCPointList")]
        public IActionResult GetProductQCPointList(string search)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(_context.ProductQCPointMaster.OrderByDescending(x => x.QCPointID));
            else
                return Ok(_context.ProductQCPointMaster
                    .Where(x => x.QCPoint.Contains(search))
                    .OrderByDescending(x => x.QCPointID));
        }


        [Route("GetProductQCPoint/{id}")]
        public IActionResult GetProductQCPoint(int id)
        {
            try
            {
                return Ok(_context.ProductQCPointMaster.Where(x => x.QCPointID == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteProductQCPoint/{id}")]
        public IActionResult DeleteProductQCPoint(int id)
        {
            var existingData = _context.ProductQCPointMaster.Where(x => x.QCPointID == id).FirstOrDefault();

            if (existingData != null)
            {
                _context.ProductQCPointMaster.Remove(existingData);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("PutProductQCPoint")]
        public IActionResult PutProductQCPoint([FromBody]ProductQCPointMaster value)
        {
            var existingData = _context.ProductQCPointMaster.Where(x => x.QCPointID == value.QCPointID).FirstOrDefault<ProductQCPointMaster>();
            if (existingData == null)
            {
                return NotFound();
            }
            existingData.QCPoint = value.QCPoint;
            existingData.QCRemark = value.QCRemark;
            _context.ProductQCPointMaster.Update(existingData);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("ProductQCPointList")]
        public IActionResult ProductQCPointList()
        {
            try
            {
                object qcpoint;
                qcpoint = _context.ProductQCPointMaster
                                    .OrderBy(c => c.QCPoint)
                                    .Select(x => new { Id = x.QCPointID, Value = x.QCPoint }).ToList();

                return Ok(qcpoint);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutProductQC")]
        public IActionResult PutProductQC([FromBody]List<ProductQC> value)
        {
            try
            {
                var existingData = _context.ProductQC
                                                 .Where(x => x.FitemId == value[0].FitemId)
                                                 .ToList();

                if (existingData != null)
                {
                    //Delete children
                    foreach (var existingproductqc in existingData)
                    {
                        foreach (var childModel in value)
                        {
                            if (existingproductqc.QCPointID == childModel.QCPointID && existingproductqc.FitemId == childModel.FitemId)
                            {
                                _context.ProductQC.Remove(existingproductqc);
                            }
                        }
                    }

                    foreach (var qc in value)
                    {
                        _context.ProductQC.Add(qc);
                        _context.SaveChanges();
                    }

                    return Ok("Product QC Saved ");
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

        [Route("RemoveProductQC")]
        public IActionResult RemoveProductQC([FromBody]List<ProductQC> value)
        {
            try
            {
                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        var existingProductQC = _context.ProductQC
                                                 .Where(x => x.FitemId == value[i].FitemId && x.QCPointID == value[i].QCPointID)
                                                 .SingleOrDefault();

                        if (existingProductQC != null)
                        {
                            // Delete children
                            _context.ProductQC.Remove(existingProductQC);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return Ok("Not Found");
                        }
                    }
                    return Ok("Removed Assigned QC");
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


        [Route("SearchQCPointChekedList")]
        public ActionResult SearchQCPointChekedList()
        {
            object record;
            record = _context.ProductQCPointMaster.OrderBy(x => x.QCPoint).ToList();
            return Ok(record);
        }


        [Route("GetExQCPointChekedList/{FitemId}")]
        public IQueryable<ViewProductQC> GetExQCPointChekedList(long FitemId)
        {
            var data = _context.ViewProductQC.Where(x => x.FitemId == FitemId).AsQueryable();
            return data;
        }

        [Route("GetNewQCId")]
        public IActionResult GetNewQCId()
        {
            try
            {
                long QCID = 0;
                var lastQCPoint = _context.ProductQC.OrderBy(x => x.QCID).LastOrDefault();
                QCID = (lastQCPoint == null ? 0 : lastQCPoint.QCID) + 1;
                return Ok(QCID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("ProductQCList")]
        public IActionResult ProductQCList([FromBody]ProductSearch PS)
        {
            try
            {
                IQueryable<ViewProductQC> query = _context.ViewProductQC;

                if (!string.IsNullOrEmpty(PS.Code))
                    query = query.Where(x => x.Code.Contains(PS.Code));


                if (PS.FItemId > 0)
                    query = query.Where(x => x.FitemId == PS.FItemId);

                if (PS.QCPointID > 0)
                    query = query.Where(x => x.QCPointID == PS.QCPointID);

                query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetProductQCPrint/{id}")]
        public IQueryable<ViewProductQC> GetProductQCPrint(long id)
        {
            return _context.ViewProductQC.Where(x => x.FitemId == id).AsQueryable();
        }

        #endregion


        [Route("PutBarcodeFromPacking")]
        public IActionResult PutBarcodeFromPacking([FromBody]ProductMultipleSize value)
        {
            var existing = _context.ProductMultipleSize.Where(x => x.FitemId == value.FitemId && x.SizeId == value.SizeId).FirstOrDefault<ProductMultipleSize>();
            if (existing == null)
            {
                return Ok("Product Not Found");
            }
            existing.SizeBarcode = value.SizeBarcode;
            _context.ProductMultipleSize.Update(existing);
            _context.SaveChanges();
            return Ok("Update Sucessfully");
        }

        [Route("GetProductListOfCategory/{masterbelongto}")]
        public IActionResult GetProductListOfCategory(int masterbelongto)
        {
            try
            {
                object ProductL = _context.ProductDetails.Where(x => x.MasterBelongTo == masterbelongto)
                    .OrderBy(c => c.Code)
                    .Select(x => new { Id = x.FitemId, Value = x.Code }).ToList();

                return Ok(ProductL);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
    }
}
