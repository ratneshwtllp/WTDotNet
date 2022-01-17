
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ILabelContract
    {
        Task<List<LabelDetails>> Get();
        Task<LabelDetails> Get(int id);
        Task<string> GetBuyerList();
        Task<List<ViewLabelDetails>> GetLabelList(string search);
        Task<int> GetNewLabelId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(LabelDetails value);
        Task<string> Put(LabelDetails value);
        Task<int> Delete(int id);
        
    }
}