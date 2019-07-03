using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Services.People;
using DrugStore.Web.Models.People.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role/List
        [HttpGet("[action]")]
        public async Task<IEnumerable> List()
        {
            var roles = await _roleService.List();
            return roles;
        }

        // GET: api/Role/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var role = await _roleService.GetRole(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST: api/Role/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _roleService.AddRole(model);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Role/Activate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _roleService.DeactivateRole(id);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _roleService.RoleExists(id)))
                {
                    return NotFound(ex);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Role/Deactivate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _roleService.DeactivateRole(id);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _roleService.RoleExists(id)))
                {
                    return NotFound(ex);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Role/Update/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdRole <= 0)
            {
                return BadRequest(model);
            }

            try
            {
                await _roleService.UpdateRole(model);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _roleService.RoleExists(model.IdRole)))
                {
                    return NotFound(ex);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(model);
        }
    }
}
