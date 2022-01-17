using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHeelContract
    {
        Task<List<HeelDetails>> Get();
        Task<HeelDetails> Get(int id);  
        Task<List<HeelDetails>> GetHeelList(string search);
        Task<string> GetListHeel();
        Task<int> GetNewHeelId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(HeelDetails value);
        Task<string> Put(HeelDetails value);
        Task<int> Delete(int id);
        
    }
}