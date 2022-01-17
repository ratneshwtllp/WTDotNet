
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ITreatmentContract
    {
        Task<List<TreatmentDetails>> Get();
        Task<TreatmentDetails> Get(int id);
        Task<List<TreatmentDetails>> GetTreatmentList(string search);
        Task<int> GetNewTreatmentId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(TreatmentDetails value);
        Task<string> Put(TreatmentDetails value);
        Task<int> Delete(int id);
    }
}