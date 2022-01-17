using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ERP.Domain.Entities;

namespace ERP.DataAccess
{
    public partial class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=IK;Trusted_Connection=True;");
        }

        public DBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entities
            modelBuilder.Entity<POMaster>(entity =>
               {
                   entity.HasKey(e => e.Poid).HasName("PK_POMaster");
                   entity.Property(e => e.Podate).HasColumnType("datetime");
                   entity.Property(e => e.EmailDate).HasColumnType("datetime");
                   entity.Property(e => e.NewDate).HasColumnType("datetime");
                   entity.Property(e => e.Dldate).HasColumnType("datetime");
               });

            modelBuilder.Entity<POChild>(entity =>
            {
                entity.HasKey(e => e.POChildID)
                    .HasName("PK_POChild");

                entity.Property(e => e.POChildID).ValueGeneratedNever();

                entity.HasOne(d => d.POMaster)
                    .WithMany(p => p.POChild)
                    .HasForeignKey(d => d.POID);
            });

            modelBuilder.Entity<POForSaleOrderDetails>(entity =>
            {
                entity.HasKey(e => e.POForSaleOrderId);
                entity.HasOne(d => d.POMaster)
                    .WithMany(p => p.POForSaleOrderDetails)
                    .HasForeignKey(d => d.Poid);
            });

            modelBuilder.Entity<RItemSupp>(entity =>
            {
                entity.HasKey(e => e.RItemSuppID)
                    .HasName("PK_RItemSupp");

                entity.Property(e => e.RItemSuppID).ValueGeneratedNever();

                entity.HasOne(d => d.RitemMaster)
                    .WithMany(p => p.RItemSupp)
                    .HasForeignKey(d => d.RItem_Id);
            });

            modelBuilder.Entity<TaxDetails>(entity =>
            {
                entity.HasKey(e => e.TaxId)
                    .HasName("PK_TaxDetails");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.TaxId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<ActualRateRmhistory>(entity =>
            {
                entity.HasKey(e => e.RmHistoryId)
                    .HasName("PK_ActualRate_RMHistory");

                entity.ToTable("ActualRate_RMHistory");

                entity.Property(e => e.RmHistoryId)
                    .HasColumnName("RM_History_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostingPrice)
                    .HasColumnName("Costing_Price")
                    .HasColumnType("money");

                entity.Property(e => e.CostingUnit)
                    .IsRequired()
                    .HasColumnName("Costing_Unit")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnName("Purchase_Price")
                    .HasColumnType("money");

                entity.Property(e => e.PurchaseUnit)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RmId).HasColumnName("RM_ID");
            });


            modelBuilder.Entity<CartonDetails>(entity =>
            {
                entity.HasKey(e => e.CartonId)
                    .HasName("PK_Carton_Details");

                entity.ToTable("Carton_Details");

                entity.Property(e => e.CartonId)
                    .HasColumnName("CartonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.Dimension)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OuterInner).HasColumnName("Outer_Inner");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ChallanEsiDetails>(entity =>
            {
                entity.HasKey(e => e.ChEsiId)
                    .HasName("PK_ChallanEsi_Details");

                entity.ToTable("ChallanEsi_Details");

                entity.Property(e => e.ChEsiId).ValueGeneratedNever();

                entity.Property(e => e.AmtInWords).HasColumnName("Amt_in_words");

                entity.Property(e => e.ChEsiBankDrawn)
                    .HasColumnName("ChEsi_Bank_drawn")
                    .HasMaxLength(50);

                entity.Property(e => e.ChEsiBankname)
                    .HasColumnName("ChEsi_Bankname")
                    .HasMaxLength(50);

                entity.Property(e => e.ChEsiBranch)
                    .HasColumnName("ChEsi_Branch")
                    .HasMaxLength(50);

                entity.Property(e => e.ChEsiDamages)
                    .HasColumnName("ChEsi_Damages")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiDateday)
                    .HasColumnName("ChEsi_Dateday")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChEsiDatedday)
                    .HasColumnName("ChEsi_Datedday")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChEsiDdno)
                    .HasColumnName("ChEsi_Ddno")
                    .HasMaxLength(20);

                entity.Property(e => e.ChEsiEmpContri)
                    .HasColumnName("ChEsi_EmpContri")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiEmployerContri)
                    .HasColumnName("ChEsi_EmployerContri")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiInterest)
                    .HasColumnName("ChEsi_Interest")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiMonthid).HasColumnName("ChEsi_Monthid");

                entity.Property(e => e.ChEsiNoofEmp).HasColumnName("ChEsi_NoofEmp");

                entity.Property(e => e.ChEsiOthers)
                    .HasColumnName("ChEsi_Others")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiPaydetail)
                    .HasColumnName("ChEsi_Paydetail")
                    .HasMaxLength(50);

                entity.Property(e => e.ChEsiPaymode)
                    .HasColumnName("ChEsi_Paymode")
                    .HasMaxLength(20);

                entity.Property(e => e.ChEsiTotal)
                    .HasColumnName("ChEsi_Total")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiTotwages)
                    .HasColumnName("ChEsi_Totwages")
                    .HasColumnType("money");

                entity.Property(e => e.ChEsiYear).HasColumnName("ChEsi_Year");

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.DepositBank).HasMaxLength(50);

                entity.Property(e => e.DepositBranch).HasMaxLength(50);

                entity.Property(e => e.DepositDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ChallanPfDetail>(entity =>
            {
                entity.HasKey(e => e.ChPfid)
                    .HasName("PK_Challan_PF_Detail");

                entity.ToTable("Challan_PF_Detail");

                entity.Property(e => e.ChPfid)
                    .HasColumnName("ChPFId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AmtInWords).HasColumnName("Amt_in_words");

                entity.Property(e => e.ChPfAdm2)
                    .HasColumnName("ChPF_Adm2")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfAdm22)
                    .HasColumnName("ChPF_Adm22")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfBankcode)
                    .HasColumnName("ChPF_bankcode")
                    .HasMaxLength(50);

                entity.Property(e => e.ChPfEmployec1)
                    .HasColumnName("ChPF_Employec1")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfEmprcon1)
                    .HasColumnName("ChPF_Emprcon1")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfEmprcon10)
                    .HasColumnName("ChPF_Emprcon10")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfEmprcon21)
                    .HasColumnName("ChPF_Emprcon21")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfInsp2)
                    .HasColumnName("ChPF_Insp2")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfInsp22)
                    .HasColumnName("ChPF_Insp22")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfMisc2)
                    .HasColumnName("ChPF_Misc2")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfMisc22)
                    .HasColumnName("ChPF_Misc22")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfMonthid).HasColumnName("ChPF_Monthid");

                entity.Property(e => e.ChPfPaydate)
                    .HasColumnName("ChPF_Paydate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChPfPenal1)
                    .HasColumnName("ChPF_Penal1")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfPenal10)
                    .HasColumnName("ChPF_Penal10")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfPenal2)
                    .HasColumnName("ChPF_Penal2")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfPenal21)
                    .HasColumnName("ChPF_Penal21")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfPenal22)
                    .HasColumnName("ChPF_Penal22")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfTsac1).HasColumnName("ChPF_TSac1");

                entity.Property(e => e.ChPfTsac10).HasColumnName("ChPF_TSac10");

                entity.Property(e => e.ChPfTsac21).HasColumnName("ChPF_TSac21");

                entity.Property(e => e.ChPfTwac1)
                    .HasColumnName("ChPF_TWac1")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfTwac10)
                    .HasColumnName("ChPF_TWac10")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfTwac21)
                    .HasColumnName("ChPF_TWac21")
                    .HasColumnType("money");

                entity.Property(e => e.ChPfYearPf)
                    .HasColumnName("ChPF_YearPf")
                    .HasMaxLength(10);

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ClickProcessDetails>(entity =>
            {
                entity.HasKey(e => e.ClickId)
                    .HasName("PK_ClickProcess_Details");

                entity.ToTable("ClickProcess_Details");

                entity.Property(e => e.ClickId)
                    .HasColumnName("Click_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClickDescrpt)
                    .HasColumnName("Click_Descrpt")
                    .HasMaxLength(150);

                entity.Property(e => e.ClickEnd)
                    .HasColumnName("Click_End")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClickRate)
                    .HasColumnName("Click_Rate")
                    .HasColumnType("money");

                entity.Property(e => e.ClickStart)
                    .HasColumnName("Click_Start")
                    .HasColumnType("datetime");

                entity.Property(e => e.JobCardId).HasColumnName("JobCard_Id");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SoId).HasColumnName("SO_Id");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ClosingMaster>(entity =>
            {
                entity.HasKey(e => e.ClosingTid)
                    .HasName("PK_Closing_Master");

                entity.ToTable("Closing_Master");

                entity.Property(e => e.ClosingTid)
                    .HasColumnName("Closing_TID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClosingDate)
                    .HasColumnName("Closing_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ClosingId).HasColumnName("Closing_ID");

                entity.Property(e => e.ClosingNo)
                    .IsRequired()
                    .HasColumnName("Closing_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.Comments).HasMaxLength(150);

                entity.Property(e => e.PlanId).HasColumnName("Plan_id");

                entity.Property(e => e.QcOnProcess).HasColumnName("Qc_On_Process");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<ClothDetails>(entity =>
            {
                entity.HasKey(e => e.Chid).HasName("PK_Cloth_Details");
                entity.ToTable("Cloth_Details");
                entity.Property(e => e.Chid)
                    .HasColumnName("CHID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Chname)
                    .IsRequired()
                    .HasColumnName("CHName")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ColorDetails>(entity =>
            {
                entity.HasKey(e => e.Clid)
                    .HasName("PK_Color_Details");

                entity.ToTable("Color_Details");

                entity.Property(e => e.Clid)
                    .HasColumnName("CLID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Clname)
                    .IsRequired()
                    .HasColumnName("CLName")
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CompanyDetails>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CName);
                entity.Property(e => e.AddressOffice);
                entity.Property(e => e.AddressWork);
                entity.Property(e => e.Phone);
                entity.Property(e => e.Fax);
                entity.Property(e => e.Email);
                entity.Property(e => e.Web);
                entity.Property(e => e.CSTT);
                entity.Property(e => e.UPTT);
                entity.Property(e => e.TIN);
                entity.Property(e => e.IECode);
                entity.Property(e => e.EmpEsiCode1);
                entity.Property(e => e.EmpEsiCode2);
                entity.Property(e => e.EmpEsiCode3);
                entity.Property(e => e.EmpPfEsttCode);
                entity.Property(e => e.Excise).HasColumnType("money");
                entity.Property(e => e.CustomTeriffNo);
                entity.Property(e => e.PANNo);
                entity.Property(e => e.POFooter1);
                entity.Property(e => e.POFooter2);
            });

            modelBuilder.Entity<Consignee>(entity =>
            {
                entity.Property(e => e.ConsigneeId)
                    .HasColumnName("Consignee_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Caddress)
                    .IsRequired()
                    .HasColumnName("caddress");

                entity.Property(e => e.Ccity)
                    .HasColumnName("ccity")
                    .HasMaxLength(30);

                entity.Property(e => e.Ccode)
                    .IsRequired()
                    .HasColumnName("ccode")
                    .HasMaxLength(50);

                entity.Property(e => e.Ccountry)
                    .HasColumnName("ccountry")
                    .HasMaxLength(30);

                entity.Property(e => e.Cmobile)
                    .HasColumnName("cmobile")
                    .HasMaxLength(15);

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasColumnName("cname")
                    .HasMaxLength(50);

                entity.Property(e => e.Cphone)
                    .HasColumnName("cphone")
                    .HasMaxLength(15);

                entity.Property(e => e.Cstate)
                    .HasColumnName("cstate")
                    .HasMaxLength(30);

                entity.Property(e => e.Czippin)
                    .HasColumnName("czippin")
                    .HasMaxLength(6);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });



            modelBuilder.Entity<CreateJobCardMaster>(entity =>
            {
                entity.HasKey(e => e.JobCardId)
                    .HasName("PK_Create_JobCard");

                entity.ToTable("Create_JobCard_Master");

                entity.Property(e => e.JobCardId)
                    .HasColumnName("JobCard_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CheckedBy).HasMaxLength(50);

                entity.Property(e => e.ContrId).HasColumnName("Contr_ID");

                entity.Property(e => e.DeliDate)
                    .HasColumnName("Deli_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.JobCardNo).HasColumnName("JobCard_No");

                entity.Property(e => e.JobPrice)
                    .HasColumnName("Job_Price")
                    .HasColumnType("money");

                entity.Property(e => e.ReqDate)
                    .HasColumnName("Req_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SoId).HasColumnName("SO_Id");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CuttingMaster>(entity =>
            {
                entity.HasKey(e => e.CuttingTid)
                    .HasName("PK_Cutting_Master");

                entity.ToTable("Cutting_Master");

                entity.Property(e => e.CuttingTid)
                    .HasColumnName("Cutting_TID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments).HasMaxLength(150);

                entity.Property(e => e.CuttingDate)
                    .HasColumnName("Cutting_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CuttingId).HasColumnName("Cutting_ID");

                entity.Property(e => e.CuttingNo)
                    .IsRequired()
                    .HasColumnName("Cutting_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcOnProcess).HasColumnName("Qc_On_Process");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });


            modelBuilder.Entity<ExtraQtyPermission>(entity =>
            {
                entity.HasKey(e => e.MessegeId)
                    .HasName("PK_Extra_Qty_Permission");

                entity.ToTable("Extra_Qty_Permission");

                entity.Property(e => e.MessegeId)
                    .HasColumnName("Messege_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdminResponse).HasColumnName("Admin_Response");

                entity.Property(e => e.BalanceQty).HasColumnName("Balance_Qty");

                entity.Property(e => e.GipRead).HasColumnName("GIP_Read");

                entity.Property(e => e.Gipno)
                    .HasColumnName("GIPNO")
                    .HasMaxLength(50);

                entity.Property(e => e.ItemId).HasColumnName("Item_Id");

                entity.Property(e => e.ItemName)
                    .HasColumnName("Item_name")
                    .HasMaxLength(150);

                entity.Property(e => e.MessegeDate)
                    .HasColumnName("Messege_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pono)
                    .HasColumnName("PONO")
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiveQty).HasColumnName("Receive_Qty");
            });

            modelBuilder.Entity<FgoodSupplier>(entity =>
            {
                entity.ToTable("FGood_Supplier");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CPerson).HasColumnName("C_Person");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Csttno).HasColumnName("CSTTNo");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Faxno).HasMaxLength(20);

                entity.Property(e => e.Ltname).HasColumnName("LTName");

                entity.Property(e => e.Ltno).HasColumnName("LTNo");

                entity.Property(e => e.Mobileno).HasMaxLength(20);

                entity.Property(e => e.Phoneno).HasMaxLength(20);

                entity.Property(e => e.Pincode).HasMaxLength(20);

                entity.Property(e => e.ReadymadeJobworker).HasColumnName("Readymade_Jobworker");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.Upttno).HasColumnName("UPTTNo");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zipcode).HasMaxLength(20);
            });

            modelBuilder.Entity<FinishDetails>(entity =>
            {
                entity.HasKey(e => e.Fid)
                    .HasName("PK_Finish_1");

                entity.ToTable("Finish");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fdesc)
                    .HasColumnName("FDesc")
                    .HasMaxLength(500);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FinishMetalDetails>(entity =>
            {
                entity.HasKey(e => e.Mfid)
                    .HasName("PK_FinishMetal_Details");

                entity.ToTable("FinishMetal_Details");

                entity.Property(e => e.Mfid)
                    .HasColumnName("MFID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Mfname)
                    .HasColumnName("MFName")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FinishReceive>(entity =>
            {
                entity.HasKey(e => e.Frid)
                    .HasName("PK_FinishReceive");

                entity.Property(e => e.Frid)
                    .HasColumnName("FRID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Frdate)
                    .HasColumnName("FRDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).IsRequired();

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FitemQcentry>(entity =>
            {
                entity.ToTable("FItem_QCEntry");

                entity.Property(e => e.FitemQcentryId)
                    .HasColumnName("FItem_QCEntry_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcBy)
                    .IsRequired()
                    .HasColumnName("QC_By")
                    .HasMaxLength(50);

                entity.Property(e => e.QcDate)
                    .HasColumnName("QC_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.QcPass).HasColumnName("QC_Pass");

                entity.Property(e => e.QcQty).HasColumnName("QC_Qty");

                entity.Property(e => e.QcReject).HasColumnName("QC_Reject");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.SubWpId).HasColumnName("SubWP_ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Fitting>(entity =>
            {
                entity.HasKey(e => e.Ftid)
                    .HasName("PK_Fitting");

                entity.Property(e => e.Ftid)
                    .HasColumnName("FTID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FittingDesc).HasMaxLength(500);

                entity.Property(e => e.FittingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_Year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Gipmaster>(entity =>
            {
                entity.HasKey(e => e.Gipno)
                    .HasName("PK_GIPMaster");

                entity.ToTable("GIPMaster");

                entity.Property(e => e.Gipno)
                    .HasColumnName("GIPNO")
                    .HasMaxLength(20);

                entity.Property(e => e.Billno).HasMaxLength(50);

                entity.Property(e => e.Challanno).HasMaxLength(50);

                entity.Property(e => e.Drivername).HasMaxLength(50);

                entity.Property(e => e.Gipdate)
                    .HasColumnName("GIPDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gipid).HasColumnName("GIPID");

                entity.Property(e => e.PoManual)
                    .IsRequired()
                    .HasColumnName("PO_Manual")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.StoreMis)
                    .IsRequired()
                    .HasColumnName("Store_Mis")
                    .HasMaxLength(50);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Vehcilno).HasMaxLength(50);
            });

            modelBuilder.Entity<GoodsReceivedMaster>(entity =>
            {
                entity.HasKey(e => e.GreceiveId)
                    .HasName("PK_GoodsReceived_Details");

                entity.ToTable("GoodsReceived_Master");

                entity.Property(e => e.GreceiveId)
                    .HasColumnName("GReceive_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.GreceiveDate)
                    .HasColumnName("GReceive_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GreceivedBy)
                    .HasColumnName("GReceived_By")
                    .HasMaxLength(50);

                entity.Property(e => e.JobCardId).HasColumnName("JobCard_Id");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SoId).HasColumnName("SO_Id");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GroupName>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_Group_Name");

                entity.ToTable("Group_Name");

                entity.Property(e => e.GroupId)
                    .HasColumnName("Group_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName1)
                    .IsRequired()
                    .HasColumnName("Group_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.NatureId).HasColumnName("Nature_ID");
            });

            modelBuilder.Entity<GspMaster>(entity =>
            {
                entity.HasKey(e => e.GspId)
                    .HasName("PK_GSP_Master");

                entity.ToTable("GSP_Master");

                entity.Property(e => e.GspId)
                    .HasColumnName("GSP_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.GoodsConsignedFrom)
                    .IsRequired()
                    .HasColumnName("Goods_Consigned_From");

                entity.Property(e => e.GoodsConsignedTo)
                    .IsRequired()
                    .HasColumnName("Goods_Consigned_To");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasColumnName("Invoice_No")
                    .HasMaxLength(50);

                entity.Property(e => e.MeansOfTransport)
                    .IsRequired()
                    .HasColumnName("MeansOf_Transport");

                entity.Property(e => e.OriginCriterion)
                    .IsRequired()
                    .HasColumnName("Origin_Criterion")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Idtable>(entity =>
            {
                entity.HasKey(e => e.Idname)
                    .HasName("PK_IDTable");

                entity.ToTable("IDTable");

                entity.Property(e => e.Idname)
                    .HasColumnName("IDName")
                    .HasMaxLength(5);

                entity.Property(e => e.Idcode)
                    .IsRequired()
                    .HasColumnName("IDCode")
                    .HasMaxLength(5);

                entity.Property(e => e.Idcount).HasColumnName("IDCount");
            });

            modelBuilder.Entity<InpassMaster>(entity =>
            {
                entity.HasKey(e => e.InpassTableId)
                    .HasName("PK_INPass_Master");

                entity.ToTable("INPass_Master");

                entity.Property(e => e.InpassTableId)
                    .HasColumnName("INPassTable_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InpassBy)
                    .IsRequired()
                    .HasColumnName("INPass_By")
                    .HasMaxLength(50);

                entity.Property(e => e.InpassDate)
                    .HasColumnName("INPass_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.InpassId).HasColumnName("INPass_ID");

                entity.Property(e => e.InpassNo)
                    .IsRequired()
                    .HasColumnName("INPass_No")
                    .HasMaxLength(50);

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<InsMaster>(entity =>
            {
                entity.HasKey(e => e.InsId)
                    .HasName("PK_Ins_Master");

                entity.ToTable("Ins_Master");

                entity.Property(e => e.InsId)
                    .HasColumnName("Ins_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Assorment).IsRequired();

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("Ins_By")
                    .HasMaxLength(50);

                entity.Property(e => e.InsDate)
                    .HasColumnName("Ins_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            });

            modelBuilder.Entity<InsertRecord>(entity =>
            {
                entity.HasKey(e => e.NumberId)
                    .HasName("PK_Insert_Record");

                entity.ToTable("Insert_Record");

                entity.Property(e => e.NumberId).HasColumnName("NumberID");

                entity.Property(e => e.DateItime)
                    .HasColumnName("DateITime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TextRemm).IsRequired();
            });

            modelBuilder.Entity<IssueRmOtherThanBomMaster>(entity =>
            {
                entity.HasKey(e => e.OtherId)
                    .HasName("PK_IssueRM_OtherThanBOM_Master");

                entity.ToTable("IssueRM_OtherThanBOM_Master");

                entity.Property(e => e.OtherId)
                    .HasColumnName("Other_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OtherDate)
                    .HasColumnName("Other_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OtherNo)
                    .HasColumnName("Other_No")
                    .HasMaxLength(50);

                entity.Property(e => e.OtherSno).HasColumnName("Other_SNo");

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.ReceiveBy)
                    .HasColumnName("Receive_By")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.HasKey(e => e.SNo)
                    .HasName("PK_Journal");

                entity.Property(e => e.SNo).HasColumnName("S_No");

                entity.Property(e => e.Bankdate).HasColumnType("datetime");

                entity.Property(e => e.Billno).HasMaxLength(50);

                entity.Property(e => e.Chqno)
                    .HasColumnName("CHQno")
                    .HasMaxLength(50);

                entity.Property(e => e.CrAmount).HasColumnType("money");

                entity.Property(e => e.DrAmount).HasColumnType("money");

                entity.Property(e => e.DrCr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FinYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InvNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InvType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lfno).HasColumnName("LFNo");

                entity.Property(e => e.Narration)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vdate)
                    .HasColumnName("VDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Vid).HasColumnName("VId");

                entity.Property(e => e.Vtype)
                    .IsRequired()
                    .HasColumnName("VType")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LabourCharge>(entity =>
            {
                entity.HasKey(e => e.LcId)
                    .HasName("PK_LabourCharge");

                entity.Property(e => e.LcId)
                    .HasColumnName("LC_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnName("Item_ID");

                entity.Property(e => e.LabourRate).HasColumnName("Labour_rate");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_Year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<LabourTypeDetails>(entity =>
            {
                entity.ToTable("LabourType_Details");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LabourType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<LeaveEncashment>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK_Leave_Encashment");

                entity.ToTable("Leave_Encashment");

                entity.Property(e => e.LeaveId)
                    .HasColumnName("LeaveID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LAmount)
                    .HasColumnName("L_Amount")
                    .HasColumnType("money");

                entity.Property(e => e.LDepttId).HasColumnName("L_DepttId");

                entity.Property(e => e.LDesigId).HasColumnName("L_desigId");

                entity.Property(e => e.LEmpDeduction)
                    .HasColumnName("L_EmpDeduction")
                    .HasColumnType("money");

                entity.Property(e => e.LEmpEsi)
                    .HasColumnName("L_EmpEsi")
                    .HasColumnType("money");

                entity.Property(e => e.LEmpEsiPer).HasColumnName("L_EmpEsi_Per");

                entity.Property(e => e.LEmpId).HasColumnName("L_EmpID");

                entity.Property(e => e.LEmpPf)
                    .HasColumnName("L_EmpPf")
                    .HasColumnType("money");

                entity.Property(e => e.LEmpPfPer).HasColumnName("L_EmpPf_Per");

                entity.Property(e => e.LEmployerContri)
                    .HasColumnName("L_EmployerContri")
                    .HasColumnType("money");

                entity.Property(e => e.LEmployerContriPer).HasColumnName("L_EmployerContri_Per");

                entity.Property(e => e.LEmployerEsi)
                    .HasColumnName("L_EmployerEsi")
                    .HasColumnType("money");

                entity.Property(e => e.LEmployerEsiPer).HasColumnName("L_EmployerEsi_Per");

                entity.Property(e => e.LEmployerPf)
                    .HasColumnName("L_EmployerPf")
                    .HasColumnType("money");

                entity.Property(e => e.LEmployerPfPer).HasColumnName("L_EmployerPf_Per");

                entity.Property(e => e.LForYear)
                    .HasColumnName("L_ForYear")
                    .HasMaxLength(5);

                entity.Property(e => e.LMonthid).HasColumnName("L_monthid");

                entity.Property(e => e.LPayableamt)
                    .HasColumnName("L_Payableamt")
                    .HasColumnType("money");

                entity.Property(e => e.LSalId).HasColumnName("L_salId");

                entity.Property(e => e.LYear)
                    .HasColumnName("L_Year")
                    .HasMaxLength(5);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<MakingChargesVoucher>(entity =>
            {
                entity.HasKey(e => e.VoucherId)
                    .HasName("PK_VoucherForMakingCharges");

                entity.ToTable("MakingCharges_Voucher");

                entity.Property(e => e.VoucherId)
                    .HasColumnName("Voucher_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AmtInWords)
                    .IsRequired()
                    .HasColumnName("Amt_InWords");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.JbcNo)
                    .IsRequired()
                    .HasColumnName("JBC_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.RecordNo)
                    .IsRequired()
                    .HasColumnName("Record_No")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.TdsAmount).HasColumnName("TDS_Amount");

                entity.Property(e => e.TodayQty).HasColumnName("Today_Qty");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VoucherDate)
                    .HasColumnName("Voucher_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VoucherNo)
                    .IsRequired()
                    .HasColumnName("Voucher_No")
                    .HasMaxLength(50);

                entity.Property(e => e.VoucherSno).HasColumnName("Voucher_Sno");

                entity.Property(e => e.WorkerId).HasColumnName("Worker_ID");
            });

            modelBuilder.Entity<MchMaster>(entity =>
            {
                entity.HasKey(e => e.MchId)
                    .HasName("PK_MCH_Master");

                entity.ToTable("MCH_Master");

                entity.Property(e => e.MchId)
                    .HasColumnName("MCH_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MchCurrencyId).HasColumnName("MCH_Currency_ID");

                entity.Property(e => e.MchDate)
                    .HasColumnName("MCH_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MchNo)
                    .IsRequired()
                    .HasColumnName("MCH_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.MchSno).HasColumnName("MCH_SNO");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(30);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<MenOutPass>(entity =>
            {
                entity.HasKey(e => e.MenopId)
                    .HasName("PK_MEN_OutPass");

                entity.ToTable("MEN_OutPass");

                entity.Property(e => e.MenopId)
                    .HasColumnName("MENOP_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MenopDate)
                    .HasColumnName("MENOP_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MenopNo)
                    .IsRequired()
                    .HasColumnName("MENOP_No")
                    .HasMaxLength(50);

                entity.Property(e => e.MenopSerialNo).HasColumnName("MENOP_Serial_No");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffContractor).HasColumnName("Staff_Contractor");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<MessageForRmfeed>(entity =>
            {
                entity.ToTable("Message_For_RMFeed");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DoneDate)
                    .HasColumnName("Done_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnName("Item_ID");

                entity.Property(e => e.MessageDate)
                    .HasColumnName("Message_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MessageFrom).HasColumnName("Message_From");

                entity.Property(e => e.MessageStatus).HasColumnName("Message_Status");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasColumnName("Message_Text");

                entity.Property(e => e.ReadByOrder).HasColumnName("Read_BY_Order");
            });

            modelBuilder.Entity<MessageForSend>(entity =>
            {
                entity.HasKey(e => e.MsgId)
                    .HasName("PK_Message_For_Send");

                entity.ToTable("Message_For_Send");

                entity.Property(e => e.MsgId).HasColumnName("Msg_ID");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.MessageMenuId).HasColumnName("Message_Menu_ID");

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MsgDate)
                    .HasColumnName("Msg_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MsgStatus).HasColumnName("msg_Status");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<MessageMenu>(entity =>
            {
                entity.Property(e => e.MessageMenuId)
                    .HasColumnName("Message_Menu_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MessageMenuName)
                    .IsRequired()
                    .HasColumnName("Message_Menu_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MetalPartDetails>(entity =>
            {
                entity.HasKey(e => e.MepId)
                    .HasName("PK_MetalPart_Details");

                entity.ToTable("MetalPart_Details");

                entity.Property(e => e.MepId)
                    .HasColumnName("MepID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MepDesc).HasMaxLength(500);

                entity.Property(e => e.MepName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<MonthDetail>(entity =>
            {
                entity.HasKey(e => e.MonthId)
                    .HasName("PK_MonthDetail");

                entity.Property(e => e.MonthId).ValueGeneratedNever();

                entity.Property(e => e.Monthname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nature>(entity =>
            {
                entity.HasKey(e => e.Nid)
                    .HasName("PK_Nature");

                entity.Property(e => e.Nid)
                    .HasColumnName("NID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NatureDesc).HasMaxLength(500);

                entity.Property(e => e.NatureName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NatureOfGroup>(entity =>
            {
                entity.HasKey(e => e.NatureId)
                    .HasName("PK_NatureOfGroup");

                entity.Property(e => e.NatureId)
                    .HasColumnName("Nature_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NatureOfGroup1)
                    .IsRequired()
                    .HasColumnName("NatureOfGroup")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NonRetunableOutPassFitem>(entity =>
            {
                entity.HasKey(e => e.ReturnableId)
                    .HasName("PK_NonRetunableOutPass_FItem");

                entity.ToTable("NonRetunableOutPass_FItem");

                entity.Property(e => e.ReturnableId)
                    .HasColumnName("Returnable_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IssueTo).HasMaxLength(150);

                entity.Property(e => e.OutPassNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sno).HasColumnName("SNo");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NonReturnableOutPassInvoiceMaster>(entity =>
            {
                entity.HasKey(e => e.NropInvId)
                    .HasName("PK_NonReturnableOutPass_InvoiceMaster");

                entity.ToTable("NonReturnableOutPass_InvoiceMaster");

                entity.Property(e => e.NropInvId)
                    .HasColumnName("NROP_INV_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NropInvNo)
                    .IsRequired()
                    .HasColumnName("NROP_INV_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.NropInvSno).HasColumnName("NROP_INV_SNo");

                entity.Property(e => e.Nropdate)
                    .HasColumnName("NROPDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SampleExport).HasColumnName("Sample_Export");

                entity.Property(e => e.SampleExportId).HasColumnName("Sample_Export_ID");

                entity.Property(e => e.SampleExportNo)
                    .IsRequired()
                    .HasColumnName("Sample_Export_No")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NropCpMaster>(entity =>
            {
                entity.HasKey(e => e.NropTableId)
                    .HasName("PK_NROP_CP_Master");

                entity.ToTable("NROP_CP_Master");

                entity.Property(e => e.NropTableId)
                    .HasColumnName("NROP_Table_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NropDate)
                    .HasColumnName("NROP_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NropId).HasColumnName("NROP_ID");

                entity.Property(e => e.NropNo)
                    .IsRequired()
                    .HasColumnName("NROP_No")
                    .HasMaxLength(50);

                entity.Property(e => e.OutPassBy)
                    .IsRequired()
                    .HasColumnName("OutPass_By")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderCancel>(entity =>
            {
                entity.ToTable("Order_Cancel");

                entity.Property(e => e.OrderCancelId)
                    .HasColumnName("Order_Cancel_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CancelDate)
                    .HasColumnName("Cancel_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CancelReason)
                    .IsRequired()
                    .HasColumnName("Cancel_Reason");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<OrderMaster>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Order_Master");

                entity.ToTable("Order_Master");

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AfterNoOfDays).HasColumnName("After_No_OfDays");

                entity.Property(e => e.CustOrderNo)
                    .IsRequired()
                    .HasColumnName("Cust_Order_No")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("Delivery_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryDateChange).HasColumnName("Delivery_Date_Change");

                entity.Property(e => e.DeliveryDateChangeDate)
                    .HasColumnName("Delivery_Date_Change_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryDateChangeReason).HasColumnName("Delivery_Date_Change_Reason");

                entity.Property(e => e.EntryModifyDate)
                    .HasColumnName("Entry_ModifyDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FobCif)
                    .HasColumnName("FOB_CIF")
                    .HasMaxLength(50);

                entity.Property(e => e.IssuedBy).HasColumnName("Issued_By");

                entity.Property(e => e.MaterialChangeComments).HasColumnName("Material_Change_Comments");

                entity.Property(e => e.MaterialRead).HasColumnName("Material_Read");

                entity.Property(e => e.MaterialStatus).HasColumnName("Material_Status");

                entity.Property(e => e.OrderEntryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("Order_No")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderReadAfterPoread).HasColumnName("Order_Read_After_PORead");

                entity.Property(e => e.ProdApprovalKeyDate).HasColumnType("datetime");

                entity.Property(e => e.ProdStatrtingKeyDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseRead).HasColumnName("Purchase_Read");

                entity.Property(e => e.ReasonForModify).HasColumnName("Reason_For_Modify");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.ShipmentApprovlKeyDate).HasColumnType("datetime");

                entity.Property(e => e.SmsBulk).HasColumnName("SMS_BULK");

                entity.Property(e => e.TransportChange).HasColumnName("Transport_Change");

                entity.Property(e => e.TransportChangeDate)
                    .HasColumnName("Transport_Change_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TransportChangeReason).HasColumnName("Transport_Change_Reason");

                entity.Property(e => e.TransportComment).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderReminder>(entity =>
            {
                entity.ToTable("Order_Reminder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("Order_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnName("Receive_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReminderDate)
                    .HasColumnName("Reminder_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReminderFor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<OrderShippment>(entity =>
            {
                entity.HasKey(e => e.ShipId)
                    .HasName("PK_Order_Shippment");

                entity.ToTable("Order_Shippment");

                entity.Property(e => e.ShipId)
                    .HasColumnName("Ship_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.ShipDate)
                    .HasColumnName("Ship_Date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<OutPassMaster>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("PK_OutPass_Master");

                entity.ToTable("OutPass_Master");

                entity.Property(e => e.TableId)
                    .HasColumnName("Table_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OutPassBy)
                    .HasColumnName("OutPass_By")
                    .HasMaxLength(50);

                entity.Property(e => e.OutPassDate)
                    .HasColumnName("OutPass_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OutPassId).HasColumnName("OutPass_ID");

                entity.Property(e => e.OutPassNo)
                    .HasColumnName("OutPass_No")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<PanningDetails>(entity =>
            {
                entity.HasKey(e => e.PanningId)
                   .HasName("PK_Panning");

                entity.ToTable("Panning");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PanningName)
                    .IsRequired()
                    .HasColumnName("Panning")
                    .HasMaxLength(50);

                entity.Property(e => e.PanningDesc).HasMaxLength(500);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PrdPlanDetails>(entity =>
            {
                entity.HasKey(e => e.PrdPlanId)
                    .HasName("PK_Prd_Plan_Details");

                entity.ToTable("Prd_Plan_Details");

                entity.Property(e => e.PrdPlanId)
                    .HasColumnName("PrdPlan_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClickDate)
                    .HasColumnName("Click_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FittingDate)
                    .HasColumnName("Fitting_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.JobCardId).HasColumnName("JobCard_Id");

                entity.Property(e => e.LeatherDate)
                    .HasColumnName("Leather_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlanPreparedBy)
                    .IsRequired()
                    .HasColumnName("Plan_Prepared_By")
                    .HasMaxLength(50);

                entity.Property(e => e.PlanRemark)
                    .HasColumnName("Plan_Remark")
                    .HasMaxLength(250);

                entity.Property(e => e.PrdPlanDate)
                    .HasColumnName("PrdPlan_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnName("Receive_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SampleDate)
                    .HasColumnName("Sample_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SkivingDate)
                    .HasColumnName("Skiving_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SoId).HasColumnName("SO_Id");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PriceQuote>(entity =>
            {
                entity.Property(e => e.PriceQuoteId)
                    .HasColumnName("PriceQuoteID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AirCfa)
                    .HasColumnName("AIR_CFA")
                    .HasMaxLength(50);

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.ConversionRate).HasColumnType("money");

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FitemId).HasColumnName("Fitem_Id");

                entity.Property(e => e.FobCif)
                    .HasColumnName("FOB_CIF")
                    .HasMaxLength(50);

                entity.Property(e => e.PrinceRange)
                    .HasColumnName("Prince_Range")
                    .HasMaxLength(50);

                entity.Property(e => e.QuatePrice)
                    .HasColumnName("Quate_Price")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<PurchaseVoucherMaster>(entity =>
            {
                entity.HasKey(e => e.PVoucherId)
                    .HasName("PK_PurchaseVoucher_Master");

                entity.ToTable("PurchaseVoucher_Master");

                entity.Property(e => e.PVoucherId)
                    .HasColumnName("P_Voucher_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.Billno).HasMaxLength(50);

                entity.Property(e => e.PVoucherDate)
                    .HasColumnName("P_Voucher_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PVoucherSid).HasColumnName("P_Voucher_SId");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.SipOrDirect).HasColumnName("SIP_or_Direct");

                entity.Property(e => e.TaxTotal).HasColumnName("Tax_Total");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.VoucherAmt).HasColumnName("Voucher_Amt");
            });

            modelBuilder.Entity<QcInProcess>(entity =>
            {
                entity.HasKey(e => e.QcProcessId)
                    .HasName("PK_QC_In_Process");

                entity.ToTable("QC_In_Process");

                entity.Property(e => e.QcProcessId)
                    .HasColumnName("Qc_Process_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnName("Item_Id");

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcDate)
                    .HasColumnName("Qc_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remarks).IsRequired();

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<ReadymadeReceiving>(entity =>
            {
                entity.HasKey(e => e.ReceiveId)
                    .HasName("PK_ReadymadeReceiving");

                entity.Property(e => e.ReceiveId)
                    .HasColumnName("ReceiveID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.ReadymadeOrJobwork).HasColumnName("Readymade_Or_Jobwork");

                entity.Property(e => e.ReceiveBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiveDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiveNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReceiveSno).HasColumnName("ReceiveSNo");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(20);

                entity.Property(e => e.SipDate)
                    .HasColumnName("SIP_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SipQty).HasColumnName("SIP_Qty");

                entity.Property(e => e.SipStatus).HasColumnName("SIP_Status");

                entity.Property(e => e.SipUser).HasColumnName("SIP_User");

                entity.Property(e => e.SubWpId).HasColumnName("SubWP_ID");
            });

            modelBuilder.Entity<ReciptsFromBuyerMaster>(entity =>
            {
                entity.HasKey(e => e.ReciptsId)
                    .HasName("PK_ReciptsFromBuyer_Master");

                entity.ToTable("ReciptsFromBuyer_Master");

                entity.Property(e => e.ReciptsId)
                    .HasColumnName("Recipts_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AwbNo)
                    .HasColumnName("Awb_No")
                    .HasMaxLength(50);

                entity.Property(e => e.BuyerId).HasColumnName("Buyer_ID");

                entity.Property(e => e.CourierId).HasColumnName("Courier_Id");

                entity.Property(e => e.MadiatorId).HasColumnName("Madiator_ID");

                entity.Property(e => e.ReciptsDate)
                    .HasColumnName("Recipts_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReciptsNo).HasColumnName("Recipts_No");

                entity.Property(e => e.ReciptsNo1)
                    .HasColumnName("ReciptsNo")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<RitemMaster>(entity =>
            {
                entity.HasKey(e => e.RitemId).HasName("PK_RItem_Master");

                entity.ToTable("RItem_Master");

                entity.Property(e => e.RitemId).HasColumnName("RItem_Id").ValueGeneratedNever();

                entity.Property(e => e.ActualCostPrice).HasColumnType("money");

                entity.Property(e => e.ActualCostUnit).HasMaxLength(50);

                entity.Property(e => e.ActualPunit).HasColumnName("ActualPUnit").HasMaxLength(50);

                entity.Property(e => e.ActualPurPrice).HasColumnType("money");

                entity.Property(e => e.Adhesive).HasMaxLength(50);

                entity.Property(e => e.AverageArea).HasMaxLength(50);

                entity.Property(e => e.Base).HasMaxLength(50);

                entity.Property(e => e.BomWasPercent).HasColumnName("BOM_WAS_Percent");

                entity.Property(e => e.Cartoon).HasMaxLength(50);

                entity.Property(e => e.Cloth).HasMaxLength(50);

                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);

                entity.Property(e => e.ColorFast).HasMaxLength(6);

                entity.Property(e => e.Colour).HasMaxLength(50);

                entity.Property(e => e.CostPrice).HasColumnType("money");

                entity.Property(e => e.CostUnit).HasMaxLength(50);

                entity.Property(e => e.Design).HasMaxLength(50);

                entity.Property(e => e.Diameter).HasMaxLength(50);

                entity.Property(e => e.Dim1).HasMaxLength(50);

                entity.Property(e => e.Dim2).HasMaxLength(50);

                entity.Property(e => e.Dnsity).HasMaxLength(50);

                entity.Property(e => e.DyedSpe).HasMaxLength(50);

                entity.Property(e => e.EuNorms).HasMaxLength(50);

                entity.Property(e => e.Finish).HasMaxLength(50);

                entity.Property(e => e.FullName).HasColumnName("Full_Name");

                entity.Property(e => e.ItOrCat).HasColumnName("It_OR_Cat");

                entity.Property(e => e.MaxStock).HasColumnName("Max_Stock");

                entity.Property(e => e.MaxWastage).HasColumnName("Max_Wastage");

                entity.Property(e => e.Metal).HasMaxLength(50);

                entity.Property(e => e.MinStock).HasColumnName("Min_Stock");

                entity.Property(e => e.MonthYear).HasColumnName("Month_Year");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                entity.Property(e => e.NickleFree).HasMaxLength(50);

                entity.Property(e => e.OpStock).HasColumnName("Op_Stock");

                entity.Property(e => e.PanningWay).HasMaxLength(50);

                entity.Property(e => e.PickThickness).HasMaxLength(50);

                entity.Property(e => e.PucrhasePrice).HasColumnType("money");

                entity.Property(e => e.Punit).HasColumnName("PUnit").HasMaxLength(50);

                entity.Property(e => e.PurchaseType).HasColumnName("Purchase_Type").HasMaxLength(50);

                entity.Property(e => e.Quality).HasMaxLength(50);

                entity.Property(e => e.RmCode).HasColumnName("RM_Code");

                entity.Property(e => e.Selection).HasMaxLength(50);

                entity.Property(e => e.SessionYear).HasColumnName("Session_year").HasMaxLength(20);

                entity.Property(e => e.Shape).HasMaxLength(50);

                entity.Property(e => e.SizeName).HasMaxLength(50);

                entity.Property(e => e.Stone).HasMaxLength(50);

                entity.Property(e => e.StrenthTest).HasMaxLength(50);

                entity.Property(e => e.Stucture).HasMaxLength(50);

                entity.Property(e => e.SuppCode).HasMaxLength(50);

                entity.Property(e => e.Thickness).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Was).HasColumnName("WAS");

                entity.Property(e => e.Washable).HasMaxLength(50);

                entity.Property(e => e.WireSize).HasMaxLength(50);
            });

            modelBuilder.Entity<RMCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryID).HasName("PK_RMCategory");
                entity.Property(e => e.CategoryID).HasColumnName("CategoryID").ValueGeneratedNever();
                entity.Property(e => e.CategoryName).HasMaxLength(100);
                entity.Property(e => e.ShortCode).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<RMSubCategory>(entity =>
            {
                entity.HasKey(e => e.SubCategoryID).HasName("PK_RMSubCategory");
                entity.Property(e => e.SubCategoryID).HasColumnName("SubCategoryID").ValueGeneratedNever();
                entity.Property(e => e.SubCategoryName).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CategoryID).HasColumnName("CategoryID"); ;
            });

            modelBuilder.Entity<SalaryDetailsMaster>(entity =>
            {
                entity.HasKey(e => e.SalId)
                    .HasName("PK_Salary_Details_Master");

                entity.ToTable("Salary_Details_Master");

                entity.Property(e => e.SalId)
                    .HasColumnName("Sal_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActulPay)
                    .HasColumnName("Actul_Pay")
                    .HasColumnType("money");

                entity.Property(e => e.Advance).HasColumnType("money");

                entity.Property(e => e.Allowance).HasColumnType("money");

                entity.Property(e => e.Bonus).HasColumnType("money");

                entity.Property(e => e.Cl).HasColumnName("CL");

                entity.Property(e => e.ContriEmprAmt)
                    .HasColumnName("Contri_Empr_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.ContriEmprPer).HasColumnName("Contri_Empr_Per");

                entity.Property(e => e.ContriType)
                    .HasColumnName("Contri_type")
                    .HasColumnType("nchar(2)");

                entity.Property(e => e.Dadiff)
                    .HasColumnName("DADiff")
                    .HasColumnType("money");

                entity.Property(e => e.DeduType)
                    .HasColumnName("Dedu_Type")
                    .HasColumnType("nchar(2)");

                entity.Property(e => e.El).HasColumnName("EL");

                entity.Property(e => e.EmpId).HasColumnName("Emp_Id");

                entity.Property(e => e.EmpPDay).HasColumnName("Emp_P_day");

                entity.Property(e => e.EpfEmplyeAmt)
                    .HasColumnName("EPF_Emplye_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.EpfEmplyePer).HasColumnName("EPF_Emplye_Per");

                entity.Property(e => e.EpfEmprAmt)
                    .HasColumnName("EPF_Empr_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.EpfEmprPer).HasColumnName("EPF_Empr_Per");

                entity.Property(e => e.EsiEmplyeAmt)
                    .HasColumnName("ESI_Emplye_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.EsiEmplyePer).HasColumnName("ESI_Emplye_Per");

                entity.Property(e => e.EsiEmprAmt)
                    .HasColumnName("ESI_Empr_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.EsiEmprPer).HasColumnName("ESI_Empr_Per");

                entity.Property(e => e.EsiLimit)
                    .HasColumnName("ESI_Limit")
                    .HasColumnType("money");

                entity.Property(e => e.EsiRate)
                    .HasColumnName("ESI_Rate")
                    .HasColumnType("money");

                entity.Property(e => e.GrdTot)
                    .HasColumnName("Grd_Tot")
                    .HasColumnType("money");

                entity.Property(e => e.IncomTax)
                    .HasColumnName("Incom_Tax")
                    .HasColumnType("money");

                entity.Property(e => e.Lic)
                    .HasColumnName("LIC")
                    .HasColumnType("money");

                entity.Property(e => e.Ltc).HasColumnName("LTC");

                entity.Property(e => e.NetSalary)
                    .HasColumnName("Net_Salary")
                    .HasColumnType("money");

                entity.Property(e => e.OtherAmt)
                    .HasColumnName("Other_Amt")
                    .HasColumnType("money");

                entity.Property(e => e.OtherLeave).HasColumnName("Other_leave");

                entity.Property(e => e.PDays).HasColumnName("P_Days");

                entity.Property(e => e.PfLimit)
                    .HasColumnName("PF_Limit")
                    .HasColumnType("money");

                entity.Property(e => e.PfRate)
                    .HasColumnName("PF_Rate")
                    .HasColumnType("money");

                entity.Property(e => e.RoundOff)
                    .HasColumnName("Round_off")
                    .HasColumnType("money");

                entity.Property(e => e.SYear)
                    .HasColumnName("S_Year")
                    .HasMaxLength(5);

                entity.Property(e => e.SalDate)
                    .HasColumnName("Sal_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SalRate)
                    .HasColumnName("Sal_Rate")
                    .HasColumnType("money");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.TotContribut)
                    .HasColumnName("Tot_Contribut")
                    .HasColumnType("money");

                entity.Property(e => e.TotDeduction)
                    .HasColumnName("Tot_Deduction")
                    .HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WDays).HasColumnName("W_Days");
            });


            modelBuilder.Entity<ShapeDetails>(entity =>
            {
                entity.HasKey(e => e.ShapeId)
                   .HasName("PK_Shape");

                entity.ToTable("Shape");

                entity.Property(e => e.ShapeId)
                    .HasColumnName("ShapeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.ShapeName)
                    .IsRequired()
                    .HasColumnName("Shape")
                    .HasMaxLength(50);

                entity.Property(e => e.ShapeDesc).HasMaxLength(500);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SipMaster>(entity =>
            {
                entity.HasKey(e => e.SipId)
                    .HasName("PK_SIP_Master");

                entity.ToTable("SIP_Master");

                entity.Property(e => e.SipId)
                    .HasColumnName("SIP_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Gipno)
                    .IsRequired()
                    .HasColumnName("GIPNo")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SipBy)
                    .IsRequired()
                    .HasColumnName("SIP_By")
                    .HasMaxLength(50);

                entity.Property(e => e.SipDate)
                    .HasColumnName("SIP_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SipNo)
                    .IsRequired()
                    .HasColumnName("SIP_No")
                    .HasMaxLength(50);

                entity.Property(e => e.SipRemark).HasColumnName("SIP_Remark");

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<SizeDetails>(entity =>
            {
                entity.HasKey(e => e.SizeId)
                    .HasName("PK_Size");

                entity.Property(e => e.SizeId)
                    .HasColumnName("SizeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SizeDesc).HasMaxLength(500);

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SkiveingDetails>(entity =>
            {
                entity.HasKey(e => e.SkiveId)
                    .HasName("PK_Skiveing_Details");

                entity.ToTable("Skiveing_Details");

                entity.Property(e => e.SkiveId)
                    .HasColumnName("Skive_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.JobCardId).HasColumnName("JobCard_Id");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SkiveDescrpt)
                    .HasColumnName("Skive_Descrpt")
                    .HasMaxLength(150);

                entity.Property(e => e.SkiveEnd)
                    .HasColumnName("Skive_End")
                    .HasColumnType("datetime");

                entity.Property(e => e.SkiveRate)
                    .HasColumnName("Skive_Rate")
                    .HasColumnType("money");

                entity.Property(e => e.SkiveStart)
                    .HasColumnName("Skive_Start")
                    .HasColumnType("datetime");

                entity.Property(e => e.SoId).HasColumnName("SO_Id");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SkivingMaster>(entity =>
            {
                entity.HasKey(e => e.SkivingTid)
                    .HasName("PK_Skiving_Master");

                entity.ToTable("Skiving_Master");

                entity.Property(e => e.SkivingTid)
                    .HasColumnName("Skiving_TID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments).HasMaxLength(150);

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcOnProcess).HasColumnName("Qc_On_Process");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.SkivingDate)
                    .HasColumnName("Skiving_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SkivingId).HasColumnName("Skiving_ID");

                entity.Property(e => e.SkivingNo)
                    .IsRequired()
                    .HasColumnName("Skiving_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<SplittingMaster>(entity =>
            {
                entity.HasKey(e => e.SplittingTid)
                    .HasName("PK_Splitting_Master");

                entity.ToTable("Splitting_Master");

                entity.Property(e => e.SplittingTid)
                    .HasColumnName("Splitting_TID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments).HasMaxLength(150);

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcOnProcess).HasColumnName("Qc_On_Process");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.SplittingDate)
                    .HasColumnName("Splitting_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SplittingId).HasColumnName("Splitting_ID");

                entity.Property(e => e.SplittingNo)
                    .IsRequired()
                    .HasColumnName("Splitting_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId)
                    .HasColumnName("Staff_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnName("STaff_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stone>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK_STONE");

                entity.ToTable("STONE");

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_Year")
                    .HasMaxLength(20);

                entity.Property(e => e.StoneDesc).HasMaxLength(500);

                entity.Property(e => e.StoneName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<StoreKeepingRmchangeMaster>(entity =>
            {
                entity.HasKey(e => e.SkRmChangeId)
                    .HasName("PK_StoreKeeping_RMChange_Master");

                entity.ToTable("StoreKeeping_RMChange_Master");

                entity.Property(e => e.SkRmChangeId)
                    .HasColumnName("SK_RM_Change_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.InpassTableId).HasColumnName("INPassTable_ID");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(20);

                entity.Property(e => e.SkBy)
                    .IsRequired()
                    .HasColumnName("SK_By")
                    .HasMaxLength(50);

                entity.Property(e => e.SkRemark)
                    .HasColumnName("SK_Remark")
                    .HasMaxLength(150);

                entity.Property(e => e.SkRmChangeDate)
                    .HasColumnName("SK_RM_Change_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<StructureDetails>(entity =>
            {
                entity.HasKey(e => e.Tsid)
                    .HasName("PK_Structure");

                entity.ToTable("Structure");

                entity.Property(e => e.Tsid)
                    .HasColumnName("TSID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.Tsdesc)
                    .HasColumnName("TSDesc")
                    .HasMaxLength(500);

                entity.Property(e => e.Tsname)
                    .IsRequired()
                    .HasColumnName("TSName")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SelectionDetails>(entity =>
            {
                entity.HasKey(e => e.SelectionID)
                    .HasName("PK_Selection");

                entity.ToTable("Selection");

                entity.Property(e => e.SelectionID)
                    .HasColumnName("SelectionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.SelectionDesc)
                    .HasColumnName("SelectionDesc")
                    .HasMaxLength(500);

                entity.Property(e => e.Selection)
                    .IsRequired()
                    .HasColumnName("Selection")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SupervisorDetails>(entity =>
            {
                entity.ToTable("Supervisor_Details");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContrOrSuper).HasColumnName("Contr_or_Super");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TempTable>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_Temp_Table_1");

                entity.ToTable("Temp_Table");

                entity.Property(e => e.EmpId)
                    .HasColumnName("Emp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressOffice)
                    .IsRequired()
                    .HasColumnName("Address_office");

                entity.Property(e => e.Advance).HasColumnType("money");

                entity.Property(e => e.Basic).HasColumnType("money");

                entity.Property(e => e.Cl)
                    .HasColumnName("CL")
                    .HasMaxLength(4);

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasColumnName("CName")
                    .HasMaxLength(50);

                entity.Property(e => e.Convey).HasColumnType("money");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Designation).HasMaxLength(30);

                entity.Property(e => e.El)
                    .HasColumnName("EL")
                    .HasMaxLength(4);

                entity.Property(e => e.EmpCode).HasColumnName("Emp_Code");

                entity.Property(e => e.EmpFname)
                    .HasColumnName("Emp_Fname")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpName)
                    .HasColumnName("Emp_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Esi)
                    .HasColumnName("ESI")
                    .HasColumnType("money");

                entity.Property(e => e.GraTotal)
                    .HasColumnName("Gra_Total")
                    .HasColumnType("money");

                entity.Property(e => e.Hra)
                    .HasColumnName("HRA")
                    .HasColumnType("money");

                entity.Property(e => e.Ltc)
                    .HasColumnName("LTC")
                    .HasMaxLength(4);

                entity.Property(e => e.Medical).HasColumnType("money");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NetSalary).HasColumnType("money");

                entity.Property(e => e.OtherLeave)
                    .HasColumnName("other_leave")
                    .HasMaxLength(4);

                entity.Property(e => e.PDays).HasColumnName("P_days");

                entity.Property(e => e.Pf)
                    .HasColumnName("PF")
                    .HasColumnType("money");

                entity.Property(e => e.PfNo)
                    .HasColumnName("Pf_no")
                    .HasMaxLength(20);

                entity.Property(e => e.RoundOff)
                    .HasColumnName("Round_off")
                    .HasColumnType("money");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("money");

                entity.Property(e => e.Special).HasColumnType("money");

                entity.Property(e => e.Suspence).HasColumnType("money");

                entity.Property(e => e.Tds)
                    .HasColumnName("TDS")
                    .HasColumnType("money");

                entity.Property(e => e.Trp)
                    .HasColumnName("TRP")
                    .HasColumnType("money");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ThicknessDetails>(entity =>
            {
                entity.HasKey(e => e.Thid)
                    .HasName("PK_Thickness");

                entity.ToTable("Thickness");

                entity.Property(e => e.Thid)
                    .HasColumnName("THID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.Thdesc)
                    .HasColumnName("THDesc")
                    .HasMaxLength(500);

                entity.Property(e => e.Thname)
                    .IsRequired()
                    .HasColumnName("THName")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TransportDetails>(entity =>
            {
                entity.HasKey(e => e.TransportId)
                    .HasName("PK_Transport_Details");

                entity.ToTable("Transport_Details");

                entity.Property(e => e.TransportId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.TranportName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransportDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserId);
            });

            modelBuilder.Entity<TypeOfColourDetails>(entity =>
            {
                entity.HasKey(e => e.TypeOfColourId)
                    .HasName("PK_TypeOfColour_Details");

                entity.ToTable("TypeOfColour_Details");

                entity.Property(e => e.TypeOfColourId)
                    .HasColumnName("TypeOfColourID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.TypeOfColour)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeOfColourDesc).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UnitDetails>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_Unit_Details");

                entity.ToTable("Unit_Details");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.Udesc)
                    .HasColumnName("UDesc")
                    .HasMaxLength(150);

                entity.Property(e => e.Uname)
                    .IsRequired()
                    .HasColumnName("UName")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Usname)
                    .IsRequired()
                    .HasColumnName("USName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WaxingMaster>(entity =>
            {
                entity.HasKey(e => e.WaxingTid)
                    .HasName("PK_Waxing_Master");

                entity.ToTable("Waxing_Master");

                entity.Property(e => e.WaxingTid)
                    .HasColumnName("Waxing_TID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments).HasMaxLength(150);

                entity.Property(e => e.PlanId).HasColumnName("Plan_Id");

                entity.Property(e => e.QcOnProcess).HasColumnName("Qc_On_Process");

                entity.Property(e => e.SessionYear)
                    .IsRequired()
                    .HasColumnName("Session_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.WaxingDate)
                    .HasColumnName("Waxing_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.WaxingId).HasColumnName("Waxing_ID");

                entity.Property(e => e.WaxingNo)
                    .IsRequired()
                    .HasColumnName("Waxing_NO")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WeekDays>(entity =>
            {
                entity.ToTable("Week_Days");

                entity.Property(e => e.WeekDaysId)
                    .HasColumnName("Week_DaysId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NoOfDays).HasColumnName("No_Of_Days");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.WeekId).HasColumnName("Week_Id");
            });

            modelBuilder.Entity<WeekNumber>(entity =>
            {
                entity.HasKey(e => e.WeekId)
                    .HasName("PK_WeekNumber");

                entity.Property(e => e.WeekId)
                    .HasColumnName("Week_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.WeekName)
                    .HasColumnName("Week_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AdhesiveDetails>(entity =>
            {
                entity.HasKey(e => e.ADID).HasName("PK_Adhesive_details");

                entity.ToTable("Adhesive_details");
                entity.Property(e => e.ADName);
                entity.Property(e => e.Description);
                entity.Property(e => e.Date).HasColumnType("datetime"); ;
                entity.Property(e => e.session_year);
                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<CurrencyDetails>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK_Currency_Details");

                entity.ToTable("Currency_Details");

                entity.Property(e => e.Cid)
                    .IsRequired()
                    .HasColumnName("CID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasColumnName("CName")
                    .HasMaxLength(50);

                entity.Property(e => e.Csname)
                    .IsRequired()
                    .HasColumnName("CSName")
                    .HasMaxLength(50);

                entity.Property(e => e.Crate)
                    .IsRequired()
                    .HasColumnName("CRate");

                entity.Property(e => e.EntryDate).HasColumnType("datetime")
                    .HasColumnName("Entry_Date");


                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CurrencyHistoryDetails>(entity =>
            {
                entity.HasKey(e => e.CurrencyHistoryId).HasName("PK_CurrencyHistoryDetails");
                entity.Property(e => e.CID);
                entity.Property(e => e.ChangeDate);
                entity.Property(e => e.PreviousPrice);
                entity.Property(e => e.ChangePrice);
                entity.Property(e => e.UserId);
            });

            modelBuilder.Entity<TrimmingDetails>(entity =>
            {
                entity.HasKey(e => e.TrimmingId)
                    .HasName("PK_Trimming_Details");

                entity.ToTable("Trimming_Details");

                entity.Property(e => e.TrimmingId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.TrimmingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrimmingDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<EmbossmentDetails>(entity =>
            {
                entity.HasKey(e => e.EmbossmentId)
                    .HasName("PK_Embossment_Details");

                entity.ToTable("Embossment_Details");

                entity.Property(e => e.EmbossmentId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.EmbossmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmbossmentDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<WaxDetails>(entity =>
            {
                entity.HasKey(e => e.WaxID)
                    .HasName("PK_WAX");

                entity.ToTable("WAX");

                entity.Property(e => e.WaxID)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.WaxName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WaxDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<BackingDetails>(entity =>
            {
                entity.HasKey(e => e.BackingId)
                    .HasName("PK_Backing_Details");

                entity.ToTable("Backing_Details");

                entity.Property(e => e.BackingId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.BackingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BackingDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<QualityDetails>(entity =>
            {
                entity.HasKey(e => e.QualityId)
                    .HasName("PK_Quality_Details");

                entity.ToTable("Quality_Details");

                entity.Property(e => e.QualityId)
                    .HasColumnName("QualityId")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("EntryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.QualityDesc).HasMaxLength(100);

                entity.Property(e => e.QualityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sessionyear)
                    .HasColumnName("Sessionyear")
                    .HasMaxLength(20);
                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            modelBuilder.Entity<QuickTestDetails>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PK_QuickTest");
                entity.ToTable("QuickTest");

                entity.Property(e => e.ID)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TestName)
                    .HasColumnName("TestName")
                    .HasMaxLength(200);

                entity.Property(e => e.MasterBelongTo).HasColumnName("MasterBelongTo");
            });

            modelBuilder.Entity<LiningDetails>(entity =>
            {
                entity.HasKey(e => e.LiningID)
                    .HasName("PK_Lining_Details");

                entity.ToTable("Lining_Details");

                entity.Property(e => e.LiningID)
                    .HasColumnName("LiningID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("EntryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.LiningDesc).HasMaxLength(100);

                entity.Property(e => e.LiningName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sessionyear)
                    .HasColumnName("Sessionyear")
                    .HasMaxLength(20);
                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            modelBuilder.Entity<WashDetails>(entity =>
            {
                entity.HasKey(e => e.WashID)
                    .HasName("PK_Wash");

                entity.ToTable("Wash");

                entity.Property(e => e.WashID)
                    .HasColumnName("WashID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                    .HasColumnName("EntryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Wash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sessionyear)
                    .HasColumnName("Sessionyear")
                    .HasMaxLength(20);
                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            modelBuilder.Entity<StitchingDetails>(entity =>
            {
                entity.HasKey(e => e.StitchingId)
                    .HasName("PK_Stitching_Details");

                entity.ToTable("Stitching_Details");

                entity.Property(e => e.StitchingId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.StitchingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StitchingDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<GroupDetails>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_GroupMaster");

                entity.ToTable("GroupMaster");

                entity.Property(e => e.GroupId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GroupDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<DesignerDetails>(entity =>
            {
                entity.HasKey(e => e.DesignerId)
                    .HasName("PK_Designer_Details");

                entity.ToTable("Designer_Details");

                entity.Property(e => e.DesignerId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.DesignerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DesignerDesc)
                    .HasMaxLength(100);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<CountryDetails>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK_Country_Details");

                entity.ToTable("Country_Details");

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                            .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<StateDetails>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK_State_Details");

                entity.ToTable("State_Details");

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate)
                      .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<CityDetails>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK_City_Details");

                entity.ToTable("City_Details");

                entity.Property(e => e.CityId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<SupplierDetails>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK_SupplierDetails");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SupplierId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<ConsigneeDetails>(entity =>
            {
                entity.HasKey(e => e.ConsigneeId)
                    .HasName("PK_Consignee_Details");

                entity.ToTable("Consignee_Details");

                entity.Property(e => e.ConsigneeId)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConsigneeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConsigneeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConsigneeAddress)
                    .IsRequired();

                entity.Property(e => e.CountryId);

                entity.Property(e => e.StateId);

                entity.Property(e => e.CityId);

                entity.Property(e => e.ConsigneePin)
                    .HasMaxLength(6);

                entity.Property(e => e.ConsigneeEmail);

                entity.Property(e => e.ConsigneeMobile)
                    .HasMaxLength(15);

                entity.Property(e => e.ConsigneePhone)
                    .HasMaxLength(15);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasMaxLength(20);

                entity.Property(e => e.UserId);

            });

            modelBuilder.Entity<ProductCategoryDetails>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryID).HasName("PK_ProductCategory");
                entity.ToTable("ProductCategory");
                entity.Property(e => e.ProductCategoryID).ValueGeneratedNever();
                entity.Property(e => e.ProductCategoryName);
                entity.Property(e => e.ProductShortCode);
                entity.Property(e => e.ProductStartWith);
                entity.Property(e => e.Description);
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<ProductSubCategoryDetails>(entity =>
            {
                entity.HasKey(e => e.ProductSubCategoryID).HasName("PK_ProductSubCategory");
                entity.ToTable("ProductSubCategory");
                entity.Property(e => e.ProductSubCategoryID).ValueGeneratedNever();
                entity.Property(e => e.ProductCategoryID);
                entity.Property(e => e.ProductSubCategoryName);
                entity.Property(e => e.Description);
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserID);
                entity.Property(e => e.ProductShortCode);
                entity.Property(e => e.ProductStartWith);
            });

            modelBuilder.Entity<StickerDetials>(entity =>
            {
                entity.HasKey(e => e.StickerID).HasName("PK_StickerDetials");
                entity.ToTable("StickerDetials");
                entity.Property(e => e.StickerID).ValueGeneratedNever();
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.StickerName);
                entity.Property(e => e.Description);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.Sessionyear);
                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<CareLabelDetails>(entity =>
            {
                entity.HasKey(e => e.CareLabelID).HasName("PK_CareLabelDetails");
                entity.ToTable("CareLabelDetails");
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.CareLabelID).ValueGeneratedNever();
                entity.Property(e => e.CareLabelLName);
                entity.Property(e => e.Description);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<HandTagDetails>(entity =>
            {
                entity.HasKey(e => e.HandTagID)
                    .HasName("PK_HandTagDetails");
                entity.ToTable("HandTagDetails");
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.HandTagID).ValueGeneratedNever();
                entity.Property(e => e.HandTagName);
                entity.Property(e => e.Description);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserID);
            });

            modelBuilder.Entity<BuyerDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerId)
                    .HasName("PK_BuyerDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.BuyerId)
                    .IsRequired()
                    .ValueGeneratedNever();

            });

            modelBuilder.Entity<WidthDetails>(entity =>
            {
                entity.HasKey(e => e.WidthId)
                    .HasName("PK_WidthDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.WidthId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<GSMDetails>(entity =>
            {
                entity.HasKey(e => e.GSMId)
                    .HasName("PK_GSMDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.GSMId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<BrandDetails>(entity =>
            {
                entity.HasKey(e => e.BrandId)
                    .HasName("PK_BrandDetails");

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IssuedByDetails>(entity =>
            {
                entity.HasKey(e => e.IssuedById)
                    .HasName("PK_IssuedByDetails");

                entity.Property(e => e.IssuedById)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LabelDetails>(entity =>
            {
                entity.HasKey(e => e.LabelId)
                    .HasName("PK_LabelDetails");

                entity.Property(e => e.LabelId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductSizeDetails>(entity =>
            {
                entity.HasKey(e => e.ProductSizeId)
                    .HasName("PK_ProductSizeDetails");

                entity.Property(e => e.ProductSizeId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RackMaster>(entity =>
            {
                entity.HasKey(e => e.RackId)
                    .HasName("PK_RackMaster");

                entity.Property(e => e.RackId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TreatmentDetails>(entity =>
            {
                entity.HasKey(e => e.TreatmentId)
                    .HasName("PK_TreatmentDetails");

                entity.Property(e => e.TreatmentId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductDetails>(entity =>
            {
                entity.HasKey(e => e.FitemId)
                    .HasName("PK_ProductDetails");

                entity.Property(e => e.FitemId)
                    .ValueGeneratedNever();
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.ActualDispatchDate).HasColumnType("datetime");
                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
                entity.Property(e => e.PartyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductMultipleColor>(entity =>
            {
                entity.HasKey(e => e.ProductMultipleColorId)
                    .HasName("PK_ProductMultipleColor");

                entity.Property(e => e.ProductMultipleColorId).ValueGeneratedNever();

                entity.HasOne(d => d.ProductDetails)
                    .WithMany(p => p.ProductMultipleColor)
                    .HasForeignKey(d => d.FitemId);
            });

            modelBuilder.Entity<ProductMultipleSize>(entity =>
            {
                entity.HasKey(e => e.ProductMultipleSizeId)
                    .HasName("PK_ProductMultipleSize");

                entity.Property(e => e.ProductMultipleSizeId).ValueGeneratedNever();

                entity.HasOne(d => d.ProductDetails)
                    .WithMany(p => p.ProductMultipleSize)
                    .HasForeignKey(d => d.FitemId);
            });

            modelBuilder.Entity<ProductProcess>(entity =>
            {
                entity.HasKey(e => e.ProductProcessID)
                    .HasName("PK_ProductProcess");

                entity.Property(e => e.ProductProcessID).ValueGeneratedNever();

                entity.HasOne(d => d.ProductDetails)
                    .WithMany(p => p.ProductProcess)
                    .HasForeignKey(d => d.FitemId);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductionProcessDetails>(entity =>
            {
                entity.HasKey(e => e.ProcessID).HasName("PK_ProductionProcessDetails");
                entity.ToTable("ProductionProcess");
                entity.Property(e => e.ProcessID);
                entity.Property(e => e.ProcessID).ValueGeneratedNever();
                entity.Property(e => e.ProcessName);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserId);
            });

            modelBuilder.Entity<BranchDetails>(entity =>
            {
                entity.HasKey(e => e.BranchID).HasName("PK_BranchDetails");
                entity.ToTable("BranchDetails");
                entity.Property(e => e.BranchID).ValueGeneratedNever();
                entity.Property(e => e.BranchName);
                entity.Property(e => e.Address);
                entity.Property(e => e.PhoneNo);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserId);
            });

            modelBuilder.Entity<DieDetails>(entity =>
            {
                entity.HasKey(e => e.DieId)
                    .HasName("PK_DieDetails");

                entity.Property(e => e.DieId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DieFitem>(entity =>
            {
                entity.HasKey(e => e.DieFitemId)
                    .HasName("PK_DieFitem");

                entity.Property(e => e.DieFitemId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<StyleDetails>(entity =>
            {
                entity.HasKey(e => e.StyleID)
                    .HasName("PK_StyleDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });



            modelBuilder.Entity<MenuDetails>(entity =>
            {
                entity.HasKey(e => e.MenuID).HasName("PK_MenuDetails");
                entity.ToTable("Menu");
                entity.Property(e => e.MenuID);
                entity.Property(e => e.MenuID).ValueGeneratedNever();
                entity.Property(e => e.MenuName);
                entity.Property(e => e.MenuURL);
                entity.Property(e => e.MenuCategoryID);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserId);
            });


            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.HasKey(e => e.MenuCategoryID).HasName("PK_MenuCategory");
                entity.ToTable("MenuCategory");
                entity.Property(e => e.MenuCategoryID);
                entity.Property(e => e.MenuCategoryID).ValueGeneratedNever();
                entity.Property(e => e.MenuCategoryName);
            });

            modelBuilder.Entity<SaleOrderMaster>(entity =>
            {
                entity.HasKey(e => e.OrderMasterID).HasName("PK_SaleOrderMaster");
                entity.ToTable("SaleOrderMaster");
                entity.Property(e => e.OrderNo);
                entity.Property(e => e.OrderMasterID).ValueGeneratedNever();
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.OrderDate);
                entity.Property(e => e.DeliveryDate);
                entity.Property(e => e.Remark);
                entity.Property(e => e.TotalQty);
                entity.Property(e => e.TotalAmount);
                entity.Property(e => e.CancelStatus);
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.UserID);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SaleOrderChild>(entity =>
            {
                entity.HasKey(e => e.OrderChildID).HasName("PK_SaleOrderChild");
                entity.Property(e => e.OrderMasterID);
                entity.Property(e => e.FitemId);
                entity.Property(e => e.Qty);
                entity.Property(e => e.Size);
                entity.Property(e => e.Price);
                entity.Property(e => e.Amount);

                entity.HasOne(d => d.SaleOrderMaster)
                    .WithMany(p => p.SaleOrderChild)
                    .HasForeignKey(d => d.OrderMasterID);
            });

            modelBuilder.Entity<FOBCIFDetails>(entity =>
            {
                entity.HasKey(e => e.FobCifID).HasName("PK_FOBCIF");
                entity.ToTable("FOBCIF");
                entity.Property(e => e.FobCifID)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<StyleDetails>(entity =>
            {
                entity.HasKey(e => e.StyleID)
                    .HasName("PK_StyleDetails");

                entity.Property(e => e.StyleID)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailSettings>(entity =>
            {
                entity.HasKey(e => e.EmailSettingsID)
                    .HasName("PK_EmailSettings");

                entity.Property(e => e.EmailSettingsID)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.HasKey(e => e.EmailTypeId)
                    .HasName("PK_EmailType");

                entity.Property(e => e.EmailTypeId)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<EmailSendDetails>(entity =>
            {
                entity.HasKey(e => e.EmailSendID)
                    .HasName("PK_SendEmailTable");

                entity.Property(e => e.EmailSendID)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.SendDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OpeningStockDetails>(entity =>
            {
                entity.HasKey(e => e.OpeningStockID)
                    .HasName("PK_OpeningStockDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContractorDetails>(entity =>
            {
                entity.HasKey(e => e.ContractorID).HasName("PK_ContractorDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.OpeningDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TempPOChild>(entity =>
            {
                entity.HasKey(e => e.SNo)
                    .HasName("PK_TempPOChild");
            });

            modelBuilder.Entity<WorkPlanMaster>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK_WorkPlanMaster");
            });

            modelBuilder.Entity<WorkPlanChild>(entity =>
            {
                entity.HasKey(e => e.PlanChildId)
                    .HasName("PK_WorkPlanChild");

                entity.Property(e => e.PlanChildId).ValueGeneratedNever();

                entity.HasOne(d => d.WorkPlanMaster)
                    .WithMany(p => p.WorkPlanChild)
                    .HasForeignKey(d => d.PlanId);
            });

            modelBuilder.Entity<IssueMaster>(entity =>
            {
                entity.HasKey(e => e.IssueID)
                    .HasName("PK_IssueMaster");
            });

            modelBuilder.Entity<IssueChild>(entity =>
            {
                entity.HasKey(e => e.IssueChildID)
                    .HasName("PK_IssueChild");

                entity.Property(e => e.IssueChildID).ValueGeneratedNever();

                entity.HasOne(d => d.IssueMaster)
                    .WithMany(p => p.IssueChild)
                    .HasForeignKey(d => d.IssueID);
            });

            modelBuilder.Entity<GRNMaster>(entity =>
            {
                entity.HasKey(e => e.GRNID)
                    .HasName("PK_GRNMaster");
            });

            modelBuilder.Entity<GRNChild>(entity =>
            {
                entity.HasKey(e => e.GRNChildID)
                    .HasName("PK_GRNChild");

                entity.Property(e => e.GRNChildID).ValueGeneratedNever();

                entity.HasOne(d => d.GRNMaster)
                    .WithMany(p => p.GRNChild)
                    .HasForeignKey(d => d.GRNID);
            });

            modelBuilder.Entity<GRNJWRMComsumption>(entity =>
            {
                entity.HasKey(e => e.GRNJWRMComsumptionId);
                entity.HasOne(d => d.GRNMaster)
                    .WithMany(p => p.GRNJWRMComsumption)
                    .HasForeignKey(d => d.GRNID);
            });

            modelBuilder.Entity<StoreKeepingMaster>(entity =>
            {
                entity.HasKey(e => e.StoreKeepingID)
                    .HasName("PK_StoreKeepingMaster");
                entity.Property(e => e.StoreKeepingDate).HasColumnType("datetime");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StoreKeepingChild>(entity =>
            {
                entity.HasKey(e => e.StoreKeepingChildID)
                    .HasName("PK_StoreKeepingChild");

                entity.HasOne(d => d.StoreKeepingMaster)
                    .WithMany(p => p.StoreKeepingChild)
                    .HasForeignKey(d => d.StoreKeepingID);
            });

            modelBuilder.Entity<ReceiveWPMaster>(entity =>
            {
                entity.HasKey(e => e.ReceiveWPID)
                    .HasName("PK_ReceiveWPMaster");
            });

            modelBuilder.Entity<PackingMaster>(entity =>
            {
                entity.HasKey(e => e.PackingID)
                    .HasName("PK_PackingMaster");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.PackingDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PackingChild>(entity =>
            {
                entity.HasKey(e => e.PackingChildID)
                    .HasName("PK_PackingChild");

                entity.HasOne(d => d.PackingMaster)
                    .WithMany(p => p.PackingChild)
                    .HasForeignKey(d => d.PackingID);
            });

            modelBuilder.Entity<InvoiceMaster>(entity =>
            {
                entity.HasKey(e => e.InvoiceID)
                    .HasName("PK_InvoiceMaster");
                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
                entity.Property(e => e.CustomPIDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<InvoiceChild>(entity =>
            {
                entity.HasKey(e => e.InvoiceChildID)
                    .HasName("PK_InvoiceChild");

                entity.HasOne(d => d.InvoiceMaster)
                       .WithMany(p => p.InvoiceChild)
                       .HasForeignKey(d => d.InvoiceID);
            });

            modelBuilder.Entity<PIMaster>(entity =>
            {
                entity.HasKey(e => e.PIMasterId)
                    .HasName("PK_PIMaster");
                entity.Property(e => e.CustomPIDate).HasColumnType("datetime");
                entity.Property(e => e.PIDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PIChild>(entity =>
            {
                entity.HasKey(e => e.PIChildId)
                    .HasName("PK_PIChild");

                entity.HasOne(d => d.PIMaster)
                    .WithMany(p => p.PIChild)
                    .HasForeignKey(d => d.PIMasterId);
            });

            modelBuilder.Entity<PackingWeightDetails>(entity =>
            {
                entity.HasKey(e => e.PackingWeightID)
                    .HasName("PK_PackingWeightDetails");

                entity.HasOne(d => d.PackingMaster)
                    .WithMany(p => p.PackingWeightDetails)
                    .HasForeignKey(d => d.PackingID);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CardsFolder_Details>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("PK_CardsFolder_Details");
            });

            modelBuilder.Entity<SampleRoomDetails>(entity =>
            {
                entity.HasKey(e => e.SampleRoomId)
                    .HasName("PK_SampleRoomDetails");
                entity.Property(e => e.StoreDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatternRoomDetails>(entity =>
            {
                entity.HasKey(e => e.PatternRoomId)
                    .HasName("PK_PatternRoomDetails");

                entity.Property(e => e.PatternRoomId)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.FItemId)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.StoreQty)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.StoreDate).HasColumnType("datetime");

                entity.Property(e => e.RackNo)
                 .IsRequired()
                 .ValueGeneratedNever();

                entity.Property(e => e.ComponentQty)
                 .IsRequired()
                 .ValueGeneratedNever();
            });

            modelBuilder.Entity<SampleIssueDetails>(entity =>
            {
                entity.HasKey(e => e.SIID)
                    .HasName("PK_SampleIssueDetails");

                entity.Property(e => e.SIID)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.FItemId)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.Code)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.IssueNo)
                 .IsRequired()
                 .ValueGeneratedNever();

                entity.Property(e => e.IssueQty)
                 .IsRequired()
                 .ValueGeneratedNever();

                entity.Property(e => e.IssueTo)
                .IsRequired()
                .ValueGeneratedNever();
            });

            modelBuilder.Entity<PatternIssueDetails>(entity =>
            {
                entity.HasKey(e => e.PIID)
                    .HasName("PK_PatternIssueDetails");

                entity.Property(e => e.PIID)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.FItemId)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.Code)
                  .IsRequired()
                  .ValueGeneratedNever();

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.IssueNo)
                 .IsRequired()
                 .ValueGeneratedNever();

                entity.Property(e => e.IssueQty)
                 .IsRequired()
                 .ValueGeneratedNever();

                entity.Property(e => e.IssueTo)
                .IsRequired()
                .ValueGeneratedNever();
            });

            modelBuilder.Entity<Sample_Request>(entity =>
            {
                entity.HasKey(e => e.Request_Id)
                    .HasName("PK_Sample_Request");
                entity.Property(e => e.RequestDate).HasColumnType("datetime");
                entity.Property(e => e.ChageUpTo).HasColumnType("datetime");
                entity.Property(e => e.Change_On).HasColumnType("datetime");
                entity.Property(e => e.Change_Read_On).HasColumnType("datetime");
            });

            modelBuilder.Entity<StoreMasterDetails>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK_StoreMasterDetails");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });




            modelBuilder.Entity<MaritalStatusDetails>(entity =>
            {
                entity.HasKey(e => e.MaritalStatusId)
                      .HasName("PK_MaritalStatusDetails");
            });





            modelBuilder.Entity<FormNumberSettingsDetails>(entity =>
            {
                entity.HasKey(e => e.SNo).HasName("PK_FormNumberSettings");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductFile>(entity =>
            {
                entity.HasKey(e => e.ProductFileId).HasName("PK_ProductFile");
                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GRNFile>(entity =>
            {
                entity.HasKey(e => e.GRNFileId).HasName("PK_GRNFile");
                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SampleRM>(entity =>
            {
                entity.HasKey(e => e.SampleRMId)
                    .HasName("PK_SampleRM");

                entity.HasOne(d => d.ProductDetails)
                    .WithMany(p => p.SampleRM)
                    .HasForeignKey(d => d.FitemId);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId)
                    .HasName("PK_ProductType");
            });

            modelBuilder.Entity<ReserveStockMaster>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK_ReserveStockMaster");
                entity.Property(e => e.ReserveDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ZipperMaster>(entity =>
            {
                entity.HasKey(e => e.ZipperId)
                    .HasName("PK_ZipperMaster");
            });

            modelBuilder.Entity<SoleMaster>(entity =>
            {
                entity.HasKey(e => e.SoleId)
                    .HasName("PK_SoleMaster");
            });

            modelBuilder.Entity<UserRights>(entity =>
            {
                entity.HasKey(e => e.UserRightsId)
                .HasName("PK_UserRights");
            });

            modelBuilder.Entity<OrderShippingDetails>(entity =>
            {
                entity.HasKey(e => e.OrderShippingID)
                .HasName("PK_OrderShippingDetails");
            });

            modelBuilder.Entity<Move_RMStock>(entity =>
            {
                entity.HasKey(e => e.MoveRMStockID)
                .HasName("PK_Move_RMStock");
            });

            modelBuilder.Entity<AdjustmentRMDetails>(entity =>
            {
                entity.HasKey(e => e.AdjustmentRMStockID)
                .HasName("PK_AdjustmentRMDetails");
            });



            modelBuilder.Entity<PFDetails>(entity =>
            {
                entity.HasKey(e => e.PFID)
                .HasName("PK_PFDetails");
            });


            modelBuilder.Entity<SessionYear>(entity =>
            {
                entity.HasKey(e => e.SessionYearId)
                .HasName("PK_SessionYear");
            });
            modelBuilder.Entity<YearDetails>(entity =>
            {
                entity.HasKey(e => e.YearId)
                .HasName("PK_YearDetail");
            });
            modelBuilder.Entity<MultiLevelSubCategory>(entity =>
            {
                entity.HasKey(e => e.TableID);
            });
            modelBuilder.Entity<DisplayPosition>(entity =>
            {
                entity.HasKey(e => e.Position)
                .HasName("PK_DisplayPosition");
            });
            modelBuilder.Entity<UploadDetails>(entity =>
            {
                entity.HasKey(e => e.MYSQLServer_Name_IP);
            });

            modelBuilder.Entity<ProcessExecutionMaster>(entity =>
            {
                entity.HasKey(e => e.PRMasterId);
            });
            modelBuilder.Entity<ProcessExecutionChild>(entity =>
            {
                entity.HasKey(e => e.PRChildId);
                entity.HasOne(e => e.ProcessExecutionMaster).WithMany(e => e.ProcessExecutionChild);
            });

            modelBuilder.Entity<ReturnRMWP>(entity =>
            {
                entity.HasKey(e => e.ReturnRMWPID).HasName("PK_ReturnRMWP");
            });

            modelBuilder.Entity<ProcessRecvMaster>(entity =>
            {
                entity.HasKey(e => e.PRRecvId);
            });
            modelBuilder.Entity<ProcessRecvChild>(entity =>
            {
                entity.HasKey(e => e.PRRecvChildId);
                entity.HasOne(e => e.ProcessRecvMaster).WithMany(e => e.ProcessRecvChild);
            });

            modelBuilder.Entity<RMMovement>(entity =>
            {
                entity.HasKey(e => e.RitemID);
            });

            modelBuilder.Entity<ReturnToSupplier>(entity =>
            {
                entity.HasKey(e => e.RTSID);
            });

            modelBuilder.Entity<IssueRMforChangeMaster>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeID)
                    .HasName("PK_IssueRMforChangeMaster");
            });

            modelBuilder.Entity<IssueRMForChangeChild>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeChildID)
                    .HasName("PK_IssueRMForChangeChild");

                entity.HasOne(d => d.IssueRMforChangeMaster)
                    .WithMany(p => p.IssueRMForChangeChild)
                    .HasForeignKey(d => d.IssueRMChangeID);
            });

            modelBuilder.Entity<IssueRMforChangeItem>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeItemID);

                entity.HasOne(d => d.IssueRMforChangeMaster)
                    .WithMany(p => p.IssueRMforChangeItem)
                    .HasForeignKey(d => d.IssueRMChangeID);
            });
            modelBuilder.Entity<RecvJW>(entity =>
            {
                entity.HasKey(e => e.RecvJWID);
            });
            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);
            });

            modelBuilder.Entity<RMBrandDetails>(entity =>
            {
                entity.HasKey(e => e.RMBrandId)
                    .HasName("PK_RMBrandDetails");

                entity.Property(e => e.RMBrandId)
                    .IsRequired()
                    .ValueGeneratedNever();

                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ComponentGroupDetails>(entity =>
            {
                entity.HasKey(e => e.CompGroupId)
                    .HasName("PK_ComponentGroupDetails");
            });
            modelBuilder.Entity<ComponentDetails>(entity =>
            {
                entity.HasKey(e => e.CompId)
                    .HasName("PK_Compo_Details");

                entity.ToTable("ComponentDetails");

                entity.Property(e => e.CompId)
                    .HasColumnName("Comp_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompDescr)
                    .HasColumnName("Comp_Descr")
                    .HasMaxLength(100);

                entity.Property(e => e.CompName)
                    .IsRequired()
                    .HasColumnName("Comp_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionYear)
                    .HasColumnName("Session_year")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.CompGroupId).HasColumnName("CompGroupId");
            });
            modelBuilder.Entity<HSNDetails>(entity =>
            {
                entity.HasKey(e => e.HSNId);
            });

            modelBuilder.Entity<PrintDetails>(entity =>
            {
                entity.HasKey(e => e.PrintId)
                    .HasName("PK_PrintDetails");
            });

            modelBuilder.Entity<POPaymentTerms>(entity =>
            {
                entity.HasKey(e => e.PTId)
                    .HasName("PK_POPaymentTerms");
            });

            modelBuilder.Entity<PODeliveryTerms>(entity =>
            {
                entity.HasKey(e => e.DTId)
                    .HasName("PK_PODeliveryTerms");
            });

            modelBuilder.Entity<POTransportDetails>(entity =>
            {
                entity.HasKey(e => e.POTransportId)
                    .HasName("PK_POTransportDetails");
            });

            modelBuilder.Entity<ConstructionDetails>(entity =>
            {
                entity.HasKey(e => e.ConstrnId)
                    .HasName("PK_ConstructionDetails");

                entity.Property(e => e.ConstrnId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GenderDetails>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.Property(e => e.GenderId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EMPGenderDetails>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.Property(e => e.GenderId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<HeelDetails>(entity =>
            {
                entity.HasKey(e => e.HeelId)
                      .HasName("PK_HeelDetails");

                entity.Property(e => e.HeelId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BuyerConsigneeDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerConsigneeId)
                    .HasName("PK_BuyerConsigneeDetails");
            });

            modelBuilder.Entity<BuyerBankDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerBankId)
                    .HasName("PK_BuyerBankDetails");
            });

            modelBuilder.Entity<DocumentTypeDetails>(entity =>
            {
                entity.HasKey(e => e.DocumentTypeId)
                    .HasName("PK_DocumentTypeDetails");
            });

            modelBuilder.Entity<RMFile>(entity =>
            {
                entity.HasKey(e => e.RMFileId);
            });

            modelBuilder.Entity<BankDetails>(entity =>
            {
                entity.HasKey(e => e.BankId)
                      .HasName("PK_BankDetails");
            });
            modelBuilder.Entity<GradeDetails>(entity =>
            {
                entity.HasKey(e => e.GradeId)
                      .HasName("PK_GradeDetails");
            });
            modelBuilder.Entity<DesignationDetails>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                      .HasName("PK_DesignationDetails");
            });

            modelBuilder.Entity<GenderDetails>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                      .HasName("PK_GenderDetails");
            });

            modelBuilder.Entity<DepartmentDetails>(entity =>
            {
                entity.HasKey(e => e.DepartmentID)
                    .HasName("PK_Department");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.DepartmentID)
                    .IsRequired()
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<ShoesCartonMaster>(entity =>
            {
                entity.HasKey(e => e.ShoesCartonMasterId)
                    .HasName("PK_ShoesCartonMaster");
            });

            modelBuilder.Entity<ShoesCartonChild>(entity =>
            {
                entity.HasKey(e => e.ShoesCartonChildId)
                    .HasName("PK_ShoesCartonChild");

                entity.HasOne(d => d.ShoesCartonMaster)
                   .WithMany(p => p.ShoesCartonChild)
                   .HasForeignKey(d => d.ShoesCartonMasterId);
            });

            modelBuilder.Entity<ContractorPayment>(entity =>
            {
                entity.HasKey(e => e.ContractorPaymentId);
            });

            modelBuilder.Entity<ContractorPaymentChild>(entity =>
            {
                entity.HasKey(e => e.ContractorPaymentChildId);

                entity.HasOne(d => d.ContractorPayment)
                   .WithMany(p => p.ContractorPaymentChild)
                   .HasForeignKey(d => d.ContractorPaymentId);
            });

            modelBuilder.Entity<ContractorAdvanceDetails>(entity =>
            {
                entity.HasKey(e => e.ContractorAdvanceId);
            });

            modelBuilder.Entity<TempTableDetails>(entity =>
            {
                entity.HasKey(e => e.TableId);
            });

            modelBuilder.Entity<TempTableDetails>(entity =>
            {
                entity.HasKey(e => e.TableId);
            });

            modelBuilder.Entity<ProcessChargeDetails>(entity =>
            {
                entity.HasKey(e => e.ProcessChargeId);
            });

            modelBuilder.Entity<ProcessChargeHistoryDetails>(entity =>
            {
                entity.HasKey(e => e.ProcessChargeHistoryId);
            });

            modelBuilder.Entity<ProductProcessPictures>(entity =>
            {
                entity.HasKey(e => e.ProductProcessPicturesId);
            });


            modelBuilder.Entity<CostingMaster>(entity =>
            {
                entity.HasKey(e => e.CostingID)
                    .HasName("PK_Costing_Master");
            });

            modelBuilder.Entity<CostingRM>(entity =>
            {
                entity.HasKey(e => e.CostingRMID)
                    .HasName("PK_CostingRM");

                entity.HasOne(d => d.CostingMaster)
                   .WithMany(p => p.CostingRM)
                   .HasForeignKey(d => d.CostingID);
            });

            modelBuilder.Entity<CostingSizeRM>(entity =>
            {
                entity.HasKey(e => e.CostingSizeRMId);

                entity.HasOne(d => d.CostingMaster)
                   .WithMany(p => p.CostingSizeRM)
                   .HasForeignKey(d => d.CostingID);
            });

            modelBuilder.Entity<CostingElementDetails>(entity =>
            {
                entity.HasKey(e => e.CostingElementId)
                    .HasName("PK_CostingElementDetails");

                entity.Property(e => e.CostingElementId)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CostingEl>(entity =>
            {
                entity.HasKey(e => e.CostingElID)
                    .HasName("PK_CostingEl");

                entity.HasOne(d => d.CostingMaster)
                   .WithMany(p => p.CostingEl)
                   .HasForeignKey(d => d.CostingID);
            });

            modelBuilder.Entity<CostingCurrencyDetails>(entity =>
            {
                entity.HasKey(e => e.CostingCurrencyId)
                    .HasName("PK_CostingCurrencyDetails");

                entity.HasOne(d => d.CostingMaster)
                   .WithMany(p => p.CostingCurrencyDetails)
                   .HasForeignKey(d => d.CostingId);
            });

            modelBuilder.Entity<CostingRMComponent>(entity =>
            {
                entity.HasKey(e => e.CostingRMComponentID)
                    .HasName("PK_CostingRMComponent");

                entity.HasOne(d => d.CostingMaster)
                   .WithMany(p => p.CostingRMComponent)
                   .HasForeignKey(d => d.CostingID);
            });

            modelBuilder.Entity<TempCostigIDsForUpdateCosting>(entity =>
            {
                entity.HasKey(e => e.TempTableID);
            });

            modelBuilder.Entity<CompanyBeneficiaryBankDetails>(entity =>
            {
                entity.HasKey(e => e.BeneficiaryBankID);
            });

            modelBuilder.Entity<CompanyIntermediaryBankDetails>(entity =>
            {
                entity.HasKey(e => e.IntermediaryBankID);
            });

            modelBuilder.Entity<BuyerCartonStickerTemplate>(entity =>
            {
                entity.HasKey(e => e.BuyerCartonStickerTemplateID);
            });

            modelBuilder.Entity<RTSGRNMaster>(entity =>
            {
                entity.HasKey(e => e.RTSGRNID);
            });

            modelBuilder.Entity<RTSGRNChild>(entity =>
            {
                entity.HasKey(e => e.RTSGRNChildID);
            });



            modelBuilder.Entity<RMProperties>(entity =>
            {
                entity.HasKey(e => e.RMPropertiesID)
                      .HasName("PK_RMProperties");
            });

            modelBuilder.Entity<RMPropertiesValue>(entity =>
            {
                entity.HasKey(e => e.RMPropertiesValueID)
                      .HasName("PK_RMPropertiesValue");
            });

            modelBuilder.Entity<RMPropertiesMapping>(entity =>
            {
                entity.HasKey(e => e.RMMappingID)
                     .HasName("PK_RMPropertiesMapping");
            });

            modelBuilder.Entity<RMChild>(entity =>
            {
                entity.HasKey(e => e.RMChildId)
                    .HasName("PK_RMChild");

                entity.Property(e => e.RMChildId).ValueGeneratedNever();

                entity.HasOne(d => d.RitemMaster)
                    .WithMany(p => p.RMChild)
                    .HasForeignKey(d => d.RItem_Id);
            });

            modelBuilder.Entity<RItemQuickTest>(entity =>
            {
                entity.HasKey(e => e.RItemQuickTestId)
                    .HasName("PK_RItemQuickTest");

                entity.Property(e => e.RItemQuickTestId).ValueGeneratedNever();

                entity.HasOne(d => d.RitemMaster)
                    .WithMany(p => p.RItemQuickTest)
                    .HasForeignKey(d => d.RItem_Id);
            });

            modelBuilder.Entity<ProductComponent>(entity =>
            {
                entity.HasKey(e => e.ProductComponentId);
            });

            modelBuilder.Entity<ProductProcessCharge>(entity =>
            {
                entity.HasKey(e => e.ProductProcessChargeId);
            });

            modelBuilder.Entity<ProductQCPointMaster>(entity =>
            {
                entity.HasKey(e => e.QCPointID);
            });

            modelBuilder.Entity<ProductQC>(entity =>
            {
                entity.HasKey(e => e.QCID);
            });

            modelBuilder.Entity<RMSupplierPriceHistory>(entity =>
            {
                entity.HasKey(e => e.RMPriceHistoryId);
            });


            modelBuilder.Entity<RMComboMaster>(entity =>
            {
                entity.HasKey(e => e.RMComboID);
            });

            modelBuilder.Entity<RMComboChild>(entity =>
            {
                entity.HasKey(e => e.RMComboChildID);
            });

            modelBuilder.Entity<UserAsSupervisorDetails>(entity =>
            {
                entity.HasKey(e => e.UserAsSupervisorId);
            });

            #region EntitiesDBContext
            modelBuilder.Entity<InvoiceSettingDetails>(entity =>
            {
                entity.HasKey(e => e.InvoiceSettingID)
                    .HasName("PK_InvoiceSettingDetails");
            });
            modelBuilder.Entity<OrderSheetClose>(entity =>
            {
                entity.HasKey(e => e.OrderSheetCloseId);
            });
            #endregion

            #region StoreRMRequest

            modelBuilder.Entity<StockTransferRequest>(entity =>
            {
                entity.HasKey(e => e.StockTransferRequestId)
                    .HasName("PK_StoreRMRequestMaster");
            });

            modelBuilder.Entity<StockTransferRequestChild>(entity =>
            {
                entity.HasKey(e => e.StockTransferRequestChildId)
                    .HasName("PK_StoreRMRequestChild");

                entity.HasOne(d => d.StockTransferRequest)
                    .WithMany(p => p.StockTransferRequestChild)
                    .HasForeignKey(d => d.StockTransferRequestId);
            });

            modelBuilder.Entity<StockTransferIssue>(entity =>
            {
                entity.HasKey(e => e.StockTransferIssueId);
            });
            modelBuilder.Entity<StockTransferIssueChild>(entity =>
            {
                entity.HasKey(e => e.StockTransferIssueChildId);
            });

            #endregion

            #region HR
            //HR Madule
            modelBuilder.Entity<HR_GradeDetails>(entity =>
                {
                    entity.HasKey(e => e.GradeId)
                          .HasName("PK_HR_GradeDetails");
                });

            modelBuilder.Entity<HR_TitleDetails>(entity =>
            {
                entity.HasKey(e => e.TitleId)
                      .HasName("PK_TitleIdDetails");
            });

            modelBuilder.Entity<HR_DesignationDetails>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                      .HasName("PK_HR_DesignationDetails");
            });

            modelBuilder.Entity<HR_GenderDetails>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                      .HasName("PK_HR_GenderDetails");
            });

            modelBuilder.Entity<HR_MaritalStatusDetails>(entity =>
            {
                entity.HasKey(e => e.MaritalStatusId)
                      .HasName("PK_MaritalStatusDetails");
            });

            modelBuilder.Entity<HR_EmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                      .HasName("PK_EmployeeDetails");
                entity.Property(e => e.DateOfJoin).HasColumnType("datetime");
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<HR_EmployeeFile>(entity =>
            {
                entity.HasKey(e => e.EmployeeFileId).HasName("PK_EmployeeFile");
                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<HR_BankDetails>(entity =>
            {
                entity.HasKey(e => e.BankId)
                      .HasName("PK_HR_BankDetails");
            });

            modelBuilder.Entity<HR_AttandanceDetails>(entity =>
            {
                entity.HasKey(e => e.AttandanceID);
            });

            modelBuilder.Entity<HR_EmployeeType>(entity =>
            {
                entity.HasKey(e => e.EmployeeTypeId).HasName("PK_EmployeeType");
            });
            modelBuilder.Entity<HR_AdvanceDetails>(entity =>
            {
                entity.HasKey(e => e.AdvanceId).HasName("PK_AdvanceDetails");
            });
            modelBuilder.Entity<HR_AttandanceStatus>(entity =>
            {
                entity.HasKey(e => e.AttandanceStatusId);
            });
            modelBuilder.Entity<HR_AllowanceDetails>(entity =>
            {
                entity.HasKey(e => e.AllowanceId);
            });

            modelBuilder.Entity<HR_SalaryDetailsMaster>(entity =>
            {
                entity.HasKey(e => e.SalId).HasName("PK_Salary_Details_Master");
            });

            modelBuilder.Entity<HR_SalaryStructureMaster>(entity =>
            {
                entity.HasKey(e => e.SalStrId);
            });

            modelBuilder.Entity<HR_SalaryStructureChild>(entity =>
            {
                entity.HasKey(e => e.SalStrChildId);
            });
            modelBuilder.Entity<HR_PFDetails>(entity =>
            {
                entity.HasKey(e => e.PFID);
            });
            modelBuilder.Entity<HR_ESIDetails>(entity =>
            {
                entity.HasKey(e => e.ESIID);
            });
            modelBuilder.Entity<HR_HolidayMaster>(entity =>
            {
                entity.HasKey(e => e.HolidayId);
            });
            modelBuilder.Entity<HR_DepartmentDetails>(entity =>
            {
                entity.HasKey(e => e.DepartmentID)
                    .HasName("PK_HR_Department");
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
                entity.Property(e => e.DepartmentID)
                    .IsRequired()
                    .ValueGeneratedNever();
            });
            #endregion
            #endregion

            #region Views
            modelBuilder.Entity<ViewShowRMSizeCosting>(entity =>
            {
                entity.HasKey(e => e.CostingSizeRMId);

            });
            modelBuilder.Entity<ViewOrderItemsForPacking>(entity =>
            {
                entity.HasKey(e => e.OrderChildID)
                    .HasName("PK_SaleOrderChild");
            });

            modelBuilder.Entity<ViewPackingMaster>(entity =>
            {
                entity.HasKey(e => e.PackingID)
                    .HasName("PK_PackingMaster");
            });

            modelBuilder.Entity<ViewPackingChild>(entity =>
            {
                entity.HasKey(e => e.PackingChildID)
                    .HasName("PK_PackingChild");
            });

            modelBuilder.Entity<ViewPackingCartons>(entity =>
            {
                entity.HasKey(e => e.RowID);
            });

            modelBuilder.Entity<ViewReceiveWP>(entity =>
            {
                entity.HasKey(e => e.ReceiveWPID)
                    .HasName("PK_ReceiveWPMaster");
            });

            modelBuilder.Entity<ViewGetRack>(entity =>
            {
                entity.HasKey(e => e.OpeningStockID)
                    .HasName("PK_OpeningStockDetails");
            });

            modelBuilder.Entity<ViewRMStock>(entity =>
            {
                entity.HasKey(e => e.RItem_Id)
                    .HasName("PK_RItem_Master");
            });

            modelBuilder.Entity<ViewRMStockRackWise>(entity =>
            {
                //entity.HasKey(e => e.RowID).HasName("PK_RackMaster");
                entity.HasKey(e => new { e.RItem_Id, e.SupplierId, e.RackId, e.LotNo });
            });

            modelBuilder.Entity<ViewPOForGRN>(entity =>
            {
                entity.HasKey(e => e.POChildID).HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewGRNMaster>(entity =>
            {
                entity.HasKey(e => e.GRNID).HasName("PK_GRNMaster");
            });

            modelBuilder.Entity<ViewGRNChild>(entity =>
            {
                entity.HasKey(e => e.GRNChildID).HasName("PK_GRNChild");
            });

            modelBuilder.Entity<ViewGRN>(entity =>
            {
                entity.HasKey(e => e.GRNChildID).HasName("PK_GRNChild");
            });

            modelBuilder.Entity<ViewGRNForStoreKeeping>(entity =>
            {
                entity.HasKey(e => e.GRNChildID);
            });

            modelBuilder.Entity<ViewIssueRMWP>(entity =>
            {
                entity.HasKey(e => e.IssueChildID)
                    .HasName("PK_IssueChild");
            });

            modelBuilder.Entity<ViewWorkPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK_WorkPlanMaster");
            });

            modelBuilder.Entity<ViewWorkPlanChild>(entity =>
            {
                entity.HasKey(e => e.PlanChildId)
                    .HasName("PK_WorkPlanChild");
            });

            modelBuilder.Entity<ViewWorkPlanBOM>(entity =>
            {
                entity.HasKey(e => new { e.CostingRMID, e.ProductSizeId });
                //entity.HasKey(e => e.CostingRMID)
                //    .HasName("PK_CostingRM");
            });

            modelBuilder.Entity<ViewContractorDetails>(entity =>
            {
                entity.HasKey(e => e.ContractorID).HasName("PK_ContractorDetails");
            });

            modelBuilder.Entity<ViewOpStockDetails>(entity =>
            {
                entity.HasKey(e => e.OpeningStockID).HasName("PK_OpeningStockDetails");
            });

            modelBuilder.Entity<ViewItemBOM>(entity =>
            {
                entity.HasKey(e => e.CostingRMID).HasName("PK_CostingRM");
            });

            modelBuilder.Entity<ViewSendEmail>(entity =>
            {
                entity.HasKey(e => e.EmailSendID)
                    .HasName("PK_SendEmailTable");
            });

            modelBuilder.Entity<ViewEmailSettings>(entity =>
            {
                entity.HasKey(e => e.EmailSettingsID).HasName("PK_EmailSettings");
            });

            //modelBuilder.Entity<ViewOrderBOM>(entity =>
            //{
            //    entity.HasKey(e => e.RowID).HasName("PK_RItem_Master");
            //});

            //modelBuilder.Entity<ViewOrderBOMSUM>(entity =>
            //{
            //    entity.HasKey(e => e.RowID).HasName("PK_RItem_Master");
            //});
            modelBuilder.Entity<ViewOrderBOM>(entity =>
            {
                entity.HasKey(e => new { e.OrderMasterID, e.FitemId, e.ProductSizeId, e.RItemID });
            });

            modelBuilder.Entity<ViewOrderBOMSUM>(entity =>
            {
                entity.HasKey(e => new { e.OrderMasterID, e.RItemID });
            });

            modelBuilder.Entity<ViewRMSupplier>(entity =>
            {
                entity.HasKey(e => e.RItemSuppID)
                    .HasName("PK_RItemSupp");
            });

            modelBuilder.Entity<ViewPOPrint>(entity =>
            {
                entity.HasKey(e => e.POChildID).HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewSaleOrder>(entity =>
            {
                entity.HasKey(e => e.OrderMasterID).HasName("PK_SaleOrderMaster");
                entity.Property(e => e.OrderNo);
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.OrderDate);
                entity.Property(e => e.DeliveryDate);
                entity.Property(e => e.Remark);
                entity.Property(e => e.TotalQty);
                entity.Property(e => e.TotalAmount);
                entity.Property(e => e.CancelStatus);
                entity.Property(e => e.BuyerName);
                entity.Property(e => e.BuyerCode);
            });

            modelBuilder.Entity<ViewSaleOrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderChildID).HasName("PK_SaleOrderChild");
                entity.Property(e => e.OrderMasterID);

                entity.Property(e => e.FitemId);
                entity.Property(e => e.Name);
                entity.Property(e => e.Code);
                entity.Property(e => e.Description);
                entity.Property(e => e.ProductSubCategoryID);
                entity.Property(e => e.ProductSubCategoryName);
                entity.Property(e => e.ProductCategoryID);
                entity.Property(e => e.Qty);
                entity.Property(e => e.Size);
                entity.Property(e => e.Price);
                entity.Property(e => e.Amount);
                entity.Property(e => e.CLName);
            });

            modelBuilder.Entity<ViewShowCosting>(entity =>
            {
                entity.HasKey(e => e.CostingID).HasName("PK_Costing_Master");
            });

            modelBuilder.Entity<ViewCostingRM>(entity =>
            {
                entity.HasKey(e => e.CostingRMID).HasName("PK_CostingRM");
            });

            modelBuilder.Entity<ViewCostingEL>(entity =>
            {
                entity.HasKey(e => e.CostingElID).HasName("PK_CostingEl");
            });

            modelBuilder.Entity<ViewUserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserDetails");
            });

            modelBuilder.Entity<ViewProductProcessDetails>(entity =>
            {
                entity.HasKey(e => e.ProductProcessID);
            });

            modelBuilder.Entity<ViewProductShow>(entity =>
            {
                entity.HasKey(e => e.FitemId).HasName("PK_ProductDetails");
            });

            modelBuilder.Entity<ViewBrandDetails>(entity =>
            {
                entity.HasKey(e => e.BrandId).HasName("PK_BrandDetails");
            });

            modelBuilder.Entity<ViewIssuedByDetails>(entity =>
            {
                entity.HasKey(e => e.IssuedById).HasName("PK_IssuedByDetails");
            });

            modelBuilder.Entity<ViewLabelDetails>(entity =>
            {
                entity.HasKey(e => e.LabelId).HasName("PK_LabelDetails");
            });

            modelBuilder.Entity<ViewRackMaster>(entity =>
            {
                entity.HasKey(e => e.RackId).HasName("PK_RackMaster");
            });

            modelBuilder.Entity<ViewRMSubCategory>(entity =>
            {
                entity.HasKey(e => e.SubCategoryID).HasName("PK_RMSubCategory");
            });

            modelBuilder.Entity<ViewStateDetails>(entity =>
            {
                entity.HasKey(e => e.StateId).HasName("PK_State_Details");
            });

            modelBuilder.Entity<ViewCityDetails>(entity =>
            {
                entity.HasKey(e => e.CityId).HasName("PK_City_Details");
            });

            modelBuilder.Entity<ViewSupplierDetails>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK_SupplierDetails");
            });

            modelBuilder.Entity<ViewBuyerDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerId).HasName("PK_BuyerDetails");
            });

            modelBuilder.Entity<ViewProduct>(entity =>
            {
                entity.HasKey(e => e.ProductSubCategoryID).HasName("PK_ProductSubCategory");
                entity.Property(e => e.ProductSubCategoryID).HasColumnName("ProductSubCategoryID");
                entity.Property(e => e.ProductCategoryID).HasColumnName("ProductCategoryID");
                entity.Property(e => e.ProductCategoryName).HasColumnName("ProductCategoryName");
                entity.Property(e => e.ProductShortCode).HasColumnName("ProductShortCode");
                entity.Property(e => e.ProductSubCategoryName).HasColumnName("ProductSubCategoryName");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.ProductStartWith).HasColumnName("ProductStartWith");

            });

            modelBuilder.Entity<ViewSticker>(entity =>
            {
                entity.HasKey(e => e.StickerID).HasName("PK_StickerDetials");
                entity.Property(e => e.StickerID).HasColumnName("StickerID");
                entity.Property(e => e.BuyerId).HasColumnName("BuyerId");
                entity.Property(e => e.BuyerName).HasColumnName("BuyerName");
                entity.Property(e => e.BuyerCode).HasColumnName("BuyerCode");
                entity.Property(e => e.StickerName).HasColumnName("StickerName");
                entity.Property(e => e.Description).HasColumnName("Description");

            });

            modelBuilder.Entity<ViewCarelabel>(entity =>
            {
                entity.HasKey(e => e.CareLabelID).HasName("PK_CareLabelDetails");
                entity.Property(e => e.CareLabelID).HasColumnName("CareLabelID");
                entity.Property(e => e.BuyerId).HasColumnName("BuyerId");
                entity.Property(e => e.BuyerName).HasColumnName("BuyerName");
                entity.Property(e => e.BuyerCode).HasColumnName("BuyerCode");
                entity.Property(e => e.CareLabelLName).HasColumnName("CareLabelLName");
                entity.Property(e => e.Description).HasColumnName("Description");

            });

            modelBuilder.Entity<ViewHandTag>(entity =>
            {
                entity.HasKey(e => e.HandTagID).HasName("PK_HandTagDetails");
                entity.Property(e => e.HandTagID);
                entity.Property(e => e.BuyerId);
                entity.Property(e => e.BuyerName);
                entity.Property(e => e.BuyerCode);
                entity.Property(e => e.HandTagName);
                entity.Property(e => e.Description);
                entity.Property(e => e.SessionYear);
                entity.Property(e => e.EntryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ViewRMSubCategory>(entity =>
            {
                entity.HasKey(e => e.SubCategoryID).HasName("PK_RMSubCategory");
                entity.Property(e => e.SubCategoryID).HasColumnName("SubCategoryID");
                entity.Property(e => e.SubCategoryName).HasColumnName("SubCategoryName");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasColumnName("CategoryName");
            });

            modelBuilder.Entity<ViewMenu>(entity =>
            {
                entity.HasKey(e => e.MenuID).HasName("PK_MenuDetails");
                entity.Property(e => e.MenuID);
                entity.Property(e => e.MenuCategoryID);
                entity.Property(e => e.MenuCategoryName);
                entity.Property(e => e.MenuName);
                entity.Property(e => e.MenuURL);
            });

            modelBuilder.Entity<ViewSampleRoom>(entity =>
            {
                entity.HasKey(e => e.SampleRoomId)
                    .HasName("PK_SampleRoomDetails");
            });

            modelBuilder.Entity<ViewInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceID)
                    .HasName("PK_Invoice");
            });

            modelBuilder.Entity<ViewPI>(entity =>
            {
                entity.HasKey(e => e.PIMasterId)
                    .HasName("PK_PIMaster");
            });

            modelBuilder.Entity<ViewCurrencyHistory>(entity =>
            {
                entity.HasKey(e => e.CurrencyHistoryId)
                     .HasName("PK_CurrencyHistoryDetails");
                entity.Property(e => e.Cname);
                entity.Property(e => e.Csname);
                entity.Property(e => e.ChangeDate);
                entity.Property(e => e.PreviousPrice);
                entity.Property(e => e.ChangePrice);
                entity.Property(e => e.Cid);
            });

            modelBuilder.Entity<ViewPackingWeight>(entity =>
            {
                entity.HasKey(e => e.RowID);
            });

            modelBuilder.Entity<ViewInvoiceChild>(entity =>
            {
                entity.HasKey(e => e.InvoiceChildID)
                    .HasName("PK_InvoiceChild");
            });

            modelBuilder.Entity<ViewInvoicePrint>(entity =>
            {
                entity.HasKey(e => e.InvoiceChildID)
                    .HasName("PK_InvoiceChild");
            });

            modelBuilder.Entity<ViewPIChild>(entity =>
            {
                entity.HasKey(e => e.PIChildId)
                    .HasName("PK_PIChild");
            });

            modelBuilder.Entity<ViewPIPrint>(entity =>
            {
                entity.HasKey(e => e.PIChildId)
                    .HasName("PK_PIChild");
            });

            modelBuilder.Entity<ViewPatternRoomDetails>(entity =>
            {
                entity.HasKey(e => e.PatternRoomId)
                    .HasName("PK_PatternRoomDetails");
            });

            modelBuilder.Entity<ViewSampleIssue>(entity =>
            {
                entity.HasKey(e => e.SIID)
                    .HasName("PK_SampleIssueDetails");
            });

            modelBuilder.Entity<ViewPatternIssueDetails>(entity =>
            {
                entity.HasKey(e => e.PIID)
                    .HasName("PK_SampleIssueDetails");
            });

            modelBuilder.Entity<ViewSample_Request>(entity =>
            {
                entity.HasKey(e => e.Request_Id)
                    .HasName("PK_Sample_Request");
            });

            modelBuilder.Entity<ViewEmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                      .HasName("PK_EmployeeDetails");
            });

            modelBuilder.Entity<ViewBelowMinimumLevel>(entity =>
            {
                entity.HasKey(e => e.RItem_Id)
                    .HasName("PK_RItem_Master");
            });

            modelBuilder.Entity<ViewProductWithoutPhoto>(entity =>
            {
                entity.HasKey(e => e.FitemId).HasName("PK_ProductDetails");
            });

            modelBuilder.Entity<ViewPODelay>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewPODelaySum>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewUnusedRMList>(entity =>
            {
                entity.HasKey(e => e.RItem_Id).HasName("PK_RItem_Master");
            });

            modelBuilder.Entity<VisitorRegistorDetails>(entity =>
            {
                entity.HasKey(e => e.VisitorRegistorId).HasName("PK_VisitorRegistorDetails");
            });

            modelBuilder.Entity<ViewCostingPendingReport>(entity =>
            {
                entity.HasKey(e => e.FitemId).HasName("PK_Costing_Master");
            });

            modelBuilder.Entity<ViewPOBalance>(entity =>
            {
                entity.HasKey(e => e.POChildID)
                    .HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewSampleRM>(entity =>
            {
                entity.HasKey(e => e.SampleRMId)
                    .HasName("PK_SampleRM");
            });

            modelBuilder.Entity<ViewCostingCurrencyDetails>(entity =>
            {
                entity.HasKey(e => e.RowID);
            });

            modelBuilder.Entity<ViewCostingCurrencyEdit>(entity =>
            {
                entity.HasKey(e => e.CostingCurrencyId);
            });

            modelBuilder.Entity<ViewDieFitemDetails>(entity =>
            {
                entity.HasKey(e => e.DieFitemId);
            });

            modelBuilder.Entity<ViewReserveStock>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK_ReserveStockMaster");
            });

            modelBuilder.Entity<ViewPOMaster>(entity =>
            {
                entity.HasKey(e => e.Poid).HasName("PK_POMaster");
            });

            modelBuilder.Entity<ViewPOPrint>(entity =>
            {
                entity.HasKey(e => e.POChildID).HasName("PK_POChild");
            });

            modelBuilder.Entity<ViewPOChild>(entity =>
            {
                entity.HasKey(e => e.POChildID).HasName("PK_POChild");
                entity.Property(e => e.POID);
                entity.Property(e => e.SNo);
                entity.Property(e => e.RItem_Id);
                entity.Property(e => e.Qty);
                entity.Property(e => e.Rate);
                entity.Property(e => e.Amount);
                entity.Property(e => e.Unit);
                entity.Property(e => e.RDesc);
                entity.Property(e => e.RMName);
                entity.Property(e => e.RMCode);
                entity.Property(e => e.SubCategoryName);
                entity.Property(e => e.CategoryName);
            });

            modelBuilder.Entity<ViewRItemShow>(entity =>
            {
                entity.HasKey(e => e.RitemId).HasName("PK_RItem_Master");
                entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasColumnName("CategoryName");
                entity.Property(e => e.SubCategoryID).HasColumnName("SubCategoryID");
                entity.Property(e => e.SubCategoryName).HasColumnName("SubCategoryName");
                entity.Property(e => e.RitemId).HasColumnName("RItem_Id");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Code).HasColumnName("Code");
                entity.Property(e => e.Title).HasColumnName("Title");
                entity.Property(e => e.UserId).HasColumnName("UserID");
                //entity.Property(e => e.UserTypeId);
                //entity.Property(e => e.UserName);
                //entity.Property(e => e.Finish);
                //entity.Property(e => e.Thickness);
                //entity.Property(e => e.SizeName);
            });


            modelBuilder.Entity<ViewUserRights>(entity =>
            {
                entity.HasKey(e => e.UserRightsId).HasName("PK_UserRights");
            });

            modelBuilder.Entity<ViewSupplierPerformance>(entity =>
            {
                entity.HasKey(e => e.POID).HasName("PK_POMaster");
            });

            modelBuilder.Entity<ViewSupplierPerformanceSumarray>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK_POMaster");
            });

            modelBuilder.Entity<ViewMoveRMStock>(entity =>
            {
                entity.HasKey(e => e.MoveRMStockID).HasName("PK_Move_RMStock");
            });

            modelBuilder.Entity<ViewAdjustmentRM>(entity =>
            {
                entity.HasKey(e => e.AdjustmentRMStockID).HasName("PK_DamageRMStock");
            });

            modelBuilder.Entity<ViewSalaryStructure>(entity =>
            {
                entity.HasKey(e => e.SalStrChildId);
            });
            modelBuilder.Entity<ViewProductSize>(entity =>
            {
                entity.HasKey(e => e.ProductMultipleSizeId);
            });
            modelBuilder.Entity<ViewProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryID);
            });
            modelBuilder.Entity<ViewReturnRMWP>(entity =>
            {
                entity.HasKey(e => e.ReturnRMWPID);
            });
            modelBuilder.Entity<ViewProcessExecution>(entity =>
            {
                entity.HasKey(e => e.PRChildId);
            });
            modelBuilder.Entity<ViewProcessRecv>(entity =>
            {
                entity.HasKey(e => e.PRRecvChildId);
            });


            modelBuilder.Entity<RequestRM>(entity =>
            {
                entity.HasKey(e => e.ReqRMId);
            });
            modelBuilder.Entity<RequestRMChild>(entity =>
            {
                entity.HasKey(e => e.ReqRMChildId);
            });
            modelBuilder.Entity<ViewReqRMList>(entity =>
            {
                entity.HasKey(e => e.ReqRMChildID);
            });
            modelBuilder.Entity<ViewRequestRM>(entity =>
            {
                entity.HasKey(e => e.ReqRMChildID);
            });
            modelBuilder.Entity<RequestRMIssue>(entity =>
            {
                entity.HasKey(e => e.ReqRMIssueId);
            });
            modelBuilder.Entity<RequestRMIssueChild>(entity =>
            {
                entity.HasKey(e => e.ReqRMIssueChildId);
            });
            modelBuilder.Entity<ViewRequestRMIssue>(entity =>
            {
                entity.HasKey(e => e.ReqRMIssueChildID);
            });
            modelBuilder.Entity<ReturnRM>(entity =>
            {
                entity.HasKey(e => e.ReturnRMID);
            });
            modelBuilder.Entity<ReturnRMChild>(entity =>
            {
                entity.HasKey(e => e.ReturnRMChildID);
            });
            modelBuilder.Entity<ViewRetRMList>(entity =>
            {
                entity.HasKey(e => e.ReturnRMChildID);
            });
            modelBuilder.Entity<ReturnRMRecv>(entity =>
            {
                entity.HasKey(e => e.ReturnRMRecvID);
            });
            modelBuilder.Entity<ReturnRMRecvChild>(entity =>
            {
                entity.HasKey(e => e.ReturnRMRecvChildID);
            });
            modelBuilder.Entity<ViewReturnRMRecv>(entity =>
            {
                entity.HasKey(e => e.ReturnRMRecvChildID);
            });
            modelBuilder.Entity<ViewStoreKeeping>(entity =>
            {
                entity.HasKey(e => e.StoreKeepingChildID);
            });

            modelBuilder.Entity<ViewWIP>(entity =>
            {
                entity.HasKey(e => new { e.PlanChildId, e.ProcessID });
            });

            modelBuilder.Entity<ViewCostingRMComponent>(entity =>
            {
                entity.HasKey(e => e.CostingRMComponentID);
            });

            modelBuilder.Entity<ViewCostingRMPrint>(entity =>
            {
                //entity.HasKey(e => e.CostingRMID).HasName("PK_CostingRM"); //ViewCostingRMPrintOld
                entity.HasKey(e => e.CostingRMComponentID).HasName("PK_CostingRMComponent");
            });

            modelBuilder.Entity<ViewRTS>(entity =>
            {
                entity.HasKey(e => e.RTSID);
            });

            modelBuilder.Entity<ViewIssueRMForChange>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeChildID)
                    .HasName("PK_IssueRMForChangeChild");
            });

            modelBuilder.Entity<ViewIssueRMForChangeMaster>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeItemID);
            });

            modelBuilder.Entity<ViewComponentDetails>(entity =>
            {
                entity.HasKey(e => e.Comp_Id);
            });
            modelBuilder.Entity<ViewHSNDetails>(entity =>
            {
                entity.HasKey(e => e.HSNId);
            });

            modelBuilder.Entity<ViewBranchDetails>(entity =>
            {
                entity.HasKey(e => e.BranchID)
                        .HasName("PK_BranchDetails");
            });

            modelBuilder.Entity<ViewRMLotRate>(entity =>
            {
                entity.HasKey(e => e.StoreKeepingChildID)
                    .HasName("PK_StoreKeepingChild");
            });

            modelBuilder.Entity<BuyerBankDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerBankId)
                    .HasName("PK_BuyerBankDetails");
            });
            modelBuilder.Entity<BuyerConsigneeDetails>(entity =>
            {
                entity.HasKey(e => e.BuyerConsigneeId)
                    .HasName("PK_BuyerConsigneeDetails");

                entity.HasOne(d => d.BuyerDetails)
                    .WithMany(p => p.BuyerConsigneeDetails)
                    .HasForeignKey(d => d.BuyerId);
            });

            modelBuilder.Entity<ViewProductFile>(entity =>
            {
                entity.HasKey(e => e.ProductFileId);
            });
            modelBuilder.Entity<ViewRMFile>(entity =>
            {
                entity.HasKey(e => e.RMFileId);
            });

            modelBuilder.Entity<ViewGRNFile>(entity =>
            {
                entity.HasKey(e => e.GRNFileId);
            });

            modelBuilder.Entity<ViewBuyerBank>(entity =>
            {
                entity.HasKey(e => e.BuyerBankId);
            });

            modelBuilder.Entity<ViewBuyerConsignee>(entity =>
            {
                entity.HasKey(e => e.BuyerConsigneeId);
            });
            modelBuilder.Entity<SupplierBankDetails>(entity =>
            {
                entity.HasKey(e => e.SupplierBankId);
            });

            modelBuilder.Entity<ViewSupplierBank>(entity =>
            {
                entity.HasKey(e => e.SupplierBankId);
            });

            modelBuilder.Entity<ViewShoesPacking>(entity =>
            {
                entity.HasKey(e => e.PackingChildID);
            });

            modelBuilder.Entity<ViewSaleOrderChild>(entity =>
            {
                entity.HasKey(e => e.RowID);
            });

            modelBuilder.Entity<ViewShoesCartonChild>(entity =>
            {
                entity.HasKey(e => e.ShoesCartonChildId);
            });

            modelBuilder.Entity<ViewShoesPackingChild>(entity =>
            {
                entity.HasKey(e => e.PackingChildID);
            });

            modelBuilder.Entity<ViewProcessExecutionMaster>(entity =>
            {
                entity.HasKey(e => e.PRMasterId);
            });

            modelBuilder.Entity<ViewProcessRecvMaster>(entity =>
            {
                entity.HasKey(e => e.PRRecvId);
            });

            modelBuilder.Entity<ViewContractorPayment>(entity =>
            {
                entity.HasKey(e => e.ContractorPaymentChildId);
            });

            modelBuilder.Entity<ViewContractorAdvanceDetails>(entity =>
            {
                entity.HasKey(e => e.ContractorAdvanceId);
            });

            modelBuilder.Entity<ViewContractorBalance>(entity =>
            {
                entity.HasKey(e => e.ContractorID);
            });

            modelBuilder.Entity<ViewProductListForProcessCharge>(entity =>
            {
                entity.HasKey(e => e.ProductMultipleSizeId);
            });

            modelBuilder.Entity<ViewProductProcessPictures>(entity =>
            {
                entity.HasKey(e => e.ProductProcessPicturesId);
            });

            modelBuilder.Entity<ViewDashBoardBuyerLastOrder>(entity =>
            {
                entity.HasKey(e => e.OrderMasterID);
            });

            modelBuilder.Entity<ViewDashBoardOrderToBeDeliver>(entity =>
            {
                entity.HasKey(e => e.OrderMasterID);
            });

            modelBuilder.Entity<ViewWPQtyForNextProcess>(entity =>
            {
                entity.HasKey(e => new { e.PlanChildId, e.ProcessID });
            });

            modelBuilder.Entity<ViewRMLedger>(entity =>
            {
                entity.HasKey(e => new { e.RecordType, e.RecordChildId });
            });

            modelBuilder.Entity<ViewProductionPacking>(entity =>
           {
               entity.HasKey(e => new { e.PackingID, e.OrderMasterID, e.FitemId, e.ProductSizeId });
           });

            modelBuilder.Entity<ViewCostingSizeRM>(entity =>
            {
                entity.HasKey(e => new { e.CostingSizeRMId });
            });

            modelBuilder.Entity<ViewCompanyBeneficiaryBankDetails>(entity =>
            {
                entity.HasKey(e => new { e.BeneficiaryBankID });
            });

            modelBuilder.Entity<ViewCompanyIntermediaryBankDetails>(entity =>
            {
                entity.HasKey(e => new { e.IntermediaryBankID });
            });

            modelBuilder.Entity<ViewRTSGRN>(entity =>
            {
                entity.HasKey(e => new { e.RTSGRNChildID });
            });

            modelBuilder.Entity<ViewRMPropertiesDetails>(entity =>
            {
                entity.HasKey(e => new { e.RMPropertiesValueID });
            });

            modelBuilder.Entity<ViewRMPropertiesMapping>(entity =>
            {
                entity.HasKey(e => e.RMMappingID);
            });

            modelBuilder.Entity<ViewRMChild>(entity =>
            {
                entity.HasKey(e => e.RMChildId);
            });


            modelBuilder.Entity<ViewStockTransferRequest>(entity =>
            {
                entity.HasKey(e => e.StockTransferRequestChildId);
            });
            modelBuilder.Entity<ViewStockTransferIssue>(entity =>
            {
                entity.HasKey(e => e.StockTransferIssueChildId);
            });

            modelBuilder.Entity<ViewProcessFinishGoodsStock>(entity =>
            {
                entity.HasKey(e => e.PRChildId);
            });

            modelBuilder.Entity<ViewProcessBOM>(entity =>
            {
                entity.HasKey(e => new { e.PRMasterId, e.ProcessID, e.RItem_id });
            });

            modelBuilder.Entity<ViewPRNoForIssueRM>(entity =>
            {
                entity.HasKey(e => e.PRMasterId);
            });

            modelBuilder.Entity<ViewProductDetails>(entity =>
            {
                entity.HasKey(e => e.FitemId).HasName("PK_ProductDetails");

            });

            modelBuilder.Entity<ViewProductComponent>(entity =>
            {
                entity.HasKey(e => e.ProductComponentId).HasName("PK_ProductComponent");

            });

            modelBuilder.Entity<ViewJWForGRN>(entity =>
            {
                entity.HasKey(e => e.IssueRMChangeItemID).HasName("PK_IssueRMForChangeChild");
            });

            modelBuilder.Entity<ViewProductProcessCharge>(entity =>
            {
                entity.HasKey(e => new { e.FitemId, e.ProductSizeId, e.ProcessId });
            });

            modelBuilder.Entity<ViewSaleOrderWPStatus>(entity =>
            {
                entity.HasKey(e => new { e.OrderChildID, e.PlanId });
            });

            modelBuilder.Entity<ViewProductQC>(entity =>
            {
                entity.HasKey(e => e.QCID).HasName("PK_ProductQC");
            });

            modelBuilder.Entity<ViewRMSupplierPriceHistory>(entity =>
            {
                entity.HasKey(e => e.RMPriceHistoryId);
            });

            modelBuilder.Entity<ViewOrderStatus>(entity =>
            {
                entity.HasKey(e => e.PlanChildId);
            });

            modelBuilder.Entity<ViewProductProcess>(entity =>
            {
                entity.HasKey(e => e.ProductProcessID);
            });

            modelBuilder.Entity<ViewRMCombo>(entity =>
            {
                entity.HasKey(e => e.RMComboChildID);
            });

            modelBuilder.Entity<ViewCostingProductForRMCombo>(entity =>
            {
                entity.HasKey(e => e.CostingID);
            });

            modelBuilder.Entity<ViewRMStatus>(entity =>
            {
                entity.HasKey(e => e.RitemID);
            });

            modelBuilder.Entity<ViewWorkPlanBOMRuning>(entity =>
            {
                entity.HasKey(e => e.PlanId);
            });

            modelBuilder.Entity<ViewSaleOrderItemBalBOM>(entity =>
            {
                entity.HasKey(e => new { e.OrderMasterID, e.FitemId,e.ProductSizeId,e.RItemID });
            });

            #region DBContext View
            modelBuilder.Entity<ViewRunningProcessReport>(entity =>
            {
                entity.HasKey(e => e.PRMasterId);
            });

            modelBuilder.Entity<ViewDelayedMaterial>(entity =>
            {
                entity.HasKey(e => e.POID);
            });

            modelBuilder.Entity<ViewRunningPOList>(entity =>
            {
                entity.HasKey(e => e.Poid).HasName("PK_POMaster");
            });

            modelBuilder.Entity<ViewSaleOrderStatus>(entity =>
            {
                entity.HasKey(e => new { e.PlanChildId, e.ProcessID });
            });
            modelBuilder.Entity<ViewSaleOrderSheetClose>(entity =>
            {
                entity.HasKey(e => e.OrderMasterID);
            });

            modelBuilder.Entity<ViewSaleOrderList>(entity =>
            {
                entity.HasKey(e => e.OrderChildID);
            });
            #endregion

            // HR View
            #region HR View
            modelBuilder.Entity<HR_ViewEmployeeDetails>(entity =>
                {
                    entity.HasKey(e => e.EmployeeId)
                          .HasName("PK_EmployeeDetails");
                });

            modelBuilder.Entity<HR_ViewAttandance>(entity =>
            {
                entity.HasKey(e => e.AttandanceID);
            });
            modelBuilder.Entity<HR_ViewAdvanceDetails>(entity =>
            {
                entity.HasKey(e => e.AdvanceId);
            });
            modelBuilder.Entity<HR_ViewSalaryStructure>(entity =>
            {
                entity.HasKey(e => e.SalStrChildId);
            });
            modelBuilder.Entity<HR_ViewCalculateMonthlySalary>(entity =>
            {
                entity.HasKey(e => e.SalStrChildId);
            });
            modelBuilder.Entity<HR_ViewEmployeeMonthlySalary>(entity =>
            {
                entity.HasKey(e => e.SalStrId);
            });
            #endregion

            #endregion
        }

        #region EntitiesDBSet
        public virtual DbSet<SaleOrderMaster> SaleOrderMaster { get; set; }
        public virtual DbSet<SaleOrderChild> SaleOrderChild { get; set; }
        public virtual DbSet<TaxDetails> TaxDetails { get; set; }
        public virtual DbSet<MenuDetails> MenuDetails { get; set; }
        public virtual DbSet<MenuCategory> MenuCategory { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }

        public virtual DbSet<DieDetails> DieDetails { get; set; }
        public virtual DbSet<DieFitem> DieFitem { get; set; }
        public virtual DbSet<BranchDetails> BranchDetails { get; set; }
        public virtual DbSet<ProductionProcessDetails> ProductionProcessDetails { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        public virtual DbSet<ProductMultipleColor> ProductMultipleColor { get; set; }
        public virtual DbSet<ProductMultipleSize> ProductMultipleSize { get; set; }
        public virtual DbSet<ProductProcess> ProductProcess { get; set; }
        public virtual DbSet<BrandDetails> BrandDetails { get; set; }
        public virtual DbSet<IssuedByDetails> IssuedByDetails { get; set; }
        public virtual DbSet<LabelDetails> LabelDetails { get; set; }
        public virtual DbSet<ProductSizeDetails> ProductSizeDetails { get; set; }
        public virtual DbSet<RackMaster> RackMaster { get; set; }
        public virtual DbSet<TreatmentDetails> TreatmentDetails { get; set; }
        public virtual DbSet<WidthDetails> WidthDetails { get; set; }
        public virtual DbSet<GSMDetails> GSMDetails { get; set; }
        public virtual DbSet<BuyerDetails> BuyerDetails { get; set; }
        public virtual DbSet<RItemSupp> RItemSupp { get; set; }
        public virtual DbSet<CurrencyDetails> CurrencyDetails { get; set; }
        public virtual DbSet<TrimmingDetails> TrimmingDetails { get; set; }
        public virtual DbSet<EmbossmentDetails> EmbossmentDetails { get; set; }
        public virtual DbSet<WaxDetails> WaxDetails { get; set; }
        public virtual DbSet<BackingDetails> BackingDetails { get; set; }
        public virtual DbSet<QuickTestDetails> QuickTestDetails { get; set; }
        public virtual DbSet<LiningDetails> LiningDetails { get; set; }
        public virtual DbSet<WashDetails> WashDetails { get; set; }
        public virtual DbSet<StitchingDetails> StitchingDetails { get; set; }
        public virtual DbSet<GroupDetails> GroupDetails { get; set; }
        public virtual DbSet<DesignerDetails> DesignerDetails { get; set; }
        public virtual DbSet<CountryDetails> CountryDetails { get; set; }
        public virtual DbSet<StateDetails> StateDetails { get; set; }
        public virtual DbSet<CityDetails> CityDetails { get; set; }
        public virtual DbSet<ConsigneeDetails> ConsigneeDetails { get; set; }
        public virtual DbSet<ProductCategoryDetails> ProductCategoryDetails { get; set; }
        public virtual DbSet<ProductSubCategoryDetails> ProductSubCategoryDetails { get; set; }
        public virtual DbSet<StickerDetials> StickerDetials { get; set; }
        public virtual DbSet<CareLabelDetails> CareLabelDetails { get; set; }
        public virtual DbSet<HandTagDetails> HandTagDetails { get; set; }
        public virtual DbSet<ActualRateRmhistory> ActualRateRmhistory { get; set; }
        public virtual DbSet<CartonDetails> CartonDetails { get; set; }
        public virtual DbSet<ChallanEsiDetails> ChallanEsiDetails { get; set; }
        public virtual DbSet<ChallanPfDetail> ChallanPfDetail { get; set; }
        public virtual DbSet<ClickProcessDetails> ClickProcessDetails { get; set; }
        public virtual DbSet<ClosingMaster> ClosingMaster { get; set; }
        public virtual DbSet<ClothDetails> ClothDetails { get; set; }
        public virtual DbSet<ColorDetails> ColorDetails { get; set; }
        public virtual DbSet<CompanyDetails> CompanyDetails { get; set; }
        public virtual DbSet<ComponentDetails> ComponentDetails { get; set; }
        public virtual DbSet<Consignee> Consignee { get; set; }
        public virtual DbSet<CreateJobCardMaster> CreateJobCardMaster { get; set; }
        public virtual DbSet<CuttingMaster> CuttingMaster { get; set; }

        public virtual DbSet<ExtraQtyPermission> ExtraQtyPermission { get; set; }
        public virtual DbSet<FgoodSupplier> FgoodSupplier { get; set; }
        public virtual DbSet<FinishDetails> FinishDetails { get; set; }
        public virtual DbSet<FinishMetalDetails> FinishMetalDetails { get; set; }
        public virtual DbSet<FinishReceive> FinishReceive { get; set; }
        public virtual DbSet<FitemQcentry> FitemQcentry { get; set; }
        public virtual DbSet<Fitting> Fitting { get; set; }
        public virtual DbSet<Gipmaster> Gipmaster { get; set; }
        public virtual DbSet<GoodsReceivedMaster> GoodsReceivedMaster { get; set; }
        public virtual DbSet<GroupName> GroupName { get; set; }
        public virtual DbSet<GspMaster> GspMaster { get; set; }
        public virtual DbSet<Idtable> Idtable { get; set; }
        public virtual DbSet<InpassMaster> InpassMaster { get; set; }
        public virtual DbSet<InsMaster> InsMaster { get; set; }
        public virtual DbSet<InsertRecord> InsertRecord { get; set; }
        public virtual DbSet<IssueRmOtherThanBomMaster> IssueRmOtherThanBomMaster { get; set; }
        public virtual DbSet<IssueRMForChangeChild> IssueRMForChangeChild { get; set; }
        public virtual DbSet<IssueRMforChangeMaster> IssueRMforChangeMaster { get; set; }
        public virtual DbSet<IssueRMforChangeItem> IssueRmforChangeItem { get; set; }
        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<LabourCharge> LabourCharge { get; set; }
        public virtual DbSet<LabourTypeDetails> LabourTypeDetails { get; set; }
        public virtual DbSet<LeaveEncashment> LeaveEncashment { get; set; }
        public virtual DbSet<MakingChargesVoucher> MakingChargesVoucher { get; set; }
        public virtual DbSet<MchMaster> MchMaster { get; set; }
        public virtual DbSet<MenOutPass> MenOutPass { get; set; }
        public virtual DbSet<MessageForRmfeed> MessageForRmfeed { get; set; }
        public virtual DbSet<MessageForSend> MessageForSend { get; set; }
        public virtual DbSet<MessageMenu> MessageMenu { get; set; }
        public virtual DbSet<MetalPartDetails> MetalPartDetails { get; set; }
        public virtual DbSet<MonthDetail> MonthDetail { get; set; }
        public virtual DbSet<Nature> Nature { get; set; }
        public virtual DbSet<NatureOfGroup> NatureOfGroup { get; set; }
        public virtual DbSet<NonRetunableOutPassFitem> NonRetunableOutPassFitem { get; set; }
        public virtual DbSet<NonReturnableOutPassInvoiceMaster> NonReturnableOutPassInvoiceMaster { get; set; }
        public virtual DbSet<NropCpMaster> NropCpMaster { get; set; }
        public virtual DbSet<OrderCancel> OrderCancel { get; set; }
        public virtual DbSet<OrderMaster> OrderMaster { get; set; }
        public virtual DbSet<OrderReminder> OrderReminder { get; set; }
        public virtual DbSet<OrderShippment> OrderShippment { get; set; }
        public virtual DbSet<OutPassMaster> OutPassMaster { get; set; }
        public virtual DbSet<PanningDetails> PanningDetails { get; set; }
        public virtual DbSet<PFDetails> PFDetails { get; set; }


        public virtual DbSet<POMaster> Pomaster { get; set; }
        public virtual DbSet<POChild> POChild { get; set; }
        public virtual DbSet<POForSaleOrderDetails> POForSaleOrderDetails { get; set; }

        public virtual DbSet<PrdPlanDetails> PrdPlanDetails { get; set; }
        public virtual DbSet<PriceQuote> PriceQuote { get; set; }
        public virtual DbSet<PurchaseVoucherMaster> PurchaseVoucherMaster { get; set; }
        public virtual DbSet<QcInProcess> QcInProcess { get; set; }
        public virtual DbSet<QualityDetails> QualityDetails { get; set; }
        public virtual DbSet<ReadymadeReceiving> ReadymadeReceiving { get; set; }
        public virtual DbSet<ReciptsFromBuyerMaster> ReciptsFromBuyerMaster { get; set; }
        public virtual DbSet<RitemMaster> RitemMaster { get; set; }
        public virtual DbSet<RMSubCategory> RMSubCategory { get; set; }
        public virtual DbSet<RMCategory> RMCategory { get; set; }
        public virtual DbSet<SalaryDetailsMaster> SalaryDetailsMaster { get; set; }
        // public virtual DbSet<SalaryStructMaster> SalaryStructMaster { get; set; }
        public virtual DbSet<ShapeDetails> ShapeDetails { get; set; }
        public virtual DbSet<SipMaster> SipMaster { get; set; }
        public virtual DbSet<SizeDetails> SizeDetails { get; set; }
        public virtual DbSet<SkiveingDetails> SkiveingDetails { get; set; }
        public virtual DbSet<SkivingMaster> SkivingMaster { get; set; }
        public virtual DbSet<SplittingMaster> SplittingMaster { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Stone> Stone { get; set; }
        public virtual DbSet<StoreKeepingRmchangeMaster> StoreKeepingRmchangeMaster { get; set; }
        public virtual DbSet<StructureDetails> StructureDetails { get; set; }
        public virtual DbSet<SelectionDetails> SelectionDetails { get; set; }
        public virtual DbSet<SupervisorDetails> SupervisorDetails { get; set; }
        public virtual DbSet<SupplierDetails> SupplierDetails { get; set; }
        public virtual DbSet<TempTable> TempTable { get; set; }
        public virtual DbSet<ThicknessDetails> ThicknessDetails { get; set; }
        public virtual DbSet<TransportDetails> TransportDetails { get; set; }
        public virtual DbSet<TypeOfColourDetails> TypeOfColourDetails { get; set; }
        public virtual DbSet<UnitDetails> UnitDetails { get; set; }
        public virtual DbSet<WeekDays> WeekDays { get; set; }
        public virtual DbSet<WeekNumber> WeekNumber { get; set; }
        public virtual DbSet<AdhesiveDetails> AdhesiveDetails { get; set; }

        public virtual DbSet<CostingMaster> CostingMaster { get; set; }
        public virtual DbSet<CostingRM> CostingRM { get; set; }
        public virtual DbSet<CostingElementDetails> CostingElementDetails { get; set; }
        public virtual DbSet<CostingEl> CostingEl { get; set; }
        public virtual DbSet<CostingRMComponent> CostingRMComponent { get; set; }
        public virtual DbSet<CostingSizeRM> CostingSizeRM { get; set; }
        public virtual DbSet<TempCostigIDsForUpdateCosting> TempCostigIDsForUpdateCosting { get; set; }

        public virtual DbSet<FOBCIFDetails> FOBCIFDetails { get; set; }
        public virtual DbSet<EmailSettings> EmailSettings { get; set; }
        public virtual DbSet<EmailType> EmailType { get; set; }
        public virtual DbSet<EmailSendDetails> EmailSendDetails { get; set; }
        public virtual DbSet<OpeningStockDetails> OpeningStockDetails { get; set; }
        public virtual DbSet<StyleDetails> StyleDetails { get; set; }
        public virtual DbSet<ContractorDetails> ContractorDetails { get; set; }
        public virtual DbSet<TempPOChild> TempPOChild { get; set; }
        public virtual DbSet<WorkPlanMaster> WorkPlanMaster { get; set; }
        public virtual DbSet<WorkPlanChild> WorkPlanChild { get; set; }
        public virtual DbSet<IssueMaster> IssueMaster { get; set; }
        public virtual DbSet<IssueChild> IssueChild { get; set; }

        public virtual DbSet<GRNMaster> GRNMaster { get; set; }
        public virtual DbSet<GRNChild> GRNChild { get; set; }
        public virtual DbSet<GRNJWRMComsumption> GRNJWRMComsumption { get; set; }

        public virtual DbSet<ReceiveWPMaster> ReceiveWPMaster { get; set; }
        public virtual DbSet<StoreKeepingMaster> StoreKeepingMaster { get; set; }
        public virtual DbSet<StoreKeepingChild> StoreKeepingChild { get; set; }
        public virtual DbSet<PackingMaster> PackingMaster { get; set; }
        public virtual DbSet<PackingChild> PackingChild { get; set; }
        public virtual DbSet<PackingWeightDetails> PackingWeightDetails { get; set; }
        public virtual DbSet<InvoiceMaster> InvoiceMaster { get; set; }
        public virtual DbSet<InvoiceChild> InvoiceChild { get; set; }
        public virtual DbSet<PIMaster> PIMaster { get; set; }
        public virtual DbSet<PIChild> PIChild { get; set; }
        public virtual DbSet<CurrencyHistoryDetails> CurrencyHistoryDetails { get; set; }
        public virtual DbSet<CardsFolder_Details> CardsFolder_Details { get; set; }
        public virtual DbSet<SampleType> SampleType { get; set; }
        public virtual DbSet<SampleRoomDetails> SampleRoomDetails { get; set; }
        public virtual DbSet<PatternRoomDetails> PatternRoomDetails { get; set; }
        public virtual DbSet<SampleIssueDetails> SampleIssueDetails { get; set; }
        public virtual DbSet<PatternIssueDetails> PatternIssueDetails { get; set; }
        public virtual DbSet<Sample_Request> Sample_Request { get; set; }
        public virtual DbSet<StoreMasterDetails> StoreMasterDetails { get; set; }

        public virtual DbSet<MaritalStatusDetails> MaritalStatusDetails { get; set; }

        public virtual DbSet<FormNumberSettingsDetails> FormNumberSettingsDetails { get; set; }
        public virtual DbSet<ProductFile> ProductFile { get; set; }
        public virtual DbSet<GRNFile> GRNFile { get; set; }
        public virtual DbSet<SampleRM> SampleRM { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<ReserveStockMaster> ReserveStockMaster { get; set; }
        public virtual DbSet<CostingCurrencyDetails> CostingCurrencyDetails { get; set; }
        public virtual DbSet<ZipperMaster> ZipperMaster { get; set; }
        public virtual DbSet<SoleMaster> SoleMaster { get; set; }
        public virtual DbSet<UserRights> UserRights { get; set; }
        public virtual DbSet<OrderShippingDetails> OrderShippingDetails { get; set; }
        public virtual DbSet<Move_RMStock> Move_RMStock { get; set; }
        public virtual DbSet<AdjustmentRMDetails> AdjustmentRMDetails { get; set; }

        public virtual DbSet<SessionYear> SessionYear { get; set; }
        public virtual DbSet<YearDetails> YearDetails { get; set; }
        public virtual DbSet<MultiLevelSubCategory> MultiLevelSubCategory { get; set; }
        public virtual DbSet<DisplayPosition> DisplayPosition { get; set; }
        public virtual DbSet<UploadDetails> UploadDetails { get; set; }
        public virtual DbSet<ProcessExecutionMaster> ProcessExecutionMaster { get; set; }
        public virtual DbSet<ProcessExecutionChild> ProcessExecutionChild { get; set; }
        public virtual DbSet<ReturnRMWP> ReturnRMWP { get; set; }
        public virtual DbSet<ProcessRecvMaster> ProcessRecvMaster { get; set; }
        public virtual DbSet<ProcessRecvChild> ProcessRecvChild { get; set; }

        public virtual DbSet<RequestRM> RequestRM { get; set; }
        public virtual DbSet<RequestRMChild> RequestRMChild { get; set; }
        public virtual DbSet<RequestRMIssue> RequestRMIssue { get; set; }
        public virtual DbSet<RequestRMIssueChild> RequestRMIssueChild { get; set; }
        public virtual DbSet<ReturnRM> ReturnRM { get; set; }
        public virtual DbSet<ReturnRMChild> ReturnRMChild { get; set; }
        public virtual DbSet<ReturnRMRecv> ReturnRMRecv { get; set; }
        public virtual DbSet<ReturnRMRecvChild> ReturnRMRecvChild { get; set; }
        public virtual DbSet<RMMovement> RMMovement { get; set; }

        public virtual DbSet<ViewIssueRMForChange> ViewIssueRmForChange { get; set; }
        public virtual DbSet<ViewIssueRMForChangeMaster> ViewIssueRMForChangeMaster { get; set; }
        public virtual DbSet<ReturnToSupplier> ReturnToSupplier { get; set; }
        public virtual DbSet<RecvJW> RecvJW { get; set; }

        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<RMBrandDetails> RMBrandDetails { get; set; }
        public virtual DbSet<ComponentGroupDetails> ComponentGroupDetails { get; set; }
        public virtual DbSet<HSNDetails> HSNDetails { get; set; }
        public virtual DbSet<PrintDetails> PrintDetails { get; set; }

        public virtual DbSet<POPaymentTerms> POPaymentTerms { get; set; }
        public virtual DbSet<PODeliveryTerms> PODeliveryTerms { get; set; }
        public virtual DbSet<POTransportDetails> POTransportDetails { get; set; }
        public virtual DbSet<ConstructionDetails> ConstructionDetails { get; set; }


        public virtual DbSet<EMPGenderDetails> EMPGenderDetails { get; set; }
        public virtual DbSet<HeelDetails> HeelDetails { get; set; }

        public virtual DbSet<BuyerConsigneeDetails> BuyerConsigneeDetails { get; set; }
        public virtual DbSet<BuyerBankDetails> BuyerBankDetails { get; set; }

        public virtual DbSet<DocumentTypeDetails> DocumentTypeDetails { get; set; }
        public virtual DbSet<RMFile> RMFile { get; set; }
        public virtual DbSet<BankDetails> BankDetails { get; set; }
        public virtual DbSet<DepartmentDetails> DepartmentDetails { get; set; }
        public virtual DbSet<GradeDetails> GradeDetails { get; set; }
        public virtual DbSet<DesignationDetails> DesignationDetails { get; set; }
        public virtual DbSet<GenderDetails> GenderDetails { get; set; }

        public virtual DbSet<ShoesCartonMaster> ShoesCartonMaster { get; set; }
        public virtual DbSet<ShoesCartonChild> ShoesCartonChild { get; set; }

        public virtual DbSet<ContractorPayment> ContractorPayment { get; set; }
        public virtual DbSet<ContractorPaymentChild> ContractorPaymentChild { get; set; }
        public virtual DbSet<ContractorAdvanceDetails> ContractorAdvanceDetails { get; set; }
        public virtual DbSet<TempTableDetails> TempTableDetails { get; set; }
        public virtual DbSet<ProcessChargeDetails> ProcessChargeDetails { get; set; }
        public virtual DbSet<ProcessChargeHistoryDetails> ProcessChargeHistoryDetails { get; set; }
        public virtual DbSet<ProductProcessPictures> ProductProcessPictures { get; set; }
        public virtual DbSet<RMSupplierPriceHistory> RMSupplierPriceHistory { get; set; }

        #region DBSet
        public virtual DbSet<InvoiceSettingDetails> InvoiceSettingDetails { get; set; }
        public virtual DbSet<OrderSheetClose> OrderSheetClose { get; set; }
        #endregion

        //HR 
        public virtual DbSet<HR_DepartmentDetails> HR_DepartmentDetails { get; set; }
        public virtual DbSet<HR_GradeDetails> HR_GradeDetails { get; set; }
        public virtual DbSet<HR_TitleDetails> HR_TitleDetails { get; set; }
        public virtual DbSet<HR_DesignationDetails> HR_DesignationDetails { get; set; }
        public virtual DbSet<HR_EmployeeDetails> HR_EmployeeDetails { get; set; }
        public virtual DbSet<HR_GenderDetails> HR_GenderDetails { get; set; }
        public virtual DbSet<HR_MaritalStatusDetails> HR_MaritalStatusDetails { get; set; }
        public virtual DbSet<HR_EmployeeFile> HR_EmployeeFile { get; set; }
        public virtual DbSet<HR_BankDetails> HR_BankDetails { get; set; }
        public virtual DbSet<HR_SalaryDetailsMaster> HR_SalaryDetailsMaster { get; set; }
        public virtual DbSet<HR_SalaryStructureMaster> HR_SalaryStructureMaster { get; set; }
        public virtual DbSet<HR_SalaryStructureChild> HR_SalaryStructureChild { get; set; }
        public virtual DbSet<HR_AttandanceDetails> HR_AttandanceDetails { get; set; }
        public virtual DbSet<HR_EmployeeType> HR_EmployeeType { get; set; }
        public virtual DbSet<HR_AdvanceDetails> HR_AdvanceDetails { get; set; }
        public virtual DbSet<HR_ESIDetails> HR_ESIDetails { get; set; }
        public virtual DbSet<HR_PFDetails> HR_PFDetails { get; set; }
        public virtual DbSet<HR_AttandanceStatus> HR_AttandanceStatus { get; set; }
        public virtual DbSet<HR_AllowanceDetails> HR_AllowanceDetails { get; set; }
        public virtual DbSet<HR_HolidayMaster> HR_HolidayMaster { get; set; }

        public virtual DbSet<CompanyBeneficiaryBankDetails> CompanyBeneficiaryBankDetails { get; set; }
        public virtual DbSet<CompanyIntermediaryBankDetails> CompanyIntermediaryBankDetails { get; set; }
        public virtual DbSet<BuyerCartonStickerTemplate> BuyerCartonStickerTemplate { get; set; }

        public virtual DbSet<RTSGRNMaster> RTSGRNMaster { get; set; }
        public virtual DbSet<RTSGRNChild> RTSGRNChild { get; set; }


        public virtual DbSet<RMProperties> RMProperties { get; set; }
        public virtual DbSet<RMPropertiesValue> RMPropertiesValue { get; set; }
        public virtual DbSet<RMPropertiesMapping> RMPropertiesMapping { get; set; }
        public virtual DbSet<RMChild> RMChild { get; set; }
        public virtual DbSet<RItemQuickTest> RItemQuickTest { get; set; }

        public virtual DbSet<StockTransferRequest> StockTransferRequest { get; set; }
        public virtual DbSet<StockTransferRequestChild> StockTransferRequestChild { get; set; }
        public virtual DbSet<StockTransferIssue> StockTransferIssue { get; set; }
        public virtual DbSet<StockTransferIssueChild> StockTransferIssueChild { get; set; }

        public virtual DbSet<ProductComponent> ProductComponent { get; set; }
        public virtual DbSet<ProductProcessCharge> ProductProcessCharge { get; set; }

        public virtual DbSet<ProductQCPointMaster> ProductQCPointMaster { get; set; }
        public virtual DbSet<ProductQC> ProductQC { get; set; }

        public virtual DbSet<RMComboMaster> RMComboMaster { get; set; }
        public virtual DbSet<RMComboChild> RMComboChild { get; set; }


        public virtual DbSet<UserAsSupervisorDetails> UserAsSupervisorDetails { get; set; }
        #endregion

        #region ViewsDBSet
        public virtual DbSet<ViewShowRMSizeCosting> ViewShowRMSizeCosting { get; set; }
        public virtual DbSet<ViewOrderItemsForPacking> ViewOrderItemsForPacking { get; set; }
        public virtual DbSet<ViewPackingMaster> ViewPackingMaster { get; set; }
        public virtual DbSet<ViewPackingChild> ViewPackingChild { get; set; }
        public virtual DbSet<ViewPackingCartons> ViewPackingCartons { get; set; }
        public virtual DbSet<ViewPackingWeight> ViewPackingWeight { get; set; }
        public virtual DbSet<ViewInvoice> ViewInvoice { get; set; }
        public virtual DbSet<ViewInvoiceChild> ViewInvoiceChild { get; set; }
        public virtual DbSet<ViewInvoicePrint> ViewInvoicePrint { get; set; }
        public virtual DbSet<ViewPI> ViewPI { get; set; }
        public virtual DbSet<ViewPIChild> ViewPIChild { get; set; }
        public virtual DbSet<ViewPIPrint> ViewPIPrint { get; set; }
        public virtual DbSet<ViewCurrencyHistory> ViewCurrencyHistory { get; set; }
        public virtual DbSet<ViewSampleRoom> ViewSampleRoom { get; set; }
        public virtual DbSet<ViewPatternRoomDetails> ViewPatternRoomDetails { get; set; }
        public virtual DbSet<ViewSampleIssue> ViewSampleIssue { get; set; }
        public virtual DbSet<ViewPatternIssueDetails> ViewPatternIssueDetails { get; set; }
        public virtual DbSet<ViewSample_Request> ViewSample_Request { get; set; }
        public virtual DbSet<ViewEmployeeDetails> ViewEmployeeDetails { get; set; }
        public virtual DbSet<ViewBelowMinimumLevel> ViewBelowMinimumLevel { get; set; }
        public virtual DbSet<ViewProductWithoutPhoto> ViewProductWithoutPhoto { get; set; }
        public virtual DbSet<ViewPODelay> ViewPODelay { get; set; }
        public virtual DbSet<ViewPODelaySum> ViewPODelaySum { get; set; }
        public virtual DbSet<ViewUnusedRMList> ViewUnusedRMList { get; set; }
        public virtual DbSet<VisitorRegistorDetails> VisitorRegistorDetails { get; set; }
        public virtual DbSet<ViewCostingPendingReport> ViewCostingPendingReport { get; set; }
        public virtual DbSet<ViewPOBalance> ViewPOBalance { get; set; }
        public virtual DbSet<ViewSampleRM> ViewSampleRM { get; set; }
        public virtual DbSet<ViewReserveStock> ViewReserveStock { get; set; }
        public virtual DbSet<ViewCostingCurrencyDetails> ViewCostingCurrencyDetails { get; set; }
        public virtual DbSet<ViewCostingCurrencyEdit> ViewCostingCurrencyEdit { get; set; }
        public virtual DbSet<ViewDieFitemDetails> ViewDieFitemDetails { get; set; }
        public virtual DbSet<ViewSaleOrderDetails> ViewSaleOrderDetails { get; set; }
        public virtual DbSet<ViewSaleOrder> ViewSaleOrder { get; set; }
        public virtual DbSet<ViewPOPrint> ViewPOPrint { get; set; }
        public virtual DbSet<ViewPOChild> ViewPOChild { get; set; }
        public virtual DbSet<ViewPOMaster> ViewPOMaster { get; set; }
        public virtual DbSet<ViewMenu> ViewMenu { get; set; }
        public virtual DbSet<ViewUserDetails> ViewUserDetails { get; set; }
        public virtual DbSet<ViewProductShow> ViewProductShow { get; set; }
        public virtual DbSet<ViewProductProcessDetails> ViewProductProcessDetails { get; set; }
        public virtual DbSet<ViewBrandDetails> ViewBrandDetails { get; set; }
        public virtual DbSet<ViewIssuedByDetails> ViewIssuedByDetails { get; set; }
        public virtual DbSet<ViewLabelDetails> ViewLabelDetails { get; set; }
        public virtual DbSet<ViewRackMaster> ViewRackMaster { get; set; }
        public virtual DbSet<ViewRMSubCategory> ViewRMSubCategory { get; set; }
        public virtual DbSet<ViewStateDetails> ViewStateDetails { get; set; }
        public virtual DbSet<ViewCityDetails> ViewCityDetails { get; set; }
        public virtual DbSet<ViewSupplierDetails> ViewSupplierDetails { get; set; }
        public virtual DbSet<ViewBuyerDetails> ViewBuyerDetails { get; set; }
        public virtual DbSet<ViewProduct> ViewProduct { get; set; }
        public virtual DbSet<ViewSticker> ViewSticker { get; set; }
        public virtual DbSet<ViewCarelabel> ViewCarelabel { get; set; }
        public virtual DbSet<ViewHandTag> ViewHandTag { get; set; }
        public virtual DbSet<ViewRItemShow> ViewRItemShow { get; set; }
        public virtual DbSet<ViewShowCosting> ViewShowCosting { get; set; }
        public virtual DbSet<ViewCostingRM> ViewCostingRM { get; set; }
        public virtual DbSet<ViewCostingEL> ViewCostingEL { get; set; }
        public virtual DbSet<ViewRMSupplier> ViewRMSupplier { get; set; }
        public virtual DbSet<ViewEmailSettings> ViewEmailSettings { get; set; }
        public virtual DbSet<ViewSendEmail> ViewSendEmail { get; set; }
        public virtual DbSet<ViewOrderBOM> ViewOrderBOM { get; set; }
        public virtual DbSet<ViewOrderBOMSUM> ViewOrderBOMSUM { get; set; }
        public virtual DbSet<ViewOpStockDetails> ViewOpStockDetails { get; set; }
        public virtual DbSet<ViewItemBOM> ViewItemBOM { get; set; }
        public virtual DbSet<ViewContractorDetails> ViewContractorDetails { get; set; }
        public virtual DbSet<ViewWorkPlan> ViewWorkPlan { get; set; }
        public virtual DbSet<ViewWorkPlanChild> ViewWorkPlanChild { get; set; }
        public virtual DbSet<ViewWorkPlanBOM> ViewWorkPlanBOM { get; set; }
        public virtual DbSet<ViewIssueRMWP> ViewIssueRMWP { get; set; }

        public virtual DbSet<ViewGRNMaster> ViewGRNMaster { get; set; }
        public virtual DbSet<ViewGRNChild> ViewGRNChild { get; set; }
        public virtual DbSet<ViewGRN> ViewGRN { get; set; }
        public virtual DbSet<ViewGRNForStoreKeeping> ViewGRNForStoreKeeping { get; set; }
        public virtual DbSet<ViewPOForGRN> ViewPOForGRN { get; set; }

        public virtual DbSet<ViewGetRack> ViewGetRack { get; set; }
        public virtual DbSet<ViewReceiveWP> ViewReceiveWP { get; set; }
        public virtual DbSet<ViewRMStock> ViewRMStock { get; set; }
        public virtual DbSet<ViewRMStockRackWise> ViewRMStockRackWise { get; set; }
        public virtual DbSet<ViewUserRights> ViewUserRights { get; set; }
        public virtual DbSet<ViewSupplierPerformance> ViewSupplierPerformance { get; set; }
        public virtual DbSet<ViewSupplierPerformanceSumarray> ViewSupplierPerformanceSumarray { get; set; }
        public virtual DbSet<ViewMoveRMStock> ViewMoveRMStock { get; set; }
        public virtual DbSet<ViewAdjustmentRM> ViewAdjustmentRM { get; set; }
        public virtual DbSet<ViewSalaryStructure> ViewSalaryStructure { get; set; }
        public virtual DbSet<ViewProductSize> ViewProductSize { get; set; }
        public virtual DbSet<ViewProductCategory> ViewProductCategory { get; set; }
        public virtual DbSet<ViewProcessExecution> ViewProcessExecution { get; set; }
        public virtual DbSet<ViewReturnRMWP> ViewReturnRMWP { get; set; }
        public virtual DbSet<ViewProcessRecv> ViewProcessRecv { get; set; }

        public virtual DbSet<ViewReqRMList> ViewReqRMList { get; set; }
        public virtual DbSet<ViewRequestRM> ViewRequestRM { get; set; }
        public virtual DbSet<ViewRequestRMIssue> ViewRequestRMIssue { get; set; }
        public virtual DbSet<ViewRetRMList> ViewRetRMList { get; set; }
        public virtual DbSet<ViewReturnRMRecv> ViewReturnRMRecv { get; set; }
        public virtual DbSet<ViewStoreKeeping> ViewStoreKeeping { get; set; }

        public virtual DbSet<ViewWIP> ViewWIP { get; set; }
        public virtual DbSet<ViewCostingRMComponent> ViewCostingRMComponent { get; set; }
        public virtual DbSet<ViewCostingRMPrint> ViewCostingRMPrint { get; set; }

        public virtual DbSet<ViewRTS> ViewRTS { get; set; }
        public virtual DbSet<ViewComponentDetails> ViewComponentDetails { get; set; }

        public virtual DbSet<ViewHSNDetails> ViewHSNDetails { get; set; }
        public virtual DbSet<ViewBranchDetails> ViewBranchDetails { get; set; }
        public virtual DbSet<ViewRMLotRate> ViewRMLotRate { get; set; }

        public virtual DbSet<ViewProductFile> ViewProductFile { get; set; }
        public virtual DbSet<ViewRMFile> ViewRMFile { get; set; }
        public virtual DbSet<ViewGRNFile> ViewGRNFile { get; set; }
        public virtual DbSet<ViewBuyerBank> ViewBuyerBank { get; set; }
        public virtual DbSet<ViewBuyerConsignee> ViewBuyerConsignee { get; set; }
        public virtual DbSet<SupplierBankDetails> SupplierBankDetails { get; set; }
        public virtual DbSet<ViewSupplierBank> ViewSupplierBank { get; set; }
        public virtual DbSet<ViewShoesPacking> ViewShoesPacking { get; set; }
        public virtual DbSet<ViewSaleOrderChild> ViewSaleOrderChild { get; set; }
        public virtual DbSet<ViewShoesCartonChild> ViewShoesCartonChild { get; set; }
        public virtual DbSet<ViewShoesPackingChild> ViewShoesPackingChild { get; set; }
        public virtual DbSet<ViewProcessExecutionMaster> ViewProcessExecutionMaster { get; set; }
        public virtual DbSet<ViewProcessRecvMaster> ViewProcessRecvMaster { get; set; }
        public virtual DbSet<ViewContractorPayment> ViewContractorPayment { get; set; }
        public virtual DbSet<ViewContractorAdvanceDetails> ViewContractorAdvanceDetails { get; set; }
        public virtual DbSet<ViewContractorBalance> ViewContractorBalance { get; set; }
        public virtual DbSet<ViewProductListForProcessCharge> ViewProductListForProcessCharge { get; set; }
        public virtual DbSet<ViewProductProcessPictures> ViewProductProcessPictures { get; set; }

        public virtual DbSet<ViewDashBoardBuyerLastOrder> ViewDashBoardBuyerLastOrder { get; set; }
        public virtual DbSet<ViewDashBoardOrderToBeDeliver> ViewDashBoardOrderToBeDeliver { get; set; }

        public virtual DbSet<ViewWPQtyForNextProcess> ViewWPQtyForNextProcess { get; set; }
        public virtual DbSet<ViewRMLedger> ViewRMLedger { get; set; }
        public virtual DbSet<ViewProductionPacking> ViewProductionPacking { get; set; }

        public virtual DbSet<ViewCostingSizeRM> ViewCostingSizeRM { get; set; }

        public virtual DbSet<ViewCompanyBeneficiaryBankDetails> ViewCompanyBeneficiaryBankDetails { get; set; }
        public virtual DbSet<ViewCompanyIntermediaryBankDetails> ViewCompanyIntermediaryBankDetails { get; set; }

        public virtual DbSet<ViewRTSGRN> ViewRTSGRN { get; set; }

        public virtual DbSet<ViewRMPropertiesDetails> ViewRMPropertiesDetails { get; set; }
        public virtual DbSet<ViewRMPropertiesMapping> ViewRMPropertiesMapping { get; set; }
        public virtual DbSet<ViewRMChild> ViewRMChild { get; set; }

        public virtual DbSet<ViewStockTransferRequest> ViewStockTransferRequest { get; set; }
        public virtual DbSet<ViewStockTransferIssue> ViewStockTransferIssue { get; set; }

        public virtual DbSet<ViewProcessFinishGoodsStock> ViewProcessFinishGoodsStock { get; set; }

        public virtual DbSet<ViewProcessBOM> ViewProcessBOM { get; set; }
        public virtual DbSet<ViewPRNoForIssueRM> ViewPRNoForIssueRM { get; set; }

        public virtual DbSet<ViewProductDetails> ViewProductDetails { get; set; }
        public virtual DbSet<ViewProductComponent> ViewProductComponent { get; set; }
        public virtual DbSet<ViewJWForGRN> ViewJWForGRN { get; set; }

        public virtual DbSet<ViewProductProcessCharge> ViewProductProcessCharge { get; set; }
        public virtual DbSet<ViewSaleOrderWPStatus> ViewSaleOrderWPStatus { get; set; }

        public virtual DbSet<ViewProductQC> ViewProductQC { get; set; }
        public virtual DbSet<ViewRMSupplierPriceHistory> ViewRMSupplierPriceHistory { get; set; }

        public virtual DbSet<ViewOrderStatus> ViewOrderStatus { get; set; }
        public virtual DbSet<ViewProductProcess> ViewProductProcess { get; set; }

        public virtual DbSet<ViewRMStatus> ViewRMStatus { get; set; }
        public virtual DbSet<ViewWorkPlanBOMRuning> ViewWorkPlanBOMRuning { get; set; }
        public virtual DbSet<ViewSaleOrderItemBalBOM> ViewSaleOrderItemBalBOM { get; set; }


        public virtual DbSet<ViewSaleOrderList> ViewSaleOrderList { get; set; }
        #region DBSet View
        public virtual DbSet<ViewRunningProcessReport> ViewRunningProcessReport { get; set; }
        public virtual DbSet<ViewRunningPOList> ViewRunningPOList { get; set; }
        public virtual DbSet<ViewDelayedMaterial> ViewDelayedMaterial { get; set; }
        public virtual DbSet<ViewSaleOrderStatus> ViewSaleOrderStatus { get; set; }
        public virtual DbSet<ViewSaleOrderSheetClose> ViewSaleOrderSheetClose { get; set; }
        #endregion

        // HR 
        public virtual DbSet<HR_ViewEmployeeDetails> HR_ViewEmployeeDetails { get; set; }
        public virtual DbSet<HR_ViewAttandance> HR_ViewAttandance { get; set; }
        public virtual DbSet<HR_ViewAdvanceDetails> HR_ViewAdvanceDetails { get; set; }
        public virtual DbSet<HR_ViewSalaryStructure> HR_ViewSalaryStructure { get; set; }
        public virtual DbSet<HR_ViewCalculateMonthlySalary> HR_ViewCalculateMonthlySalary { get; set; }
        public virtual DbSet<HR_ViewEmployeeMonthlySalary> HR_ViewEmployeeMonthlySalary { get; set; }

        public virtual DbSet<ViewRMCombo> ViewRMCombo { get; set; }
        public virtual DbSet<ViewCostingProductForRMCombo> ViewCostingProductForRMCombo { get; set; }
        #endregion
    }
}