
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICostingElementContract
    {
        Task<List<CostingElementDetails>> Get();
        Task<CostingElementDetails> Get(int id);
        Task<List<CostingElementDetails>> GetCostingElementList(string search);
        Task<int> GetNewCostingElementId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(CostingElementDetails value);
        Task<string> Put(CostingElementDetails value);
        Task<int> Delete(int id);
    }
}