
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IComponentGroup
    {
        Task<int> Delete(int id);
        Task<int> GetNewId();
        Task<string> Post(ComponentGroupDetails  value);
        Task<string> Put(ComponentGroupDetails value);
        Task<List<ComponentGroupDetails>> Get();
        Task<ComponentGroupDetails> Get(int id);
        Task<List<ComponentGroupDetails>> GetComponentGroupList(string search);
        Task<int> CheckDuplicate(string value);
    }
}