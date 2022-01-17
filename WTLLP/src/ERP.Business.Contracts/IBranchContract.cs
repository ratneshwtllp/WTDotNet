
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IBranchContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewBranchId();
        Task<string> Post(BranchDetails  value);
        Task<string> Put(BranchDetails  value);
        Task<List<BranchDetails >> Get();
        Task<BranchDetails> Get(int id);
        Task<List<BranchDetails >> GetBranchList(string search);
        Task<int> CheckDuplicate(string value);
        Task<string> GetListState();
        Task<string> GetListCity(int stateid);
    }
}