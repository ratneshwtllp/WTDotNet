
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IUserAsSupervisorContract
    {
        Task<string> GetUserList();
        Task<List<ContractorDetails>> GetContractorList();
        Task<int> GetNewId();
        Task<string> Post(List<UserAsSupervisorDetails> value);
        Task<List<UserAsSupervisorDetails>> GetUserSupervisor(int userid);
    }
}