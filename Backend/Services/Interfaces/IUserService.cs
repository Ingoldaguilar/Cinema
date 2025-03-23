using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserByIdAsync(int userId);
        Task<List<UserResponseDTO>> GetUsersAsync();
        Task<UserResponseDTO> AddUserAsync(UserRequestDTO user);
        Task<UserResponseDTO> UpdateUserAsync(UserResponseDTO user);
        Task<UserResponseDTO> DeleteUserAsync(int userId);
    }
}
