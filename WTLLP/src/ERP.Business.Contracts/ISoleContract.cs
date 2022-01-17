using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISoleContract
    {
        Task<List<SoleMaster>> Get();
        Task<SoleMaster> Get(int id);
        Task<List<SoleMaster>> GetSoleList(string search);
        Task<int> GetNewSoleId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(SoleMaster value);
        Task<string> Put(SoleMaster value);
        Task<int> Delete(int id);
    }
}