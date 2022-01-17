
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISupplierContract
    {
        Task<List<SupplierDetails>> Get();
        Task<SupplierDetails> Get(int id);
        Task<string> GetCountryList();
        Task<string> GetStateList(int Countryid);
        Task<string> GetCityList(int Stateid);
        Task<string> GetCurrencyList();
        Task<List<ViewSupplierDetails>> GetSupplierList(string search);
        Task<int> GetNewSupplierId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(SupplierDetails value);
        Task<string> Put(SupplierDetails value);
        Task<int> Delete(int id);
        Task<List<ViewSupplierBank>> GetSupplierBank(long id);
        Task<int> GetNewSupplierBankId();
        Task<string> PostSupplierBank(SupplierBankDetails value);
        Task<int> DeleteSupplierBank(int id);
        Task<string> GetBankNameList();
        Task<List<ViewSupplierDetails>> GetSupplierForPrint(long id);

    }
}