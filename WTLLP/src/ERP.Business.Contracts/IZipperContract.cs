using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IZipperContract
    {
        Task<List<ZipperMaster>> Get();
        Task<ZipperMaster> Get(int id);
        Task<List<ZipperMaster>> GetZipperList(string search);
        Task<int> GetNewZipperId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(ZipperMaster value);
        Task<string> Put(ZipperMaster value);
        Task<int> Delete(int id);
    }
}