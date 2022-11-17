// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="DownloadsController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.Models;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class DownloadsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DownloadsController : ApiController
    {
        /// <summary>
        /// Posts the downloads.
        /// </summary>
        /// <param name="Download">The download.</param>
        /// <returns>ResponseHelper.CreateResponse.</returns>
        [HttpPost]
        public ResponseHelper.CreateResponse PostDownloads(Downloads Download)
        {
            var SEP = RequestHelper.AuthorizeRequest("LibraryUpload");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var FileID = SepFunctions.GetIdentity();
                if (Download.FileID != 0) FileID = Download.FileID;

                SepCommon.DAL.Downloads.Download_Save(FileID, SepFunctions.Session_User_ID(), Download.CatID, Download.Field1, Download.Field2, Download.Field3, Download.Field4, Download.eDownload, true, Download.FileName, Download.PortalID);

                var cResponse = new ResponseHelper.CreateResponse
                {
                    Id = FileID
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