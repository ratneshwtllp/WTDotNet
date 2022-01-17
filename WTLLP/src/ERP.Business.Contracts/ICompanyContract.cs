
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICompanyContract
    {
        Task<List<CompanyDetails >> Get();
        Task<CompanyDetails> Get(int id);
        Task<List<CompanyDetails>> GetList(string search);

        Task<int> GetNewId();

        Task<string> Post(CompanyDetails value);
        Task<string> Put(CompanyDetails value);

        Task<int> Delete(int id);
         
        Task<int> CheckDuplicate(string value);
    }
}