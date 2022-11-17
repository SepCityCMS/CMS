// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="module_stats.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class module_stats.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class module_stats : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The ds
        /// </summary>
        private readonly DataSet ds = new DataSet();

        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection conn;

        /// <summary>
        /// The da
        /// </summary>
        private SqlDataAdapter da;

        /// <summary>
        /// Advertisings the expired.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Advertising_Expired()
        {
            using (var cmd = new SqlCommand("SELECT Count(AdID) AS Counter FROM Advertisements WHERE MaxClicks <= TotalClicks OR MaxExposures <= TotalExposures", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

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
        /// Forumses the replies.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Forums_Replies()
        {
            using (var cmd = new SqlCommand("SELECT Count(TopicID) AS Counter FROM ForumsMessages WHERE ReplyID > 0", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Gets the default XML.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Get_Default_XML()
        {
            string sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            sXML += "<root>" + Environment.NewLine;
            sXML += "<FIELD1>[[advertising]]</FIELD1>" + Environment.NewLine;
            sXML += "<FIELD2>[[members]]</FIELD2>" + Environment.NewLine;
            sXML += "</root>" + Environment.NewLine;

            return sXML;
        }

        /// <summary>
        /// Gets the main totals.
        /// </summary>
        /// <param name="strText">The string text.</param>
        /// <returns>System.String.</returns>
        public string Get_Main_Totals(string strText)
        {
            var str = string.Empty;
            switch (Strings.LCase(strText))
            {
                case "[[blogs]]":
                    str = Totals_Blogger();
                    break;

                case "[[advertising]]":
                    str = Totals_Advertising();
                    break;

                case "[[albums]]":
                    str = Totals_Albums();
                    break;

                case "[[articles]]":
                    str = Totals_Articles();
                    break;

                case "[[auctions]]":
                    str = Totals_Auctions();
                    break;

                case "[[businesses]]":
                    str = Totals_Businesses();
                    break;

                case "[[classifieds]]":
                    str = Totals_Classifieds();
                    break;

                case "[[discounts]]":
                    str = Totals_Discounts();
                    break;

                case "[[elearning]]":
                    str = Totals_Elearning();
                    break;

                case "[[events]]":
                    str = Totals_Events();
                    break;

                case "[[faqs]]":
                    str = Totals_FAQs();
                    break;

                case "[[forums]]":
                    str = Totals_Forums();
                    break;

                case "[[guestbook]]":
                    str = Totals_Guestbook();
                    break;

                case "[[personal]]":
                    str = Totals_Personal();
                    break;

                case "[[libraries]]":
                    str = Totals_Libraries();
                    break;

                case "[[links]]":
                    str = Totals_Links();
                    break;

                case "[[members]]":
                    str = Totals_Members();
                    break;

                case "[[polls]]":
                    str = Totals_Polls();
                    break;

                case "[[profiles]]":
                    str = Totals_Profiles();
                    break;

                case "[[referral]]":
                    str = Totals_Referral();
                    break;

                case "[[shopping]]":
                    str = Totals_Shopping();
                    break;

                case "[[speakers]]":
                    str = Totals_Speakers();
                    break;

                case "[[vouchers]]":
                    str = Totals_Vouchers();
                    break;
            }

            return str;
        }

        /// <summary>
        /// Gets the module totals.
        /// </summary>
        /// <param name="strTableName">Name of the string table.</param>
        /// <param name="strDistinct">The string distinct.</param>
        /// <returns>System.Int64.</returns>
        public long Get_Module_Totals(string strTableName, string strDistinct = "")
        {
            var ds2 = new DataSet();

            if (!string.IsNullOrWhiteSpace(strDistinct))
                using (var cmd = new SqlCommand("SELECT DISTINCT " + strDistinct + " FROM " + strTableName + " WHERE Status <> -1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds2);
                    conn.Close();
                    ds2.Dispose();
                    return SepFunctions.toLong(SepFunctions.openNull(ds2.Tables[0].Rows.Count));
                }

            using (var cmd = new SqlCommand("SELECT Count(*) AS Counter FROM " + strTableName + " WHERE Status <> -1", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds2);
                conn.Close();
                ds2.Dispose();
                return SepFunctions.toLong(SepFunctions.openNull(ds2.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Memberses the active.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Members_Active()
        {
            using (var cmd = new SqlCommand("SELECT Count(UserID) AS Counter FROM Members WHERE Status=1", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Memberses the not active.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Members_Not_Active()
        {
            using (var cmd = new SqlCommand("SELECT Count(UserID) AS Counter FROM Members WHERE Status='0'", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Modules the stats.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Module_Stats()
        {
            var str = string.Empty;
            var GetField = new string[31];

            using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='ADMMAIN' AND UserID='0'", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();

                string sXML;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    sXML = Get_Default_XML();
                    for (var i = 1; i <= 30; i++) GetField[i] = SepFunctions.ParseXML("FIELD" + i, sXML);
                }
                else
                {
                    sXML = SepFunctions.openNull(ds.Tables[0].Rows[0]["ScriptText"]);
                    for (var i = 1; i <= 30; i++) GetField[i] = SepFunctions.ParseXML("FIELD" + i, sXML);
                }

                ds.Dispose();

                str += "<table>";
                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[1])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[2])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[3])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[4])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[5])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[1])))
                        str += "<td>" + Get_Main_Totals(GetField[1]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[2])))
                        str += "<td>" + Get_Main_Totals(GetField[2]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[3])))
                        str += "<td>" + Get_Main_Totals(GetField[3]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[4])))
                        str += "<td>" + Get_Main_Totals(GetField[4]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[5])))
                        str += "<td>" + Get_Main_Totals(GetField[5]) + "</td>";
                    str += "</tr>";
                }

                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[6])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[7])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[8])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[9])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[10])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[6])))
                        str += "<td>" + Get_Main_Totals(GetField[6]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[7])))
                        str += "<td>" + Get_Main_Totals(GetField[7]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[8])))
                        str += "<td>" + Get_Main_Totals(GetField[8]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[9])))
                        str += "<td>" + Get_Main_Totals(GetField[9]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[10])))
                        str += "<td>" + Get_Main_Totals(GetField[10]) + "</td>";
                    str += "</tr>";
                }

                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[11])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[12])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[13])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[14])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[15])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[11])))
                        str += "<td>" + Get_Main_Totals(GetField[11]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[12])))
                        str += "<td>" + Get_Main_Totals(GetField[12]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[13])))
                        str += "<td>" + Get_Main_Totals(GetField[13]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[14])))
                        str += "<td>" + Get_Main_Totals(GetField[14]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[15])))
                        str += "<td>" + Get_Main_Totals(GetField[15]) + "</td>";
                    str += "</tr>";
                }

                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[16])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[17])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[18])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[19])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[20])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[16])))
                        str += "<td>" + Get_Main_Totals(GetField[16]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[17])))
                        str += "<td>" + Get_Main_Totals(GetField[17]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[18])))
                        str += "<td>" + Get_Main_Totals(GetField[18]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[19])))
                        str += "<td>" + Get_Main_Totals(GetField[19]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[20])))
                        str += "<td>" + Get_Main_Totals(GetField[20]) + "</td>";
                    str += "</tr>";
                }

                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[21])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[22])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[23])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[24])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[25])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[21])))
                        str += "<td>" + Get_Main_Totals(GetField[21]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[22])))
                        str += "<td>" + Get_Main_Totals(GetField[22]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[23])))
                        str += "<td>" + Get_Main_Totals(GetField[23]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[24])))
                        str += "<td>" + Get_Main_Totals(GetField[24]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[25])))
                        str += "<td>" + Get_Main_Totals(GetField[25]) + "</td>";
                    str += "</tr>";
                }

                if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[26])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[27])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[28])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[29])) || !string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[30])))
                {
                    str += "<tr>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[26])))
                        str += "<td>" + Get_Main_Totals(GetField[26]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[27])))
                        str += "<td>" + Get_Main_Totals(GetField[27]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[28])))
                        str += "<td>" + Get_Main_Totals(GetField[28]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[29])))
                        str += "<td>" + Get_Main_Totals(GetField[29]) + "</td>";
                    if (!string.IsNullOrWhiteSpace(Get_Main_Totals(GetField[30])))
                        str += "<td>" + Get_Main_Totals(GetField[30]) + "</td>";
                    str += "</tr>";
                }

                str += "</table>";
            }

            return str;
        }

        /// <summary>
        /// Pollses the expired.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Polls_Expired()
        {
            using (var cmd = new SqlCommand("SELECT Count(PollID) AS Counter FROM PNQQuestions WHERE EndDate < GETDATE()", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Pollses the running.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Polls_Running()
        {
            using (var cmd = new SqlCommand("SELECT Count(PollID) AS Counter FROM PNQQuestions WHERE EndDate > GETDATE()", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Referrals the visits.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Referral_Visits()
        {
            long intCount = 0;
            using (var cmd = new SqlCommand("SELECT Visitors FROM ReferralStats WHERE Visitors > 0 ORDER BY ID", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++) intCount += SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["Visitors"]));
                return intCount;
            }
        }

        /// <summary>
        /// Shoppings the sale products.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Shopping_Sale_Products()
        {
            using (var cmd = new SqlCommand("SELECT Count(ProductID) AS Counter FROM ShopProducts WHERE SalePrice <> '0' AND SalePrice <> '0.00'", conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
                return SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[0]["Counter"]));
            }
        }

        /// <summary>
        /// Totalses the advertising.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Advertising()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Advertising") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Ads") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Advertisements")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Expired Ads") + "</td>";
            str += "<td>" + Strings.ToString(Advertising_Expired()) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Advertisements", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the albums.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Albums()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Photo Albums") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Albums") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("PhotoAlbums")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("PhotoAlbums", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the articles.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Articles()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Articles") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Posts") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Articles")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Articles", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the auctions.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Auctions()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Auctions") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Running Posts") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("AuctionAds")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("AuctionAds", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the blogger.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Blogger()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Blogs") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Blogs") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Blog")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Blog", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the businesses.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Businesses()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Business Directory") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Posts") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("BusinessListings")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("BusinessListings", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the classifieds.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Classifieds()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Classified Ads") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Running Ads") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ClassifiedsAds")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ClassifiedsAds", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the discounts.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Discounts()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Discount System") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Discounts") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("DiscountSystem")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("DiscountSystem", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the elearning.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Elearning()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("E-Learning") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Classes") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ELearnCourses")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Exams") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ELearnExams")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Students") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ELearnStudents")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Assignments") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ELearnHomework")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the events.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Events()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Event Calendar") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Events") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("EventCalendar")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("EventCalendar", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the fa qs.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_FAQs()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("FAQ's") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total FAQ's") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("FAQ")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the forums.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Forums()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Forums") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Topics") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ForumsMessages")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Replies") + "</td>";
            str += "<td>" + Strings.ToString(Forums_Replies()) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ForumsMessages", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the guestbook.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Guestbook()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Guestbook") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Entries") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Guestbook")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the libraries.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Libraries()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Downloads") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Files") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("LibrariesFiles")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("LibrariesFiles", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the links.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Links()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Links") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Posts") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("LinksWebsites")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("LinksWebsites", "UserID")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the members.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Members()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Members") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Members")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Active") + "</td>";
            str += "<td>" + Strings.ToString(Members_Active()) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Not Active") + "</td>";
            str += "<td>" + Strings.ToString(Members_Not_Active()) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the personal.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Personal()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Personal Pages") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Sites") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("UPagesSites")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the polls.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Polls()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Polls") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Polls") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("PNQQuestions")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Running Polls") + "</td>";
            str += "<td>" + Strings.ToString(Polls_Running()) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Expired Polls") + "</td>";
            str += "<td>" + Strings.ToString(Polls_Expired()) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the profiles.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Profiles()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("User Profiles") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Profiles") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Profiles")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the referral.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Referral()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Refer a Friend") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Referrals") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ReferralAddresses")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Visits") + "</td>";
            str += "<td>" + Strings.ToString(Referral_Visits()) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the shopping.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Shopping()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Shopping Mall") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Products") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("ShopProducts")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total On Sale") + "</td>";
            str += "<td>" + Strings.ToString(Shopping_Sale_Products()) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the speakers.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Speakers()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Speaker's Bureau") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Speakers") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Speakers")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Speeches") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("SpeakSpeeches")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Topics") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("SpeakTopics")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Totalses the vouchers.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Totals_Vouchers()
        {
            var str = string.Empty;
            str += "<table width=\"150\">";
            str += "<tr class=\"HeaderStyle\"><th colspan=\"2\">" + SepFunctions.LangText("Vouchers") + "</th></tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Vouchers") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Vouchers")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Unique Users") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("Vouchers", "UserID")) + "</td>";
            str += "</tr>";
            str += "<tr class=\"RowStyle\">";
            str += "<td>" + SepFunctions.LangText("Total Bought") + "</td>";
            str += "<td>" + Strings.ToString(Get_Module_Totals("VouchersPurchased")) + "</td>";
            str += "</tr>";
            str += "</table>";
            return str;
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
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
                if (da != null)
                {
                    da.Dispose();
                }

                if (ds != null)
                {
                    ds.Dispose();
                }

                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            conn = new SqlConnection(SepFunctions.Database_Connection());

            ModuleStats.InnerHtml = Module_Stats();
        }
    }
}