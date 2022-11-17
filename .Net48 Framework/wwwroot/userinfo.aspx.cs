// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="userinfo.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class userinfo.
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class userinfo : Page
    {
        /// <summary>
        /// The s user identifier
        /// </summary>
        public static string sUserID = string.Empty;

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Searches the members.
        /// </summary>
        public void Search_Members()
        {
            ManageGridView.Visible = true;
            UserInformation.Visible = false;

            if (!string.IsNullOrWhiteSpace(Keywords.Value))
                using (var SqlDataSource1 = new SqlDataSource())
                {
                    SqlDataSource1.ID = "SqlDataSource1";
                    Page.Controls.Add(SqlDataSource1);
                    SqlDataSource1.ConnectionString = SepFunctions.Database_Connection();
                    SqlDataSource1.SelectCommand = "SELECT UserID,Username,FirstName,LastName,LastLogin,CreateDate FROM Members WHERE (Username LIKE '%" + SepFunctions.FixWord(Keywords.Value) + "%' OR City LIKE '%" + SepFunctions.FixWord(Keywords.Value) + "%' OR State LIKE '%" + SepFunctions.FixWord(Keywords.Value) + "%') AND Status=1 ORDER BY Username";
                    ManageGridView.DataSource = SqlDataSource1;
                    ManageGridView.DataBind();
                }
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ManageGridView.Columns[0].HeaderText = SepFunctions.LangText("User Name");
                    ManageGridView.Columns[1].HeaderText = SepFunctions.LangText("Full Name");
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            TranslatePage();

            GlobalVars.ModuleID = 0;

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID, "Members"), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            sUserID = SepCommon.SepCore.Request.Item("UserID");

            var str = new StringBuilder();

            var lineText = string.Empty;
            long iForums = 0;
            long iArticles = 0;
            long iAuctions = 0;
            long iBlogs = 0;
            long iBusinesses = 0;
            long iClassifieds = 0;
            long iDiscounts = 0;
            long iFiles = 0;
            long iLinks = 0;
            long iAlbums = 0;
            long iProperties = 0;
            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(sUserID))
            {
                ManageGridView.Visible = false;
                UserInformation.Visible = true;
                if (SepFunctions.Setup(69, "VideoConferenceEnable") != "Enable")
                {
                    VideoCall.Visible = false;
                }
                else
                {
                    var jConfig = SepCommon.DAL.VideoConference.VideoConfig_Get(sUserID);
                    if (jConfig.ContactOnline)
                    {
                        VideoCall.HRef = GetInstallFolder() + "video/default.aspx?DoAction=MakeCall&UserID=" + sUserID;
                    }
                    else
                    {
                        VideoCall.Visible = false;
                    }
                }
                if (SepFunctions.Setup(33, "FriendsEnable") != "Yes") AddFriend.Visible = false;

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT M.UserID,M.Username,M.Male,M.LastLogin,M.CreateDate,M.AccessClass,M.City,M.State," + SepFunctions.Upload_SQL_Select(string.Empty, 63, "FileName", "M.UserID") + " FROM Members AS M WHERE M.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                UserInformation.Visible = false;
                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("User was not found in our database.") + "</div>";
                            }
                            else
                            {
                                RS.Read();
                                sUserID = SepFunctions.openNull(RS["UserID"]);
                                using (var cmd2 = new SqlCommand("SELECT UserID FROM OnlineUsers WHERE UserID=@UserID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@UserID", sUserID);
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                            lineText = "online";
                                        else
                                            lineText = "offline";
                                    }
                                }

                                OnlineText.InnerHtml = SepFunctions.openNull(RS["UserName"]) + " is " + lineText;
                                UserName.InnerHtml = SepFunctions.openNull(RS["UserName"]);
                                ProfileImage.InnerHtml = "<img src=\"" + SepFunctions.userProfileImage(sUserID) + "\" border=\"0\" class=\"img-fluid\" />";
                                var sLocationSeperator = !string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["City"])) && !string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["State"])) ? ", " : string.Empty;

                                Location.InnerHtml = SepFunctions.openNull(RS["City"]) + sLocationSeperator + SepFunctions.openNull(RS["State"]);
                                JoinDate.InnerHtml = Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["CreateDate"])), Strings.DateNamedFormat.ShortDate);
                                LastLogin.InnerHtml = Strings.FormatDateTime(SepFunctions.toDate(SepFunctions.openNull(RS["LastLogin"])), Strings.DateNamedFormat.ShortDate);
                                using (var cmd2 = new SqlCommand("SELECT ClassName FROM AccessClasses WHERE ClassID='" + SepFunctions.FixWord(SepFunctions.openNull(RS["AccessClass"])) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows)
                                        {
                                            RS2.Read();
                                            Membership.InnerHtml = SepFunctions.openNull(RS2["ClassName"]);
                                        }
                                        else
                                        {
                                            Membership.InnerHtml = SepFunctions.LangText("N/A");
                                        }
                                    }
                                }

                                double iDaysJoined = DateAndTime.DateDiff(DateAndTime.DateInterval.Day, Convert.ToDateTime(SepFunctions.openNull(RS["CreateDate"])), DateTime.Now);
                                str.Append("<div id=\"UserInfoHeader\">");
                                str.Append("<div class=\"UserInfoLeft\">");
                                if (SepFunctions.Setup(12, "ForumsEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(FM.TopicID) AS ForumCount FROM ForumsMessages AS FM,Members AS M WHERE FM.UserID=M.UserID AND FM.UserID='" + SepFunctions.FixWord(sUserID) + "' AND FM.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty, conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iForums = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["ForumCount"])));
                                                if (iForums > 0 && iDaysJoined > 0)
                                                {
                                                    double iForumAverage = SepFunctions.toLong(Strings.FormatNumber(iForums / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "forums_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iForums + "~~ forum threads") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iForumAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(35, "ArticlesEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(ART.ArticleID) AS ArticleCount FROM Articles AS ART,Categories AS CAT WHERE ART.UserID='" + SepFunctions.FixWord(sUserID) + "' AND ART.Status=1 AND ART.PortalID=" + SepFunctions.Get_Portal_ID() + " AND ART.CatID=CAT.CatID", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iArticles = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["ArticleCount"])));
                                                if (iArticles > 0 && iDaysJoined > 0)
                                                {
                                                    double iArticleAverage = SepFunctions.toLong(Strings.FormatNumber(iArticles / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "articles_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iArticles + "~~ articles posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iArticleAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(31, "AuctionEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(AdID) AS AuctionCount FROM AuctionAds WHERE UserID='" + SepFunctions.FixWord(sUserID) + "' AND Status=1 AND PortalID=" + SepFunctions.Get_Portal_ID() + " AND AdID=LinkID", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iAuctions = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["AuctionCount"])));
                                                if (iAuctions > 0 && iDaysJoined > 0)
                                                {
                                                    double iAuctionAverage = SepFunctions.toLong(Strings.FormatNumber(iAuctions / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "auction_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iAuctions + "~~ auctions posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iAuctionAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(61, "BlogsEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(BlogID) as BlogCount FROM Blog WHERE UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iBlogs = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["BlogCount"])));
                                                if (iBlogs > 0 && iDaysJoined > 0)
                                                {
                                                    double iBlogAverage = SepFunctions.toLong(Strings.FormatNumber(iBlogs / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "blogs_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iBlogs + "~~ blogs added") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iBlogAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(20, "BusinessEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(BL.BusinessID) AS BusinessCount FROM BusinessListings AS BL,Categories AS CAT WHERE CAT.CatID=BL.CatID AND BL.PortalID=" + SepFunctions.Get_Portal_ID() + " AND BL.LinkID=BL.BusinessID AND BL.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iBusinesses = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["BusinessCount"])));
                                                if (iBusinesses > 0 && iDaysJoined > 0)
                                                {
                                                    double iBusinessAverage = SepFunctions.toLong(Strings.FormatNumber(iBusinesses / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "businesses_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iBusinesses + "~~ businesses posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iBusinessAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(44, "ClassifiedEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(CA.AdID) AS AdCount FROM ClassifiedsAds AS CA, Members AS M WHERE CA.UserID=M.UserID AND CA.EndDate > GETDATE() AND CA.SoldOut='0' AND CA.LinkID=CA.AdID AND CA.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iClassifieds = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["AdCount"])));
                                                if (iClassifieds > 0 && iDaysJoined > 0)
                                                {
                                                    double iClassifiedAverage = SepFunctions.toLong(Strings.FormatNumber(iClassifieds / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "classifieds_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iClassifieds + "~~ classifieds posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iClassifiedAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(44, "DiscountsEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(DiscountID) as DiscountCount FROM DiscountSystem WHERE UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iDiscounts = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["DiscountCount"])));
                                                if (iDiscounts > 0 && iDaysJoined > 0)
                                                {
                                                    double iDiscountAverage = SepFunctions.toLong(Strings.FormatNumber(iDiscounts / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "discounts_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iDiscounts + "~~ coupons posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iDiscountAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(10, "LibraryEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(FL.FileID) AS FileCount FROM LibrariesFiles AS FL,Categories CAT,Members AS M WHERE FL.UserID=M.UserID AND FL.CatID=CAT.CatID AND CAT.CatID IN (SELECT CatID FROM CategoriesModules WHERE ModuleID='10' AND CatID=CAT.CatID) AND FL.Status=1 AND (FL.PortalID=" + SepFunctions.Get_Portal_ID() + " AND CAT.Sharing='0' AND CAT.CatID IN (SELECT CatID FROM CategoriesPortals WHERE PortalID=" + SepFunctions.Get_Portal_ID() + " AND CatID=CAT.CatID) OR (CAT.CatID IN (SELECT CatID FROM CategoriesPortals WHERE (PortalID=" + SepFunctions.Get_Portal_ID() + " OR PortalID = -1) AND CatID=CAT.CatID) AND CAT.Sharing='1')) AND FL.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iFiles = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["FileCount"])));
                                                if (iFiles > 0 && iDaysJoined > 0)
                                                {
                                                    double iFileAverage = SepFunctions.toLong(Strings.FormatNumber(iFiles / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "downloads_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iFiles + "~~ files uploaded") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iFileAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(19, "LinksEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(Mod.LinkID) AS LinkCount FROM LinksWebsites AS Mod,Categories AS CAT WHERE CAT.CatID=Mod.CatID AND Mod.PortalID=" + SepFunctions.Get_Portal_ID() + " AND Mod.Status=1 AND UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iLinks = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["LinkCount"])));
                                                if (iLinks > 0 && iDaysJoined > 0)
                                                {
                                                    double iLinkAverage = SepFunctions.toLong(Strings.FormatNumber(iLinks / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "links_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iLinks + "~~ websites posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iLinkAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(28, "PhotosEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(AlbumID) as AlbumCount FROM PhotoAlbums WHERE UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iAlbums = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["AlbumCount"])));
                                                if (iAlbums > 0 && iDaysJoined > 0)
                                                {
                                                    double iAlbumAverage = SepFunctions.toLong(Strings.FormatNumber(iAlbums / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "photos.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iAlbums + "~~ photo albums") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iAlbumAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(32, "RStateEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT Count(RP.PropertyID) AS PropertyCount FROM RStateProperty AS RP, Members WHERE RP.UserID=Members.UserID AND RP.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                iProperties = Convert.ToInt64(SepFunctions.toDouble(SepFunctions.openNull(RS2["PropertyCount"])));
                                                if (iProperties > 0 && iDaysJoined > 0)
                                                {
                                                    double iPropertyAverage = SepFunctions.toLong(Strings.FormatNumber(iProperties / iDaysJoined, 2));
                                                    str.Append("<a href=\"" + sInstallFolder + "realestate_search.aspx?UserID=" + sUserID + "\">" + SepFunctions.LangText("~~" + iProperties + "~~ properties posted") + "</a> " + SepFunctions.LangText("(Averaged ~~" + iPropertyAverage + "~~ a day)") + "<br/>");
                                                }
                                            }
                                        }
                                    }

                                str.Append("</div>");

                                str.Append("<div class=\"UserInfoRight\">");

                                if (SepFunctions.Setup(63, "ProfilesEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT P.ProfileID,M.UserName FROM Profiles AS P,Members AS M WHERE P.UserID='" + SepFunctions.FixWord(sUserID) + "'" + Strings.ToString(SepFunctions.Setup(60, "PortalProfiles") == "Yes" ? string.Empty : " AND P.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty), conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                str.Append("<a href=\"" + sInstallFolder + "profile/" + SepFunctions.openNull(RS2["ProfileID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS2["UserName"])) + "/\">" + SepFunctions.LangText("View User Profile") + "</a><br/>");
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(18, "MatchEnable") == "Enable")
                                    using (var cmd2 = new SqlCommand("SELECT MM.ProfileID,M.UserName FROM MatchMaker AS MM,Members AS M WHERE M.PortalID=" + SepFunctions.Get_Portal_ID() + " AND MM.UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                str.Append("<a href=\"" + sInstallFolder + "match/" + SepFunctions.openNull(RS2["ProfileID"]) + "/" + SepFunctions.UrlEncode(SepFunctions.openNull(RS2["UserName"])) + "/\">" + SepFunctions.LangText("View MatchMaker Profile") + "</a><br/>");
                                            }
                                        }
                                    }

                                if (SepFunctions.Setup(17, "MessengerEnable") != "Enable")
                                {
                                    var cName = "MsgButton";
                                    var csType = GetType();

                                    var cs = Page.ClientScript;

                                    if (!cs.IsClientScriptIncludeRegistered(csType, cName)) cs.RegisterClientScriptBlock(csType, cName, "<script>$(document).ready(function () {$('#MessageButton').hide();});</script>");
                                }

                                str.Append("</div>");
                                str.Append("</div>");

                                str.Append("<div id=\"UserPostings\"></div>");
                            }
                        }
                    }
                }

                Activity.InnerHtml = Strings.ToString(str);
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Search_Members();
        }
    }
}