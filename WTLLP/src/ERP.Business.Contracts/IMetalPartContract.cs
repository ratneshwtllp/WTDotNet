
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IMetalPartContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewMetalPartId();
        Task<string> Post(MetalPartDetails value);
        Task<string> Put(MetalPartDetails value);
        Task<List<MetalPartDetails>> Get();
        Task<MetalPartDetails> Get(int id);
        Task<List<MetalPartDetails>> GetMetalPartList(string search);
        Task<int> CheckDuplicate(string value);
    }
}