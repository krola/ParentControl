using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Infrastructure.Owin.Model;

namespace ParentControl.Infrastructure.Contracts
{
    public interface IHttpService
    {
        LoginTokenResult Authenticate(string username, string password);

        string GetRequest(string url, params RequestParameter[] parameters);

        Response PostRequest(string url, params RequestParameter[] parameters);

        Response PostRequest(string url, object body);

        bool IsConnected { get; }

        string Token { get; }
    }
}
