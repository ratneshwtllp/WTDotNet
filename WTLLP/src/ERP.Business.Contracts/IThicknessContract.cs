
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IThicknessContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewThicknessId();
        Task<string> Post(ThicknessDetails value);
        Task<string> Put(ThicknessDetails value);
        Task<List<ThicknessDetails>> Get();
        Task<ThicknessDetails> Get(int id);
        Task<List<ThicknessDetails>> GetThicknessList(string search);
        Task<int> CheckDuplicate(string value);
    }
}