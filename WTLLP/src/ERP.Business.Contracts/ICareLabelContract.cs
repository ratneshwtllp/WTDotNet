
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICareLabelContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewCarelabelId();
        Task<string> Post(CareLabelDetails value);
        Task<string> Put(CareLabelDetails value);
        Task<List<ViewCarelabel >> Get();
        Task<CareLabelDetails> Get(int id);
        Task<List<ViewCarelabel>> GetCareLabelDetailsList(string search);
        Task<int> CheckDuplicate(string value,int BuyerId);
        Task<string> BuyerList();
    }
}