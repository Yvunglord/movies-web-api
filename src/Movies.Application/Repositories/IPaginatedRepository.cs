using System.Linq.Expressions;
using Movies.Application.DTOs.Common;

namespace Movies.Application.Repositories
{
    public interface IPaginatedRepository<T>
    {
        Task<PagedResponse<T>> GetPaginatedAsync(
            PaginationQuery paginationQuery,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
    }
}