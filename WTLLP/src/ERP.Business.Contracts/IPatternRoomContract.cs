using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPatternRoomContract
    {
        Task<List<PatternRoomDetails>> Get();
        Task<ViewPatternRoomDetails> Get(int id);
        Task<List<ViewPatternRoomDetails>> GetPatternRoomList(string search);
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetProductDetails(long fitemid); 
        Task<int> GetNewPatternRoomId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(PatternRoomDetails value);
        Task<string> Put(PatternRoomDetails value);
        Task<int> Delete(int id);
    }
}