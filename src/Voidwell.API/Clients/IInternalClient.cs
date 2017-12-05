using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Voidwell.API.Clients
{
    public interface IInternalClient
    {
        Task<JToken> GetAllBlogPosts();
        Task<JToken> GetBlogPost(string blogPostId);
        Task<JToken> CreateBlogPost(JToken content);
        Task<JToken> UpdateBlogPost(JToken content);
        Task DeleteBlogPost(string blogPostId);
    }
}
