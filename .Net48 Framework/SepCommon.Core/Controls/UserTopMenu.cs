// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="UserTopMenu.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using CuteChat;
    using SepCommon.Core;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using Strings = SepCommon.Core.SepCore.Strings;

    /// <summary>
    /// Class UserTopMenu.
    /// </summary>
    public class UserTopMenu
    {
        /// <summary>
        /// The c common
        /// </summary>
        private string GetAccessKeys = string.Empty;

        /// <summary>
        /// The get admin page name
        /// </summary>
        private string GetAdminPageName = string.Empty;

        /// <summary>
        /// The get enable
        /// </summary>
        private string GetEnable = string.Empty;

        /// <summary>
        /// The get manage keys
        /// </summary>
        private string GetManageKeys = string.Empty;

        /// <summary>
        /// The get page name
        /// </summary>
        private string GetPageName = string.Empty;

        /// <summary>
        /// The get write hide
        /// </summary>
        private bool GetWriteHide;

        /// <summary>
        /// The get write keys
        /// </summary>
        private string GetWriteKeys = string.Empty;

        /// <summary>
        /// Loads the module information.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        public void Load_Module_Info(int iModuleID)
        {
            switch (iModuleID)
            {
                case 2:
                    GetAccessKeys = SepFunctions.Security("AdsAccess");
                    GetManageKeys = SepFunctions.Security("AdsAdmin");
                    GetEnable = "AdsEnable";
                    GetPageName = "advertising.aspx";
                    GetAdminPageName = "banners.aspx";
                    break;

                case 5:
                    GetAccessKeys = SepFunctions.Security("DiscountsAccess");
                    GetWriteKeys = SepFunctions.Security("DiscountsPost");
                    GetManageKeys = SepFunctions.Security("DiscountsAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "DiscountsEnable";
                    GetPageName = "discounts.aspx";
                    GetAdminPageName = "discounts.aspx";
                    break;

                case 7:
                    GetAccessKeys = SepFunctions.Security("UPagesAccess");
                    GetWriteKeys = SepFunctions.Security("UPagesCreate");
                    GetManageKeys = SepFunctions.Security("UPagesManage");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "UPagesEnable";
                    if (SepFunctions.isUserPage() == false)
                    {
                        GetPageName = "userpages.aspx";
                    }
                    else
                    {
                        GetPageName = "default.aspx";
                    }

                    GetAdminPageName = "userpages.aspx";
                    break;

                case 9:
                    GetAccessKeys = SepFunctions.Security("FAQAccess");
                    GetManageKeys = SepFunctions.Security("FAQAdmin");
                    GetWriteKeys = "2";
                    GetWriteHide = true;
                    GetEnable = "FAQEnable";
                    GetPageName = "faq.aspx";
                    GetAdminPageName = "faq.aspx";
                    break;

                case 10:
                    GetAccessKeys = SepFunctions.Security("LibraryAccess");
                    GetWriteKeys = SepFunctions.Security("LibraryUpload");
                    GetManageKeys = SepFunctions.Security("LibraryAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "LibraryEnable";
                    GetPageName = "downloads.aspx";
                    GetAdminPageName = "downloads.aspx";
                    break;

                case 12:
                    GetAccessKeys = SepFunctions.Security("ForumsAccess");
                    GetWriteKeys = SepFunctions.Security("ForumsPost");
                    GetManageKeys = SepFunctions.Security("ForumsAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "ForumsEnable";
                    GetPageName = "forums.aspx";
                    GetAdminPageName = "forums.aspx";
                    break;

                case 13:
                    GetAccessKeys = SepFunctions.Security("FormsAccess");
                    GetManageKeys = SepFunctions.Security("FormsAdmin");
                    GetEnable = "FormsEnable";
                    GetPageName = "forms.aspx";
                    GetAdminPageName = "forms.aspx";
                    break;

                case 14:
                    GetAccessKeys = SepFunctions.Security("GuestbookAccess");
                    GetWriteKeys = SepFunctions.Security("GuestbookSign");
                    GetManageKeys = SepFunctions.Security("GuestbookAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "GuestbookEnable";
                    GetPageName = "guestbook.aspx";
                    GetAdminPageName = "guestbook.aspx";
                    break;

                case 17:
                    GetAccessKeys = SepFunctions.Security("MessengerAccess");
                    GetEnable = "MessengerEnable";
                    GetPageName = "messenger.aspx";
                    break;

                case 18:
                    GetAccessKeys = SepFunctions.Security("MatchAccess");
                    GetWriteKeys = SepFunctions.Security("MatchModify");
                    GetManageKeys = SepFunctions.Security("MatchAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "MatchEnable";
                    GetPageName = "matchmaker.aspx";
                    GetAdminPageName = "matchmaker.aspx";
                    break;

                case 19:
                    GetAccessKeys = SepFunctions.Security("LinksAccess");
                    GetWriteKeys = SepFunctions.Security("LinksPost");
                    GetManageKeys = SepFunctions.Security("LinksAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "LinksEnable";
                    GetPageName = "links.aspx";
                    GetAdminPageName = "linkdirectory.aspx";
                    break;

                case 20:
                    GetAccessKeys = SepFunctions.Security("BusinessAccess");
                    GetWriteKeys = SepFunctions.Security("BusinessPost");
                    GetManageKeys = SepFunctions.Security("BusinessAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "BusinessEnable";
                    GetPageName = "businesses.aspx";
                    GetAdminPageName = "business.aspx";
                    break;

                case 23:
                    GetAccessKeys = SepFunctions.Security("NewsAccess");
                    GetManageKeys = SepFunctions.Security("NewsAdmin");
                    GetEnable = "NewsEnable";
                    GetPageName = "news.aspx";
                    GetAdminPageName = "news.aspx";
                    break;

                case 25:
                    GetAccessKeys = SepFunctions.Security("PollsAccess");
                    GetManageKeys = SepFunctions.Security("PollsAdmin");
                    GetEnable = "PollsEnable";
                    GetPageName = "polls.aspx";
                    GetAdminPageName = "polls.aspx";
                    break;

                case 28:
                    GetAccessKeys = SepFunctions.Security("PhotosAccess");
                    GetWriteKeys = SepFunctions.Security("PhotosCreate");
                    GetManageKeys = SepFunctions.Security("PhotosAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "PhotosEnable";
                    GetPageName = "photos.aspx";
                    GetAdminPageName = "photoalbums.aspx";
                    break;

                case 31:
                    GetAccessKeys = SepFunctions.Security("AuctionAccess");
                    GetWriteKeys = SepFunctions.Security("AuctionPost");
                    GetManageKeys = SepFunctions.Security("AuctionAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "AuctionEnable";
                    GetPageName = "auction.aspx";
                    GetAdminPageName = "auction.aspx";
                    break;

                case 32:
                    GetAccessKeys = SepFunctions.Security("RStateAccess");
                    GetWriteKeys = SepFunctions.Security("RStatePost");
                    GetManageKeys = SepFunctions.Security("RStateAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "RStateEnable";
                    GetPageName = "realestate.aspx";
                    GetAdminPageName = "realestate.aspx";
                    break;

                case 33:
                    GetPageName = "account.aspx?DoAction=MainPage";
                    break;

                case 35:
                    GetAccessKeys = SepFunctions.Security("ArticlesAccess");
                    GetWriteKeys = SepFunctions.Security("ArticlesPost");
                    GetManageKeys = SepFunctions.Security("ArticlesAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "ArticlesEnable";
                    GetPageName = "articles.aspx";
                    GetAdminPageName = "articles.aspx";
                    break;

                case 37:
                    GetAccessKeys = SepFunctions.Security("ELearningAccess");
                    GetManageKeys = SepFunctions.Security("ELearningAdmin");
                    GetEnable = "ELearningEnable";
                    GetPageName = "elearning.aspx";
                    GetAdminPageName = "elearning.aspx";
                    break;

                case 40:
                    GetAccessKeys = SepFunctions.Security("HotNotAccess");
                    GetWriteKeys = SepFunctions.Security("HotNotUpload");
                    GetManageKeys = SepFunctions.Security("HotNotAdmin");
                    GetEnable = "HotNotEnable";
                    GetPageName = "hotornot.aspx";
                    GetAdminPageName = "hotornot.aspx";
                    break;

                case 41:
                    GetAccessKeys = SepFunctions.Security("ShopMallAccess");
                    GetManageKeys = SepFunctions.Security("ShopMallAdmin");
                    GetEnable = "ShopMallEnable";
                    GetPageName = "shopmall.aspx";
                    GetAdminPageName = "shoppingmall.aspx";
                    break;

                case 43:
                    GetAccessKeys = SepFunctions.Security("ReferAccess");
                    GetEnable = "ReferEnable";
                    GetPageName = "refer.aspx";
                    break;

                case 44:
                    GetAccessKeys = SepFunctions.Security("ClassifiedAccess");
                    GetWriteKeys = SepFunctions.Security("ClassifiedPost");
                    GetManageKeys = SepFunctions.Security("ClassifiedAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "ClassifiedEnable";
                    GetPageName = "classifieds.aspx";
                    GetAdminPageName = "classifiedads.aspx";
                    break;

                case 46:
                    GetAccessKeys = SepFunctions.Security("EventsAccess");
                    GetWriteKeys = SepFunctions.Security("EventsPost");
                    GetManageKeys = SepFunctions.Security("EventsAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "EventsEnable";
                    GetPageName = "events.aspx";
                    GetAdminPageName = "eventcalendar.aspx";
                    break;

                case 47:
                    GetAccessKeys = SepFunctions.Security("GamesAccess");
                    GetManageKeys = SepFunctions.Security("GamesAdmin");
                    GetEnable = "GamesEnable";
                    GetPageName = "games.aspx";
                    GetAdminPageName = "games.aspx";
                    break;

                case 50:
                    GetAccessKeys = SepFunctions.Security("SpeakerAccess");
                    GetManageKeys = SepFunctions.Security("SpeakerAdmin");
                    GetEnable = "SpeakerEnable";
                    GetPageName = "speakers.aspx";
                    GetAdminPageName = "speakerbureau.aspx";
                    break;

                case 60:
                    GetAccessKeys = SepFunctions.Security("PortalsAccess");
                    GetWriteKeys = SepFunctions.Security("PortalsCreate");
                    GetManageKeys = SepFunctions.Security("PortalsAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "PortalsEnable";
                    GetPageName = "portals.aspx";
                    GetAdminPageName = "portals.aspx";
                    break;

                case 61:
                    GetAccessKeys = SepFunctions.Security("BlogsAccess");
                    GetWriteKeys = SepFunctions.Security("BlogsCreate");
                    GetManageKeys = SepFunctions.Security("BlogsAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "BlogsEnable";
                    GetPageName = "blogs.aspx";
                    GetAdminPageName = "blogs.aspx";
                    break;

                case 63:
                    GetAccessKeys = SepFunctions.Security("ProfilesAccess");
                    GetWriteKeys = SepFunctions.Security("ProfilesModify");
                    GetManageKeys = SepFunctions.Security("ProfilesAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "ProfilesEnable";
                    GetPageName = "profiles.aspx";
                    GetAdminPageName = "userprofiles.aspx";
                    break;

                case 64:
                    GetAccessKeys = SepFunctions.Security("ConferenceAccess");
                    GetManageKeys = SepFunctions.Security("ConferenceAdmin");
                    GetEnable = "ConferenceEnable";
                    GetPageName = "celebrities.aspx";
                    break;

                case 65:
                    GetAccessKeys = SepFunctions.Security("VoucherAccess");
                    GetWriteKeys = SepFunctions.Security("VoucherModify");
                    GetManageKeys = SepFunctions.Security("VoucherAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "VoucherEnable";
                    GetPageName = "vouchers.aspx";
                    GetAdminPageName = "vouchers.aspx";
                    break;

                case 66:
                    GetAccessKeys = SepFunctions.Security("PCRAccess");
                    GetWriteKeys = SepFunctions.Security("PCREmployer");
                    GetManageKeys = SepFunctions.Security("PCRAdmin");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "PCREnable";
                    GetPageName = "careers.aspx";
                    GetAdminPageName = "careers.aspx";
                    break;

                case 69:
                    GetAccessKeys = SepFunctions.Security("VideoConferenceAccessKeys");
                    if (SepFunctions.Setup(994, "PostHide") == "Yes")
                    {
                        GetWriteHide = true;
                    }
                    else
                    {
                        GetWriteHide = false;
                    }

                    GetEnable = "VideoConferenceEnable";
                    GetPageName = "conference.aspx";
                    break;
            }
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var ModuleID = 0;

            ModuleID = SepFunctions.toInt(SepCommon.Core.SepCore.Session.getSession("ModuleID"));

            var CategoryID = SepFunctions.toLong(SepCommon.Core.SepCore.Request.Item("CatID"));

            if (ModuleID == 0 || ModuleID == 16)
            {
                return output.ToString();
            }

            var ShowMemberLink = false;
            var GetAcctPageName = string.Empty;

            var href = string.Empty;
            var jscript = string.Empty;

            var showPost = true;
            var showMenu = false;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            //SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection());
            //conn.Open();

            Load_Module_Info(ModuleID);

            if (CategoryID > 0 && ModuleID != 51)
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CAT.AccessKeys,CAT.WriteKeys,CAT.WriteHide,CAT.ManageKeys FROM Categories AS CAT WHERE CAT.CatID=@CatID AND CAT.CatID IN (SELECT CatID FROM CategoriesPortals WHERE PortalID=@PortalID AND CatID=CAT.CatID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@CatID", CategoryID);
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                GetAccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                                GetManageKeys = SepFunctions.openNull(RS["ManageKeys"]);
                                GetWriteKeys = SepFunctions.openNull(RS["WriteKeys"]);
                                GetWriteHide = SepFunctions.openBoolean(RS["WriteHide"]);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GetWriteKeys))
            {
                if (GetWriteHide && SepFunctions.CompareKeys(GetWriteKeys, true) == false)
                {
                    showPost = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(GetEnable))
            {
                if (SepFunctions.Setup(ModuleID, GetEnable) != "Enable" && SepFunctions.Setup(ModuleID, GetEnable) != "Yes")
                {
                    SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
                    return output.ToString();
                }
            }

            if (ModuleID == 10)
            {
                output.AppendLine("<div class=\"contentstyle\" id=\"libraryupdate\" style=\"display:none\"><iframe id=\"libraryupdateframe\" style=\"overflow: visible; width: 0px; height: 0px;border: 0px;\" src=\"\"></iframe></div>");
            }

            if (SepFunctions.Setup(993, "FavoritesTop") != "No")
            {
                output.AppendLine("<div id=\"AddFavorites\" style=\"display:none;\" title=\"" + SepFunctions.LangText("Add to Favorites") + "\">");
                output.AppendLine("<div id=\"AddFavoritesMsg\">");
                output.AppendLine("<p>" + SepFunctions.LangText("Are you sure you want to add this page to your favorites?") + "</p>");
                output.AppendLine("</div>");
                output.AppendLine("</div>");
            }

            if (!string.IsNullOrWhiteSpace(GetAccessKeys))
            {
                SepFunctions.RequireLogin(GetAccessKeys);
                if (SepFunctions.CompareKeys(GetAccessKeys, true) != true)
                {
                    SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=403");
                    return output.ToString();
                }
            }

            if (SepFunctions.isUserPage())

                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PageName,PageTitle FROM UPagesPages WHERE UserID=@UserID AND MenuID=0 ORDER BY Weight,PageName", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.Core.SepCore.Request.Item("UserName")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                output.AppendLine("<ul class=\"nav nav-tabs\">");
                                while (RS.Read())
                                {
                                    output.AppendLine("<li role=\"presentation\"><a class=\"nav-link\" href=\"" + sInstallFolder + SepFunctions.openNull(RS["PageName"]) + "\">" + SepFunctions.openNull(RS["PageTitle"]) + "</a></li>");
                                }

                                output.AppendLine("</ul>");
                                output.AppendLine("<br/>");
                            }
                        }
                    }
                }

            output.AppendLine("<ul class=\"nav nav-tabs\">");

            if (!string.IsNullOrWhiteSpace(GetAdminPageName) && SepFunctions.CompareKeys(GetManageKeys, true))
            {
                showMenu = true;
                output.AppendLine("<li role=\"presentation\"><a class=\"nav-link\" href=\"" + sImageFolder + "spadmin/default.aspx?DoPageURL=" + SepFunctions.UrlEncode(GetAdminPageName + "?PortalID=" + SepFunctions.Get_Portal_ID()) + "&Expand=Modules&PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Management") + "</a></li>");
            }

            if (ModuleID == 42)
            {
                if (ChatSystem.HasStarted == false)
                {
                    ChatSystem.Start(new AppSystem());
                }

                if (ChatWebUtility.CurrentIdentityIsAdministrator)
                {
                    output.AppendLine("<li role=\"presentation\"><a class=\"nav-link\" href=\"" + sImageFolder + "CuteSoft_Client/CuteChat/Chatadmin/\" target=\"_blank\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Management") + "</a></li>");
                }
            }

            if (!string.IsNullOrWhiteSpace(GetPageName) && ModuleID != 33 && ModuleID != 17)
            {
                showMenu = true;
                if (SepFunctions.Setup(993, "MainPageTop") != "No")
                {
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + GetPageName + "\"><i class=\"fa fa-list\"></i> " + SepFunctions.LangText("Main") + "</a></li>");
                }
            }

            switch (ModuleID)
            {
                case 4:
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage"), true))
                    {
                        if (SepFunctions.Get_Portal_ID() == 0)
                        {
                            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE ModuleID='4'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"window.open('" + sImageFolder + "spadmin/webpages_modify.aspx?UniqueID=" + SepFunctions.openNull(RS["UniqueID"]) + "&TopMenu=False&PortalID=" + SepFunctions.Get_Portal_ID() + "','_blank', 'width=900,height=650,location=no,menubar=no,titlebar=no,toolbar=no,resizable=yes');return false;\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Edit Page") + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand("SELECT UniqueID FROM PortalPages WHERE PageID='4' AND PortalID=@PortalID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"window.open('" + sImageFolder + "spadmin/webpages_modify.aspx?UniqueID=" + SepFunctions.openNull(RS["UniqueID"]) + "&TopMenu=False&PortalID=" + SepFunctions.Get_Portal_ID() + "','_blank', 'width=900,height=650,location=no,menubar=no,titlebar=no,toolbar=no,resizable=yes');return false;\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Edit Page") + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                case 5:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "discounts_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "discounts_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post Discount") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "discounts_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Discounts") + "</a></li>");
                    break;

                case 7:
                    if (SepFunctions.isUserPage() == false)
                    {
                        showMenu = true;
                        if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                        {
                            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand("SELECT Guestbook FROM UPagesSites WHERE UserID=@UserID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_config.aspx\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Configuration") + "</a></li>");
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_pages.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("Manage Pages") + "</a></li>");
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_pages_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Add a Web Page") + "</a></li>");
                                            if (SepFunctions.openBoolean(RS["Guestbook"]))
                                            {
                                                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_guestbook.aspx\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Manage Guestbook") + "</a></li>");
                                            }

                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + SepFunctions.GetInstallFolder(true) + "!" + SepFunctions.GetUserInformation("UserName", SepFunctions.Session_User_ID()) + "/\" target=\"_blank\"><i class=\"fa fa-th-list\"></i> " + SepFunctions.LangText("View Your Site") + "</a></li>");
                                        }
                                        else
                                        {
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_site_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Create your Site") + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "userpages_site_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Create your Site") + "</a></li>");
                        }
                    }

                    break;

                case 9:
                    showMenu = true;
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"window.open('" + sImageFolder + "spadmin/faq_modify.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "', '_blank', 'width=900,height=650,scrollbars=yes,location=no,menubar=no,titlebar=no,toolbar=no,resizable=yes');return false;\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post a FAQ") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "faq_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=FAQ\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 10:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "downloads_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "downloads_upload.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-upload\"></i> " + SepFunctions.LangText("Upload File") + "</a></li>");
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "downloads_manage.aspx\"><i class=\"fa fa-download\"></i> " + SepFunctions.LangText("My Uploads") + "</a></li>");
                    }

                    break;

                case 12:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "forums_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Forums\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 14:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "guestbook_sign.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Sign Guestbook") + "</a></li>");
                    break;

                case 17:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger.aspx\"><i class=\"fa fa-inbox\"></i> " + SepFunctions.LangText("Inbox") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger_sent.aspx\"><i class=\"fa fa-envelope-o\"></i> " + SepFunctions.LangText("Sent Items") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger_compose.aspx\"><i class=\"fa fa-envelope\"></i> " + SepFunctions.LangText("Compose") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger_find_user.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Find a User") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger_blocked_users.aspx\"><i class=\"fa fa-user-times\"></i> " + SepFunctions.LangText("Blocked Users") + "</a></li>");
                    break;

                case 18:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "matchmaker_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "matchmaker_profile_modify.aspx\"><i class=\"fa fa-user\"></i> " + SepFunctions.LangText("Add/Edit Profile") + "</a></li>");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ProfileID FROM MatchMaker WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "match/" + SepFunctions.openNull(RS["ProfileID"]) + "/" + SepFunctions.Session_User_Name() + "\"><i class=\"fa fa-th-list\"></i> " + SepFunctions.LangText("View My Profile") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    break;

                case 19:
                    showMenu = true;
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "links_post.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Add Website") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "links_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Links\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 20:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "businesses_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.CompareKeys(SepFunctions.Security("BusinessMyListings")))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "businesses_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Listings") + "</a></li>");
                    }

                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "businesses_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post Business") + "</a></li>");
                    }

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Business\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 23:
                    showMenu = true;
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=News\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 28:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "photos_my_albums.aspx\"><i class=\"fa fa-file-image-o\"></i> " + SepFunctions.LangText("Manage My Albums") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "photos_album_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Add Album") + "</a></li>");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "photos.aspx?UserID=" + SepFunctions.Session_User_ID() + "\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("View My Albums") + "</a></li>");
                    }

                    break;

                case 31:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "auction_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "auction_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Auctions") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "auction_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post an Ad") + "</a></li>");
                    }

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Auction\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 32:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "realestate_my_properties.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Properties") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "realestate_property_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post Property") + "</a></li>");
                        if (SepFunctions.CompareKeys(SepFunctions.Security("RStateTenants"), false) || SepFunctions.Setup(994, "PostHide") == "No")
                        {
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "realestate_tenants.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Tenants") + "</a></li>");
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "realestate_tenant_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Add Tenant") + "</a></li>");
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "realestate_tenant_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Quick-Check Tenants") + "</a></li>");
                        }
                    }

                    break;

                case 33:

                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "account.aspx\"><i class=\"fa fa-list\"></i> " + SepFunctions.LangText("Main") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "account_edit.aspx\"><i class=\"fa fa-list-ul\"></i> " + SepFunctions.LangText("Edit Account") + "</a></li>");
                    if (SepFunctions.ModuleActivated(38))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT UnitPrice, RecurringPrice, ModelNumber FROM ShopProducts WHERE ModuleID='38' AND Status <> -1", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        while (RS.Read())
                                        {
                                            if (SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"])) != SepFunctions.Format_Currency(0) || SepFunctions.Format_Currency(SepFunctions.openNull(RS["RecurringPrice"])) != SepFunctions.Format_Currency(0))
                                            {
                                                var arrClassID = Strings.Split(SepFunctions.openNull(RS["ModelNumber"]), "-");
                                                Array.Resize(ref arrClassID, 2);
                                                using (SqlCommand cmd2 = new SqlCommand("SELECT ClassID FROM AccessClasses WHERE ClassID=@ClassID AND PrivateClass='0' AND (PortalIDs LIKE '%|' + @PortalIDs + '|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0)", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@ClassID", arrClassID[1]);
                                                    cmd2.Parameters.AddWithValue("@PortalIDs", Strings.ToString(SepFunctions.Get_Portal_ID()));
                                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                    {
                                                        if (RS2.HasRows)
                                                        {
                                                            ShowMemberLink = true;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }

                                        if (ShowMemberLink)
                                        {
                                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "memberships.aspx\"><i class=\"fa fa-users\"></i> " + SepFunctions.LangText("Memberships") + "</a></li>");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (SepFunctions.ModuleActivated(973))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ModuleID='973' AND Status <> -1", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "buy_credits.aspx\"><i class=\"fa fa-shopping-cart\"></i> " + SepFunctions.LangText("Purchase Credits") + "</a></li>");
                                    }
                                }
                            }
                        }

                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "order_stats.aspx\"><i class=\"fa fa-cart-plus\"></i> " + SepFunctions.LangText("Order Stats") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdsStats"), true) && SepFunctions.Setup(2, "AdsStatsEnable") == "Yes")
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT AdID FROM Advertisements WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "advertising_stats.aspx\"><i class=\"fa fa-area-chart\"></i> " + SepFunctions.LangText("Ad Stats") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    if (SepFunctions.Setup(33, "FriendsEnable") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "friends.aspx\"><i class=\"fa fa-users\"></i> " + SepFunctions.LangText("Friends") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "favorites.aspx\"><i class=\"fa fa-star-o\"></i> " + SepFunctions.LangText("Favorites") + "</a></li>");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && SepFunctions.Setup(41, "ShopMallEnable") == "Enable" && SepFunctions.ModuleActivated(41))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ID FROM Favorites WHERE UserID=@UserID AND Left(PageURL, 9) = 'WISHLIST:'", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "wish_list.aspx\"><i class=\"fa fa-list-ol\"></i> " + SepFunctions.LangText("Wish List") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    // === Start Affiliate
                    if (SepFunctions.Setup(17, "AffiliateEnable") == "Enable" && SepFunctions.ModuleActivated(39))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE ModuleID='39'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "link_to_us.aspx\"><i class=\"fa fa-file-image-o\"></i> " + SepFunctions.LangText("Link to Us") + "</a></li>");
                                    }
                                }
                            }
                        }

                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "affiliate_stats.aspx\"><i class=\"fa fa-bar-chart\"></i> " + SepFunctions.LangText("Affiliate Stats") + "</a></li>");
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "affiliate_tree.aspx\"><i class=\"fa fa-tree\"></i> " + SepFunctions.LangText("Affiliate Tree") + "</a></li>");
                    }

                    // === End Affiliate
                    if (SepFunctions.Setup(17, "MessengerEnable") == "Enable")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "messenger.aspx\"><i class=\"fa fa-envelope\"></i> " + SepFunctions.LangText("Messenger") + "</a></li>");
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) && SepFunctions.Setup(63, "ProfilesEnable") == "Enable" && SepFunctions.ModuleActivated(63))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ProfileID,M.UserName FROM Profiles AS P, Members AS M WHERE M.UserID=P.UserID AND P.UserID=@UserID" + Strings.ToString(SepFunctions.Setup(60, "PortalProfiles") == "Yes" ? string.Empty : " AND P.PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty), conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "profile/" + SepFunctions.openNull(RS["ProfileID"]) + "/" + SepFunctions.openNull(RS["UserName"]) + "/\"><i class=\"fa fa-user\"></i> " + SepFunctions.LangText("Profile") + "</a></li>");
                                    }
                                    else
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "profiles_modify.aspx\"><i class=\"fa fa-user\"></i> " + SepFunctions.LangText("Create Profile") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    if (SepFunctions.Setup(61, "BlogsEnable") == "Enable" && SepFunctions.ModuleActivated(61))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "blogs_search.aspx?UserID=" + SepFunctions.Session_User_ID() + "\"><i class=\"fa fa-newspaper-o\"></i> " + SepFunctions.LangText("Blogs") + "</a></li>");
                    }

                    if (SepFunctions.Setup(12, "ForumsEnable") == "Enable" && SepFunctions.ModuleActivated(12))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "forums_search.aspx?UserID=" + SepFunctions.Session_User_ID() + "\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("Forums") + "</a></li>");
                    }

                    string strSql = "";
                    if (SepFunctions.Get_Portal_ID() == 0)
                    {
                        strSql = "SELECT MP.UniqueID,MP.PageID,MP.ModuleID,MP.UserPageName,MP.LinkText,MP.TargetWindow FROM ModulesNPages AS MP WHERE MP.MenuID=8 AND MP.Status=1 AND Activated=1 ORDER BY MP.Weight,MP.LinkText";
                    }
                    else
                    {
                        strSql = "SELECT MP.UniqueID,MP.PageID,MP.UserPageName,MP.LinkText,MP.TargetWindow FROM PortalPages AS MP WHERE (MP.PortalID=" + SepFunctions.Get_Portal_ID() + " OR MP.PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR MP.PortalIDs LIKE '%|-1|%') AND MP.MenuID=8 AND MP.Status=1 ORDER BY MP.Weight,MP.LinkText";
                    }

                    using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(strSql, conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    while (RS.Read())
                                    {
                                        switch (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])))
                                        {
                                            case 201:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]);
                                                break;

                                            case 200:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]) + "?ID=" + SepFunctions.openNull(RS["UniqueID"]);
                                                break;

                                            default:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]);
                                                break;
                                        }

                                        switch (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])))
                                        {
                                            case 201:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]);
                                                break;

                                            case 200:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]) + "?ID=" + SepFunctions.openNull(RS["UniqueID"]);
                                                break;

                                            default:
                                                GetAcctPageName = SepFunctions.openNull(RS["UserPageName"]);
                                                break;
                                        }

                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + string.Empty + GetAcctPageName + "\"><i class=\"fa fa-cog\"></i> " + SepFunctions.openNull(RS["LinkText"]) + "</a></li>");
                                    }
                                }
                            }
                        }
                    }
                    if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sImageFolder + "spadmin/default.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Administration") + "</a></li>");
                    }

                    break;

                case 35:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "articles_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "articles_manage.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Manage Articles") + "</a></li>");
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "articles_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post Article") + "</a></li>");
                    }

                    if (!string.IsNullOrWhiteSpace(SepCommon.Core.SepCore.Request.Item("ArticleID")))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "article_display.aspx?DoAction=Print&ArticleID=" + SepFunctions.UrlEncode(SepCommon.Core.SepCore.Request.Item("ArticleID")) + "\" target=\"_blank\"><i class=\"fa fa-print\"></i> " + SepFunctions.LangText("Print this Article") + "</a></li>");
                    }

                    if (!string.IsNullOrWhiteSpace(SepCommon.Core.SepCore.Request.Item("ArticleID")) && SepFunctions.CompareKeys(GetManageKeys, true))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "articles_modify.aspx?ArticleID=" + SepFunctions.UrlEncode(SepCommon.Core.SepCore.Request.Item("ArticleID")) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Edit Article") + "</a></li>");
                    }

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Articles\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 36:
                    showMenu = true;
                    if (CategoryID > 0)
                    {
                        if (showPost)
                        {
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "reviews.aspx?DoAction=Post&CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post a Review") + "</a></li>");
                        }
                    }

                    break;

                case 37:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "elearning_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "elearning_my_courses.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Courses") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "elearning_my_exams.aspx\"><i class=\"fa fa-list-ul\"></i> " + SepFunctions.LangText("My Exams") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "elearning_my_assignments.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Assignments") + "</a></li>");
                    break;

                case 40:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "hotornot.aspx?Show=Male\"><i class=\"fa fa-male\"></i> " + SepFunctions.LangText("Show Male Only") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "hotornot.aspx?Show=Female\"><i class=\"fa fa-female\"></i> " + SepFunctions.LangText("Show Female Only") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "hotornot_my_pictures.aspx\"><i class=\"fa fa-upload\"></i> " + SepFunctions.LangText("My Pictures") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "report_listing.aspx?URL=" + SepFunctions.UrlEncode(SepCommon.Core.SepCore.Request.RawUrl()) + "&UniqueID=" + SepFunctions.UrlEncode(SepCommon.Core.SepCore.Request.Item("UploadID")) + "&ModuleID=" + ModuleID + "\" id=\"HotNotReport\"><i class=\"fa fa-list-ul\"></i> " + SepFunctions.LangText("Report Picture") + "</a></li>");
                    break;

                case 41:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "shopping_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.Setup(41, "ShopMallSalesPage") != "No")
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE PortalID=@PortalID AND ModuleID=@ModuleID AND SalePrice <> '0'", conn))
                            {
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "shopping_sales.aspx\"><i class=\"fa fa-usd\"></i> " + SepFunctions.LangText("Current Sales") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "shopping_wishlist.aspx\"><i class=\"fa fa-list-ol\"></i> " + SepFunctions.LangText("Wish List") + "</a></li>");
                    if (SepFunctions.CompareKeys(SepFunctions.Security("ShopMallStore")))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "shopping_my_store.aspx\"><i class=\"fa fa-usd\"></i> " + SepFunctions.LangText("My Store") + "</a></li>");
                    }

                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Products\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 43:
                    if (SepFunctions.Setup(43, "ReferDisplayStats") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "refer_stats.aspx\"><i class=\"fa fa-line-chart\"></i> " + SepFunctions.LangText("Statistics") + "</a></li>");
                    }

                    break;

                case 44:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "classifieds_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Classifieds") + "</a></li>");
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "classifieds_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post a Classified") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "classifieds_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Classified\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 46:
                    showMenu = true;

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "events_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "events_modify.aspx?EventDate=" + SepFunctions.UrlEncode(SepCommon.Core.SepCore.Request.Item("EventDate")) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post Event") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Events\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 42:
                case 55:
                case 56:
                case 57:
                    showMenu = true;
                    break;

                case 60:
                    showMenu = true;
                    if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsCreate"), false))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "portals_create.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Create Portal") + "</a></li>");
                    }

                    break;

                case 61:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "blogs_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "blogs_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Add a Blog") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "blogs_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Blogs") + "</a></li>");
                    if (SepFunctions.Setup(993, "RSSTop") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "rss.aspx?DoAction=Blog\" target=\"_blank\"><i class=\"fa fa-rss\"></i> " + SepFunctions.LangText("RSS") + "</a></li>");
                    }

                    break;

                case 63:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "profiles_search.aspx\"><i class=\"fa fa-search\"></i> " + SepFunctions.LangText("Search") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "profiles_modify.aspx\"><i class=\"fa fa-user\"></i> " + SepFunctions.LangText("Add/Edit Profile") + "</a></li>");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT ProfileID FROM Profiles WHERE UserID='" + SepFunctions.Session_User_ID() + "'" + Strings.ToString(SepFunctions.Setup(60, "PortalProfiles") == "Yes" ? string.Empty : " AND PortalID=" + SepFunctions.Get_Portal_ID() + string.Empty), conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "profile/" + SepFunctions.openNull(RS["ProfileID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.Session_User_Name()) + "/\"><i class=\"fa fa-th-list\"></i> " + SepFunctions.LangText("View My Profile") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    break;

                case 64:
                    showMenu = true;
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        if (SepFunctions.toLong(SepFunctions.GetUserInformation("AccessClass", SepFunctions.Session_User_ID())) == SepFunctions.toLong(SepFunctions.Setup(64, "ModeratorClass")))
                        {
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "celebrities_profile_modify.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Profile") + "</a></li>");
                            output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "celebrities_schedule.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("Schedule") + "</a></li>");
                        }
                    }

                    break;

                case 65:
                    showMenu = true;
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "vouchers_modify.aspx?CatID=" + SepFunctions.UrlEncode(Strings.ToString(CategoryID)) + "\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post a Voucher") + "</a></li>");
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                    {
                        using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Vouchers WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "vouchers_manage.aspx\"><i class=\"fa fa-list-alt\"></i> " + SepFunctions.LangText("My Vouchers") + "</a></li>");
                                    }
                                }
                            }
                        }
                    }

                    break;

                case 66:
                    showMenu = true;
                    if (showPost)
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "careers_my_company.aspx\"><i class=\"fa fa-building\"></i> " + SepFunctions.LangText("My Company") + "</a></li>");
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "careers_my_jobs.aspx\"><i class=\"fa fa-briefcase\"></i> " + SepFunctions.LangText("My Positions") + "</a></li>");
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "careers_job_modify.aspx\"><i class=\"fa fa-plus\"></i> " + SepFunctions.LangText("Post a Position") + "</a></li>");
                    }

                    if (SepFunctions.CompareKeys(SepFunctions.Security("PCRBrowse"), true))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "careers_browse_candidates.aspx\"><i class=\"fa fa-list-ul\"></i> " + SepFunctions.LangText("Browse Candidates") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "careers_my_resume.aspx\"><i class=\"fa fa-list-ul\"></i> " + SepFunctions.LangText("My Resume") + "</a></li>");
                    break;

                case 67:
                    showMenu = true;
                    if (SepFunctions.Setup(67, "CRMVersion") == "SmarterTrack" || SepFunctions.Setup(67, "CRMVersion") == "SuiteCRM")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "knowledgebase.aspx\"><i class=\"fa fa-database\"></i> " + SepFunctions.LangText("Knowledge Base") + "</a></li>");
                    }

                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "tickets.aspx\"><i class=\"fa fa-ticket\"></i> " + SepFunctions.LangText("My Tickets") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "tickets_create.aspx\"><i class=\"fa fa-support\"></i> " + SepFunctions.LangText("Submit a New Ticket") + "</a></li>");
                    if (SepFunctions.Setup(67, "CRMVersion") == "SmarterTrack" && SepFunctions.Setup(67, "STKBNewsEnable") == "Yes")
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "news_feed.aspx\"><i class=\"fa fa-database\"></i> " + SepFunctions.LangText("News") + "</a></li>");
                    }

                    break;

                case 69:
                    showMenu = true;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "conference_schedule.aspx\"><i class=\"fa fa-ticket\"></i> " + SepFunctions.LangText("My Schedule") + "</a></li>");
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "conference_meeting.aspx\"><i class=\"fa fa-support\"></i> " + SepFunctions.LangText("Create Meeting") + "</a></li>");
                    if (SepFunctions.CompareKeys(SepFunctions.Security("VideoConferenceAcceptKeys"), false))
                    {
                        output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "conference_config.aspx\"><i class=\"fa fa-database\"></i> " + SepFunctions.LangText("Configuration") + "</a></li>");
                    }

                    break;

                case 999:
                    if (!string.IsNullOrWhiteSpace(SepCommon.Core.SepCore.Request.Item("UniqueID")))
                    {
                        if (SepFunctions.CompareKeys(SepFunctions.Security("AdminEditPage"), true))
                        {
                            if (SepFunctions.Get_Portal_ID() == 0)
                            {
                                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE PageID='200' AND UniqueID=@UniqueID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UniqueID", SepCommon.Core.SepCore.Request.Item("UniqueID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                showMenu = true;
                                                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"window.open('" + sImageFolder + "spadmin/webpages_modify.aspx?UniqueID=" + SepFunctions.openNull(RS["UniqueID"]) + "&TopMenu=False&PortalID=" + SepFunctions.Get_Portal_ID() + "','_blank', 'width=900,height=650,location=no,menubar=no,titlebar=no,toolbar=no,resizable=yes');return false;\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Edit Page") + "</a></li>");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd = new SqlCommand("SELECT UniqueID FROM PortalPages WHERE PageID='4' AND PortalID=@PortalID AND UniqueID=@UniqueID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                        cmd.Parameters.AddWithValue("@UniqueID", SepCommon.Core.SepCore.Request.Item("UniqueID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                showMenu = true;
                                                output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"#\" onclick=\"window.open('" + sImageFolder + "spadmin/webpages_modify.aspx?UniqueID=" + SepFunctions.openNull(RS["UniqueID"]) + "&TopMenu=False&PortalID=" + SepFunctions.Get_Portal_ID() + "','_blank', 'width=900,height=650,location=no,menubar=no,titlebar=no,toolbar=no,resizable=yes');return false;\"><i class=\"fa fa-cog\"></i> " + SepFunctions.LangText("Edit Page") + "</a></li>");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
            }

            if (ModuleID != 33 && showMenu && ModuleID != 17)
            {
                if (SepFunctions.Setup(43, "ReferTop") == "Yes" && SepFunctions.Setup(43, "ReferEnable") == "Enable" && ModuleID != 43)
                {
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sInstallFolder + "refer.aspx?URL=" + SepFunctions.UrlEncode("/" + GetPageName + SepCommon.Core.SepCore.Request.Url.Query()) + "&ModuleID=" + ModuleID + "\"><i class=\"fa fa-envelope\"></i> " + SepFunctions.LangText("Refer a Friend") + "</a></li>");
                }

                if (SepFunctions.Setup(993, "FavoritesTop") != "No")
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()))
                    {
                        var saveButton = string.Empty;
                        saveButton = "<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"saveButtonmodal\" onclick=\"saveFavorite('" + CategoryID + "', '" + ModuleID + "', '" + SepFunctions.UrlEncode(GetPageName + Strings.ToString(!string.IsNullOrWhiteSpace(SepCommon.Core.SepCore.Request.Url.Query()) ? SepCommon.Core.SepCore.Request.Url.Query() + "&" : "?") + "ModuleID=" + ModuleID) + "');$(this).hide();\">" + SepFunctions.LangText("Save") + "</button>";
                        jscript = "openDialog('AddFavorites', 500, 170, unescape('" + SepFunctions.EscQuotes(saveButton) + "'));return false;";
                    }
                    else
                    {
                        href = sInstallFolder + "login.aspx";
                    }

                    var sHasHref = !string.IsNullOrWhiteSpace(href) ? href : "#";
                    var sHasJS = !string.IsNullOrWhiteSpace(jscript) ? " onclick=\"" + jscript + "\"" : string.Empty;
                    output.AppendLine("<li class=\"nav-item\"><a class=\"nav-link\" href=\"" + sHasHref + "\"" + sHasJS + "><i class=\"fa fa-star-o\"></i> " + SepFunctions.LangText("Add to Favorites") + "</a></li>");
                }
            }

            output.AppendLine("</ul>");

            output.AppendLine("<br/>");

            return output.ToString();
        }
    }
}