using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Store.Product;
using DrugStore.Web.Services.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Authorize(Roles = "Admin, Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: api/Product/list
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var products = await _productService.List();
            return products;
        }

        // GET: api/Product/listInSale/text
        [HttpGet("[action]/{text}")]
        public async Task<IEnumerable> ListInSale([FromRoute] string text)
        {
            var products = await _productService.ListInSale(text);
            return products;
        }

        // GET: api/Product/id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/GetByBarCode/barcode
        [HttpGet("[action]/{barcode}")]
        public async Task<IActionResult> GetByBarCode([FromRoute] string barCode)
        {
            var product = await _productService.GetProductByBarCode(barCode);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.AddProduct(product);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT: api/Product/Update/
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.IdProduct <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateProduct(product);
            }
            catch (NullReferenceException)
            {
                if (!(await _productService.ProductExists(product.IdProduct)))
                {
                    return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/product/Delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productService.SearchProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            try
            {
                await _productService.DeleteProduct(product);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        // PUT: api/Product/Deactivate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _productService.DeactivateProduct(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _productService.ProductExists(id)))
                {
                    return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Product/activate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _productService.ActivateProduct(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _productService.ProductExists(id)))
                {
                    return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

