using BCrypt.Net;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Register(string email, string password)
        {
            if (_dbContext.Users.Any(u => u.Email == email))
            {
                return "User already exists";
            }

            var hasedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = hasedPassword,
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return "User registered successfully";
        }

        public User ? Login(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return isValid ? user : null;
        }

        public string GenerateToken(User user, IConfiguration config)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer : config["Jwt:Issuer"],
                audience : config["Jwt:Audience"],
                claims : claims,
                expires : DateTime.Now.AddHours(1),
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
