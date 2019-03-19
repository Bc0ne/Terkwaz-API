namespace Terkwaz.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Terkwaz.Data.Context;
    using Terkwaz.Domain.Blog;
    using System.Linq;

    public class BlogRepository : IBlogRepository
    {
        private readonly TerkwazDbContext _context;

        public BlogRepository(TerkwazDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("DbContext is null");
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Blog>> GetAllBlogsAsync() => await _context.Blogs.Include(x => x.User).ToListAsync();
    }
}
