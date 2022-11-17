// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="MessengerController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using System.Data.SqlClient;
    using System.Security;
    using System.Web.Http;
    using wwwroot.ApiTypes;

    /// <summary>
    /// Class MessengerController.
    /// Implements the <see cref="System.Web.Http.ApiController" />
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class MessengerController : ApiController
    {
        /// <summary>
        /// Searches the messages.
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpGet]
        public SepCommon.Models.Messages SearchMessages()
        {
            var SEP = RequestHelper.AuthorizeRequest("MessengerRead");
            if (SEP == false)
                throw RequestHelper.UnAuthorized("SessionId is invalid");
            try
            {
                var sReturn = new SepCommon.Models.Messages();

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ID,Subject,Message,FromUserID,(SELECT UserName FROM Members WHERE UserID=FromUserID) AS FromUserName FROM Messenger WHERE ReadNew='0' AND ToUserID=@ToUserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ToUserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                sReturn.Subject = SepFunctions.openNull(RS["Subject"]);
                                sReturn.Message = SepFunctions.openNull(RS["Message"]);
                                sReturn.FromUsername = SepFunctions.openNull(RS["FromUserName"]);
                                sReturn.FromUserID = SepFunctions.openNull(RS["FromUserID"]);
                                using (var cmd2 = new SqlCommand("UPDATE Messenger SET ReadNew=1 WHERE ID=@MessageID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@MessageID", SepFunctions.openNull(RS["ID"]));
                                    cmd2.ExecuteNonQuery();
                                }
                            }

                        }
                    }
                }

                return sReturn;
            }
            catch (SecurityException ex)
            {
                throw RequestHelper.Forbidden(ex.Message);
            }
        }
    }
}