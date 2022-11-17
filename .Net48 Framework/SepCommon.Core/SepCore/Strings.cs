// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Strings.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core.SepCore
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A strings.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Values that represent date named formats.
        /// </summary>
        public enum DateNamedFormat
        {
            /// <summary>
            /// An enum constant representing the general date option.
            /// </summary>
            GeneralDate = 0,

            /// <summary>
            /// An enum constant representing the long date option.
            /// </summary>
            LongDate = 1,

            /// <summary>
            /// An enum constant representing the long time option.
            /// </summary>
            LongTime = 2,

            /// <summary>
            /// An enum constant representing the short date option.
            /// </summary>
            ShortDate = 3,

            /// <summary>
            /// An enum constant representing the short time option.
            /// </summary>
            ShortTime = 4
        }

        /// <summary>
        /// Ascs.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>An int.</returns>
        public static int Asc(string str)
        {
            return Convert.ToInt32(Convert.ToChar(str));
        }

        /// <summary>
        /// Format currency.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns>The formatted currency.</returns>
        public static string FormatCurrency(object Expression)
        {
            try
            {
                return string.Format("{0:C}", Convert.ToDecimal(Expression));
            }
            catch
            {
                return string.Format("{0:C}", 0);
            }
        }

        /// <summary>
        /// Format date time.
        /// </summary>
        /// <param name="Exspession">The exspession Date/Time.</param>
        /// <param name="NamedFormat">The named format.</param>
        /// <returns>The formatted date time.</returns>
        public static string FormatDateTime(DateTime Exspession, DateNamedFormat NamedFormat)
        {
            switch (NamedFormat)
            {
                case DateNamedFormat.LongDate:
                    return Exspession.ToLongDateString();

                case DateNamedFormat.LongTime:
                    return Exspession.ToLongTimeString();

                case DateNamedFormat.ShortDate:
                    return Exspession.ToShortDateString();

                case DateNamedFormat.ShortTime:
                    return Exspession.ToShortTimeString();

                default:
                    return ToString(Exspession);
            }
        }

        /// <summary>
        /// Format number.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <param name="NumDigitsAfterDecimal">(Optional) Number of digits after decimals.</param>
        /// <returns>The formatted number.</returns>
        public static string FormatNumber(object Expression, int NumDigitsAfterDecimal = -1)
        {
            if (NumDigitsAfterDecimal > 0)
            {
                return ToString(decimal.Round(Convert.ToDecimal(Expression), NumDigitsAfterDecimal, MidpointRounding.AwayFromZero));
            }
            else
            {
                return ToString(Convert.ToInt32(Expression));
            }
        }

        /// <summary>
        /// In string.
        /// </summary>
        /// <param name="SearchString">The search string.</param>
        /// <param name="SearchFor">The search for.</param>
        /// <returns>An int.</returns>
        public static int InStr(string SearchString, string SearchFor)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(SearchFor))
            {
                return 0;
            }

            return SearchString.IndexOf(SearchFor, StringComparison.CurrentCultureIgnoreCase) + 1;
        }

        /// <summary>
        /// In string.
        /// </summary>
        /// <param name="StartPos">The start position.</param>
        /// <param name="SearchString">The search string.</param>
        /// <param name="SearchFor">The search for.</param>
        /// <returns>An int.</returns>
        public static int InStr(int StartPos, string SearchString, string SearchFor)
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(SearchFor))
            {
                return 0;
            }

            return SearchString.IndexOf(SearchFor, StartPos, StringComparison.CurrentCultureIgnoreCase) + 1;
        }

        /// <summary>
        /// In string reverse.
        /// </summary>
        /// <param name="StringCheck">The string check.</param>
        /// <param name="StringMatch">A match specifying the string.</param>
        /// <returns>An int.</returns>
        public static int InStrRev(string StringCheck, string StringMatch)
        {
            if (string.IsNullOrWhiteSpace(StringCheck))
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(StringMatch))
            {
                return 0;
            }

            return StringCheck.LastIndexOf(StringMatch, StringComparison.CurrentCultureIgnoreCase) + 1;
        }

        /// <summary>
        /// Joins.
        /// </summary>
        /// <param name="SourceArray">Array of sources.</param>
        /// <param name="delimiter">(Optional) The delimiter.</param>
        /// <returns>A string.</returns>
        public static string Join(object[] SourceArray, string delimiter = " ")
        {
            try
            {
                return string.Join(delimiter, SourceArray);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Cases.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns>A string.</returns>
        public static string LCase(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return Value;
            }

            return Value.ToLowerInvariant();
        }

        /// <summary>
        /// Lefts.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string Left(string param, int length)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return param;
            }

            length = Math.Abs(length);

            return (param.Length <= length ? param : param.Substring(0, length));
        }

        /// <summary>
        /// Lens.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>An int.</returns>
        public static int Len(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return 0;
            }

            return param.Length;
        }

        /// <summary>
        /// Trims.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>A string.</returns>
        public static string LTrim(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.TrimStart();
        }

        /// <summary>
        /// Mids.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string Mid(string param, int startIndex, int length)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return param;
            }

            return (param.Length <= length ? param : param.Substring(startIndex - 1, length));
        }

        /// <summary>
        /// Mids.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A string.</returns>
        public static string Mid(string param, int startIndex)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return param;
            }

            return (param.Length <= startIndex ? param : param.Substring(startIndex - 1));
        }

        /// <summary>
        /// Replaces.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <param name="Find">The find.</param>
        /// <param name="Replacement">The replacement.</param>
        /// <returns>A string.</returns>
        public static string Replace(string Expression, string Find, string Replacement)
        {
            if (string.IsNullOrWhiteSpace(Expression))
            {
                return Expression;
            }

            return Regex.Replace(Expression, Regex.Escape(Find), Replacement, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Rights.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string Right(string param, int length)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return param;
            }

            return (param.Length <= length ? param : param.Substring(param.Length - length, length));
        }

        /// <summary>
        /// Splits.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A string[].</returns>
        public static string[] Split(string param, string delimiter)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return null;
            }

            if (string.IsNullOrEmpty(delimiter))
            {
                return new[] { param };
            }

            try
            {
                return param.Split(new[] { delimiter }, StringSplitOptions.None);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(object s)
        {
            if (s == null)
            {
                return "";
            }

            try
            {
                return s.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Trims.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>A string.</returns>
        public static string Trim(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.Trim();
        }

        /// <summary>
        /// Cases.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns>A string.</returns>
        public static string UCase(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return Value;
            }

            return Value.ToUpperInvariant();
        }
    }
}