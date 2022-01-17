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
    public class PO : IPOContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;        
        public PO(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Post(POMaster value )
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(POMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "PO", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetRMCategoryList(long suppid)
        {

            var response = await client.GetAsync(apiServiceUrl + "PO/GetRMCategoryList/" + suppid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMSubCategoryList(long suppid, long catid)
        {

            var response = await client.GetAsync(apiServiceUrl + "PO/GetRMSubCategoryList/" + suppid.ToString() + "/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(long suppid, long scatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetRMList/" + suppid.ToString() + "/" + scatid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMListFromSupplier(long SupplierId)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMListFromSupplier/" + SupplierId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMDetails(int ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<double> GetTaxRate(int taxid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Tax/GetTaxRate/" + taxid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content);
        }

        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> FillTax()
        {
            var response = await client.GetAsync(apiServiceUrl + "Tax/filltax");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetNewPONO()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetNewPONO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewPOSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetNewPOSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<long> GetNewPOID()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetNewPOID");
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt64(content); 
        }

        public async Task<long> GetNewPOChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetNewPOChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<POMaster> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<POMaster>(content);
            return result;
        }

        public async Task <List<ViewPOChild>> GetPOChild(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetPOChild/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOChild>>(content);
            return result;
        }

        public async Task<List<ViewPOPrint>> GetListCancel(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetListCancel/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOPrint>>(content);
            return result;
        }

        public async Task<List<ViewPOPrint>> GetPOForPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetPOForPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOPrint>>(content);
            return result;
        }

        public async Task<string> RMSuppRate(long rmid, long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMSupplier/" + rmid + "/" + suppid);
            var content = await response.Content.ReadAsStringAsync();

            return (content);
        }

        public async Task<string> GetCurrencyofSupplier(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetCurrencyofSupplier/" + suppid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutPOStatus(long poid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/PutPOStatus/" + poid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutPOEmailStatus(long poid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/PutPOEmailStatus/" + poid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //emails
        public async Task<long> GetNewEmailSendID()
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetNewEmailSendID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostEmailSend(EmailSendDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "emails/PostEmailSend", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewSendEmail> GetViewSendEmail(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "emails/GetViewSendEmail/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewSendEmail>(content);
            return result;
        }

        public async Task<List<TempPOChild>> GetTempPOChild()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetTempPOChild");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TempPOChild>>(content);
            return result;
        }

        public async Task<ViewRItemShow> RItemShow(int ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/RItemShow/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewRItemShow>(content);
            return result;
        }

        public async Task<List<ViewPOBalance>> GetPOBalanceList(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/GetPOBalanceList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOBalance>>(content);
            return result;
        }

        public async Task<List<ViewPODelay>> PODelayReport()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/PODelayReport");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPODelay>>(content);
            return result;
        }

        public async Task<List<ViewPODelaySum>> PODelaySum()
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/PODelaySum");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPODelaySum>>(content);
            return result;
        }

        public async Task<List<ViewSupplierPerformance>> GetSupplierPerformanceList(long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetSupplierPerformanceList/?supplierid=" + supplierid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSupplierPerformance>>(content);
            return result;
        }

        public async Task<List<ViewSupplierPerformanceSumarray>> GetSupplierAverageDelayList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/GetSupplierAverageDelayList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSupplierPerformanceSumarray>>(content);
            return result;
        }
        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "SessionYear/GetShortSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewPOPrint>> ViewPo(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/ViewPO", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOPrint>>(content);
            return result;
        }

        public async Task<string> GetCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSubCategoryList(long categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/RMSubCategory/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetListPOPT()
        {
            var response = await client.GetAsync(apiServiceUrl + "popt/GetListPOPT");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetListPODT()
        {
            var response = await client.GetAsync(apiServiceUrl + "podt/GetListPODT");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetListPOT()
        {
            var response = await client.GetAsync(apiServiceUrl + "pot/GetListPOT");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetListBranch()
        {
            var response = await client.GetAsync(apiServiceUrl + "branch/GetListBranch");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewBranchDetails> GetBranchDetails(long branchid)
        {
            var response = await client.GetAsync(apiServiceUrl + "branch/GetBranchDetails/" + branchid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewBranchDetails>(content);
            return result;
        }

        public async Task<ViewSupplierDetails> GetSupplierDetails(long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierDetails/" + suppid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewSupplierDetails>(content);
            return result;
        }

        public async Task<POMaster> GetPONO_Session(POMaster _oPOMaster)
        {
            var myContent = JsonConvert.SerializeObject(_oPOMaster);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/GetPONO_Session", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<POMaster>(content);
            return result;
        }

        public async Task<string> GetBuyerCodeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerCodeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSaleOrderList(long BuyerId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Saleorder/GetOrderNoList/" + BuyerId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        #region PO
        public async Task<List<ViewRunningPOList>> GetRunningPOList(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/GetRunningPOList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRunningPOList>>(content);
            return result;
        }

        public async Task<string> RunningPOListComplete(long poid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/RunningPOListComplete/" + poid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutPOCompleteStatus(List<POMaster> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/PutPOCompleteStatus", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewPOPrint>> GetCompletedPOList(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/GetCompletedPOList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPOPrint>>(content);
            return result;
        }

        public async Task<string> UndoCompletedPOList(long poid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PO/UndoCompletedPOList/" + poid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewDelayedMaterial>> GetDelayedMaterialList(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PO/GetDelayedMaterialList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewDelayedMaterial>>(content);
            return result;
        }
        #endregion
    }
}
