using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IMenuContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewMenuId();
        Task<string> Post(MenuDetails value);
        Task<string> Put(MenuDetails value);
        Task<List<ViewMenu >> Get();
        Task<MenuDetails> Get(int id);
        Task<List<ViewMenu>> GetMenuDetailsList(string search);
        Task<int> CheckDuplicate(string value);
        Task<string> MenuCategoryNameList();
        Task<int> GetNewId();
        Task<List<ViewMenu>> GetList(string search);
    }
}