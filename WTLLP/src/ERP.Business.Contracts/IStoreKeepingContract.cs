using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IStoreKeepingContract
    {
        Task<List<StoreKeepingMaster>> Get();
        Task<StoreKeepingMaster> Get(long Grnid);

        //Task<List<ViewGRNMaster>> GetViewGRNMaster(string search);  
        //Task<List<ViewGRNMaster>> GetViewGRNMasterForCancelList();  
        //Task<List<ViewGRNChild>> GetGRNChild(long Grnid);

        Task<long> GetNewStoreKeepingID();
        Task<long> GetNewStoreKeepingChildID();

        Task<long> GetNewStoreKeepingSerial();
        Task<string> GetNewStoreKeepingNO();

        Task<string> GetStoreList();
        Task<string> GetGRNList();
        Task<string> GetRackNoList(long suppid, long rmid);
        Task<string> GetStoreRackList(long storeid);

        Task<List<ViewGRNForStoreKeeping>> GetViewGRNForStoreKeeping(long grnid);
        Task<ViewGRNMaster> GetViewGRNMaster(long grnid);

        Task<string> Post(StoreKeepingMaster value);
        Task<string> Put(StoreKeepingMaster value);

        Task<string> PutStoreKeepingStatus(long grnid);
        //Task<string> PutGRNStatus(long grnid);

        Task<string> Delete(long id);
        Task<List<ViewStoreKeeping>> GetStoreKeepingList(StoreSearch obj);
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<string> GetSupplierList();

        Task<string> GetNewRTSGRNNo();
        Task<string> PostRTSGRN(RTSGRNMaster value);
        Task<string> GetShortSessionYearList();
        Task<List<ViewRTSGRN>> GetRTSGRNList(StoreSearch obj);
    }
}