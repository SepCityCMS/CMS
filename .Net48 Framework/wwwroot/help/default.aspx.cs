// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="default.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;

    /// <summary>
    /// Class _default2.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class _default2 : Page
    {
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
            if (SepFunctions.toInt(SepCommon.SepCore.Request.Item("HelpId")) > 0) IFrameContent.Text = "<iframe name=\"hmcontent\" id=\"hmcontent\" src=\"searchresult.aspx?ArticleID=" + SepFunctions.toInt(SepCommon.SepCore.Request.Item("HelpId")) + "\" scrolling=\"auto\" title=\"Content Page\" frameborder=\"0\"></iframe>";
            else IFrameContent.Text = "<iframe name=\"hmcontent\" id=\"hmcontent\" src=\"dashboard.aspx\" scrolling=\"auto\" title=\"Content Page\" frameborder=\"0\"></iframe>";
        }
    }
}