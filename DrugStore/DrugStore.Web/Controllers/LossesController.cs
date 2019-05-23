using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Store.Losses;
using DrugStore.Web.Services.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LossesController : ControllerBase
    {
        private readonly ILossesService _lossesService;
        public LossesController(ILossesService lossesService)
        {
            _lossesService = lossesService;
        }
        // GET: api/Losses
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var Categories = await _lossesService.List();          
            return Categories;
        }

        // GET: api/Losses/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var loss = await _lossesService.GetLoss(id);

            if (loss == null)
            {
                return NotFound();
            }
            return Ok(loss);
        }

        // POST: api/Losses
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel lossModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _lossesService.AddLoss(lossModel);
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Losses/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateViewModel lossModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lossModel.IdLosses <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _lossesService.UpdateLoss(lossModel);
            }
            catch (DbUpdateException)
            {
                // check out if exists
                if (!(await _lossesService.LossExists(lossModel.IdLosses)))
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
            var loss = await _lossesService.SearchLoss(id);

            // check out if exists and get object Entity
            if (loss == null)
            {
                return NotFound();
            }

            try
            {
                await _lossesService.DeleteLoss(loss);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok(loss);
        }
    }
}
