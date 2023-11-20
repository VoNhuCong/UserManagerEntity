using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagerEntity.Contract.Requests;
using UserManagerEntity.Models.Entities;
using UserManagerEntity.Repository;
using UserManagerEntity.Services;

namespace UserManagerEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _IUnitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _IUnitOfWork = unitOfWork;
        }

        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlog(BlogRequest blog)
        {
            var newBlog = new Blog()
            {
                BlogID = Guid.NewGuid(),
                BlogName = blog.BlogName,
                Id = blog.Id,
                BlogDescription = blog.BlogDescription
            };
            _IUnitOfWork.Blogs.Add(newBlog);

            await _IUnitOfWork.CompleteAsync();
            
            return Ok();
        }

        [HttpGet("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(Guid Id)
        {
            var blog = await _IUnitOfWork.Blogs.GetAsync(Id.ToString());
            blog.BlogName = "Blog đầu tiên";
            await _IUnitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet("GetBlog")]
        public async Task<IActionResult> GetBlog(Guid Id)
        {
            var blog = await _IUnitOfWork.Blogs.GetAsync(Id);
            return Ok(blog);
        }

        [HttpGet("RemoveBlog")]
        public async Task<IActionResult> RemoveBlog(Guid Id)
        {
            Blog blog = await _IUnitOfWork.Blogs.GetAsync(Id);
            _IUnitOfWork.Blogs.Remove(blog);
            await _IUnitOfWork.CompleteAsync(); 
            return Ok(blog);
        }
    }
}
