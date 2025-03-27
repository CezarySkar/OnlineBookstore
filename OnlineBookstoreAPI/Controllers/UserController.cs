using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookstoreAPI.Data;
using OnlineBookstoreAPI.DTOs;
using OnlineBookstoreAPI.Models;
using OnlineBookstoreAPI.Services;

namespace OnlineBookstoreAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IConfiguration _config;
        public UserController(ApplicationDbContext context, IConfiguration config)
        {
            _ctx = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new User 
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password,
                Role = createUserDto.Role
            };

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(AddUser), new {message = "User created successfully!"}, user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _ctx.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenService = new TokenService(_config);
            switch (user.Role)
            {
                case "Admin":
                    var token = tokenService.GenerateToken(loginDto.Username, "Admin");
                    return Ok(new {Token =  token});
                case "Standard":
                    var tokenS = tokenService.GenerateToken(loginDto.Username, "Standard");
                    return Ok(new { Token = tokenS });
                default: return BadRequest("Role not handlend yet.");
            }

        }
    }
}
