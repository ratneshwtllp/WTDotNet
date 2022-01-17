
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IDocumentType
    {
        Task<int> Delete(int id);
        Task<int> GetNewId();
        Task<string> Post(DocumentTypeDetails   value);
        Task<string> Put(DocumentTypeDetails value);
        Task<List<DocumentTypeDetails>> Get();
        Task<DocumentTypeDetails> Get(int id);
        Task<List<DocumentTypeDetails>> GetDocumentTypeList(string search);
        Task<int> CheckDuplicate(string value);
    }
}