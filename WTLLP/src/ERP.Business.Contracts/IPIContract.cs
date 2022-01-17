using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IPIContract
    {
        Task<List<PIMaster>> Get();
        Task<ViewPI> Get(long packingid);
        Task<List<ViewPI>> Get(string search);
        Task<List<ViewPIChild>> GetChild(long pimasterid);

        Task<long> GetNewPIMasterID();
        Task<long> GetNewPIChildID(); 
        Task<long> GetNewPISerial();
        Task<string> GetNewPINO();
         
        Task<string> GetConsigneeNameList();
        Task<string> FOBCIFList();
        Task<string> GetBuyerNameList();   //buyer
        Task<string> GetOrderNoListForPI(long buyerid);    
        Task<string> GetSingleOrderNo(long orderid);   

        Task<ViewBuyerDetails> GetBuyerDetails(long buyerid);
        Task<ConsigneeDetails> GetConsigneeRecord(long consigneeid);
        Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid);

        Task<string> Post(PIMaster value);
        Task<string> Put(PIMaster value);
        Task<string> Cancel(long issueid); 
        Task<string> Delete(long packingid);
        Task<List<ViewPIPrint>> GetPIPrint(long piid);



    }
}