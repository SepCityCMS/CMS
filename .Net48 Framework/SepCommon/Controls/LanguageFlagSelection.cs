// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="LanguageFlagSelection.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.Text;

    /// <summary>
    /// Class LanguageFlagSelection.
    /// </summary>
    public class LanguageFlagSelection
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=en&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_english.gif\" alt=\"English Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=fr&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_french.gif\" alt=\"French Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=ms&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_malaysia.png\" alt=\"Malaya Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=pt&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_portuguese.gif\" alt=\"Portuguese Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=ru&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_russian.gif\" alt=\"Russian Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=es&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_spanish.gif\" alt=\"Spanish Version\" border=\"0\" /></a> ");
            output.AppendLine("<a href=\"http://translate.google.com/translate?js=n&prev=_t&hl=en&ie=UTF-8&layout=2&eotf=1&sl=" + Strings.Left(SepFunctions.GetSiteLanguage(), 2) + "&tl=nl&u=" + SepFunctions.GetSiteDomain() + "\"><img src=\"" + SepFunctions.GetDirValue("images", true) + "spadmin/flag_dutch.gif\" alt=\"Dutch Version\" border=\"0\" /></a>");

            return output.ToString();
        }
    }
}