// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Debugging.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using SepCommon.Core.SepCore;
    using System;
    using System.IO;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Debug log.
        /// </summary>
        /// <param name="sText">The text.</param>
        public static void Debug_Log(string sText)
        {
            using (var outfile = new StreamWriter(GetDirValue("App_Data") + "debug.log", true))
            {
                outfile.Write(Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.LongDate) + " : " + sText + Environment.NewLine);
            }
        }

        /// <summary>
        /// Debug query string.
        /// </summary>
        public static void Debug_QueryString()
        {
            foreach (var Item in Request.Form())
            {
                Debug_Log(Item + " - " + HTMLEncode(Request.Form(Strings.ToString(Item))));
            }

            foreach (var Item in Request.QueryString())
            {
                Debug_Log(Item + " - " + HTMLEncode(Request.QueryString(Strings.ToString(Item))));
            }

            Debug_Log(Environment.NewLine);
        }
    }
}