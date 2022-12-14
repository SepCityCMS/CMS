// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="FlashMessenger.cs" company="SepCity, Inc.">
//     Copyright ? SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using CuteChat;
    using SepCommon;
    using System.Text;
    using Strings = SepCommon.SepCore.Strings;

    /// <summary>
    /// Class FlashMessenger.
    /// </summary>
    public class FlashMessenger
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (SepFunctions.Setup(GlobalVars.ModuleID, "VideoConferenceEnable") != "Enable")
            {
                if (SepFunctions.Setup(6, "IMessengerEnable") != "Enable")
                {
                    output.AppendLine(string.Empty);

                    return output.ToString();
                }

                if (SepFunctions.CompareKeys(SepFunctions.Security("IMessengerAccess"), true) == false)
                {
                    output.AppendLine(string.Empty);

                    return output.ToString();
                }
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_Name()) && !string.IsNullOrWhiteSpace(SepFunctions.Session_Password()) && string.IsNullOrWhiteSpace(Strings.Replace(Strings.ToString(SepCommon.SepCore.Session.getSession(Strings.Left(Strings.Replace(SepFunctions.Setup(992, "WebSiteName"), " ", string.Empty), 5) + "MessengerOpened")), "'", string.Empty)))
            {
                if (System.IO.File.Exists(SepFunctions.GetDirValue("CuteSoft_Client") + "CuteChat\\IntegrationUtility.js"))
                {
                    if (ChatSystem.HasStarted == false)
                    {
                        try
                        {
                            ChatSystem.Start(new AppSystem());
                        }
                        catch
                        {
                        }
                    }
                    var sInstallFolder = SepFunctions.GetInstallFolder();

                    output.AppendLine("<script src=\"" + sInstallFolder + "CuteSoft_Client/CuteChat/IntegrationUtility.js\" type=\"text/javascript\"></script>");
                    output.AppendLine("<script type=\"text/javascript\">");
                    output.AppendLine("$(document).ready(function() {");
                    output.AppendLine("  try{Chat_OpenMessengerDialog();}catch(e){}");
                    output.AppendLine("});");
                    output.AppendLine("</script>");
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(SepFunctions.Setup(992, "WebSiteName"), " ", string.Empty), 5) + "MessengerOpened", "true");
                }
            }

            return output.ToString();
        }
    }
}