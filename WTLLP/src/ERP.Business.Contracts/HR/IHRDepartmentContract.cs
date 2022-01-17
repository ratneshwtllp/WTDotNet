
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHRDepartmentContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewDepartmentId();
        Task<string> Post(HR_DepartmentDetails  value);
        Task<string> Put(HR_DepartmentDetails  value);
        Task<List<HR_DepartmentDetails >> Get();
        Task<HR_DepartmentDetails> Get(int id);
        Task<List<HR_DepartmentDetails >> GetDepartmentList(string search);
        Task<int> CheckDuplicate(string value);
    }
}