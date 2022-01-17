
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStrucureContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewStructureId();
        Task<string> Post(StructureDetails value);
        Task<string> Put(StructureDetails value);
        Task<List<StructureDetails>> Get();
        Task<StructureDetails> Get(int id);
        Task<List<StructureDetails>> GetStructureList(string search);
        Task<int> CheckDuplicate(string value);
    }
}