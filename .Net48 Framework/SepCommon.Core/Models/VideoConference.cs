// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="AffiliateHistory.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.Models
{
    using System;

    /// <summary>
    /// Class VideoSchedule.
    /// </summary>
    public class VideoSchedule
    {
        public long MeetingID { get; set; }
        public DateTime StartDate { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Accepted { get; set; }
        public DateTime DateAccepted { get; set; }
        public string Notes { get; set; }
        public string FromUserID { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string UserID { get; set; }

    }

    /// <summary>
    /// Class VideoConfig.
    /// </summary>
    public class VideoConfig
    {
        public bool ContactOnline { get; set; }
        public string SundayAvailableStart { get; set; }
        public string SundayAvailableEnd { get; set; }
        public string MondayAvailableStart { get; set; }
        public string MondayAvailableEnd { get; set; }
        public string TuesdayAvailableStart { get; set; }
        public string TuesdayAvailableEnd { get; set; }
        public string WednesdayAvailableStart { get; set; }
        public string WednesdayAvailableEnd { get; set; }
        public string ThursdayAvailableStart { get; set; }
        public string ThursdayAvailableEnd { get; set; }
        public string FridayAvailableStart { get; set; }
        public string FridayAvailableEnd { get; set; }
        public string SaturdayAvailableStart { get; set; }
        public string SaturdayAvailableEnd { get; set; }
        public string UserID { get; set; }

    }
}
