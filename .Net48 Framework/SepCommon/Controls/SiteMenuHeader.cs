// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SiteMenuHeader.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Class SiteMenuHeader.
    /// </summary>
    public class SiteMenuHeader
    {
        /// <summary>
        /// The m menu identifier
        /// </summary>
        private int m_MenuID;

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public int MenuID
        {
            get
            {
                var s = Convert.ToInt32(m_MenuID);
                return s;
            }

            set => m_MenuID = value;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            if (MenuID == 0)
            {
                output.AppendLine("MenuID is Required");
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
                            if (MenuID == 3)
                            {
                                if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"MemberMenu\"]") != null)
                                {
                                    if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id=\"MemberMenu\"]").InnerText == "0")
                                    {
                                        return output.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string sReturn;

            if (MenuID > 0 && MenuID < 8)
            {
                if (SepFunctions.Get_Portal_ID() == 0)
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(993, "Menu" + MenuID + "Text")))
                    {
                        sReturn = SepFunctions.Setup(993, "Menu" + MenuID + "Text");
                    }
                    else
                    {
                        sReturn = SepFunctions.LangText("Site Menu ~~" + MenuID + "~~");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("SiteMenu" + MenuID, SepFunctions.Get_Portal_ID())))
                    {
                        sReturn = SepFunctions.PortalSetup("SiteMenu" + MenuID, SepFunctions.Get_Portal_ID());
                    }
                    else
                    {
                        sReturn = SepFunctions.LangText("Site Menu ~~" + MenuID + "~~");
                    }
                }
            }
            else
            {
                sReturn = SepFunctions.LangText("Invalid MenuID");
            }

            output.Append(sReturn);

            return output.ToString();
        }
    }
}