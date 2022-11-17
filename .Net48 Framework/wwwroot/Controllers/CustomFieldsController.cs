// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="CustomFieldsController.cs" company="SepCity, Inc.">
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
    /// Class CustomFieldsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CustomFieldsController : ApiController
    {
        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="Answer">The answer.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [Route("api/customfields/answers")]
        [HttpPost]
        public ResponseHelper.CreateResponse GetMessages([FromBody] CustomFieldsAnswers Answer)
        {
            var SEP = RequestHelper.AuthorizeRequest("|1|");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                SepCommon.DAL.CustomFields.Answers_Save(Answer.UserID, Answer.ModuleID, Answer.UniqueID, Answer.ProductID, Answer.CustomData);
                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = Answer.UniqueID
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