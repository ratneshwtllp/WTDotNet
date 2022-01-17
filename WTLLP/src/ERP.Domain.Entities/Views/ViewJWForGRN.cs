using System;

namespace ERP.Domain.Entities
{
    public class ViewJWForGRN
    {
        public long IssueRMChangeID { get; set; }
        public long IssueRMChangeItemID { get; set; }
        public string IssueRMChangeNo { get; set; }
       

        public double JWQty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Full_Name { set; get; }
        public long RItem_Id { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string USName { get; set; }
        public double ReceivedGRNQty { set; get; }
        public double Balance { set; get; }

    }
}
