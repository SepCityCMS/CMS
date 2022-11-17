// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="stocks.aspx.cs" company="SepCity, Inc.">
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
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class stocks.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class stocks : Page
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Stock Quote");
                    StockSymbolsLabel.InnerText = SepFunctions.LangText("Stock Symbols (Add multiple quotes by seperating them by comma's):");
                    StockSymbolsRequired.ErrorMessage = SepFunctions.LangText("~~Stock Symbols~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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
            var sInstallFolder = SepFunctions.GetInstallFolder();
            var StocksUserQuotes = string.Empty;

            TranslatePage();

            GlobalVars.ModuleID = 15;

            if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && Response.IsClientConnected)
            {
                SepCommon.SepCore.Session.setCookie("returnUrl", SepFunctions.GetInstallFolder() + "stocks.aspx");
                SepFunctions.Redirect(sInstallFolder + "login.aspx");
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var cReplace = new Replace();

            PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

            cReplace.Dispose();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Stocks WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            StocksUserQuotes = SepFunctions.openNull(RS["Symbols"]);
                        }
                        else
                        {
                            StocksUserQuotes = SepFunctions.Setup(GlobalVars.ModuleID, "StocksSymbols");
                        }

                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(StocksUserQuotes)) StockQuotes.InnerHtml = "<p align=\"center\"><iframe width=\"160\" height=\"120\" scrolling=\"no\" frameborder=\"0\" style=\"border:none;\" src=\"http://widgets.freestockcharts.com/WidgetServer/WatchListWidget.aspx?sym=" + StocksUserQuotes + "&style=WLBlueStyle&w=160\"></iframe></p>";

            if (!Page.IsPostBack) StockSymbols.Value = StocksUserQuotes;
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var addNewRecord = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT *  FROM Stocks WHERE UserID=@UserID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows) addNewRecord = true;
                    }
                }

                if (addNewRecord)
                    using (var cmd = new SqlCommand("INSERT INTO Stocks (UserID,Symbols) VALUES(@UserID, @StockSymbols)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.Parameters.AddWithValue("@StockSymbols", StockSymbols.Value);
                        cmd.ExecuteNonQuery();
                    }
                else
                    using (var cmd = new SqlCommand("UPDATE Stocks SET Symbols=@StockSymbols WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.Parameters.AddWithValue("@StockSymbols", StockSymbols.Value);
                        cmd.ExecuteNonQuery();
                    }
            }

            StockQuotes.InnerHtml = "<p align=\"center\"><iframe width=\"160\" height=\"120\" scrolling=\"no\" frameborder=\"0\" style=\"border:none;\" src=\"http://widgets.freestockcharts.com/WidgetServer/WatchListWidget.aspx?sym=" + StockSymbols.Value + "&style=WLBlueStyle&w=160\"></iframe></p>";

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Stock quote(s) have been successfully saved.") + "</div>";
        }
    }
}