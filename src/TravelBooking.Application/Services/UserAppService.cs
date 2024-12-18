using AutoMapper;
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

        public UserAppService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

                var userEntity = _mapper.Map<Users>(createUserDto);
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
    }

}
