using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Auth;

namespace PetSpaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly PetSpaDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(PetSpaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Status == "active");

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            int? customerId = null;
            if (user.Role == "customer")
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.UserId == user.UserId);
                customerId = customer?.CustomerId;
            }

            var token = GenerateJwtToken(user.UserId, user.Email, user.Role);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                ImageUrl = user.ImageUrl,
                CustomerId = customerId
            };
        }

        public async Task<LoginResponseDto?> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return null;

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "customer",
                Status = "active",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var customer = new Customer
            {
                UserId = user.UserId,
                Address = dto.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user.UserId, user.Email, user.Role);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                ImageUrl = user.ImageUrl,
                CustomerId = customer.CustomerId
            };
        }

        public string GenerateJwtToken(int userId, string email, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}