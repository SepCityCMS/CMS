// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="FeedParser.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// .
        /// </summary>
        private const string attributePattern = "\\bname=\"[^\"]*?\"";

        /// <summary>
        /// A pattern specifying the container.
        /// </summary>
        private const string containerPattern = "<name\\b[^>]*?(/>|>(\\w|\\W)*?</name>)";

        /// <summary>
        /// A pattern specifying the strip.
        /// </summary>
        private const string stripPattern = "</{0,1}name[^>]*?>";

        /// <summary>
        /// Parses.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="container">The container.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="maxResults">The maximum results.</param>
        /// <param name="allowHTML">True to allow, false to suppress the HTML.</param>
        /// <returns>An ArrayList.</returns>
        public static ArrayList Parse(string content, string container, string[] keys, string[] fields, int maxResults, bool allowHTML)
        {
            var results = new ArrayList();
            var items = Regex.Matches(content, containerPattern.Replace("name", container));

            foreach (Match item in items)
            {
                var pairs = new NameValueCollection();
                foreach (var field in fields)
                {
                    var index = Array.IndexOf(fields, field);
                    var mask = field;
                    var pos = field.IndexOf('@');
                    if (pos > -1)
                    {
                        mask = field.Substring(0, pos);
                    }

                    var found = Regex.Match(item.Value, containerPattern.Replace("name", mask));
                    if (pos > -1 && !found.Equals(Match.Empty))
                    {
                        mask = field.Substring(pos + 1);
                        found = Regex.Match(found.Value, attributePattern.Replace("name", mask));
                    }

                    if (!found.Equals(Match.Empty))
                    {
                        string value;
                        if (pos > -1)
                        {
                            value = found.Value.Replace(mask + "=", string.Empty).Replace("\"", string.Empty);
                        }
                        else
                        {
                            value = Regex.Replace(found.Value, stripPattern.Replace("name", field), string.Empty);
                        }

                        // keep untagged entity information
                        if (value.IndexOf("<![CDATA[", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            value = value.Substring(0, value.Length - 3).Substring(9);
                        }

                        if (allowHTML == false)
                        {
                            value = System.Net.WebUtility.HtmlDecode(value);

                            // tranform breaks to new lines
                            value = Regex.Replace(value, stripPattern.Replace("name", "br"), Environment.NewLine);

                            // remove all tags
                            value = Regex.Replace(value, stripPattern.Replace("name", string.Empty), string.Empty);

                            // trim lines
                            value = value.Replace(" " + Environment.NewLine, Environment.NewLine).Trim('\r', '\n');
                        }

                        pairs.Add(keys[index], value);
                    }
                }

                results.Add(pairs);
                if (results.Count == maxResults)
                {
                    break;
                }
            }

            return results;
        }
    }
}