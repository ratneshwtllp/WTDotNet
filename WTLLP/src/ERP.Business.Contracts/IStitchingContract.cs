
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStitchingContract
    {
        Task<List<StitchingDetails>> Get();
        Task<StitchingDetails> Get(int id);
        Task<List<StitchingDetails>> GetStitchingList(string search);
        Task<int> GetNewStitchingId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(StitchingDetails value);
        Task<string> Put(StitchingDetails value);
        Task<int> Delete(int id);
    }
}