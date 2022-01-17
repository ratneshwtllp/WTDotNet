using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using ERP.Helper;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


namespace ERP.Business
{
    public class Upload : IUploadContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Upload(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<UploadDetails> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Upload");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UploadDetails>(content);
            return result;
        }
    }
}
