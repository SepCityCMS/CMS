// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Startup.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

using DotVVM.Framework.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Owin;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

[assembly: OwinStartup(typeof(wwwroot.Startup))]
namespace wwwroot
{

    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The authentication codes
        /// </summary>
        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath);

            dotvvmConfiguration.AssertConfigurationIsValid();
            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });

            app.Use((context, next) =>
            {
                var httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
                return next();
            });
            app.UseStageMarker(PipelineStage.MapHandler);
        }

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            Paths.LoginPath = "/Account/Login";
            Paths.LogoutPath = "/Account/Logout";

            // Enable Sign In Cookie
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });

            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = CookieAuthenticationDefaults.AuthenticationType, AuthenticationMode = AuthenticationMode.Passive, CookieName = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationType, ExpireTimeSpan = TimeSpan.FromMinutes(5) });

            // Enable google authentication
            app.UseGoogleAuthentication();

            // Setup Authorization Server

            // Refresh token provider which creates and receives referesh token
            app.UseOAuthAuthorizationServer(
                new OAuthAuthorizationServerOptions
                {
                    AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                    TokenEndpointPath = new PathString(Paths.TokenPath),
                    ApplicationCanDisplayErrors = true,
                    AllowInsecureHttp = true,
                    Provider = new OAuthAuthorizationServerProvider { OnValidateClientRedirectUri = ValidateClientRedirectUri, OnValidateClientAuthentication = ValidateClientAuthentication, OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials, OnGrantClientCredentials = GrantClientCredetails },
                    AuthorizationCodeProvider = new AuthenticationTokenProvider { OnCreate = CreateAuthenticationCode, OnReceive = ReceiveAuthenticationCode },
                    RefreshTokenProvider = new AuthenticationTokenProvider { OnCreate = CreateRefreshToken, OnReceive = ReceiveRefreshToken }
                });
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvcCore();

            services.AddLogging();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "SepCity API", Version = "v1" }); });
        }

        /// <summary>
        /// Creates the authentication code.
        /// </summary>
        /// <param name="context">The context.</param>
        private void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        /// <summary>
        /// Creates the refresh token.
        /// </summary>
        /// <param name="context">The context.</param>
        private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        /// <summary>
        /// Grants the client credetails.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

            context.Validated(identity);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Grants the resource owner credentials.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

            context.Validated(identity);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Receives the authentication code.
        /// </summary>
        /// <param name="context">The context.</param>
        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            if (_authenticationCodes.TryRemove(context.Token, out string value)) context.DeserializeTicket(value);
        }

        /// <summary>
        /// Receives the refresh token.
        /// </summary>
        /// <param name="context">The context.</param>
        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }

        /// <summary>
        /// Validates the client authentication.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.TryGetBasicCredentials(out string clientId, out string clientSecret) || context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                if (clientId == Clients.Client1.Id && clientSecret == Clients.Client1.Secret)
                    context.Validated();
                else if (clientId == Clients.Client2.Id && clientSecret == Clients.Client2.Secret) context.Validated();
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Validates the client redirect URI.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == Clients.Client1.Id)
                context.Validated(Clients.Client1.RedirectUrl);
            else if (context.ClientId == Clients.Client2.Id) context.Validated(Clients.Client2.RedirectUrl);
            return Task.FromResult(0);
        }
    }
}