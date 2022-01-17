using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPOPTContract
    {
        Task<List<POPaymentTerms>> Get();
        Task<POPaymentTerms> Get(int id);
        Task<List<POPaymentTerms>> GetPOPTList(string search);
        Task<int> GetNewPOPTId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(POPaymentTerms value);
        Task<string> Put(POPaymentTerms value);
        Task<int> Delete(int id);
        //Task<string> GetListPOPT();
    }
}