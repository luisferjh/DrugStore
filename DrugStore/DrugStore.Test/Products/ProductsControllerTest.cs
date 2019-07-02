using DrugStore.Data;
using DrugStore.Web.Controllers;
using DrugStore.Web.Models.Store.Product;
using DrugStore.Web.Services.Store;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DrugStore.Test.Products
{
    public class ProductsControllerTest
    {
        private ProductService service;
        public static DbContextOptions<DbContextDrugStore> dbContextOptions { get; }
        //var context;
        DbInitializeProduct db;

        static ProductsControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<DbContextDrugStore>()
                .UseInMemoryDatabase(databaseName: "Test_database")
                .Options;
        }

        public ProductsControllerTest()
        {
            var context = new DbContextDrugStore(dbContextOptions);
            db = new DbInitializeProduct();
            db.Initial(context);

            service = new ProductService(context);
        }

        #region Get All
        [Fact]
        public async void List_WhenCalled_ReturnAllItems()
        { 
            //Arrange
            var controller = new ProductController(service);

            //Act
            var products = await controller.List();

            //Assert
            var results = products.Should().BeAssignableTo<IEnumerable<ProductViewModel>>().Subject;
            
        }

        #endregion

        #region Get by Id
        [Fact]
        public async void Get_IdExisting_ReturnOkObjectResult()
        {
            //Arrange
            var controller = new ProductController(service);
            int idProduct = 1;

            //Act
            var productRq = await controller.GetProduct(idProduct);

            //Assert
            var results = productRq.Should().BeOfType<OkObjectResult>().Subject;
            var product = results.Value.Should().BeAssignableTo<ProductViewModel>().Subject;
            product.IdCategory.Should().Be(1);
            
        }
       
        [Fact]
        public async void Get_IdNotExisting_ReturnNotFound()
        {
            //Arrange
            var controller = new ProductController(service);
            int idProduct = 13;

            //Act
            var productRq = await controller.GetProduct(idProduct);

            //Assert
            var result = productRq.Should().BeOfType<NotFoundResult>().Subject;
            Assert.IsType<NotFoundResult>(productRq);
        }

        [Fact]
        public async void Get_IdExisting_ReturnRightItem()
        {
            //Arrange
            var controller = new ProductController(service);
            int idProduct = 2;

            //Act
            var productRq = await controller.GetProduct(idProduct);

            //Assert
            var result = productRq.Should().BeOfType<OkObjectResult>().Subject;
            Assert.Equal(2, result.Value.Should().BeAssignableTo<ProductViewModel>().Subject.IdProduct);
        }
        #endregion

        #region Post Add   

        [Fact]
        public async void Create_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ProductController(service);
            CreateProduct addManager = new CreateProduct();
            var productAdd = addManager.addInvalid();
            controller.ModelState.AddModelError("IdCategory", "Required");

            // Act
            var badResponse = await controller.Create(productAdd);

            // Assert
            var result = badResponse.Should().BeOfType<BadRequestObjectResult>().Subject;
            //Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Create_WhenAdded_OkResult()
        {
            //Arrange
            var controller = new ProductController(service);
            CreateProduct addManager = new CreateProduct();            
            var productAdd = addManager.add();
            //find it
            int idnew = 3;

            //Act 
            var productAddRq = await controller.Create(productAdd);
            var productRq = await controller.GetProduct(idnew);
            var resultProductRq = productRq.Should().BeOfType<OkObjectResult>().Subject;

            //Assert 
            var result = productAddRq.Should().BeOfType<OkResult>().Subject;
            Assert.Equal(idnew, resultProductRq.Value.Should().BeAssignableTo<ProductViewModel>().Subject.IdProduct);           
        }

        [Fact]
        public async void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var controller = new ProductController(service);
            CreateProduct addManager = new CreateProduct();
            var productAdd = addManager.add();
         
            //context   
            var context = new DbContextDrugStore(dbContextOptions);
            

            // Act
            var resultOk = await controller.Create(productAdd);
            var records = db.CountRecords(context);
            var Allproducts = await controller.List();

            // Assert
            var AllresultsList = Allproducts.Should().BeAssignableTo<IEnumerable<ProductViewModel>>().Subject;
            AllresultsList.Should().HaveCount(records);                     
        }

        #endregion

        #region Update
        [Fact]
        public async void Update_validObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var controller = new ProductController(service);
            int idproduct = 2;
                              

            // Act
            var productExist = await controller.GetProduct(idproduct);
            var OkResultGet = productExist.Should().BeOfType<OkObjectResult>().Subject;
            var productValue = OkResultGet.Value.Should().BeAssignableTo<ProductViewModel>().Subject;

            // --Modified         
            var model = new UpdateViewModel();
            model.IdProduct = productValue.IdProduct;
            model.IdCategory = productValue.IdCategory;
            model.IdLaboratory = productValue.IdLaboratory;
            model.ProductName = productValue.ProductName;
            model.BarCode = productValue.BarCode;
            model.Indicative = "2 veces al dia despues de cada comida";
            model.Stock = productValue.Stock;
            model.Price = productValue.Price ;
            model.Condition = productValue.Condition;

            var productUpdateOk = await controller.Update(model);

            // Assert
            var resultOkUpdate = productUpdateOk.Should().BeOfType<OkResult>().Subject;
            
            productExist = await controller.GetProduct(idproduct);
            OkResultGet = productExist.Should().BeOfType<OkObjectResult>().Subject;
            productValue = OkResultGet.Value.Should().BeAssignableTo<ProductViewModel>().Subject;

            Assert.Equal(model.Indicative, productValue.Indicative);
        }

        [Fact]
        public async void Update_invalidObjectPassed_ReturnsBadResult()
        {
            // Arrange
            var controller = new ProductController(service);       

            // --Modified         
            var model = new UpdateViewModel();
            model.IdProduct = -2;
            model.IdCategory = 1;
            model.IdLaboratory = 2;
            model.ProductName = "Axoximocilina";
            model.BarCode = "";
            model.Indicative = "suministrar dos dosis minimas";
            model.Stock = 25;
            model.Price = 12.00m;
            model.Condition = true;

            // Act         
            var productUpdateOk = await controller.Update(model);

            // Assert
            var resultOkUpdate = productUpdateOk.Should().BeOfType<BadRequestResult>().Subject;
                          
        }

        [Fact]
        public async void Update_ObjectNotExistsPassed_ReturnNotFound()
        {
            // Arrange
            var controller = new ProductController(service);

            // --Modified         
            var model = new UpdateViewModel();
            model.IdProduct = 5;
            model.IdCategory = 1;
            model.IdLaboratory = 2;
            model.ProductName = "Axoximocilina";
            model.BarCode = "";
            model.Indicative = "suministrar dos dosis minimas";
            model.Stock = 25;
            model.Price = 12.00m;
            model.Condition = true;

            // Act         
            var productUpdateNotFound = await controller.Update(model);

            // Assert
            var resultNotFound = productUpdateNotFound.Should().BeOfType<NotFoundResult>().Subject;

        }

        //[Fact]
        //public async void Update_ObjectNotValidContextPassed_ReturnBadRequest()
        //{
        //    // Arrange
        //    var controller = new ProductController(service);

        //    // --Modified         
        //    var model = new UpdateViewModel();
        //    model.IdProduct = 2;         
        //    model.IdLaboratory = 12;           
        //    model.BarCode = "";
        //    model.Indicative = "suministrar dos dosis minimas";
        //    model.Stock = 25;
        //    model.Price = 12.00m;
        //    model.Condition = true;

        //    // Act         
        //    var productBadRequest = await controller.Update(model);

        //    // Assert
        //    var resultBadRequest = productBadRequest.Should().BeOfType<BadRequestResult>().Subject;

        //}
        #endregion

        #region Delete
        [Fact]
        public async void Delete_IdNotValid_NotFound()
        {
            //Arrange  
            var controller = new ProductController(service);
            var postId = 10;
            
            //Act  
            var productNotFound = await controller.Delete(postId);

            //Assert  
            var okResult = productNotFound.Should().BeOfType<NotFoundResult>().Subject;
            //Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void Delete_IdValid_OkObjectResult()
        {
            var controller = new ProductController(service);
            var postId = 2;

            //Act  
            var productOk = await controller.Delete(postId);

            //Assert  
            var okResult = productOk.Should().BeOfType<OkObjectResult>().Subject;
            //Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void Remove_IdValid_RemoveOneItem()
        {
            var controller = new ProductController(service);
            var postId = 2;

            //Act  
            var productOk = await controller.Delete(postId);
            var resultListProducts = await controller.List();

            //Assert   
            resultListProducts.Should().NotBeEmpty().And.HaveCount(1);
        }


        #endregion
    }
}

