// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SiteTemplate.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System.IO;
    using System.Xml;

    /// <summary>
    /// A site template.
    /// </summary>
    public static class SiteTemplate
    {
        /// <summary>
        /// Gets a variable.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The variable.</returns>
        public static string getVariable(string id)
        {
            try
            {
                var sFolderName = SepFunctions.getTemplateFolder();
                var configXML = Load_ConfigXML(false);

                if (!string.IsNullOrWhiteSpace(configXML))
                {
                    var sInstallFolder = SepFunctions.GetInstallFolder(true);

                    var doc = new XmlDocument();
                    doc.Load(configXML);

                    // Select the book node with the matching attribute value.
                    var root = doc.DocumentElement;

                    if (root.SelectSingleNode("/root/CustomVariables/Variable[@name='" + id + "']/Value") != null)
                    {
                        switch (id)
                        {
                            case "HeaderImg":

                                return sInstallFolder + "skins/" + sFolderName + "/images/" + root.SelectSingleNode("/root/CustomVariables/Variable[@name='" + id + "']/Value").InnerText;

                            default:

                                return root.SelectSingleNode("/root/CustomVariables/Variable[@name='" + id + "']/Value").InnerText;
                        }
                    }

                    return string.Empty;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Query if 'id' is visible.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if visible, false if not.</returns>
        public static bool isVisible(string id)
        {
            try
            {
                var isUserPage = SepFunctions.isUserPage();

                var configXML = Load_ConfigXML(true);

                if (!string.IsNullOrWhiteSpace(configXML))
                {
                    var doc = new XmlDocument();
                    doc.Load(configXML);

                    // Select the book node with the matching attribute value.
                    var root = doc.DocumentElement;

                    if (isUserPage)
                    {
                        if (id == "Banners" || id == "Sponsors")
                        {
                            id = "Advertisements";
                        }

                        if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id='" + id + "']") != null)
                        {
                            if (root.SelectSingleNode("/root/UPFeatureList/UPFeature[@id='" + id + "']").InnerText == "1")
                            {
                                return true;
                            }

                            return false;
                        }

                        return false;
                    }

                    if (root.SelectSingleNode("/root/FeatureList/Feature[@id='" + id + "']") != null)
                    {
                        if (root.SelectSingleNode("/root/FeatureList/Feature[@id='" + id + "']").InnerText == "1")
                        {
                            return true;
                        }

                        return false;
                    }

                    return false;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads configuration XML.
        /// </summary>
        /// <param name="excludeUserPage">True to exclude, false to include the user page.</param>
        /// <returns>The configuration XML.</returns>
        public static string Load_ConfigXML(bool excludeUserPage)
        {
            try
            {
                var sFolderName = SepFunctions.getTemplateFolder();
                var sConfigFile = SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config.xml";

                if (SepFunctions.Get_Portal_ID() > 0)
                {
                    if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config-" + SepFunctions.Get_Portal_ID() + ".xml"))
                    {
                        sConfigFile = SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config-" + SepFunctions.Get_Portal_ID() + ".xml";
                    }
                }

                if (SepFunctions.isUserPage() && excludeUserPage == false)
                {
                    var sUserID = SepFunctions.GetUserID(SepCore.Request.Item("UserName"));
                    if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config-" + SepFunctions.CleanFileName(sUserID) + ".xml"))
                    {
                        sConfigFile = SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config-" + SepFunctions.CleanFileName(sUserID) + ".xml";
                    }
                }

                if (File.Exists(sConfigFile))
                {
                    return sConfigFile;
                }

                if (File.Exists(SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config_default.xml"))
                {
                    return SepCore.HostingEnvironment.MapPath("~/skins\\") + sFolderName + "\\config_default.xml";
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}