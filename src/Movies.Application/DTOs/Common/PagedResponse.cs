namespace Movies.Application.DTOs.Common
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public PaginationMetadata Pagination { get; set; }
        public Dictionary<string, string> Links { get; set; } = [];

        public PagedResponse(IEnumerable<T> data, PaginationMetadata pagination)
        {
            Data = data;
            Pagination = pagination;
        }
    }
}