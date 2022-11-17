// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 05-07-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="save_image.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.spadmin.ImageManager
{
    using SepCommon;
    using System;
    using System.IO;

    /// <summary>
    /// Class save_image.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class save_image : System.Web.UI.Page
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
            string sBase = SepFunctions.GetDirValue("skins") + "images\\";

            byte[] imageBytes = Convert.FromBase64String(SepCommon.SepCore.Strings.Replace(SepCommon.SepCore.Request.Form("imageBase64"), "data:image/png;base64,", string.Empty));
            string fileId = SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity());

            string filePath = sBase + fileId + Path.GetExtension(SepCommon.SepCore.Request.Form("FileName"));
            File.WriteAllBytes(filePath, imageBytes);

            SepCommon.SepCore.Response.Write(fileId + Path.GetExtension(SepCommon.SepCore.Request.Form("FileName")));
        }
    }
}