using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Store.Product;
using DrugStore.Web.Services.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: api/Category
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var products = await _productService.List();
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
            return Ok();
        }

        // PUT: api/Product/5
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
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _productService.ProductExists(product.IdProduct)))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
                      
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
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
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            return Ok(product);
        }
    }
}
