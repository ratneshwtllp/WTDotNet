using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IESIContract
    {
        Task<List<HR_ESIDetails>> Get();
        Task<HR_ESIDetails> Get(int id);
        Task<List<HR_ESIDetails>> GetESIList();
        Task<int> GetNewESIId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_ESIDetails value);
        Task<string> Put(HR_ESIDetails value);
        Task<int> Delete(int id);
    }
}