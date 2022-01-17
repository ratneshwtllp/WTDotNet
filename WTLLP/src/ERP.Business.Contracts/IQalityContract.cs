
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IQalityContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewQualityId();
        Task<string> Post(QualityDetails value);
        Task<string> Put(QualityDetails value);
        Task<List<QualityDetails>> Get();
        Task<QualityDetails> Get(int id);
        Task<List<QualityDetails>> GetQualityList(string search);
        Task<int> CheckDuplicate(string value);
    }
}