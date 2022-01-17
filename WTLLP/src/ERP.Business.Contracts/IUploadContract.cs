using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IUploadContract
    {
        Task<UploadDetails> Get();
    }
}