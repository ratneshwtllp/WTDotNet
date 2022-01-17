
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICityContract
    {
        Task<List<ViewCityDetails>> Get(); 
        Task<ViewCityDetails> Get(int id);
        Task<string> GetCountryList();
        Task<string> GetStateList(int Countryid);
        Task<List<ViewCityDetails>> GetCityList(string search);
        Task<int> GetNewCityId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(CityDetails value);
        Task<string> Put(CityDetails value);
        Task<int> Delete(int id);
    }
}