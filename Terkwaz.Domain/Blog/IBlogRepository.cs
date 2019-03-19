namespace Terkwaz.Domain.Blog
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IBlogRepository
    {
        Task AddBlogAsync(Blog blog);

        Task<ICollection<Blog>> GetAllBlogsAsync();

        Task<Blog> GetBlogByIdAsync(long id);

        Task UpdateBlogAsync(Blog blogById);

        Task DeleteBlogByIdAsync(Blog blog);
    }
}
