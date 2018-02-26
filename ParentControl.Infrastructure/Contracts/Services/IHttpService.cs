using ParentControl.DTO;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IHttpService
    {
        Authorization Authenticate(string username, string password);

        string GetRequest(string url, params RequestParameter[] parameters);

        Response PostRequest(string url, params RequestParameter[] parameters);

        Response PostRequest(string url, object body);

        Response PutRequest(string s, object body);

        bool IsConnected { get; }

        string Token { get; }

        
    }
}
