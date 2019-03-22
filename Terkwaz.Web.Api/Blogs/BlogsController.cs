namespace Terkwaz.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Pharmatolia.API.Models;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Terkwaz.Api.Models.Blog;
    using Terkwaz.Domain.Blog;
    using Terkwaz.Domain.Notification;
    using Terkwaz.Domain.User;
    using Terkwaz.Web.Api.Notifications;

    [Authorize]
    [ApiController]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository _blogRepository;
        private IUserRepository _userRepository;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public BlogsController(IBlogRepository blogRepository,
            IUserRepository userRepository,
            IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBlogByIdAsync(long id)
        {
            var blogById = await _blogRepository.GetBlogByIdAsync(id);


            if (blogById == null)
            {
                return NotFound(new ApiError(404, HttpStatusCode.NotFound.ToString(), "Blog wasn't found."));
            }

            var blogOutputModel = Mapper.Map<BlogOutputModel>(blogById);

            return Ok(blogOutputModel);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllBlogsAsync();

            var blogsOutputModel = Mapper.Map<ICollection<BlogOutputModel>>(blogs);

            return Ok(blogsOutputModel);
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

            await _hubContext.Clients.All.BroadcastNotification(new BlogNotification
            {
                BlogId = blog.Id,
                BlogTitle = blog.Title,
                AuthorName = blog.User.FullName
            });

            return Ok(blogOutputModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBlogByIdAsync(long id, [FromBody] BlogUpdateInputModel model)
        {
            var blogById = await _blogRepository.GetBlogByIdAsync(id);

            if (blogById == null)
            {
                return NotFound(new ApiError(404, HttpStatusCode.NotFound.ToString(), "Blog wasn't found."));
            }

            blogById.Update(model.Title, model.Subtitle, model.PhotoUrl, model.Body);

            await _blogRepository.UpdateBlogAsync(blogById);

            var blogOutputModel = Mapper.Map<BlogOutputModel>(blogById);

            return Ok(blogOutputModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBlogByIdAsync(long id)
        {
            var blogById = await _blogRepository.GetBlogByIdAsync(id);

            if (blogById == null)
            {
                return NotFound(new ApiError(404, HttpStatusCode.NotFound.ToString(), "Blog wasn't found."));
            }

            await _blogRepository.DeleteBlogByIdAsync(blogById);

            return Ok();
        }
    }
}

