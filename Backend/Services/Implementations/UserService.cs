using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

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

        UserDTO Convertir(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        User Convertir(UserDTO userDTO)
        {
            return new User
            {
                UserId = userDTO.UserId,
                Username = userDTO.Username,
                PasswordHash = userDTO.PasswordHash,
                Email = userDTO.Email,
                Role = userDTO.Role,
                CreatedAt = userDTO.CreatedAt
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _workUnit.UserDAL.GetAsync(userId); 
            return user == null ? null : Convertir(user);
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _workUnit.UserDAL.GetAllAsync();
            return users.Select(Convertir).ToList();
        }

        public async Task<UserDTO> AddUserAsync(UserDTO user)
        {
            var userEntity = Convertir(user);
            await _workUnit.UserDAL.AddAsync(userEntity);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("User {UserId} added", userEntity.UserId);
            return Convertir(userEntity);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            var userEntity = Convertir(user);
            await _workUnit.UserDAL.UpdateAsync(userEntity);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("User {UserId} updated", userEntity.UserId);
            return Convertir(userEntity);
        }

        public async Task<UserDTO> DeleteUserAsync(int userId)
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
            return Convertir(user);
        }
    }
}
