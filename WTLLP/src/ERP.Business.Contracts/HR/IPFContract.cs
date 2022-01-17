using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPFContract
    {
        Task<List<HR_PFDetails>> Get();
        Task<HR_PFDetails> Get(int id);
        Task<List<HR_PFDetails>> GetPFList();
        Task<int> GetNewPFId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_PFDetails value);
        Task<string> Put(HR_PFDetails value);
        Task<int> Delete(int id);
    }
}