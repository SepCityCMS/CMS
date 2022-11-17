// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="ForumTopics.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class ForumTopics.
    /// </summary>
    public class ForumTopics
    {
        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>The attachment.</value>
        public string Attachment { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>The cat identifier.</value>
        public long CatID { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>The date posted.</value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the date registered.
        /// </summary>
        /// <value>The date registered.</value>
        public DateTime DateRegistered { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email reply].
        /// </summary>
        /// <value><c>true</c> if [email reply]; otherwise, <c>false</c>.</value>
        public bool EmailReply { get; set; }

        /// <summary>
        /// Gets or sets the expire date.
        /// </summary>
        /// <value>The expire date.</value>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Gets or sets the hits.
        /// </summary>
        /// <value>The hits.</value>
        public long Hits { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the online status.
        /// </summary>
        /// <value>The online status.</value>
        public string OnlineStatus { get; set; }

        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the profile identifier.
        /// </summary>
        /// <value>The profile identifier.</value>
        public long ProfileID { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        /// <value>The profile image.</value>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>The replies.</value>
        public long Replies { get; set; }

        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>The reply identifier.</value>
        public long ReplyID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier.
        /// </summary>
        /// <value>The topic identifier.</value>
        public long TopicID { get; set; }

        /// <summary>
        /// Gets or sets the total posts.
        /// </summary>
        /// <value>The total posts.</value>
        public long TotalPosts { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
    }
}