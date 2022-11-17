// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ChangeLog.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class ChangeLog.
    /// </summary>
    public static class ChangeLog
    {
        /// <summary>
        /// Gets the change logs.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns>List&lt;Models.ChangeLog&gt;.</returns>
        public static List<Models.ChangeLog> GetChangeLogs(int ModuleID, string SortExpression = "DateChanged", string SortDirection = "DESC")
        {
            var lChangeLog = new List<Models.ChangeLog>();

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "DateChanged";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT ChangeID,UniqueID,ModuleID,Subject,DateChanged FROM ChangeLog WHERE ModuleID='" + ModuleID + "' ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dChangeLog = new Models.ChangeLog { ChangeID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"])) };
                    dChangeLog.ModuleID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]));
                    dChangeLog.UniqueID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]));
                    dChangeLog.ModuleName = SepFunctions.GetModuleName(SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"])));
                    dChangeLog.Subject = SepFunctions.openNull(ds.Tables[0].Rows[i]["Subject"]);
                    dChangeLog.DateChanged = SepFunctions.toDate(SepFunctions.openNull(ds.Tables[0].Rows[i]["DateChanged"]));
                    switch (SepFunctions.toInt(SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"])))
                    {
                        case 5:
                            dChangeLog.ViewURL = "discounts_modify.aspx?DiscountID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 9:
                            dChangeLog.ViewURL = "faq_modify.aspx?FAQID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 10:
                            dChangeLog.ViewURL = "downloads_modify.aspx?FileID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 14:
                            dChangeLog.ViewURL = "guestbook_modify.aspx?EntryID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 18:
                            dChangeLog.ViewURL = "matchmaker_modify.aspx?ProfileID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 19:
                            dChangeLog.ViewURL = "linkdirectory_modify.aspx?LinkID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 20:
                            dChangeLog.ViewURL = "business_modify.aspx?BusinessID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 23:
                            dChangeLog.ViewURL = "news_modify.aspx?NewsID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 31:
                            dChangeLog.ViewURL = "auction_modify.aspx?AdID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 32:
                            dChangeLog.ViewURL = "realestate_modify.aspx?PropertyID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 35:
                            dChangeLog.ViewURL = "articles_modify.aspx?ArticleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 37:
                            dChangeLog.ViewURL = "elearning_modify.aspx?CourseID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 41:
                            dChangeLog.ViewURL = "shoppingmall_modify.aspx?ProductID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 44:
                            dChangeLog.ViewURL = "classifiedads_modify.aspx?AdID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 46:
                            dChangeLog.ViewURL = "eventcalendar_modify.aspx?EventID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 61:
                            dChangeLog.ViewURL = "blogs_modify.aspx?BlogID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 63:
                            dChangeLog.ViewURL = "userprofiles_modify.aspx?ProfileID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;

                        case 65:
                            dChangeLog.ViewURL = "vouchers_modify.aspx?VoucherID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["UniqueID"]) + "&ChangeID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ChangeID"]) + "&ModuleID=" + SepFunctions.openNull(ds.Tables[0].Rows[i]["ModuleID"]);
                            break;
                    }

                    lChangeLog.Add(dChangeLog);
                }
            }

            return lChangeLog;
        }
    }
}