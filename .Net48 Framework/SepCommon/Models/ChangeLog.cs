// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ChangeLog.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class ChangeLog.
    /// </summary>
    public class ChangeLog
    {
        /// <summary>
        /// Gets or sets the change data.
        /// </summary>
        /// <value>The change data.</value>
        public string ChangeData { get; set; }

        /// <summary>
        /// Gets or sets the change identifier.
        /// </summary>
        /// <value>The change identifier.</value>
        public long ChangeID { get; set; }

        /// <summary>
        /// Gets or sets the date changed.
        /// </summary>
        /// <value>The date changed.</value>
        public DateTime DateChanged { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public long ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }

        /// <summary>
        /// Gets or sets the view URL.
        /// </summary>
        /// <value>The view URL.</value>
        public string ViewURL { get; set; }
    }
}