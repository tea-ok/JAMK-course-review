using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JAMKCourseReviewAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                return Ok(new
                {
                    userId = user.Id,
                    username = user.UserName,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName
                });
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
    }
}