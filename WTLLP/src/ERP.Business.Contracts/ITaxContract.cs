using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ITaxContract
    {
        Task<List<TaxDetails>> Get();
        Task<TaxDetails> Get(int id);
        Task<List<TaxDetails>> GetTaxList(string search);
        Task<int> GetNewTaxId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(TaxDetails value);
        Task<string> Put(TaxDetails value);
        Task<int> Delete(int id);
        
    }
}