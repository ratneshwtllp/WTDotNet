using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace ERP.Business
{
    public class DashBoard : IDashBoard
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;
        public DashBoard(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<ViewDashBoardBuyerLastOrder>> GetViewDashBoardBuyerLastOrder(OrderSearch Obj)
        {
            var myContent = JsonConvert.SerializeObject(Obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "DashBoard/GetViewDashBoardBuyerLastOrder", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewDashBoardBuyerLastOrder>>(content);
            return result;
        }

        public async Task<List<ViewDashBoardOrderToBeDeliver>> GetViewDashBoardOrderToBeDeliver(OrderSearch Obj)
        {
            var myContent = JsonConvert.SerializeObject(Obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "DashBoard/GetViewDashBoardOrderToBeDeliver", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewDashBoardOrderToBeDeliver>>(content);
            return result;
        }
    }
}
