namespace kAI_webAPI.Models.User
{
    public class ApiResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; } = null; // token, user, etc.
    }
}
