
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IUnitContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewUnitId();
        Task<string> Post(UnitDetails value);
        Task<string> Put(UnitDetails value);
        Task<List<UnitDetails>> Get();
        Task<UnitDetails> Get(int id);
        Task<List<UnitDetails>> GetUnitList(string search);
        Task<int> CheckDuplicate(string value);
    }
}