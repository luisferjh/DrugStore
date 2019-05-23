using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Orders.Order;
using DrugStore.Web.Services.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderIncomeController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderIncomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/OrderIncome
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Orders = await _orderService.List();
            return Orders;
        }

        // GET: api/OrderIncome/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var order = await _orderService.GetOrderIncome(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/OrderIncome
        [HttpPost("action")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderService.AddOrderIncome(orderModel);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/OrderIncome/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (orderModel.IdOrderIncome <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _orderService.UpdateOrderIncome(orderModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _orderService.OrderIncomeExists(orderModel.IdOrderIncome)))
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
            var order = await _orderService.SearchOrderIncome(id);

            // check out if exists and get object Entity
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                await _orderService.DeleteOrderIncome(order);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(order);
        }
    }
}
