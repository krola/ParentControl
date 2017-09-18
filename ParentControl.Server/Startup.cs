using Microsoft.Owin;
using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using ParentControl.Server;
using ParentControl.Server.App_Start;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Providers;

[assembly: OwinStartup(typeof(OwinAuthentication.Startup))]
namespace OwinAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = NinjectWebCommon.CreateKernel();

            ConfigureOAuth(app, kernel);
            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            app.UseNinjectMiddleware(() => kernel);
            app.UseNinjectWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app, IKernel kernel)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider(kernel.Get<IUserRepository>())
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}