
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IUserRightContract
    {
        Task<string> GetUserList();
        Task<List<ViewUserRights>> GetmenuList(int menucatid);
        Task<string> GetMenuCatList();
        Task<List<MenuDetails>> SearchMenuChekedList(int menucatid);
        Task<int> GetNewId();
        Task<string> PostUserMenu(UserRights value);
        Task<List<ViewUserRights>> GetViewMenuList(int userid);
        Task<string> RemoveMenu(UserRights value);
    }
}