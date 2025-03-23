using Backend.DTO;
using Backend.Helpers;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Backend.Services.Implementations
{
    public class UserService : IUserService
    {
        IWorkUnit _workUnit;    
        ILogger<UserService> _logger;

        public UserService(IWorkUnit workUnit, ILogger<UserService> logger)
        {
            _workUnit = workUnit ?? throw new ArgumentNullException(nameof(workUnit));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private UserResponseDTO? ToResponse(User user) => user == null ? null : new UserResponseDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };

        private User FromRequest(UserRequestDTO user) => new User
        {
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            Role = user.Role
        };

        private User FromResponse(UserResponseDTO user) => new User
        {
            UserId = user.UserId,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };

        public async Task<UserResponseDTO> GetUserByIdAsync(int userId)
        {
            var user = await _workUnit.UserDAL.GetAsync(userId); 
            return user == null ? null : ToResponse(user);
        }

        public async Task<List<UserResponseDTO>> GetUsersAsync()
        {
            var users = await _workUnit.UserDAL.GetAllAsync();
            return users.Select(ToResponse).ToList();
        }

        public async Task<UserResponseDTO> AddUserAsync(UserRequestDTO user)
        {
            var userEntity = FromRequest(user);
            if (UsersHelper.CheckUsersRole(userEntity.Role))
            {
                _logger.LogWarning("Invalid role {Role}", userEntity.Role);
                return null;
            }
            await _workUnit.UserDAL.AddAsync(userEntity);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("User {UserId} added", userEntity.UserId);
            return ToResponse(userEntity);
        }

        public async Task<UserResponseDTO> UpdateUserAsync(UserResponseDTO user)
        {
            var userEntity = FromResponse(user);
            if (UsersHelper.CheckUsersRole(userEntity.Role))
            {
                _logger.LogWarning("Invalid role {Role}", userEntity.Role);
                return null;
            }
            await _workUnit.UserDAL.UpdateAsync(userEntity);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("User {UserId} updated", userEntity.UserId);
            return ToResponse(userEntity);
        }

        public async Task<UserResponseDTO> DeleteUserAsync(int userId)
        {
            var user = await _workUnit.UserDAL.GetAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return null;
            }
            await _workUnit.UserDAL.DeleteAsync(user);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("User {UserId} deleted", userId);
            return ToResponse(user);
        }
    }
}
