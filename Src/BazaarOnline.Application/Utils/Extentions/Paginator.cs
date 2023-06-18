using BazaarOnline.Application.DTOs.PaginationDTO;

namespace BazaarOnline.Application.Utils.Extentions
{
    public static class Paginator
    {
        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query,
            PaginationFilterDTO pagination)
        {
            return query.Skip(pagination.Offset).Take(pagination.Take);
        }

        public static IEnumerable<TEntity> Paginate<TEntity>(this IEnumerable<TEntity> query,
            PaginationFilterDTO pagination)
        {
            return query.Skip(pagination.Offset).Take(pagination.Take);
        }
    }
}