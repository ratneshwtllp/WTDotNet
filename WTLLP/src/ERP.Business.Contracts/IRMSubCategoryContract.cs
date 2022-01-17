
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMSubCategoryContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewRMSubCategoryId();
        Task<string> Post(RMSubCategory value);
        Task<string> Put(RMSubCategory value);
        Task<List<RMSubCategory>> Get();
        Task<RMSubCategory> Get(int id);
        Task<List<ViewRMSubCategory>> GetRMSubCategoryList(string search);
        Task<string> GetCategoryList();
        Task<string> GetHSNList();
        Task<int> CheckDuplicate(string value);
    }
}