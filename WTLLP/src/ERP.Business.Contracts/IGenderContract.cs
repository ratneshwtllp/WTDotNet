using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IGenderContract
    {
        Task<List<GenderDetails>> Get();
        Task<GenderDetails> Get(int id);  
        Task<List<GenderDetails>> GetGenderList(string search);
        Task<string> GetListGender();
        Task<int> GetNewGenderId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(GenderDetails value);
        Task<string> Put(GenderDetails value);
        Task<int> Delete(int id);
        
    }
}