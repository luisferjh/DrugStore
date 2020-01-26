using DrugStore.Data;
using DrugStore.Web.Controllers;
using DrugStore.Web.Services.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DrugStore.Test
{
    public class CategoryControllerTest
    {
        private CategoryService service;
        public static DbContextOptions<DbContextDrugStore> dbContextOptions { get; }        

        static CategoryControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<DbContextDrugStore>()
                .UseInMemoryDatabase(databaseName: "Test_database")
                .Options;
        }

        public CategoryControllerTest()
        {
            var context = new DbContextDrugStore(dbContextOptions);
            DbInitialize db = new DbInitialize();
            db.Initial(context);

            // corregir o arreglar esto para adaptar el IMapper : PENDIENTE
            //service = new CategoryService(context);
        }

        #region Get By Id

        [Fact]
        public async void Task_GetPostById_Return_OkResult()
        {
            //Arrange
            var controller = new CategoryController(service);
            var categoryId = 2;

            //Act 
            var data = await controller.Get(categoryId);

            //Assert 
            Assert.IsType<OkObjectResult>(data);
        }
        #endregion

    }
}
