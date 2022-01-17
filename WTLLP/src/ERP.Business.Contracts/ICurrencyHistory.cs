using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Entities;

namespace ERP.Business.Contracts
{
    public interface ICurrencyHistory
    {
        //Task<int> Delete(int id);
        Task<int> GetNewCurrencyHistoryId();
        Task<string> Post(CurrencyHistoryDetails  value);
        //Task<string> Put(CurrencyHistoryDetails value);
        Task<List<CurrencyHistoryDetails>> GetCurrencyHistory();
       // Task<CurrencyHistoryDetails> GetCurrencyHistory(int id);
       // Task<List<CurrencyHistoryDetails>> GetCurrencyHistoryList(string search);
       // Task<int> CheckDuplicate(string value);
    }
}
