// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="WebApiConfig.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Class WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Gets the URL prefix.
        /// </summary>
        /// <value>The URL prefix.</value>
        public static string UrlPrefix => "api";

        /// <summary>
        /// Gets the URL prefix relative.
        /// </summary>
        /// <value>The URL prefix relative.</value>
        public static string UrlPrefixRelative => "~/api";

        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="routes">The routes.</param>
        public static void Register(HttpConfiguration config, RouteCollection routes)
        {
            config.MapHttpAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            config.Routes.MapHttpRoute("DefaultApi", UrlPrefix + "/{controller}/{id}", new { id = RouteParameter.Optional });

            routes.MapRoute(name: "TwilioToken", url: "twiliotoken", defaults: new { controller = "TwilioToken", action = "Index" });

            routes.MapRoute(name: "TwilioVoice", url: "TwilioVoice", defaults: new { controller = "TwilioVoice", action = "Index" });
        }
    }
}