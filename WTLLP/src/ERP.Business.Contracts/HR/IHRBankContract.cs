using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHRBankContract
    {
        Task<List<HR_BankDetails>> Get();
        Task<HR_BankDetails> Get(int id);
        Task<List<HR_BankDetails>> GetBankList(string search);
        Task<int> GetNewBankId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_BankDetails value);
        Task<string> Put(HR_BankDetails value);
        Task<int> Delete(int id);
    }
}