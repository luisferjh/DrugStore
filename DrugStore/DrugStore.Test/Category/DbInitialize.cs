using DrugStore.Data;
using DrugStore.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Test
{
    public class DbInitialize
    {
        public DbInitialize()
        {

        }
        public void Initial(DbContextDrugStore context)
        {
            context.Categories.AddRange(

                new Category() { Name = "Generico", Description = "productos de caracter generico", Condition = true },
                new Category() { Name = "Comercial", Description = "productos de caracter comercial", Condition = true }
            );

            context.SaveChanges();
        }
    }
}
