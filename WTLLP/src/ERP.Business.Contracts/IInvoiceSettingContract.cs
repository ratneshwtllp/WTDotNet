
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IInvoiceSettingContract
    {
        Task<InvoiceSettingDetails> GetInvoiceSettingDetails();
        Task<string> Post(InvoiceSettingDetails value);
        Task<string> Put(InvoiceSettingDetails value);  
    }
}