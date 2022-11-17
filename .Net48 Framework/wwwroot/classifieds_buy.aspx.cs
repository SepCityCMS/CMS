// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="classifieds_buy.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class classifieds_buy.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class classifieds_buy : Page
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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 44;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "ClassifiedEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ClassifiedAccess"));

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "classifieds_buy.aspx?AdID=" + SepCommon.SepCore.Request.Item("AdID") + "&Quantity=" + SepCommon.SepCore.Request.Item("Quantity"));
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("AdID")))
            {
                var jAds = SepCommon.DAL.Classifieds.Classified_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

                if (jAds.AdID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Business~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
                else
                {
                    if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostPurchaseItem", "GetPurchaseItem", SepCommon.SepCore.Request.Item("AdID"), false) == false)
                    {
                        SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                        return;
                    }

                    if (jAds.UserID == SepFunctions.Session_User_ID())
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You cannot purchase your own item.") + "</div>";
                        DisplayContent.Visible = false;
                        return;
                    }

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT Count(AdID) AS Quantity FROM ClassifiedsAds WHERE LinkID=@AdID AND SoldOut='0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", SepCommon.SepCore.Request.Item("AdID"));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("Quantity")) > SepFunctions.toInt(SepFunctions.openNull(RS["Quantity"])) || SepFunctions.toInt(SepCommon.SepCore.Request.Item("Quantity")) == 0)
                                    {
                                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You can not order more items then what is available.") + "</div>";
                                        DisplayContent.Visible = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You can not order more items then what is available.") + "</div>";
                                    DisplayContent.Visible = false;
                                    return;
                                }

                            }
                        }
                    }

                    decimal TotalPrice = jAds.Price * SepFunctions.toInt(SepCommon.SepCore.Request.Item("Quantity"));
                    TotalAdPrice.InnerHtml = SepFunctions.Format_Currency(TotalPrice);
                }
            }
            else
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Classified Ad~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
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
        /// Handles the Click event of the OrderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void OrderButton_Click(object sender, EventArgs e)
        {
            var GetQuantity = 0;
            var GetAdID = string.Empty;

            var jAds = SepCommon.DAL.Classifieds.Classified_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("AdID")));

            if (jAds.AdID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Classified Ad~~ does not exist.") + "</div>";
                DisplayContent.Visible = false;
            }
            else
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT Count(AdID) AS Quantity FROM ClassifiedsAds WHERE LinkID=@AdID AND SoldOut='0'", conn))
                    {
                        cmd.Parameters.AddWithValue("@AdID", SepCommon.SepCore.Request.Item("AdID"));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                GetQuantity = SepFunctions.toInt(SepFunctions.openNull(RS["Quantity"]));
                            }

                        }
                    }

                    for (var i = 1; i <= SepFunctions.toInt(SepCommon.SepCore.Request.Item("Quantity")); i++)
                    {
                        using (var cmd = new SqlCommand("SELECT AdID FROM ClassifiedsAds WHERE LinkID=@AdID AND SoldOut='0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", SepCommon.SepCore.Request.Item("AdID"));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetAdID = SepFunctions.openNull(RS["AdID"]);
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(GetAdID))
                    {
                        using (var cmd = new SqlCommand("SELECT AdID FROM ClassifiedsAds WHERE LinkID=@AdID AND AdID <> @AdID AND SoldOut='0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@AdID", GetAdID);
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (!RS.HasRows)
                                {
                                    using (var cmd2 = new SqlCommand("SELECT AdID FROM ClassifiedsAds WHERE AdID=@AdID AND SoldOut='0'", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@AdID", GetAdID);
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (!RS2.HasRows)
                                            {
                                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This Item has been sold.") + "</div>";
                                                DisplayContent.Visible = false;
                                                return;
                                            }

                                            if (GetQuantity > 0)
                                            {
                                                using (var cmd3 = new SqlCommand("UPDATE ClassifiedsAds SET SoldUserID='" + SepFunctions.Session_User_ID() + "',SoldOut='1',SoldDate=GETDATE() WHERE AdID=@AdID AND SoldOut='0'", conn))
                                                {
                                                    cmd3.Parameters.AddWithValue("@AdID", GetAdID);
                                                    cmd3.ExecuteNonQuery();
                                                }
                                            }
                                            else
                                            {
                                                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This Item has been sold.") + "</div>";
                                                DisplayContent.Visible = false;
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (GetQuantity > 0)
                                    {
                                        using (var cmd2 = new SqlCommand("UPDATE ClassifiedsAds SET SoldUserID='" + SepFunctions.Session_User_ID() + "',SoldOut='1',SoldDate=GETDATE() WHERE AdID=@AdID AND AdID <> @AdID AND SoldOut='0'", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@AdID", GetAdID);
                                            cmd2.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This Item has been sold.") + "</div>";
                                        DisplayContent.Visible = false;
                                        return;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("This Item has been sold.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                }

                string sActDesc = SepFunctions.LangText("[[Username]] has bought a classified ad") + Environment.NewLine;
                sActDesc += SepFunctions.LangText("Price: ~~" + SepFunctions.Format_Currency(jAds.Price) + "~~") + Environment.NewLine;
                SepFunctions.Activity_Write("BUYCLASSAD", sActDesc, GlobalVars.ModuleID, SepCommon.SepCore.Request.Item("AdID"));
            }

            var strPayPal = jAds.PayPalEmail;

            DisplayContent.Visible = false;

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully submitted your order to the seller.");
            ErrorMessage.InnerHtml += "<p align=\"center\"><a href=\"messenger_compose.aspx?UserID=" + SepFunctions.UrlEncode(jAds.UserID) + "\">" + SepFunctions.LangText("Contact the Seller") + "</a></p>";
            if (!string.IsNullOrWhiteSpace(strPayPal)) ErrorMessage.InnerHtml += "<p align=\"center\"><a href=\"https://www.paypal.com/spadmin/webscr?cmd=_xclick&business=" + SepFunctions.UrlEncode(strPayPal) + "&item_name=" + SepFunctions.UrlEncode(jAds.Title) + "&item_number=" + SepCommon.SepCore.Request.Item("AdID") + "&amount=" + SepFunctions.UrlEncode(Strings.ToString(jAds.Price)) + "&no_shipping=0&no_note=1\" target=\"_blank\">" + SepFunctions.LangText("Pay seller via PayPal") + "</a></p>";
            ErrorMessage.InnerHtml += "</div>";
        }
    }
}