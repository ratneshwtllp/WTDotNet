using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IContractorContract
    {
        Task<List<ContractorDetails>> Get();
        Task<ContractorDetails> Get(int id);
        Task<List<ViewContractorDetails>> GetContractorRecord(string search);
        Task<ViewContractorDetails> GetContractorRecord(long id);
        Task<string> GetCompanyList();
        Task<string> GetContractorList();

        Task<long> GetNewContractorId();
        Task<string> Post(ContractorDetails value);
        Task<string> Put(ContractorDetails value); 
       
        Task<int> CheckDuplicate(string value);
        Task<string> Delete(long id);
    }
}