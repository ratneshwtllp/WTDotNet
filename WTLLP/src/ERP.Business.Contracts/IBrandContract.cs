
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IBrandContract
    {
        Task<List<BrandDetails>> Get();
        Task<BrandDetails> Get(int id);
        Task<string> GetBuyerList();
        Task<List<ViewBrandDetails>> GetBrandList(string search);
        Task<int> GetNewBrandId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(BrandDetails value);
        Task<string> Put(BrandDetails value);
        Task<int> Delete(int id);
        
    }
}