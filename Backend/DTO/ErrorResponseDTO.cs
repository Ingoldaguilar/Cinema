namespace Backend.DTO
{
    public class ErrorResponseDTO
    {
        public bool Success { get; set; }
        public List<ErrorDTO> Errors { get; set; }
    }
}
