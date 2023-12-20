using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JAMKCourseReviewAPI.Models;
using JAMKCourseReviewAPI.Data;

namespace JAMKCourseReviewAPI.Services
{
    public interface IUserService
    {
        User Create(User user);
        string Authenticate(string username, string password);
        string GenerateTestToken(); // Testing token generation
        bool ValidateToken(string token); // Testing token validation
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Create(User user)
        {
            // Hash the password before saving the user to the database
            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            // Verify the hashed password
            if (user == null || password == null || !VerifyPassword(password, user.Password))
                return null;

            // Generate a JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateTestToken() // Testing token generation
        {
            // Generate a JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Test User") }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token) // Testing token validation, mirrors the middleware used in Program.cs
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            
            }

            // Generate a salt
            var salt = new byte[16];
            using (var rng = RNGCryptoServiceProvider.Create())
            {
                rng.GetBytes(salt);
            }

            // Generate the hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(20);

            // Combine the salt and hash
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to a string and return
            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string savedPasswordHash)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            
            }

            // Extract the bytes from the saved password hash
            var hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Get the salt
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(20);

            // Compare the results
            // for (int i = 0; i < 20; i++)
            // {
            //     if (hashBytes[i + 16] != hash[i])
            //         return false;
            // }

            // return true;

            // Compare the results using secure comparison
            return hashBytes.Skip(16).SequenceEqual(hash);
        }
    }
}