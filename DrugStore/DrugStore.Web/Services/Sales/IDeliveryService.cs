using DrugStore.Entities.Sales;
using DrugStore.Web.Models.Sales.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Sales
{
    public interface IDeliveryService
    {
        Task<IEnumerable<DeliveryViewModel>> List();
        Task<DeliveryViewModel> GetDelivery(int id);
        Task AddDelivery(CreateViewModel deliveryModel);
        Task UpdateDelivery(UpdateViewModel deliveryModel);
        Task DeleteDelivery(Delivery delivery);
        Task<bool> DeliveryExists(int id);
        Task<Delivery> SearchDelivery(int id);
    }
}
