using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductDetails
    {
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public long? StartWith { get; set; }
        public string Code { get; set; }
        public string SubCode { get; set; }
        public string Description { get; set; }
        public long MasterBelongTo { get; set; }
        public long BelongTo { get; set; }
        public int ItOrCat { get; set; }
        public string ItemCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string OrderNo { get; set; }
        public string TypeOfSample { get; set; }
        public string Supplier { get; set; }
        public string SupplierContact { get; set; }
        public string Customer { get; set; }
        public string CustomerContact { get; set; }
        public DateTime? ReqDelivery { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Office { get; set; }
        public string Unit { get; set; }
        public string Quality { get; set; }
        public string Designer { get; set; }
        public string DesignDesc { get; set; }
        public int Color { get; set; }
        public string ColorDesc { get; set; }
        public string Quantity { get; set; }
        public string Size { get; set; }
        public string Hangtags { get; set; }
        public string Sticker { get; set; }
        public string Backing { get; set; }
        public string Stitching { get; set; }
        public string Trimming { get; set; }
        public string Sketch { get; set; }
        public string Lining { get; set; }
        public string Labels { get; set; }
        public string CareLabels { get; set; }
        public string Note { get; set; }
        public string OtherAttachment { get; set; }
        public string Embossment { get; set; }
        public double Price { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int? FinishStatus { get; set; }
        public string Finish { get; set; }
        public double? ProductionTime { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public double? ManDays { get; set; }
        public DateTime? ActualDispatchDate { get; set; }
        public string MessageStatus { get; set; }
        public string Wash { get; set; }
        public string Wax { get; set; }
        public string LiningColour { get; set; }
        public int? Treatment { get; set; }
        public string TreatmentName { get; set; }
        public string ItemWeight { get; set; }
        public string ItemInvoiceName { get; set; }
        public string ItemInvoiceCode { get; set; }
        public string GroupName { get; set; }
        public string ByRefrence { get; set; }
        public int? SizeGroupId { get; set; }
        public int SizeId { get; set; }
        public DateTime? PartyDate { get; set; }
        public string PhotoPath { get; set; }

        public long NumericItemCode { get; set; }
        public string Suffix { get; set; }
        public string CodeAlias { get; set; }
        public string PartyCode { get; set; }
        public string Barcode { get; set; }
        public string Logo { get; set; }
        public int StyleId { get; set; }
        public long RitemId { get; set; }
        public int ProductTypeId { get; set; }
        public string SoleName { get; set; }
        public string ZipperName { get; set; }
        public long BuyerId { get; set; }
        public int IsActive { get; set; }
        public int IsOpenForAll { get; set; }
        public int Online_Transfer { get; set; }

        public string Paper1 { set; get; }
        public string Paper2 { set; get; }
        public string Color2 { set; get; }
        public string Print { set; get; }
        public string Thickness { set; get; }

        public string Fabric1 { set; get; }
        public string Fabric2 { set; get; }
        public string PRDWidth { get; set; }
        public string Connstruction { get; set; }
        public int GenderId { get; set; }
        public string Heel { get; set; }
        public string GSM { set; get; }
        public long SubItemsBelongTo { set; get; }

        public int IsSizeRun { get; set; }

        public virtual ICollection<SampleRM> SampleRM { get; set; }
        public virtual ICollection<ProductMultipleColor> ProductMultipleColor { get; set; }
        public virtual ICollection<ProductMultipleSize> ProductMultipleSize { get; set; }
        public virtual ICollection<ProductProcess> ProductProcess { get; set; }
    }
}
