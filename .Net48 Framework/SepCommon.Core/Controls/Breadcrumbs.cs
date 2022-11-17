// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Breadcrumbs.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class Breadcrumbs.
    /// </summary>
    public class Breadcrumbs
    {
        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get => Strings.ToString(m_Text);

            set => m_Text = value;
        }

        /// <summary>
        /// Gets the category levels.
        /// </summary>
        /// <param name="intCatID">The int cat identifier.</param>
        /// <param name="iModuleID">The i module identifier.</param>
        /// <returns>System.String.</returns>
        public string GetCategoryLevels(ref string intCatID, int iModuleID)
        {
            var CatIDs = string.Empty;

            string[] arrCatID = null;

            if (!string.IsNullOrWhiteSpace(intCatID) && Strings.InStr(intCatID, ",") == 0 && iModuleID != 51)
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    arrCatID = Strings.Split(intCatID, ",");
                    intCatID = arrCatID[0];
                    using (SqlCommand cmd = new SqlCommand("SELECT ListUnder,CatID FROM Categories WHERE CatID='" + SepFunctions.toLong(intCatID) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                CatIDs = "," + SepFunctions.openNull(RS["CatID"]);
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ListUnder"])) > 0)
                                {
                                    using (SqlCommand cmd2 = new SqlCommand("SELECT ListUnder,CatID FROM Categories WHERE CatID='" + SepFunctions.toLong(SepFunctions.openNull(RS["ListUnder"])) + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                CatIDs += "," + SepFunctions.openNull(RS2["CatID"]);

                                                if (SepFunctions.toLong(SepFunctions.openNull(RS2["ListUnder"])) > 0)
                                                {
                                                    using (SqlCommand cmd3 = new SqlCommand("SELECT CatID FROM Categories WHERE CatID='" + SepFunctions.toLong(SepFunctions.openNull(RS2["ListUnder"])) + "'", conn))
                                                    {
                                                        using (SqlDataReader RS3 = cmd3.ExecuteReader())
                                                        {
                                                            if (RS3.HasRows)
                                                            {
                                                                RS3.Read();
                                                                CatIDs += "," + SepFunctions.openNull(RS3["CatID"]);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Strings.Len(CatIDs) > 0)
                {
                    return Strings.Right(CatIDs, Strings.Len(CatIDs) - 1);
                }

                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var iModuleID = 0;

            iModuleID = SepFunctions.toInt(Session.getSession("ModuleID"));

            string[] arrCatID = null;
            var BuildCatList = string.Empty;
            var sCatId = Request.Item("CatID");
            var sContentTitle = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sURL = string.Empty;
            var currentURL = Strings.LCase(Request.Path());

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                try
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString("UniqueID")) && (iModuleID == 0 || iModuleID == 999))
                    {
                        if (SepFunctions.Get_Portal_ID() == 0)
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT Linktext,UserPageName FROM ModulesNPages WHERE UniqueID=@UniqueID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", Request.QueryString("UniqueID"));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        output.AppendLine("<ol class=\"breadcrumb\">");
                                        output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                        output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.openNull(RS["LinkText"]) + "</li>");
                                        output.AppendLine("</ol>");
                                        return output.ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT Linktext,UserPageName FROM PortalPages WHERE (PortalID=@PortalID OR PortalID = -1) AND UniqueID=@UniqueID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UniqueID", Request.QueryString("UniqueID"));
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        output.AppendLine("<ol class=\"breadcrumb\">");
                                        output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                        output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.openNull(RS["LinkText"]) + "</li>");
                                        output.AppendLine("</ol>");
                                        return output.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                switch (iModuleID)
                {
                    case 2:
                        if (!string.IsNullOrWhiteSpace(Request.Item("PlanID")))
                        {
                            sContentTitle = SepFunctions.LangText("Order");
                        }

                        break;
                    case 5:
                        switch (currentURL)
                        {
                            case "/discounts_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("DiscountID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Discount");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post Discount");
                                }

                                sURL = "discounts.aspx";
                                break;
                            case "/discounts_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Discounts");
                                sURL = "discounts.aspx";
                                break;
                            case "/discounts_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Discounts");
                                sURL = "discounts.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("DiscountID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,LabelText FROM DiscountSystem WHERE DiscountID=@DiscountID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@DiscountID", Request.Item("DiscountID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["LabelText"]);
                                                sURL = "discounts.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 9:
                        switch (currentURL)
                        {
                            case "/faq_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search FAQ's");
                                sURL = "faq.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("FAQID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,Question FROM FAQ WHERE FAQID=@FAQID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@FAQID", Request.Item("FAQID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["Question"]);
                                                sURL = "faq.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 10:
                        switch (currentURL)
                        {
                            case "/downloads_upload.aspx":
                                sCatId = string.Empty;
                                sContentTitle = SepFunctions.LangText("Upload File");
                                sURL = "downloads.aspx";
                                break;
                            case "/downloads_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Discounts");
                                sURL = "downloads.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("FileID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,Field1 FROM LibrariesFiles WHERE FileID=@FileID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@FileID", Request.Item("FileID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["Field1"]);
                                                sURL = "downloads.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 12:
                        if (!string.IsNullOrWhiteSpace(Request.Item("TopicID")))
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT CatID,Subject FROM ForumsMessages WHERE TopicID=@TopicID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TopicID", Request.Item("TopicID"));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sCatId = SepFunctions.openNull(RS["CatID"]);
                                        sContentTitle = SepFunctions.openNull(RS["Subject"]);
                                        sURL = "forums.aspx";
                                    }
                                }
                            }
                        }

                        break;

                    case 14:
                        if (currentURL == "/guestbook_sign.aspx")
                        {
                            sContentTitle = SepFunctions.LangText("Sign Guestbook");
                            sURL = "guestbook.aspx";
                        }

                        break;

                    case 18:
                        switch (currentURL)
                        {
                            case "/matchmaker_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Profiles");
                                sURL = "matchmaker.aspx";
                                break;
                            case "/matchmaker_profile_modify.aspx":
                                sContentTitle = SepFunctions.LangText("Add/Edit Profile");
                                sURL = "matchmaker.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("ProfileID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT M.UserName FROM MatchMaker AS P, Members AS M WHERE P.UserID=M.UserID AND P.ProfileID=@ProfileID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ProfileID", Request.Item("ProfileID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sContentTitle = SepFunctions.openNull(RS["UserName"]);
                                                sURL = "matchmaker.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 19:
                        switch (currentURL)
                        {
                            case "/links_post.aspx":
                                sContentTitle = SepFunctions.LangText("Add Website");
                                sURL = "links.aspx";
                                break;
                            case "/links_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Websites");
                                sURL = "links.aspx";
                                break;
                        }

                        break;

                    case 20:
                        switch (currentURL)
                        {
                            case "/businesses_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("BusinessID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Business");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post Business");
                                }

                                sURL = "businesses.aspx";
                                break;
                            case "/businesses_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Businesses");
                                sURL = "businesses.aspx";
                                break;
                            case "/businesses_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Listings");
                                sURL = "businesses.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("BusinessID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,BusinessName FROM BusinessListings WHERE BusinessID=@BusinessID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@BusinessID", Request.Item("BusinessID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["BusinessName"]);
                                                sURL = "businesses.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 21:
                        sContentTitle = SepFunctions.LangText("Login");
                        break;

                    case 23:
                        if (!string.IsNullOrWhiteSpace(Request.Item("NewsID")))
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT Topic FROM News WHERE NewsID=@NewsID", conn))
                            {
                                cmd.Parameters.AddWithValue("@NewsID", Request.Item("NewsID"));
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        RS.Read();
                                        sContentTitle = SepFunctions.openNull(RS["Topic"]);
                                        sURL = "news.aspx";
                                    }
                                }
                            }
                        }

                        break;

                    case 31:
                        switch (currentURL)
                        {
                            case "/auction_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("AdID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit an Ad");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post an Ad");
                                }

                                sURL = "auction.aspx";
                                break;
                            case "/auction_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Ads");
                                sURL = "auction.aspx";
                                break;
                            case "/auction_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Auctions");
                                sURL = "auction.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("AdID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,Title FROM AuctionAds WHERE AdID=@AdID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@AdID", Request.Item("AdID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["Title"]);
                                                sURL = "auction.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 32:
                        switch (currentURL)
                        {
                            case "/realestate_property_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("PropertyID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Property");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Add Property");
                                }

                                sURL = "realestate.aspx";
                                break;
                            case "/realestate_my_properties.aspx":
                                sContentTitle = SepFunctions.LangText("My Properties");
                                sURL = "realestate.aspx";
                                break;
                            case "/realestate_tenant_modify.aspx":
                                sContentTitle = SepFunctions.LangText("My Tenants");
                                sURL = "realestate.aspx";
                                break;
                            case "/realestate_tenant_search.aspx":
                                sContentTitle = SepFunctions.LangText("My Tenants");
                                sURL = "realestate.aspx";
                                break;
                            case "/realestate_tenant_view.aspx":
                                sContentTitle = SepFunctions.LangText("My Tenants");
                                sURL = "realestate.aspx";
                                break;
                            case "/realestate_tenants.aspx":
                                sContentTitle = SepFunctions.LangText("My Tenants");
                                sURL = "realestate.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("PropertyID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT Title FROM RStateProperty WHERE PropertyID=@PropertyID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@PropertyID", Request.Item("PropertyID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sContentTitle = SepFunctions.openNull(RS["Title"]);
                                                sURL = "realestate.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 35:
                        switch (currentURL)
                        {
                            case "/articles_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("ArticleID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Article");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post Article");
                                }

                                sURL = "articles.aspx";
                                break;
                            case "/articles_manage.aspx":
                                sContentTitle = SepFunctions.LangText("Manage Articles");
                                sURL = "articles.aspx";
                                break;
                            case "/articles_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Articles");
                                sURL = "articles.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("ArticleID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,Headline FROM Articles WHERE ArticleID=@ArticleID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ArticleID", Request.Item("ArticleID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["Headline"]);
                                                sURL = "articles.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 37:
                        switch (currentURL)
                        {
                            case "/elearning_exam_take.aspx":
                                sContentTitle = SepFunctions.LangText("My Exams");
                                sURL = "elearning.aspx";
                                break;
                            case "/elearning_my_assignments.aspx":
                                sContentTitle = SepFunctions.LangText("My Assignments");
                                sURL = "elearning.aspx";
                                break;
                            case "/elearning_my_courses.aspx":
                                sContentTitle = SepFunctions.LangText("My Courses");
                                sURL = "elearning.aspx";
                                break;
                            case "/elearning_my_exams.aspx":
                                sContentTitle = SepFunctions.LangText("My Exams");
                                sURL = "elearning.aspx";
                                break;
                            case "/elearning_submit_assignment.aspx":
                                sContentTitle = SepFunctions.LangText("My Assignments");
                                sURL = "elearning.aspx";
                                break;
                            case "/elearning_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Courses");
                                sURL = "elearning.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("CourseID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,CourseName FROM ELearnCourses WHERE CourseID=@CourseID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@CourseID", Request.Item("CourseID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["CourseName"]);
                                                sURL = "elearning.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;
                    case 41:
                        switch (currentURL)
                        {
                            case "/shopping_products_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("ProductID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Product");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Add Product");
                                }

                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_my_products.aspx":
                                sContentTitle = SepFunctions.LangText("My Product");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Products");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_shipping.aspx":
                                sContentTitle = SepFunctions.LangText("Shipping Method");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_shipping_config.aspx":
                                sContentTitle = SepFunctions.LangText("Shipping Method");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_shipping_modify.aspx":
                                sContentTitle = SepFunctions.LangText("Shipping Method");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_view_store.aspx":
                                sContentTitle = SepFunctions.LangText("View Store");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_wishlist.aspx":
                                sContentTitle = SepFunctions.LangText("Wish List");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_sales.aspx":
                                sContentTitle = SepFunctions.LangText("Current Sales");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_analytics.aspx":
                                sContentTitle = SepFunctions.LangText("Analytics");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_my_orders.aspx":
                                sContentTitle = SepFunctions.LangText("My Orders");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_my_store.aspx":
                                sContentTitle = SepFunctions.LangText("My Store");
                                sURL = "shopmall.aspx";
                                break;
                            case "/shopping_order_modify.aspx":
                                sContentTitle = SepFunctions.LangText("My Orders");
                                sURL = "shopmall.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("ProductID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,ProductName FROM ShopProducts WHERE ProductID=@ProductID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", Request.Item("ProductID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["ProductName"]);
                                                sURL = "shopmall.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 44:
                        switch (currentURL)
                        {
                            case "/classifieds_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("AdID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit an Ad");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post an Ad");
                                }

                                sURL = "classifieds.aspx";
                                break;
                            case "/classifieds_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Classifieds");
                                sURL = "classifieds.aspx";
                                break;
                            case "/classifieds_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Ads");
                                sURL = "classifieds.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("AdID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,Title FROM ClassifiedsAds WHERE AdID=@AdID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@AdID", Request.Item("AdID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["Title"]);
                                                sURL = "classifieds.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 46:
                        switch (currentURL)
                        {
                            case "/events_modify.aspx":
                                if (!string.IsNullOrWhiteSpace(Request.Item("EventID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit Event");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post Event");
                                }

                                sURL = "events.aspx";
                                break;
                            case "/classifieds_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Events");
                                sURL = "events.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("EventID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT Subject FROM EventCalendar WHERE EventID=@EventID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@EventID", Request.Item("EventID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sContentTitle = SepFunctions.openNull(RS["Subject"]);
                                                sURL = "events.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 61:
                        switch (currentURL)
                        {
                            case "/blogs_modify.aspx":
                                if (!string.IsNullOrWhiteSpace(Request.Item("BlogID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit a Blog");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Add Blog");
                                }

                                sURL = "blogs.aspx";
                                break;
                            case "/blogs_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Blogs");
                                sURL = "blogs.aspx";
                                break;
                            case "/blogs_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Blogs");
                                sURL = "blogs.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("BlogID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,BlogName FROM Blog WHERE BlogID=@BlogID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@BlogID", Request.Item("BlogID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["BlogName"]);
                                                sURL = "blogs.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 63:
                        switch (currentURL)
                        {
                            case "/profiles_search.aspx":
                                sContentTitle = SepFunctions.LangText("Search Profiles");
                                sURL = "matchmaker.aspx";
                                break;
                            case "/profiles_modify.aspx":
                                sContentTitle = SepFunctions.LangText("Add/Edit Profile");
                                sURL = "profiles.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("ProfileUser")))
                                {
                                    sContentTitle = Request.Item("ProfileUser");
                                    sURL = "profiles.aspx";
                                }

                                break;
                        }

                        break;

                    case 65:
                        switch (currentURL)
                        {
                            case "/vouchers_modify.aspx":
                                sCatId = string.Empty;
                                if (!string.IsNullOrWhiteSpace(Request.Item("VoucherID")))
                                {
                                    sContentTitle = SepFunctions.LangText("Edit a Voucher");
                                }
                                else
                                {
                                    sContentTitle = SepFunctions.LangText("Post a Voucher");
                                }

                                sURL = "vouchers.aspx";
                                break;
                            case "/vouchers_manage.aspx":
                                sContentTitle = SepFunctions.LangText("My Vouchers");
                                sURL = "vouchers.aspx";
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(Request.Item("VoucherID")))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CatID,BuyTitle FROM Vouchers WHERE VoucherID=@VoucherID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@VoucherID", Request.Item("VoucherID"));
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                sCatId = SepFunctions.openNull(RS["CatID"]);
                                                sContentTitle = SepFunctions.openNull(RS["BuyTitle"]);
                                                sURL = "vouchers.aspx";
                                            }
                                        }
                                    }
                                }

                                break;
                        }

                        break;

                    case 69:
                        switch (currentURL)
                        {
                            case "/conference_schedule.aspx":
                                sCatId = string.Empty;
                                sContentTitle = SepFunctions.LangText("My Schedule");

                                sURL = "conference.aspx";
                                break;
                            case "/conference_config.aspx":
                                sContentTitle = SepFunctions.LangText("Configuration");
                                sURL = "conference.aspx";
                                break;
                            case "/conference_meeting.aspx":
                                sContentTitle = SepFunctions.LangText("Create Meeting");
                                sURL = "conference.aspx";
                                break;
                            default:
                                sURL = "conference.aspx";
                                break;
                        }

                        break;
                }

                if (currentURL == "/refer.aspx" && iModuleID > 0 && iModuleID != 43)
                {
                    sContentTitle = SepFunctions.LangText("Refer a Friend");
                    sURL = "refer.aspx";
                }

                if (!string.IsNullOrWhiteSpace(sCatId))
                {
                    arrCatID = Strings.Split(GetCategoryLevels(ref sCatId, iModuleID), ",");
                    if (arrCatID != null)
                    {
                        if (iModuleID == 12)
                        {
                            for (var i = Information.LBound(arrCatID) + 1; i <= Information.UBound(arrCatID); i++)
                            {
                                try
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CategoryName,CatID FROM Categories WHERE CatID=@CatID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@CatID", arrCatID[i]);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                if (i == Information.UBound(arrCatID) && string.IsNullOrWhiteSpace(sContentTitle))
                                                {
                                                    BuildCatList += "<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.openNull(RS["CategoryName"]) + "</li>";
                                                }
                                                else
                                                {
                                                    BuildCatList += "<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "cat" + iModuleID + "/" + SepFunctions.openNull(RS["CatID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["CategoryName"])) + "\">" + SepFunctions.openNull(RS["CategoryName"]) + "</a></li>";
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    // Catch any Errors
                                }
                            }
                        }
                        else
                        {
                            for (var i = Information.LBound(arrCatID); i <= Information.UBound(arrCatID); i++)
                            {
                                try
                                {
                                    using (SqlCommand cmd = new SqlCommand("SELECT CategoryName,CatID FROM Categories WHERE CatID=@CatID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@CatID", arrCatID[Information.UBound(arrCatID) - i]);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                if (i == Information.UBound(arrCatID) && string.IsNullOrWhiteSpace(sContentTitle))
                                                {
                                                    BuildCatList += "<li class=\"breadcrumb-item actve\" aria-current=\"page\">" + SepFunctions.openNull(RS["CategoryName"] + "</li>");
                                                }
                                                else
                                                {
                                                    BuildCatList += "<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "cat" + iModuleID + "/" + SepFunctions.openNull(RS["CatID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["CategoryName"])) + "\">" + SepFunctions.openNull(RS["CategoryName"]) + "</a></li>";
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    // Catch any Errors
                                }
                            }
                        }
                    }
                }

                if (iModuleID != 16)
                {
                    output.AppendLine("<ol class=\"breadcrumb\">");
                    if (iModuleID > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT Linktext,UserPageName FROM ModulesNPages WHERE ModuleID=@ModuleID", conn))
                        {
                            cmd.Parameters.AddWithValue("@ModuleID", iModuleID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (!RS.HasRows)
                                {
                                    if (!string.IsNullOrWhiteSpace(sContentTitle))
                                    {
                                        output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                        if (!string.IsNullOrWhiteSpace(Text))
                                        {
                                            var href1 = !string.IsNullOrWhiteSpace(sURL) ? "<a href=\"" + sInstallFolder + sURL + "\">" : string.Empty;
                                            var href2 = !string.IsNullOrWhiteSpace(sURL) ? "</a>" : string.Empty;

                                            output.AppendLine("<li class=\"breadcrumb-item\">" + href1 + sContentTitle + href2 + "</li>");
                                            output.Append(Text);
                                        }
                                        else
                                        {
                                            output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + sContentTitle + "</li>");
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(Text))
                                        {
                                            output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                            output.Append(Text);
                                        }
                                        else
                                        {
                                            output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.LangText("Home") + "</li>");
                                        }
                                    }
                                }
                                else
                                {
                                    RS.Read();
                                    if (SepFunctions.toLong(sCatId) > 0)
                                    {
                                        var activeLI = !string.IsNullOrWhiteSpace(sContentTitle) ? "<li class=\"breadcrumb-item active\" aria-current=\"page\">" + sContentTitle + "</li>" : string.Empty;
                                        output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                        output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + SepFunctions.openNull(RS["UserPageName"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>");
                                        output.Append(BuildCatList + activeLI);
                                    }
                                    else
                                    {
                                        if (iModuleID == 33)
                                        {
                                            sURL = "account.aspx";
                                        }
                                        else
                                        {
                                            sURL = SepFunctions.openNull(RS["UserPageName"]);
                                        }

                                        if (!string.IsNullOrWhiteSpace(sContentTitle))
                                        {
                                            output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                            output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + string.Empty + SepFunctions.openNull(RS["UserPageName"]) + "\">" + SepFunctions.openNull(RS["LinkText"]) + "</a></li>");
                                            if (!string.IsNullOrWhiteSpace(Text))
                                            {
                                                var href1 = !string.IsNullOrWhiteSpace(sURL) ? "<a href=\"" + sInstallFolder + sURL + "\">" : string.Empty;
                                                var href2 = !string.IsNullOrWhiteSpace(sURL) ? "</a>" : string.Empty;
                                                output.AppendLine("<li class=\"breadcrumb-item\">" + href1 + sContentTitle + href2 + "</li>");
                                                output.Append(Text);
                                            }
                                            else
                                            {
                                                output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + sContentTitle + "</li>");
                                            }
                                        }
                                        else
                                        {
                                            output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>");
                                            if (!string.IsNullOrWhiteSpace(Text))
                                            {
                                                var href1 = !string.IsNullOrWhiteSpace(sURL) ? "<a href=\"" + sInstallFolder + sURL + "\">" : string.Empty;
                                                var href2 = !string.IsNullOrWhiteSpace(sURL) ? "</a>" : string.Empty;
                                                output.AppendLine("<li class=\"breadcrumb-item\">" + href1 + SepFunctions.openNull(RS["LinkText"]) + href2 + "</li>");
                                                output.Append(Text);
                                            }
                                            else
                                            {
                                                output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.openNull(RS["LinkText"]) + "</li>");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(sContentTitle))
                        {
                            output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>" + sContentTitle + Text);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(Text))
                            {
                                output.AppendLine("<li class=\"breadcrumb-item\"><a href=\"" + sInstallFolder + "default.aspx\">" + SepFunctions.LangText("Home") + "</a></li>" + Text);
                            }
                            else
                            {
                                output.AppendLine("<li class=\"breadcrumb-item active\" aria-current=\"page\">" + SepFunctions.LangText("Home") + "</li>");
                            }
                        }
                    }

                    output.AppendLine("</ol>");
                }
            }

            return output.ToString();
        }
    }
}