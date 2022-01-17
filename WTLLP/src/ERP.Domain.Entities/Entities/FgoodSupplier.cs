using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FgoodSupplier
    {
        public long Id { get; set; }
        public int? ReadymadeJobworker { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CPerson { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Phoneno { get; set; }
        public string Faxno { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Csttno { get; set; }
        public string Upttno { get; set; }
        public string Ltno { get; set; }
        public string Ltname { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public string Mobileno { get; set; }
    }
}
