
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IShapeContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewShapeId();
        Task<string> Post(ShapeDetails value);
        Task<string> Put(ShapeDetails value);
        Task<List<ShapeDetails>> Get();
        Task<ShapeDetails> Get(int id);
        Task<List<ShapeDetails>> GetShapeList(string search);
        Task<int> CheckDuplicate(string value);
    }
}