
namespace Asp.netCore_FinSharkProjAPI.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false; // Default to ascending order
        public int PageNumber { get; set; } = 1; // Default to first page
        public int PageSize { get; set; } = 20; // Default to 20 items per page
    }
}
