using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IPackingWeightContract
    {
        //Task<List<PackingWeightDetails>> Get();
        //Task<PackingWeightDetails> Get(long packingweightid);
        //Task<List<PackingWeightDetails>> Get(string search); 

        Task<long> GetNewPackingWeightID();
        Task<string> GetPackingNoList();

        Task<int> IsPackingWeightExist(long packingid);

        Task<List<PackingWeightDetails>> GetPackingWeightDetails(long packingid);
        Task<List<ViewPackingCartons>> GetPackingCartonDetails(long packingid);

        Task<string> Post(List<PackingWeightDetails> value);
        Task<string> Put(List<PackingWeightDetails> value);
        Task<string> ChangeStatus(long packingid);

    }
}