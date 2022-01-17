
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface ISalaryStructureContract
    {
        Task<string> GetDepartmentofEmployee();
        Task<string> GetEmployeeList(long departmentid);
        Task<string> GetAllowncesList();
        Task<HR_ViewSalaryStructure> GetEmpDetails(long id);
        Task<List<HR_ViewSalaryStructure>> GetChildDetails(long EmployeeId);
        Task<int> GetNewSalStrId();
        Task<long> GetNewSalStrChildId();
        Task<string> Post(HR_SalaryStructureMaster value);
    }
}