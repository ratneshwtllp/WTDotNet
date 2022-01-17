using ERP.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.API.Controllers
{

    [Route("api")]
    public class ConfigurationController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        private ApplicationSettings ApplicationSettings { get; set; }
        private IConfigurationRoot ConfigRoot { get; set; }

        public ConfigurationController(IOptions<ApplicationSettings> settings, IConfiguration configuration, IConfigurationRoot configRoot)
        {
            ApplicationSettings = settings.Value;
            Configuration = configuration;

            ConfigRoot = configRoot;
        }

        [HttpGet("reloadconfig")]
        public ActionResult ReloadConfig()
        {

            ConfigRoot.Reload();

            // this should give the latest value from config
            var lastVal = Configuration.GetValue<string>("ApplicationSettings:ApplicationName");

            return Ok(lastVal);
        }

        [HttpGet("appname")]
        public string AppName()
        {
            return ApplicationSettings.ApplicationName;
        }


        [HttpGet("appname_key")]
        public string AppNameKey()
        {
            return Configuration.GetValue<string>("MySettings:ApplicationName");
        }

        [HttpGet("maxlistcount_key")]
        public int MaxListCountKey()
        {
            return Configuration.GetValue<int>("MySettings:MaxItemsPerList");
        }
    }
}
