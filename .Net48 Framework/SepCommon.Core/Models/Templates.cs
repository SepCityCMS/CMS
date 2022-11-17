// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Templates.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class Templates.
    /// </summary>
    public class Templates
    {
        /// <summary>
        /// Gets or sets the access keys.
        /// </summary>
        /// <value>The access keys.</value>
        public string AccessKeys { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable user page].
        /// </summary>
        /// <value><c>true</c> if [enable user page]; otherwise, <c>false</c>.</value>
        public bool EnableUserPage { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        /// <value>The name of the folder.</value>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        public long Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the screen shot.
        /// </summary>
        /// <value>The screen shot.</value>
        public string ScreenShot { get; set; }

        /// <summary>
        /// Gets or sets the screen shot large.
        /// </summary>
        /// <value>The screen shot large.</value>
        public string ScreenShotLarge { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public long TemplateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use template].
        /// </summary>
        /// <value><c>true</c> if [use template]; otherwise, <c>false</c>.</value>
        public bool useTemplate { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        /// <value>The HTML.</value>
        public string HTML { get; set; }

        /// <summary>
        /// Gets or sets the CSS.
        /// </summary>
        /// <value>The CSS.</value>
        public string CSS { get; set; }
    }
}