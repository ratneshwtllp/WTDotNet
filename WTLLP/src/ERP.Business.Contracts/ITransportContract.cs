

using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ITransportContract
    {
        Task<List<TransportDetails>> Get();
        Task<TransportDetails> Get(int id);
        Task<List<TransportDetails>> GetTransportList(string search);
        Task<int> GetNewTransportId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(TransportDetails value);
        Task<string> Put(TransportDetails value);
        Task<int> Delete(int id);
        
    }
}