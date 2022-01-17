
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IWaxContract
    {
        Task<List<WaxDetails>> Get();
        Task<WaxDetails> Get(int id);
        Task<List<WaxDetails>> GetWaxList(string search);
        Task<int> GetNewWaxId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(WaxDetails value);
        Task<string> Put(WaxDetails value);
        Task<int> Delete(int id);
        
    }
}