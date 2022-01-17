
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICountryContract
    {
        Task<List<CountryDetails>> Get();
        Task<CountryDetails> Get(int id);
        Task<List<CountryDetails>> GetCountryList(string search);
        Task<int> GetNewCountryId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(CountryDetails value);
        Task<string> Put(CountryDetails value);
        Task<int> Delete(int id);
        
    }
}