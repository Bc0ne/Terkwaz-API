namespace Terkwaz.Web.Api.Blogs
{
    using AutoMapper;
    using Terkwaz.Api.Models.Blog;
    using Terkwaz.Domain.Blog;

    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Blog, BlogOutputModel>();
        }
    }
}
