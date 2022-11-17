
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.Data.SqlClient;

    public class ClassifiedsController : ControllerBase
    {
        [CheckOption("username", "ClassifiedAccess")]
        [Route("api/classifieds/feedback")]
        [HttpPost]
        public Models.ClassifiedAds SearchFeedback([FromBody] Models.ClassifiedAdsFeedback Feedback)
        {
            var returnXML = new Models.ClassifiedAds();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Rating,Message FROM ClassifiedsFeedback WHERE FeedbackID=@FeedbackID AND AdID=@AdID", conn))
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
    }
}