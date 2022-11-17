// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="shopping_shipping_config.aspx.cs" company="SepCity, Inc.">
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
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    /// <summary>
    /// Class shopping_shipping_config.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class shopping_shipping_config : Page
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

            TranslatePage();

            GlobalVars.ModuleID = 41;

            if ((SepFunctions.Setup(GlobalVars.ModuleID, "ShopMallEnable") != "Enable" || SepFunctions.isUserPage()) && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("ShopMallStore"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            var StoreID = SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID());
            if (StoreID == 0)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Access Denied. You do not have a store setup.") + "</div>";
                return;
            }

            if (!Page.IsPostBack)
            {
                if (!File.Exists(SepFunctions.GetDirValue("app_data") + "settings-store-" + StoreID + ".xml"))
                {
                    using (var fs = File.Create(SepFunctions.GetDirValue("app_data") + "settings-store-" + StoreID + ".xml"))
                    {
                        var strXml = string.Empty;

                        strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        strXml += "<ROOTLEVEL>" + Environment.NewLine;

                        strXml += "<FedExAccountNum></FedExAccountNum>" + Environment.NewLine;
                        strXml += "<FedExMeterNum></FedExMeterNum>" + Environment.NewLine;
                        strXml += "<FedExServiceKey></FedExServiceKey>" + Environment.NewLine;
                        strXml += "<FedExServicePass></FedExServicePass>" + Environment.NewLine;
                        strXml += "<UPSAccountNum></UPSAccountNum>" + Environment.NewLine;
                        strXml += "<UPSUserName></UPSUserName>" + Environment.NewLine;
                        strXml += "<UPSPassword></UPSPassword>" + Environment.NewLine;
                        strXml += "<UPSShipperNum></UPSShipperNum>" + Environment.NewLine;
                        strXml += "<USPSUserID></USPSUserID>" + Environment.NewLine;

                        strXml += "</ROOTLEVEL>" + Environment.NewLine;
                        var info = new UTF8Encoding(true).GetBytes(strXml);
                        fs.Write(info, 0, info.Length);
                    }
                }
                else
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings-store-" + StoreID + ".xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            // Select the book node with the matching attribute value.
                            var root = doc.DocumentElement;

                            if (root.SelectSingleNode("/ROOTLEVEL/FedExAccountNum") != null) FedExAccountNum.Value = root.SelectSingleNode("/ROOTLEVEL/FedExAccountNum").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/FedExMeterNum") != null) FedExMeterNum.Value = root.SelectSingleNode("/ROOTLEVEL/FedExMeterNum").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/FedExServiceKey") != null) FedExServiceKey.Value = root.SelectSingleNode("/ROOTLEVEL/FedExServiceKey").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/FedExServicePass") != null)
                                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/FedExServicePass").InnerText))
                                    FedExServicePass.Value = SepFunctions.LangText("(Encrypted Data)");

                            if (root.SelectSingleNode("/ROOTLEVEL/UPSAccountNum") != null) UPSAccountNum.Value = root.SelectSingleNode("/ROOTLEVEL/UPSAccountNum").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/UPSUserName") != null) UPSUserName.Value = root.SelectSingleNode("/ROOTLEVEL/UPSUserName").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/UPSPassword") != null)
                                if (!string.IsNullOrWhiteSpace(root.SelectSingleNode("/ROOTLEVEL/UPSPassword").InnerText))
                                    UPSPassword.Value = SepFunctions.LangText("(Encrypted Data)");

                            if (root.SelectSingleNode("/ROOTLEVEL/UPSShipperNum") != null) UPSShipperNum.Value = root.SelectSingleNode("/ROOTLEVEL/UPSShipperNum").InnerText;

                            if (root.SelectSingleNode("/ROOTLEVEL/USPSUserID") != null) USPSUserID.Value = root.SelectSingleNode("/ROOTLEVEL/USPSUserID").InnerText;
                        }
                    }
                }
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var strXml = string.Empty;

            strXml += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            strXml += "<ROOTLEVEL>" + Environment.NewLine;

            strXml += "<FedExAccountNum>" + SepFunctions.HTMLEncode(FedExAccountNum.Value) + "</FedExAccountNum>" + Environment.NewLine;
            strXml += "<FedExMeterNum>" + SepFunctions.HTMLEncode(FedExMeterNum.Value) + "</FedExMeterNum>" + Environment.NewLine;
            strXml += "<FedExServiceKey>" + SepFunctions.HTMLEncode(FedExServiceKey.Value) + "</FedExServiceKey>" + Environment.NewLine;
            strXml += "<FedExServicePass>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(FedExServicePass.Value)) + "</FedExServicePass>" + Environment.NewLine;
            strXml += "<UPSAccountNum>" + SepFunctions.HTMLEncode(UPSAccountNum.Value) + "</UPSAccountNum>" + Environment.NewLine;
            strXml += "<UPSUserName>" + SepFunctions.HTMLEncode(UPSUserName.Value) + "</UPSUserName>" + Environment.NewLine;
            strXml += "<UPSPassword>" + SepFunctions.HTMLEncode(SepFunctions.Encrypt(UPSPassword.Value)) + "</UPSPassword>" + Environment.NewLine;
            strXml += "<UPSShipperNum>" + SepFunctions.HTMLEncode(UPSShipperNum.Value) + "</UPSShipperNum>" + Environment.NewLine;
            strXml += "<USPSUserID>" + SepFunctions.HTMLEncode(USPSUserID.Value) + "</USPSUserID>" + Environment.NewLine;

            strXml += "</ROOTLEVEL>" + Environment.NewLine;

            using (var outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-store-" + SepCommon.DAL.ShoppingMall.Store_Get_StoreID(SepFunctions.Session_User_ID()) + ".xml"))
            {
                outfile.Write(strXml);
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings successfully saved.") + "</div>";
        }
    }
}