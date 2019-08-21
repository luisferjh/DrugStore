using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Orders;
using DrugStore.Web.Models.Orders.Order;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly DbContextDrugStore _context;

        public OrderService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> List()
        {
            return await _context.OrderIncomes
                .Include(u => u.User)
                .Include(c => c.Provider)
                .OrderByDescending(i => i.IdOrderIncome)
                .Take(100)
                .Select(o => new OrderViewModel
                {
                    IdOrderIncome = o.IdOrderIncome,
                    IdProvider = o.IdProvider,
                    Proveedor = o.Provider.ProviderName,
                    IdUser = o.IdUser,
                    User = o.User.UserName,
                    DateEntry = EF.Property<DateTime>(o,"DateOn"),                  
                    Total = o.Total,
                    State = o.State
                }).ToListAsync();


        }

        public async Task<OrderViewModel> GetOrderIncome(int id)
        {
            var order = await _context.OrderIncomes.FindAsync(id);
            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                IdOrderIncome = order.IdOrderIncome,
                IdProvider = order.IdProvider,
                Proveedor = order.Provider.ProviderName,
                IdUser = order.IdUser,
                User = order.User.UserName,                
                DateEntry =  _context.Entry(order).Property<DateTime>("DateOn").CurrentValue,                
                Total = order.Total,
                State = order.State
            };
        }

        public async Task AddOrderIncome(CreateViewModel orderModel)
        {
            // we create the date with the current date
            var dateOrderIncome = DateTime.Now;  

            OrderIncome order = new OrderIncome
            {               
                IdProvider = orderModel.IdProvider,
                IdUser = orderModel.IdUser,                              
                Total = orderModel.Total,
                State = "Accept"
            };

            _context.OrderIncomes.Add(order);
            await _context.SaveChangesAsync();

            // Adding details to detailOrder
            var IdOrderIncome = order.IdOrderIncome;

            // We loop de property details in CreateViewModel to add every
            // detail in OrderDetail Table in the same method Add of OrderIncome
            foreach (var det in orderModel.Details)
            {
                OrderIncomeDetails detail = new OrderIncomeDetails
                {
                    IdOrderIncome = IdOrderIncome,
                    IdProduct = det.IdProduct,
                    Amount = det.Amount,
                    DueDate = det.DueDate,
                    UnitCost = det.UnitCost
                };
                _context.OrderIncomeDetails.Add(detail);
            }
            await _context.SaveChangesAsync();

        }

        public async Task UpdateOrderIncome(UpdateViewModel orderModel)
        {
            var order = await _context.OrderIncomes.FirstOrDefaultAsync(o =>
            o.IdOrderIncome == orderModel.IdOrderIncome);

            order.IdOrderIncome = orderModel.IdOrderIncome;
            order.IdProvider = orderModel.IdProvider;
            order.IdUser = orderModel.IdUser;                      
            order.Total = orderModel.Total;
            order.State = orderModel.State;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderIncome(OrderIncome order)
        {
            _context.OrderIncomes.Remove(order);

            await _context.SaveChangesAsync();
        }
      
        public async Task<bool> OrderIncomeExists(int id)
        {
            return await _context.OrderIncomes.AnyAsync(o => o.IdOrderIncome == id);
        }

        public async Task<OrderIncome> SearchOrderIncome(int id)
        {
            var order = await _context.OrderIncomes.FindAsync(id);
            if (order == null)
            {
                return null;
            }

            return order;
        }
    }
}
