using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPIPrint
    {
        //pimaster
        public long PIMasterId { get; set; }
        public long PISNo { get; set; }
        public string PINo { get; set; }
        public DateTime PIDate { get; set; }
        public long BuyerID { get; set; }
        public long ConsigneeId { get; set; }
        public string ConsigneeAdd { get; set; }
        public long CID { get; set; }
        public long OrderId { get; set; }
        public int CompanyId { get; set; }
        public long BranchId { get; set; }
        public string Notify { get; set; }
        public string Precarrigeby { get; set; }
        public string PlaceofReceipt { get; set; }
        public string VesselFlightNo { get; set; }
        public string PortofLoading { get; set; }
        public string PortofDischarge { get; set; }
        public string PortofDelivery { get; set; }
        public string AwbbINo { get; set; }
        public string BuyerOrderno { get; set; }
        public string OtherRefrence { get; set; }
        public string BuyerifOther { get; set; }
        public string CountryofOrigin { get; set; }
        public string CountryofDestination { get; set; }
        public string FinalDestination { get; set; }
        public string Terms { get; set; }
        public string ExporterRefrence { get; set; }
        public string PayemntTerm { get; set; }
        public string BLAWB { get; set; }
        public int FOBCIFID { get; set; }
        public DateTime CustomPIDate { get; set; }
        public string AccountNo { get; set; }
        public string CustomPITerm { get; set; }
        public string CustomPIPayment { get; set; }
        public string Transport { get; set; }
        public string PIRemark { get; set; }
        public int TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public double PIAmount { get; set; }
        public string AmtWords { get; set; }
        //public string SessionYear { get; set; }
        //public int UserID { get; set; }

        //pi child 
        public long PIChildId { get; set; }
        public long FitemID { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int SNo { get; set; }

        //product
        public string Name { get; set; }
        public string Code { get; set; }
         
        //buyer
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerPhoneNo { get; set; }
        public string BuyerMobileNo { get; set; }
        public string BuyerEmail { get; set; }
        public int CurrencyID { get; set; } 

        //consignee
        public string ConsigneeCode { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeEmail { get; set; }
        public string ConsigneeMobile { get; set; }
        public string ConsigneePhone { get; set; }

        //fobcif
        public string FOBCIF { get; set; }

        //order
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        //currency
        public string CName { get; set; }
        public string CSName { get; set; }

        //company
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string GSTN { get; set; }
        public string Fax { get; set; }
        public string IECode { get; set; }

        //sizedetails
        public int SizeID { get; set; }
        public string SizeName { get; set; }

        //color details
        public int CLID { get; set; }
        public string CLName { get; set; }


    }
}
