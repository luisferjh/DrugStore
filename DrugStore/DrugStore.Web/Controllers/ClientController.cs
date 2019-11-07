using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.People.Client;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Client
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Clients = await _clientService.List();
            return Clients;
        }

        // GET: api/Client/GetByPhoneNumber/phoneNumber
        [HttpGet("[action]/{phoneNumber}")]
        public async Task<IActionResult> GetByPhoneNumber([FromRoute] string phoneNumber)
        {
            var client = await _clientService.GetClientByPhoneNumber(phoneNumber);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);         
        }

        // GET: api/Client/ListInSale/phoneNumber
        [HttpGet("[action]/{name}/{lastName}")]
        public async Task<IEnumerable> ListInSale([FromRoute] string name, [FromRoute] string lastName)
        {
            var clients = await _clientService.ClientInSale(name, lastName);
            return clients;
        }

        // GET: api/Client/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var client = await _clientService.GetClient(id);

            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // POST: api/Client/model
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clientService.AddClient(clientModel);
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

        // PUT: api/Client/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel clientModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (clientModel.IdClient <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _clientService.UpdateClient(clientModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _clientService.ClientExists(clientModel.IdClient)))
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

        // PUT: api/client/Deactivate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _clientService.DeactivateClient(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _clientService.ClientExists(id)))
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

        // PUT: api/client/activate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _clientService.ActivateClient(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _clientService.ClientExists(id)))
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var client = await _clientService.SearchClient(id);

            // check out if exists and get object Entity
            if (client == null)
            {
                return NotFound();
            }

            try
            {
                await _clientService.DeleteClient(client);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(client);
        }
    }
}
