
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IProductionProcessContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewProductionProcessId();
        Task<string> Post(ProductionProcessDetails  value);
        Task<string> Put(ProductionProcessDetails value);
        Task<List<ProductionProcessDetails>> Get();
        Task<ProductionProcessDetails> Get(int id);
        Task<List<ProductionProcessDetails>> GetProductionProcessList(string search);
        Task<int> CheckDuplicate(string value);
    }
}