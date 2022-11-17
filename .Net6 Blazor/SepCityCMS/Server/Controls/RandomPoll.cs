// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RandomPoll.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class RandomPoll.
    /// </summary>
    public class RandomPoll
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(25, "PollsEnable") != "Enable")
            {
                return output.ToString();
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PollsAccess"), true) == false)
            {
                return output.ToString();
            }

            var PollID = string.Empty;
            var Total = 0;
            long RecordCounter = 0;
            var rndNumber = 0;

            var PollIds = new Hashtable();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT PollID FROM PNQQuestions WHERE  CONVERT(varchar, StartDate,126) <= CONVERT(varchar, GetDate(),126) AND  CONVERT(varchar, EndDate,126) >= CONVERT(varchar, GetDate(),126) AND PollID IN (SELECT UniqueID FROM Associations WHERE ModuleID='25' AND (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalID = -1) AND UniqueID=PollID)", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                PollIds.Add("Poll" + RecordCounter, SepFunctions.openNull(RS["PollID"]));
                                RecordCounter += 1;
                            }

                            if (RecordCounter > 0)
                            {
                                if (RecordCounter == 1)
                                {
                                    rndNumber = 0;
                                }
                                else
                                {
                                    var random = new Random();
                                    rndNumber = random.Next(0, Convert.ToInt32(RecordCounter));
                                    if (rndNumber == -1)
                                    {
                                        rndNumber = 0;
                                    }
                                }

                                if (PollIds.ContainsKey("Poll" + rndNumber))
                                {
                                    PollID = PollIds["Poll" + rndNumber].ToString();
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(PollID))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Question FROM PNQQuestions WHERE StartDate <= CONVERT(char(10), GetDate(),126) AND EndDate >= CONVERT(char(10), GetDate(),126) AND PollID IN (SELECT UniqueID FROM Associations WHERE ModuleID='25' AND (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalID = -1) AND UniqueID=PollID) AND PollID='" + SepFunctions.FixWord(PollID) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == false)
                            {
                                output.AppendLine("<div class=\"col-lg-12\">" + SepFunctions.LangText("There are no polls to display at this time.") + "</div>");
                            }
                            else
                            {
                                RS.Read();
                                using (SqlCommand cmd2 = new SqlCommand("SELECT OptionID,PollOption,SelectedCount FROM PNQOptions WHERE PollID='" + SepFunctions.FixWord(PollID) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows == false)
                                        {
                                            output.AppendLine("<div class=\"col-lg-12\">" + SepFunctions.LangText("There are no polls to display at this time.") + "</div>");
                                        }
                                        else
                                        {
                                            output.Append(SepFunctions.openNull(RS["Question"]) + "<br/>");
                                            output.AppendLine("<div id=\"PollOptionsRandom\">");
                                            while (RS2.Read())
                                            {
                                                Total += SepFunctions.toInt(SepFunctions.openNull(RS2["SelectedCount"]));
                                                output.Append("<div class=\"col-lg-12\">" + SepFunctions.openNull(RS2["PollOption"]));
                                                if (SepFunctions.CompareKeys(SepFunctions.Security("PollsVote")))
                                                {
                                                    output.AppendLine(" : <a href=\"javascript:void(0)\" onclick=\"castVote('" + PollID + "', '" + SepFunctions.openNull(RS2["OptionID"]) + "', '" + SepFunctions.Get_Portal_ID() + "', 'Random');return false;\">" + SepFunctions.LangText("Vote") + "</a><br/>");
                                                }
                                                else
                                                {
                                                    SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "poll/" + PollID + "/" + SepFunctions.openNull(RS["Question"]) + "/");
                                                    output.AppendLine(" : <a href=\"" + sInstallFolder + "login.aspx\">" + SepFunctions.LangText("Vote") + "</a><br/>");
                                                }

                                                output.Append("</div>");
                                            }

                                            output.AppendLine("<div class=\"col-lg-12\">" + SepFunctions.LangText("Total Votes") + " " + Total + "</div>");
                                            output.AppendLine("</div>");
                                            output.AppendLine("<div id=\"PollResultsRandom\" class=\"ui-widget ui-widget-content ui-corner-all\" style=\"width:200px; height: 300px; display:none;\"></div>");
                                            output.AppendLine("<div class=\"col-lg-12\"><a href=\"" + sInstallFolder + "polls.aspx\">" + SepFunctions.LangText("View All Polls") + "</a></div>");
                                            output.AppendLine("<div class=\"col-lg-12\"><a href=\"javascript:showResults('Random', '" + PollID + "');\" id=\"resultsLinkRandom\">" + SepFunctions.LangText("View Results") + "</a></div>");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    output.AppendLine("<div class=\"col-lg-12\">" + SepFunctions.LangText("There are no polls to display at this time.") + "</div>");
                }
            }

            return output.ToString();
        }
    }
}