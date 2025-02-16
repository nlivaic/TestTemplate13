using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using TestTemplate13.Application.Sorting.Models;
using TestTemplate13.Common.Base;

namespace TestTemplate13.Data.QueryableExtensions
{
    public static class Sorting
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IEnumerable<SortCriteria> sortCriteria)
            where T : BaseEntity<Guid>
         => sortCriteria.Any()
            ? query.OrderBy(string.Join(',', sortCriteria))
            : query.OrderBy(x => x.Id);
    }
}
