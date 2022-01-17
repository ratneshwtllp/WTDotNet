using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStyleContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewStyleId();
        Task<string> Post(StyleDetails value);
        Task<string> Put(StyleDetails value);
        Task<List<StyleDetails>> Get();
        Task<StyleDetails> Get(int id);
        Task<string> GetStyleList(); 
        Task<List<StyleDetails>> GetStyleRecord(string search); 
        Task<int> CheckDuplicate(string value);
    }
}