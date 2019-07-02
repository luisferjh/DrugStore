using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Services.People;
using DrugStore.Web.Models.People.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DrugStore.Web.Controllers
{
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

        // GET: api/User/5
        [HttpGet("[action]/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
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

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
