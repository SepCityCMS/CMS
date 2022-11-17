
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.Data.SqlClient;

    public class AuctionsController : ControllerBase
    {
        [CheckOption("username", "AuctionAccess")]
        [Route("api/auctions/feedback")]
        [HttpPost]
        public Models.AuctionAds SearchFeedback([FromBody] Models.AuctionAdsFeedback Feedback)
        {
            var returnXML = new Models.AuctionAds();

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
    }
}