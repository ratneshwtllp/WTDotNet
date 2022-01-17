namespace ERP.Domain.Entities
{
    public partial class ViewIssueRMForChangeMaster
    {
        public long IssueRMChangeID { get; set; } 
        public System.DateTime IssueRMChangeDate { get; set; }
        public long IssueRMChangeSNo { get; set; } 
        public string  IssueRMChangeNo { get; set; } 
        public string Name { get; set; } 
        public string Code { get; set; } 
        public string  USName { get; set; }
        public string SubCategoryName { get; set; }
        public long RItem_Id { get; set; }
        public string Remark { get; set; }
        public double JWQty { get; set; }
        public double Balance { get; set; }
        public int IsComplete { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long IssueRMChangeItemID { get; set; }
        public int SNo { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string GSTN { get; set; }
        public string AddressWork { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierGSTIN { get; set; }     
        public int SuppAddressId { get; set; }
        public int InHouse_Outdoor { get; set; }
        public string SupplierSecondAddress { get; set; }

        public double TotalAmount { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmountAfterTax { get; set; }
        public string AmountInWords { get; set; }
        public string VehicleNo { get; set; }
        public string Transport { get; set; }
        public int TransportId { get; set; }
        public string PlaceofSupply { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
        
    }
}
