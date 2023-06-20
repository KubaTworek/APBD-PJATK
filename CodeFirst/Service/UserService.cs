using CodeFirst.Middleware;
using CodeFirst.Model;
using CodeFirst.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CodeFirst.Service
{
    public class UserService : IUserService
    {
        private readonly MainDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(MainDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(NewUserRequest request)
        {
            var roleId = _context.Roles
                        .Where(r => r.Name == "User")
                        .Select(r => r.IdRole)
                        .FirstOrDefault();

            var newUser = new User()
            {
                Username = request.Login,
                Salt = Guid.NewGuid().ToString(),
                IdRole = roleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, request.Password + newUser.Salt);

            newUser.Password = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public object GenerateJwt(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Login);
            if (user == null)
            {
                throw new BadRequestException("No found user");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("No found user");
            }

            var secret = "sdn209jsldjp29jdlsjdlsjdisjdklmlmjklml2me9wj92292992929esklmdsmdsmd.sdsd";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:7090",
                audience: "http://localhost:7090",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            _context.SaveChanges();

            return new
            {
                tokenString,
                refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                return Convert.ToBase64String(r);
            }
        }

        public object RefreshToken(string refreshToken)
        {
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid refresh token");
            }

            var loginRequest = new LoginRequest { Login = user.Username };
            var newToken = GenerateJwt(loginRequest);

            return newToken;
        }
    }
}
