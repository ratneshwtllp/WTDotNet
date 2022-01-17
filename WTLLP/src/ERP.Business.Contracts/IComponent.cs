
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IComponent
    {
        Task<int> Delete(int id);
        Task<int> GetNewId();
        Task<string> Post(ComponentDetails  value);
        Task<string> Put(ComponentDetails value);
        Task<List<ComponentDetails>> Get();
        Task<ViewComponentDetails> Get(int id);
        Task<List<ViewComponentDetails >> GetComponentList(string search);
        Task<int> CheckDuplicate(string value);
        Task<string> ComponentGroupList();
    }
}