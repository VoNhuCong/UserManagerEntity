using Microsoft.EntityFrameworkCore;
using UserManagerEntity.Models;
using UserManagerEntity.Models.Entities;

namespace UserManagerEntity.Repository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        public Task<Blog> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly DataContext _context;

        public BlogRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Blog?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blog = await _context.Blogs
                                       .FirstOrDefaultAsync(d => d.BlogID == id, cancellationToken: cancellationToken);
            return blog;
        }
    }
}
