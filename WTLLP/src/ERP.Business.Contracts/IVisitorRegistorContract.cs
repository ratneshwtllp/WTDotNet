using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IVisitorRegistorContract
    {
        Task<List<VisitorRegistorDetails>> Get();
        Task<VisitorRegistorDetails> Get(int id);
        Task<List<VisitorRegistorDetails>> GetVisitorRegistorList(string search);
        Task<int> GetNewVisitorRegistorId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(VisitorRegistorDetails value);
        Task<string> Put(VisitorRegistorDetails value);
        Task<int> Delete(int id);

    }
}