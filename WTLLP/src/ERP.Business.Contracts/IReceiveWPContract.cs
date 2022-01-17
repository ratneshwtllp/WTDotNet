using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IReceiveWPContract
    {
        Task<List<ReceiveWPMaster>> Get();
        Task<ReceiveWPMaster> Get(long issueid);

        Task<List<ViewReceiveWP>> GetList(string search); 

        Task<long> GetNewReceiveWPID(); 
        Task<string> GetPlanNoList();
        Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid);
        Task<ViewWorkPlan> GetViewWorkPlan(long planid);

        Task<string> Post(ReceiveWPMaster value);

        Task<string> PutIssueStatus(long receivewpid);
        Task<string> DeleteReceiveWP(long receivewpid);

    }
}