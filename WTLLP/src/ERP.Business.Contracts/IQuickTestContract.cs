
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IQuickTestContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewQuickTestId();
        Task<string> Post(QuickTestDetails value);
        Task<string> Put(QuickTestDetails value);
        Task<List<QuickTestDetails>> Get();
        Task<QuickTestDetails> Get(int id);
        Task<List<QuickTestDetails>> GetQuickTestList(string search);
        Task<int> CheckDuplicate(string value);
    }
}