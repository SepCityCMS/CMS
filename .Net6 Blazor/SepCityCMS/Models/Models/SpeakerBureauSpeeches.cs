// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="SpeakerBureauSpeeches.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Models
{
    /// <summary>
    /// Class SpeakerBureauSpeeches.
    /// </summary>
    public class SpeakerBureauSpeeches
    {
        /// <summary>
        /// Gets or sets the portal identifier.
        /// </summary>
        /// <value>The portal identifier.</value>
        public long PortalID { get; set; }

        /// <summary>
        /// Gets or sets the speaker identifier.
        /// </summary>
        /// <value>The speaker identifier.</value>
        public long SpeakerID { get; set; }

        /// <summary>
        /// Gets or sets the speech identifier.
        /// </summary>
        /// <value>The speech identifier.</value>
        public long SpeechID { get; set; }

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
    }
}