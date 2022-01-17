using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ERP.Business
{
    public class CompanyBeneficiary : ICompanyBeneficiaryContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public CompanyBeneficiary(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        //public async Task<List<ViewHSNDetails>> Get()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "HSN");
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewHSNDetails>>(content);
        //    return result;
        //}

        //public async Task<string> GetTaxList()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "HSN/GetTaxList");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        public async Task<CompanyBeneficiaryBankDetails> GetCompanyBeneficiaryBankDetails(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Packing/GetCompanyBeneficiaryBankDetails/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CompanyBeneficiaryBankDetails>(content);
            return result;
        }


        public async Task<int> DeleteCompanyBeneficiary(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Packing/DeleteCompanyBeneficiary/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GetBankNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "bank/GetBankNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCompanyList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Company/GetCompanyList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<int> GetNewBeneficiaryBankID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Packing/GetNewBeneficiaryBankID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(CompanyBeneficiaryBankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Packing/PostCompanyBeneficiaryBankDetails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(CompanyBeneficiaryBankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Packing/PutCompanyBeneficiaryBankDetails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewCompanyBeneficiaryBankDetails>> GetCompanyBeneficiaryList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Packing/GetCompanyBeneficiaryList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCompanyBeneficiaryBankDetails>>(content);
            return result;
        }

    }
}

