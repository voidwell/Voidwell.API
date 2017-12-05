using System.Threading.Tasks;

namespace Voidwell.API
{
    public interface ITokenRetriever
    {
        Task<string> GetRequestToken();
    }
}