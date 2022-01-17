using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IInvoiceContract
    {
        Task<List<InvoiceMaster>> Get();
        Task<ViewInvoice> Get(long invoiceid);
        Task<List<ViewInvoice>> Get(string search);
        Task<List<ViewInvoiceChild>> GetChild(long invoiceid);

        Task<long> GetNewInvoiceID();
        Task<long> GetNewInvoiceChildID();
        Task<long> GetNewInvoiceSerial();
        Task<string> GetNewInvoiceNO();

        Task<string> GetConsigneeNameList();
        Task<string> FOBCIFList();
        Task<string> GetPendingPackingNoList();
        Task<string> GetSinglePackingNo(long packingid);

        Task<ConsigneeDetails> GetConsigneeRecord(long consigneeid);
        Task<List<ViewPackingWeight>> GetNoofCartonsByDimension(long pckid);
        Task<ViewPackingMaster> GetBuyerFromPacking(long pckid);
        Task<List<ViewPackingChild>> GetRecordForInvoice(long pckid);
        Task<List<ViewPackingChild>> GetRecord(long pckid);
        Task<List<PackingWeightDetails>> GetPackingWeightDetails(long packingid);

        Task<string> Post(InvoiceMaster value);
        Task<string> Put(InvoiceMaster value);
        Task<string> Cancel(long invoiceid);
        Task<string> Delete(long invoiceid);
        Task<List<ViewInvoicePrint>> GetInvoicePrint(long invoiceid);

        Task<List<ViewInvoice>> ViewInvoice(InvoiceSearch IS);
        Task<string> GetBuyerList();
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetFinalDestinationList();
        Task<string> GetCurrencyList();
        Task<string> GetSessionYearList();
    }
}