
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStateContract
    {
        Task<List<ViewStateDetails>> Get();
        Task<StateDetails> Get(int id);
        Task<string> GetCountryList();
        Task<List<ViewStateDetails>> GetStateList(string search);
        Task<int> GetNewStateId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(StateDetails value);
        Task<string> Put(StateDetails value);
        Task<int> Delete(int id);
        
    }
}