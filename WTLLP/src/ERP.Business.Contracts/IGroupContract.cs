
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IGroupContract
    {
        Task<List<GroupDetails>> Get();
        Task<GroupDetails> Get(int id);
        Task<List<GroupDetails>> GetGroupList(string search);
        Task<int> GetNewGroupId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(GroupDetails value);
        Task<string> Put(GroupDetails value);
        Task<int> Delete(int id);
        
    }
}