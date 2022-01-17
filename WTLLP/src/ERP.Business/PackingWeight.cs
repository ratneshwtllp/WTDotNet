using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ERP.Business
{
    public class PackingWeight : IPackingWeightContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public PackingWeight(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        //public async Task<List<PackingWeightDetails>> Get()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "packingweight");
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<PackingWeightDetails>>(content);
        //    return result;
        //}

        //public async Task<PackingWeightDetails> Get(long packingweightid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "packingweight/" + packingweightid);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<PackingWeightDetails>(content);
        //    return result;
        //}

        //public async Task<List<PackingWeightDetails>> Get(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "packingweight/Get/?search=" + search);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<PackingWeightDetails>>(content);
        //    return result;
        //}

        public async Task<long> GetNewPackingWeightID()
        {
            var response = await client.GetAsync(apiServiceUrl + "packingweight/GetNewid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetPackingNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> IsPackingWeightExist(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packingweight/IsPackingWeightExist/" + packingid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<PackingWeightDetails>> GetPackingWeightDetails(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packingweight/GetPackingWeightDetails/" + packingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<PackingWeightDetails>>(content);
            return result;
        }

        public async Task<List<ViewPackingCartons>> GetPackingCartonDetails(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetCartonList/" + packingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingCartons>>(content);
            return result;
        }

        public async Task<string> Post(List<PackingWeightDetails> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PackingWeight", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(List<PackingWeightDetails> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "PackingWeight", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> ChangeStatus(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/ChangeStatus/" + packingid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


    }
}
