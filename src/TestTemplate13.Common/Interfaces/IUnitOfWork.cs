using System.Threading.Tasks;

namespace TestTemplate13.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}