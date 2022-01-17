
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHSNContract
    {
        Task<List<ViewHSNDetails>> Get(); 
        Task<ViewHSNDetails> Get(int id);
        Task<string> GetTaxList();
        Task<List<ViewHSNDetails>> GetHSNList(string search);
        Task<int> GetNewHSNId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HSNDetails  value);
        Task<string> Put(HSNDetails value);
        Task<int> Delete(int id);
    }
}