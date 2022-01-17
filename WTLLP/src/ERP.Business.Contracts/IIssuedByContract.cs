

using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IIssuedByContract
    {
        Task<List<IssuedByDetails>> Get();
        Task<IssuedByDetails> Get(int id);
        Task<string> GetBuyerList();
        Task<List<ViewIssuedByDetails>> GetIssuedByList(string search);
        Task<int> GetNewIssuedById();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(IssuedByDetails value);
        Task<string> Put(IssuedByDetails value);
        Task<int> Delete(int id);
        
    }
}