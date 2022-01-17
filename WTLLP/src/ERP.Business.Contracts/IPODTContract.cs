using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPODTContract
    {
        Task<List<PODeliveryTerms>> Get();
        Task<PODeliveryTerms> Get(int id);
        Task<List<PODeliveryTerms>> GetPODTList(string search);
        Task<int> GetNewPODTId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(PODeliveryTerms value);
        Task<string> Put(PODeliveryTerms value);
        Task<int> Delete(int id);
        //Task<string> GetListPODT();
    }
}