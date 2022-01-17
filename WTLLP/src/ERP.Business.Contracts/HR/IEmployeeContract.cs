
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IEmployeeContract
    {
        Task<List<HR_EmployeeDetails>> Get();
        Task<HR_ViewEmployeeDetails> Get(int id);
        Task<string> TitleList();
        Task<string> DepartmentList();
        Task<string> DesignationList();
        Task<string> BankList();
        Task<string> GradeList();
        Task<string> GetGenderList();
        Task<string> GetMaritalStatusList();

        Task<List<HR_ViewEmployeeDetails>> GetEmployeeList(string search);
        Task<int> GetNewEmployeeId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_EmployeeDetails value);
        Task<string> Put(HR_EmployeeDetails value);
        Task<int> Delete(int id);
        Task<string> GetEmployeeImagePath(long EmployeeId);
        Task<List<HR_EmployeeFile>> GetEmployeeFileRecord(long id);
        Task<long> GetNewEMPFileId();
        Task<string> PostEmployeeFile(List<HR_EmployeeFile> value);
        Task<string> GetEmployeeType();
    }
}