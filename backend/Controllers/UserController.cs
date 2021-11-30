using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsersAsync()
        {
            var users = await _userService.getAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> createUsersAsync(List<User> users)
        {
            try
            {
                await _userService.createUsers(users);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Message = e.Message
                });
            }
        }

    }
}