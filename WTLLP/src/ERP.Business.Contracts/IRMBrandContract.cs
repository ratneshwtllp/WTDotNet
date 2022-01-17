using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMBrandContract
    {
        Task<List<RMBrandDetails>> Get();
        Task<RMBrandDetails> Get(int id);
        //Task<string> GetBuyerList();
        Task<List<RMBrandDetails>> GetRMBrandList(string search);
        Task<int> GetNewRMBrandId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(RMBrandDetails value);
        Task<string> Put(RMBrandDetails value);
        Task<int> Delete(int id);
    }
}