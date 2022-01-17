using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Buyer.Models
{
    public class BuyerModel
    {
        public long BuyerId { get; set; }
        [Display(Name = "Buyer Name")]
        public string BuyerName { get; set; }
        [Display(Name = "Buyer Code")]
        public string BuyerCode { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Address")]
        public string BuyerAddress { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }
        [Display(Name = "Phone No")]
        public string BuyerPhoneNo { get; set; }
        [Display(Name = "Mobile No")]
        public string BuyerMobileNo { get; set; }
        [Display(Name = "FAX No")]
        public string BuyerFaxNo { get; set; }
        [Display(Name = "Email")]
        public string BuyerEmail { get; set; }
        [Display(Name = "Website")]
        public string BuyerWeb { get; set; }
        [Display(Name = "PAN No")]
        public string BuyerPANNo { get; set; }
        [Display(Name = "CST No")]
        public string BuyerCSTNo { get; set; }
        [Display(Name = "UPTT No")]
        public string BuyerUPTTNo { get; set; }
        [Display(Name = "TIN No")]
        public string BuyerTINNo { get; set; }
        [Display(Name = "Local TAX")]
        public string BuyerLocalTAX { get; set; }
        [Display(Name = "Terms")]
        public string BuyerTerms { get; set; }
        [Display(Name = "Description")]
        public string BuyerDesc { get; set; }
        public long? PType { get; set; }
        public int? BlackListed { get; set; }
        public SelectList CountryList { set; get; }
        public SelectList StateList { set; get; }
        public SelectList CityList { set; get; }
        [Display(Name = "Currency")]
        public int CID { get; set; }
        public SelectList CurrencyList { set; get; }
        public string CSName { get; set; }
        public string CName { get; set; }
        [Display(Name = "Buyer User")]
        public string BuyerUserID { get; set; }
        [Display(Name = "Buyer Password")]
        public string BuyerPW { get; set; }
        [Display(Name = "FOB/CIF")]
        public int FOB_CIF { get; set; }
        [Display(Name = "Logo Path")]
        public string LogoPath { get; set; }
        public int IsActive { get; set; }
        public int IsAllowLogin { get; set; }
        public int IsGeneralBuyer { get; set; }
        public int Online_Transfer { get; set; }
        public SelectList FOBList { set; get; }
        public SelectList IsActiveList { set; get; }
        public SelectList IsAllowLoginList { set; get; }
        public SelectList IsGeneralBuyerList { set; get; }
        public string Beneficiary { get; set; }
        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        [Display(Name = "Bank Address")]
        public string BankAddress { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "SwiftCode")]
        public string BankSwiftCode { get; set; }
        [Display(Name = "IFSC Code")]
        public string BankIFSCCode { get; set; }
        public SelectList BankList { set; get; }
        [Display(Name = "Verify Account No")]
        public string VerifyAccountNo { get; set; }
        public List<BuyerConsigneeModel> BuyerConsigneeModel { get; set; }
        public List<BuyerBankModel> BuyerBankModel { get; set; }
    }
}
