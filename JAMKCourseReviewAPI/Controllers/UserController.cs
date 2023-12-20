using System;
using Microsoft.AspNetCore.Mvc;
using JAMKCourseReviewAPI.Models;
using JAMKCourseReviewAPI.Services;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            try
            {
                var newUser = new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    EmailAddress = user.EmailAddress,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                
                _userService.Create(newUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                var token = _userService.Authenticate(model.Username, model.Password);

                if (token == null)
                    return Unauthorized();

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("test-token-add")] // Testing token generation
        public IActionResult GenerateTestToken()
        {
            try
            {
                var token = _userService.GenerateTestToken();

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Return a HTTP Status code and a message in case of error
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("test-token-validity")] // Testing token validation
        public IActionResult ValidateTestToken(string token)
        {
            try
            {
                bool isValid = _userService.ValidateToken(token);

                if (isValid)
                {
                    return Ok(new { message = "Token is valid" });
                }
                else
                {
                    return BadRequest(new { message = "Token is not valid" });
                }
            }
            catch (Exception ex)
            {
                // Return a HTTP Status code and a message in case of error
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}