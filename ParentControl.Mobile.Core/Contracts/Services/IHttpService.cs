using ParentControl.Core.Services.Model;
using System.Threading.Tasks;

namespace ParentControl.Core.Contracts.Services
{
    public interface IHttpService
    {
        Task<string> GetRequestAsync(string url, params RequestParameter[] parameters);

        Task<Response> PostRequestAsync(string url, params RequestParameter[] parameters);

        Task<Response> PostRequestAsync(string url, object body);

        bool IsAuthenticated { get; }

        AuthorizationResult AuthenticationData { get; set; }
    }
}
