namespace Movies.Application.DTOs.Common
{
    public class PaginationQuery
    {
        private const int MAX_PAGE_SIZE = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public string SortBy { get; set; } = string.Empty;
        public bool SortDescending { get; set; }
    }
}