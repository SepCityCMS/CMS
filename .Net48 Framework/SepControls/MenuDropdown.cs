// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="MenuDropdown.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class MenuDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("MenuID")]
    [ToolboxData("<{0}:MenuDropdown runat=server></{0}:MenuDropdown>")]
    public class MenuDropdown : WebControl
    {
        /// <summary>
        /// The m menu identifier
        /// </summary>
        private string m_MenuID;

        /// <summary>
        /// The m on user page
        /// </summary>
        private bool m_OnUserPage;

        /// <summary>
        /// The m show not on a menu
        /// </summary>
        private bool m_ShowNotOnAMenu;

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string MenuID
        {
            get
            {
                var s = Context.Request.Form[ID];
                if (s == null)
                {
                    s = Strings.ToString(m_MenuID);
                }

                return s;
            }

            set => m_MenuID = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [on user page].
        /// </summary>
        /// <value><c>true</c> if [on user page]; otherwise, <c>false</c>.</value>
        public bool OnUserPage
        {
            get
            {
                var s = Convert.ToBoolean(m_OnUserPage);
                return s;
            }

            set => m_OnUserPage = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show not on a menu].
        /// </summary>
        /// <value><c>true</c> if [show not on a menu]; otherwise, <c>false</c>.</value>
        public bool ShowNotOnAMenu
        {
            get
            {
                var s = Convert.ToBoolean(m_ShowNotOnAMenu);
                return s;
            }

            set => m_ShowNotOnAMenu = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess")) == false && OnUserPage == false)
            {
                return;
            }

            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" class=\"" + CssClass + "\">");
            if (ShowNotOnAMenu)
            {
                output.WriteLine("<option value=\"0\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 0 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Not on a Menu") + "</option>");
            }

            if (OnUserPage)
            {
                output.WriteLine("<option value=\"0\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 0 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Top Menu") + "</option>");
                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu1")))
                {
                    output.WriteLine("<option value=\"71\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 71 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu1") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu2")))
                {
                    output.WriteLine("<option value=\"72\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 72 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu2") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu3")))
                {
                    output.WriteLine("<option value=\"73\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 73 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu3") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu4")))
                {
                    output.WriteLine("<option value=\"74\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 74 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu4") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu5")))
                {
                    output.WriteLine("<option value=\"75\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 75 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu5") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu6")))
                {
                    output.WriteLine("<option value=\"76\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 76 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu6") + "</option>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu7")))
                {
                    output.WriteLine("<option value=\"77\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 77 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(7, "UPagesMenu7") + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu1") == "Yes")
                {
                    output.WriteLine("<option value=\"1\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 1 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu1Text")) ? SepFunctions.Setup(993, "Menu1Text") : SepFunctions.LangText("Site Menu ~~1~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu2") == "Yes")
                {
                    output.WriteLine("<option value=\"2\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 2 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu2Text")) ? SepFunctions.Setup(993, "Menu2Text") : SepFunctions.LangText("Site Menu ~~2~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu3") == "Yes")
                {
                    output.WriteLine("<option value=\"3\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 3 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu3Text")) ? SepFunctions.Setup(993, "Menu3Text") : SepFunctions.LangText("Site Menu ~~3~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu4") == "Yes")
                {
                    output.WriteLine("<option value=\"4\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 4 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu4Text")) ? SepFunctions.Setup(993, "Menu4Text") : SepFunctions.LangText("Site Menu ~~4~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu5") == "Yes")
                {
                    output.WriteLine("<option value=\"5\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 5 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu5Text")) ? SepFunctions.Setup(993, "Menu5Text") : SepFunctions.LangText("Site Menu ~~5~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu6") == "Yes")
                {
                    output.WriteLine("<option value=\"6\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 6 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu6Text")) ? SepFunctions.Setup(993, "Menu6Text") : SepFunctions.LangText("Site Menu ~~6~~")) + "</option>");
                }

                if (SepFunctions.Setup(7, "UPagesMainMenu7") == "Yes")
                {
                    output.WriteLine("<option value=\"7\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 7 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu7Text")) ? SepFunctions.Setup(993, "Menu7Text") : SepFunctions.LangText("Site Menu ~~7~~")) + "</option>");
                }
            }
            else
            {
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    output.WriteLine("<option value=\"1\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 1 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu1Text")) ? SepFunctions.Setup(993, "Menu1Text") : SepFunctions.LangText("Site Menu ~~1~~")) + "</option>");
                    output.WriteLine("<option value=\"2\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 2 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu2Text")) ? SepFunctions.Setup(993, "Menu2Text") : SepFunctions.LangText("Site Menu ~~2~~")) + "</option>");
                    output.WriteLine("<option value=\"3\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 3 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu3Text")) ? SepFunctions.Setup(993, "Menu3Text") : SepFunctions.LangText("Site Menu ~~3~~")) + "</option>");
                    output.WriteLine("<option value=\"4\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 4 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu4Text")) ? SepFunctions.Setup(993, "Menu4Text") : SepFunctions.LangText("Site Menu ~~4~~")) + "</option>");
                    output.WriteLine("<option value=\"5\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 5 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu5Text")) ? SepFunctions.Setup(993, "Menu5Text") : SepFunctions.LangText("Site Menu ~~5~~")) + "</option>");
                    output.WriteLine("<option value=\"6\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 6 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu6Text")) ? SepFunctions.Setup(993, "Menu6Text") : SepFunctions.LangText("Site Menu ~~6~~")) + "</option>");
                    output.WriteLine("<option value=\"7\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 7 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu7Text")) ? SepFunctions.Setup(993, "Menu7Text") : SepFunctions.LangText("Site Menu ~~7~~")) + "</option>");
                    output.WriteLine("<option value=\"8\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 8 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Top Menu") + "</option>");
                    output.WriteLine("<option value=\"10\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 10 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Main Menu") + "</option>");
                }
                else
                {
                    output.WriteLine("<option value=\"1\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 1 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu1", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu1", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~1~~")) + "</option>");
                    output.WriteLine("<option value=\"2\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 2 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu2", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu2", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~2~~")) + "</option>");
                    output.WriteLine("<option value=\"3\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 3 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu3", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu3", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~3~~")) + "</option>");
                    output.WriteLine("<option value=\"4\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 4 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu4", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu4", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~4~~")) + "</option>");
                    output.WriteLine("<option value=\"5\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 5 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu5", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu5", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~5~~")) + "</option>");
                    output.WriteLine("<option value=\"6\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 6 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu6", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu6", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~6~~")) + "</option>");
                    output.WriteLine("<option value=\"7\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 7 ? " selected=\"selected\"" : string.Empty) + ">" + Strings.ToString(!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu7", SepFunctions.Get_Portal_ID())) ? SepFunctions.PortalSetup("SiteMenu7", SepFunctions.Get_Portal_ID()) : SepFunctions.LangText("Site Menu ~~7~~")) + "</option>");
                    output.WriteLine("<option value=\"8\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 8 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Top Menu") + "</option>");
                    output.WriteLine("<option value=\"10\"" + Strings.ToString(SepFunctions.toLong(MenuID) == 10 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Main Menu") + "</option>");
                }
            }

            output.WriteLine("</select>");
        }
    }
}