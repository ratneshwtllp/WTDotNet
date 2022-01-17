
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IGSMContract
    {
        Task<List<GSMDetails>> Get();
        Task<GSMDetails> Get(int id);
        Task<List<GSMDetails>> GetGSMList(string search);
        Task<int> GetNewGSMId();
        //Task<int> CheckDuplicate(string value);
        Task<int> CheckDuplicate(int value);
        Task<string> Post(GSMDetails value);
        Task<string> Put(GSMDetails value);
        Task<int> Delete(int id);
    }
}