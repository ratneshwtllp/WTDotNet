
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IProductCategoryContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewProductCategoryId();
        Task<string> Post(ProductCategoryDetails value);
        Task<string> Put(ProductCategoryDetails value);
        Task<List<ProductCategoryDetails>> Get();
        Task<ProductCategoryDetails> Get(int id);
        Task<List<ViewProductCategory >> GetProductCategoryList(string search);
        Task<int> CheckDuplicate(string value);
        Task<string> GetBuyerList();
        Task<string> GetPositionList();
        Task<string> GetModifyPositionList();
    }
}