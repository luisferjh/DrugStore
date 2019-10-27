 using DrugStore.Data;
using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public class ProductService : IProductService
    {
        //Brought context of database 
        private readonly DbContextDrugStore _context;
        public ProductService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductViewModel
            {
                IdProduct = product.IdProduct,
                IdCategory = product.IdCategory,
                IdLaboratory = product.IdLaboratory,
                ProductName = product.ProductName,
                BarCode = product.BarCode,
                Indicative = product.Indicative,
                Stock = product.Stock,
                UnitPrice = product.UnitPrice,
                SalePrice = product.SalePrice,
                Condition = product.Condition
            };
        }

        public async Task<ProductViewModel> GetProductByBarCode(string barCode)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(l => l.Laboratory)
                .Where(c => c.Condition == true)
                .SingleOrDefaultAsync(c => c.BarCode == barCode);

            if (product == null)
            {
                return null;
            }

            return new ProductViewModel
            {
                IdProduct = product.IdProduct,
                IdCategory = product.IdCategory,
                Category = product.Category.Name,
                IdLaboratory = product.IdLaboratory,
                Laboratory = product.Laboratory.LaboratoryName,
                ProductName = product.ProductName,
                BarCode = product.BarCode,
                Indicative = product.Indicative,
                Stock = product.Stock,
                UnitPrice = product.UnitPrice,
                SalePrice = product.SalePrice,
                Condition = product.Condition
            };
        }

        public async Task<IEnumerable<ProductViewModel>> List()
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(l => l.Laboratory)
                .ToListAsync();

            return product.Select(p => new ProductViewModel
            {
                IdProduct = p.IdProduct,
                IdCategory = p.IdCategory,
                Category = p.Category.Name,
                IdLaboratory = p.IdLaboratory,
                Laboratory = p.Laboratory.LaboratoryName,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                Indicative = p.Indicative,
                Stock = p.Stock,
                UnitPrice = p.UnitPrice,
                SalePrice = p.SalePrice,
                Condition = p.Condition
            });
        }

        public async Task<IEnumerable<ProductViewModel>> ListInSale(string text)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(l => l.Laboratory)
                .Where(c => c.ProductName.Contains(text))
                .Where(c => c.Condition == true)
                .ToListAsync();

            return product.Select(p => new ProductViewModel
            {
                IdProduct = p.IdProduct,
                IdCategory = p.IdCategory,
                Category = p.Category.Name,
                IdLaboratory = p.IdLaboratory,
                Laboratory = p.Laboratory.LaboratoryName,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                Indicative = p.Indicative,
                Stock = p.Stock,
                UnitPrice = p.UnitPrice,
                SalePrice = p.SalePrice,
                Condition = p.Condition
            });
        }

        public async Task AddProduct(CreateViewModel model)
        {
            Product product = new Product
            {
                IdCategory = model.IdCategory,
                IdLaboratory = model.IdLaboratory,
                ProductName = model.ProductName,
                BarCode = model.BarCode,
                Indicative = model.Indicative,
                Stock = model.Stock,
                UnitPrice = model.UnitPrice,
                SalePrice = model.SalePrice,
                Condition = model.Condition
            };

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(UpdateViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p =>
            p.IdProduct == model.IdProduct);

            product.IdCategory = model.IdCategory;
            product.IdLaboratory = model.IdLaboratory;
            product.ProductName = model.ProductName;
            product.BarCode = model.BarCode;
            product.Indicative = model.Indicative;
            product.Stock = model.Stock;
            product.UnitPrice = model.UnitPrice;
            product.SalePrice = model.SalePrice;       

            await _context.SaveChangesAsync();

        }

        //Delete product by id
        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task ActivateProduct(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.IdProduct == id);

            product.Condition = true;

            await _context.SaveChangesAsync();
        }

        public async Task DeactivateProduct(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.IdProduct == id);

            product.Condition = false;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.IdProduct == id);
        }

        public async Task<Product> SearchProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            return product;
        }

    }
}
