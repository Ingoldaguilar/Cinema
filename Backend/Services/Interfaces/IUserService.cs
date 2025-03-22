using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> AddUserAsync(UserDTO user);
        Task<UserDTO> UpdateUserAsync(UserDTO user);
        Task<UserDTO> DeleteUserAsync(int userId);
    }
}
