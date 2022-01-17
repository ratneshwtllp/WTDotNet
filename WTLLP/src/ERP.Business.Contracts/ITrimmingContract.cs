
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ITrimmingContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewTrimmingId();
        Task<string> Post(TrimmingDetails value);
        Task<string> Put(TrimmingDetails value);
        Task<List<TrimmingDetails>> Get();
        Task<TrimmingDetails> Get(int id);
        Task<List<TrimmingDetails>> GetTrimmingList(string search);
        Task<int> CheckDuplicate(string value);
    }
}