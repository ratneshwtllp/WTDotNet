using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISalaryContract
    {
        Task<List<HR_ViewCalculateMonthlySalary>> SalaryList(AttandanceSearchModel obj);
        Task<List<HR_ViewEmployeeMonthlySalary>> EmployeeSalaryList(AttandanceSearchModel obj);
        Task<string> GetEmployeeList();
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetDepartmentList();
    }
}