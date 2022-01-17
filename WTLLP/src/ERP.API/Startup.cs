using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ERP.DataAccess;
using ERP.Api.Helper;

namespace ERP.API
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
            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string sqlConnectionString = Configuration.GetConnectionString("ERPConnection");
            bool useInMemoryProvider = false;// bool.Parse(Configuration.GetConnectionString("InMemoryProvider"));

            services.AddDbContext<DBContext>(options =>
            {
                switch (useInMemoryProvider)
                {
                    case true:
                        options.UseInMemoryDatabase();
                        break;
                    default:
                        options.UseSqlServer(sqlConnectionString);//,b => b.MigrationsAssembly("IK.API"));
                        break;
                }
            });

            // Add framework services.
            services.AddCors();
            services.AddMvcCore()
                 .AddJsonFormatters();
            //Add InMemory Cache service.
            services.AddMemoryCache();
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            // Add our Config object so it can be injected
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddSingleton<IConfigurationRoot>(Configuration);   // IConfigurationRoot
            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton<IConfiguration>(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseStaticFiles();
            // Add MVC to the request pipeline.
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            //app.UseExceptionHandler(
            //  builder =>
            //  {
            //      builder.Run(
            //        async context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            //            var error = context.Features.Get<IExceptionHandlerFeature>();
            //            if (error != null)
            //            {
            //                //context.Response.AddApplicationError(error.Error.Message);
            //                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
            //            }
            //        });
            //  });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
            DbInitializer.Initialize(app.ApplicationServices);

        }
    }
}
