// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="rss.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;

    /// <summary>
    /// Class rss.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class rss : Page
    {
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
        /// RSSs the articles.
        /// </summary>
        public void RSS_Articles()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Articles.GetArticles();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Headline) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "article/" + cRSS[i].ArticleID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Headline)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Full_Article) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].Headline_Date)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the auctions.
        /// </summary>
        public void RSS_Auctions()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Auctions.GetAuctionAds();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Title) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "auction/" + cRSS[i].AdID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Title)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the blogs.
        /// </summary>
        public void RSS_Blogs()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Blogs.GetBlogs();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].BlogName) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "blogs/" + cRSS[i].BlogID + "/" + SepFunctions.Format_ISAPI(cRSS[i].BlogName)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the business.
        /// </summary>
        public void RSS_Business()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Businesses.GetBusinesses();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].BusinessName) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "business/" + cRSS[i].BusinessID + "/" + SepFunctions.Format_ISAPI(cRSS[i].BusinessName)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the classifieds.
        /// </summary>
        public void RSS_Classifieds()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Classifieds.GetClassifiedAds(availableItems: true);
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Title) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "classified/" + cRSS[i].AdID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Title)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the downloads.
        /// </summary>
        public void RSS_Downloads()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Downloads.GetDownloads("Mod.DatePosted", "DESC");
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Field1) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "downloads_view.aspx?FileID=" + cRSS[i].FileID) + "</link>");
                    if (!string.IsNullOrWhiteSpace(cRSS[i].Field3)) Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Field3) + "</description>");
                    else Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Field2) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the e learning.
        /// </summary>
        public void RSS_ELearning()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.ELearning.GetELearningCourses("Mod.StartDate", "DESC");
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].CourseName) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "course/" + cRSS[i].CourseID + "/" + SepFunctions.Format_ISAPI(cRSS[i].CourseName)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].StartDate)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the events.
        /// </summary>
        public void RSS_Events()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Events.GetEvents();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Subject) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "event/" + cRSS[i].EventID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Subject)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].EventContent) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].EventDate)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the FAQ.
        /// </summary>
        public void RSS_FAQ()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.FAQs.GetFAQs();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Question) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "faq/" + cRSS[i].FAQID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Question)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Answer) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the forums.
        /// </summary>
        public void RSS_Forums()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.Forums.GetForumTopics();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Subject) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "forums/" + cRSS[i].TopicID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Subject)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Message) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the links.
        /// </summary>
        public void RSS_Links()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.LinkDirectory.GetLinksWebsite();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].LinkName) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(cRSS[i].LinkURL) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the news.
        /// </summary>
        public void RSS_News()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.News.GetNews();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Topic) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "news/" + cRSS[i].NewsID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Topic)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Headline) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the products.
        /// </summary>
        public void RSS_Products()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.ShoppingMall.GetShopProducts("Mod.DatePosted", "DESC");
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].ProductName) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "shopping/" + cRSS[i].ProductID + "/" + SepFunctions.Format_ISAPI(cRSS[i].ProductName)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].ShortDescription) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
            }
        }

        /// <summary>
        /// RSSs the real estate.
        /// </summary>
        public void RSS_Real_Estate()
        {
            var GetSiteName = SepFunctions.Setup(992, "WebsiteName");

            var cRSS = SepCommon.DAL.RealEstate.GetRealEstateProperties();
            if (cRSS.Count > 0)
            {
                Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                Response.Write("<rss version=\"2.0\">");
                Response.Write("<channel>");
                Response.Write("<title>" + SepFunctions.HTMLEncode(GetSiteName) + "</title>");
                Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true)) + "</link>");
                Response.Write("<description></description>");
                Response.Write("<language>en-us</language>");
                Response.Write("<copyright>" + SepFunctions.HTMLEncode("Copyright " + DateAndTime.Year(DateTime.Today) + " " + GetSiteName) + "</copyright>");
                Response.Write("<lastBuildDate>" + SepFunctions.HTMLEncode(Strings.ToString(DateTime.Now)) + "</lastBuildDate>");
                for (var i = 0; i <= 9; i++)
                {
                    if (i >= cRSS.Count)
                        break;
                    Response.Write("<item>");
                    Response.Write("<title>" + SepFunctions.HTMLEncode(cRSS[i].Title) + "</title>");
                    Response.Write("<link>" + SepFunctions.HTMLEncode(SepFunctions.GetMasterDomain(true) + "realestate/" + cRSS[i].PropertyID + "/" + SepFunctions.Format_ISAPI(cRSS[i].Title)) + "</link>");
                    Response.Write("<description>" + SepFunctions.HTMLEncode(cRSS[i].Description) + "</description>");
                    Response.Write("<author>" + SepFunctions.HTMLEncode(GetSiteName) + "</author>");
                    Response.Write("<pubDate>" + SepFunctions.HTMLEncode(Strings.ToString(cRSS[i].DatePosted)) + "</pubDate>");
                    Response.Write("</item>");
                }

                Response.Write("</channel>");
                Response.Write("</rss>");
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
            Context.Response.AddHeader("ContentType", "text/plain");

            switch (SepCommon.SepCore.Request.Item("DoAction"))
            {
                case "Articles":
                    RSS_Articles();
                    break;

                case "Auction":
                    RSS_Auctions();
                    break;

                case "Blog":
                    RSS_Blogs();
                    break;

                case "Business":
                    RSS_Business();
                    break;

                case "Classified":
                    RSS_Classifieds();
                    break;

                case "Downloads":
                    RSS_Downloads();
                    break;

                case "ELearning":
                    RSS_ELearning();
                    break;

                case "Events":
                    RSS_Events();
                    break;

                case "FAQ":
                    RSS_FAQ();
                    break;

                case "Forums":
                    RSS_Forums();
                    break;

                case "Links":
                    RSS_Links();
                    break;

                case "News":
                    RSS_News();
                    break;

                case "Products":
                    RSS_Products();
                    break;

                case "RealEstate":
                    RSS_Real_Estate();
                    break;
            }

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}