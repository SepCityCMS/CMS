// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="AccessTokenController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon.DAL;
    using System;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class AccessTokenController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AccessTokenController : ApiController
    {
        /// <summary>
        /// Gets the specified cred.
        /// </summary>
        /// <param name="cred">The cred.</param>
        /// <returns>SepCommon.DAL.AccessTokenInfo.</returns>
        [HttpGet]
        public AccessTokenInfo Get([FromUri] Login cred)
        {
            try
            {
                var sToken = SepCommon.DAL.Members.Login(cred.Username, cred.Password, string.Empty, string.Empty, string.Empty, 0, false, string.Empty);
                if (!string.IsNullOrWhiteSpace(sToken))
                {
                    var token = new SepCommon.DAL.AccessTokenInfo
                    {
                        AccessToken = sToken
                    };
                    return token;
                }

                throw RequestHelper.UnAuthorized("Invalid username and/or password.");
            }
            catch (Exception)
            {
                throw RequestHelper.UnAuthorized("Invalid username and/or password.");
            }
        }
    }
}