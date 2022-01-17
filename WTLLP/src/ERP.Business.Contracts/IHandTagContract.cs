
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IHandTagContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewHandTagId();
        Task<string> Post(HandTagDetails   value);
        Task<string> Put(HandTagDetails value);
        Task<List<ViewHandTag >> Get();
        Task<HandTagDetails> Get(int id);
        Task<List<ViewHandTag>> GetHandTagDetailsList(string search);
        Task<int> CheckDuplicate(string value,int BuyerId);
        Task<string> BuyerList();
    }
}