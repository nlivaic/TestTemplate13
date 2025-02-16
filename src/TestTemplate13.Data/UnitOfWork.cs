using System.Threading.Tasks;
using TestTemplate13.Common.Interfaces;

namespace TestTemplate13.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTemplate13DbContext _dbContext;

        public UnitOfWork(TestTemplate13DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}