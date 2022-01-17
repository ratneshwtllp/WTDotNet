using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ITitleContract
    {
        Task<List<HR_TitleDetails>> Get();
        Task<HR_TitleDetails> Get(int id);
        Task<List<HR_TitleDetails>> GetTitleList(string search);
        Task<int> GetNewTitleId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_TitleDetails value);
        Task<string> Put(HR_TitleDetails value);
        Task<int> Delete(int id);
    }
}