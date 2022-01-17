using ERP.Business;
using ERP.Business.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NonFactors.Mvc.Grid;
using System;
using System.IO;

namespace ERP.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDataProtection()
            //.DisableAutomaticKeyGeneration();

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@".\logskey\"));

            // Add framework services.
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); });

            services.AddMvc();


            services.AddMvcGrid();
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<WebApplicationSettings>(Configuration.GetSection("WebApplicationSettings"));
            services.Configure<FormOptions>(x => x.ValueCountLimit = 4096);
            //services.AddSingleton<IConfigurationRoot>(Configuration);   // IConfigurationRoot
            ////// *If* you need access to generic IConfiguration this is **required**
            //services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConfigurationRoot>(Configuration);   // IConfigurationRoot
            services.AddSingleton<IConfiguration>(Configuration);   // IConfiguration explicitly

            //services.AddTransient<IWebApplicationSettings, WebApplicationSettings>();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");

                options.AreaViewLocationFormats.Add("/Costing/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/HistoryRoom/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/HR/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/IN/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Integration/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Masters/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Order/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/PackingInvoice/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Production/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Products/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Purchase/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/RawMaterial/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Store/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Tools/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/ZohoBooks/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            #region Transient
            services.AddTransient<IRMCategoryContract, RMCat>();
            services.AddTransient<IRMSubCategoryContract, RMSubCat>();
            services.AddTransient<IRMContract, RM>();
            services.AddTransient<IAdhesiveContract, Adhesive>();
            services.AddTransient<ICartonContract, Carton>();
            services.AddTransient<IClothContract, Cloth>();
            services.AddTransient<IColorContract, Color>();
            services.AddTransient<IFinishMetalContract, FinishMetal>();
            services.AddTransient<IMetalPartContract, MetalPart>();
            services.AddTransient<IPanningContract, Panning>();
            services.AddTransient<IQalityContract, Quality>();
            services.AddTransient<ISelectionContract, Selection>();
            services.AddTransient<IShapeContract, Shape>();
            services.AddTransient<ISizeContract, Size>();
            services.AddTransient<IStrucureContract, Structure>();
            services.AddTransient<IThicknessContract, Thickness>();
            services.AddTransient<IUnitContract, Unit>();
            services.AddTransient<ICurrencyContract, Currency>();
            services.AddTransient<ITrimmingContract, Trimming>();
            services.AddTransient<IEmbossmentContract, Embossment>();
            services.AddTransient<IWaxContract, Wax>();
            services.AddTransient<IBackingContract, Backing>();
            services.AddTransient<IQuickTestContract, QuickTest>();
            services.AddTransient<ILiningContract, Lining>();
            services.AddTransient<IWashContract, Wash>();
            services.AddTransient<IQalityContract, Quality>();
            services.AddTransient<IStitchingContract, Stitching>();
            services.AddTransient<IGroupContract, Group>();
            services.AddTransient<IDesignerContract, Designer>();
            services.AddTransient<ICountryContract, Country>();
            services.AddTransient<IStateContract, State>();
            services.AddTransient<ICityContract, City>();
            services.AddTransient<ISupplierContract, Supplier>();
            services.AddTransient<IConsigneeContract, Consignee>();
            services.AddTransient<IProductCategoryContract, ProductCategory>();
            services.AddTransient<IProductSubCategoryContract, ProductSubCategory>();
            services.AddTransient<IStickerContract, Sticker>();
            services.AddTransient<ICareLabelContract, CareLabel>();
            services.AddTransient<IHandTagContract, HandTag>();
            services.AddTransient<ITransportContract, Transport>();
            services.AddTransient<IBuyerContract, Buyer>();
            services.AddTransient<IWidthContract, Width>();
            services.AddTransient<IGSMContract, GSM>();
            services.AddTransient<ICompanyContract, Company>();
            services.AddTransient<IBrandContract, Brand>();
            services.AddTransient<IIssuedByContract, IssuedBy>();
            services.AddTransient<ILabelContract, Label>();
            services.AddTransient<ITreatmentContract, Treatment>();
            services.AddTransient<IProductSizeContract, ProductSize>();
            services.AddTransient<IRackMasterContract, Rack>();
            services.AddTransient<IProductContract, Product>();
            services.AddTransient<IProductionProcessContract, ProductionProcess>();
            services.AddTransient<IBranchContract, Branch>();
            services.AddTransient<IBankContract, Bank>();
            services.AddTransient<IDieContract, Die>();
            services.AddTransient<IUserContract, User>();
            services.AddTransient<IMenuContract, Menu>();
            services.AddTransient<IPOContract, PO>();
            services.AddTransient<ITaxContract, Tax>();
            services.AddTransient<ICostingContract, Costing>();
            services.AddTransient<ICostingElementContract, CostingElement>();
            services.AddTransient<ISaleOrderContract, SaleOrder>();
            services.AddTransient<IEmailsContract, Emails>();
            services.AddTransient<IOpeningStockContract, OpeningStock>();
            services.AddTransient<IStyleContract, Style>();
            services.AddTransient<IContractorContract, Contractor>();

            services.AddTransient<IWorkPlanContract, WorkPlan>();

            services.AddTransient<IIssueContract, Issue>();
            services.AddTransient<IGRNContract, GRN>();
            services.AddTransient<IStoreKeepingContract, StoreKeeping>();
            services.AddTransient<IRMStockContract, RMStock>();
            services.AddTransient<IReceiveWPContract, ReceiveWP>();
            services.AddTransient<IPackingContract, Packing>();
            services.AddTransient<IPackingWeightContract, PackingWeight>();
            services.AddTransient<IInvoiceContract, Invoice>();
            services.AddTransient<IPIContract, PI>();
            services.AddTransient<ICardContract, Card>();
            services.AddTransient<ISampleRoomContract, SampleRoom>();
            services.AddTransient<IPatternRoomContract, PatternRoom>();
            services.AddTransient<ISampleIssueContract, SampleIssue>();
            services.AddTransient<IPatternIssueContract, PatternIssue>();
            services.AddTransient<ISampleChangeRequestContract, SampleChangeRequest>();
            services.AddTransient<IStoreMasterContract, StoreMaster>();

            services.AddTransient<IVisitorRegistorContract, VisitorRegistor>();
            services.AddTransient<IFormSettingContract, FormSetting>();
            services.AddTransient<IDBBackup, DBBackupEntry>();
            services.AddTransient<IUserRightContract, UserRight>();
            services.AddTransient<IMoveRMStockContract, MoveRMStock>();
            services.AddTransient<IAdjustmentRMContract, AdjustmentRM>();

            services.AddTransient<IUploadContract, Upload>();
            services.AddTransient<IComponent, Component>();
            services.AddTransient<IRMForChangeContract, IuuseRMForChange>();
            services.AddTransient<IRMBrandContract, RMBrand>();
            services.AddTransient<IComponentGroup, ComponentGroup>();
            services.AddTransient<IHSNContract, HSN>();
            services.AddTransient<IPrintContract, Print>();
            services.AddTransient<IPOPTContract, POPT>();
            services.AddTransient<IPODTContract, PODT>();
            services.AddTransient<IPOTContract, POT>();
            services.AddTransient<IConstructionContract, Construction>();
            services.AddTransient<IGenderContract, Gender>();
            services.AddTransient<IHeelContract, Heel>();
            services.AddTransient<IDocumentType, DocumentType>();
            services.AddTransient<IZWorkPlanContract, ZWorkPlan>();
            services.AddTransient<IDepartmentContract, Department>();
            services.AddTransient<IDashBoard, DashBoard>();

            services.AddTransient<ICompanyBeneficiaryContract, CompanyBeneficiary>();
            services.AddTransient<ICompanyIntermediaryContract, CompanyIntermediary>();

            services.AddTransient<IRMPropertiesContract, BRMProperties>();
            services.AddTransient<IUserAsSupervisorContract, UserAsSupervisor>();

            #region Startup
            services.AddTransient<IInvoiceSettingContract, InvoiceSetting>();
            #endregion

            #region HR
            services.AddTransient<IHRGradeContract, HRGrade>();
            services.AddTransient<ITitleContract, Title>();
            services.AddTransient<IHRDepartmentContract, HRDepartment>();
            services.AddTransient<IDesignationContract, Designation>();
            services.AddTransient<IHRBankContract, HRBank>();
            services.AddTransient<IEmployeeContract, Employee>();
            services.AddTransient<IAttandanceContract, Attandance>();
            services.AddTransient<IAdvanceContract, Advance>();
            services.AddTransient<IESIContract, ESI>();
            services.AddTransient<IPFContract, PF>();
            services.AddTransient<IAllowanceContract, Allowance>();
            services.AddTransient<ISalaryStructureContract, SalaryStructure>();
            services.AddTransient<IHolidayContract, Holiday>();
            services.AddTransient<ISalaryContract, Salary>();
            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc(routes =>
            {
                // Areas support
                routes.MapRoute(
                name: "areaRoute",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
