using System.Collections.Generic;
using TestTemplate13.Application.Sorting.Models;

namespace TestTemplate13.Application.Tests.Helpers
{
    public class TargetParameters2
        : BaseSortable<MappingTargetModel2>
    {
        public override IEnumerable<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
    }
}
