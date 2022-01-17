
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IWidthContract
    {
        Task<List<WidthDetails>> Get();
        Task<WidthDetails> Get(int id);
        Task<List<WidthDetails>> GetWidthList(string search);
        Task<string> GetUnitList();
        Task<int> GetNewWidthId();
        Task<string> Post(WidthDetails value);
        Task<string> Put(WidthDetails value);
        Task<int> Delete(int id);
    }
}