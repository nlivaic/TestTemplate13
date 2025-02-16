using TestTemplate13.Core.Entities;
using TestTemplate13.Core.Interfaces;

namespace TestTemplate13.Data.Repositories
{
    public class FooRepository : Repository<Foo>, IFooRepository
    {
        public FooRepository(TestTemplate13DbContext context)
            : base(context)
        {
        }
    }
}
