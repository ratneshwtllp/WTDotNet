using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public  interface IDashBoard
    {
        Task<List<ViewDashBoardBuyerLastOrder>> GetViewDashBoardBuyerLastOrder(OrderSearch Obj);
        Task<List<ViewDashBoardOrderToBeDeliver>> GetViewDashBoardOrderToBeDeliver(OrderSearch Obj);
    }
}
