using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Sales;
using DrugStore.Web.Models.Sales.Sale;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly DbContextDrugStore _context;

        public SaleService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SaleViewModel>> List()
        {            

            return await _context.Sales
                .Include(u => u.IdUser)
                .Include(c => c.IdClient)
                .OrderByDescending(v => v.IdSale)
                .Take(100)
                .Select(s => new SaleViewModel
                {
                    IdSale = s.IdSale,
                    IdUser = s.IdUser,
                    UserName = s.User.UserName,
                    IdClient = s.IdClient,
                    ClientName = s.Client.Name + " " + s.Client.LastName,
                    TypeSale = s.TypeSale,
                    VoucherSeries = s.VoucherSeries,
                    VoucherNumber = s.VoucherNumber,
                    SaleDate = EF.Property<DateTime>(s, "DateOn"),
                    SaleDateUpdate = EF.Property<DateTime>(s, "LastUpdated"),
                    TotalPrice = s.TotalPrice,
                    State = s.State
                }).ToListAsync(); 

        }

        public async Task<SaleViewModel> GetSale(int id)
        {

            var sale = await _context.Sales
                .Include(u => u.User)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(v => v.IdSale == id);

            if (sale == null)
            {
                return null;
            }
          
            return new SaleViewModel
            {
                IdSale = sale.IdSale,
                IdUser = sale.IdUser,
                UserName = sale.User.UserName,
                IdClient = sale.IdClient,
                ClientName = sale.Client.Name + " " + sale.Client.LastName,
                TypeSale = sale.TypeSale,
                VoucherSeries = sale.VoucherSeries,
                VoucherNumber = sale.VoucherNumber,                
                SaleDate = _context.Entry(sale).Property<DateTime>("DateOn").CurrentValue.Date,
                SaleDateUpdate = _context.Entry(sale).Property<DateTime>("LastUpdated").CurrentValue.Date,
                TotalPrice = sale.TotalPrice,
                State = sale.State
            };
        }

        public async Task AddSale(CreateViewModel saleModel)
        {
            // we create the date with the current date
            var dateOrderIncome = DateTime.Now;

            Sale sale = new Sale
            {               
                IdUser = saleModel.IdUser,
                IdClient = saleModel.IdClient,
                TypeSale = saleModel.TypeSale,
                VoucherSeries = saleModel.VoucherSeries,
                VoucherNumber = saleModel.VoucherNumber,                
                TotalPrice = saleModel.TotalPrice,
                State = "Accepted"
            };

            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();

            // Addind Details to DetailSale
            var id = sale.IdSale;

            // We loop de property details in CreateViewModel to add every
            // detail in SaleDetail Table in the same method Add of Sale
            foreach (var det in saleModel.Details)
            {
                SaleDetail detail = new SaleDetail
                {
                    IdSale = id,
                    IdProduct = det.IdProduct,
                    Amount = det.Amount,
                    Discount = det.Discount,
                    UnitCost = det.UnitCost,
                    UnitPrice = det.UnitPrice
                };
                _context.saleDetails.Add(detail);
            }
            await _context.SaveChangesAsync();            
        }

        public async Task UpdateSale(UpdateViewModel saleModel)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s =>
            s.IdSale == saleModel.IdSale);

            sale.IdSale = saleModel.IdSale;
            sale.IdUser = saleModel.IdUser;
            sale.IdClient = saleModel.IdClient;
            sale.TypeSale = saleModel.TypeSale;
            sale.VoucherSeries = saleModel.VoucherSeries;
            sale.VoucherNumber = saleModel.VoucherNumber;            
            sale.TotalPrice = saleModel.TotalPrice;
            sale.State = saleModel.State;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSale(Sale sale)
        {
            _context.Sales.Remove(sale);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaleExists(int id)
        {
            return await _context.Sales.AnyAsync(s => s.IdSale == id);
        }

        public async Task<Sale> SearchSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return null;
            }

            return sale;
        }

      
    }
}
