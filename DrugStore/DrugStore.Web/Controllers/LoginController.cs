using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Web.Models.People.User;
using DrugStore.Web.Services.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userCredentials = await _userService.Login(model);

            if (userCredentials == null)
            {
                return NotFound();
            }

            if (!_userService.CheckPassword(model.Password, userCredentials.PasswordHash, userCredentials.PasswordSalt))
            {
                return NotFound();
            }

            var token = _userService.GenerateToken(userCredentials);

            return Ok(new { token = token });
        }
        
    }
}
