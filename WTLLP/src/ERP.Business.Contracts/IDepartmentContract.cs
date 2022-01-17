
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IDepartmentContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewDepartmentId();
        Task<string> Post(DepartmentDetails  value);
        Task<string> Put(DepartmentDetails  value);
        Task<List<DepartmentDetails >> Get();
        Task<DepartmentDetails> Get(int id);
        Task<List<DepartmentDetails >> GetDepartmentList(string search);
        Task<int> CheckDuplicate(string value);
    }
}