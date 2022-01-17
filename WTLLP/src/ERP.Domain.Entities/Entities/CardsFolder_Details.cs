using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class CardsFolder_Details 
    {
        //CardId, Representetive_Name, Firm_Name, Address, Website, Email, Description, Designetion, Phone1, Phone2, Phone3, Mobile1, Mobile2, Mobile3
        public int CardId { get; set; }
        public string Representetive_Name { get; set; }
        public string Firm_Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Designetion { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
    }
}
