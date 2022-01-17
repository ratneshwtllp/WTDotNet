
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IStickerContract
    {
        Task<int> Delete(int id);
        Task<int> GetNewStickerId();
        Task<string> Post(StickerDetials  value);
        Task<string> Put(StickerDetials value);
        Task<List<ViewSticker >> Get();
        Task<StickerDetials> Get(int id);
        Task<List<ViewSticker>> GetStickerDetialsList(string search);
        Task<int> CheckDuplicate(string value,int BuyerId);
        Task<string> BuyerList();
    }
}