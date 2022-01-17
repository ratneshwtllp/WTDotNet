
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IAdhesiveContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewAdhesiveId();
        Task<string> Post(AdhesiveDetails value);
        Task<string> Put(AdhesiveDetails value);
        Task<List<AdhesiveDetails>> Get();
        Task<AdhesiveDetails> Get(int id);
        Task<List<AdhesiveDetails>> GetAdhesiveList(string search);
        Task<int> CheckDuplicate(string value);        
    }
}