using DrugStore.Web.Models.Store.Product;
using DrugStore.Web.Models.Store;
using System;
using System.Collections.Generic;
using System.Text;
using DrugStore.Data;

namespace DrugStore.Test.Products
{
    public class CreateProduct
    {
        public CreateViewModel add() {
            return new CreateViewModel
            {
                IdCategory = 2,
                IdLaboratory = 4,
                ProductName = "Dolex",
                BarCode = "",
                Indicative = "Liquido",
                Stock = 10,
                Price = 5000,
                Condition = true
            };
        }

        public CreateViewModel addInvalid()
        {
            return new CreateViewModel
            {              
                IdLaboratory = 4,
                ProductName = "Dolex",
                BarCode = "",
                Indicative = "Liquido",
                Stock = 10,
                Price = 5000,
                Condition = true
            };
        }

        //public void UpdateProduct(DbContextDrugStore context, UpdateViewModel model)
        //{
        //    var productUpdate = await context.Products.FindAsync(idproduct);

        //    productUpdate.IdProduct = model.IdProduct;
        //    productUpdate.IdCategory = model.IdCategory;
        //    productUpdate.IdLaboratory = model.IdLaboratory;
        //    productUpdate.ProductName = model.ProductName;
        //    productUpdate.BarCode = model.BarCode;
        //    productUpdate.Indicative = model.Indicative;
        //    productUpdate.Stock = model.Stock;
        //    productUpdate.Price = model.Price;
        //    productUpdate.Condition = model.Condition;

        //    context.SaveChanges();
        //}
    }
}
