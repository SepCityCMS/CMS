// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="image_sizing.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class image_sizing.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class image_sizing : Page
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
        /// Populates the image sizes.
        /// </summary>
        public void Populate_Image_Sizes()
        {
            XmlDocument doc = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "image_sizes.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    doc.Load(reader);

                    // Select the book node with the matching attribute value.
                    var root = doc.DocumentElement;

                    // Articles
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/Enable").InnerText != "Yes")
                            Enable35.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/Height") != null) Height35.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE35/Width") != null) Width35.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE35/Width").InnerText;

                    // Auction
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/Enable").InnerText != "Yes")
                            Enable31.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/Height") != null) Height31.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE31/Width") != null) Width31.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE31/Width").InnerText;

                    // Business Directory
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/Enable").InnerText != "Yes")
                            Enable20.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/Height") != null) Height20.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE20/Width") != null) Width20.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE20/Width").InnerText;

                    // Classified Ads
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/Enable").InnerText != "Yes")
                            Enable44.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/Height") != null) Height44.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE44/Width") != null) Width44.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE44/Width").InnerText;

                    // Downloads
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/Enable").InnerText != "Yes")
                            Enable10.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/Height") != null) Height10.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE10/Width") != null) Width10.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE10/Width").InnerText;

                    // Event Calendar
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/Enable").InnerText != "Yes")
                            Enable46.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/Height") != null) Height46.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE46/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE46/Width") != null) Width46.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE46/Width").InnerText;

                    // Match Maker
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/Enable").InnerText != "Yes")
                            Enable18.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/Height") != null) Height18.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE18/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE18/Width") != null) Width18.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE18/Width").InnerText;

                    // News
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/Enable").InnerText != "Yes")
                            Enable23.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/Height") != null) Height23.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE23/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE23/Width") != null) Width23.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE23/Width").InnerText;

                    // Photo Albums
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/Enable").InnerText != "Yes")
                            Enable28.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/Height") != null) Height28.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE28/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE28/Width") != null) Width28.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE28/Width").InnerText;

                    // Real Estate
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/Enable").InnerText != "Yes")
                            Enable32.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/Height") != null) Height32.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE32/Width") != null) Width32.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE32/Width").InnerText;

                    // Shopping Mall
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/Enable").InnerText != "Yes")
                            Enable41.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/Height") != null) Height41.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE41/Width") != null) Width41.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE41/Width").InnerText;

                    // Speakers Bureau
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/Enable").InnerText != "Yes")
                            Enable50.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/Height") != null) Height50.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE50/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE50/Width") != null) Width50.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE50/Width").InnerText;

                    // User Profiles
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/Enable") != null)
                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/Enable").InnerText != "Yes")
                            Enable63.Checked = false;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/Height") != null) Height63.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/Height").InnerText;
                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE63/Width") != null) Width63.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE63/Width").InnerText;
                }
            }
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
                    ArticlesLabel.InnerText = SepFunctions.LangText("Articles:");
                    AuctionLabel.InnerText = SepFunctions.LangText("Auction:");
                    BusinessLabel.InnerText = SepFunctions.LangText("Business Directory:");
                    ClassifiedLabel.InnerText = SepFunctions.LangText("Classified Ads:");
                    DownloadsLabel.InnerText = SepFunctions.LangText("Downloads:");
                    EventsLabel.InnerText = SepFunctions.LangText("Event Calendar:");
                    MatchLabel.InnerText = SepFunctions.LangText("Match Maker:");
                    NewsLabel.InnerText = SepFunctions.LangText("News:");
                    PhotoAlbumsLabel.InnerText = SepFunctions.LangText("Photo Albums:");
                    RealEstateLabel.InnerText = SepFunctions.LangText("Real Estate:");
                    ShopMallLabel.InnerText = SepFunctions.LangText("Shopping Mall:");
                    SpeakersLabel.InnerText = SepFunctions.LangText("Speaker's Bureau:");
                    UserProfilesLabel.InnerText = SepFunctions.LangText("User Profiles:");
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), true) == false)
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
            {
                if (SepFunctions.ModuleActivated(35) == false) ArticlesRow.Visible = false;
                if (SepFunctions.ModuleActivated(31) == false) AuctionRow.Visible = false;
                if (SepFunctions.ModuleActivated(20) == false) BusinessesRow.Visible = false;
                if (SepFunctions.ModuleActivated(44) == false) ClassifiedsRow.Visible = false;
                if (SepFunctions.ModuleActivated(10) == false) DownloadsRow.Visible = false;
                if (SepFunctions.ModuleActivated(46) == false) EventCalendarRow.Visible = false;
                if (SepFunctions.ModuleActivated(18) == false) MatchMakerRow.Visible = false;
                if (SepFunctions.ModuleActivated(23) == false) NewsRow.Visible = false;
                if (SepFunctions.ModuleActivated(28) == false) AlbumsRow.Visible = false;
                if (SepFunctions.ModuleActivated(32) == false) RealEstateRow.Visible = false;
                if (SepFunctions.ModuleActivated(41) == false) ShoppingRow.Visible = false;
                if (SepFunctions.ModuleActivated(50) == false) SpeakersRow.Visible = false;
                if (SepFunctions.ModuleActivated(63) == false) UserProfilesRow.Visible = false;

                try
                {
                    Populate_Image_Sizes();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SetupSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SetupSave_Click(object sender, EventArgs e)
        {
            var strXml = string.Empty;

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;

            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<MODULE35>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable35.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height35.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width35.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE35>" + Environment.NewLine;

            strXml += "<MODULE31>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable31.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height31.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width31.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE31>" + Environment.NewLine;

            strXml += "<MODULE20>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable20.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height20.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width20.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE20>" + Environment.NewLine;

            strXml += "<MODULE44>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable44.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height44.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width44.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE44>" + Environment.NewLine;

            strXml += "<MODULE10>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable10.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height10.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width10.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE10>" + Environment.NewLine;

            strXml += "<MODULE46>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable46.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height46.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width46.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE46>" + Environment.NewLine;

            strXml += "<MODULE18>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable18.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height18.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width18.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE18>" + Environment.NewLine;

            strXml += "<MODULE23>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable23.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height23.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width23.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE23>" + Environment.NewLine;

            strXml += "<MODULE28>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable28.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height28.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width28.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE28>" + Environment.NewLine;

            strXml += "<MODULE32>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable32.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height32.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width32.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE32>" + Environment.NewLine;

            strXml += "<MODULE41>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable41.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height41.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width41.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE41>" + Environment.NewLine;

            strXml += "<MODULE50>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable50.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height50.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width50.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE50>" + Environment.NewLine;

            strXml += "<MODULE63>" + Environment.NewLine;
            strXml += "<Enable>" + Strings.ToString(Enable63.Checked ? "Yes" : "No") + "</Enable>" + Environment.NewLine;
            strXml += "<Height>" + SepFunctions.toLong(Height63.Value) + "</Height>" + Environment.NewLine;
            strXml += "<Width>" + SepFunctions.toLong(Width63.Value) + "</Width>" + Environment.NewLine;
            strXml += "</MODULE63>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "image_sizes.xml"))
            {
                outfile.Write(strXml);
            }

            SaveMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings successfully saved.") + "</div>";
        }
    }
}