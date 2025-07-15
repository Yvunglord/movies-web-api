using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movies.Application.DTOs.Common;
using Movies.Infrastructure.Data;

namespace Movies.Application.Repositories
{
    public class PaginatedRepository<T> : IPaginatedRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;

        public PaginatedRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<PagedResponse<T>> GetPaginatedAsync(PaginationQuery paginationQuery, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        { 
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(
                new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { 
                query = query.Include(includeProperty);
            }

            var totalCount = await query.CountAsync();

            if (orderBy != null)
                query = orderBy(query);
            else if (!string.IsNullOrWhiteSpace(paginationQuery.SortBy.ToString()))
                query = ApplySorting(query, paginationQuery.SortBy.ToString(), paginationQuery.SortDescending);

            var items = await query
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();

            var metadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = paginationQuery.PageSize,
                CurrentPage = paginationQuery.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)paginationQuery.PageSize)
            };

            return new PagedResponse<T>(items, metadata);
        }

        private static IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy, bool desc)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);

            var converted = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);

            return desc
                ? query.OrderByDescending(lambda)
                : query.OrderBy(lambda);
        }
    }
}