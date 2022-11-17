using SepCommon;
using SepCommon.SepCore;
using System.Data.SqlClient;
using wwwroot.BusinessObjects;

namespace wwwroot.ViewModels
{

    public class DefaultViewModel : MasterPageViewModel
    {

        public string PageText { get; set; }

        public DefaultViewModel()
        {
            GlobalVars.ModuleID = 16;

            if (SepCommon.SepCore.Request.Item("DoAction") == "JoinNewsletter")
            {
                var NewsLetText = string.Empty;
                long aa = 0;

                if (Strings.Len(SepCommon.SepCore.Request.Item("NLEmailAddress")) < 6 || Strings.InStr(SepCommon.SepCore.Request.Item("NLEmailAddress"), "@") == 0 || Strings.InStr(SepCommon.SepCore.Request.Item("NLEmailAddress"), ".") == 0)
                    NewsLetText = SepFunctions.LangText("Either you didn't specify a correct email or someone has already joined with the email address you specified.");
                else
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT LetterID,NewsletName FROM Newsletters", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Newsletter" + SepFunctions.openNull(RS["LetterID"]))))
                                        using (var cmd2 = new SqlCommand("SELECT NUserID FROM NewslettersUsers WHERE LetterID=@LetterID AND EmailAddress=@EmailAddress", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@LetterID", SepFunctions.openNull(RS["LetterID"]));
                                            cmd2.Parameters.AddWithValue("@EmailAddress", SepCommon.SepCore.Request.Item("NLEmailAddress"));
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (!RS2.HasRows)
                                                {
                                                    NewsLetText = SepFunctions.LangText("Thank you for joining our newsletter!");
                                                    using (var cmd3 = new SqlCommand("INSERT INTO NewslettersUsers (NUserID, LetterID, UserID, EmailAddress, Status, PortalID) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.openNull(RS["LetterID"], true) + "','" + SepFunctions.Session_User_ID() + "','" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("NLEmailAddress")) + "', '1', '" + SepFunctions.Get_Portal_ID() + "')", conn))
                                                    {
                                                        cmd3.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    NewsLetText = SepFunctions.LangText("You have already joined the ~~" + SepFunctions.openNull(RS["NewsletName"]) + "~~ newsletter.");
                                                }
                                            }
                                        }

                                    aa += 1;
                                }
                            }
                        }
                    }

                if (!string.IsNullOrWhiteSpace(NewsLetText)) PageText += "<script type=\"text/javascript\" language=\"JavaScript\">alert(unescape('" + SepFunctions.EscQuotes(NewsLetText) + "'))</script>";
            }

            var cReplace = new Replace();

            PageText += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();
        }

    }
}

