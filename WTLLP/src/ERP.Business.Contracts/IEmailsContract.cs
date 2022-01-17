using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IEmailsContract
    {
        Task<List<EmailSettings>> Get();
        Task<EmailSettings> Get(long emailid);
        Task<List<ViewEmailSettings>> GetViewEmailSettingsSearch(string search);
        Task<List<ViewEmailSettings>> GetViewEmailSettings();
        Task<long> GetNewEmailID();
        Task<string> GetEmailsList();

        //Task<int> GetNewPOSerial();
        //Task<List<ViewPOPrint>> GetPOForPrint(long Poid);

        Task<string> Post(EmailSettings value);
        Task<string> Put(EmailSettings value);

        Task<string> Delete(long id);
    }
}