// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Globals.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using SepCommon.SepCore;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        // USED IN ASPX PAGES
        /// <summary>
        /// Dates the picker.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="showTime">The show time.</param>
        /// <param name="showDate">The show date.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <returns>System.String.</returns>
        public static string Date_Picker(string fieldName, string showTime, string showDate, string minDate)
        {
            var sReturn = new StringBuilder();

            var siteLang = GetSiteLanguage();
            siteLang = Strings.LCase(siteLang.Split('-')[0]) + "-" + siteLang.Split('-')[1];

            switch (siteLang)
            {
                case "nl-NL":
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        sReturn.Append("format: 'd-m-Y',");
                    }

                    sReturn.Append("datepicker: " + showDate + ",");
                    sReturn.Append("lang: 'nl'" + string.Empty);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate + string.Empty);
                    }

                    sReturn.Append("});");
                    break;

                case "fr-CA":
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        sReturn.Append("format: 'Y-m-d',");
                    }

                    sReturn.Append("datepicker: " + showDate + ",");
                    sReturn.Append("lang: 'fr'" + string.Empty);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate + string.Empty);
                    }

                    sReturn.Append("});");
                    break;

                case "fr-FR":
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        sReturn.Append("format: 'd/m/Y',");
                    }

                    sReturn.Append("datepicker: " + showDate + ",");
                    sReturn.Append("lang: 'fr'" + string.Empty);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate + string.Empty);
                    }

                    sReturn.Append("});");
                    break;

                case "pt-BR":
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        sReturn.Append("format: 'd/m/Y',");
                    }

                    sReturn.Append("datepicker: " + showDate + ",");
                    sReturn.Append("lang: 'pt'" + string.Empty);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate + string.Empty);
                    }

                    sReturn.Append("});");
                    break;

                case "es-ES":
                case "es-MX":
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        sReturn.Append("format: 'd/m/Y',");
                    }

                    sReturn.Append("datepicker: " + showDate + ",");
                    sReturn.Append("lang: 'es'" + string.Empty);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate + string.Empty);
                    }

                    sReturn.Append("});");
                    break;

                default:
                    sReturn.Append("$('#" + fieldName + "').datetimepicker({");
                    sReturn.Append("timepicker: " + showTime + ",");
                    if (showDate == "false")
                    {
                        sReturn.Append("format: 'H:i',");
                    }
                    else
                    {
                        if (showTime == "true")
                        {
                            sReturn.Append("format: '" + getJavascriptDateFormat() + " h:m a',");
                        }
                        else
                        {
                            //sReturn.Append("format: 'm/d/Y',");
                            sReturn.Append("format: '" + getJavascriptDateFormat() + "',");
                        }
                    }

                    sReturn.Append("datepicker: " + showDate);
                    if (!string.IsNullOrWhiteSpace(minDate))
                    {
                        sReturn.Append(", minDate: " + minDate);
                    }

                    sReturn.Append("});");
                    sReturn.Append("$('#" + fieldName + "').attr('autocomplete', 'off');");
                    break;
            }

            return Strings.ToString(sReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getJavascriptDateFormat()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Replace("yyyy", "Y").Replace("MM", "m").Replace("M", "m").Replace("dd", "d");
        }

        /// <summary>
        /// Translate status.
        /// </summary>
        /// <param name="StatusID">Identifier for the status.</param>
        /// <returns>A string.</returns>
        public static string Translate_Status(string StatusID)
        {
            switch (toInt(StatusID))
            {
                case 0:
                    return LangText("Pending");

                case 1:
                    return LangText("Active");

                default:
                    return LangText("N/A");
            }
        }
    }
}