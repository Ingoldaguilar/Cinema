namespace Backend.DTO
{
    public class UserRequestDTO
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
