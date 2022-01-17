using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHRGradeContract
    {
        Task<List<HR_GradeDetails>> Get();
        Task<HR_GradeDetails> Get(int id);
        Task<List<HR_GradeDetails>> GetGradeList(string search);
        Task<int> GetNewGradeId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_GradeDetails value);
        Task<string> Put(HR_GradeDetails value);
        Task<int> Delete(int id);
    }
}