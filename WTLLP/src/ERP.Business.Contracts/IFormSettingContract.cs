using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IFormSettingContract
    {
        Task<List<FormNumberSettingsDetails>> Get();
        Task<string> Put(FormNumberSettingsDetails value);
    }
}