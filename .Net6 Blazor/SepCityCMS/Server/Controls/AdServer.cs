// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AdServer.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Class AdServer.
    /// </summary>
    public class AdServer
    {
        /// <summary>
        /// The m ad identifier
        /// </summary>
        private string m_AdID;

        /// <summary>
        /// The m preview
        /// </summary>
        private bool m_Preview;

        /// <summary>
        /// The m zone identifier
        /// </summary>
        private string m_ZoneID;

        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>The ad identifier.</value>
        public string AdID
        {
            get => Strings.ToString(m_AdID);

            set => m_AdID = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AdServer" /> is preview.
        /// </summary>
        /// <value><c>true</c> if preview; otherwise, <c>false</c>.</value>
        public bool Preview
        {
            get => Convert.ToBoolean(m_Preview);

            set => m_Preview = value;
        }

        /// <summary>
        /// Gets or sets the zone identifier.
        /// </summary>
        /// <value>The zone identifier.</value>
        public string ZoneID
        {
            get => Strings.ToString(m_ZoneID);

            set => m_ZoneID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ZoneID))
            {
                output.AppendLine("ZoneID is Required");
                return output.ToString();
            }

            if (SepFunctions.Setup(2, "AdsEnable") != "Enable")
            {
                output.Append(string.Empty);

                return output.ToString();
            }

            if (SepFunctions.isUserPage())
            {
                var sTemplateFolder = SepFunctions.getTemplateFolder();
                var sConfigFile = SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config_default.xml";

                if (File.Exists(SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config.xml"))
                {
                    sConfigFile = SepFunctions.GetDirValue("skins") + sTemplateFolder + "\\config.xml";
                }

                if (File.Exists(sConfigFile))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(sConfigFile))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"Advertisements\"]") != null)
                            {
                                if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"Advertisements\"]").InnerText == "0")
                                {
                                    return output.ToString();
                                }
                            }
                        }
                    }
                }
            }

            var iModuleID = 0;

            var wc = string.Empty;
            var GetImageID = string.Empty;

            var sInstallFolder = SepFunctions.GetInstallFolder();
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            if (SepFunctions.Setup(2, "AdsEnable") == "Enable")
            {
                if (!string.IsNullOrWhiteSpace(AdID))
                {
                    GetImageID = AdID;
                }
                else
                {
                    iModuleID = SepFunctions.toInt(Session.getSession("ModuleID"));

                    if (!string.IsNullOrWhiteSpace(Request.Item("CatID")))
                    {
                        wc += "(CatIDs LIKE '%|" + SepFunctions.FixWord(Request.Item("CatID")) + "|%' OR PageIDs LIKE '%|" + iModuleID + "|%' OR PageIDs LIKE '%|-1|%')";
                    }
                    else
                    {
                        wc += "(CatIDs LIKE '%|0|%' OR CatIDs IS NULL OR datalength(CatIDs) = 0)";
                        if (iModuleID > 0)
                        {
                            wc += " AND (PageIDs LIKE '%|" + iModuleID + "|%'";
                        }
                        else
                        {
                            wc += " AND (PageIDs LIKE '%|" + SepFunctions.FixWord(Request.Item("UniqueID")) + "|%'";
                        }

                        wc += " OR PageIDs LIKE '%|-1|%')";
                    }

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("SELECT AdID FROM Advertisements, TargetZones WHERE (TotalExposures < MaxExposures OR MaxExposures = -1) AND (TotalClicks < MaxClicks OR MaxClicks = -1) AND Advertisements.ZoneID=TargetZones.ZoneID AND Advertisements.ZoneID=" + SepFunctions.toLong(ZoneID) + " AND CONVERT(varchar, StartDate,126) <= CONVERT(varchar, GetDate(),126) AND CONVERT(varchar, EndDate,126) >= CONVERT(varchar, GetDate(),126) AND (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Advertisements.Status=1 ORDER BY NEWID()", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                                    {
                                        wc += " OR (Country='" + SepFunctions.FixWord(SepFunctions.GetUserInformation("Country")) + "' OR State='" + SepFunctions.FixWord(SepFunctions.GetUserInformation("State")) + "')";
                                    }
                                    else
                                    {
                                        var sCity = string.Empty;
                                        var sState = string.Empty;
                                        var sCountry = string.Empty;
                                        Integrations.SepFunctions.IP2Location(SepFunctions.GetUserIP(), ref sCity, ref sState, ref sCountry);
                                        if (!string.IsNullOrWhiteSpace(sState) || !string.IsNullOrWhiteSpace(sCountry))
                                        {
                                            wc += " OR (";
                                            if (!string.IsNullOrWhiteSpace(sCountry))
                                            {
                                                wc += "Country='" + SepFunctions.FixWord(sCountry) + "'";
                                            }

                                            if (!string.IsNullOrWhiteSpace(sCountry) && !string.IsNullOrWhiteSpace(sState))
                                            {
                                                wc += " OR ";
                                            }

                                            if (!string.IsNullOrWhiteSpace(sState))
                                            {
                                                wc += "State='" + SepFunctions.FixWord(sState) + "'";
                                            }

                                            wc += ")";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("SELECT TOP 1 AdID,Description,UseHTML,HTMLCode,ImageURL,ImageType,ImageData,TotalExposures FROM Advertisements, TargetZones WHERE (TotalExposures < MaxExposures OR MaxExposures = -1) AND (TotalClicks < MaxClicks OR MaxClicks = -1) AND Advertisements.ZoneID=TargetZones.ZoneID AND Advertisements.ZoneID=" + SepFunctions.toLong(ZoneID) + " AND  CONVERT(varchar, StartDate,126) <= CONVERT(varchar, GetDate(),126) AND  CONVERT(varchar, EndDate,126) >= CONVERT(varchar, GetDate(),126) AND (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%' OR datalength(PortalIDs) = 0) AND Advertisements.Status=1 AND (" + wc + ") ORDER BY NEWID()", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetImageID = SepFunctions.openNull(RS["AdID"]);
                                    if (Preview == false)
                                    {
                                        using (var cmd2 = new SqlCommand("UPDATE Advertisements SET TotalExposures='" + (SepFunctions.toLong(SepFunctions.openNull(RS["TotalExposures"])) + 1) + "' WHERE AdID='" + SepFunctions.FixWord(GetImageID) + "'", conn))
                                        {
                                            cmd2.ExecuteNonQuery();
                                        }
                                    }

                                    if (SepFunctions.openBoolean(RS["UseHTML"]))
                                    {
                                        output.Append(SepFunctions.openNull(RS["HTMLCode"]));
                                    }
                                    else
                                    {
                                        var sPreviewMode1 = Preview == false ? "<a href=\"" + sInstallFolder + "default.aspx?DoAction=AdRedirect&AdID=" + GetImageID + "\" target=\"" + SepFunctions.Setup(2, "SponsorsTarget") + "\">" : string.Empty;
                                        var sPreviewMode2 = Preview == false ? "</a>" : string.Empty;
                                        if (!Information.IsDBNull(RS["ImageData"]))
                                        {
                                            output.Append(sPreviewMode1 + "<img src=\"" + sImageFolder + "spadmin/show_image.aspx?ModuleID=2&AdID=" + GetImageID + "\" class=\"img-fluid\" border=\"0\" alt=\"" + SepFunctions.openNull(RS["Description"]) + "\"/>" + sPreviewMode2);
                                        }
                                        else
                                        {
                                            output.Append(sPreviewMode1 + "<img src=\"" + SepFunctions.openNull(RS["ImageURL"]) + "\" class=\"img-fluid\" border=\"0\" alt=\"" + SepFunctions.openNull(RS["Description"]) + "\"/>" + sPreviewMode2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}