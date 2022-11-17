// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Portals.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    /// <summary>
    /// Class Portals.
    /// </summary>
    public class Portals
    {
        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        /// <value>The name of the domain.</value>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>The name of the friendly.</value>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide list].
        /// </summary>
        /// <value><c>true</c> if [hide list]; otherwise, <c>false</c>.</value>
        public bool HideList { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the login keys.
        /// </summary>
        /// <value>The login keys.</value>
        public string LoginKeys { get; set; }

        /// <summary>
        /// Gets or sets the manage keys.
        /// </summary>
        /// <value>The manage keys.</value>
        public string ManageKeys { get; set; }

        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>The plan identifier.</value>
        public long PlanID { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the portal title.
        /// </summary>
        /// <value>The portal title.</value>
        public string PortalTitle { get; set; }

        /// <summary>
        /// Gets or sets the portal URL.
        /// </summary>
        /// <value>The portal URL.</value>
        public string PortalUrl { get; set; }

        /// <summary>
        /// Gets or sets the site logo URL.
        /// </summary>
        /// <value>The site logo URL.</value>
        public string SiteLogoURL { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public long Status { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}