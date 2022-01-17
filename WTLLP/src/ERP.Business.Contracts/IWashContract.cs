
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IWashContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewWashId();
        Task<string> Post(WashDetails  value);
        Task<string> Put(WashDetails value);
        Task<List<WashDetails>> Get();
        Task<WashDetails> Get(int id);
        Task<List<WashDetails>> GetWashList(string search);
        Task<int> CheckDuplicate(string value);
    }
}