using AutoMapper;
using Azure.Core;
using MediatR;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Users;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;

namespace TravelBooking.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public UserAppService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<RequestResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                // Validación de datos
                if (string.IsNullOrWhiteSpace(createUserDto.FirstName) || string.IsNullOrWhiteSpace(createUserDto.Email))
                {
                    return RequestResult<UserDto>.CreateUnsuccessful(new[] { "Name and email are required." });
                }
                if (!IsValidEmail(createUserDto.Email))
                {
                    return RequestResult<UserDto>.CreateUnsuccessful(new[] { "The email provided is not valid." });
                }
                var userEntity = _mapper.Map<Users>(createUserDto);
                // Hash the password
                var (passwordHash, passwordSalt) = _jwtService.HashPassword(createUserDto.Password);
                userEntity.PasswordHash = passwordHash;
                userEntity.PasswordSalt = passwordSalt;
                await _userRepository.CreateAsync(userEntity);

                var userDto = _mapper.Map<UserDto>(userEntity);
                return RequestResult<UserDto>.CreateSuccessful(userDto, new[] { "User created successfully." });
            }
            catch (Exception ex)
            {
                return RequestResult<UserDto>.CreateError($"Error creating user: {ex.Message}");
            }
        }

        public async Task<RequestResult<UserDto>> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(userId);
                if (existingUser == null)
                {
                    return RequestResult<UserDto>.CreateUnsuccessful(new[] { "User not found." });
                }

                _mapper.Map(updateUserDto, existingUser);
                await _userRepository.UpdateAsync(existingUser);

                var userDto = _mapper.Map<UserDto>(existingUser);
                return RequestResult<UserDto>.CreateSuccessful(userDto, new[] { "User updated successfully." });
            }
            catch (Exception ex)
            {
                return RequestResult<UserDto>.CreateError($"Error updating user: {ex.Message}");
            }
        }

        public async Task<RequestResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

                return RequestResult<IEnumerable<UserDto>>.CreateSuccessful(userDtos);
            }
            catch (Exception ex)
            {
                return RequestResult<IEnumerable<UserDto>>.CreateError($"Error querying users: {ex.Message}");
            }
        }

        #region Methods privates
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

}
