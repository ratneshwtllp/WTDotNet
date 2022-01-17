using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IAllowanceContract
    {
        Task<List<HR_AllowanceDetails>> Get();
        Task<HR_AllowanceDetails> Get(int id);
        Task<List<HR_AllowanceDetails>> GetList(string search);
        Task<int> GetNewId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_AllowanceDetails value);
        Task<string> Put(HR_AllowanceDetails value);
        Task<int> Delete(int id);
    }
}