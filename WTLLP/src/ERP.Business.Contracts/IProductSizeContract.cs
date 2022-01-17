
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IProductSizeContract
    {
        Task<List<ProductSizeDetails>> Get();
        Task<ProductSizeDetails> Get(int id);
        Task<List<ProductSizeDetails>> GetProductSizeList(string search);
        Task<int> GetNewProductSizeId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(ProductSizeDetails value);
        Task<string> Put(ProductSizeDetails value);
        Task<int> Delete(int id);
    }
}