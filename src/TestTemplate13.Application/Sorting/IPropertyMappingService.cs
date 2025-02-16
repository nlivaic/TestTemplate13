using System.Collections.Generic;
using TestTemplate13.Application.Sorting.Models;

namespace TestTemplate13.Application.Sorting
{
    public interface IPropertyMappingService
    {
        IEnumerable<SortCriteria> Resolve(BaseSortable sortableSource, BaseSortable sortableTarget);
    }
}
