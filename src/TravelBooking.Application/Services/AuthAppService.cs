using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.Repositories;

namespace TravelBooking.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _tokenService;
        private readonly ILogger<AuthAppService> _logger;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "AuthAppService";

        public AuthAppService(IJwtService jwtService, ILogger<AuthAppService> logger, IUserRepository userRepository)
        {
            _tokenService = jwtService;
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            try
            {
                // 1. Validate credentials against the domain layer
                var user =  await _userRepository.GetUserByUsernameAsync(username);
                if (user == null)
                    return string.Empty;

                // 2. Generate the token  
                bool isValid = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
                if (isValid)
                {
                    return _tokenService.GenerateToken(user.Username);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{className} - {new StackFrame().GetMethod()?.Name ?? "UnknownMethod"}:{new StackFrame().GetFileLineNumber()}");
                return string.Empty;
            }
        }

        #region Methods Privates

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
        #endregion
    }
}
