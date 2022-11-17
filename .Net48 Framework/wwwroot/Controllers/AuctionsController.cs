// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-20-2019
// ***********************************************************************
// <copyright file="AuctionsController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using SepCommon.Models;
    using System.Data.SqlClient;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class AuctionsController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AuctionsController : ApiController
    {
        /// <summary>
        /// Searches the feedback.
        /// </summary>
        /// <param name="Feedback">The feedback.</param>
        /// <returns>SepCommon.Models.AuctionAds.</returns>
        [Route("api/auctions/feedback")]
        [HttpPost]
        public AuctionAds SearchFeedback([FromBody] AuctionAdsFeedback Feedback)
        {
            var SEP = RequestHelper.AuthorizeRequest("AuctionAccess");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var returnXML = new SepCommon.Models.AuctionAds();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Rating,Message FROM AuctionFeedback WHERE FeedbackID=@FeedbackID AND AdID=@AdID", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", Feedback.AdID);
                        cmd.Parameters.AddWithValue("@FeedbackID", Feedback.FeedbackID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                returnXML.Rating = SepFunctions.toInt(SepFunctions.openNull(RS["Rating"]));
                                returnXML.Description = SepFunctions.openNull(RS["Message"]);
                            }

                        }
                    }
                }

                return returnXML;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}