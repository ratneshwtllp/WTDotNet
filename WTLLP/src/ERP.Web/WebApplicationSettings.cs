using ERP.Helper;

namespace ERP.Web
{
    public class WebApplicationSettings : IWebApplicationSettings
    {
        public string ApplicationName { get; set; } = "My Application";
        public string ApiServiceUrl { get; set; }
    }
}
