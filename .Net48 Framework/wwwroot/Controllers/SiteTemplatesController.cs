// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="SiteTemplatesController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon.Models;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class SiteTemplatesController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SiteTemplatesController : ApiController
    {
        /// <summary>
        /// Applies the site template.
        /// </summary>
        /// <param name="Template">The template.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [Route("api/sitetemplates/apply")]
        [HttpPost]
        public ResponseHelper.CreateResponse ApplySiteTemplate([FromBody] Templates Template)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminSiteLooks");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                SepCommon.DAL.SiteTemplates.Template_Mark_Active(Template.TemplateID, Template.PortalID);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = Template.TemplateID
                };
                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }

        /// <summary>
        /// Applies the site template.
        /// </summary>
        /// <param name="Template">The template.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [Route("api/sitetemplates/save")]
        [HttpPost]
        public ResponseHelper.CreateResponse SaveSiteTemplate([FromBody] Templates Template)
        {
            var SEP = RequestHelper.AuthorizeRequest("AdminSiteLooks");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                SepCommon.DAL.SiteTemplates.Template_Upload(Template.TemplateID, SepCommon.SepFunctions.HTMLDecode(Template.HTML), Template.CSS, Template.PortalID, Template.Timestamp);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = Template.TemplateID
                };
                return cResponse;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}