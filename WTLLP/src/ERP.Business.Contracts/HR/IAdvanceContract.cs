
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IAdvanceContract
    {
        Task<string> GetEmployeeList();
        Task<List<HR_ViewAdvanceDetails>> GetAdvanceList(string search);
        Task<string> Post(HR_AdvanceDetails  value);
        Task<string> Put(HR_AdvanceDetails value);
        Task<int> Delete(int id);
    }
}