// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ContentImages.cs" company="SepCity, Inc.">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class ContentImages.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ContentImages runat=server></{0}:ContentImages>")]
    public class ContentImages : WebControl
    {
        /// <summary>
        /// The m content unique identifier
        /// </summary>
        private string m_ContentUniqueID;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// The m user identifier
        /// </summary>
        private string m_UserID;

        /// <summary>
        /// Gets or sets the content unique identifier.
        /// </summary>
        /// <value>The content unique identifier.</value>
        public string ContentUniqueID
        {
            get => Strings.ToString(m_ContentUniqueID);

            set => m_ContentUniqueID = value;
        }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get => Strings.ToString(m_Text);

            set => m_Text = value;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID
        {
            get => Strings.ToString(m_UserID);

            set => m_UserID = value;
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
        /// Raises the <see cref="System.Web.UI.Control.PreRender" /> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs" /> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);
            var cName = "prettyPhotoJS";
            var cUrl = sImageFolder + "js/jquery/jquery.prettyPhoto.js";
            var csType = GetType();

            var cs = Page.ClientScript;

            if (!cs.IsClientScriptIncludeRegistered(csType, cName))
            {
                cs.RegisterClientScriptInclude(csType, cName, ResolveClientUrl(cUrl));
            }
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            long nextRow = 0;

            var wc = string.Empty;

            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (!string.IsNullOrWhiteSpace(ContentUniqueID))
            {
                wc += " AND Uploads.UniqueID='" + SepFunctions.FixWord(ContentUniqueID) + "'";
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                wc += " AND Uploads.UserID='" + SepFunctions.FixWord(UserID) + "'";
            }

            output.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + sImageFolder + "skins/public/styles/prettyPhoto.css\" />");

            output.WriteLine("<script type=\"text/javascript\" charset=\"utf-8\">");
            output.WriteLine("$(document).ready(function () {");
            output.WriteLine("$(\"a[rel^='prettyPhoto']\").prettyPhoto({");
            output.WriteLine("animationSpeed:  'normal', /* fast/slow/normal */");
            output.WriteLine("opacity: 0.80, /* Value between 0 and 1 */");
            output.WriteLine("showTitle: false /* true/false */");
            output.WriteLine("});");
            output.WriteLine("});");
            output.WriteLine("</script>");

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UploadID FROM Uploads WHERE Uploads.ModuleID=@ModuleID AND Uploads.isTemp='0' AND Uploads.Approved='1' AND (Right(FileName, 4) = '.png' OR Right(FileName, 4) = '.jpg' OR Right(FileName, 4) = '.gif')" + wc + " ORDER BY Weight ASC,DatePosted DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@ModuleID", ModuleID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            nextRow += 1;
                            output.WriteLine("<span style=\"padding:5px;\"><a href=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "\" rel=\"prettyPhoto[pp_gal]\"><img src=\"" + sImageFolder + "spadmin/show_image.aspx?UploadID=" + SepFunctions.openNull(RS["UploadID"]) + "&Size=thumb\" border=\"0\" alt=\"\" /></a></span>");
                            if (nextRow == 2 && ModuleID != 28)
                            {
                                output.WriteLine("<br/>");
                                nextRow = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}