using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Sales;
using DrugStore.Web.Models.Sales.Delivery;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.Sales
{
    public class DeliveryService : IDeliveryService
    {
        private readonly DbContextDrugStore _context;

        public DeliveryService(DbContextDrugStore context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DeliveryViewModel>> List()
        {
            return await _context.Deliveries
            .Select(d => new DeliveryViewModel
            {
                IdSale = d.IdSale,
                IdClient = d.IdClient,
                Adress = d.Adress,
                Date = EF.Property<DateTime>(d, "DateOn"),
                State = d.State
            }).ToListAsync();
        }
        public async Task<DeliveryViewModel> GetDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return null;
            }

            return new DeliveryViewModel
            {
                IdSale = delivery.IdSale,
                IdClient = delivery.IdClient,
                Adress = delivery.Adress,
                Date = _context.Entry(delivery).Property<DateTime>("DateOn").CurrentValue,
                State = delivery.State
            };
        }

        public async Task AddDelivery(CreateViewModel deliveryModel)
        {
            Delivery delivery = new Delivery
            {
                IdSale = deliveryModel.IdSale,
                IdClient = deliveryModel.IdClient,
                Adress = deliveryModel.Adress,                
                State = deliveryModel.State
            };

            _context.Deliveries.Add(delivery);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateDelivery(UpdateViewModel deliveryModel)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d =>
            d.IdSale == deliveryModel.IdSale);

            delivery.IdSale = deliveryModel.IdSale;
            delivery.IdClient = deliveryModel.IdClient;
            delivery.Adress = deliveryModel.Adress;            
            delivery.State = deliveryModel.State;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteDelivery(Delivery delivery)
        {
            _context.Deliveries.Remove(delivery);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeliveryExists(int id)
        {
            return await _context.Deliveries.AnyAsync(d => d.IdSale == id);
        }

        public async Task<Delivery> SearchDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return null;
            }

            return delivery;
        }    
        
    }
}
