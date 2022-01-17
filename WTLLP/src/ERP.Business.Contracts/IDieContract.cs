
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IDieContract
    {
        Task<List<DieDetails>> Get();
        Task<DieDetails> Get(int id);
        Task<List<DieDetails>> GetDieList(string search);
        Task<List<ProductDetails>> GetFitemList();
        Task<List<ProductDetails>> SearchProductChekedList(SearchModel obj);
        Task<string> SearchProductList(SearchModel obj);

        Task<List<DieFitem>> GetDieFitemList(long Dieid);
        Task<int> GetNewDieId();
        Task<int> GetNewDieFitemId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(DieDetails value);
        Task<string> Put(DieDetails value);
        Task<string> PutDieFitem(List<DieFitem> value);
        Task<int> Delete(int id);

        Task<List<ViewDieFitemDetails>> GetViewDieFitemList(long dieid);
        Task<string> RemoveDieFitem(List<DieFitem> value);
    }
}