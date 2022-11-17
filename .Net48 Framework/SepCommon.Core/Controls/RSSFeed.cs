// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="RSSFeed.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls.Core
{
    using SepCommon.Core;
    using SepCommon.Core.SepCore;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class RSSFeed.
    /// </summary>
    public class RSSFeed
    {
        /// <summary>
        /// The m URL
        /// </summary>
        private string m_URL;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string URL
        {
            get => Strings.ToString(m_URL);

            set => m_URL = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();
            if (string.IsNullOrWhiteSpace(URL))
            {
                output.AppendLine("URL is Required");
                return output.ToString();
            }

            string[] arrURL = null;

            if (Strings.InStr(URL, "||") > 0)
            {
                arrURL = Strings.Split(URL, "||");
                Array.Resize(ref arrURL, 2);
                var inidata = string.Empty;

                using (var objReader = new StreamReader(SepFunctions.GetDirValue("skins") + arrURL[1] + ".html"))
                {
                    inidata = objReader.ReadToEnd();
                }

                if (!string.IsNullOrWhiteSpace(inidata))
                {
                    output.Append(SepFunctions.RSS_Feed_Read(arrURL[0], inidata));
                }
                else
                {
                    output.Append(SepFunctions.RSS_Feed_Read(arrURL[0]));
                }
            }
            else
            {
                output.Append(SepFunctions.RSS_Feed_Read(URL));
            }

            return output.ToString();
        }
    }
}