using System;
using DrugStore.Entities.Sales;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Sales.Sale;

namespace DrugStore.Web.Services.Sales
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> List();
        Task<SaleViewModel> GetSale(int id);
        Task AddSale(CreateViewModel saleModel);
        Task UpdateSale(UpdateViewModel saleModel);
        Task DeleteSale(Sale sale);
        Task<bool> SaleExists(int id);
        Task<Sale> SearchSale(int id);
    }
}
