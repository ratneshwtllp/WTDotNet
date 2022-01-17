
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IEmbossmentContract
    {
        Task<List<EmbossmentDetails>> Get();
        Task<EmbossmentDetails> Get(int id);
        Task<List<EmbossmentDetails>> GetEmbossmentList(string search);
        Task<int> GetNewEmbossmentId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(EmbossmentDetails value);
        Task<string> Put(EmbossmentDetails value);
        Task<int> Delete(int id);
    }
}