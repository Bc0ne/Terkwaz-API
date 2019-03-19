namespace Terkwaz.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pharmatolia.API.Models;
    using System.Net;
    using System.Threading.Tasks;
    using Terkwaz.Api.Models.Blog;
    using Terkwaz.Domain.Blog;
    using Terkwaz.Domain.User;

    [Authorize]
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository _blogRepository;
        private IUserRepository _userRepository;

        public BlogsController(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync()
        {
            var blogs = _blogRepository.GetAllBlogs();


        }

        [HttpPost]
        public async Task<IActionResult> AddBlogAsync([FromBody] BlogInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Data isn't valid."));
            }

            if (!long.TryParse(this.User.Identity.Name, out long userId))
            {
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "UserId isn't valid."));
            }

            var userById = await _userRepository.GetUserByIdAsync(userId);

            if (userById == null)
            {
                return NotFound(new ApiError(404, HttpStatusCode.NotFound.ToString(), "User wasn't found."));
            }

            var blog = Blog.New(userById, model.Title, model.Subtitle, model.ImageUrl, model.Body);

            await _blogRepository.AddBlogAsync(blog);

            var blogOutputModel = Mapper.Map<BlogOutputModel>(blog);

            return Ok(blogOutputModel);
        }
    }
}
