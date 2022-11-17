// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Categories.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    /// <summary>
    /// Class Categories.
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// Gets or sets a value indicating whether [access hide].
        /// </summary>
        /// <value><c>true</c> if [access hide]; otherwise, <c>false</c>.</value>
        public bool AccessHide { get; set; }

        /// <summary>
        /// Gets or sets the access keys.
        /// </summary>
        /// <value>The access keys.</value>
        public string AccessKeys { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>The name of the category.</value>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the type of the cat.
        /// </summary>
        /// <value>The type of the cat.</value>
        public string CatType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exc portal security].
        /// </summary>
        /// <value><c>true</c> if [exc portal security]; otherwise, <c>false</c>.</value>
        public bool ExcPortalSecurity { get; set; }

        /// <summary>
        /// Gets or sets the image data.
        /// </summary>
        /// <value>The image data.</value>
        public string ImageData { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the list under.
        /// </summary>
        /// <value>The list under.</value>
        public long ListUnder { get; set; }

        /// <summary>
        /// Gets or sets the manage keys.
        /// </summary>
        /// <value>The manage keys.</value>
        public string ManageKeys { get; set; }

        /// <summary>
        /// Gets or sets the moderator.
        /// </summary>
        /// <value>The moderator.</value>
        public string Moderator { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the module i ds.
        /// </summary>
        /// <value>The module i ds.</value>
        public string ModuleIDs { get; set; }

        /// <summary>
        /// Gets or sets the portal i ds.
        /// </summary>
        /// <value>The portal i ds.</value>
        public string PortalIDs { get; set; }

        /// <summary>
        /// Gets or sets the seo description.
        /// </summary>
        /// <value>The seo description.</value>
        public string SEODescription { get; set; }

        /// <summary>
        /// Gets or sets the seo page title.
        /// </summary>
        /// <value>The seo page title.</value>
        public string SEOPageTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Categories" /> is sharing.
        /// </summary>
        /// <value><c>true</c> if sharing; otherwise, <c>false</c>.</value>
        public bool Sharing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show list].
        /// </summary>
        /// <value><c>true</c> if [show list]; otherwise, <c>false</c>.</value>
        public bool ShowList { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public long Weight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [write hide].
        /// </summary>
        /// <value><c>true</c> if [write hide]; otherwise, <c>false</c>.</value>
        public bool WriteHide { get; set; }

        /// <summary>
        /// Gets or sets the write keys.
        /// </summary>
        /// <value>The write keys.</value>
        public string WriteKeys { get; set; }
    }
}