
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IUserContract
    {
        Task<List<UserDetails>> Get();
        Task<UserDetails> Get(int id);
        Task<int> GetId(string name, string psd);
        Task<List<ViewUserDetails>> GetUserList(string search);
        Task<string> GetDepartmentList();
        Task<int> GetNewUserId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(UserDetails value);
        Task<string> Put(UserDetails value);
        Task<int> Delete(int id);
        Task<string> GetUserTypeList();
        Task<ViewUserDetails> GetUserDetails(int userid);
    }
}