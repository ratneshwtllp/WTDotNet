
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISaleOrderContract
    {
        Task<long> GetNewOrderMasterID();
        Task<long> GetOrderChildID();
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProSubCategoryFromBuyer(long buyerid);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetFItemCodeList(long id);

        Task<string> GetPartyFItemList(long Partyid);

        Task<string> GetFitemDetails(long id);
        Task<SaleOrderMaster> CheckDuplicateOrder(string value);
        Task<string> GetProductDetails(long id);
        Task<string>BuyerList(); 
        Task<int> CheckDuplicate(string value);
        Task<List<ViewSaleOrder>> ViewSaleOrder();
        Task<List<ViewSaleOrder>> GetViewSaleOrder(string search);
        Task<string> GetIssuedByList(int buyerId);
        Task<ViewSaleOrder> Get(long OrderMasterID);
        Task<List<ViewSaleOrderDetails >> GetSaleOrderChild(long OrderMasterID);
        Task<string> Post(SaleOrderMaster value);
        Task<string> Put(SaleOrderMaster value);
        void Delete(int id);
        void UpdateOrder(int id);
        Task<string> TransportList();
        Task<string> FOBCIFList();
        Task<string> PutOrderStatus(long OrderMasterID);

        Task<long> GetNewOrderShippingId();
        Task<ViewSaleOrder> GetShippingDetails(long OrderMasterID);
        Task<string> Post(OrderShippingDetails value);
        Task<string> Put(OrderShippingDetails value);

        Task<List<ViewSaleOrderDetails>> GetViewSaleOrder(OrderSearch SO);
        Task<string> GetBuyerCodeList();
        Task<string> GetMultiLevelSubCat(long catid);
        Task<string> GetSizeList(long fitemid);

        Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid);
        Task<List<ViewSaleOrderDetails>> GetSaleOrderPrint(long id);
        Task<string> PutRequestCancelStatus(SaleOrderMaster so);
        Task<string> PutRequestUndoStatus(SaleOrderMaster so);
        Task<string> PostShoesSale(SaleOrderMaster value);
         
        Task<string> PutShoesSale(SaleOrderMaster value);
        Task<List<SaleOrderChild>> GetOrderProductList(long ordermasterid);
        Task<List<ViewSaleOrderDetails>> GetOrderItemsDetail(long ordermasterid, long fitemid);
        Task<List<ViewProductSize>> GetProductSizeList(long fitemid);

        Task<double> GetFitemSizePrice(long id, int sizeid);

        #region orderStatus
        Task<string> GetOrderNoList(long buyerid);
        Task<List<ViewSaleOrderDetails>> GetOrderItemsDetails(long orderid);
        Task<List<ViewSaleOrderStatus>> GetOrderStatus(long orderid, long orderchildid);
        #endregion

        #region CloseFile     
        Task<List<ViewSaleOrderDetails>> GetSaleOrderDetails(long orderid);
        Task<string> PostOrdeSheetClose(OrderSheetClose value);
        Task<List<ViewSaleOrderSheetClose>> GetViewSaleOrderSheetClose(OrderSearch SO);
        Task<int> DeleteOrderSheetClose(int id);
        Task<string> GetOrderNoListForClose(long buyerid);
        #endregion
    }
}