// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Replace.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server
{
    using Models;
    using Newtonsoft.Json;
    using SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Customs the fields shop cart.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Custom_Fields_ShopCart()
        {
            var str = new StringBuilder();
            var strFieldValue = string.Empty;
            string[] arrValue = null;

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT PollID FROM PNQQuestions WHERE CONVERT(varchar, StartDate, 126) <= CONVERT(varchar, GetDate(),126) AND CONVERT(varchar, EndDate, 126) >= CONVERT(varchar, GetDate(),126) AND PollID IN (SELECT UniqueID FROM Associations WHERE ModuleID='25' AND (PortalID='" + Get_Portal_ID() + "' OR PortalID = -1) AND UniqueID=PollID)", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            str.Append("<div style=\"margin-left:30px;\">");
                            while (RS.Read())
                            {
                                strFieldValue = openNull(RS["FieldValue"]);
                                str.Append(openNull(RS["FieldName"]) + ": ");
                                if (Strings.InStr(strFieldValue, "||") > 0)
                                {
                                    arrValue = Strings.Split(strFieldValue, "||");
                                    Array.Resize(ref arrValue, 2);
                                    str.Append(arrValue[0] + " " + LangText("(Cost: ~~" + Format_Currency(toDouble(arrValue[1])) + "~~)") + "<br/>");
                                }
                                else
                                {
                                    str.Append(strFieldValue + "<br/>");
                                }
                            }

                            str.Append("</div>");
                        }
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Data table to HTML table.
        /// </summary>
        /// <param name="tableCaption">The table caption.</param>
        /// <param name="tableId">Identifier for the table.</param>
        /// <param name="inTable">The in table.</param>
        /// <param name="showHeaders">(Optional) True to show, false to hide the headers.</param>
        /// <returns>A string.</returns>
        public static string DataTableToHTMLTable(string tableCaption, string tableId, DataTable inTable, bool showHeaders = true)
        {
            var dString = new StringBuilder();

            dString.Append("<div>" + Environment.NewLine);
            dString.Append("<table class=\"GridViewStyle\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"" + tableId + "\" style=\"border-collapse:collapse;\">" + Environment.NewLine);
            if (!string.IsNullOrWhiteSpace(tableCaption))
            {
                dString.Append("<caption>" + tableCaption + "</caption>" + Environment.NewLine);
            }

            if (showHeaders)
            {
                dString.Append(GetHeader(inTable));
            }

            dString.Append(GetBody(inTable));
            dString.Append("</table>" + Environment.NewLine);
            dString.Append("</div>" + Environment.NewLine);
            dString.Append("<script type=\"text/javascript\">restyleGridView('#" + tableId + "');</script>");

            return Strings.ToString(dString);
        }

        /// <summary>
        /// Format price.
        /// </summary>
        /// <param name="sSalePrice">The sale price.</param>
        /// <param name="sUnitPrice">The unit price.</param>
        /// <param name="sRecurringPrice">The recurring price.</param>
        /// <param name="sRecurringCycle">The recurring cycle.</param>
        /// <returns>The formatted price.</returns>
        public static string Format_Price(string sSalePrice, string sUnitPrice, string sRecurringPrice, string sRecurringCycle)
        {
            var str = string.Empty;

            if (toDecimal(sSalePrice) > 0)
            {
                str = Pricing_Long_Price(toDecimal(sSalePrice), toDecimal(sRecurringPrice), sRecurringCycle);
            }
            else
            {
                str = Pricing_Long_Price(toDecimal(sUnitPrice), toDecimal(sRecurringPrice), sRecurringCycle);
            }

            return str;
        }

        /// <summary>
        /// Formats the sale price.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sRecurringPrice">The s recurring price.</param>
        /// <param name="sRecurringCycle">The s recurring cycle.</param>
        /// <returns>System.String.</returns>
        public static string Format_Sale_Price(string sSalePrice, string sRecurringPrice, string sRecurringCycle)
        {
            return Pricing_Long_Price(toDecimal(sSalePrice), toDecimal(sRecurringPrice), sRecurringCycle);
        }

        /// <summary>
        /// Formats the discount price.
        /// </summary>
        /// <param name="PriceType">Type of the price.</param>
        /// <param name="PriceOff">The price off.</param>
        /// <returns>System.String.</returns>
        public static string FormatDiscountPrice(string PriceType, string PriceOff)
        {
            var GetPrice = string.Empty;

            switch (toLong(PriceType))
            {
                case 0:
                    GetPrice = Format_Currency(PriceOff) + " " + LangText("Off");
                    break;

                case 1:
                    GetPrice = PriceOff + "%";
                    break;

                default:
                    GetPrice = Format_Currency(PriceOff);
                    break;
            }

            return GetPrice;
        }

        /// <summary>
        /// Gets sale percentage.
        /// </summary>
        /// <param name="sSalePrice">The sale price.</param>
        /// <param name="sUnitPrice">The unit price.</param>
        /// <param name="sRecurringPrice">The recurring price.</param>
        /// <returns>The sale percentage.</returns>
        public static string Get_Sale_Percentage(string sSalePrice, string sUnitPrice, string sRecurringPrice)
        {
            double sSavePercent = 0;
            decimal sSavePrice = 0;

            if (toDecimal(sSalePrice) < toDecimal(sUnitPrice) && toDecimal(sSalePrice) > 0)
            {
                sSavePrice = toDecimal(sUnitPrice) - toDecimal(sSalePrice);
                sSavePercent = Format_Double(Strings.FormatNumber(100 - toDecimal(sSalePrice) / toDecimal(sUnitPrice) * 100, 0));
                var sSetupSave = toDouble(sRecurringPrice) > 0 ? " " + LangText("on the setup") : string.Empty;
                return "(-" + Format_Currency(sSavePrice) + "), " + LangText("save") + " " + sSavePercent + "%" + sSetupSave;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the continue URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetContinueURL()
        {
            return UrlEncode(Request.RawUrl() + Request.Url.Query());
        }

        /// <summary>
        /// Orders the product.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="StoreID">The store identifier.</param>
        /// <returns>System.String.</returns>
        public static string orderProduct(string ProductID, string StoreID)
        {
            return "orderProduct('" + ProductID + "', '" + StoreID + "');return false;";
        }

        /// <summary>
        /// Replace fields.
        /// </summary>
        /// <param name="sText">The text.</param>
        /// <param name="UserId">Identifier for the user.</param>
        /// <param name="sUniqueID">(Optional) Unique identifier.</param>
        /// <param name="bBoolean">(Optional) True to boolean.</param>
        /// <returns>A string.</returns>
        public static string Replace_Fields(string sText, string UserId, string sUniqueID = "", bool bBoolean = false)
        {
            var invoiceStr = new StringBuilder();

            double iProdGrandTotal = 0;
            double iProdTotalPrice = 0;

            if (!string.IsNullOrWhiteSpace(Request.Item("AffiliateID")))
            {
                if (Strings.InStr(sText, "[[AffiliateID]]") > 0)
                {
                    sText = Strings.Replace(sText, "[[AffiliateID]]", Request.Item("AffiliateID"));
                }
            }

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Members WHERE UserID='" + FixWord(UserId) + "'", conn))
                {
                    using (SqlDataReader UserRS = cmd.ExecuteReader())
                    {
                        if (UserRS.HasRows)
                        {
                            while (UserRS.Read())
                            {
                                for (var i = 0; i <= UserRS.FieldCount - 1; i++)
                                {
                                    if (Strings.InStr(sText, "[[Members." + openNull(UserRS[i]) + "]]") > 0)
                                    {
                                        sText = Strings.Replace(sText, "[[Members." + openNull(UserRS[i]) + "]]", openNull(UserRS[i]));
                                    }
                                }
                            }

                            if (Strings.InStr(sText, "[[FirstName]]") > 0)
                            {
                                sText = Strings.Replace(sText, "[[FirstName]]", openNull(UserRS["FirstName"]));
                            }

                            if (Strings.InStr(sText, "[[LastName]]") > 0)
                            {
                                sText = Strings.Replace(sText, "[[LastName]]", openNull(UserRS["LastName"]));
                            }

                            if (Strings.InStr(sText, "[[AffiliateID]]") > 0)
                            {
                                sText = Strings.Replace(sText, "[[AffiliateID]]", openNull(UserRS["AffiliateID"]));
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(sUniqueID))
                {
                    if (Strings.InStr(sText, "[[Voucher.Print]]") > 0)
                    {
                        if (bBoolean)
                        {
                            sText = Strings.Replace(sText, "[[Voucher.Print]]", Vouchers_Print(sUniqueID));
                        }
                        else
                        {
                            sText = Strings.Replace(sText, "[[Voucher.Print]]", "<a href=\"" + GetSiteDomain() + "vouchers.aspx?DoAction=Print&CartID=" + UrlEncode(sUniqueID) + "\">" + LangText("Click here to view your voucher") + "</a>");
                        }
                    }
                }

                // Display Invoices
                if (Strings.InStr(sText, "[[Invoices.Display_Invoice]]") > 0 && !string.IsNullOrWhiteSpace(Session.getSession(Strings.Left(Setup(992, "WebSiteName"), 5) + "InvoiceID")))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Members WHERE UserID='" + FixWord(UserId) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                invoiceStr.Append("<table class=\"FieldsetBox\" width=\"500\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                                invoiceStr.Append("<tr class=\"FieldsetBoxTitle\">");
                                invoiceStr.Append("<td width=\"290\">" + LangText("Product Name") + "</td>");
                                invoiceStr.Append("<td width=\"80\" align=\"center\">" + LangText("Unit Price") + "</td>");
                                invoiceStr.Append("<td width=\"50\" align=\"center\">" + LangText("Qty") + "</td>");
                                invoiceStr.Append("<td width=\"80\" align=\"center\">" + LangText("Total") + "</td>");
                                invoiceStr.Append("</tr>");
                                while (RS.Read())
                                {
                                    iProdTotalPrice = (toDouble(openNull(RS["UnitPrice"])) + toLong(openNull(RS["RecurringPrice"]))) * toLong(openNull(RS["Quantity"]));
                                    invoiceStr.Append("<input type=\"hidden\" name=\"subprice" + openNull(RS["InvoiceProductID"]) + "\" value=\"" + Strings.ToString(toDouble(openNull(RS["UnitPrice"])) + toLong(openNull(RS["RecurringPrice"]))) + "\">");
                                    invoiceStr.Append("<tr id=\"Product" + openNull(RS["ProductID"]) + "\">");
                                    invoiceStr.Append("<td valign=\"top\">");
                                    invoiceStr.Append(openNull(RS["ProductName"]));
                                    if (!string.IsNullOrWhiteSpace(openNull(RS["ProductID"])))
                                    {
                                        invoiceStr.Append("<br/>");
                                        invoiceStr.Append(Custom_Fields_ShopCart());
                                    }

                                    invoiceStr.Append("</td>");
                                    invoiceStr.Append("<td align=\"center\" valign=\"top\">" + Format_Currency((toDouble(openNull(RS["UnitPrice"])) + toLong(openNull(RS["RecurringPrice"]))).ToString()) + "</td>");
                                    invoiceStr.Append("<td align=\"center\" valign=\"top\">" + openNull(RS["Quantity"]) + "</td>");
                                    invoiceStr.Append("<td align=\"center\" valign=\"top\" id=\"itemprice" + openNull(RS["InvoiceProductID"]) + "\">" + Format_Currency(Strings.ToString(iProdTotalPrice)) + "</td>");
                                    invoiceStr.Append("</tr>");

                                    using (var cmd2 = new SqlCommand("SELECT FieldValue FROM CustomFieldUsers WHERE UniqueID='" + FixWord(Request.Item("InvoiceID")) + "' AND ModuleID='41' AND PortalID='" + Get_Portal_ID() + "'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            while (RS2.Read())
                                            {
                                                invoiceStr.Append("<tr id=\"Product" + openNull(RS["ProductID"]) + "2\">");
                                                invoiceStr.Append("<td valign=\"top\" colspan=\"4\"><span style=\"padding-left:20px\">" + openNull(RS2["FieldValue"]) + "</span></td>");
                                                invoiceStr.Append("</tr>");
                                            }
                                        }
                                    }

                                    iProdGrandTotal = iProdGrandTotal + iProdTotalPrice;
                                }

                                invoiceStr.Append("<tr>");
                                invoiceStr.Append("<td colspan=\"4\"><div id=\"NewProductList\" style=\"width:500px\"></div></td>");
                                invoiceStr.Append("</tr><tr class=\"FieldsetBoxTitle\">");
                                invoiceStr.Append("<td colspan=\"3\" align=\"right\">" + LangText("Grand Total:") + "</td>");
                                invoiceStr.Append("<td align=\"center\" id=\"idGrandTotal\">" + Format_Currency(Strings.ToString(iProdGrandTotal)) + "</td>");
                                invoiceStr.Append("</tr>");
                                invoiceStr.Append("</table>");
                            }
                        }
                        sText = Strings.Replace(sText, "[[Invoices.Display_Invoice]]", Strings.ToString(invoiceStr));
                    }
                }
            }

            return sText;
        }

        /// <summary>
        /// Replace widgets.
        /// </summary>
        /// <param name="strPageText">The page text.</param>
        /// <param name="ModuleID">Identifier for the module.</param>
        /// <param name="isLatestNews">(Optional) True if is latest news, false if not.</param>
        /// <returns>A string.</returns>
        public static string Replace_Widgets(string strPageText, long ModuleID, bool isLatestNews = false)
        {
            var posa = 0;
            var strNum = string.Empty;

            var sInstallFolder = GetInstallFolder();
            var didReplace = false;

            var sUserID = string.Empty;

            if (ModuleID == 7)
            {
                sUserID = GetUserID(Request.Item("UserName"));
            }

            if (Strings.InStr(strPageText, "[") == 0 && Strings.InStr(strPageText, "]") == 0)
            {
                return strPageText;
            }

            if (Strings.InStr(strPageText, "[[ProfilePic]]") > 0)
            {
                strPageText = Strings.Replace(strPageText, "[[ProfilePic]]", "<img src=\"" + userProfileImage(Session_User_ID()) + "\" border=\"0\" alt=\"My Profile Picture\">");
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestArticles||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestArticles||") + Strings.Len("[[DisplayNewestArticles||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cArticles = DAL.Articles.GetArticles(CategoryId: -1, showAvailable: true, UserID: isUserPage() && Setup(7, "UPagesTop10") == "Yes" ? sUserID : string.Empty);
                var countArticles = 0;
                var sColumn = string.Empty;
                var gridArticles = new DataTable();
                gridArticles.Columns.Add("Article", typeof(string));
                for (var i = 0; i <= cArticles.Count - 1; i++)
                {
                    didReplace = true;
                    var rowArticles = gridArticles.NewRow();
                    var SumDots = Strings.Len(cArticles[i].Summary) > 250 ? "..." : string.Empty;

                    sColumn = "<div class=\"article-bx\">";
                    sColumn += "  <div class=\"row\">";
                    sColumn += "    <div class=\"col-md-4 no-padding\">";
                    sColumn += "      <div class=\"article-img\">";
                    sColumn += "        <img src=\"" + cArticles[i].DefaultPicture + "\" class=\"img-fluid\" border=\"0\" alt='" + cArticles[i].Headline + "' />";
                    sColumn += "      </div>";
                    sColumn += "    </div>";
                    sColumn += "    <div class=\"col-md-8\">";
                    sColumn += "      <div class=\"article-content-area\">";
                    sColumn += "        <div class=\"article-top-content\">";
                    sColumn += "          <h3><a href=\"" + sInstallFolder + "article/" + cArticles[i].ArticleID + "/" + Format_ISAPI(cArticles[i].Headline) + "/\">" + cArticles[i].Headline + "</a></h3>";
                    sColumn += "          <p>" + Strings.Left(cArticles[i].Summary, 250) + SumDots + "</p>";
                    sColumn += "        </div>";
                    sColumn += "        <div class=\"article-btn-group\">";
                    sColumn += "          <p><i class=\"fa fa-user-o\"></i> Posted By: <strong><a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + cArticles[i].UserID + "\">" + cArticles[i].Author + "</a></strong></p>";
                    sColumn += "          <p><span>Date :</span> " + cArticles[i].Headline_Date + "</p>";
                    sColumn += "          <a href=\"" + sInstallFolder + "article/" + cArticles[i].ArticleID + "/" + Format_ISAPI(cArticles[i].Headline) + "/\" class=\"btn btn-primary\">Read More</a>";
                    sColumn += "        </div>";
                    sColumn += "      </div>";
                    sColumn += "    </div>";
                    sColumn += "  </div>";
                    sColumn += "</div>";

                    rowArticles["Article"] = sColumn;
                    gridArticles.Rows.Add(rowArticles);

                    countArticles += 1;
                    if (countArticles == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestArticles||" + strNum + "]]", DataTableToHTMLTable("Newest Articles", "newArticles", gridArticles, false));
                gridArticles.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestArticles||" + strNum + "|Simple]]") > 0)
                {
                    var simpleArticles = string.Empty;
                    countArticles = 0;
                    for (var i = 0; i <= cArticles.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleArticles += "<a href=\"" + sInstallFolder + "article/" + cArticles[i].ArticleID + "/" + Format_ISAPI(cArticles[i].Headline) + "/\">" + Strings.FormatDateTime(cArticles[i].Headline_Date, Strings.DateNamedFormat.ShortDate) + ": " + cArticles[i].Headline + "</a><br/>";
                        countArticles += 1;
                        if (countArticles == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestArticles||" + strNum + "|Simple]]", simpleArticles);
                }

                // Slider Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestArticles||" + strNum + "|Slider]]") > 0)
                {
                    var sliderArticles = string.Empty;
                    countArticles = 0;
                    sliderArticles += "<div class=\"product-part\"><ul class=\"product-slider\">";
                    for (var i = 0; i <= cArticles.Count - 1; i++)
                    {
                        didReplace = true;
                        sliderArticles += "<li>" + "<div class=\"pro-img\">" + "<img src=\"" + cArticles[i].DefaultPicture + "\" class=\"img-fluid\" />" + "</div>" + "<div class=\"pro-detail\">" + "<h5>" + LangText("Date Posted") + " : " + Strings.FormatDateTime(cArticles[i].Start_Date, Strings.DateNamedFormat.ShortDate) + "</h5>" + "<h4><a href=\"" + sInstallFolder + "article/" + cArticles[i].ArticleID + "/" + Format_ISAPI(cArticles[i].Headline) + "/\">" + cArticles[i].Headline + "</a></h4>" + "<p>" + Strings.Left(cArticles[i].Summary, 70) + "</p>" + "</div>" + "</li>";
                        countArticles += 1;
                        if (countArticles == toInt(strNum))
                        {
                            break;
                        }
                    }

                    sliderArticles += "</ul></div>";

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestArticles||" + strNum + "|Slider]]", sliderArticles);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestAuctions||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestAuctions||") + Strings.Len("[[DisplayNewestAuctions||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cAuctions = DAL.Auctions.GetAuctionAds(userId: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countAuctions = 0;
                var gridAuctions = new DataTable();
                gridAuctions.Columns.Add("Thumbnail", typeof(string));
                gridAuctions.Columns.Add("Title", typeof(string));
                gridAuctions.Columns.Add("Current Bid", typeof(string));
                for (var i = 0; i <= cAuctions.Count - 1; i++)
                {
                    didReplace = true;
                    var rowAuctions = gridAuctions.NewRow();
                    rowAuctions["Thumbnail"] = "<a href=\"" + sInstallFolder + "auction/" + cAuctions[i].AdID + "/" + Format_ISAPI(cAuctions[i].Title) + "/\">" + "<img src=\"" + cAuctions[i].DefaultPicture + "\" border=\"0\" alt\"" + cAuctions[i].Title + "\" />" + "</a>";
                    rowAuctions["Title"] = "<a href=\"" + sInstallFolder + "auction/" + cAuctions[i].AdID + "/" + Format_ISAPI(cAuctions[i].Title) + "/\">" + cAuctions[i].Title + "</a>";
                    rowAuctions["Current Bid"] = "<a href=\"" + sInstallFolder + "auction/" + cAuctions[i].AdID + "/" + Format_ISAPI(cAuctions[i].Title) + "/\">" + cAuctions[i].CurrentBid + "</a>";
                    gridAuctions.Rows.Add(rowAuctions);
                    countAuctions += 1;
                    if (countAuctions == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestAuctions||" + strNum + "]]", DataTableToHTMLTable("Newest Auctions", "newAuctions", gridAuctions));
                gridAuctions.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestAuctions||" + strNum + "|Simple]]") > 0)
                {
                    var simpleAuctions = string.Empty;
                    countAuctions = 0;
                    for (var i = 0; i <= cAuctions.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleAuctions += "<a href=\"" + sInstallFolder + "auction/" + cAuctions[i].AdID + "/" + Format_ISAPI(cAuctions[i].Title) + "/\">" + Strings.FormatDateTime(cAuctions[i].EndDate, Strings.DateNamedFormat.ShortDate) + ": " + cAuctions[i].Title + "</a><br/>";
                        countAuctions += 1;
                        if (countAuctions == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestAuctions||" + strNum + "|Simple]]", simpleAuctions);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestBlogs||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestBlogs||") + Strings.Len("[[DisplayNewestBlogs||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cBlogs = DAL.Blogs.GetBlogs(showAvailable: true, UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countBlogs = 0;
                var gridBlogs = string.Empty;
                for (var i = 0; i <= cBlogs.Count - 1; i++)
                {
                    didReplace = true;
                    gridBlogs += "<a href=\"" + sInstallFolder + "blogs/" + cBlogs[i].BlogID + "/" + Format_ISAPI(cBlogs[i].BlogName) + "/\">" + cBlogs[i].BlogName + "</a><br />";
                    gridBlogs += "Posted " + Time_Long_Difference(toDate(Strings.FormatDateTime(cBlogs[i].DatePosted, Strings.DateNamedFormat.ShortDate))) + " ago by " + cBlogs[i].Username + "<br/>";
                    gridBlogs += cBlogs[i].Description + "<br/><hr/>";
                    countBlogs += 1;
                    if (countBlogs == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestBlogs||" + strNum + "]]", "Newest Blogs<br/>" + gridBlogs);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestBlogs||" + strNum + "|Simple]]") > 0)
                {
                    var simpleBlogs = string.Empty;
                    countBlogs = 0;
                    for (var i = 0; i <= cBlogs.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleBlogs += "<a href=\"" + sInstallFolder + "blogs/" + cBlogs[i].BlogID + "/" + Format_ISAPI(cBlogs[i].BlogName) + "/\">" + Strings.FormatDateTime(cBlogs[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cBlogs[i].BlogName + "</a><br/>";
                        countBlogs += 1;
                        if (countBlogs == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestBlogs||" + strNum + "|Simple]]", simpleBlogs);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestBusiness||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestBusiness||") + Strings.Len("[[DisplayNewestBusiness||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cBusinesses = DAL.Businesses.GetBusinesses(userId: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countBusinesses = 0;
                var gridBusinesses = string.Empty;
                for (var i = 0; i <= cBusinesses.Count - 1; i++)
                {
                    didReplace = true;
                    gridBusinesses += "<div class=\"property-bx\" style=\"padding:5px;\">";
                    gridBusinesses += "<span style=\"display:inline-block;float:left;margin-left:15px;\"><strong>" + cBusinesses[i].BusinessName + "</strong>" + "</span><span class=\"badge badge-info\" style=\"display:inline-block;float:right;margin-right:15px;\">" + "Hits: <span class=\"badge badge-light\">" + cBusinesses[i].Visits + "</span></span>";
                    gridBusinesses += "<div style=\"clear:both;\"></div>";
                    gridBusinesses += "<div class=\"property-content\">";
                    gridBusinesses += "<p>" + cBusinesses[i].Description + "</p>";
                    gridBusinesses += "<p>Member Comments (" + cBusinesses[i].TotalComments + ")</p>";
                    gridBusinesses += "<div class=\"text-right\">";
                    gridBusinesses += "<a class=\"btn btn-primary\" href=\"" + sInstallFolder + "business/" + cBusinesses[i].BusinessID + "/" + Format_ISAPI(cBusinesses[i].BusinessName) + "/\">" + "Details</a>";
                    gridBusinesses += "<a class=\"btn btn-secondary\" href=\"" + sInstallFolder + "refer.aspx?URL=" + UrlEncode(sInstallFolder + "business/" + cBusinesses[i].BusinessID + "/" + Format_ISAPI(cBusinesses[i].BusinessName) + "/") + "\">" + "Refer</a>";
                    gridBusinesses += "</div>";
                    gridBusinesses += "</div>";
                    gridBusinesses += "</div>";
                    countBusinesses += 1;
                    if (countBusinesses == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestBusiness||" + strNum + "]]", "Newest Businesses<br/>" + gridBusinesses);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestBusiness||" + strNum + "|Simple]]") > 0)
                {
                    var simpleBusinesses = string.Empty;
                    countBusinesses = 0;
                    for (var i = 0; i <= cBusinesses.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleBusinesses += "<a href=\"" + sInstallFolder + "business/" + cBusinesses[i].BusinessID + "/" + Format_ISAPI(cBusinesses[i].BusinessName) + "/\">" + Strings.FormatDateTime(cBusinesses[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cBusinesses[i].BusinessName + "</a><br/>";
                        countBusinesses += 1;
                        if (countBusinesses == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestBusiness||" + strNum + "|Simple]]", simpleBusinesses);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestClassifieds||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestClassifieds||") + Strings.Len("[[DisplayNewestClassifieds||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cClassifieds = DAL.Classifieds.GetClassifiedAds(availableItems: true, userId: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countClassifieds = 0;
                var gridClassifieds = new DataTable();
                gridClassifieds.Columns.Add("Thumbnail", typeof(string));
                gridClassifieds.Columns.Add("Title", typeof(string));
                gridClassifieds.Columns.Add("Price", typeof(string));
                for (var i = 0; i <= cClassifieds.Count - 1; i++)
                {
                    didReplace = true;
                    var rowClassifieds = gridClassifieds.NewRow();
                    rowClassifieds["Thumbnail"] = "<a href=\"" + sInstallFolder + "classified/" + cClassifieds[i].AdID + "/" + Format_ISAPI(cClassifieds[i].Title) + "/\"><img src='" + cClassifieds[i].DefaultPicture + "' border=\"0\" alt='" + cClassifieds[i].Title + "' /></a>";
                    rowClassifieds["Title"] = "<div style=\"width: 100%;\"><a href=\"" + sInstallFolder + "classified/" + cClassifieds[i].AdID + "/" + Format_ISAPI(cClassifieds[i].Title) + "/\">" + cClassifieds[i].Title + "</a></div>";
                    rowClassifieds["Price"] = "<a href=\"" + sInstallFolder + "classified/" + cClassifieds[i].AdID + "/" + Format_ISAPI(cClassifieds[i].Title) + "/\">" + Format_Currency(cClassifieds[i].Price) + "</a>";
                    gridClassifieds.Rows.Add(rowClassifieds);
                    countClassifieds += 1;
                    if (countClassifieds == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestClassifieds||" + strNum + "]]", DataTableToHTMLTable("Newest Classifieds", "newClassifieds", gridClassifieds));
                gridClassifieds.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestClassifieds||" + strNum + "|Simple]]") > 0)
                {
                    var simpleClassifieds = string.Empty;
                    countClassifieds = 0;
                    for (var i = 0; i <= cClassifieds.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleClassifieds += "<a href=\"" + sInstallFolder + "classified/" + cClassifieds[i].AdID + "/" + Format_ISAPI(cClassifieds[i].Title) + "/\">" + Strings.FormatDateTime(cClassifieds[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cClassifieds[i].Title + "</a><br/>";
                        countClassifieds += 1;
                        if (countClassifieds == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestClassifieds||" + strNum + "|Simple]]", simpleClassifieds);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestDiscounts||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestDiscounts||") + Strings.Len("[[DisplayNewestDiscounts||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cDiscounts = DAL.Discounts.GetDiscounts(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countDiscounts = 0;
                var gridDiscounts = string.Empty;
                gridDiscounts += "<script type=\"text/javascript\">" + Environment.NewLine;
                gridDiscounts += "	function Print(a) {" + Environment.NewLine;
                gridDiscounts += "		var row = $(a).closest(\"div.discount-card\").clone(true);" + Environment.NewLine;
                gridDiscounts += "		var printWin = window.open('', '', 'left=0\", \",top=0,width=1000,height=600,status=0');" + Environment.NewLine;
                gridDiscounts += "		$(\".PrintCoupon\", row).remove();" + Environment.NewLine;
                gridDiscounts += "		var sStyle = '<link rel=\"stylesheet\" href=\"" + sInstallFolder + "skins/public/styles/public.css\" type=\"text/css\" />';" + Environment.NewLine;
                gridDiscounts += "		var printBut = '<p align=\"center\"><input type=\"button\" value=\"Print\" onclick=\"print()\" /></p>';" + Environment.NewLine;
                gridDiscounts += "		var dv = $(\"<div />\");" + Environment.NewLine;
                gridDiscounts += "		dv.append(sStyle);" + Environment.NewLine;
                gridDiscounts += "		dv.append(row);" + Environment.NewLine;
                gridDiscounts += "		dv.append(printBut);" + Environment.NewLine;
                gridDiscounts += "		printWin.document.write(dv.html());" + Environment.NewLine;
                gridDiscounts += "		printWin.document.close();" + Environment.NewLine;
                gridDiscounts += "		printWin.focus();" + Environment.NewLine;
                gridDiscounts += "	}" + Environment.NewLine;
                gridDiscounts += "</script>" + Environment.NewLine;
                for (var i = 0; i <= cDiscounts.Count - 1; i++)
                {
                    didReplace = true;
                    gridDiscounts += "<div class=\"discount-card\">";
                    gridDiscounts += "   <div class=\"discount-card-body\">";
                    gridDiscounts += "	  <div class=\"table-responsive\">";
                    gridDiscounts += "		 <table class=\"DiscountTable\" width=\"500\" height=\"231\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                    gridDiscounts += "			<tbody>";
                    gridDiscounts += "			   <tr>";
                    gridDiscounts += "				  <td valign=\"top\">";
                    gridDiscounts += "					 <br>";
                    gridDiscounts += "					 <table align=\"center\" height=\"205\" cellpadding=\"0\" cellspacing=\"0\" width=\"95%\">";
                    gridDiscounts += "						<tbody>";
                    gridDiscounts += "						   <tr class=\"TableHeader\">";
                    gridDiscounts += "							  <td colspan=\"2\" align=\"center\" valign=\"top\">";
                    gridDiscounts += "								 <table cellpadding=\"0\" cellspacing=\"0\" width=\"98%\">";
                    gridDiscounts += "									<tbody>";
                    gridDiscounts += "									   <tr>";
                    gridDiscounts += "										  <td width=\"170\" style=\"line-height:26px;\">";
                    gridDiscounts += "											 <p><b>Code:</b> " + cDiscounts[i].DiscountCode + "<br>";
                    gridDiscounts += "												<b>Expires:</b> " + Strings.FormatDateTime(cDiscounts[i].ExpireDate, Strings.DateNamedFormat.ShortDate) + "<br>";
                    gridDiscounts += "												<b>Quantity:</b> " + cDiscounts[i].Quantity + "<br>";
                    gridDiscounts += "												" + Strings.ToString(cDiscounts[i].CompanyName) + string.Empty;
                    gridDiscounts += "											 </p>";
                    gridDiscounts += "											 " + Strings.ToString(cDiscounts[i].BarCodeImage != null ? "<img src=\"" + cDiscounts[i].BarCodeImage + "\" border=\"0\" />" : "<img src=\"" + sInstallFolder + "images/admin/barcode.gif\" border=\"0\" />") + string.Empty;
                    gridDiscounts += "										  </td>";
                    gridDiscounts += "										  <td width=\"120\" align=\"left\" valign=\"top\">";
                    gridDiscounts += "											 " + Strings.ToString(cDiscounts[i].ProductImage != null ? "<img src=\"" + cDiscounts[i].ProductImage + "\" border=\"0\" class=\"coupon-img img-rounded img-fluid\" />" : string.Empty) + string.Empty;
                    gridDiscounts += "											 <p style=\"font-size:16px; line-height:16px; padding-top:8px;\">" + Strings.ToString(!string.IsNullOrWhiteSpace(Strings.ToString(cDiscounts[i].LabelText)) ? cDiscounts[i].LabelText : string.Empty) + "</p>";
                    gridDiscounts += "										  </td>";
                    gridDiscounts += "										  <td width=\"28%\" align=\"center\" valign=\"top\">";
                    gridDiscounts += "											 <div style=\"background-color:#ff0101; width:125px; height:125px; border-radius:50%; padding:30px 15px 15px; color:#fff; text-align:center\">";
                    gridDiscounts += "												<h2 style=\"margin:0\">" + FormatDiscountPrice(Strings.ToString(cDiscounts[i].MarkOffType), Strings.ToString(cDiscounts[i].MarkOffPrice)) + "</h2>";
                    gridDiscounts += "											 </div>";
                    gridDiscounts += "										  </td>";
                    gridDiscounts += "									   </tr>";
                    gridDiscounts += "									</tbody>";
                    gridDiscounts += "								 </table>";
                    gridDiscounts += "							  </td>";
                    gridDiscounts += "						   </tr>";
                    gridDiscounts += "						   <tr>";
                    gridDiscounts += "							  <td colspan=\"2\" height=\"30\">";
                    gridDiscounts += "								 <table width=\"100%\">";
                    gridDiscounts += "									<tbody>";
                    gridDiscounts += "									   <tr>";
                    gridDiscounts += "										  <td width=\"100%\" valign=\"Bottom\"><span style=\"color: #F02828\"><b>Disclaimer</b></span><b>: </b>" + Strings.ToString(!string.IsNullOrWhiteSpace(Strings.ToString(cDiscounts[i].Disclaimer)) ? Strings.ToString(cDiscounts[i].Disclaimer) : "N/A") + "</td>";
                    gridDiscounts += "									   </tr>";
                    gridDiscounts += "									</tbody>";
                    gridDiscounts += "								 </table>";
                    gridDiscounts += "							  </td>";
                    gridDiscounts += "						   </tr>";
                    gridDiscounts += "						</tbody>";
                    gridDiscounts += "					 </table>";
                    gridDiscounts += "				  </td>";
                    gridDiscounts += "			   </tr>";
                    gridDiscounts += "			</tbody>";
                    gridDiscounts += "		 </table>";
                    gridDiscounts += "	  </div>";
                    gridDiscounts += "   </div>";
                    gridDiscounts += "   <div class=\"discount-card-footer\">";
                    gridDiscounts += "	  <a href=\"javascript:void(0)\" onclick=\"Print(this)\" class=\"card-link\">Print this Coupon</a>";
                    gridDiscounts += "   </div>";
                    gridDiscounts += "</div>";
                    countDiscounts += 1;
                    if (countDiscounts == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestDiscounts||" + strNum + "]]", "Newest Discounts<br/>" + gridDiscounts);
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestEvents||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestEvents||") + Strings.Len("[[DisplayNewestEvents||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cEvents = DAL.Events.GetEvents(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countEvents = 0;
                var gridEvents = new DataTable();
                gridEvents.Columns.Add("Event Date", typeof(string));
                gridEvents.Columns.Add("Subject", typeof(string));
                for (var i = 0; i <= cEvents.Count - 1; i++)
                {
                    didReplace = true;
                    var rowEvents = gridEvents.NewRow();
                    rowEvents["Event Date"] = "<a href=\"" + sInstallFolder + "event/" + cEvents[i].EventID + "/" + Format_ISAPI(cEvents[i].Subject) + "/\">" + Strings.FormatDateTime(cEvents[i].EventDate, Strings.DateNamedFormat.ShortDate) + "</a>";
                    rowEvents["Subject"] = "<a href=\"" + sInstallFolder + "event/" + cEvents[i].EventID + "/" + Format_ISAPI(cEvents[i].Subject) + "/\">" + cEvents[i].Subject + "</a>";
                    gridEvents.Rows.Add(rowEvents);
                    countEvents += 1;
                    if (countEvents == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestEvents||" + strNum + "]]", DataTableToHTMLTable("Newest Events", "newEvents", gridEvents));
                gridEvents.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestEvents||" + strNum + "|Simple]]") > 0)
                {
                    var simpleEvents = string.Empty;
                    countEvents = 0;
                    for (var i = 0; i <= cEvents.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleEvents += "<a href=\"" + sInstallFolder + "event/" + cEvents[i].EventID + "/" + Format_ISAPI(cEvents[i].Subject) + "/\">" + Strings.FormatDateTime(cEvents[i].EventDate, Strings.DateNamedFormat.ShortDate) + ": " + cEvents[i].Subject + "</a><br/>";
                        countEvents += 1;
                        if (countEvents == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestEvents||" + strNum + "|Simple]]", simpleEvents);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestDownloads||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestDownloads||") + Strings.Len("[[DisplayNewestDownloads||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cDownloads = DAL.Downloads.GetDownloads(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countDownloads = 0;
                var gridDownloads = new DataTable();
                gridDownloads.Columns.Add("Date Posted", typeof(string));
                gridDownloads.Columns.Add("Name / Title", typeof(string));
                for (var i = 0; i <= cDownloads.Count - 1; i++)
                {
                    didReplace = true;
                    var rowDownloads = gridDownloads.NewRow();
                    rowDownloads["Date Posted"] = "<a href=\"" + sInstallFolder + "downloads_view.aspx?FileID=" + cDownloads[i].FileID + "\">" + Strings.FormatDateTime(cDownloads[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</a>";
                    rowDownloads["Name / Title"] = "<a href=\"" + sInstallFolder + "downloads_view.aspx?FileID=" + cDownloads[i].FileID + "\">" + cDownloads[i].Field1 + "</a>";
                    gridDownloads.Rows.Add(rowDownloads);
                    countDownloads += 1;
                    if (countDownloads == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestDownloads||" + strNum + "]]", DataTableToHTMLTable("Newest Downloads", "newDownloads", gridDownloads));
                gridDownloads.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestDownloads||" + strNum + "|Simple]]") > 0)
                {
                    var simpleDownloads = string.Empty;
                    countDownloads = 0;
                    for (var i = 0; i <= cDownloads.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleDownloads += "<a href=\"" + sInstallFolder + "downloads_view.aspx?FileID=" + cDownloads[i].FileID + "\">" + Strings.FormatDateTime(cDownloads[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cDownloads[i].Field1 + "</a><br/>";
                        countDownloads += 1;
                        if (countDownloads == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestDownloads||" + strNum + "|Simple]]", simpleDownloads);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestForums||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestForums||") + Strings.Len("[[DisplayNewestForums||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cForums = DAL.Forums.GetForumTopics(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countForums = 0;
                var gridForums = new DataTable();
                gridForums.Columns.Add("Date Posted", typeof(string));
                gridForums.Columns.Add("Subject", typeof(string));
                gridForums.Columns.Add("Posted By", typeof(string));
                for (var i = 0; i <= cForums.Count - 1; i++)
                {
                    didReplace = true;
                    var rowForums = gridForums.NewRow();
                    rowForums["Date Posted"] = "<a href=\"" + sInstallFolder + "forum/" + cForums[i].TopicID + "/" + Format_ISAPI(cForums[i].Subject) + "/\">" + Strings.FormatDateTime(cForums[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</a>";
                    rowForums["Subject"] = "<a href=\"" + sInstallFolder + "forum/" + cForums[i].TopicID + "/" + Format_ISAPI(cForums[i].Subject) + "/\">" + cForums[i].Subject + "</a>";
                    rowForums["Posted By"] = "<a href=\"" + sInstallFolder + "forum/" + cForums[i].TopicID + "/" + Format_ISAPI(cForums[i].Subject) + "/\">" + cForums[i].Username + "</a>";
                    gridForums.Rows.Add(rowForums);
                    countForums += 1;
                    if (countForums == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestForums||" + strNum + "]]", DataTableToHTMLTable("Newest Forums", "newForums", gridForums));
                gridForums.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestForums||" + strNum + "|Simple]]") > 0)
                {
                    var simpleForums = string.Empty;
                    countForums = 0;
                    for (var i = 0; i <= cForums.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleForums += "<a href=\"" + sInstallFolder + "forum/" + cForums[i].TopicID + "/" + Format_ISAPI(cForums[i].Subject) + "/\">" + Strings.FormatDateTime(cForums[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cForums[i].Subject + "</a><br/>";
                        countForums += 1;
                        if (countForums == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestForums||" + strNum + "|Simple]]", simpleForums);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestLinks||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestLinks||") + Strings.Len("[[DisplayNewestLinks||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cLinks = DAL.LinkDirectory.GetLinksWebsite(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countLinks = 0;
                var gridLinks = new DataTable();
                gridLinks.Columns.Add("Date Posted", typeof(string));
                gridLinks.Columns.Add("Site Name", typeof(string));
                for (var i = 0; i <= cLinks.Count - 1; i++)
                {
                    didReplace = true;
                    var rowLinks = gridLinks.NewRow();
                    rowLinks["Date Posted"] = "<a href=\"" + sInstallFolder + "links/" + cLinks[i].LinkID + "/" + Format_ISAPI(cLinks[i].LinkName) + "/\">" + Strings.FormatDateTime(cLinks[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</a>";
                    rowLinks["Site Name"] = "<a href=\"" + sInstallFolder + "links/" + cLinks[i].LinkID + "/" + Format_ISAPI(cLinks[i].LinkName) + "/\">" + cLinks[i].LinkName + "</a>";
                    gridLinks.Rows.Add(rowLinks);
                    countLinks += 1;
                    if (countLinks == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestLinks||" + strNum + "]]", DataTableToHTMLTable("Newest Links", "newLinks", gridLinks));
                gridLinks.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestLinks||" + strNum + "|Simple]]") > 0)
                {
                    var simpleLinks = string.Empty;
                    countLinks = 0;
                    for (var i = 0; i <= cLinks.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleLinks += "<a href=\"" + sInstallFolder + "links/" + cLinks[i].LinkID + "/" + Format_ISAPI(cLinks[i].LinkName) + "/\">" + Strings.FormatDateTime(cLinks[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cLinks[i].LinkName + "</a><br/>";
                        countLinks += 1;
                        if (countLinks == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestLinks||" + strNum + "|Simple]]", simpleLinks);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestNews||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestNews||") + Strings.Len("[[DisplayNewestNews||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cNews = DAL.News.GetNews(showAvailable: true, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countNews = 0;
                var gridNews = string.Empty;
                for (var i = 0; i <= cNews.Count - 1; i++)
                {
                    didReplace = true;
                    gridNews += "<div class=\"article-bx\">";
                    gridNews += "	<div class=\"row\">";
                    gridNews += "		<div class=\"col-md-12\">";
                    gridNews += "			<div class=\"article-content-area\">";
                    gridNews += "				<div class=\"article-top-content\">";
                    gridNews += "					<h3><a href=\"" + sInstallFolder + "news/" + cNews[i].NewsID + "/" + Format_ISAPI(cNews[i].Topic) + "/\">" + cNews[i].Topic + "</a></h3>";
                    gridNews += "					<p>" + Strings.Left(Strings.ToString(cNews[i].Headline), 500) + Strings.ToString(Strings.Len(Strings.ToString(cNews[i].Headline)) > 500 ? "..." : string.Empty) + "</p>";
                    gridNews += "				</div>";
                    gridNews += "				<div class=\"article-btn-group\">";
                    gridNews += "					<p><span>Date :</span> " + Strings.FormatDateTime(cNews[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</p>";
                    gridNews += "					<a href=\"" + sInstallFolder + "news/" + cNews[i].NewsID + "/" + Format_ISAPI(cNews[i].Topic) + "/\" class=\"btn btn-primary\">Read More</a>";
                    gridNews += "				</div>";
                    gridNews += "			</div>";
                    gridNews += "		</div>";
                    gridNews += "	</div>";
                    gridNews += "</div>";
                    countNews += 1;
                    if (countNews == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestNews||" + strNum + "]]", "Newest News<br/>" + gridNews);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestNews||" + strNum + "|Simple]]") > 0)
                {
                    var simpleNews = string.Empty;
                    countNews = 0;
                    for (var i = 0; i <= cNews.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleNews += "<a href=\"" + sInstallFolder + "news/" + cNews[i].NewsID + "/" + Format_ISAPI(cNews[i].Topic) + "/\">" + Strings.FormatDateTime(cNews[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cNews[i].Topic + "</a><br/>";
                        countNews += 1;
                        if (countNews == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestNews||" + strNum + "|Simple]]", simpleNews);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestPolls||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestPolls||") + Strings.Len("[[DisplayNewestPolls||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cPolls = DAL.Polls.GetPolls("StartDate", "DESC");
                var countPolls = 0;
                var gridPolls = new DataTable();
                gridPolls.Columns.Add("Start Date", typeof(string));
                gridPolls.Columns.Add("Poll Questions", typeof(string));
                for (var i = 0; i <= cPolls.Count - 1; i++)
                {
                    didReplace = true;
                    var rowPolls = gridPolls.NewRow();
                    rowPolls["Start Date"] = "<a href=\"" + sInstallFolder + "poll/" + cPolls[i].PollID + "/" + Format_ISAPI(cPolls[i].Question) + "/\">" + Strings.FormatDateTime(cPolls[i].StartDate, Strings.DateNamedFormat.ShortDate) + "</a>";
                    rowPolls["Poll Questions"] = "<a href=\"" + sInstallFolder + "poll/" + cPolls[i].PollID + "/" + Format_ISAPI(cPolls[i].Question) + "/\">" + cPolls[i].Question + "</a>";
                    gridPolls.Rows.Add(rowPolls);
                    countPolls += 1;
                    if (countPolls == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestPolls||" + strNum + "]]", DataTableToHTMLTable("Newest Polls", "newPolls", gridPolls));
                gridPolls.Dispose();

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestPolls||" + strNum + "|Simple]]") > 0)
                {
                    var simplePolls = string.Empty;
                    countPolls = 0;
                    for (var i = 0; i <= cPolls.Count - 1; i++)
                    {
                        didReplace = true;
                        simplePolls += "<a href=\"" + sInstallFolder + "poll/" + cPolls[i].PollID + "/" + Format_ISAPI(cPolls[i].Question) + "/\">" + Strings.FormatDateTime(cPolls[i].StartDate, Strings.DateNamedFormat.ShortDate) + ": " + cPolls[i].Question + "</a><br/>";
                        countPolls += 1;
                        if (countPolls == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestPolls||" + strNum + "|Simple]]", simplePolls);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestPositions||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestPositions||") + Strings.Len("[[DisplayNewestPositions||");
                strNum = Strings.Mid(strPageText, posa, 2);
                try
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    var cPCR = new Integrations.PCRecruiter();
                    cPCR.Members2PCR();
                    var sessionId = cPCR.GetSessionId();
                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions?Fields=DatePosted,CompanyId,CompanyName,JobId,JobTitle,City,MinSalary,MaxSalary,JobType&ResultsPerPage=10&Order=DatePosted DESC");
                    WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                    WRequest.Method = "GET";
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobResults>(jsonString);
                    var countPositions = 0;
                    var gridPositions = new DataTable();
                    gridPositions.Columns.Add("Date Posted", typeof(string));
                    gridPositions.Columns.Add("Job Title", typeof(string));
                    for (var i = 0; i <= pcrResults.Results.Count - 1; i++)
                    {
                        didReplace = true;
                        var rowPositions = gridPositions.NewRow();
                        rowPositions["Date Posted"] = Strings.FormatDateTime(pcrResults.Results[i].DatePosted.Value, Strings.DateNamedFormat.ShortDate);
                        rowPositions["Job Title"] = "<a href=\"" + sInstallFolder + "position/" + pcrResults.Results[i].JobId + "/" + Format_ISAPI(pcrResults.Results[i].JobTitle) + "/\">" + pcrResults.Results[i].JobTitle + "</a>";
                        gridPositions.Rows.Add(rowPositions);
                        countPositions += 1;
                        if (countPositions == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestPositions||" + strNum + "]]", DataTableToHTMLTable("Newest Positions", "newPositions", gridPositions));
                    cPCR.Dispose();
                    gridPositions.Dispose();
                }
                catch
                {
                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestPositions||" + strNum + "]]", string.Empty);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestProducts||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestProducts||") + Strings.Len("[[DisplayNewestProducts||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cProducts = DAL.ShoppingMall.GetShopProducts("Mod.DatePosted", "DESC", showSalesOnly: false, ShowOnlyAvailable: true);
                var countProducts = 0;
                var gridProducts = string.Empty;

                gridProducts += "<script type=\"text/javascript\">" + Environment.NewLine;
                gridProducts += "	var skipRestyling = true;" + Environment.NewLine;

                gridProducts += "	function saveWishList(ProductID) {" + Environment.NewLine;
                gridProducts += "		$.ajax({" + Environment.NewLine;
                gridProducts += "			url: config.siteBase + 'favorites_add.aspx?ModuleID=41&PageURL=WISHLIST:' + ProductID," + Environment.NewLine;
                gridProducts += "			error: function (xhr) {" + Environment.NewLine;
                gridProducts += "				alert(\"There has been an error loading data.\");console.log(xhr);" + Environment.NewLine;
                gridProducts += "			}," + Environment.NewLine;
                gridProducts += "			success: function (data) {" + Environment.NewLine;
                gridProducts += "				openModal('AddWishList', 300, 200);" + Environment.NewLine;
                gridProducts += "				$('#AddWishListMsg').html(data);" + Environment.NewLine;
                gridProducts += "			}" + Environment.NewLine;
                gridProducts += "		});" + Environment.NewLine;
                gridProducts += "		return false;" + Environment.NewLine;
                gridProducts += "	}" + Environment.NewLine;

                gridProducts += "	function orderProduct(ProductID, StoreID) {" + Environment.NewLine;
                gridProducts += "			var Invoice = new Object();" + Environment.NewLine;
                gridProducts += "			var Products = [];" + Environment.NewLine;
                gridProducts += "			var ProductsObj = new Object();" + Environment.NewLine;
                gridProducts += "			Invoice.InvoiceID = \"" + Session_Invoice_ID() + "\";" + Environment.NewLine;
                gridProducts += "			Invoice.UserID = \"" + Session_User_ID() + "\";" + Environment.NewLine;
                gridProducts += "			Invoice.Status = 0;" + Environment.NewLine;
                gridProducts += "			Invoice.OrderDate = \"" + DateTime.Today + "\";" + Environment.NewLine;
                gridProducts += "			Invoice.ModuleID = 41;" + Environment.NewLine;
                gridProducts += "			ProductsObj[\"ProductID\"] = ProductID;" + Environment.NewLine;
                gridProducts += "			ProductsObj[\"Quantity\"] = 1;" + Environment.NewLine;
                gridProducts += "			Products.push(ProductsObj);" + Environment.NewLine;
                gridProducts += "			Invoice.Products = Products;" + Environment.NewLine;
                gridProducts += "			Invoice.EmailInvoice = false;" + Environment.NewLine;
                gridProducts += "			Invoice.LinkID = 0;" + Environment.NewLine;
                gridProducts += "			Invoice.StoreID = StoreID;" + Environment.NewLine;
                gridProducts += "			Invoice.PortalID = \"" + Get_Portal_ID() + "\";" + Environment.NewLine;

                gridProducts += "		$.ajax({" + Environment.NewLine;
                gridProducts += "			type: \"POST\"," + Environment.NewLine;
                gridProducts += "			headers: { \"Content-Type\": \"application/json\", \"Accept\": \"application/json\" }," + Environment.NewLine;
                gridProducts += "			data: JSON.stringify(Invoice)," + Environment.NewLine;
                gridProducts += "			url: config.imageBase + \"api/invoices\"," + Environment.NewLine;
                gridProducts += "			dataType: \"json\"," + Environment.NewLine;
                gridProducts += "			contentType: \"application/json\"," + Environment.NewLine;
                gridProducts += "			error: function (xhr) {" + Environment.NewLine;
                gridProducts += "				alert(\"There has been an error loading data.\");" + Environment.NewLine;
                gridProducts += "			}," + Environment.NewLine;
                gridProducts += "			success: function () {" + Environment.NewLine;
                gridProducts += "				document.location.href = \"" + sInstallFolder + "viewcart.aspx?ContinueURL=" + GetContinueURL() + "\";" + Environment.NewLine;
                gridProducts += "			}" + Environment.NewLine;
                gridProducts += "		});" + Environment.NewLine;

                gridProducts += "		return false;" + Environment.NewLine;
                gridProducts += "	}" + Environment.NewLine;
                gridProducts += "</script>" + Environment.NewLine;

                for (var i = 0; i <= cProducts.Count - 1; i++)
                {
                    didReplace = true;
                    var showSaleRow = Show_Sale_Row(cProducts[i].SalePrice.ToString(), cProducts[i].UnitPrice.ToString());
                    gridProducts += "<div class=\"shoping-product\">";
                    gridProducts += "	<ul>";
                    gridProducts += "		<li>";
                    gridProducts += "			<div class=\"pro-image\">";
                    gridProducts += "				<a href=\"" + sInstallFolder + "shopping/" + cProducts[i].ProductID + "/" + Format_ISAPI(cProducts[i].ProductName) + "/\">";
                    gridProducts += "					<img src='" + cProducts[i].DefaultPicture + "' border=\"0\" alt='" + cProducts[i].ProductName + "' class=\"img-fluid\" />";
                    gridProducts += "				</a>";
                    gridProducts += "			</div>";
                    gridProducts += "			<div class=\"pro-text\">";
                    gridProducts += "				<h3 class=\"pro-name\"><a href=\"" + sInstallFolder + "shopping/" + cProducts[i].ProductID + "/" + Format_ISAPI(cProducts[i].ProductName) + "/\">" + cProducts[i].ProductName + "</a></h3>";
                    gridProducts += "				<p>";
                    gridProducts += "					" + Strings.ToString(!string.IsNullOrWhiteSpace(Strings.ToString(cProducts[i].ShortDescription)) ? "<br />" + cProducts[i].ShortDescription : string.Empty) + string.Empty;
                    gridProducts += "				</p>";
                    gridProducts += "				<h4 class=\"main-price\">Price: " + Strings.ToString(showSaleRow ? "<s>" : string.Empty) + Format_Price(Strings.ToString(cProducts[i].SalePrice), Strings.ToString(cProducts[i].UnitPrice), Strings.ToString(cProducts[i].RecurringPrice), Strings.ToString(cProducts[i].RecurringCycle)) + Strings.ToString(showSaleRow ? "</s>" : string.Empty);
                    if (showSaleRow)
                    {
                        gridProducts += "					<span>" + Format_Sale_Price(cProducts[i].SalePrice.ToString(), cProducts[i].RecurringPrice.ToString(), cProducts[i].RecurringCycle) + "</span>";
                    }

                    gridProducts += "				</h4><a class=\"btn btn-success\" href=\"javascript:void(0)\" onclick=\"" + orderProduct(Strings.ToString(cProducts[i].ProductID), Strings.ToString(cProducts[i].StoreID)) + "\">Add to Cart</a>";
                    gridProducts += "				<a class=\"btn btn-secondary\" href=\"javascript:void(0)\" onclick=\"" + Strings.ToString(!string.IsNullOrWhiteSpace(Session_User_ID()) ? "saveWishList('" + cProducts[i].ProductID + "');return false;" : "document.location.href='" + GetInstallFolder() + "login.aspx';return false;") + "\">Add to Wish List</a>";
                    gridProducts += "			</div>";
                    gridProducts += "		</li>";
                    gridProducts += "	</ul>";
                    gridProducts += "</div>";
                    countProducts += 1;
                    if (countProducts == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestProducts||" + strNum + "]]", "Newest Products<br/>" + gridProducts);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestProducts||" + strNum + "|Simple]]") > 0)
                {
                    var simpleProducts = string.Empty;
                    countProducts = 0;
                    for (var i = 0; i <= cProducts.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleProducts += "<a href=\"" + sInstallFolder + "shopping/" + cProducts[i].ProductID + "/" + Format_ISAPI(cProducts[i].ProductName) + "/\">" + Strings.FormatDateTime(cProducts[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cProducts[i].ProductName + "</a><br/>";
                        countProducts += 1;
                        if (countProducts == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestProducts||" + strNum + "|Simple]]", simpleProducts);
                }

                // Slider Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestProducts||" + strNum + "|Slider]]") > 0)
                {
                    var sliderProducts = string.Empty;
                    countProducts = 0;
                    sliderProducts += "<div class=\"product-part\"><ul class=\"product-slider\">";
                    for (var i = 0; i <= cProducts.Count - 1; i++)
                    {
                        didReplace = true;
                        sliderProducts += "<li>" + "<div class=\"pro-img\">" + "<img src=\"" + cProducts[i].DefaultPicture + "\" class=\"img-fluid\" />" + "</div>" + "<div class=\"pro-detail\">" + "<h5>" + LangText("Date Posted") + " : " + Strings.FormatDateTime(cProducts[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</h5>" + "<h4><a href=\"" + sInstallFolder + "shopping/" + cProducts[i].ProductID + "/" + Format_ISAPI(cProducts[i].ProductName) + "/\">" + cProducts[i].ProductName + "</a></h4>" + "<p>" + Strings.Left(cProducts[i].ShortDescription, 70) + "</p>" + "<div class=\"price\">" + Format_Price(Strings.ToString(cProducts[i].SalePrice), Strings.ToString(cProducts[i].UnitPrice), Strings.ToString(cProducts[i].RecurringPrice), cProducts[i].RecurringCycle) + "</div>" + "</div>" + "</li>";
                        countProducts += 1;
                        if (countProducts == toInt(strNum))
                        {
                            break;
                        }
                    }

                    sliderProducts += "</ul></div>";

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestProducts||" + strNum + "|Slider]]", sliderProducts);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestProfiles||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestProfiles||") + Strings.Len("[[DisplayNewestProfiles||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cProfiles = DAL.UserProfiles.GetUserProfiles(StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countProfiles = 0;
                var gridProfiles = string.Empty;
                for (var i = 0; i <= cProfiles.Count - 1; i++)
                {
                    didReplace = true;

                    gridProfiles += "<div class=\"ArticleList\" style=\"overflow: hidden; white-space: nowrap; width: 100%;\">";
                    gridProfiles += "	<div style=\"float: left;\">";
                    gridProfiles += "		<img src=\"" + cProfiles[i].DefaultPicture + "\" border=\"0\" alt=\"\" />";
                    gridProfiles += "		<br />";
                    gridProfiles += "		Last Login: " + Strings.FormatDateTime(cProfiles[i].LastLogin, Strings.DateNamedFormat.ShortDate);
                    gridProfiles += "		<br />";
                    gridProfiles += "		Views: " + cProfiles[i].Views;
                    gridProfiles += "	</div>";
                    gridProfiles += "	<div style=\"display: inline-block;\">";
                    gridProfiles += "		<a href=\"" + sInstallFolder + "profile/" + cProfiles[i].ProfileID + "/" + Format_ISAPI(cProfiles[i].Username) + "/\">" + cProfiles[i].Username + "</a>";
                    gridProfiles += "		<br />";
                    gridProfiles += "		" + Strings.ToString(Convert.ToInt32(cProfiles[i].Age) > 17 && ShowAge() ? "Age: " + cProfiles[i].Age + "<br />" : string.Empty) + string.Empty;
                    gridProfiles += "		" + Strings.ToString(ShowGender() ? "Gender: " + cProfiles[i].Sex + "<br />" : string.Empty) + string.Empty;
                    gridProfiles += "		Location: " + cProfiles[i].Location;
                    gridProfiles += "		<br />";
                    gridProfiles += "		Distance: " + cProfiles[i].Distance;
                    gridProfiles += "		<br />";
                    gridProfiles += "	</div>";
                    gridProfiles += "</div>";
                    countProfiles += 1;
                    if (countProfiles == toInt(strNum))
                    {
                        break;
                    }
                }

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestProfiles||" + strNum + "]]", "Newest Profiles<br/>" + gridProfiles);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestProfiles||" + strNum + "|Simple]]") > 0)
                {
                    var simpleProfiles = string.Empty;
                    countProfiles = 0;
                    for (var i = 0; i <= cProfiles.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleProfiles += "<a href=\"" + sInstallFolder + "profile/" + cProfiles[i].ProfileID + "/" + Format_ISAPI(cProfiles[i].Username) + "/\">" + Strings.FormatDateTime(cProfiles[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cProfiles[i].Username + "</a><br/>";
                        countProfiles += 1;
                        if (countProfiles == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestProfiles||" + strNum + "|Simple]]", simpleProfiles);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestProperties||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestProperties||") + Strings.Len("[[DisplayNewestProperties||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cProperties = DAL.RealEstate.GetRealEstateProperties(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countProperties = 0;
                var gridProperties = string.Empty;

                gridProperties += "<script type=\"text/javascript\">";
                gridProperties += "	$(document).ready(function () {";
                gridProperties += "		restyleGridView(\"#NewestPropertiesGrid\");";
                gridProperties += "	});";
                gridProperties += "</script>";

                gridProperties += "<table id=\"NewestPropertiesGrid\"><tbody><tr><td>";
                for (var i = 0; i <= cProperties.Count - 1; i++)
                {
                    didReplace = true;

                    gridProperties += "<table class=\"Table\" width=\"100%\">";
                    gridProperties += "	<tr class=\"TableHeader\">";
                    gridProperties += "		<td>";
                    gridProperties += "			<a href=\"" + sInstallFolder + "property/" + cProperties[i].PropertyID + "/" + Format_ISAPI(cProperties[i].Title) + "/\" class=\"PropertyTitle\">" + cProperties[i].Title + "</a>";
                    gridProperties += "		</td>";
                    gridProperties += "		<td align=\"right\">";
                    gridProperties += "			<span class=\"PropertyHits\">Hits: " + cProperties[i].Visits + "</span>";
                    gridProperties += "		</td>";
                    gridProperties += "	</tr>";
                    gridProperties += "	<tr class=\"TableBody2\">";
                    gridProperties += "		<td valign=\"top\" width=\"90%\">" + cProperties[i].Description + "</td>";
                    gridProperties += "		<td align=\"right\" valign=\"top\" width=\"10%\">";
                    gridProperties += "			<a href=\"" + sInstallFolder + "property/" + cProperties[i].PropertyID + "/" + Format_ISAPI(cProperties[i].Title) + "/\" class=\"btn btn-primary\" style=\"width: 90px\">Details</a>";
                    gridProperties += "			<a href=\"" + sInstallFolder + "refer.aspx?PageURL=" + UrlEncode(sInstallFolder + "property/" + cProperties[i].PropertyID + "/" + Format_ISAPI(cProperties[i].Title) + "/") + "\" class=\"btn btn-secondary\" style=\"width: 90px\">Refer</a>";
                    gridProperties += "		</td>";
                    gridProperties += "	</tr>";
                    gridProperties += "</table>";
                    gridProperties += "<br />";
                    countProperties += 1;
                    if (countProperties == toInt(strNum))
                    {
                        break;
                    }
                }

                gridProperties += "</td></tr></tbody></table>";

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestProperties||" + strNum + "]]", "Newest Properties<br/>" + gridProperties);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestProperties||" + strNum + "|Simple]]") > 0)
                {
                    var simpleProperties = string.Empty;
                    countProperties = 0;
                    for (var i = 0; i <= cProperties.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleProperties += "<a href=\"" + sInstallFolder + "property/" + cProperties[i].PropertyID + "/" + Format_ISAPI(cProperties[i].Title) + "/\">" + Strings.FormatDateTime(cProperties[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cProperties[i].Title + "</a><br/>";
                        countProperties += 1;
                        if (countProperties == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestProperties||" + strNum + "|Simple]]", simpleProperties);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestVouchers||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestVouchers||") + Strings.Len("[[DisplayNewestVouchers||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cVouchers = DAL.Vouchers.GetVouchers(UserID: sUserID, StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countVouchers = 0;
                using (DataTable gridVouchers = new DataTable())
                {
                    gridVouchers.Columns.Add("Date Posted", typeof(string));
                    gridVouchers.Columns.Add("Title", typeof(string));
                    gridVouchers.Columns.Add("Sale Price", typeof(string));
                    for (var i = 0; i <= cVouchers.Count - 1; i++)
                    {
                        didReplace = true;
                        var rowVouchers = gridVouchers.NewRow();
                        rowVouchers["Date Posted"] = "<a href=\"" + sInstallFolder + "voucher/" + cVouchers[i].VoucherID + "/" + Format_ISAPI(cVouchers[i].BuyTitle) + "/\">" + Strings.FormatDateTime(cVouchers[i].DatePosted, Strings.DateNamedFormat.ShortDate) + "</a>";
                        rowVouchers["Title"] = "<a href=\"" + sInstallFolder + "voucher/" + cVouchers[i].VoucherID + "/" + Format_ISAPI(cVouchers[i].BuyTitle) + "/\">" + cVouchers[i].BuyTitle + "</a>";
                        rowVouchers["Sale Price"] = "<a href=\"" + sInstallFolder + "voucher/" + cVouchers[i].VoucherID + "/" + Format_ISAPI(cVouchers[i].BuyTitle) + "/\">" + Format_Currency(cVouchers[i].SalePrice) + "</a>";
                        gridVouchers.Rows.Add(rowVouchers);
                        countVouchers += 1;
                        if (countVouchers == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestVouchers||" + strNum + "]]", DataTableToHTMLTable("Newest Vouchers", "newVouchers", gridVouchers));
                }

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestVouchers||" + strNum + "|Simple]]") > 0)
                {
                    var simpleVouchers = string.Empty;
                    countVouchers = 0;
                    for (var i = 0; i <= cVouchers.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleVouchers += "<a href=\"" + sInstallFolder + "voucher/" + cVouchers[i].VoucherID + "/" + Format_ISAPI(cVouchers[i].BuyTitle) + "/\">" + Strings.FormatDateTime(cVouchers[i].DatePosted, Strings.DateNamedFormat.ShortDate) + ": " + cVouchers[i].BuyTitle + "</a><br/>";
                        countVouchers += 1;
                        if (countVouchers == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestVouchers||" + strNum + "|Simple]]", simpleVouchers);
                }
            }

            if (Strings.InStr(strPageText, "[[DisplayNewestUserPages||") > 0)
            {
                // DataTable Newest Listings
                posa = Strings.InStr(strPageText, "[[DisplayNewestUserPages||") + Strings.Len("[[DisplayNewestUserPages||");
                strNum = Strings.Mid(strPageText, posa, 2);
                var cUserPages = DAL.UserPages.GetUserPages(StartDate: isLatestNews ? Strings.ToString(DateAndTime.DateAdd(DateAndTime.DateInterval.Day, -1, DateTime.Now)) : string.Empty);
                var countUserPages = 0;
                var gridUserPages = string.Empty;
                gridUserPages += "<script type=\"text/javascript\">";
                gridUserPages += "	$(document).ready(function () {";
                gridUserPages += "		restyleGridView(\"#NewUserSiteGrid\");";
                gridUserPages += "	});";
                gridUserPages += "</script>";

                gridUserPages += "<table id=\"NewUserSiteGrid\"><tbody><tr><td>";
                for (var i = 0; i <= cUserPages.Count - 1; i++)
                {
                    didReplace = true;

                    gridUserPages += "<table width=\"95%\" align=\"center\" class=\"Table\">";
                    gridUserPages += "	<tr class=\"TableHeader\">";
                    gridUserPages += "		<td>";
                    gridUserPages += "			<a href=\"" + sInstallFolder + "members/" + cUserPages[i].UserName + "/\" target=\"_blank\">" + cUserPages[i].SiteName + "</a>";
                    gridUserPages += "		</td>";
                    gridUserPages += "		<td align=\"right\">Hits: " + cUserPages[i].Visits + "</td>";
                    gridUserPages += "	</tr>";
                    gridUserPages += "	<tr class=\"TableBody1\">";
                    gridUserPages += "		<td valign=\"top\" width=\"100%\">" + cUserPages[i].Description + "</td>";
                    gridUserPages += "		<td align=\"right\" valign=\"top\" width=\"130\">";
                    gridUserPages += "			<a href=\"javascript:void(0)\" class=\"btn btn-primary\" onclick=\"window.open('" + sInstallFolder + "members/" + cUserPages[i].UserName + "')\" style=\"width: 130px\">Visit</a>";
                    gridUserPages += "			<a href=\"" + sInstallFolder + "refer.aspx?URL=" + UrlEncode(sInstallFolder + "members/" + cUserPages[i].UserName + "/") + "\" class=\"btn btn-secondary\" style=\"width: 130px\">Refer</a>";
                    gridUserPages += "		</td>";
                    gridUserPages += "	</tr>";
                    gridUserPages += "</table>";
                    gridUserPages += "<br />";
                    countUserPages += 1;
                    if (countUserPages == toInt(strNum))
                    {
                        break;
                    }
                }

                gridUserPages += "</td></tr></tbody></table>";

                strPageText = Strings.Replace(strPageText, "[[DisplayNewestUserPages||" + strNum + "]]", "Newest User Sites<br/>" + gridUserPages);

                // Simple Newest Listings
                if (Strings.InStr(strPageText, "[[DisplayNewestUserPages||" + strNum + "|Simple]]") > 0)
                {
                    var simpleUserPages = string.Empty;
                    countUserPages = 0;
                    for (var i = 0; i <= cUserPages.Count - 1; i++)
                    {
                        didReplace = true;
                        simpleUserPages += "<a href=\"" + sInstallFolder + "members/" + cUserPages[i].UserName + "/\">" + cUserPages[i].UserName + "</a><br/>";
                        countUserPages += 1;
                        if (countUserPages == toInt(strNum))
                        {
                            break;
                        }
                    }

                    strPageText = Strings.Replace(strPageText, "[[DisplayNewestUserPages||" + strNum + "|Simple]]", simpleUserPages);
                }
            }

            if (isLatestNews && didReplace == false)
            {
                return string.Empty;
            }

            return strPageText;
        }

        /// <summary>
        /// Shows the sale row.
        /// </summary>
        /// <param name="sSalePrice">The s sale price.</param>
        /// <param name="sUnitPrice">The s unit price.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Show_Sale_Row(string sSalePrice, string sUnitPrice)
        {
            if (toDecimal(sSalePrice) < toDecimal(sUnitPrice) && toDecimal(sSalePrice) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Shows the age.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ShowAge()
        {
            if (Setup(994, "AskBirthDate") != "Yes")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shows the gender.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ShowGender()
        {
            if (Setup(994, "AskGender") != "Yes")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vouchers print.
        /// </summary>
        /// <param name="sInvoiceProductID">Identifier for the invoice product.</param>
        /// <returns>A string.</returns>
        public static string Vouchers_Print(string sInvoiceProductID)
        {
            var str = new StringBuilder();

            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT VP.VoucherID,VP.PurchaseCode,V.BuyTitle,V.FinePrint,V.RegularPrice,V.BusinessName,V.Address,V.City,V.State,V.ZipCode,V.PhoneNumber,V.RedemptionStart,V.RedemptionEnd,V.Disclaimer,(SELECT TOP 1 FileName FROM Uploads WHERE UniqueID=V.VoucherID AND ModuleID='65' AND Approved='1' AND isTemp='0') AS FileName2 FROM VouchersPurchased AS VP, Vouchers AS V WHERE V.VoucherID=VP.VoucherID AND VP.CartID='" + FixWord(sInvoiceProductID) + "' AND VP.Redeemed='0'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            str.Append("<table width=\"90%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: #000000 2px dashed;\">");
                            str.Append("<tr>");
                            str.Append("<td valign=\"top\"><br/><table align=\"center\" height=\"187\" cellpadding=\"0\" cellspacing=\"0\" width=\"95%\">");
                            str.Append("<tr class=\"TableHeader\">");
                            str.Append("<td align=\"center\" height=\"20\">" + openNull(RS["BuyTitle"]) + " : " + openNull(RS["PurchaseCode"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<tr>");
                            str.Append("<td align=\"center\"><b>www." + Request.ServerVariables("SERVER_NAME") + "</b></td>");
                            str.Append("</tr><tr>");
                            str.Append("<td align=\"center\">Power Buy Deals Voucher</td>");
                            str.Append("</tr>");
                            if (!string.IsNullOrWhiteSpace(openNull(RS["BusinessName"])))
                            {
                                str.Append("<tr>");
                                str.Append("<td align=\"center\"><b>" + openNull(RS["BusinessName"]) + "</b><br/>");
                                str.Append(openNull(RS["Address"]) + "<br/>" + openNull(RS["City"]) + ", " + openNull(RS["State"]) + " " + openNull(RS["ZipCode"]));
                                if (!string.IsNullOrWhiteSpace(openNull(RS["PhoneNumber"])))
                                {
                                    str.Append("<br/>" + openNull(RS["PhoneNumber"]));
                                }

                                str.Append("</td>");
                                str.Append("</tr>");
                            }

                            str.Append("<tr>");
                            str.Append("<td align=\"center\" width=\"100%\"><span style=\"font-size: 16px; color: #FF0000;\">Voucher Redemption Value: " + Format_Currency(openNull(RS["RegularPrice"])) + "</span><br/>Valid from " + DateAndTime.DateAdd(DateAndTime.DateInterval.Day, toInt(Setup(65, "VoucherDaysAdd")), Convert.ToDateTime(openNull(RS["RedemptionStart"]))) + " - " + openNull(RS["RedemptionEnd"]) + "</td>");
                            str.Append("</tr>");
                            str.Append("<td height=\"20\">");
                            str.Append("<table width=\"100%\">");
                            str.Append("<tr>");
                            str.Append("<td width=\"100%\" valign=\"Bottom\">");
                            if (!string.IsNullOrWhiteSpace(openNull(RS["Disclaimer"])))
                            {
                                str.Append("<b>" + LangText("Disclaimer:") + "</b> " + openNull(RS["Disclaimer"]) + "<br/>");
                            }

                            str.Append(openNull(RS["FinePrint"]));
                            str.Append("</td>");
                            str.Append("</tr>");
                            str.Append("</table></td>");
                            str.Append("</tr>");
                            str.Append("</table></td>");
                            str.Append("</tr>");
                            if (!string.IsNullOrWhiteSpace(openNull(RS["FileName2"])))
                            {
                                str.Append("<tr>");
                                str.Append("<td align=\"center\">" + LoadImage(openNull(RS["FileName2"]), 65) + "</td>");
                                str.Append("</tr>");
                            }

                            str.Append("</table><br/>");
                        }
                    }
                }
            }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Gets a body.
        /// </summary>
        /// <param name="dTable">The table.</param>
        /// <returns>The body.</returns>
        private static string GetBody(DataTable dTable)
        {
            var dString = new StringBuilder();

            foreach (DataRow dRow in dTable.Rows)
            {
                dString.Append("<tr class=\"RowStyle\">" + Environment.NewLine);
                for (var dCount = 0; dCount <= dTable.Columns.Count - 1; dCount++)
                {
                    dString.AppendFormat("<td valign=\"top\">{0}</td>" + Environment.NewLine, dRow[dCount]);
                }

                dString.Append("</tr>" + Environment.NewLine);
            }

            return Strings.ToString(dString);
        }

        /// <summary>
        /// Gets a header.
        /// </summary>
        /// <param name="dTable">The table.</param>
        /// <returns>The header.</returns>
        private static string GetHeader(DataTable dTable)
        {
            var dString = new StringBuilder();

            dString.Append("<tr class=\"HeaderStyle\">" + Environment.NewLine);
            foreach (DataColumn dColumn in dTable.Columns)
            {
                var sWidth = Strings.InStr(dColumn.ColumnName, "Date") > 0 ? " style=\"width:150px\"" : string.Empty;
                dString.AppendFormat("<th align=\"left\" scope=\"col\"" + sWidth + ">{0}</th>" + Environment.NewLine, dColumn.ColumnName);
            }

            dString.Append("</tr>" + Environment.NewLine);

            return Strings.ToString(dString);
        }
    }
}