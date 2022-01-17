using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IBankContract
    {
        Task<List<BankDetails>> Get();
        Task<BankDetails> Get(int id);
        Task<List<BankDetails>> GetBankList(string search);
        Task<int> GetNewBankId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(BankDetails value);
        Task<string> Put(BankDetails value);
        Task<int> Delete(int id);
    }
}