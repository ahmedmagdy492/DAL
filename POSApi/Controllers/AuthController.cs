using BLL.Services;
using DAL.Models;
using POSApi.ViewModels;
using SharedLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace POSApi.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController()
        {
            _authService = new AuthService(new Sha256Hashing());
            _userService = new UserService();
        }

        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Input");

            var user = await _authService.Login(model.Username, model.Password);

            if (user == null) return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Input");

            var existingUser = _userService.GetUserByUsername(model.Username);

            if (existingUser != null) return BadRequest("Username is already taken");

            var user = new User
            {
                IsCustomer = true,
                Username = model.Username,
                Password = model.Password,
                Name = model.Name
            };
            var createdUser = await _authService.Register(user);
            return Ok(createdUser);
        }
    }
}
