
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISelectionContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewSelectionId();
        Task<string> Post(SelectionDetails value);
        Task<string> Put(SelectionDetails value);
        Task<List<SelectionDetails>> Get();
        Task<SelectionDetails> Get(int id);
        Task<List<SelectionDetails>> GetSelectionList(string search);
        Task<int> CheckDuplicate(string value);
    }
}