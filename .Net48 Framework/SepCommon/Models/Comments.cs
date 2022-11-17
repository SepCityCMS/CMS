// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Comments.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class Comments.
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        /// <value>The comment identifier.</value>
        public long CommentID { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the like identifier.
        /// </summary>
        /// <value>The like identifier.</value>
        public long LikeID { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>The reply identifier.</value>
        public long ReplyID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public long UniqueID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user dislikes].
        /// </summary>
        /// <value><c>true</c> if [user dislikes]; otherwise, <c>false</c>.</value>
        public bool UserDislikes { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user likes].
        /// </summary>
        /// <value><c>true</c> if [user likes]; otherwise, <c>false</c>.</value>
        public bool UserLikes { get; set; }
    }
}