using API.Interfaces;
using API.Repositories;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;
        public UnitOfWork(BlogContext context)
        {
            _context = context;

        }
        public IArticleRepository Articles => new ArticleRepository(_context);
        public IUserRepository Users => new UserRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}