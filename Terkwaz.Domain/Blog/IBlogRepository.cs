namespace Terkwaz.Domain.Blog
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IBlogRepository
    {
        Task AddBlogAsync(Blog blog);

        Task<ICollection<Blog>> GetAllBlogs();
    }
}
