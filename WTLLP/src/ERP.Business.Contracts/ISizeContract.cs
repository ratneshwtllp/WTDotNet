
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISizeContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewSizeId();
        Task<string> Post(SizeDetails value);
        Task<string> Put(SizeDetails value);
        Task<List<SizeDetails>> Get();
        Task<SizeDetails> Get(int id);
        Task<List<SizeDetails>> GetSizeList(string search);
        Task<int> CheckDuplicate(string value);
    }
}