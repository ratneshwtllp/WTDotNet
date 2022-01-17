using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
namespace ERP.Business
{
    public class IuuseRMForChange : IRMForChangeContract 
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public IuuseRMForChange(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
        public async Task<string> DeleteIssueRMchange(long issuechangeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/Delete/" + issuechangeid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> UpdateIssueRM(long IssueRMChangeChildID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/UpdateIssueRM/" + IssueRMChangeChildID);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewIssueRMForChange >> GetViewIssueRMForChange(long IssueRMChangeID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetViewIssueRmForChange/" + IssueRMChangeID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChange>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMForChangeMaster>> GetViewIssueRMForChangeMaster(long IssueRMChangeID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetViewIssueRMForChangeMaster/" + IssueRMChangeID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChangeMaster>>(content);
            return result;
        }
        public async Task<List<ViewIssueRMForChange>> GetViewIssueRMForChange(SearchDetails SD)
        {
            var myContent = JsonConvert.SerializeObject(SD);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "IssueRMForChange/ViewIssueRmForChange", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChange>>(content);
            return result;
        }

        public async Task<List<RecvJW>> GetRecvJW(long IssueRMChangeID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetRecvJW/" + IssueRMChangeID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RecvJW>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMForChangeMaster>> GetMaster()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetMaster");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChangeMaster>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMForChangeMaster>> GetMasterCompleted()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetMasterCompleted");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChangeMaster>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMForChangeMaster>> GetBalanceList()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetBalanceList");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewIssueRMForChangeMaster>>(content);
            return result;
        }

        public async Task<long> GetNewIssueRMChangeID()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetNewIssueRMChangeID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewRecvJWID()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetNewRecvJWID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewIssueRMChangeChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetNewIssueRMChangeChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<long> GetNewIssueRMChangeItemID()
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetNewIssueRMChangeItemID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<long> GetIssueRMChangeSerial(int jwtype)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetIssueRMChangeSerial/" + jwtype);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetIssueRMChangeNo(int jwtype)
        {
            try
            {
                var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetIssueRMChangeNo/" + jwtype);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Post(IssueRMforChangeMaster  value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "IssueRMForChange", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PostJW(RecvJW value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "IssueRMForChange/PostJW", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMSubCategoryList(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSubCategory/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(int scatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + scatid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMListFromSupplier(long SupplierId)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMListFromSupplier/" + SupplierId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSupplierList(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetSupplierList/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRackListFromRitemNSupplierid(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetRackListFromRitemNSupplierid/" + ritemid + "/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRackNoList(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetRackNoList/" + supplierid + "/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetQtyOnRack(long ritemid, long supplierid,long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetQtyOnRack/" + ritemid + "/" + supplierid+"/"+ rackid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRitemDetails(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/RItemCurrentStockShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> IsComplete(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/IsComplete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> RMSuppRate(long rmid, long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMSupplier/" + rmid + "/" + suppid);
            var content = await response.Content.ReadAsStringAsync();

            return (content);
        }

        public async Task<SupplierDetails> GetSupplierDetails(long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Supplier/GetSupplierDetails/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SupplierDetails>(content);
            return result;
        }

        public async Task<string> GetCurrencyofSupplier(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "po/GetCurrencyofSupplier/" + suppid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         
        public async Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetBatch_LotNoFromRM_Supp_Rack/" + ritemid + "/" + suppid + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //public async Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStockFromBatchLotNo/" + rowidlotid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Convert.ToDouble(content); ;
        //}

        public async Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStock/" + ritemid + "/" + suppid + "/" + rackid + "?lotno=" + lotno.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content); ;
        }

        public async Task<string> GetRMLotStock(long ritemid, long suppid, long rackid, string lotno)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMLotStock/" + ritemid + "/" + suppid + "/" + rackid + "?lotno=" + lotno.ToString());
            var content = await response.Content.ReadAsStringAsync();

            //var result = JsonConvert.DeserializeObject<ViewRMStockRackWise>(content);
            return content;
        }


        public async Task<string> GetRTSNo()
        {
            try
            {
                var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/GetRTSNo" );
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> PostRTS(ReturnToSupplier value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "IssueRMForChange/PostRTS", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewRTS>> ViewRTS(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/ViewRTS/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRTS>>(content);
            return result;
        }

        public async Task<List<ViewRTS>> ViewRTS(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "IssueRMForChange/ViewRTS", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRTS>>(content);
            return result;
        }

        public async Task<string> GetLastPO(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetLastPO/" + ritemid + "/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutRTSApproved(long RTSID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/PutRTSApproved/" + RTSID);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PutRTSDecline(long RTSID)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssueRMForChange/PutRTSDecline/" + RTSID);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetShortSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetShortSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
