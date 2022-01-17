using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewInvoiceChild
    {
        //Invoice 
        public long InvoiceID { get; set; }
        //public long InvoiceSNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long PackingID { get; set; }
        //public long BuyerID { get; set; }
        //public int CompanyId { get; set; }
        //public long BranchId { get; set; }
        //public int CID { get; set; }
        //public long ConsigneeId { get; set; }
        //public string ConsigneeAdd { get; set; }
        //public string Notify { get; set; }
        //public string Precarrigeby { get; set; }
        //public string PlaceofReceipt { get; set; }
        //public string VesselFlightNo { get; set; }
        //public string PortofLoading { get; set; }
        //public string PortofDischarge { get; set; }
        //public string PortofDelivery { get; set; }
        //public string AwbbINo { get; set; }
        //public string BuyerOrderno { get; set; }
        //public string OtherRefrence { get; set; }
        //public string BuyerifOther { get; set; }
        //public string CountryofOrigin { get; set; }
        //public string CountryofDestination { get; set; }
        //public string FinalDestination { get; set; }
        //public string Terms { get; set; }
        //public string InvoiceType { get; set; }
        //public string ExporterRefrence { get; set; }
        //public string PayemntTerm { get; set; }
        //public string BLAWB { get; set; }
        //public int FOBCIFID { get; set; }
        //public string Transport { get; set; }
        //public string PackingRemark { get; set; }
        public int TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public string PackingMarks { get; set; }
        public string ShippingMarks { get; set; }
        public string CartonsDimension { get; set; }
        public int TotalCarton { get; set; }
        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TotalVol5 { get; set; }
        public double TotalVol6 { get; set; }
        public double Freight { get; set; }
        //public DateTime CustomPIDate { get; set; }
        //public string AccountNo { get; set; }
        //public string CustomPITerm { get; set; }
        //public string CustomPIPayment { get; set; }
        public double InvoiceAmount { get; set; }
        public string AmtWords { get; set; }
        //public string SessionYear { get; set; }
        //public int UserID { get; set; }

        //invoiceChild
        public long InvoiceChildID { get; set; }
        //public long InvoiceID { get; set; }
        public long FitemID { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int SNo { get; set; }

        //product
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PartyCode { get; set; }
        public int Color { get; set; }
        public string Unit { get; set; }

        //color
        public string CLName { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        


    }
}
