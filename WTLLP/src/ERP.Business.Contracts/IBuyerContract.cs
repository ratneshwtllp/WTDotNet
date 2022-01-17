
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IBuyerContract
    {
        Task<List<BuyerDetails>> Get();
        Task<BuyerDetails> Get(int id);
        Task<string> GetCountryList();
        Task<string> GetCurrencyList();
        Task<string> GetStateList(int Countryid);
        Task<string> GetCityList(int Stateid);
        Task<List<ViewBuyerDetails>> GetBuyerList(string search);
        Task<int> GetNewBuyerId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(BuyerDetails value);
        Task<string> Put(BuyerDetails value);
        Task<int> Delete(int id);
        Task<List<ConsigneeDetails>> GetConsigneeName();
        Task<string> GetBankNameList();
        Task<int> GetNewBuyerConsigneeId();
        Task<List<BuyerConsigneeDetails>> GetBuyerConsignee(int BuyerId);
        Task<List<ConsigneeDetails>> GetConsigneeList(int buyerid);
        Task<List<ViewBuyerBank>> GetBuyerBank(long id);
        Task<int> GetNewBuyerBankId();
        Task<string> PostBuyerBank(BuyerBankDetails value);
        Task<int> DeleteBuyerBank(int id);
        Task<List<ViewBuyerDetails>> GetBuyerDetails(long id);
        Task<List<ViewBuyerConsignee>> BuyerConsigneePrint(long id);
    }
}