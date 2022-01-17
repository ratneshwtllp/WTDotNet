
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IDesignerContract
    {
        Task<List<DesignerDetails>> Get();
        Task<DesignerDetails> Get(int id);
        Task<List<DesignerDetails>> GetDesignerList(string search);
        Task<int> GetNewDesignerId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(DesignerDetails value);
        Task<string> Put(DesignerDetails value);
        Task<int> Delete(int id);
        
    }
}