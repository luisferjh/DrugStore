﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Services.People;
using DrugStore.Web.Models.People.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace DrugStore.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserViewModel>> List()
        {
            var users = await _userService.List();
            return users;
        }

        // GET: api/User/Get/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/User/GetProfile/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] int id)
        {
            var user = await _userService.UserProfileNav(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/Activate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _userService.ActivateUser(id);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _userService.UserExists(id)))
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

        // PUT: api/User/Deactivate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _userService.DeactivateUser(id);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _userService.UserExists(id)))
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

        // POST: api/User
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                await _userService.AddUser(model);
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

        // PUT: api/User/Update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdUser <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateUser(model);
            }
            catch (NullReferenceException ex)
            {
                if (!(await _userService.UserExists(model.IdUser)))
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

        // PUT: api/User/UpdatePassword/id
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdatePassword([FromRoute] int id, [FromBody] UserPasswordViewModel userPass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest();
            }
                    
            try
            {
                await _userService.ChangePassword(id, userPass);
            }            
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(ex);
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

    }
}
