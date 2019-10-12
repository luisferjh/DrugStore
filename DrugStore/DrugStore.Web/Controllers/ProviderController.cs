using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.People.Provider;
using DrugStore.Web.Services.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Authorize(Roles = "Admin, Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }
        // GET: api/Provider
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Providers = await _providerService.List();
            return Providers;
        }

        // GET: api/Provider/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var provider = await _providerService.GetProvider(id);

            if (provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }

        // POST: api/Provider
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel providerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _providerService.AddProvider(providerModel);
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

        // PUT: api/Provider/
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel providerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (providerModel.IdProvider <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _providerService.UpdateProvider(providerModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _providerService.ProviderExists(providerModel.IdProvider)))
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
            var provider = await _providerService.SearchProvider(id);

            // check out if exists and get object Entity
            if (provider == null)
            {
                return NotFound();
            }

            try
            {
                await _providerService.DeleteProvider(provider);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(provider);
        }
    }
}
