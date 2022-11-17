// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="site_template_functions.aspx.cs" company="SepCity, Inc.">
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
    using System.Xml;

    /// <summary>
    /// Class site_template_functions.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class site_template_functions : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s temporary folder
        /// </summary>
        private string sTempFolder = string.Empty;

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
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID())) ViewStateUserKey = SepFunctions.Session_User_ID();

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminSiteLooks")))
            {
                form1.Visible = false;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminSiteLooks"), false) == false)
            {
                form1.Visible = false;
                return;
            }

            sTempFolder = SepFunctions.GetDirValue("App_Data") + "templates\\temp\\";

            var sForm = string.Empty;

            long aa = 0;

            var m_xmld = new XmlDocument() { XmlResolver = null };
            using (var sreader = new StreamReader(sTempFolder + "template-config.xml"))
            {
                using (var reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    m_xmld.Load(reader);

                    string[] sRightColumnArray = null;
                    var sRightColumn = m_xmld.SelectSingleNode("//root/RightColumn").InnerText;

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        sRightColumnArray = Strings.Split(sRightColumn, ",");
                        aa = Information.UBound(sRightColumnArray) + 1;
                        if (sRightColumnArray != null)
                            for (var i = 0; i <= Information.UBound(sRightColumnArray); i++)
                            {
                                sForm += "<select name=\"InsertFunction" + i + "\">";
                                sForm += "<option value=\"\">" + SepFunctions.LangText("Empty") + "</option>";
                                sForm += "<option value=\"[[MemberStats]]\"" + Strings.ToString(sRightColumnArray[i] == "[[MemberStats]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Members Statistics") + "</option>";
                                sForm += "<option value=\"[[Members]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Members]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Latest Members") + "</option>";
                                sForm += "<option value=\"[[AccountMenu]]\"" + Strings.ToString(sRightColumnArray[i] == "[[AccountMenu]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Menu") + "</option>";
                                if (SepFunctions.Setup(46, "EventsEnable") == "Enable")
                                    sForm += "<option value=\"[[Calendar]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Calendar]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Event Calendar") + "</option>";
                                if (SepFunctions.Setup(33, "FriendsEnable") == "Yes")
                                    sForm += "<option value=\"[[Friends]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Friends]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Friends List") + "</option>";
                                if (SepFunctions.Setup(25, "PollsEnable") == "Enable")
                                    sForm += "<option value=\"[[Polls]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Polls]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Random Poll") + "</option>";
                                sForm += "<option value=\"[[Stocks]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Stocks]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Stock Quotes") + "</option>";
                                sForm += "<option value=\"[[WhosOn]]\"" + Strings.ToString(sRightColumnArray[i] == "[[WhosOn]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Who's Online") + "</option>";
                                sForm += "<option value=\"[[SiteLogo]]\"" + Strings.ToString(sRightColumnArray[i] == "[[SiteLogo]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Site Logo / Name") + "</option>";
                                if (SepFunctions.Setup(17, "MessengerEnable") == "Enable")
                                    sForm += "<option value=\"[[UnreadMessages]]\"" + Strings.ToString(sRightColumnArray[i] == "[[UnreadMessages]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Unread Messages") + "</option>";
                                if (SepFunctions.Setup(39, "AffiliateEnable") == "Enable")
                                    sForm += "<option value=\"[[AffiliateStats]]\"" + Strings.ToString(sRightColumnArray[i] == "[[AffiliateStats]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Affiliate Statistics") + "</option>";
                                sForm += "<option value=\"[[ViewCart]]\"" + Strings.ToString(sRightColumnArray[i] == "[[ViewCart]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("View Shopping Cart") + "</option>";
                                sForm += "<option value=\"[[Newsletters]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Newsletters]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Newsletters") + "</option>";
                                if (SepFunctions.Setup(60, "PortalsEnable") == "Enable")
                                    sForm += "<option value=\"[[PortalList]]\"" + Strings.ToString(sRightColumnArray[i] == "[[PortalList]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Portal Listing") + "</option>";
                                for (var j = 1; j <= 7; j++)
                                    using (var cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE MenuID=@MenuID AND Activated='1'", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@MenuID", j);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows) sForm += "<option value=\"[[SiteMenu" + j + "]]\"" + Strings.ToString(sRightColumnArray[i] == "[[SiteMenu" + j + "]]" ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu" + j + "Text")) ? SepFunctions.Setup(993, "Menu" + j + "Text") : SepFunctions.LangText("Site Menu ~~" + j + "~~")) + "</option>";
                                        }
                                    }

                                if (SepFunctions.Setup(2, "AdsEnable") == "Enable")
                                    using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='2' ORDER BY ZoneName", conn))
                                    {
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                                while (RS.Read())
                                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                                        sForm += "<option value=\"[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]\"" + Strings.ToString(sRightColumnArray[i] == "[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Advertsting") + " (" + SepFunctions.openNull(RS["ZoneName"]) + ")</option>";
                                        }
                                    }

                                if (SepFunctions.Setup(1, "CREnable") == "Yes")
                                    using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='1' ORDER BY ZoneName", conn))
                                    {
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                                while (RS.Read())
                                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                                        sForm += "<option value=\"[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]\"" + Strings.ToString(sRightColumnArray[i] == "[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]" ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Content Rotator") + " (" + SepFunctions.openNull(RS["ZoneName"]) + ")</option>";
                                        }
                                    }

                                sForm += "</select><br/>";
                            }

                        if (aa > 1)
                            for (var i = 1; i <= 25 - aa; i++)
                            {
                                sForm += "<select name=\"InsertFunction" + (i + aa) + "\">";
                                sForm += "<option value=\"\">" + SepFunctions.LangText("Empty") + "</option>";
                                sForm += "<option value=\"[[MemberStats]]\">" + SepFunctions.LangText("Members Statistics") + "</option>";
                                sForm += "<option value=\"[[Members]]\">" + SepFunctions.LangText("Latest Members") + "</option>";
                                sForm += "<option value=\"[[AccountMenu]]\">" + SepFunctions.LangText("Account Menu") + "</option>";
                                if (SepFunctions.Setup(46, "EventsEnable") == "Enable")
                                    sForm += "<option value=\"[[Calendar]]\">" + SepFunctions.LangText("Event Calendar") + "</option>";
                                if (SepFunctions.Setup(33, "FriendsEnable") == "Yes")
                                    sForm += "<option value=\"[[Friends]]\">" + SepFunctions.LangText("Friends List") + "</option>";
                                if (SepFunctions.Setup(25, "PollsEnable") == "Enable")
                                    sForm += "<option value=\"[[Polls]]\">" + SepFunctions.LangText("Random Poll") + "</option>";
                                sForm += "<option value=\"[[Stocks]]\">" + SepFunctions.LangText("Stock Quotes") + "</option>";
                                sForm += "<option value=\"[[WhosOn]]\">" + SepFunctions.LangText("Who's Online") + "</option>";
                                sForm += "<option value=\"[[SiteLogo]]\">" + SepFunctions.LangText("Site Logo / Name") + "</option>";
                                if (SepFunctions.Setup(17, "MessengerEnable") == "Enable")
                                    sForm += "<option value=\"[[UnreadMessages]]\">" + SepFunctions.LangText("Unread Messages") + "</option>";
                                if (SepFunctions.Setup(39, "AffiliateEnable") == "Enable")
                                    sForm += "<option value=\"[[AffiliateStats]]\">" + SepFunctions.LangText("Affiliate Statistics") + "</option>";
                                sForm += "<option value=\"[[ViewCart]]\">" + SepFunctions.LangText("View Shopping Cart") + "</option>";
                                sForm += "<option value=\"[[Newsletters]]\">" + SepFunctions.LangText("Newsletters") + "</option>";
                                for (var j = 1; j <= 7; j++)
                                    using (var cmd = new SqlCommand("SELECT UniqueID FROM ModulesNPages WHERE MenuID=@MenuID AND Activated='1'", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@MenuID", j);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows) sForm += "<option value=\"[[SiteMenu" + j + "]]\">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu" + j + "Text")) ? SepFunctions.Setup(993, "Menu" + j + "Text") : SepFunctions.LangText("Site Menu ~~" + j + "~~")) + "</option>";
                                        }
                                    }

                                if (SepFunctions.Setup(2, "AdsEnable") == "Enable")
                                    using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='2' ORDER BY ZoneName", conn))
                                    {
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                                while (RS.Read())
                                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                                        sForm += "<option value=\"[[Ads|" + SepFunctions.openNull(RS["ZoneName"]) + "]]\">" + SepFunctions.LangText("Advertsting") + " (" + SepFunctions.openNull(RS["ZoneName"]) + ")</option>";
                                        }
                                    }

                                if (SepFunctions.Setup(1, "CREnable") == "Yes")
                                    using (var cmd = new SqlCommand("SELECT ZoneName FROM TargetZones WHERE ModuleID='1' ORDER BY ZoneName", conn))
                                    {
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                                while (RS.Read())
                                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["ZoneName"])))
                                                        sForm += "<option value=\"[[CR|" + SepFunctions.openNull(RS["ZoneName"]) + "]]\">" + SepFunctions.LangText("Content Rotator") + " (" + SepFunctions.openNull(RS["ZoneName"]) + ")</option>";
                                        }
                                    }

                                sForm += "</select><br/>";
                            }
                    }
                }
            }

            FunctionList.InnerHtml = sForm;
        }

        /// <summary>
        /// Handles the Click event of the SaveBut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveBut_Click(object sender, EventArgs e)
        {
            long aa = 0;
            var sConfig = string.Empty;

            var jScript = string.Empty;

            sConfig += "<?xml version = \"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            sConfig += "<root>" + Environment.NewLine;
            sConfig += "<RightColumn>";
            for (var i = 0; i <= 25; i++)
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("InsertFunction" + i)))
                {
                    aa += 1;
                    if (aa > 1)
                        sConfig += ",";
                    sConfig += SepCommon.SepCore.Request.Item("InsertFunction" + i);
                }

            sConfig += "</RightColumn>" + Environment.NewLine;
            sConfig += "</root>" + Environment.NewLine;

            using (var cTemplate = new site_template())
            {
                using (var objWriter = new StreamWriter(cTemplate.Load_Folder(SepCommon.SepCore.Request.Item("ModTemp")) + "template-config.xml"))
                {
                    objWriter.Write(sConfig);
                }

                using (var objWriter = new StreamWriter(sTempFolder + "template-config.xml"))
                {
                    objWriter.Write(sConfig);
                }
            }

            jScript += "<script type=\"text/javascript\">";
            jScript += "parent.document.getElementById('TemplateFrame').src='site_template_builder.aspx?ModTemp=" + SepFunctions.UrlEncode(SepCommon.SepCore.Request.Item("ModTemp")) + "&PortalID=" + SepFunctions.Get_Portal_ID() + "';";
            jScript += "parent.closeDialog('DivDynHrefFrame');";
            jScript += "</script>";

            var cstype = GetType();

            Page.ClientScript.RegisterClientScriptBlock(cstype, "ButtonClickScript", jScript);
        }
    }
}