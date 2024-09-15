using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Proxy.Models;
using Proxy.Interfaces;
using Serilog;

namespace Proxy.Controllers
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                
                return NotFound($"User with id {id} not found.");
            }
            

            return Ok(user);
        }
    }
}
