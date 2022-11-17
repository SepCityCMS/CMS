// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UserPagesMenu.cs" company="SepCity, Inc.">
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
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class UserPagesMenu.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:UserPagesMenu runat=server></{0}:UserPagesMenu>")]
    public class UserPagesMenu : WebControl
    {
        /// <summary>
        /// The m menu identifier
        /// </summary>
        private int m_MenuID;

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
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
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (MenuID == 0)
            {
                output.WriteLine("MenuID is Required");
                return;
            }

            var str = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(7, "UPagesMenu" + MenuID)) && SepFunctions.isUserPage())
                {
                    str.AppendLine("<div class=\"row\">");
                    str.AppendLine("<ul class=\"sb_menu sb_menu7" + MenuID + "\" id=\"" + ID + "\">");
                    using (var cmd = new SqlCommand("SELECT PageID,PageName,PageTitle FROM UPagesPages WHERE MenuID=7" + MenuID + " AND UserID='" + SepFunctions.GetUserID(Request.Item("UserName")) + "' ORDER BY Weight,PageName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                str.Append("<li><a href=\"" + sInstallFolder + SepFunctions.openNull(RS["PageName"]) + "\">" + SepFunctions.openNull(RS["PageTitle"]) + "</a></li>");
                            }
                        }
                    }

                    str.AppendLine("</ul>");
                    str.AppendLine("</div>");
                }
            }

            output.Write(Strings.ToString(str));
        }
    }
}