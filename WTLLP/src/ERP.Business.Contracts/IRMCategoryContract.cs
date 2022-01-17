
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMCategoryContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewRMCategoryId();
        Task<string> Post(RMCategory value);
        Task<string> Put(RMCategory value);
        Task<List<RMCategory>> Get();
        Task<RMCategory> Get(int id);
        Task<List<RMCategory>> GetRMCategoryList(string search);
        Task<int> CheckDuplicate(string value);
    }
}