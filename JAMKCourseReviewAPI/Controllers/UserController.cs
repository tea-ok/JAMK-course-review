using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JAMKCourseReviewAPI.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // GET: /api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(Register model)
        {
            var newUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // GET: /api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { message = "Logged in" });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        // GET: /api/users/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out" });
        }

        // GET: /api/users/protected
        [Authorize]
        [HttpGet("protected")] // Testing authorization
        public IActionResult Protected()
        {
            string username = User.Identity.Name;
            return Ok(new { message = $"Hello {username}" });
        }
    }
}