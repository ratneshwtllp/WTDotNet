
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IConsigneeContract
    {
        Task<List<ConsigneeDetails>> Get();
        Task<ConsigneeDetails> Get(int id);
        Task<string> GetCountryList();
        Task<string> GetStateList(int Countryid);
        Task<string> GetCityList(int Stateid);
        Task<List<ConsigneeDetails>> GetConsigneeList(string search);
        Task<int> GetNewConsigneeId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(ConsigneeDetails value);
        Task<string> Put(ConsigneeDetails value);
        Task<int> Delete(int id);
        
    }
}