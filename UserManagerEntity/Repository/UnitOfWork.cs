using static UserManagerEntity.Repository.UnitOfWork;
using UserManagerEntity.Models;

namespace UserManagerEntity.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository Blogs { get; }
        Task<int> CompleteAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Blogs = new BlogRepository(_context);
        }
        public IBlogRepository Blogs { get; private set; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
