using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IConstructionContract
    {
        Task<List<ConstructionDetails>> Get();
        Task<ConstructionDetails> Get(int id);
        //Task<string> GetBuyerList();
        Task<List<ConstructionDetails>> GetConstructionList(string search);
        Task<string> GetListConstruction();
        Task<int> GetNewConstructionId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(ConstructionDetails value);
        Task<string> Put(ConstructionDetails value);
        Task<int> Delete(int id);
        
    }
}