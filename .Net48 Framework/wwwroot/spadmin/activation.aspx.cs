// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="activation.aspx.cs" company="SepCity, Inc.">
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
    using wwwroot.SepActivation;

    /// <summary>
    /// Class activation.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class activation : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Activation");
                    LicenseKeyLabel.InnerText = SepFunctions.LangText("License Key:");
                    SoftwareEditionLabel.InnerText = SepFunctions.LangText("Software Edition:");
                    StatusLabel.InnerText = SepFunctions.LangText("Status:");
                    PurchaseDateLabel.InnerText = SepFunctions.LangText("Date Purchased:");
                    ExpireDateLabel.InnerText = SepFunctions.LangText("Expiration:");
                    ModuleListLabel.InnerText = SepFunctions.LangText("Modules:");
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

            if (SepFunctions.Admin_Login_Required("2"))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys("|2|", false) == false)
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

            var LicenseUser = string.Empty;
            var LicensePass = string.Empty;
            var LicenseUserKey = string.Empty;
            var sModuleList = string.Empty;
            var sVersion = string.Empty;

            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "license.xml"))
            {
                ActivationFieldset.Visible = false;
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("License file is missing from your website.") + "</div>";
            }
            else
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "license.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        // Select the book node with the matching attribute value.
                        var root = doc.DocumentElement;

                        using (var soapClient = new activationSoapClient("activationSoap"))
                        {
                            LicenseUser = SepFunctions.ParseXML("Username", root.InnerXml);
                            LicensePass = SepFunctions.ParseXML("Password", root.InnerXml);
                            LicenseUserKey = SepFunctions.ParseXML("LicenseKey", root.InnerXml);

                            var jActivation = soapClient.Get_License_Details(LicenseUser, LicensePass, LicenseUserKey);

                            try
                            {
                                if (!string.IsNullOrWhiteSpace(jActivation.ErrorMessage))
                                {
                                    ActivationFieldset.Visible = false;
                                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + jActivation.ErrorMessage + "</div>";
                                }
                                else
                                {
                                    sModuleList = jActivation.ModuleList;
                                    sVersion = jActivation.SoftwareEdition;

                                    LicenseKey.InnerHtml = jActivation.LicenseKey;
                                    SoftwareEdition.InnerHtml = sVersion;
                                    Status.InnerHtml = jActivation.Status;
                                    PurchaseDate.InnerHtml = Strings.FormatDateTime(SepFunctions.toDate(jActivation.PurchaseDate), Strings.DateNamedFormat.ShortDate);
                                    ExpireDate.InnerHtml = Strings.FormatDateTime(SepFunctions.toDate(jActivation.ExpireDate), Strings.DateNamedFormat.ShortDate);
                                    if (!string.IsNullOrWhiteSpace(sModuleList))
                                    {
                                        var arrModules = Strings.Split(Strings.Replace(sModuleList, "|", string.Empty), ",");

                                        if (arrModules != null)
                                        {
                                            for (var i = 0; i <= Information.UBound(arrModules); i++)
                                                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                                {
                                                    conn.Open();
                                                    using (var cmd = new SqlCommand("SELECT LinkText FROM ModulesNPages WHERE ModuleID=@ModuleID", conn))
                                                    {
                                                        cmd.Parameters.AddWithValue("@ModuleID", arrModules[i]);
                                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                                        {
                                                            if (RS.HasRows)
                                                            {
                                                                RS.Read();
                                                                ModuleList.InnerHtml += SepFunctions.openNull(RS["LinkText"]) + "<br/>";
                                                                using (var cmd2 = new SqlCommand("UPDATE ModulesNPages SET Activated='1' WHERE ModuleID=@ModuleID", conn))
                                                                {
                                                                    cmd2.Parameters.AddWithValue("@ModuleID", arrModules[i]);
                                                                    cmd2.ExecuteNonQuery();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ModuleList.InnerHtml += SepFunctions.LangText("Unknown Module") + "<br/>";
                                                            }

                                                        }
                                                    }
                                                }
                                        }
                                    }
                                    else
                                    {
                                        ModuleList.InnerHtml = SepFunctions.LangText("No Additional Modules Purchased");
                                    }
                                }

                                SepFunctions.Cache_Remove();
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    ActivationFieldset.Visible = false;
                                    if (!string.IsNullOrWhiteSpace(jActivation.ErrorMessage)) ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + jActivation.ErrorMessage + "</div>";
                                    else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error loading activation information.") + "<br/><br/>" + ex.Message + "</div>";
                                }
                                catch (Exception ex2)
                                {
                                    ActivationFieldset.Visible = false;
                                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error loading activation information.") + "<br/><br/>" + ex2.Message + "</div>";
                                }
                            }
                        }
                    }
                }

                try
                {
                    using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "license.xml"))
                    {
                        var sXML = string.Empty;
                        sXML += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        sXML += "<ROOTLEVEL>" + Environment.NewLine;
                        sXML += "\t" + "<Username>" + LicenseUser + "</Username>" + Environment.NewLine;
                        sXML += "\t" + "<Password>" + LicensePass + "</Password>" + Environment.NewLine;
                        sXML += "\t" + "<LicenseKey>" + LicenseUserKey + "</LicenseKey>" + Environment.NewLine;
                        sXML += "\t" + "<LastChecked>" + Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate) + "</LastChecked>" + Environment.NewLine;
                        sXML += "\t" + "<ModuleList>" + sModuleList + "</ModuleList>" + Environment.NewLine;
                        sXML += "\t" + "<Version>" + sVersion + "</Version>" + Environment.NewLine;
                        sXML += "</ROOTLEVEL>" + Environment.NewLine;
                        outfile.Write(sXML);
                    }
                }
                catch (Exception ex)
                {
                    ActivationFieldset.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error loading activation information.") + "<br/><br/>" + ex.Message + "</div>";
                }
            }
        }
    }
}