
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IFinsihContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewFinishId();
        Task<string> Post(FinishDetails value);
        Task<string> Put(FinishDetails value);
        Task<FinishDetails> Get(int id);
        Task<List<FinishDetails>> GetFinishList(string search);
    }
}