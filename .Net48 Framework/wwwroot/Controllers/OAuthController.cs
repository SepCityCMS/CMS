// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="OAuthController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.SepCore;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class OAuthController.
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class OAuthController : Controller
    {
        /// <summary>
        /// Authorizes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            var identity = ticket?.Identity;
            if (identity == null)
            {
                SepFunctions.Redirect("/account/login?scope=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("scope")) + "&client_id=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("client_id")) + Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReturnUrl")) ? "&ReturnUrl=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ReturnUrl")) : string.Empty));

                return null;
            }

            var validScopes = "|email|profile|";

            var scopes = (Request.QueryString.Get("scope") ?? string.Empty).Split(' ');
            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrWhiteSpace(Request.Form.Get("submit.Grant")))
                {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                    foreach (var scope in scopes)
                        if (Strings.InStr(validScopes, "|" + scope + "|") > 0)
                            identity.AddClaim(new Claim("urn:oauth:scope", scope));
                    authentication.SignIn(identity);
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReturnUrl")))
                    {
                        SepFunctions.Redirect(SepCommon.SepCore.Request.Item("ReturnUrl"));
                    }
                    else
                    {
                        var ctx = Request.GetOwinContext();
                        var user = ctx.Authentication.User;
                        var claims = user.Claims;

                        var points = new Dictionary<string, string>();
                        foreach (var claim_loopVariable in claims) points.Add(claim_loopVariable.Type, claim_loopVariable.Value);
                        Response.Write(JsonConvert.SerializeObject(points));
                    }

                    return null;
                }

                if (!string.IsNullOrWhiteSpace(Request.Form.Get("submit.Login")))
                {
                    authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    authentication.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                    SepFunctions.Redirect("/account/login?scope=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("scope")) + "&client_id=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("client_id")) + Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReturnUrl")) ? "&ReturnUrl=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ReturnUrl")) : string.Empty));

                    return new HttpUnauthorizedResult();
                }
            }

            // return View();
            return null;
        }
    }
}