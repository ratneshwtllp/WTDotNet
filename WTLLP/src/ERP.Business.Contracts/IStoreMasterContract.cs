using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStoreMasterContract
    {
        Task<List<StoreMasterDetails>> Get();
        Task<StoreMasterDetails> Get(int id);
        Task<List<StoreMasterDetails>> GetStoreMasterList(string search);
        Task<int> GetNewStoreMasterId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(StoreMasterDetails value);
        Task<string> Put(StoreMasterDetails value);
        Task<int> Delete(int id);

    }
}