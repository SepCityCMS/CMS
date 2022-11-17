// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Globals.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// A globals.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Creates a canonical.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        /// <returns>The new canonical.</returns>
        public static HtmlGenericControl CreateCanonical(string url)
        {
            using (var script = new HtmlGenericControl())
            {
                script.TagName = "link";
                script.Attributes.Add("rel", "Canonical");
                script.Attributes.Add("href", url);

                return script; // -V3071
            }
        }

        /// <summary>
        /// Creates CSS link.
        /// </summary>
        /// <param name="cssFilePath">Full pathname of the CSS file.</param>
        /// <param name="integrity">(Optional) The integrity.</param>
        /// <returns>The new CSS link.</returns>
        public static HtmlLink CreateCssLink(string cssFilePath, string integrity = "")
        {
            using (var link = new HtmlLink())
            {
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                if (!string.IsNullOrWhiteSpace(integrity))
                {
                    link.Attributes.Add("integrity", integrity);
                    link.Attributes.Add("crossorigin", "anonymous");
                }

                link.Href = link.ResolveUrl(cssFilePath);
                return link; // -V3071
            }
        }

        /// <summary>
        /// Creates java script link.
        /// </summary>
        /// <param name="scriptFilePath">Full pathname of the script file.</param>
        /// <param name="integrity">(Optional) The integrity.</param>
        /// <returns>The new java script link.</returns>
        public static HtmlGenericControl CreateJavaScriptLink(string scriptFilePath, string integrity = "")
        {
            using (var script = new HtmlGenericControl())
            {
                script.TagName = "script";
                script.Attributes.Add("type", "text/javascript");
                script.Attributes.Add("src", script.ResolveUrl(scriptFilePath));
                if (!string.IsNullOrWhiteSpace(integrity))
                {
                    script.Attributes.Add("integrity", integrity);
                    script.Attributes.Add("crossorigin", "anonymous");
                }

                return script; // -V3071
            }
        }

        /// <summary>
        /// Gets save message.
        /// </summary>
        /// <param name="master">The master.</param>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="SaveID">Identifier for the save.</param>
        /// <param name="ActDesc">Information describing the act.</param>
        /// <param name="overrideContentName">(Optional) Name of the override content.</param>
        /// <returns>The save message.</returns>
        public static string GetSaveMessage(MasterPage master, int ModuleID, int SaveID, string ActDesc, string overrideContentName = "")
        {
            string sReturn;
            if (!string.IsNullOrWhiteSpace(ActDesc))
            {
                LogGoogleAnalytics(master, ModuleID, GoogleAnalyticsActType(SaveID), ActDesc);
            }

            if (!string.IsNullOrWhiteSpace(overrideContentName))
            {
                sReturn = overrideContentName;
            }
            else
            {
                switch (ModuleID)
                {
                    case 1:
                        sReturn = "Content";
                        break;

                    case 2:
                        sReturn = "Advertisement";
                        break;

                    case 5:
                        sReturn = "Discount";
                        break;

                    case 7:
                        sReturn = "Page";
                        break;

                    case 8:
                        sReturn = "Comment";
                        break;

                    case 9:
                        sReturn = "FAQ";
                        break;

                    case 10:
                        sReturn = "Download";
                        break;

                    case 12:
                        sReturn = "Topic";
                        break;

                    case 13:
                        sReturn = "Form";
                        break;

                    case 14:
                        sReturn = "Guestbook Entry";
                        break;

                    case 17:
                        sReturn = "Message";
                        break;

                    case 18:
                        sReturn = "Profile";
                        break;

                    case 19:
                        sReturn = "Link";
                        break;

                    case 20:
                        sReturn = "Business Listing";
                        break;

                    case 23:
                        sReturn = "News";
                        break;

                    case 24:
                        sReturn = "Newsletter";
                        break;

                    case 25:
                        sReturn = "Poll";
                        break;

                    case 28:
                        sReturn = "Photo Album";
                        break;

                    case 31:
                        sReturn = "Auction";
                        break;

                    case 32:
                        sReturn = "Property";
                        break;

                    case 35:
                        sReturn = "Article";
                        break;

                    case 37:
                        sReturn = "Course";
                        break;

                    case 38:
                        sReturn = "Membership";
                        break;

                    case 39:
                        sReturn = "Affiliate";
                        break;

                    case 40:
                        sReturn = "Hot or Not";
                        break;

                    case 41:
                        sReturn = "Product";
                        break;

                    case 44:
                        sReturn = "Classified Ad";
                        break;

                    case 46:
                        sReturn = "Event";
                        break;

                    case 47:
                        sReturn = "Game";
                        break;

                    case 50:
                        sReturn = "Speaker";
                        break;

                    case 60:
                        sReturn = "Portal";
                        break;

                    case 61:
                        sReturn = "Blog";
                        break;

                    case 62:
                        sReturn = "Invoice";
                        break;

                    case 63:
                        sReturn = "Profile";
                        break;

                    case 64:
                        sReturn = "Invoice";
                        break;

                    case 65:
                        sReturn = "Voucher";
                        break;

                    case 989:
                        sReturn = "Access Class";
                        break;

                    case 990:
                        sReturn = "Group List";
                        break;

                    case 998:
                        sReturn = "Activity";
                        break;

                    default:
                        sReturn = "Record";
                        break;
                }
            }

            switch (SaveID)
            {
                case 1:
                    sReturn = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("~~" + sReturn + "~~ is waiting for approval.") + "</div>";
                    break;

                case 2:
                    sReturn = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("~~" + sReturn + "~~ has been successfully updated.") + "</div>";
                    break;

                case 3:
                    sReturn = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("~~" + sReturn + "~~ has been successfully added.") + "</div>";
                    break;

                default:
                    sReturn = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error saving ~~" + sReturn + "~~.") + "</div>";
                    break;
            }

            return sReturn;
        }

        /// <summary>
        /// Google analytics act type.
        /// </summary>
        /// <param name="ActDesc">Information describing the act.</param>
        /// <returns>A string.</returns>
        public static string GoogleAnalyticsActType(int ActDesc)
        {
            switch (ActDesc)
            {
                case 1: return "approval";
                case 2: return "updated";
                case 3: return "added";
                default: return "N/A";
            }
        }

        /// <summary>
        /// Loads site theme.
        /// </summary>
        /// <param name="master">The master.</param>
        public static void LoadSiteTheme(MasterPage master)
        {
            if (SepFunctions.Setup(993, "RequireSSL") == "Yes" && SepFunctions.Get_Portal_ID() == 0)
            {
                if (HttpContext.Current.Request.IsSecureConnection.Equals(false))
                {
                    SepCore.Response.Redirect("https://" + SepCore.Request.ServerVariables("HTTP_HOST") + HttpContext.Current.Request.RawUrl);
                }
            }

            var sInstallFolder = SepCore.Strings.Len(SepCore.HttpRuntime.AppDomainAppVirtualPath()) > 1 ? SepCore.HttpRuntime.AppDomainAppVirtualPath() : string.Empty + "/";

            var jsFiles = (ContentPlaceHolder)master.FindControl("EmbeddedScripts");

            HtmlGenericControl si = new HtmlGenericControl
            {
                TagName = "script"
            };
            si.Attributes.Add("type", "text/javascript");
            si.InnerHtml = "var config = {imageBase: '" + SepFunctions.GetInstallFolder(true) + "',siteBase: '" + SepFunctions.GetInstallFolder(false) + "'};";

            jsFiles.Controls.Add(si);

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey")))
            {
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/facebook.js"));
                HtmlGenericControl facebookJS = new HtmlGenericControl
                {
                    TagName = "script"
                };
                facebookJS.Attributes.Add("type", "text/javascript");
                facebookJS.InnerHtml = "window.fbAsyncInit = function () {" + Environment.NewLine;
                facebookJS.InnerHtml += "FB.init({" + Environment.NewLine;
                facebookJS.InnerHtml += "appId: '" + SepFunctions.Setup(989, "FacebookAPIKey") + "', // App ID" + Environment.NewLine;
                facebookJS.InnerHtml += "status: true, // check login status" + Environment.NewLine;
                facebookJS.InnerHtml += "cookie: true, // enable cookies to allow the server to access the session" + Environment.NewLine;
                facebookJS.InnerHtml += "xfbml: true  // parse XFBML" + Environment.NewLine;
                facebookJS.InnerHtml += "});" + Environment.NewLine;

                facebookJS.InnerHtml += "FB.Event.subscribe('auth.authResponseChange', function (response) {" + Environment.NewLine;
                facebookJS.InnerHtml += "if (response.status === 'connected') {" + Environment.NewLine;
                facebookJS.InnerHtml += "//SUCCESS" + Environment.NewLine;
                facebookJS.InnerHtml += "} else if (response.status === 'not_authorized') {" + Environment.NewLine;
                facebookJS.InnerHtml += "//FAILED" + Environment.NewLine;
                facebookJS.InnerHtml += "} else {" + Environment.NewLine;
                facebookJS.InnerHtml += "//UNKNOWN ERROR" + Environment.NewLine;
                facebookJS.InnerHtml += "}" + Environment.NewLine;
                facebookJS.InnerHtml += "});" + Environment.NewLine;
                facebookJS.InnerHtml += "};" + Environment.NewLine;

                facebookJS.InnerHtml += "// Load the SDK asynchronously" + Environment.NewLine;
                facebookJS.InnerHtml += "(function (d) {" + Environment.NewLine;
                facebookJS.InnerHtml += "var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];" + Environment.NewLine;
                facebookJS.InnerHtml += "if (d.getElementById(id)) { return; }" + Environment.NewLine;
                facebookJS.InnerHtml += "js = d.createElement('script'); js.id = id; js.async = true;" + Environment.NewLine;
                facebookJS.InnerHtml += "js.src = \"//connect.facebook.net/en_US/all.js\";" + Environment.NewLine;
                facebookJS.InnerHtml += "ref.parentNode.insertBefore(js, ref);" + Environment.NewLine;
                facebookJS.InnerHtml += "}(document));" + Environment.NewLine;
                jsFiles.Controls.Add(facebookJS);
            }

            var LinkedInAPI = SepFunctions.Setup(989, "LinkedInAPI");
            var LinkedInSecret = SepFunctions.Setup(989, "LinkedInSecret");
            if (!string.IsNullOrWhiteSpace(LinkedInAPI) && !string.IsNullOrWhiteSpace(LinkedInSecret))
            {
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/linkedin.js"));

                HtmlGenericControl LinkInJS = new HtmlGenericControl
                {
                    TagName = "script"
                };
                LinkInJS.Attributes.Add("type", "text/javascript");
                LinkInJS.Attributes.Add("src", "https://platform.linkedin.com/in.js");

                LinkInJS.InnerHtml += "api_key: " + LinkedInAPI + Environment.NewLine;
                LinkInJS.InnerHtml += "onLoad: OnLinkedInFrameworkLoad" + Environment.NewLine;
                LinkInJS.InnerHtml += "authorize: true" + Environment.NewLine;
                jsFiles.Controls.Add(LinkInJS);
            }

            jsFiles.Controls.Add(CreateCssLink("https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css", "sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn"));

            jsFiles.Controls.Add(CreateCssLink(sInstallFolder + "skins/public/styles/public.css"));

            jsFiles.Controls.Add(CreateCssLink("https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css", "sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"));

            jsFiles.Controls.Add(CreateCssLink("https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick.min.css", "sha256-UK1EiopXIL+KVhfbFa8xrmAWPeBjMVdvYMYkTAEv/HI="));

            jsFiles.Controls.Add(CreateCssLink("https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick-theme.min.css", "sha256-4hqlsNP9KM6+2eA8VUT0kk4RsMRTeS7QGHIM+MZ5sLY="));

            jsFiles.Controls.Add(CreateJavaScriptLink("https://code.jquery.com/jquery-3.6.0.min.js", "sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4="));
            jsFiles.Controls.Add(CreateJavaScriptLink("https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js", "sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49"));
            jsFiles.Controls.Add(CreateJavaScriptLink("https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js", "sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF"));

            jsFiles.Controls.Add(CreateJavaScriptLink("https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js", "sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI="));

            jsFiles.Controls.Add(CreateJavaScriptLink("https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick.min.js", "sha256-NXRS8qVcmZ3dOv3LziwznUHPegFhPZ1F/4inU7uC8h0="));

            jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/bootbox.min.js"));

            jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/main.js"));

            var currentURL = SepCore.Request.RawUrl();
            string canonicalTag = string.Empty;
            string contentUniqueID;
            if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "cat")) == sInstallFolder + "cat")
            {
                var arrUrl = SepCore.Strings.Split(currentURL, "/");
                Array.Resize(ref arrUrl, 5);
                if (sInstallFolder != "/")
                {
                    contentUniqueID = arrUrl[3];
                }
                else
                {
                    contentUniqueID = arrUrl[2];
                }

                if (sInstallFolder != "/")
                {
                    switch (arrUrl[2])
                    {
                        case "cat5":
                            canonicalTag = "discounts.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat7":
                            canonicalTag = "userpages.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat9":
                            canonicalTag = "faq.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat10":
                            canonicalTag = "downloads.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat12":
                            canonicalTag = "forums.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat19":
                            canonicalTag = "links.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat20":
                            canonicalTag = "businesses.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat23":
                            canonicalTag = "news.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat31":
                            canonicalTag = "auction.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat35":
                            canonicalTag = "articles.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat37":
                            canonicalTag = "elearning.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat41":
                            canonicalTag = "shopmall.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat44":
                            canonicalTag = "classifieds.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat60":
                            canonicalTag = "portals.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat61":
                            canonicalTag = "blogs.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat65":
                            canonicalTag = "vouchers.aspx?CatID=" + contentUniqueID;
                            break;
                    }
                }
                else
                {
                    switch (arrUrl[1])
                    {
                        case "cat5":
                            canonicalTag = "discounts.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat7":
                            canonicalTag = "userpages.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat9":
                            canonicalTag = "faq.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat10":
                            canonicalTag = "downloads.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat12":
                            canonicalTag = "forums.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat19":
                            canonicalTag = "links.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat20":
                            canonicalTag = "businesses.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat23":
                            canonicalTag = "news.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat31":
                            canonicalTag = "auction.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat35":
                            canonicalTag = "articles.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat37":
                            canonicalTag = "elearning.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat41":
                            canonicalTag = "shopmall.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat44":
                            canonicalTag = "classifieds.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat60":
                            canonicalTag = "portals.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat61":
                            canonicalTag = "blogs.aspx?CatID=" + contentUniqueID;
                            break;

                        case "cat65":
                            canonicalTag = "vouchers.aspx?CatID=" + contentUniqueID;
                            break;
                    }
                }
            }
            else
            {
                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "article/")) == sInstallFolder + "article/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "article_display.aspx?ArticleID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "albums/")) == sInstallFolder + "albums/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "photos_albums_view.aspx?AlbumID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "auction/")) == sInstallFolder + "auction/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "auction_display.aspx?AdID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "blogs/")) == sInstallFolder + "blogs/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "blogs_view.aspx?BlogID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "discounts/")) == sInstallFolder + "discounts/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "discounts_view.aspx?DiscountID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "course/")) == sInstallFolder + "course/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "elearning_course_view.aspx?CourseID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "faq/")) == sInstallFolder + "faq/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "faq_display.aspx?FAQID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "celebrity/")) == sInstallFolder + "celebrity/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "celebrities_profile_view.aspx?ProfileID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "forms/")) == sInstallFolder + "forms/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "forms_view.aspx?FormID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "forum/")) == sInstallFolder + "forum/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "forums_display.aspx?TopicID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "game/")) == sInstallFolder + "game/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "games_play.aspx?GameID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "linkdir/")) == sInstallFolder + "linkdir/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "links_redirect.aspx?LinkID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "match/")) == sInstallFolder + "match/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "matchmaker_view_profile.aspx?ProfileID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "news/")) == sInstallFolder + "news/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "news_display.aspx?NewsID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "poll/")) == sInstallFolder + "poll/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "polls_display.aspx?PollID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "profile/")) == sInstallFolder + "profile/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "profiles_display.aspx?ProfileID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "position/")) == sInstallFolder + "position/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "careers_job_view.aspx?JobId=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "@")) == sInstallFolder + "@")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 3);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[2];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[1];
                    }

                    if (SepCore.Strings.Len(contentUniqueID) > 0)
                    {
                        canonicalTag = "profiles_display.aspx?ProfileUser=" + SepCore.Strings.Right(contentUniqueID, SepCore.Strings.Len(contentUniqueID) - 1);
                    }
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "property/")) == sInstallFolder + "property/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "realestate_property_view.aspx?PropertyID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "page/")) == sInstallFolder + "page/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "viewpage.aspx?UniqueID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "business/")) == sInstallFolder + "business/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "businesses_display.aspx?BusinessID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "classified/")) == sInstallFolder + "classified/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "classifieds_display.aspx?AdID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "event/")) == sInstallFolder + "event/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "events_view.aspx?EventID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "shopping/")) == sInstallFolder + "shopping/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "shop_product_view.aspx?ProductID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "uservideo/")) == sInstallFolder + "uservideo/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "video/default.aspx?UserID=" + contentUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "voucher/")) == sInstallFolder + "voucher/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        contentUniqueID = arrUrl[3];
                    }
                    else
                    {
                        contentUniqueID = arrUrl[2];
                    }

                    canonicalTag = "vouchers_display.aspx?VoucherID=" + contentUniqueID;
                }
            }

            // Set canonical
            if (!string.IsNullOrWhiteSpace(canonicalTag))
            {
                HtmlGenericControl canonicalLink = new HtmlGenericControl
                {
                    TagName = "link"
                };
                canonicalLink.Attributes.Add("rel", "canonical");
                canonicalLink.Attributes.Add("href", canonicalTag);
                jsFiles.Controls.Add(canonicalLink);
            }

            // Set Language
            var siteLang = SepFunctions.GetSiteLanguage();
            siteLang = SepCore.Strings.LCase(siteLang.Split('-')[0]) + "-" + siteLang.Split('-')[1];

            if (!string.IsNullOrWhiteSpace(siteLang) && siteLang != "en-US")
            {
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/cldr.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/cldr/event.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/cldr/supplemental.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/currency.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/date.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/message.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/number.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/plural.js"));
                jsFiles.Controls.Add(CreateJavaScriptLink(sInstallFolder + "js/jquery/globalize/relative-time.js"));

                HtmlGenericControl langJS = new HtmlGenericControl
                {
                    TagName = "script"
                };
                langJS.Attributes.Add("type", "text/javascript");
                langJS.InnerHtml += "Globalize.locale('" + siteLang + "');";
                jsFiles.Controls.Add(langJS);
            }

            // End Language
            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsID")))
            {
                HtmlGenericControl analyticsJS = new HtmlGenericControl
                {
                    TagName = "script"
                };
                analyticsJS.Attributes.Add("type", "text/javascript");
                analyticsJS.InnerHtml += "(function (i, s, o, g, r, a, m) {" + Environment.NewLine;
                analyticsJS.InnerHtml += "i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {" + Environment.NewLine;
                analyticsJS.InnerHtml += "(i[r].q = i[r].q || []).push(arguments);" + Environment.NewLine;
                analyticsJS.InnerHtml += "}, i[r].l = 1 * new Date(); a = s.createElement(o)," + Environment.NewLine;
                analyticsJS.InnerHtml += "m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g;" + Environment.NewLine;
                analyticsJS.InnerHtml += "m.parentNode.insertBefore(a, m);" + Environment.NewLine;
                analyticsJS.InnerHtml += "})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');" + Environment.NewLine;
                analyticsJS.InnerHtml += "ga('create', '" + SepFunctions.Setup(989, "GoogleAnalyticsID") + "');" + Environment.NewLine;
                analyticsJS.InnerHtml += "ga('send', 'pageview');" + Environment.NewLine;
                jsFiles.Controls.Add(analyticsJS);
            }

            // Add power by SepCity for standard version
            if (SepFunctions.isProfessionalEdition() == false)
            {
                var objPowerBy = (Literal)master.FindControl("PoweredBySepCity");
                if (objPowerBy == null)
                {
                    SepCore.Response.Write("<script type=\"text/javascript\">alert('This site must have the following code in the template file unless if you purchase the \"Enterprise Edition\".\\n\\n<asp:Literal ID=\"PoweredBySepCity\" runat=\"server\" />');</script>");
                }
                else
                {
                    objPowerBy.Text = "<p><a href=\"https://www.sepcity.com\" target=\"_blank\">Powered by SepCity, Inc. CMS / Portal Solutions.</a></p>";
                }
            }
        }

        /// <summary>
        /// Logs google analytics.
        /// </summary>
        /// <param name="master">The master.</param>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="ActType">Type of the act.</param>
        /// <param name="ActDesc">Information describing the act.</param>
        public static void LogGoogleAnalytics(MasterPage master, int ModuleID, string ActType, string ActDesc)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleAnalyticsID")))
            {
                var moduleName = SepFunctions.GetModuleName(ModuleID);
                var jsFiles = (ContentPlaceHolder)master.FindControl("EmbeddedScripts");
                HtmlGenericControl analyticsJS = new HtmlGenericControl
                {
                    TagName = "script"
                };
                analyticsJS.Attributes.Add("type", "text/javascript");
                analyticsJS.InnerHtml += "LogGoogleEvent(unescape('" + SepFunctions.EscQuotes(moduleName) + "'), unescape('" + SepFunctions.EscQuotes(ActType) + "'), unescape('" + SepFunctions.EscQuotes(ActDesc) + "'));" + Environment.NewLine;
                jsFiles.Controls.Add(analyticsJS);
            }
        }

        /// <summary>
        /// Redirect URL.
        /// </summary>
        /// <param name="currentURL">URL of the current.</param>
        /// <returns>A string.</returns>
        public static string RedirectURL(string currentURL)
        {
            var sInstallFolder = SepCore.Strings.Len(SepCore.HttpRuntime.AppDomainAppVirtualPath()) > 1 ? SepCore.HttpRuntime.AppDomainAppVirtualPath() : string.Empty + "/";

            long iPortalID = 0;
            var FriendlyPortalID = string.Empty;
            var rewritePage = string.Empty;

            if (currentURL == null)
            {
                return string.Empty;
            }

            if (currentURL == "/")
            {
                return string.Empty;
            }

            if (currentURL.EndsWith(".js", StringComparison.CurrentCulture))
            {
                return string.Empty;
            }

            if (currentURL.EndsWith(".css", StringComparison.CurrentCulture))
            {
                return string.Empty;
            }

            if (currentURL.EndsWith(".gif", StringComparison.CurrentCulture))
            {
                return string.Empty;
            }

            if (currentURL.EndsWith(".png", StringComparison.CurrentCulture))
            {
                return string.Empty;
            }

            if (currentURL.EndsWith(".jpg", StringComparison.CurrentCulture))
            {
                return string.Empty;
            }

            if (SepCore.Strings.InStr(currentURL, sInstallFolder + "install/") > 0)
            {
                return string.Empty;
            }

            if (SepCore.Strings.InStr(currentURL, sInstallFolder + "spadmin/") > 0)
            {
                return string.Empty;
            }

            if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "members/")) == sInstallFolder + "members/")
            {
                currentURL = SepCore.Strings.Replace(currentURL, sInstallFolder + "members/", sInstallFolder + "!");
            }

            string sUniqueID;
            if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "go/")) == sInstallFolder + "go/")
            {
                var arrUrl = SepCore.Strings.Split(currentURL, "/");
                var sPageName = string.Empty;
                if (sInstallFolder != "/")
                {
                    sUniqueID = arrUrl[3];
                    for (var i = 4; i <= SepCore.Information.UBound(arrUrl); i++)
                    {
                        if (i > 4)
                        {
                            sPageName += "/";
                        }

                        sPageName += arrUrl[i];
                    }
                }
                else
                {
                    sUniqueID = arrUrl[2];
                    for (var i = 3; i <= SepCore.Information.UBound(arrUrl); i++)
                    {
                        if (i > 3)
                        {
                            sPageName += "/";
                        }

                        sPageName += arrUrl[i];
                    }
                }

                if (sPageName.Contains(".aspx"))
                {
                    sPageName += SepCore.Request.Url.Query();
                }

                if (!string.IsNullOrWhiteSpace(sPageName))
                {
                    if (sPageName.Contains(".aspx"))
                    {
                        if (SepCore.Strings.InStr(sPageName, "?") > 0)
                        {
                            var sURL = SepCore.Strings.InStr(sPageName, "FriendlyPortalID=") == 0 ? "&FriendlyPortalID=" + sUniqueID : string.Empty;
                            rewritePage = sInstallFolder + sPageName + sURL;
                        }
                        else
                        {
                            var sURL = SepCore.Strings.InStr(sPageName, "FriendlyPortalID=") == 0 ? "?FriendlyPortalID=" + sUniqueID : string.Empty;
                            rewritePage = sInstallFolder + sPageName + sURL;
                        }
                    }
                    else
                    {
                        FriendlyPortalID = sUniqueID;
                        currentURL = sInstallFolder + sPageName;
                    }
                }
                else
                {
                    rewritePage = sInstallFolder + "default.aspx?FriendlyPortalID=" + sUniqueID;
                }
            }

            if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "portals/")) == sInstallFolder + "portals/")
            {
                var arrUrl = SepCore.Strings.Split(currentURL, "/");
                var sPageName = string.Empty;
                if (sInstallFolder != "/")
                {
                    sUniqueID = arrUrl[3];
                    for (var i = 4; i <= SepCore.Information.UBound(arrUrl); i++)
                    {
                        if (i > 4)
                        {
                            sPageName += "/";
                        }

                        sPageName += arrUrl[i];
                    }
                }
                else
                {
                    sUniqueID = arrUrl[2];
                    for (var i = 3; i <= SepCore.Information.UBound(arrUrl); i++)
                    {
                        if (i > 3)
                        {
                            sPageName += "/";
                        }

                        sPageName += arrUrl[i];
                    }
                }

                if (sPageName.Contains(".aspx"))
                {
                    sPageName += SepCore.Request.Url.Query();
                }

                if (!string.IsNullOrWhiteSpace(sPageName))
                {
                    if (sPageName.Contains(".aspx"))
                    {
                        if (SepCore.Strings.InStr(sPageName, "?") > 0)
                        {
                            var sURL = SepCore.Strings.InStr(sPageName, "PortalID=") == 0 ? "&PortalID=" + sUniqueID : string.Empty;
                            rewritePage = sInstallFolder + sPageName + sURL;
                        }
                        else
                        {
                            var sURL = SepCore.Strings.InStr(sPageName, "PortalID=") == 0 ? "?PortalID=" + sUniqueID : string.Empty;
                            rewritePage = sInstallFolder + sPageName + sURL;
                        }
                    }
                    else
                    {
                        iPortalID = Convert.ToInt64(sUniqueID);
                        currentURL = sInstallFolder + sPageName;
                    }
                }
                else
                {
                    rewritePage = sInstallFolder + "default.aspx?PortalID=" + sUniqueID;
                }
            }

            // /catmoduleId/CatId/CatName/ /cat44/802230618454246/Antiques/
            if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "cat")) == sInstallFolder + "cat")
            {
                var arrUrl = SepCore.Strings.Split(currentURL, "/");
                Array.Resize(ref arrUrl, 5);
                if (sInstallFolder != "/")
                {
                    sUniqueID = arrUrl[3];
                }
                else
                {
                    sUniqueID = arrUrl[2];
                }

                if (sInstallFolder != "/")
                {
                    switch (arrUrl[2])
                    {
                        case "cat5":
                            rewritePage = sInstallFolder + "discounts.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat7":
                            rewritePage = sInstallFolder + "userpages.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat9":
                            rewritePage = sInstallFolder + "faq.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat10":
                            rewritePage = sInstallFolder + "downloads.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat12":
                            rewritePage = sInstallFolder + "forums.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat19":
                            rewritePage = sInstallFolder + "links.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat20":
                            rewritePage = sInstallFolder + "businesses.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat23":
                            rewritePage = sInstallFolder + "news.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat31":
                            rewritePage = sInstallFolder + "auction.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat35":
                            rewritePage = sInstallFolder + "articles.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat37":
                            rewritePage = sInstallFolder + "elearning.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat41":
                            rewritePage = sInstallFolder + "shopmall.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat44":
                            rewritePage = sInstallFolder + "classifieds.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat60":
                            rewritePage = sInstallFolder + "portals.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat61":
                            rewritePage = sInstallFolder + "blogs.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat65":
                            rewritePage = sInstallFolder + "vouchers.aspx?CatID=" + sUniqueID;
                            break;
                    }
                }
                else
                {
                    switch (arrUrl[1])
                    {
                        case "cat5":
                            rewritePage = sInstallFolder + "discounts.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat7":
                            rewritePage = sInstallFolder + "userpages.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat9":
                            rewritePage = sInstallFolder + "faq.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat10":
                            rewritePage = sInstallFolder + "downloads.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat12":
                            rewritePage = sInstallFolder + "forums.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat19":
                            rewritePage = sInstallFolder + "links.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat20":
                            rewritePage = sInstallFolder + "businesses.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat23":
                            rewritePage = sInstallFolder + "news.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat31":
                            rewritePage = sInstallFolder + "auction.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat35":
                            rewritePage = sInstallFolder + "articles.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat37":
                            rewritePage = sInstallFolder + "elearning.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat41":
                            rewritePage = sInstallFolder + "shopmall.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat44":
                            rewritePage = sInstallFolder + "classifieds.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat60":
                            rewritePage = sInstallFolder + "portals.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat61":
                            rewritePage = sInstallFolder + "blogs.aspx?CatID=" + sUniqueID;
                            break;

                        case "cat65":
                            rewritePage = sInstallFolder + "vouchers.aspx?CatID=" + sUniqueID;
                            break;
                    }
                }
            }
            else
            {
                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "albums/")) == sInstallFolder + "albums/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "photos_albums_view.aspx?AlbumID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "article/")) == sInstallFolder + "article/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "article_display.aspx?ArticleID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "auction/")) == sInstallFolder + "auction/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "auction_display.aspx?AdID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "blogs/")) == sInstallFolder + "blogs/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "blogs_view.aspx?BlogID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "discounts/")) == sInstallFolder + "discounts/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "discounts_view.aspx?DiscountID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "course/")) == sInstallFolder + "course/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "elearning_course_view.aspx?CourseID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "faq/")) == sInstallFolder + "faq/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "faq_display.aspx?FAQID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "celebrity/")) == sInstallFolder + "celebrity/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "celebrities_profile_view.aspx?ProfileID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "forms/")) == sInstallFolder + "forms/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "forms_view.aspx?FormID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "forum/")) == sInstallFolder + "forum/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "forums_display.aspx?TopicID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "game/")) == sInstallFolder + "game/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "games_play.aspx?GameID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "linkdir/")) == sInstallFolder + "linkdir/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "links_redirect.aspx?LinkID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "match/")) == sInstallFolder + "match/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "matchmaker_view_profile.aspx?ProfileID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "news/")) == sInstallFolder + "news/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "news_display.aspx?NewsID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "poll/")) == sInstallFolder + "poll/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "polls_display.aspx?PollID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "profile/")) == sInstallFolder + "profile/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "profiles_display.aspx?ProfileID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "position/")) == sInstallFolder + "position/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "careers_job_view.aspx?JobId=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "@")) == sInstallFolder + "@")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 3);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[2];
                    }
                    else
                    {
                        sUniqueID = arrUrl[1];
                    }

                    if (SepCore.Strings.Len(sUniqueID) > 0)
                    {
                        rewritePage = sInstallFolder + "profiles_display.aspx?ProfileUser=" + SepCore.Strings.Right(sUniqueID, SepCore.Strings.Len(sUniqueID) - 1);
                    }
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "property/")) == sInstallFolder + "property/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "realestate_property_view.aspx?PropertyID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "!")) == sInstallFolder + "!")
                {
                    var sPageName = string.Empty;
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, SepCore.Information.UBound(arrUrl) + 1);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[2];
                        for (var i = 3; i <= SepCore.Information.UBound(arrUrl); i++)
                        {
                            sPageName += "/" + arrUrl[i];
                        }
                    }
                    else
                    {
                        sUniqueID = arrUrl[1];
                        for (var i = 2; i <= SepCore.Information.UBound(arrUrl); i++)
                        {
                            sPageName += "/" + arrUrl[i];
                        }
                    }

                    if (string.IsNullOrWhiteSpace(sPageName) || sPageName == "/")
                    {
                        sPageName = "default.aspx";
                    }

                    if (SepCore.Strings.Left(sPageName, 1) == "/")
                    {
                        sPageName = SepCore.Strings.Right(sPageName, SepCore.Strings.Len(sPageName) - 1);
                    }

                    if (SepCore.Strings.Len(sUniqueID) > 0)
                    {
                        var sQuery = string.Empty;
                        foreach (var Item in HttpContext.Current.Request.QueryString)
                        {
                            if (SepCore.Strings.LCase(SepCore.Strings.ToString(Item)) != "username" && SepCore.Strings.LCase(SepCore.Strings.ToString(Item)) != "pagename")
                            {
                                sQuery += "&" + SepCore.Strings.ToString(Item) + "=" + HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.QueryString[SepCore.Strings.ToString(Item)]);
                            }
                        }

                        rewritePage = sInstallFolder + "userpages_site_view.aspx?UserName=" + SepCore.Strings.Right(sUniqueID, SepCore.Strings.Len(sUniqueID) - 1) + "&PageName=" + System.Net.WebUtility.UrlDecode(sPageName) + sQuery;
                    }
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "page/")) == sInstallFolder + "page/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "viewpage.aspx?UniqueID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "business/")) == sInstallFolder + "business/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "businesses_display.aspx?BusinessID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "classified/")) == sInstallFolder + "classified/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "classifieds_display.aspx?AdID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "event/")) == sInstallFolder + "event/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "events_view.aspx?EventID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "shopping/")) == sInstallFolder + "shopping/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "shop_product_view.aspx?ProductID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "uservideo/")) == sInstallFolder + "uservideo/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "video/default.aspx?UserID=" + sUniqueID;
                }

                if (SepCore.Strings.Left(currentURL, SepCore.Strings.Len(sInstallFolder + "voucher/")) == sInstallFolder + "voucher/")
                {
                    var arrUrl = SepCore.Strings.Split(currentURL, "/");
                    Array.Resize(ref arrUrl, 5);
                    if (sInstallFolder != "/")
                    {
                        sUniqueID = arrUrl[3];
                    }
                    else
                    {
                        sUniqueID = arrUrl[2];
                    }

                    rewritePage = sInstallFolder + "vouchers_display.aspx?VoucherID=" + sUniqueID;
                }
            }

            if (!string.IsNullOrWhiteSpace(rewritePage))
            {
                // FriendlyPortalID
                if (iPortalID > 0)
                {
                    if (SepCore.Strings.InStr(rewritePage, "?") > 0)
                    {
                        rewritePage += SepCore.Strings.InStr(rewritePage, "PortalID=") == 0 ? "&PortalID=" + iPortalID : string.Empty;
                    }
                    else
                    {
                        rewritePage += SepCore.Strings.InStr(rewritePage, "PortalID=") == 0 ? "?PortalID=" + iPortalID : string.Empty;
                    }
                }

                if (!string.IsNullOrWhiteSpace(FriendlyPortalID))
                {
                    if (SepCore.Strings.InStr(rewritePage, "?") > 0)
                    {
                        rewritePage += SepCore.Strings.InStr(rewritePage, "FriendlyPortalID=") == 0 ? "&FriendlyPortalID=" + FriendlyPortalID : string.Empty;
                    }
                    else
                    {
                        rewritePage += SepCore.Strings.InStr(rewritePage, "FriendlyPortalID=") == 0 ? "?FriendlyPortalID=" + FriendlyPortalID : string.Empty;
                    }
                }

                return rewritePage;
            }

            return string.Empty;
        }

        /// <summary>
        /// Saves a template.
        /// </summary>
        /// <param name="TemplateID">Identifier for the template.</param>
        /// <param name="saveFeatures">True to save features.</param>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="PortalId">Identifier for the portal.</param>
        public static void Save_Template(string TemplateID, bool saveFeatures, int ModuleID, long PortalId)
        {
            var skinsFolder = SepFunctions.GetDirValue("skins");

            var jSiteTemplates = DAL.SiteTemplates.Template_Get(SepFunctions.toLong(TemplateID));

            var sConfigFile = skinsFolder + jSiteTemplates.FolderName + "\\config_default.xml";

            if (File.Exists(sConfigFile))
            {
                long xmlCount = 0;
                var doc = new XmlDocument();
                doc.Load(sConfigFile);
                var root = doc.DocumentElement;
                XmlNodeList nodelist;
                if (saveFeatures)
                {
                    nodelist = root.GetElementsByTagName("Feature");
                    foreach (XmlElement node in nodelist)
                    {
                        xmlCount += 1;
                        node.InnerText = SepCore.Strings.ToString(SepFunctions.toLong(SepCore.Request.Item("Feature" + xmlCount)));
                    }

                    if (SepFunctions.Setup(7, "UPagesEnable") == "Enable")
                    {
                        xmlCount = 0;
                        nodelist = root.GetElementsByTagName("UPFeature");
                        foreach (XmlElement node in nodelist)
                        {
                            xmlCount += 1;
                            node.InnerText = SepCore.Strings.ToString(SepFunctions.toLong(SepCore.Request.Item("UPFeature" + xmlCount)));
                        }
                    }
                    else
                    {
                        xmlCount = 0;
                        nodelist = root.GetElementsByTagName("UPFeature");
                        foreach (XmlElement node in nodelist)
                        {
                            xmlCount += 1;
                            node.InnerText = SepCore.Strings.ToString(SepFunctions.toLong(SepCore.Request.Item("Feature" + xmlCount)));
                        }
                    }
                }

                xmlCount = 0;
                nodelist = root.GetElementsByTagName("Variable");
                foreach (XmlElement node in nodelist)
                {
                    xmlCount += 1;
                    switch (node["Question"].GetAttribute("type"))
                    {
                        case "Image":
                            try
                            {
                                var userPostedFile = HttpContext.Current.Request.Files["Custom" + xmlCount];
                                var sFileName = string.Empty;
                                var sFileExt = string.Empty;

                                if (userPostedFile != null)
                                {
                                    if (userPostedFile.ContentLength > 0)
                                    {
                                        sFileExt = SepCore.Strings.LCase(Path.GetExtension(userPostedFile.FileName));
                                        if (sFileExt == ".jpg" || sFileExt == ".jpeg" || sFileExt == ".gif" || sFileExt == ".png")
                                        {
                                            sFileName = SepFunctions.GetIdentity() + sFileExt;
                                            node["Value"].InnerText = sFileName;
                                            userPostedFile.SaveAs(skinsFolder + jSiteTemplates.FolderName + "\\images\\" + sFileName);
                                        }
                                    }
                                    else
                                    {
                                        if (ModuleID == 7)
                                        {
                                            if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml"))
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                            else
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config_default.xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                        }
                                        else if (ModuleID == 60 && PortalId > 0)
                                        {
                                            if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepCore.Strings.ToString(PortalId)) + ".xml"))
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepCore.Strings.ToString(PortalId)) + ".xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                            else
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config_default.xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                        }
                                        else
                                        {
                                            if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config.xml"))
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config.xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                            else
                                            {
                                                var ReadDoc = new XmlDocument();
                                                ReadDoc.Load(SepCore.HostingEnvironment.MapPath("~/skins/") + jSiteTemplates.FolderName + "\\config_default.xml");
                                                node["Value"].InnerText = ReadDoc.SelectSingleNode("/root/CustomVariables/Variable[@name='" + node.ParentNode["Variable"].GetAttribute("name") + "']/Value").InnerText;
                                                ReadDoc = null;
                                                GC.Collect();
                                                GC.WaitForPendingFinalizers();
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }

                            break;

                        case "HTML":
                            node["Value"].InnerText = SepCore.Request.Item("txtCustom" + xmlCount);
                            break;

                        default:
                            node["Value"].InnerText = SepCore.Request.Item("Custom" + xmlCount);
                            break;
                    }
                }

                if (ModuleID == 60 && PortalId > 0)
                {
                    if (File.Exists(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepCore.Strings.ToString(PortalId)) + ".xml") == false)
                    {
                        using (var sw = File.CreateText(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepCore.Strings.ToString(PortalId)) + ".xml"))
                        {
                            sw.WriteLine(doc.OuterXml);
                        }
                    }
                    else
                    {
                        using (var outfile = new StreamWriter(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepCore.Strings.ToString(PortalId)) + ".xml"))
                        {
                            outfile.Write(doc.OuterXml);
                        }
                    }
                }
                else
                {
                    if (File.Exists(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml") == false)
                    {
                        using (var sw = File.CreateText(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml"))
                        {
                            sw.WriteLine(doc.OuterXml);
                        }
                    }
                    else
                    {
                        using (var outfile = new StreamWriter(skinsFolder + jSiteTemplates.FolderName + "\\config-" + SepFunctions.CleanFileName(SepFunctions.Session_User_ID()) + ".xml"))
                        {
                            outfile.Write(doc.OuterXml);
                        }
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}

namespace SepCommon
{
    using System;

    /// <summary>
    /// A global variables. This class cannot be inherited.
    /// </summary>
    public sealed class GlobalVars
    {
        /// <summary>
        /// Global variable to get the ModuleID for each module.
        /// </summary>
        /// <value>The identifier of the module.</value>
        public static int ModuleID
        {
            get
            {
                try
                {
                    return Convert.ToInt16(SepCore.Session.getSession("ModuleID"));
                }
                catch
                {
                    return 0;
                }
            }

            set => SepCore.Session.setSession("ModuleID", SepCore.Strings.ToString(value));
        }
    }
}