namespace Terkwaz.Web.Api.Blogs
{
    using AutoMapper;
    using System.Collections.Generic;
    using Terkwaz.Api.Models.Blog;
    using Terkwaz.Domain.Blog;

    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Blog, BlogOutputModel>()
                .ForPath(dest => dest.Author.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.Author.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForPath(dest => dest.Author.PhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));

            //CreateMap<ICollection<Blog>, ICollection<BlogOutputModel>>();
        }
    }
}
