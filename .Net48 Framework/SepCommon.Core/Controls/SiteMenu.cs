// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SiteMenu.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Class SiteMenu.
    /// </summary>
    public class SiteMenu
    {
        /// <summary>
        /// The m disable li
        /// </summary>
        private bool m_DisableLI;

        /// <summary>
        /// The m disable ul
        /// </summary>
        private bool m_DisableUL;

        /// <summary>
        /// The m menu identifier
        /// </summary>
        private int m_MenuID;

        /// <summary>
        /// The m override link class
        /// </summary>
        private string m_OverrideLinkClass;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [disable li].
        /// </summary>
        /// <value><c>true</c> if [disable li]; otherwise, <c>false</c>.</value>
        public bool DisableLI
        {
            get
            {
                var s = Convert.ToBoolean(m_DisableLI);
                return s;
            }

            set => m_DisableLI = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disable ul].
        /// </summary>
        /// <value><c>true</c> if [disable ul]; otherwise, <c>false</c>.</value>
        public bool DisableUL
        {
            get
            {
                var s = Convert.ToBoolean(m_DisableUL);
                return s;
            }

            set => m_DisableUL = value;
        }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public int MenuID
        {
            get
            {
                var s = Convert.ToInt32(m_MenuID);
                return s;
            }

            set => m_MenuID = value;
        }

        /// <summary>
        /// Gets or sets the override link class.
        /// </summary>
        /// <value>The override link class.</value>
        public string OverrideLinkClass
        {
            get => SepCommon.Core.SepCore.Strings.ToString(m_OverrideLinkClass);

            set => m_OverrideLinkClass = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (MenuID == 0)
            {
                output.AppendLine("MenuID is Required");
                return output.ToString();
            }

            var str = new StringBuilder();

            var sCheckDup = string.Empty;
            var sId = string.Empty;
            var wc = string.Empty;

            var bAcctMenu = false;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (MenuID == 10)
            {
                bAcctMenu = true;
            }

            if (!string.IsNullOrWhiteSpace(CssClass))
            {
                CssClass = " " + CssClass;
            }

            if (string.IsNullOrWhiteSpace(OverrideLinkClass))
            {
                OverrideLinkClass = "nav-link";
            }

            if (SepFunctions.isUserPage())
            {
                var sTemplateFolder = SepFunctions.getTemplateFolder();
                var sConfigFile = SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config_default.xml";

                if (File.Exists(SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config.xml"))
                {
                    sConfigFile = SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config.xml";
                }

                if (File.Exists(sConfigFile))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(sConfigFile))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (MenuID == 3)
                            {
                                if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"MemberMenu\"]") != null)
                                {
                                    if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"MemberMenu\"]").InnerText == "0")
                                    {
                                        return output.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (bAcctMenu)
            {
                str.AppendLine("<h6>" + SepFunctions.LangText("Welcome ~~" + SepFunctions.Session_User_Name() + "~~") + "</h6>");
            }

            if (DisableUL == false)
            {
                str.AppendLine("<ul class=\"nav" + CssClass + "\">");
            }

            if (bAcctMenu)
            {
                if (SepFunctions.ModuleActivated(68) == false)
                {
                    if (DisableLI == false)
                    {
                        str.AppendLine("<li class=\"nav-item\">");
                    }

                    str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"" + sInstallFolder + "account.aspx\">" + SepFunctions.LangText("Account Info") + "</a>");
                    if (DisableLI == false)
                    {
                        str.AppendLine("</li>");
                    }
                }

                if (SepFunctions.Setup(6, "IMessengerEnable") == "Enable" && SepFunctions.CompareKeys(SepFunctions.Security("IMessengerAccess"), true))
                {
                    str.AppendLine("<script src=\"" + sInstallFolder + "CuteSoft_Client/CuteChat/IntegrationUtility.js\"></script>");
                    if (DisableLI == false)
                    {
                        str.AppendLine("<li class=\"nav-item\">");
                    }

                    str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"javascript:void(0)\" onclick=\"try{Chat_OpenMessengerDialog();}catch(e){alert(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Messenger not configured")) + "'))}\">" + SepFunctions.LangText("Open Messenger") + "</a>");
                    if (DisableLI == false)
                    {
                        str.AppendLine("</li>");
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (SepFunctions.isUserPage())
                {
                    if (SepFunctions.Setup(7, "UPagesMainMenu" + MenuID) == "Yes")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT PageID,PageName,PageTitle FROM UPagesPages WHERE MenuID=" + MenuID + " AND UserID='" + SepFunctions.GetUserID(SepCommon.Core.SepCore.Request.Item("UserName")) + "' ORDER BY Weight,PageName", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (bAcctMenu == false && RS.HasRows == false)
                                {
                                    return output.ToString();
                                }

                                while (RS.Read())
                                {
                                    str.AppendLine(REP_Site_Menu_Loop(SepFunctions.openNull(RS["PageID"]), SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])), SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])), SepFunctions.openNull(RS["PageName"]), string.Empty, SepFunctions.openNull(RS["PageTitle"])));
                                }
                            }
                        }
                    }
                }

                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT MP.UniqueID,MP.PageID,MP.ModuleID,MP.UserPageName,MP.LinkText,MP.TargetWindow,MP.AccessKeys FROM ModulesNPages AS MP WHERE MP.MenuID=" + MenuID + " AND MP.Status=1 AND Activated=1 AND MP.UserPageName <> '' ORDER BY MP.Weight,MP.LinkText", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (bAcctMenu == false && RS.HasRows == false)
                            {
                                return output.ToString();
                            }

                            while (RS.Read())
                            {
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) > 0)
                                {
                                    if (string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["AccessKeys"])) || (SepFunctions.CompareKeys(SepFunctions.openNull(RS["AccessKeys"])) && SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])))))
                                    {
                                        if (SepFunctions.isUserPage() && SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) == 7)
                                        {
                                            // Hide user pages when in a user page
                                        }
                                        else
                                        {
                                            str.AppendLine(REP_Site_Menu_Loop(SepFunctions.openNull(RS["UniqueID"]), SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])), SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])), SepFunctions.openNull(RS["UserPageName"]), SepFunctions.openNull(RS["TargetWindow"]), SepFunctions.openNull(RS["LinkText"])));
                                        }
                                    }
                                }
                                else
                                {
                                    str.AppendLine(REP_Site_Menu_Loop(SepFunctions.openNull(RS["UniqueID"]), SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])), SepFunctions.toInt(SepFunctions.openNull(RS["ModuleID"])), SepFunctions.openNull(RS["UserPageName"]), SepFunctions.openNull(RS["TargetWindow"]), SepFunctions.openNull(RS["LinkText"])));
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT MP.UniqueID,MP.PageID FROM PortalPages AS MP WHERE (MP.PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR MP.PortalIDs LIKE '%|-1|%') AND MP.MenuID=" + MenuID + " AND MP.Status=1", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                if (SepCommon.Core.SepCore.Strings.InStr(sCheckDup, "|" + SepFunctions.openNull(RS["PageID"]) + "|") == 0)
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                    {
                                        if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["PageID"]))))
                                        {
                                            sCheckDup += "|" + SepFunctions.openNull(RS["PageID"]) + "|";
                                            sId += "," + SepFunctions.openNull(RS["UniqueID"]);
                                        }
                                    }
                                    else
                                    {
                                        sId += "," + SepFunctions.openNull(RS["UniqueID"]);
                                    }
                                }
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT MP.UniqueID,MP.PageID FROM PortalPages AS MP WHERE MP.PortalID=" + SepFunctions.Get_Portal_ID() + " AND MP.MenuID=" + MenuID + " AND MP.Status=1", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                if (SepCommon.Core.SepCore.Strings.InStr(sCheckDup, "|" + SepFunctions.openNull(RS["PageID"]) + "|") == 0)
                                {
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])) < 200)
                                    {
                                        if (SepFunctions.ModuleActivated(SepFunctions.toLong(SepFunctions.openNull(RS["PageID"]))))
                                        {
                                            sCheckDup += "|" + SepFunctions.openNull(RS["PageID"]) + "|";
                                            sId += "," + SepFunctions.openNull(RS["UniqueID"]);
                                        }
                                    }
                                    else
                                    {
                                        sId += "," + SepFunctions.openNull(RS["UniqueID"]);
                                    }
                                }
                            }
                        }
                    }

                    if (SepCommon.Core.SepCore.Strings.Len(sId) > 0)
                    {
                        wc = " UniqueID IN (" + SepCommon.Core.SepCore.Strings.Right(sId, SepCommon.Core.SepCore.Strings.Len(sId) - 1) + ")";
                    }

                    if (!string.IsNullOrWhiteSpace(wc))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT MP.UniqueID,MP.PageID,MP.UserPageName,MP.LinkText,MP.TargetWindow FROM PortalPages AS MP WHERE (" + wc + ") AND MP.MenuID=" + MenuID + " AND MP.Status=1 ORDER BY MP.Weight,MP.LinkText", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                while (RS.Read())
                                {
                                    str.AppendLine(REP_Site_Menu_Loop(SepFunctions.openNull(RS["UniqueID"]), SepFunctions.toLong(SepFunctions.openNull(RS["PageID"])), 0, SepFunctions.openNull(RS["UserPageName"]), SepFunctions.openNull(RS["TargetWindow"]), SepFunctions.openNull(RS["LinkText"])));
                                }
                            }
                        }
                    }
                }
            }

            if (bAcctMenu)
            {
                if (DisableLI == false)
                {
                    str.AppendLine("<li class=\"nav-item\">");
                }

                str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"javascript:void(0)\" onclick=\"confirm(unescape('" + SepFunctions.EscQuotes(SepFunctions.LangText("Are you sure you wish to logout?")) + "'), function() {document.location.href='" + sInstallFolder + "logout.aspx';})\">" + SepFunctions.LangText("Logout") + "</a>");
                if (DisableLI == false)
                {
                    str.AppendLine("</li>");
                }

                if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) && SepFunctions.ModuleActivated(22))
                {
                    if (DisableLI == false)
                    {
                        str.AppendLine("<li class=\"nav-item\">");
                    }

                    str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"" + sImageFolder + "twilio/default.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\">" + SepFunctions.LangText("Twilio Panel") + "</a>");
                    if (DisableLI == false)
                    {
                        str.AppendLine("</li>");
                    }
                }

                if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true))
                {
                    if (DisableLI == false)
                    {
                        str.AppendLine("<li class=\"nav-item\">");
                    }

                    str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"" + sImageFolder + "spadmin/default.aspx?PortalID=" + SepFunctions.Get_Portal_ID() + "\" target=\"_blank\">" + SepFunctions.LangText("Administration") + "</a>");
                    if (DisableLI == false)
                    {
                        str.AppendLine("</li>");
                    }
                }
            }

            if (DisableUL == false)
            {
                str.AppendLine("</ul>");
            }

            output.Append(SepCommon.Core.SepCore.Strings.ToString(str));

            return output.ToString();
        }

        /// <summary>
        /// Reps the site menu loop.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="PageID">The page identifier.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="UserPageName">Name of the user page.</param>
        /// <param name="TargetWindow">The target window.</param>
        /// <param name="LinkText">The link text.</param>
        /// <returns>System.String.</returns>
        public string REP_Site_Menu_Loop(string ID, long PageID, long ModuleID, string UserPageName, string TargetWindow, string LinkText)
        {
            var str = new StringBuilder();

            var GetPageName = string.Empty;
            var sTarget = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();

            switch (PageID)
            {
                case 201:
                    if (SepCommon.Core.SepCore.Strings.Left(UserPageName, 2) == "~/" || (SepCommon.Core.SepCore.Strings.Left(UserPageName, 1) != "/" && SepCommon.Core.SepCore.Strings.Left(UserPageName, 4) != "www." && SepCommon.Core.SepCore.Strings.Left(UserPageName, 5) != "http:" && SepCommon.Core.SepCore.Strings.Left(UserPageName, 6) != "https:"))
                    {
                        GetPageName = sInstallFolder + SepCommon.Core.SepCore.Strings.Replace(UserPageName, "~/", string.Empty);
                    }
                    else if (SepCommon.Core.SepCore.Strings.Left(UserPageName, 4) == "www.")
                    {
                        GetPageName = "http://" + UserPageName;
                    }
                    else
                    {
                        GetPageName = UserPageName;
                    }

                    break;

                case 200:
                    GetPageName = sInstallFolder + "page/" + ID + "/" + SepFunctions.Format_ISAPI(LinkText);
                    break;

                default:
                    GetPageName = sInstallFolder + UserPageName;
                    break;
            }

            if (SepFunctions.Get_Portal_ID() == 0)
            {
                if (ModuleID > 0)
                {
                    sTarget = "_self";
                }
                else
                {
                    sTarget = !string.IsNullOrWhiteSpace(TargetWindow) ? TargetWindow : "_self";
                }
            }
            else
            {
                sTarget = !string.IsNullOrWhiteSpace(TargetWindow) ? TargetWindow : "_self";
            }

            if (DisableLI == false)
            {
                str.AppendLine("<li class=\"nav-item\">");
            }

            str.AppendLine("<a class=\"" + OverrideLinkClass + "\" href=\"" + GetPageName + "\" target=\"" + sTarget + "\">" + SepFunctions.LangText(LinkText, false) + "</a>");
            if (DisableLI == false)
            {
                str.AppendLine("</li>");
            }

            return SepCommon.Core.SepCore.Strings.ToString(str);
        }
    }
}