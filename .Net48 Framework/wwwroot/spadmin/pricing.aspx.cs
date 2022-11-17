// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="pricing.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class pricing.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class pricing : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Pricing Setup");
                    NewListingLabel.InnerText = SepFunctions.LangText("Price for a new listing:");
                    FeaturedListingLabel.InnerText = SepFunctions.LangText("Price for a featured listing:");
                    BoldTitleLabel.InnerText = SepFunctions.LangText("Price for bold title:");
                    HighlightListingLabel.InnerText = SepFunctions.LangText("Price for highlighted listing:");
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
            TranslatePage();
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSetup")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSetup"), false) == false)
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

            if (!Page.IsPostBack)
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT Description FROM ShopProducts WHERE ModuleID=@ModuleID AND ProductName='Posting Price'", conn))
                    {
                        cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")));
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                string inidata = SepFunctions.openNull(RS["Description"]);
                                NewListing.Value = SepFunctions.Format_Currency(SepFunctions.ParseXML("NEWLISTING", inidata));
                                FeaturedListing.Value = SepFunctions.Format_Currency(SepFunctions.ParseXML("FEATURELISTING", inidata));
                                BoldTitle.Value = SepFunctions.Format_Currency(SepFunctions.ParseXML("BOLDTITLE", inidata));
                                HighlightListing.Value = SepFunctions.Format_Currency(SepFunctions.ParseXML("HIGHLIGHTLISTING", inidata));
                            }
                            else
                            {
                                NewListing.Value = SepFunctions.Format_Currency("0");
                                FeaturedListing.Value = SepFunctions.Format_Currency("0");
                                BoldTitle.Value = SepFunctions.Format_Currency("0");
                                HighlightListing.Value = SepFunctions.Format_Currency("0");
                            }

                        }
                    }
                }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var bUpdate = false;

            string inidata = "<NEWLISTING>" + NewListing.Value + "</NEWLISTING>";
            inidata += "<FEATURELISTING>" + FeaturedListing.Value + "</FEATURELISTING>";
            inidata += "<BOLDTITLE>" + BoldTitle.Value + "</BOLDTITLE>";
            inidata += "<HIGHLIGHTLISTING>" + HighlightListing.Value + "</HIGHLIGHTLISTING>";

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT Description FROM ShopProducts WHERE ModuleID=@ModuleID AND ProductName='Posting Price'", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")));
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows) bUpdate = true;
                    }
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                    SqlStr = "UPDATE ShopProducts SET Description=@Description WHERE ModuleID=@ModuleID AND ProductName='Posting Price'";
                else
                    SqlStr = "INSERT INTO ShopProducts (ProductID, ProductName, ModuleID, DatePosted, PortalID, Description, AffiliateUnitPrice, AffiliateRecurringPrice, ExcludeAffiliate, CatID) VALUES (@ProductID, @ProductName, @ModuleID, @DatePosted, @PortalID, @Description, '0', '0', '0', '0')";
                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", SepFunctions.GetIdentity());
                    cmd.Parameters.AddWithValue("@ProductName", "Posting Price");
                    cmd.Parameters.AddWithValue("@ModuleID", SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID")));
                    cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                    cmd.Parameters.AddWithValue("@Description", inidata);
                    cmd.ExecuteNonQuery();
                }
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Pricing settings have been successfully saved.") + "</div>";
        }
    }
}