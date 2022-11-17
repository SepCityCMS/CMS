// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_modify.aspx.cs" company="SepCity, Inc.">
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
    /// Class portals_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_modify : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Portal");
                    CategoryLabel.InnerText = SepFunctions.LangText("Select a Category in the box below:");
                    PortalNameLabel.InnerText = SepFunctions.LangText("Portal Name:");
                    DescriptionLabel.InnerText = SepFunctions.LangText("Description:");
                    DomainNameLabel.InnerText = SepFunctions.LangText("Domain Name:");
                    LanguageLabel.InnerText = SepFunctions.LangText("Portal Language:");
                    TemplateLabel.InnerText = SepFunctions.LangText("Portal Template:");
                    SiteLogoLabel.InnerText = SepFunctions.LangText("Site Logo:");
                    LoginKeysLabel.InnerText = SepFunctions.LangText("Keys to login to this portal:");
                    ManageKeysLabel.InnerText = SepFunctions.LangText("Keys to manage this portal:");
                    HidePortal.Text = SepFunctions.LangText("Hide portal from showing on directory list.");
                    PortalNameRequired.ErrorMessage = SepFunctions.LangText("~~Portal Name~~ is required.");
                    DescriptionRequired.ErrorMessage = SepFunctions.LangText("~~Description~~ is required.");
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

            GlobalVars.ModuleID = 60;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("PortalsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false) == false)
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

            MainDomain.InnerHtml = SepFunctions.GetMasterDomain(false);

            if (!Page.IsPostBack)
            {
                var dPortals = SepCommon.DAL.Portals.GetPortalPlans();
                if (dPortals.Count > 0)
                {
                    PricePlan.Items.Add(new ListItem("None (Access to all Available Modules)", string.Empty));
                    for (var i = 0; i < dPortals.Count; i++) PricePlan.Items.Add(new ListItem(dPortals[i].PlanName + " (" + SepFunctions.Format_Long_Price(SepFunctions.toDecimal(dPortals[i].OnetimePrice), SepFunctions.toDecimal(dPortals[i].RecurringPrice), dPortals[i].RecurringCycle) + ")", Strings.ToString(dPortals[i].PlanID)));
                }
                else
                {
                    PricePlanRow.Visible = false;
                }
            }

            if (!Page.IsPostBack && SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")) > 0)
            {
                var jPortals = SepCommon.DAL.Portals.Portal_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")));

                if (jPortals.PortalID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Portal~~ does not exist.") + "</div>";
                    ModFormDiv.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Portal");
                    PortalID.Value = SepCommon.SepCore.Request.Item("PortalID");
                    Category.CatID = Strings.ToString(jPortals.CatID);
                    PortalName.Value = jPortals.PortalTitle;
                    Description.Value = jPortals.Description;
                    DomainName.Value = jPortals.DomainName;
                    Language.Text = jPortals.Language;
                    Template.Text = jPortals.Template;
                    LoginKeys.Text = jPortals.LoginKeys;
                    ManageKeys.Text = jPortals.ManageKeys;
                    FriendlyName.Value = jPortals.FriendlyName;
                    HidePortal.Checked = jPortals.HideList;
                    PricePlan.Value = Strings.ToString(jPortals.PlanID);
                    Status.Value = Strings.ToString(jPortals.Status);
                    UserID.Value = jPortals.UserID;
                    UserName.Value = jPortals.UserName;

                    if (Strings.Len(jPortals.SiteLogoURL) > 0)
                    {
                        SiteLogoImg.ImageUrl = jPortals.SiteLogoURL;
                        SiteLogoImg.Style.Add("display", "block");
                        SiteLogoLabel.InnerText = SepFunctions.LangText("Replace Website Logo");
                        SiteLogoImg.Visible = true;
                        RemoveSiteLogo.Visible = true;
                    }
                    else
                    {
                        SiteLogoImg.Visible = false;
                        RemoveSiteLogo.Visible = false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(PortalID.Value)) PortalID.Value = Strings.ToString(SepFunctions.GetIdentity());

                if (!Page.IsPostBack)
                {
                    RemoveSiteLogo.Visible = false;
                    UserID.Value = SepFunctions.Session_User_ID();
                    UserName.Value = SepFunctions.Session_User_Name();
                }
                else
                {
                    if (SepFunctions.toLong(PortalID.Value) > 0)
                    {
                        var jPortals = SepCommon.DAL.Portals.Portal_Get(SepFunctions.toLong(PortalID.Value));
                        if (jPortals.PortalID > 0) ManageKeys.Text = jPortals.ManageKeys;
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
            var imageData = string.Empty;
            var ImageFileName = string.Empty;
            var imageContentType = string.Empty;

            if (!string.IsNullOrWhiteSpace(SepCommon.DAL.Portals.Validate_Friendly_Name(SepFunctions.toLong(PortalID.Value), FriendlyName.Value)))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepCommon.DAL.Portals.Validate_Friendly_Name(SepFunctions.toLong(PortalID.Value), FriendlyName.Value) + "</div>";
                return;
            }

            if (RemoveSiteLogo.Checked)
            {
                imageData = string.Empty;
                SiteLogoImg.Visible = false;
                RemoveSiteLogo.Visible = false;
                RemoveSiteLogo.Checked = false;
            }
            else
            {
                if (SiteLogo.PostedFile == null || string.IsNullOrWhiteSpace(SiteLogo.PostedFile.FileName))
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT FileName,ContentType,FileData FROM Uploads WHERE ModuleID='60' AND UniqueID='" + SepFunctions.FixWord(PortalID.Value) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    try
                                    {
                                        if (!SepCommon.SepCore.Information.IsDBNull(RS["FileData"]))
                                        {
                                            SiteLogoImg.ImageUrl = "data:image/png;base64," + SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"]));
                                        }
                                        SiteLogoImg.Style.Add("display", "block");
                                        SiteLogoLabel.InnerText = SepFunctions.LangText("Replace Website Logo");
                                        SiteLogoImg.Visible = true;
                                        RemoveSiteLogo.Visible = true;
                                        if (!SepCommon.SepCore.Information.IsDBNull(RS["FileData"]))
                                        {
                                            imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString((byte[])RS["FileData"]));
                                        }
                                        imageContentType = SepFunctions.openNull(RS["ContentType"]);
                                        ImageFileName = SepFunctions.openNull(RS["FileName"]);
                                    }
                                    catch
                                    {
                                        SiteLogoImg.Visible = false;
                                        RemoveSiteLogo.Visible = false;
                                    }
                                }
                                else
                                {
                                    SiteLogoImg.Visible = false;
                                    RemoveSiteLogo.Visible = false;
                                }

                            }
                        }
                    }
                else
                    try
                    {
                        var imageBytes = new byte[SepFunctions.toInt(Strings.ToString(SiteLogo.PostedFile.InputStream.Length)) + 1];
                        SiteLogo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                        imageData = SepFunctions.Base64Encode(SepFunctions.BytesToString(imageBytes));
                        ImageFileName = SiteLogo.PostedFile.FileName;
                        imageContentType = SiteLogo.PostedFile.ContentType;

                        if (Strings.Len(imageData) > 0)
                        {
                            SiteLogoImg.ImageUrl = "data:image/png;base64," + imageData;
                            SiteLogoImg.Style.Add("display", "block");
                            SiteLogoLabel.InnerText = SepFunctions.LangText("Replace Website Logo");
                            SiteLogoImg.Visible = true;
                            RemoveSiteLogo.Visible = true;
                        }
                        else
                        {
                            SiteLogoImg.Visible = false;
                            RemoveSiteLogo.Visible = false;
                        }
                    }
                    catch
                    {
                        SiteLogoImg.Visible = false;
                        RemoveSiteLogo.Visible = false;
                    }
            }

            var intReturn = SepCommon.DAL.Portals.Portal_Save(SepFunctions.toLong(PortalID.Value), PortalName.Value, Description.Value, SepFunctions.toLong(Category.CatID), DomainName.Value, ManageKeys.Text, LoginKeys.Text, HidePortal.Checked, UserID.Value, Language.Text, Template.Text, ImageFileName, imageData, imageContentType, SepFunctions.toInt(Status.Value), SepFunctions.toLong(PricePlan.Value), FriendlyName.Value);

            Globals.Save_Template(Template.Text, false, 60, SepFunctions.toLong(PortalID.Value));

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }
    }
}