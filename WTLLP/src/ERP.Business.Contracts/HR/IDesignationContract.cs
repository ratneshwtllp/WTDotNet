using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IDesignationContract
    {
        Task<List<HR_DesignationDetails>> Get();
        Task<HR_DesignationDetails> Get(int id);
        Task<List<HR_DesignationDetails>> GetDesignationList(string search);
        Task<int> GetNewDesignationId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HR_DesignationDetails value);
        Task<string> Put(HR_DesignationDetails value);
        Task<int> Delete(int id);
    }
}