
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IClothContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewClothId();
        Task<string> Post(ClothDetails value);
        Task<string> Put(ClothDetails value);
        Task<List<ClothDetails>> Get();
        Task<ClothDetails> Get(int id);
        Task<List<ClothDetails>> GetClothList(string search);
        Task<int> CheckDuplicate(string value);
    }
}