using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Supplier.Models
{
    public class SupplierModel
    {
        public long SupplierId { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        public string Pincode { get; set; }

        [Display(Name = "Phone No")]
        public string SupplierPhoneNo { get; set; }
        [Display(Name = "Mobile No")]
        public string SupplierMobileNo { get; set; }
        [Display(Name = "FAX No")]
        public string SupplierFaxNo { get; set; }
        [Display(Name = "Email")]
        public string SupplierEmail { get; set; }
        [Display(Name = "Website")]
        public string SupplierWeb { get; set; }
        [Display(Name = "PAN No")]
        public string SupplierPANNo { get; set; }
        [Display(Name = "CST No")]
        public string SupplierCSTNo { get; set; }
        [Display(Name = "UPTT No")]
        public string SupplierUPTTNo { get; set; }
        [Display(Name = "TIN No")]
        public string SupplierTINNo { get; set; }
        [Display(Name = "Local TAX")]
        public string SupplierLocalTAX { get; set; }
        [Display(Name = "Terms")]
        public string SupplierTerms { get; set; }
        [Display(Name = "Description")]
        public string SupplierDesc { get; set; }
        public long? PType { get; set; }
        public int? BlackListed { get; set; }

        [Display(Name = "Currency")]
        public int CID { get; set; }   //Currency ID
        public string CName { get; set; }   //Currency Name 
        public string CSName { get; set; }  //Currency Short Name

        public SelectList CountryList { set; get; }
        public SelectList StateList { set; get; }
        public SelectList CityList { set; get; }
        public SelectList CurrencyList { set; get; }

        [Display(Name = "GST No.")]
        public string GSTN { get; set; }

        public string Beneficiary { get; set; }
        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        [Display(Name = "Bank Address")]
        public string BankAddress { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "Swift Code")]
        public string BankSwiftCode { get; set; }
        [Display(Name = "IFSC Code")]
        public string BankIFSCCode { get; set; }
        public SelectList BankList { set; get; }
        [Display(Name = "Verify Account No")]
        public string VerifyAccountNo { get; set; }
        public List<SupplierBankModel> SupplierBankModel { get; set; }
    }
}