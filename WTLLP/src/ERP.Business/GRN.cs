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
    public class GRN : IGRNContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public GRN(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<ViewGRNMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRNMaster>>(content);
            return result;
        }

        public async Task<GRNMaster> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GRNMaster>(content);
            return result;
        }

        public async Task<List<ViewGRN>> GetList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetList/?search=" + search); 
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRN>>(content);
            return result;
        }

        public async Task<List<ViewGRN>> GetListCancel(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetListCancel/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRN>>(content);
            return result;
        }

        public async Task<long> GetNewGRNID()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetNewGRNID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewGRNChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetNewGRNChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewGRNSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetNewGRNSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetNewGRNNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetNewGRNNO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }



        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPONoList(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetPONoList/" + suppid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }



        public async Task<List<ViewPOForGRN>> GetPoChildRecord(long poid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetPoChildRecord/" + poid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOForGRN>>(content);
            return result;
        }

        public async Task<List<ViewPOForGRN>> GetMultiPOChildRecord(long[] poids)
        {
            string searchkey = "?";
            for (int i = 0; i < poids.Length; i++)
            {
                searchkey += "poids=" + poids[i].ToString() + "&";
            }
            var response = await client.GetAsync(apiServiceUrl + "grn/GetMultiPOChildRecord/" + searchkey.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOForGRN>>(content);
            return result;
        }

        public async Task<string> Post(GRNMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "grn", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(GRNMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "grn", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutGRNStatus(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/PutGRNStatus/" + grnid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutStoreKeepingStatus(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/PutStoreKeepingStatus/" + grnid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetNewGRNFileId()
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetNewGRNFileId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //public async Task<string> PostGRNFile(List<GRNFile> value)
        //{
        //    var myContent = JsonConvert.SerializeObject(value);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PostAsync(apiServiceUrl + "grn/PostGRNFile", byteContent);
        //    var content = response.Content.ReadAsStringAsync();
        //    return (content.Result);
        //}

        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "SessionYear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewGRN>> GetGRNList(GRNSearch GS)
        {
            var myContent = JsonConvert.SerializeObject(GS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "GRN/GetGRNList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRN>>(content);
            return result;
        }

        public async Task<List<GRNMaster>> CheckBillChallan(GRNSearch GS)
        {
            //SupplierID
            //BillNo
            //ChallanNo
            var myContent = JsonConvert.SerializeObject(GS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "GRN/CheckBillChallan", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<GRNMaster>>(content);
            return result;
        }

        public async Task<List<ViewGRN>> GetGRNPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "GRN/GetGRNForPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRN>>(content);
            return result;
        }

        public async Task<int> GetIsBatchOfRM()
        {
            var response = await client.GetAsync(apiServiceUrl + "FormSetting/GetIsBatchOfRM");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<ViewGRNFile>> GetGRNFileRecord(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GetGRNFileRecord/" + grnid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRNFile>>(content);
            return result;
        }

        public async Task<string> GRNDocumentTypeList(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "DocumentType/GRNDocumentTypeList/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostGRNFile(GRNFile value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "grn/PostGRNFile", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> DeleteGRNFile(long id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "grn/DeleteGRNFile/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GRNFileLocation(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/GRNFileLocation/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToString(content);
        }

        //public async Task<string> GetRMCategoryList()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "RMCategory/GetList");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<string> GetRMCategoryList(long suppid)
        //{

        //    var response = await client.GetAsync(apiServiceUrl + "PO/GetRMCategoryList/" + suppid.ToString());
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<string> GetRMSubCategoryList(long suppid, long catid)
        //{

        //    var response = await client.GetAsync(apiServiceUrl + "PO/GetRMSubCategoryList/" + suppid.ToString() + "/" + catid.ToString());
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<string> GetRMList(long suppid, long scatid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "PO/GetRMList/" + suppid.ToString() + "/" + scatid.ToString());
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        public async Task<string> GetRMList(long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMListFromSupplier/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMDetails(int ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetListBranch()
        {
            var response = await client.GetAsync(apiServiceUrl + "branch/GetListBranch");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetJWNoList(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetJWNoList/" + suppid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewJWForGRN>> GetMultiJWChildRecord(long[] issuermchangeid)
        {
            string searchkey = "?";
            for (int i = 0; i < issuermchangeid.Length; i++)
            {
                searchkey += "issuermchangeid=" + issuermchangeid[i].ToString() + "&";
            }
            var response = await client.GetAsync(apiServiceUrl + "grn/GetMultiJWChildRecord/" + searchkey.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewJWForGRN>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMForChange>> GetMultiJWIssueRMList(long[] issuermchangeid)
        {
            string searchkey = "?";
            for (int i = 0; i < issuermchangeid.Length; i++)
            {
                searchkey += "issuermchangeid=" + issuermchangeid[i].ToString() + "&";
            }
            var response = await client.GetAsync(apiServiceUrl + "grn/GetMultiJWIssueRMList/" + searchkey.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChange>>(content);
            return result;
        }

        public async Task<string> GetPONoListAgainstDate(GRNSearch search)
        {
            var myContent = JsonConvert.SerializeObject(search);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "grn/GetPONoListAgainstDate", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetJWNoListAgainstDate(GRNSearch search)
        {
            var myContent = JsonConvert.SerializeObject(search);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "GRN/GetJWNoListAgainstDate", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<GRNMaster> GetGRNNo_Session(GRNMaster _oGRNMaster)
        {
            var myContent = JsonConvert.SerializeObject(_oGRNMaster);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "GRN/GetGRNNo_Session", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GRNMaster>(content);
            return result;
        }

        public async Task<ViewSupplierDetails> GetSupplierDetails(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierDetails/" + suppid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewSupplierDetails>(content);
            return result;
        }

        public async Task<ViewBranchDetails> GetBranchDetails(long branchid)
        {
            var response = await client.GetAsync(apiServiceUrl + "branch/GetBranchDetails/" + branchid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewBranchDetails>(content);
            return result;
        }

        public async Task<string> GetTaxList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/GetTaxList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
