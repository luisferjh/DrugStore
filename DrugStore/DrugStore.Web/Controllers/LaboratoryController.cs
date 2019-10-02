using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.Store.Laboratory;
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
    public class LaboratoryController : ControllerBase
    {
        private readonly ILaboratoryService _laboratoryService;
        public LaboratoryController(ILaboratoryService laboratoryService)
        {
            _laboratoryService = laboratoryService;
        }

        // GET: api/Category
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var labs = await _laboratoryService.List();
            //return Ok(Categories);
            return labs;
        }

        // GET: api/Laboratory
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetLab([FromRoute] int id)
        {
            var lab = await _laboratoryService.GetLaboratory(id);

            if (lab == null)
            {
                return NotFound();
            }

            return Ok(lab);
        }

        // POST: api/Laboratory
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            try
            {
                await _laboratoryService.AddLaboratory(model);
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

        // PUT: api/Laboratory    
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateLab([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdLaboratory <= 0)
            {
                return BadRequest();
            }
                     
            try
            {
                await _laboratoryService.UpdateLaboratory(model);
            }
            catch (DbUpdateException)
            {
                if (!(await _laboratoryService.LabExists(model.IdLaboratory)))
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

        // PUT: api/laboratory/Deactivate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _laboratoryService.DeactivateLab(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _laboratoryService.LabExists(id)))
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

        // PUT: api/laboratory/activate/2
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _laboratoryService.ActivateLab(id);
            }
            catch (NullReferenceException)
            {
                if (!(await _laboratoryService.LabExists(id)))
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
        public async Task<IActionResult> DeleteLab([FromRoute] int id)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var lab = await _laboratoryService.SearchLabById(id);

            //check out if exists
            if (lab == null)
            {
                return NotFound();
            }
           
            try
            {
                await _laboratoryService.DeleteLaboratory(lab);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            
            return Ok(lab);
        }
    }
}
