using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRMStatus
    {
        //@RitemID ,@Code ,@Full_Name,@BelongTo,@MasterBelongTo,
        //@Title, @CurrentStock,@POPLQty,@JWPL,@RMTotal, @RMBalWP,@RMBalOrder

        public long RitemID { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public long BelongTo { get; set; }
        public long MasterBelongTo { get; set; }
        public string Title { get; set; }
        public string USName { get; set; }
        public double CurrentStock { get; set; }
        public double POPLQty { get; set; }
        public double JWPL { get; set; }
        public double RMTotal { get; set; }
        public double RMBalWP { get; set; }
        public double RMBalOrder { get; set; }
        public double Required { get; set; }        
    }
}
