namespace ERP.Helper
{
    public interface IWebApplicationSettings
    {
        string ApiServiceUrl { get; set; }
        string ApplicationName { get; set; }
    }
}