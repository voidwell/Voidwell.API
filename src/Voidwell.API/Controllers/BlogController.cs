using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Voidwell.API.Clients;

namespace Voidwell.API.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IVoidwellClient _voidwellClient;

        public BlogController(IVoidwellClient voidwellClient)
        {
            _voidwellClient = voidwellClient;
        }

        [HttpGet]
        public Task<JToken> GetAllBlogPosts()
        {
            return _voidwellClient.GetAllBlogPosts();
        }

        [HttpGet("{blogPostId}")]
        public Task<JToken> GetBlogPosts(string blogPostId)
        {
            return _voidwellClient.GetBlogPost(blogPostId);
        }

        [HttpPost]
        public Task<JToken> PostBlogPost([FromBody]JToken content)
        {
            return _voidwellClient.CreateBlogPost(content);
        }

        [HttpPut]
        public Task<JToken> PutBlogPost([FromBody]JToken content)
        {
            return _voidwellClient.UpdateBlogPost(content);
        }

        [HttpDelete("{blogPostId}")]
        public Task DeleteBlogPost(string blogPostId)
        {
            return _voidwellClient.DeleteBlogPost(blogPostId);
        }
    }
}
