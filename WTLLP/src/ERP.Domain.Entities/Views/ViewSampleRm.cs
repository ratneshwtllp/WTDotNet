using System;

namespace ERP.Domain.Entities
{ 
    public class ViewSampleRM
    {
        //samplerm
        public long SampleRMId { get; set; }
        public long FitemId { get; set; }
        public long RitemId { get; set; }

        //ritem 
        public string Name { get; set; } 
        public string Code { get; set; } 
        public string Title { get; set; } 
        public string Full_Name { get; set; }

        //rmsubcat
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

        //rmcat 
        public long CategoryID { get; set; }        //rmcate
        public string CategoryName { get; set; }

        //product
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
         
    }

}
