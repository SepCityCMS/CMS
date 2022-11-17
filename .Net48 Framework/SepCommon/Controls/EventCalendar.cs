// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="EventCalendar.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Text;

    /// <summary>
    /// Class EventCalendar.
    /// </summary>
    public class EventCalendar
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(46, "EventsEnable") != "Enable")
            {
                return output.ToString();
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("EventsAccess"), true) == false)
            {
                return output.ToString();
            }

            output.Append(SepFunctions.Draw_Calendar(84, SepFunctions.toDate(SepCommon.SepCore.Request.Item("EventDate"))));

            return output.ToString();
        }
    }
}