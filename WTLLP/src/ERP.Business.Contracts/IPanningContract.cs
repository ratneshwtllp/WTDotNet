
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPanningContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewPanningId();
        Task<string> Post(PanningDetails value);
        Task<string> Put(PanningDetails value);
        Task<List<PanningDetails>> Get();
        Task<PanningDetails> Get(int id);
        Task<List<PanningDetails>> GetPanningList(string search);
        Task<int> CheckDuplicate(string value);
    }
}