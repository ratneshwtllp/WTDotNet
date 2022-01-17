using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISampleRoomContract
    {
        Task<List<SampleRoomDetails>> Get();
        Task<ViewSampleRoom> Get(int id);
        Task<List<ViewSampleRoom>> GetSampleRoomList(string search);
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetProductDetails(long fitemid); 
        Task<string> GetSampleRoomTypeList();
        Task<int> GetNewSampleRoomId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(SampleRoomDetails value);
        Task<string> Put(SampleRoomDetails value);
        Task<int> Delete(int id);
    }
}