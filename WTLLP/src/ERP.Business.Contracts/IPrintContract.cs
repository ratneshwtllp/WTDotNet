using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPrintContract
    {
        Task<List<PrintDetails>> Get();
        Task<PrintDetails> Get(int id);
        Task<List<PrintDetails>> GetPrintList(string search);
        Task<int> GetNewPrintId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(PrintDetails value);
        Task<string> Put(PrintDetails value);
        Task<int> Delete(int id);
        //Task<string> GetListPrint();
    }
}