namespace Terkwaz.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Terkwaz.Data.Context;
    using Terkwaz.Domain.Blog;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(x => x.User).OrderByDescending(x => x.CreationDate).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(long id)
        {
            return await _context.Blogs.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteBlogByIdAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }
    }
}
