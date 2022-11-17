// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="DateAndTime.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.SepCore
{
    using System;
    using System.Globalization;

    /// <summary>
    /// A date and time.
    /// </summary>
    public static class DateAndTime
    {
        /// <summary>
        /// Values that represent date intervals.
        /// </summary>
        public enum DateInterval
        {
            /// <summary>
            /// An enum constant representing the day option.
            /// </summary>
            Day = 0,

            /// <summary>
            /// An enum constant representing the month option.
            /// </summary>
            Month = 1,

            /// <summary>
            /// An enum constant representing the year option.
            /// </summary>
            Year = 2,

            /// <summary>
            /// An enum constant representing the hour option.
            /// </summary>
            Hour = 3,

            /// <summary>
            /// An enum constant representing the minute option.
            /// </summary>
            Minute = 4,

            /// <summary>
            /// An enum constant representing the week of year option.
            /// </summary>
            WeekOfYear = 5,

            /// <summary>
            /// An enum constant representing the second option.
            /// </summary>
            Second = 6
        }

        /// <summary>
        /// Date add.
        /// </summary>
        /// <param name="DateInterval">The date interval.</param>
        /// <param name="Number">Number of.</param>
        /// <param name="DateValue">The date value Date/Time.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime DateAdd(DateInterval DateInterval, int Number, DateTime DateValue)
        {
            switch (DateInterval)
            {
                case DateInterval.Second:
                    return DateValue.AddSeconds(Number);

                case DateInterval.Minute:
                    return DateValue.AddMinutes(Number);

                case DateInterval.Hour:
                    return DateValue.AddHours(Number);

                case DateInterval.WeekOfYear:
                    return DateValue.AddDays(Number * 7);

                case DateInterval.Month:
                    return DateValue.AddMonths(Number);

                case DateInterval.Year:
                    return DateValue.AddYears(Number);

                default:
                    return DateValue.AddDays(Number);
            }
        }

        /// <summary>
        /// Date difference.
        /// </summary>
        /// <param name="DateInterval">The date interval.</param>
        /// <param name="Date1">The date 1 Date/Time.</param>
        /// <param name="Date2">The date 2 Date/Time.</param>
        /// <returns>A long.</returns>
        public static double DateDiff(DateInterval DateInterval, DateTime Date1, DateTime Date2)
        {
            switch (DateInterval)
            {
                case DateInterval.Second:
                    return (Date2 - Date1).TotalSeconds;

                case DateInterval.Minute:
                    return (Date2 - Date1).Minutes;

                case DateInterval.Hour:
                    return (Date2 - Date1).Hours;

                case DateInterval.Month:
                    return ((Date2.Year - Date1.Year) * 12) + Date2.Month - Date1.Month;

                case DateInterval.Year:
                    if ((Date2 - Date1).Days > 0)
                    {
                        return (Date2 - Date1).Days / 365;
                    }
                    else
                    {
                        return 0;
                    }

                default:
                    return (Date2 - Date1).Days;
            }
        }

        /// <summary>
        /// Days the given date value.
        /// </summary>
        /// <param name="DateValue">The date value Date/Time.</param>
        /// <returns>An int.</returns>
        public static int Day(DateTime DateValue)
        {
            return DateValue.Day;
        }

        /// <summary>
        /// Months the given date value.
        /// </summary>
        /// <param name="DateValue">The date value Date/Time.</param>
        /// <returns>An int.</returns>
        public static int Month(DateTime DateValue)
        {
            return DateValue.Month;
        }

        /// <summary>
        /// Month name.
        /// </summary>
        /// <param name="Month">The month.</param>
        /// <returns>A string.</returns>
        public static string MonthName(int Month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[Month - 1];
        }

        /// <summary>
        /// Weekday name.
        /// </summary>
        /// <param name="Weekday">The weekday.</param>
        /// <param name="Abbreviate">(Optional) True to abbreviate.</param>
        /// <returns>A string.</returns>
        public static string WeekdayName(int Weekday, bool Abbreviate = false)
        {
            if (Abbreviate)
            {
                return Strings.Left(CultureInfo.CurrentCulture.DateTimeFormat.DayNames[Weekday - 1], 3);
            }
            else
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.DayNames[Weekday - 1];
            }
        }

        /// <summary>
        /// Years the given date value.
        /// </summary>
        /// <param name="DateValue">The date value Date/Time.</param>
        /// <returns>An int.</returns>
        public static int Year(DateTime DateValue)
        {
            return DateValue.Year;
        }
    }
}