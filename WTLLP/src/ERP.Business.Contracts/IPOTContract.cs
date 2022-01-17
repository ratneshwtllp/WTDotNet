using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPOTContract
    {
        Task<List<POTransportDetails>> Get();
        Task<POTransportDetails> Get(int id);
        Task<List<POTransportDetails>> GetPOTList(string search);
        Task<int> GetNewPOTId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(POTransportDetails value);
        Task<string> Put(POTransportDetails value);
        Task<int> Delete(int id);
        //Task<string> GetListPOT();
    }
}