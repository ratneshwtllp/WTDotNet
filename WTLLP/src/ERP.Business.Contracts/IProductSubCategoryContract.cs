
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IProductSubCategoryContract
    {
        Task<int> Delete(int id);
        Task<long> GetNewProductSubCategoryId();

        Task<string> MPost(ProductSubCategoryDetails value);
        Task<string> MPut(ProductSubCategoryDetails value);

        Task<List<ProductSubCategoryDetails>> Get();
        Task<ProductSubCategoryDetails> Get(int id);
        Task<List<ViewProduct>> GetProductSubCategoryList(string search);
        Task<int> CheckDuplicate(string value,long ProductCategoryID);
        Task<int> CheckDuplicate(string value);
        Task<string> ProductCategoryList();
        Task<string> GetMultiLevelSubCat();

        Task<string> Post(ProductSubCategoryDetails value);
        Task<string> Put(ProductSubCategoryDetails value);
    }
}