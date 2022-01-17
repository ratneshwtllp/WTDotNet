
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ILiningContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewLiningId();
        Task<string> Post(LiningDetails value);
        Task<string> Put(LiningDetails value);
        Task<List<LiningDetails>> Get();
        Task<LiningDetails> Get(int id);
        Task<List<LiningDetails>> GetLiningList(string search);
        Task<int> CheckDuplicate(string value);
    }
}