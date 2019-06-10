using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Sales.Sale;
using DrugStore.Web.Services.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/Sale/5
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Sales = await _saleService.List();
            return Sales;
        }

        // GET: api/Sale
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var sale = await _saleService.GetSale(id);

            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }
       
        // POST: api/Sale
        [HttpPost("action")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel saleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _saleService.AddSale(saleModel);
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

        // PUT: api/Sale/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel saleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (saleModel.IdClient <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _saleService.UpdateSale(saleModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _saleService.SaleExists(saleModel.IdSale)))
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
            var sale = await _saleService.SearchSale(id);

            // check out if exists and get object Entity
            if (sale == null)
            {
                return NotFound();
            }

            try
            {
                await _saleService.DeleteSale(sale);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(sale);
        }
    }
}
