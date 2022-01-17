using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RitemMaster
    {
        public long RitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int BelongTo { get; set; }
        public int MasterBelongTo { get; set; }
        public int ItOrCat { get; set; }
        public string Finish { get; set; }
        public string Thickness { get; set; }
        public string Colour { get; set; }
        public string Shape { get; set; }
        public string NickleFree { get; set; }
        public string SizeName { get; set; }
        public string WireSize { get; set; }
        public string Design { get; set; }
        public string Stone { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Diameter { get; set; }
        public string PickThickness { get; set; }
        public int Punit { get; set; }
        public double PucrhasePrice { get; set; }
        public int CostUnit { get; set; }
        public double CostPrice { get; set; }
        public double ConversionFactor { get; set; }
        public double OpStock { get; set; }
        public double MinStock { get; set; }
        public double MaxStock { get; set; }
        public double Was { get; set; }
        public double BomWasPercent { get; set; }
        public string PanningWay { get; set; }
        public string Stucture { get; set; }
        public string Washable { get; set; }
        public string AverageArea { get; set; }
        public string Selection { get; set; }
        public string StrenthTest { get; set; }
        public string Base { get; set; }
        public string DyedSpe { get; set; }
        public string EuNorms { get; set; }
        public int MinDeliveryDays { get; set; }
        public string Dnsity { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string ColorFast { get; set; }
        public string Description { get; set; }
        public string Cloth { get; set; }
        public string Metal { get; set; }
        public string Quality { get; set; }
        public string Adhesive { get; set; }
        public string Cartoon { get; set; }
        public string FullName { get; set; }
        public string PurchaseType { get; set; }
        public string ActualPunit { get; set; }
        public double ActualPurPrice { get; set; }
        public string ActualCostUnit { get; set; }
        public double ActualCostPrice { get; set; }
        public double ActualConversionFactor { get; set; }
        public long RmCode { get; set; }
        public string SuppCode { get; set; }
        public long MonthYear { get; set; }
        public string Title { get; set; }
        public double MaxWastage { get; set; }
        public string Width { get; set; }
        public int GSM { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RMBrandId { get; set; }
        public int HSNId { get; set; }
        public int IsCertificateRequiredForPurchase { get; set; }

        public DateTime RMUpdateOn { get; set; }
        public int RMUpdateUser { get; set; }

        public int RMReviewStatus { get; set; }
        public DateTime RMReviewOn { get; set; }
        public int RMReviewUser { get; set; }

        public virtual ICollection<RItemSupp> RItemSupp { get; set; }
        public virtual ICollection<RMChild> RMChild { get; set; }
        public virtual ICollection<RItemQuickTest> RItemQuickTest { get; set; }

        public int SizeID { set; get; }
        public int CLID { set; get; }

        //public double Weight { get; set; }
    }
}
