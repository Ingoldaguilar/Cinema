namespace Backend.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<ErrorDTO>? Errors { get; set; }

        public static ApiResponse<T> Ok(T data) => new() { Success = true, Data = data };
        public static ApiResponse<T> Fail(List<ErrorDTO> errors) => new() { Success = false, Errors = errors };
    }
}
