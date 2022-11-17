
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Data.SqlClient;

    public class MessengerController : ControllerBase
    {
        [CheckOption("username", "MessengerRead")]
        [HttpGet]
        public Models.Messages SearchMessages()
        {
            var sReturn = new Models.Messages();

            using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ID,Subject,Message,FromUserID,(SELECT UserName FROM Members WHERE UserID=FromUserID) AS FromUserName FROM Messenger WHERE ReadNew='0' AND ToUserID=@ToUserID", conn))
                {
                    cmd.Parameters.AddWithValue("@ToUserID", Server.SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            sReturn.Subject = Server.SepFunctions.openNull(RS["Subject"]);
                            sReturn.Message = Server.SepFunctions.openNull(RS["Message"]);
                            sReturn.FromUsername = Server.SepFunctions.openNull(RS["FromUserName"]);
                            sReturn.FromUserID = Server.SepFunctions.openNull(RS["FromUserID"]);
                            using (var cmd2 = new SqlCommand("UPDATE Messenger SET ReadNew=1 WHERE ID=@MessageID", conn))
                            {
                                cmd2.Parameters.AddWithValue("@MessageID", Server.SepFunctions.openNull(RS["ID"]));
                                cmd2.ExecuteNonQuery();
                            }
                        }

                    }
                }
            }

            return sReturn;
        }
    }
}