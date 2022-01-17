using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IAttandanceContract
    {
        Task<List<HR_ViewAttandance>> List(AttandanceSearchModel obj);
        Task<List<HR_ViewEmployeeDetails>> GetEmployeeList(string search);
        Task<string> GetEmployeeList();
        Task<string> GetAttandanceStatusList();
        Task<string> Post(List<HR_AttandanceDetails> value);
        Task<List<HR_ViewCalculateMonthlySalary>> SalaryList(AttandanceSearchModel obj);
        //Task<ModelProductDetails> Get(int id);
        //Task<int> GetNewAddModelId();
        //Task<string> Post(List<ModelProductDetails> value);
        //Task<string> GetProCategoryList();
        //Task<string> GetProSubCategoryList(long pcategoryId);
        //Task<string> GetProductList(long subcategoryid);
        //Task<string> GetProductDetails(long fitemid);
        //Task<string> GetModelList();
        //Task<List<ViewProductShow>> GetModelProductDetails(long Subcatid);
        //Task<List<ViewAttandance>> GetViewAttandance();
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetDepartmentList();
    }
}