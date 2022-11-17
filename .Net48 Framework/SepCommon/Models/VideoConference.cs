// ***********************************************************************
// Assembly         : SepCommon
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

namespace SepCommon.Models
{
    using System;

    /// <summary>
    /// Class VideoSchedule.
    /// </summary>
    public class VideoSchedule
    {
        /// <summary>
        /// 
        /// </summary>
        public long MeetingID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Accepted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateAccepted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FromUserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
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
