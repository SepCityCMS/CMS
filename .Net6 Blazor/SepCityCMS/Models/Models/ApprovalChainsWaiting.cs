// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ApprovalChainsWaiting.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class ApprovalChainsWaiting.
    /// </summary>
    public class ApprovalChainsWaiting
    {
        /// <summary>
        /// Gets or sets the name of the chain.
        /// </summary>
        /// <value>The name of the chain.</value>
        public string ChainName { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }

        /// <summary>
        /// Gets or sets the xmlid.
        /// </summary>
        /// <value>The xmlid.</value>
        public long XMLID { get; set; }
    }
}