
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICompanyBeneficiaryContract
    {
        //Task<List<ViewHSNDetails>> Get(); 

        //Task<string> GetTaxList();
     
        //Task<int> CheckDuplicate(string value);


        Task<CompanyBeneficiaryBankDetails> GetCompanyBeneficiaryBankDetails(int id);
        Task<int> GetNewBeneficiaryBankID();
        Task<int> DeleteCompanyBeneficiary(int id);
        Task<string> GetCompanyList();
        Task<string> GetBankNameList();
        Task<string> Post(CompanyBeneficiaryBankDetails value);
        Task<string> Put(CompanyBeneficiaryBankDetails value);
        Task<List<ViewCompanyBeneficiaryBankDetails>> GetCompanyBeneficiaryList(string search);

    }
}