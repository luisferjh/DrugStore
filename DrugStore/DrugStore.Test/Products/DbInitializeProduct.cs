using DrugStore.Data;
using DrugStore.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrugStore.Test.Products
{
    public class DbInitializeProduct
    {
        public DbInitializeProduct()
        {

        }
        public void Initial(DbContextDrugStore context)
        {
            // saved categories
            context.Categories.AddRange(

                new Category() { Name = "Generico",
                                 Description = "productos de caracter generico",
                                 Condition = true },
                new Category() { Name = "Comercial",
                                 Description = "productos de caracter comercial",
                                 Condition = true }
            );

            context.SaveChanges();

            // Saved Laboratories

            context.Laboratories.AddRange(

                new Laboratory() {
                    LaboratoryName = "Bayer",
                    Description = "Bayer es una compañía de innovación que investiga" +
                                  " en las áreas de Ciencias de la Vida centradas en la " +
                                  "salud y la agricultura.",
                    Condition = true
                },
                new Laboratory() {
                    LaboratoryName = "Tecnoquimicas",
                    Description = "Somos un grupo empresarial colombiano de reconocido liderazgo en la " +
                                    "industria farmacéutica y de consumo masivo, comprometido desde hace más " +
                                    "de 80 años con el crecimiento económico y el avance social de nuestro país",
                    Condition = true
                },
                new Laboratory() {
                    LaboratoryName = "Genfar",
                    Description = "Genfar es una empresa que creció con el compromiso de fabricar y llevar " +
                                    "medicamentos de calidad a cada vez más personas",
                    Condition = true
                },
                new Laboratory() {
                    LaboratoryName = "Sanofi",
                    Description = "ofrecemos soluciones de salud innovadoras en una amplia gama de afecciones " +
                                    "de salud: ya sea una afección común como un resfriado, alergias, problemas " +
                                    "digestivos o afecciones graves tales como el cáncer o la esclerosis múltiple;",
                    Condition = true }
            );

            context.SaveChanges();

            context.Products.AddRange(

                new Product() {
                    IdCategory = 1,
                    IdLaboratory = 2,
                    ProductName = "Acetaminofen",
                    BarCode = "sdfsdf",
                    Indicative = "tabletas, 3 al dia",
                    Stock = 100,
                    UnitPrice = 800,
                    SalePrice = 1000,
                    Condition = true
                },
                new Product()
                {
                    IdCategory = 1,
                    IdLaboratory = 1,
                    ProductName = "Trimebutina",
                    BarCode = "sdfsdf",
                    Indicative = "tabletas",
                    Stock = 10,
                    UnitPrice = 3000,
                    SalePrice = 4000,
                    Condition = true
                }           
            );

            context.SaveChanges();
        }

        public int CountRecords(DbContextDrugStore context)
        {
            return context.Products.Count();
        }
    }
}
