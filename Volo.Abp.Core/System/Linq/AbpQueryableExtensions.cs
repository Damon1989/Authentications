using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace System.Linq
{
    public static class AbpQueryableExtensions
    {
        public static IQueryable<T> PageBy<T>([NotNull] this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            Check.NotNull(query, nameof(query));
            return query.Skip(skipCount).Take(maxResultCount);
        }

        public static TQueryable PageBy<T, TQueryable>([NotNull] this TQueryable query, int skipCount,
            int maxResultCount) where TQueryable:IQueryable<T>
        {
            Check.NotNull(query, nameof(query));

            return (TQueryable) query.Skip(skipCount).Take(maxResultCount);
        }

        public static IQueryable<T> WhereIf<T>([NotNull] this IQueryable<T> query, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            Check.NotNull(query, nameof(query));

            return condition
                ? query.Where(predicate)
                : query;
        }

        public static TQueryable WhereIf<T, TQueryable>([NotNull] this TQueryable query, bool condition,
            Expression<Func<T, bool>> predicate)
            where TQueryable : IQueryable<T>
        {
            Check.NotNull(query, nameof(query));

            return condition
                ? (TQueryable) query.Where(predicate)
                : query;
        }

        public static IQueryable<T> WhereIf<T>([NotNull] this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            Check.NotNull(query, nameof(query));

            return condition
                ? query.Where(predicate)
                : query;
        }

        public static TQueryable WhereIf<T, TQueryable>([NotNull] this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate)
            where TQueryable : IQueryable<T>
        {
            Check.NotNull(query, nameof(query));

            return condition
                ? (TQueryable)query.Where(predicate)
                : query;
        }
    }
}
