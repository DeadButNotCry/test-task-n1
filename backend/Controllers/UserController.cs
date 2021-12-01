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
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsersAsync(List<User> users)
        {
            try
            {
                await _userService.CreateUsers(users);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAllUsersAsync()
        {
            await _userService.DeleteAllUsers();
            return NoContent();
        }

        [HttpGet("LifeTimeHistogram")]
        public async Task<IActionResult> GetHistogram()
        {
            var histogramData = await _userService.GetHistogramData();
            return Ok(histogramData);
        }

        [HttpGet("RollingRetention")]
        public IActionResult GetRollingRetention()
        {
            return Ok(new
            {
                Percent = _userService.GetRollingRetention()
            });
        }
    }
}