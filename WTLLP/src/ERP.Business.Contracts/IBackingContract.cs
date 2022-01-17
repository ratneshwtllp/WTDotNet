
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IBackingContract
    {
        Task<List<BackingDetails>> Get();
        Task<BackingDetails> Get(int id);
        Task<List<BackingDetails>> GetBackingList(string search);
        Task<int> GetNewBackingId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(BackingDetails value);
        Task<string> Put(BackingDetails value);
        Task<int> Delete(int id);        
    }
}