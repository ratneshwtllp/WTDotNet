
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICardContract
    {
        Task<List<CardsFolder_Details>> Get();
        Task<CardsFolder_Details> Get(int id);
        Task<List<CardsFolder_Details>> GetCardList(string search);
        Task<int> GetNewCardId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(CardsFolder_Details value);
        Task<string> Put(CardsFolder_Details value);
        Task<int> Delete(int id);

    }
}