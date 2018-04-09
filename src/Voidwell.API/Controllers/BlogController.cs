using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IInternalClient _internalClient;

        public BlogController(IInternalClient internalClient)
        {
            _internalClient = internalClient;
        }

        [HttpGet]
        public Task<JToken> GetAllBlogPosts()
        {
            return _internalClient.GetAllBlogPosts();
        }

        [HttpGet("{blogPostId}")]
        public Task<JToken> GetBlogPosts(string blogPostId)
        {
            return _internalClient.GetBlogPost(blogPostId);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public Task<JToken> PostBlogPost([FromBody]JToken content)
        {
            return _internalClient.CreateBlogPost(content);
        }

        [HttpPut("{blogPostId}")]
        [Authorize(Roles = "Administrator")]
        public Task<JToken> PutBlogPost(string blogPostId, [FromBody]JToken content)
        {
            return _internalClient.UpdateBlogPost(blogPostId, content);
        }

        [HttpDelete("{blogPostId}")]
        [Authorize(Roles = "Administrator")]
        public Task DeleteBlogPost(string blogPostId)
        {
            return _internalClient.DeleteBlogPost(blogPostId);
        }
    }
}
