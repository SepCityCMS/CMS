// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="portals_setup.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class portals_setup.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class portals_setup : Page
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
                    SiteLang.Items[0].Text = SepFunctions.LangText("English (United States)");
                    SiteLang.Items[1].Text = SepFunctions.LangText("Dutch (The Netherlands)");
                    SiteLang.Items[2].Text = SepFunctions.LangText("French (Canada)");
                    SiteLang.Items[3].Text = SepFunctions.LangText("French (France)");
                    SiteLang.Items[4].Text = SepFunctions.LangText("Malaya (Malaysia)");
                    SiteLang.Items[5].Text = SepFunctions.LangText("Portuguese (Brazil)");
                    SiteLang.Items[6].Text = SepFunctions.LangText("Russian (Russia)");
                    SiteLang.Items[7].Text = SepFunctions.LangText("Spanish (Mexico)");
                    SiteLang.Items[8].Text = SepFunctions.LangText("Spanish (Spain)");
                    FullNameLabel.InnerText = SepFunctions.LangText("Full Name:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    CompanyNameLabel.InnerText = SepFunctions.LangText("Company Name:");
                    CompanySloganLabel.InnerText = SepFunctions.LangText("Company Slogan:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    CompanyStateLabel.InnerText = SepFunctions.LangText("State your company is located in:");
                    CompanyZipCodeLabel.InnerText = SepFunctions.LangText("Your company zip/postal code:");
                    CompanyCountryLabel.InnerText = SepFunctions.LangText("Your company country:");
                    SiteLangLabel.InnerText = SepFunctions.LangText("Select your primary language:");
                    Menu1TextLabel.InnerText = SepFunctions.LangText("Menu 1 Text:");
                    Menu2TextLabel.InnerText = SepFunctions.LangText("Menu 2 Text:");
                    Menu3TextLabel.InnerText = SepFunctions.LangText("Menu 3 Text:");
                    Menu4TextLabel.InnerText = SepFunctions.LangText("Menu 4 Text:");
                    Menu5TextLabel.InnerText = SepFunctions.LangText("Menu 5 Text:");
                    Menu6TextLabel.InnerText = SepFunctions.LangText("Menu 6 Text:");
                    Menu7TextLabel.InnerText = SepFunctions.LangText("Menu 7 Text:");
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

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), true) == false)
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
                var jPortals = SepCommon.DAL.Portals.Portal_Get(SepFunctions.Get_Portal_ID());

                Category.CatID = Strings.ToString(jPortals.CatID);
                PortalName.Value = jPortals.PortalTitle;
                Description.Value = jPortals.Description;
                FriendlyName.Value = jPortals.FriendlyName;
                HidePortal.Checked = jPortals.HideList;
                UserID.Value = jPortals.UserID;

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

                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-" + SepFunctions.Get_Portal_ID() + ".xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/FullName") != null) FullName.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/FullName").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/EmailAddress") != null) EmailAddress.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/EmailAddress").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/CompanyName") != null) CompanyName.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/CompanyName").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/CompanySlogan") != null) CompanySlogan.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/CompanySlogan").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/Street") != null) StreetAddress.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/Street").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/City") != null) City.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/City").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/State") != null) CompanyState.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/State").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/PostalCode") != null) CompanyZipCode.Value = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/PostalCode").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/AdminInfo/Country") != null) CompanyCountry.Text = root.SelectSingleNode("/ROOTLEVEL/AdminInfo/Country").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteSetup/SiteLang") != null) SiteLang.Value = root.SelectSingleNode("/ROOTLEVEL/SiteSetup/SiteLang").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu1") != null) Menu1Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu1").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu2") != null) Menu2Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu2").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu3") != null) Menu3Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu3").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu4") != null) Menu4Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu4").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu5") != null) Menu5Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu5").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu6") != null) Menu6Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu6").InnerText;

                        if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu7") != null) Menu7Text.Value = root.SelectSingleNode("/ROOTLEVEL/SiteLayout/SiteMenu7").InnerText;
                    }
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
            var imageData = string.Empty;
            var ImageFileName = string.Empty;
            var imageContentType = string.Empty;
            var PortalID = SepFunctions.Get_Portal_ID();

            if (!string.IsNullOrWhiteSpace(SepCommon.DAL.Portals.Validate_Friendly_Name(PortalID, FriendlyName.Value)))
            {
                SaveMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepCommon.DAL.Portals.Validate_Friendly_Name(PortalID, FriendlyName.Value) + "</div>";
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
                        using (var cmd = new SqlCommand("SELECT FileName,ContentType,FileData FROM Uploads WHERE ModuleID='60' AND UniqueID='" + SepFunctions.FixWord(Strings.ToString(PortalID)) + "'", conn))
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

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Portals SET PortalTitle=@PortalTitle, CatID=@CatID, Description=@Description, HideList=@HideList, FriendlyName=@FriendlyName WHERE PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.Parameters.AddWithValue("@CatID", SepFunctions.toLong(Category.CatID));
                    cmd.Parameters.AddWithValue("@PortalTitle", PortalName.Value);
                    cmd.Parameters.AddWithValue("@Description", Description.Value);
                    cmd.Parameters.AddWithValue("@HideList", HidePortal.Checked);
                    cmd.Parameters.AddWithValue("@FriendlyName", FriendlyName.Value);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE ModuleID='60' AND UniqueID=@UniqueID AND PortalID=@PortalID", conn))
                {
                    cmd.Parameters.AddWithValue("@UniqueID", PortalID);
                    cmd.Parameters.AddWithValue("@PortalID", PortalID);
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrWhiteSpace(imageData))
                    try
                    {
                        using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID,UniqueID,PortalID,UserID,ModuleID,FileName,FileSize,ContentType,isTemp,Approved,DatePosted,FileData) VALUES(@UploadID,@UniqueID,@PortalID,@UserID,'60',@FileName,@FileSize,@ContentType,'0','1',@DatePosted,@FileData)", conn))
                        {
                            cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@UniqueID", PortalID);
                            cmd.Parameters.AddWithValue("@PortalID", PortalID);
                            cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                            cmd.Parameters.AddWithValue("@FileName", ImageFileName);
                            cmd.Parameters.AddWithValue("@FileSize", imageData.Length);
                            cmd.Parameters.AddWithValue("@ContentType", imageContentType);
                            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                            cmd.Parameters.AddWithValue("@FileData", SepFunctions.StringToBytes(SepFunctions.Base64Decode(imageData)));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                    }
            }

            SepFunctions.Additional_Data_Save(false, 60, Strings.ToString(PortalID), UserID.Value, SepFunctions.GetUserInformation("UserName", UserID.Value), "Portal", "Portals", string.Empty);

            XmlDocument doc = new XmlDocument() { XmlResolver = null };

            using (var reader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-" + SepFunctions.Get_Portal_ID() + ".xml"))
            {
                doc.Load(reader);
            }

            var root = doc.DocumentElement;

            string strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<AdminInfo>" + Environment.NewLine;
            strXml += "<FullName>" + SepFunctions.HTMLEncode(FullName.Value) + "</FullName>" + Environment.NewLine;
            strXml += "<EmailAddress>" + SepFunctions.HTMLEncode(EmailAddress.Value) + "</EmailAddress>" + Environment.NewLine;
            strXml += "<CompanyName>" + SepFunctions.HTMLEncode(CompanyName.Value) + "</CompanyName>" + Environment.NewLine;
            strXml += "<CompanySlogan>" + SepFunctions.HTMLEncode(CompanySlogan.Value) + "</CompanySlogan>" + Environment.NewLine;
            strXml += "<Street>" + SepFunctions.HTMLEncode(StreetAddress.Value) + "</Street>" + Environment.NewLine;
            strXml += "<City>" + SepFunctions.HTMLEncode(City.Value) + "</City>" + Environment.NewLine;
            strXml += "<State>" + SepFunctions.HTMLEncode(CompanyState.Value) + "</State>" + Environment.NewLine;
            strXml += "<PostalCode>" + SepFunctions.HTMLEncode(CompanyZipCode.Value) + "</PostalCode>" + Environment.NewLine;
            strXml += "<Country>" + SepFunctions.HTMLEncode(CompanyCountry.Text) + "</Country>" + Environment.NewLine;
            strXml += "</AdminInfo>" + Environment.NewLine;

            strXml += "<SiteSetup>" + Environment.NewLine;
            strXml += "<SiteLang>" + SepFunctions.HTMLEncode(SiteLang.Value) + "</SiteLang>" + Environment.NewLine;
            strXml += "</SiteSetup>" + Environment.NewLine;

            strXml += "<SiteLayout>" + Environment.NewLine;
            strXml += "<SiteMenu1>" + SepFunctions.HTMLEncode(Menu1Text.Value) + "</SiteMenu1>" + Environment.NewLine;
            strXml += "<SiteMenu2>" + SepFunctions.HTMLEncode(Menu2Text.Value) + "</SiteMenu2>" + Environment.NewLine;
            strXml += "<SiteMenu3>" + SepFunctions.HTMLEncode(Menu3Text.Value) + "</SiteMenu3>" + Environment.NewLine;
            strXml += "<SiteMenu4>" + SepFunctions.HTMLEncode(Menu4Text.Value) + "</SiteMenu4>" + Environment.NewLine;
            strXml += "<SiteMenu5>" + SepFunctions.HTMLEncode(Menu5Text.Value) + "</SiteMenu5>" + Environment.NewLine;
            strXml += "<SiteMenu6>" + SepFunctions.HTMLEncode(Menu6Text.Value) + "</SiteMenu6>" + Environment.NewLine;
            strXml += "<SiteMenu7>" + SepFunctions.HTMLEncode(Menu7Text.Value) + "</SiteMenu7>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/SiteLayout/WebSiteLayout") != null)
                strXml += "<WebSiteLayout>" + root.SelectSingleNode("/ROOTLEVEL/SiteLayout/WebSiteLayout").InnerText + "</WebSiteLayout>" + Environment.NewLine;
            else
                strXml += "<WebSiteLayout></WebSiteLayout>" + Environment.NewLine;
            strXml += "</SiteLayout>" + Environment.NewLine;

            strXml += "<PaymentGateway>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/UseMasterPortal") != null)
                strXml += "<UseMasterPortal>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/UseMasterPortal").InnerText + "</UseMasterPortal>" + Environment.NewLine;
            else
                strXml += "<UseMasterPortal>1</UseMasterPortal>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/AuthorizeNet") != null)
                strXml += "<AuthorizeNet>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/AuthorizeNet").InnerText + "</AuthorizeNet>" + Environment.NewLine;
            else
                strXml += "<AuthorizeNet></AuthorizeNet>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/PayPal") != null)
                strXml += "<PayPal>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/PayPal").InnerText + "</PayPal>" + Environment.NewLine;
            else
                strXml += "<PayPal></PayPal>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/eSelectAPIToken") != null)
                strXml += "<eSelectAPIToken>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/eSelectAPIToken").InnerText + "</eSelectAPIToken>" + Environment.NewLine;
            else
                strXml += "<eSelectAPIToken></eSelectAPIToken>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/eSelectStoreID") != null)
                strXml += "<eSelectStoreID>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/eSelectStoreID").InnerText + "</eSelectStoreID>" + Environment.NewLine;
            else
                strXml += "<eSelectStoreID></eSelectStoreID>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckEmailTo") != null)
                strXml += "<CheckEmailTo>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckEmailTo").InnerText + "</CheckEmailTo>" + Environment.NewLine;
            else
                strXml += "<CheckEmailTo></CheckEmailTo>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckAddress") != null)
                strXml += "<CheckAddress>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckAddress").InnerText + "</CheckAddress>" + Environment.NewLine;
            else
                strXml += "<CheckAddress></CheckAddress>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckInstructions") != null)
                strXml += "<CheckInstructions>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/CheckInstructions").InnerText + "</CheckInstructions>" + Environment.NewLine;
            else
                strXml += "<CheckInstructions></CheckInstructions>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPAccountID") != null)
                strXml += "<MSPAccountID>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPAccountID").InnerText + "</MSPAccountID>" + Environment.NewLine;
            else
                strXml += "<MSPAccountID></MSPAccountID>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPSiteID") != null)
                strXml += "<MSPSiteID>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPSiteID").InnerText + "</MSPSiteID>" + Environment.NewLine;
            else
                strXml += "<MSPSiteID></MSPSiteID>" + Environment.NewLine;
            if (root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPSiteCode") != null)
                strXml += "<MSPSiteCode>" + root.SelectSingleNode("/ROOTLEVEL/PaymentGateway/MSPSiteCode").InnerText + "</MSPSiteCode>" + Environment.NewLine;
            else
                strXml += "<MSPSiteCode></MSPSiteCode>" + Environment.NewLine;
            strXml += "</PaymentGateway>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + SepFunctions.Get_Portal_ID() + ".xml"))
            {
                outfile.Write(strXml);
            }

            SepFunctions.Cache_Remove();

            SaveMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings successfully saved.") + "</div>";
        }
    }
}