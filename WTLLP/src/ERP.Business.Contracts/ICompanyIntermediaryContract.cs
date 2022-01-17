
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICompanyIntermediaryContract
    {
        Task<CompanyIntermediaryBankDetails> GetCompanyIntermediaryBankDetails(int id);
        Task<int> GetNewIntermediaryBankID();
        Task<int> DeleteCompanyIntermediary(int id);
        Task<string> GetCompanyList();
        Task<string> GetBankNameList();
        Task<string> Post(CompanyIntermediaryBankDetails value);
        Task<string> Put(CompanyIntermediaryBankDetails value);
        Task<List<ViewCompanyIntermediaryBankDetails>> GetCompanyIntermediaryList(string search);

    }
}