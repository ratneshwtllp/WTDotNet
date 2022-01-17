using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewBuyerDetails
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string ContactPerson { get; set; }
        public string BuyerAddress { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; }
        public string BuyerPhoneNo { get; set; }
        public string BuyerMobileNo { get; set; }
        public string BuyerFaxNo { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerWeb { get; set; }
        public string BuyerPANNo { get; set; }
        public string BuyerCSTNo { get; set; }
        public string BuyerUPTTNo { get; set; }
        public string BuyerTINNo { get; set; }
        public string BuyerLocalTAX { get; set; }
        public string BuyerTerms { get; set; }
        public string BuyerDesc { get; set; }
        public long? PType { get; set; }
        public int? BlackListed { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string CSName { get; set; }
        public string CName { get; set; }
        public int CID { get; set; }

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string BuyerUserID { get; set; }
        public string BuyerPW { get; set; }
        public int FOB_CIF { get; set; }
        public string LogoPath { get; set; }
        public int IsActive { get; set; }
        public int IsAllowLogin { get; set; }
        public int IsGeneralBuyer { get; set; }
        public int Online_Transfer { get; set; }
    }
}
