using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Entities;

namespace ERP.Business.Contracts
{
    public interface ICurrencyContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewCurrencyId();
        Task<int> GetNewCurrencyHistoryId();
        Task<decimal > GetPreviuosPrice(int id);
        Task<string> Post(CurrencyDetails value);
        Task<string> Put(CurrencyDetails value);
        Task<List<CurrencyDetails>> Get();
        Task<CurrencyDetails> Get(int id);

        Task<List<CurrencyDetails>> GetCurrencyList(string search);
     
        Task<List<ViewCurrencyHistory>> GetCurrencyHistoryList(int  cid);
        Task<int> CheckDuplicate(string value);
    }
}
