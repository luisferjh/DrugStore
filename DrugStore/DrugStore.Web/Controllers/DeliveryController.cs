using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Sales.Delivery;
using DrugStore.Web.Services.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        // GET: api/Delivery
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Delivery = await _deliveryService.List();
            return Delivery;
        }

        // GET: api/Delivery/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var delivery = await _deliveryService.GetDelivery(id);

            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        // POST: api/Delivery
        [HttpPost("action")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel deliveryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryService.AddDelivery(deliveryModel);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Delivery/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel deliveryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (deliveryModel.IdSale <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _deliveryService.UpdateDelivery(deliveryModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _deliveryService.DeliveryExists(deliveryModel.IdClient)))
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
            var delivery = await _deliveryService.SearchDelivery(id);

            // check out if exists and get object Entity
            if (delivery == null)
            {
                return NotFound();
            }

            try
            {
                await _deliveryService.DeleteDelivery(delivery);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(delivery);
        }
    }
}
