namespace Terkwaz.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Terkwaz.Domain.Blog;

    [Authorize]
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository _blogRepository;

        public BlogsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
    }
}
