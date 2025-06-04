namespace kAI_webAPI.Helpers
{
    public class QueryObject
    {
        public string? Username { get; set; } = null;
        public string? Fullname { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
