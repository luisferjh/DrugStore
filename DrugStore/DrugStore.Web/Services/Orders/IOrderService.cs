using DrugStore.Entities.Orders;
using DrugStore.Web.Models.Orders.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> List();
        Task<OrderViewModel> GetOrderIncome(int id);
        Task AddOrderIncome(CreateViewModel orderModel);
        Task UpdateOrderIncome(UpdateViewModel orderModel);
        Task DeleteOrderIncome(OrderIncome order);
        Task<bool> OrderIncomeExists(int id);
        Task<OrderIncome> SearchOrderIncome(int id);
    }
}
