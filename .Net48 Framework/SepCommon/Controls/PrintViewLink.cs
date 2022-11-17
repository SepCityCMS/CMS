// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="PrintViewLink.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class PrintViewLink.
    /// </summary>
    public class PrintViewLink
    {
        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<a href=\"" + SepCommon.SepCore.Request.ServerVariables("SCRIPT_NAME") + "?TopMenu=False\">" + SepFunctions.LangText("Print") + "</a>");

            return output.ToString();
        }
    }
}