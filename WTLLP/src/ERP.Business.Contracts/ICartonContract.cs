
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICartonContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewCartonId();
        Task<string> Post(CartonDetails value);
        Task<string> Put(CartonDetails value);
        Task<List<CartonDetails>> Get();
        Task<CartonDetails> Get(int id);
        Task<List<CartonDetails>> GetCartonList(string search);
    }
}