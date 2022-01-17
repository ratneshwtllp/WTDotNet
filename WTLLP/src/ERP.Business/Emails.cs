using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ERP.Business
{
    public class Emails : IEmailsContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;   
             
        public Emails(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<EmailSettings>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "emails");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<EmailSettings>>(content);
            return result;
        }

        public async Task<EmailSettings> Get(long emailid)
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/" + emailid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<EmailSettings>(content);
            return result;
        }

        public async Task<List<ViewEmailSettings>> GetViewEmailSettingsSearch(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetViewEmailSettingsSearch/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewEmailSettings>>(content);
            return result;
        }

        public async Task<List<ViewEmailSettings>> GetViewEmailSettings()
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetViewEmailSettings");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewEmailSettings>>(content);
            return result;
        }

        public async Task<string> GetEmailsList()
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetEmailsList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewEmailID()
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetNewEmailID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> Post(EmailSettings value )
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "emails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(EmailSettings value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "emails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Delete(long id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "emails/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //public async Task<int> GetNewPOSerial()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "po/GetNewPOSerial");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Convert.ToInt16(content);
        //}

        //public async Task<List<ViewPOPrint>> GetPOForPrint(long id)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "po/GetPOForPrint/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewPOPrint>>(content);
        //    return result;
        //}


    }
}
