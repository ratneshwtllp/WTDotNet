
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IColorContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewColorId();
        Task<string> Post(ColorDetails value);
        Task<string> Put(ColorDetails value);
        Task<List<ColorDetails>> Get();
        Task<ColorDetails> Get(int id);
        Task<List<ColorDetails>> GetColorList(string search);
        Task<int> CheckDuplicate(string value);
    }
}