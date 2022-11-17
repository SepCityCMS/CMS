// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Encoding.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using SepCommon.SepCore;
    using System;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Base 64 decode.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>A string.</returns>
        public static string Base64Decode(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return BytesToString(Convert.FromBase64String(str));
            }

            return string.Empty;
        }

        /// <summary>
        /// Base 64 encode.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>A string.</returns>
        public static string Base64Encode(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return Convert.ToBase64String(StringToBytes(str));
            }

            return string.Empty;
        }

        /// <summary>
        /// Bytes to string.
        /// </summary>
        /// <param name="ba">The ba.</param>
        /// <returns>A string.</returns>
        public static string BytesToString(byte[] ba)
        {
            var sencoding = Encoding.GetEncoding(0);
            return sencoding.GetString(ba);
        }

        /// <summary>
        /// Escape quotes.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>A string.</returns>
        public static string EscQuotes(string s)
        {
            return Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(Strings.Replace(s, "\\", "\\\\"), "'", "\\'"), Environment.NewLine, "\\r"), Environment.NewLine, "\\n"), "%", "%25"), "<", "%3C"), ">", "%3E"), "\"", "%22");
        }

        /// <summary>
        /// HTML decode.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <returns>A string.</returns>
        public static string HTMLDecode(string strText)
        {
            if (!string.IsNullOrWhiteSpace(strText))
            {
                return System.Net.WebUtility.HtmlDecode(strText);
            }

            return string.Empty;
        }

        /// <summary>
        /// HTML encode.
        /// </summary>
        /// <param name="strText">The text.</param>
        /// <returns>A string.</returns>
        public static string HTMLEncode(string strText)
        {
            if (!string.IsNullOrWhiteSpace(strText))
            {
                return System.Net.WebUtility.HtmlEncode(strText);
            }

            return string.Empty;
        }

        /// <summary>
        /// String to bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>A byte[].</returns>
        public static byte[] StringToBytes(string str)
        {
            var bencoding = Encoding.GetEncoding(0);
            return bencoding.GetBytes(str);
        }

        /// <summary>
        /// URL decode.
        /// </summary>
        /// <param name="sEncoded">The encoded.</param>
        /// <returns>A string.</returns>
        public static string UrlDecode(string sEncoded)
        {
            if (!string.IsNullOrWhiteSpace(sEncoded))
            {
                return System.Net.WebUtility.UrlDecode(sEncoded);
            }

            return string.Empty;
        }

        /// <summary>
        /// URL encode.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>A string.</returns>
        public static string UrlEncode(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                return System.Net.WebUtility.UrlEncode(s);
            }

            return string.Empty;
        }
    }
}