// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="Messages.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class Messages.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>The date sent.</value>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or sets the default picture.
        /// </summary>
        /// <value>The default picture.</value>
        public string DefaultPicture { get; set; }

        /// <summary>
        /// Gets or sets from user identifier.
        /// </summary>
        /// <value>From user identifier.</value>
        public string FromUserID { get; set; }

        /// <summary>
        /// Gets or sets from username.
        /// </summary>
        /// <value>From username.</value>
        public string FromUsername { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public long MessageID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read new].
        /// </summary>
        /// <value><c>true</c> if [read new]; otherwise, <c>false</c>.</value>
        public bool ReadNew { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Converts to userid.
        /// </summary>
        /// <value>To user identifier.</value>
        public string ToUserID { get; set; }

        /// <summary>
        /// Converts to username.
        /// </summary>
        /// <value>To username.</value>
        public string ToUsername { get; set; }

        /// <summary>
        /// Gets or sets the user photo.
        /// </summary>
        /// <value>The user photo.</value>
        public string UserPhoto { get; set; }
    }
}