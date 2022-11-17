// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="ResponseHelper.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.ApiTypes
{
    /// <summary>
    /// Class ResponseHelper.
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Class CreateResponse.
        /// </summary>
        public class CreateResponse
        {
            // Nested types should not be visible
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public long Id { get; set; }
        }

        /// <summary>
        /// Class DeleteResponseSuccess.
        /// </summary>
        public class DeleteResponseSuccess
        {
            // Nested types should not be visible
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="DeleteResponseSuccess" /> is success.
            /// </summary>
            /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
            public bool Success { get; set; }
        }
    }
}