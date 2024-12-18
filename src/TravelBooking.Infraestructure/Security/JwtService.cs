using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Infraestructure.Security
{
    public class JwtService : IJwtService
    {
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        private readonly int expiresInMinutes;

        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration containing JWT settings such as SecretKey, Issuer, Audience, and TokenExpiryInMinutes.
        /// </param>
        /// <remarks>
        /// Extracts JWT configuration values from the provided <see cref="IConfiguration"/> instance. 
        /// These values are used to generate and validate JSON Web Tokens (JWTs).
        /// </remarks>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            key = configuration["JwtSettings:SecretKey"];
            issuer = configuration["JwtSettings:Issuer"];
            audience = configuration["JwtSettings:Audience"];
            expiresInMinutes = int.Parse(configuration["JwtSettings:TokenExpiryInMinutes"]);
        }
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }
    }
}
