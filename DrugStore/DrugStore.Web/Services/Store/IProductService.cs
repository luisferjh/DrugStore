  using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> List();
        Task<ProductViewModel> GetProduct(int id);
        Task<ProductViewModel> GetProductByBarCode(string barCode);
        Task<IEnumerable<ProductViewModel>> ListInSale(string text);
        Task AddProduct(CreateViewModel model);
        Task UpdateProduct(UpdateViewModel model);
        Task DeleteProduct(Product Pproduct);
        Task ActivateProduct(int id);
        Task DeactivateProduct(int id);
        Task<bool> ProductExists(int id);
        Task<Product> SearchProductById(int id);
    }
}

