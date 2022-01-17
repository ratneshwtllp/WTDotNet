
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IFinishMetalContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewFinishMetalId();
        Task<string> Post(FinishMetalDetails value);
        Task<string> Put(FinishMetalDetails value);
        Task<FinishMetalDetails> Get(int id);
        Task<List<FinishMetalDetails>> GetFinishMetalList(string search);
        Task<int> CheckDuplicate(string value);
    }
}