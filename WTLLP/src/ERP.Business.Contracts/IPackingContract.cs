using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IPackingContract
    {
        Task<List<PackingMaster>> Get();
        Task<PackingMaster> Get(long packingid);
        Task<List<ViewPackingMaster>> Viewpacking(PackingSearch PS);  
        Task<long> GetNewPackingID();
        Task<long> GetNewPackingChildID(); 
        Task<long> GetNewPackingSerial();
        Task<string> GetNewPackingNO();
        Task<long> GetNewCartonNo();

        Task<string> GetBuyerCodeList();
        Task<string> GetOrderNoList(long buyerid);
        Task<string> GetDimensionList();
          
        Task<List<ViewPackingChild>> GetPackingChild(long packingid);
        Task<List<ViewOrderItemsForPacking>> GetOrderItemsForPacking(long orderid); 

        Task<string> Post(PackingMaster value);
        Task<string> Put(PackingMaster value);
        Task<string> ChangeStatus(long packingid);

        Task<List<ViewShoesPacking>> GetShoesPackingRecord(long id);
        Task<ViewProductSize> GetProductDetails(string barcode);
        Task<ViewSaleOrderChild> GetBarcodeDetails(long orderid, string barcode);

        Task<long> GetNewShoesCartonMasterId();
        Task<long> GetNewShoesCartonChildId();
        Task<string> PostShoesCarton(ShoesCartonMaster value);
        Task<List<ViewShoesCartonChild>> GetShoesCartonList(PackingSearch search);
        Task<List<ViewShoesCartonChild>> GetShoesCartonListForPacking(PackingSearch search);
        Task<List<ViewShoesPackingChild>> GetShoesPackingList(PackingSearch search);
        Task<List<ViewProductionPacking>> ProductionPackingPrint(long id);
        Task<List<ViewPackingChild >> PackingSlipPrint(long id);

        Task<string> PostBuyerCartonStickerTemplate(BuyerCartonStickerTemplate value);
        Task<BuyerCartonStickerTemplate> GetBuyerCartonSticker(long buyerid);

        Task<string> PutBarcodeFromPacking(ProductMultipleSize obj);
    }
}