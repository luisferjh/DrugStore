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

            return new ProductViewModel {
                IdProduct = product.IdProduct,
                IdCategory = product.IdCategory,
                IdLaboratory = product.IdLaboratory,
                ProductName = product.ProductName,
                BarCode = product.BarCode,
                Indicative = product.Indicative,
                Stock = product.Stock,                
                Price = product.Price,
                Condition = product.Condition
            };
        }

        public async Task<IEnumerable<ProductViewModel>> List()
        {
            var product = await _context.Products                
                .Include(c => c.Category)
                .Include(l => l.Laboratory)                
                .ToListAsync();

            return product.Select(p => new ProductViewModel {
                IdProduct = p.IdProduct,
                IdCategory = p.IdCategory,
                Category = p.Category.Name,
                IdLaboratory = p.IdLaboratory,
                Laboratory = p.Laboratory.LaboratoryName,
                ProductName = p.ProductName,
                BarCode = p.BarCode,
                Indicative = p.Indicative,
                Stock = p.Stock,               
                Price = p.Price,
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
                Price = model.Price,
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
            product.Price = model.Price;
            product.Condition = model.Condition;         

            await _context.SaveChangesAsync();
                          
        }
        
        //Delete product by id
        public async Task DeleteProduct(Product product)
        {           
            _context.Products.Remove(product);

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
