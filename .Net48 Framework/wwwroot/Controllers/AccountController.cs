// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="AccountController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Microsoft.AspNet.Identity;
    using SepCommon;
    using SepCommon.SepCore;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class AccountController.
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AccountController : Controller
    {
        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            if (Request.HttpMethod == "POST")
                if (!string.IsNullOrWhiteSpace(Request.Form.Get("submit.Signin")))
                {
                    var sResponse = SepCommon.DAL.Members.Login(Request.Form["username"], Request.Form["password"], string.Empty, string.Empty, string.Empty, 0, false, string.Empty);
                    if (Strings.Left(sResponse, 7) == "USERID:")
                    {
                        sResponse = Strings.Replace(sResponse, "USERID:", string.Empty);
                        var userid = Strings.Split(sResponse, "||")[0];
                        var Username = Strings.Split(sResponse, "||")[1];
                        var Password = Strings.Split(sResponse, "||")[2];
                        var AccessKeys = Strings.Split(sResponse, "||")[3];
                        SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", userid);
                        SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", Username);
                        SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", Password);
                        SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", AccessKeys);

                        var claims = new List<Claim>
                        {
                            new Claim("userid", userid),
                            new Claim("username", Username),
                            new Claim("email", SepFunctions.GetUserInformation("EmailAddress", userid)),
                            new Claim("firstname", SepFunctions.GetUserInformation("FirstName", userid)),
                            new Claim("lastname", SepFunctions.GetUserInformation("LastName", userid))
                        };
                        var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignOut();
                        authenticationManager.SignIn(id);

                        if (authenticationManager.User.Identity.IsAuthenticated)
                        {
                            SepFunctions.Redirect("/oauth/authorize?scope=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("scope")) + "&client_id=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("client_id")) + Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("ReturnUrl")) ? "&ReturnUrl=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ReturnUrl")) : string.Empty));

                            return null;
                        }
                    }
                }

            return View();
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();

            return View();
        }
    }
}