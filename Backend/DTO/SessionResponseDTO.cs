namespace Backend.DTO
{
    public class SessionResponseDTO
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
