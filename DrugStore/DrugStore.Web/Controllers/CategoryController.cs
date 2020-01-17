using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Store.Category;
using DrugStore.Web.Services.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Authorize(Roles ="Seller,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Categories = await _categoryService.List();
            //return Ok(Categories);
            return Categories;
        }

        // GET: api/Category/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var Category = await _categoryService.Get(id);

            if (Category == null)
            {
                return NotFound();
            }
            return Ok(Category);
        }

        // POST: api/Category
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }                       

            try
            {
                await _categoryService.AddCategory(category);
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

        // PUT: api/Category     
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdCategory <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.UpdateCategory(model);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _categoryService.CategoryExists(model.IdCategory)))
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
            var category = await _categoryService.SearchCategory(id);

            // check out if exists and get object Entity
            if (category == null)
            {
                return NotFound();
            }
           
            try
            {
                await _categoryService.DeleteCategory(category);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }          
        
            return Ok(category);
        }
    }
}
