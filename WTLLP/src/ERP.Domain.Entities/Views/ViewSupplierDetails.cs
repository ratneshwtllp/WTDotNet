using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSupplierDetails
    {   
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string ContactPerson { get; set; }
        public string SupplierAddress { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; }
        public string SupplierPhoneNo { get; set; }
        public string SupplierMobileNo { get; set; }
        public string SupplierFaxNo { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierWeb { get; set; }
        public string SupplierPANNo { get; set; }
        public string SupplierCSTNo { get; set; }
        public string SupplierUPTTNo { get; set; }
        public string SupplierTINNo { get; set; }
        public string SupplierLocalTAX { get; set; }
        public string SupplierTerms { get; set; }
        public string SupplierDesc { get; set; }
        public long? PType { get; set; }
        public int? BlackListed { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int CID { get; set; }
        public string CName { get; set; }
        public string CSName { get; set; }
        public string GSTN { get; set; }

    }
}
