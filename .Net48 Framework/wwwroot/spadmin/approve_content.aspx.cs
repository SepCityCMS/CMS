// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="approve_content.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using SepControls;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class approve_content.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class approve_content : Page
    {
        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang))) ModifyLegend.InnerHtml = SepFunctions.LangText(string.Empty);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            TranslatePage();

            GlobalVars.ModuleID = 983;

            var str = new StringBuilder();

            var inidata = string.Empty;

            var sTitle = string.Empty;

            var aa = 0;
            string[] arrFieldLabel = null;
            string[] arrFieldName = null;
            string[] arrFieldType = null;

            var intModuleID = SepFunctions.toInt(SepCommon.SepCore.Request.Item("ModuleID"));

            switch (intModuleID)
            {
                case 10:
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve File Library Download");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Category");
                                    arrFieldName[aa] = "CATID";
                                    arrFieldType[aa] = "CatID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    switch (SepFunctions.ParseXML("CATTYPE", inidata))
                                    {
                                        case "Audio":
                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Song Title");
                                            arrFieldName[aa] = "FIELD1";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Album");
                                            arrFieldName[aa] = "FIELD2";
                                            arrFieldType[aa] = "Text";
                                            break;

                                        case "Document":
                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Document Title");
                                            arrFieldName[aa] = "FIELD1";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Description");
                                            arrFieldName[aa] = "FIELD3";
                                            arrFieldType[aa] = "Textarea";
                                            break;

                                        case "Video":
                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Video Title");
                                            arrFieldName[aa] = "FIELD1";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Description");
                                            arrFieldName[aa] = "FIELD3";
                                            arrFieldType[aa] = "Textarea";
                                            break;

                                        case "Image":
                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Caption");
                                            arrFieldName[aa] = "FIELD1";
                                            arrFieldType[aa] = "Text";
                                            break;

                                        default:
                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Program Name");
                                            arrFieldName[aa] = "FIELD1";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Version");
                                            arrFieldName[aa] = "FIELD2";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Price");
                                            arrFieldName[aa] = "FIELD4";
                                            arrFieldType[aa] = "Text";

                                            aa += 1;
                                            Array.Resize(ref arrFieldLabel, aa + 1);
                                            Array.Resize(ref arrFieldName, aa + 1);
                                            Array.Resize(ref arrFieldType, aa + 1);
                                            arrFieldLabel[aa] = SepFunctions.LangText("Description");
                                            arrFieldName[aa] = "FIELD3";
                                            arrFieldType[aa] = "Textarea";
                                            break;
                                    }

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("File Name");
                                    arrFieldName[aa] = "FILENAME";
                                    arrFieldType[aa] = "File";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Downloads");
                                    arrFieldName[aa] = "DOWNLOADS";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";
                                }
                            }
                        }
                    }

                    break;

                case 18:

                    // MatchMaker
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Profile");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("About Me");
                                    arrFieldName[aa] = "ABOUTME";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("About My Match");
                                    arrFieldName[aa] = "ABOUTMYMATCH";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";
                                }
                            }
                        }
                    }

                    break;

                case 19:

                    // Link Directory
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Website");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Category");
                                    arrFieldName[aa] = "CATID";
                                    arrFieldType[aa] = "CatID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Site Title");
                                    arrFieldName[aa] = "LINKNAME";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Link URL");
                                    arrFieldName[aa] = "LINKURL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Description");
                                    arrFieldName[aa] = "DESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";
                                }
                            }
                        }
                    }

                    break;

                case 20:
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Business");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Category");
                                    arrFieldName[aa] = "CATID";
                                    arrFieldType[aa] = "CatID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Business Name");
                                    arrFieldName[aa] = "BUSINESSNAME";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Contact Email");
                                    arrFieldName[aa] = "CONTACTEMAIL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Phone Number");
                                    arrFieldName[aa] = "PHONENUMBER";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Site URL");
                                    arrFieldName[aa] = "SITEURL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Description");
                                    arrFieldName[aa] = "DESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Address");
                                    arrFieldName[aa] = "ADDRESS";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("City/Town");
                                    arrFieldName[aa] = "CITY";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("State/Province");
                                    arrFieldName[aa] = "STATE";
                                    arrFieldType[aa] = "State";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Zip Code");
                                    arrFieldName[aa] = "ZIPCODE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Country");
                                    arrFieldName[aa] = "COUNTRY";
                                    arrFieldType[aa] = "Country";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Full Description");
                                    arrFieldName[aa] = "FULLDESCRIPTION";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Album ID");
                                    arrFieldName[aa] = "ALBUMID";
                                    arrFieldType[aa] = "AlbumID";
                                }
                            }
                        }
                    }

                    break;

                case 35:
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Article");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Category");
                                    arrFieldName[aa] = "CATID";
                                    arrFieldType[aa] = "CatID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Headline");
                                    arrFieldName[aa] = "HEADLINE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Headline Date");
                                    arrFieldName[aa] = "HEADLINE_DATE";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Author");
                                    arrFieldName[aa] = "AUTHOR";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Summary");
                                    arrFieldName[aa] = "SUMMARY";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Source");
                                    arrFieldName[aa] = "SOURCE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Source URL");
                                    arrFieldName[aa] = "ARTICLE_URL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Full Article");
                                    arrFieldName[aa] = "FULL_ARTICLE";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Meta Keywords");
                                    arrFieldName[aa] = "META_KEYWORDS";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Meta Description");
                                    arrFieldName[aa] = "META_DESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";
                                }
                            }
                        }
                    }

                    break;

                case 63:

                    // User Profiles
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Profile");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("About Me");
                                    arrFieldName[aa] = "ABOUTME";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("About My Match");
                                    arrFieldName[aa] = "ABOUTMYMATCH";
                                    arrFieldType[aa] = "HTML";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";
                                }
                            }
                        }
                    }

                    break;

                case 65:

                    // Vouchers
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Voucher");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Category");
                                    arrFieldName[aa] = "CATID";
                                    arrFieldType[aa] = "CatID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Buy Title");
                                    arrFieldName[aa] = "BUYTITLE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Short Description");
                                    arrFieldName[aa] = "SHORTDESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Long Description");
                                    arrFieldName[aa] = "LONGDESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Sale Price");
                                    arrFieldName[aa] = "SALEPRICE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Original Value");
                                    arrFieldName[aa] = "REGULARPRICE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Quantity");
                                    arrFieldName[aa] = "QUANTITY";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Redemption Start");
                                    arrFieldName[aa] = "REDEMPTIONSTART";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Redemption End");
                                    arrFieldName[aa] = "REDEMPTIONEND";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Purchase Code");
                                    arrFieldName[aa] = "PURCHASECODE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Business Name");
                                    arrFieldName[aa] = "BUSINESSNAME";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Street Address");
                                    arrFieldName[aa] = "ADDRESS";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("City");
                                    arrFieldName[aa] = "CITY";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("State");
                                    arrFieldName[aa] = "STATE";
                                    arrFieldType[aa] = "State";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Zip/Postal Code");
                                    arrFieldName[aa] = "ZIPCODE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Country");
                                    arrFieldName[aa] = "COUNTRY";
                                    arrFieldType[aa] = "Country";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Contact Email");
                                    arrFieldName[aa] = "CONTACTEMAIL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Contact Name");
                                    arrFieldName[aa] = "CONTACTNAME";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Phone Number");
                                    arrFieldName[aa] = "PHONENUMBER";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Days to Run");
                                    arrFieldName[aa] = "DAYSTOEXPIRE";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Fine Print");
                                    arrFieldName[aa] = "FINEPRINT";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Auto Responder to Merchant");
                                    arrFieldName[aa] = "SENDEMAILTEMP";
                                    arrFieldType[aa] = "EmailID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Auto Responder to Purchaser");
                                    arrFieldName[aa] = "SENDEMAILTEMPORDER";
                                    arrFieldType[aa] = "EmailID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Auto Responder to Admin When Purchase is Made");
                                    arrFieldName[aa] = "SENDEMAILTEMPADMIN";
                                    arrFieldType[aa] = "EmailID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Twitter HTML");
                                    arrFieldName[aa] = "TWITTERHTML";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("About Power Buy URL");
                                    arrFieldName[aa] = "CATEGORYURL";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Date Posted");
                                    arrFieldName[aa] = "DATEPOSTED";
                                    arrFieldType[aa] = "Date";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Portal");
                                    arrFieldName[aa] = "PORTALID";
                                    arrFieldType[aa] = "PortalID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Username");
                                    arrFieldName[aa] = "USERID";
                                    arrFieldType[aa] = "UserID";
                                }
                            }
                        }
                    }

                    break;

                case 999:
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM ApprovalXML WHERE XMLID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("XMLID")) + "'", conn))
                        {
                            using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                            {
                                if (ApproveRS.HasRows)
                                {
                                    ApproveRS.Read();
                                    inidata = SepFunctions.openNull(ApproveRS["XMLData"]);
                                    sTitle = SepFunctions.LangText("Approve Web Page");

                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Link Text");
                                    arrFieldName[aa] = "LINKTEXT";
                                    arrFieldType[aa] = "Text";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Menu");
                                    arrFieldName[aa] = "MENUID";
                                    arrFieldType[aa] = "MenuID";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Enable");
                                    arrFieldName[aa] = "ENABLE";
                                    arrFieldType[aa] = "Enable";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Meta Description");
                                    arrFieldName[aa] = "DESCRIPTION";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Meta Keywords");
                                    arrFieldName[aa] = "KEYWORDS";
                                    arrFieldType[aa] = "Textarea";

                                    aa += 1;
                                    Array.Resize(ref arrFieldLabel, aa + 1);
                                    Array.Resize(ref arrFieldName, aa + 1);
                                    Array.Resize(ref arrFieldType, aa + 1);
                                    arrFieldLabel[aa] = SepFunctions.LangText("Page Text");
                                    arrFieldName[aa] = "PAGETEXT";
                                    arrFieldType[aa] = "HTML";
                                }
                            }
                        }
                    }

                    break;
            }

            if (arrFieldLabel == null)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.SendGenericError(400) + "</div>";
                return;
            }

            ModifyLegend.InnerHtml = sTitle;
            ModuleUniqueID.Value = SepFunctions.ParseXML("UNIQUEID", inidata);
            ModuleID.Value = Strings.ToString(intModuleID);

            for (var i = 0; i <= Information.UBound(arrFieldLabel); i++)
            {
                if (arrFieldType[i] != "CatID" && arrFieldType[i] != "HTML" && arrFieldType[i] != "AlbumID")
                    using (var sLabel = new HtmlGenericControl("label"))
                    {
                        sLabel.InnerText = arrFieldLabel[i];
                        FormShow.Controls.Add(sLabel);
                    }

                switch (arrFieldType[i])
                {
                    case "CatID":
                        long GetBusinessMaxCat = Convert.ToInt32(SepFunctions.toDouble(SepFunctions.Setup(20, "BusinessMaxCat")));
                        if (GetBusinessMaxCat == 0)
                            GetBusinessMaxCat = 1;

                        if (intModuleID == 20)
                        {
                            for (var j = 1; j <= GetBusinessMaxCat; j++)
                            {
                                var sCatDropdown = new CategoryDropdown
                                {
                                    ID = arrFieldName[i],
                                    CatID = SepFunctions.ParseXML("CATID" + j, inidata),
                                    ModuleID = intModuleID
                                };
                                FormShow.Controls.Add(sCatDropdown);
                            }
                        }
                        else
                        {
                            var sCatDropdown = new CategoryDropdown
                            {
                                ID = arrFieldName[i],
                                CatID = SepFunctions.ParseXML(arrFieldName[i], inidata),
                                ModuleID = intModuleID
                            };
                            FormShow.Controls.Add(sCatDropdown);
                        }

                        break;

                    case "PortalID":
                        var sPortalDropdown = new PortalDropdown
                        {
                            ID = arrFieldName[i],
                            Text = SepFunctions.ParseXML(arrFieldName[i], inidata)
                        };
                        FormShow.Controls.Add(sPortalDropdown);
                        break;

                    case "Textarea":
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<textarea id=\"" + arrFieldName[i] + "\" name=\"" + arrFieldName[i] + "\" class=\"form-control\">" + SepFunctions.ParseXML(arrFieldName[i], inidata) + "</textarea>";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "File":
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<a href=\"" + SepFunctions.GetDirValue("LibraryDir", true) + SepFunctions.ParseXML(arrFieldName[i], inidata) + "\" target=\"_blank\">" + SepFunctions.ParseXML(arrFieldName[i], inidata) + "</a>";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "Image":
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<a href=\"" + SepFunctions.GetDirValue("ImageDir", true) + SepFunctions.ParseXML(arrFieldName[i], inidata) + "\" target=\"_blank\">" + SepFunctions.ParseXML(arrFieldName[i], inidata) + "</a>";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "Date":
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<input type=\"text\" id=\"" + arrFieldName[i] + "\" name=\"" + arrFieldName[i] + "\" class=\"form-control\" value=\"" + SepFunctions.ParseXML(arrFieldName[i], inidata) + "\" />";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "HTML":
                        using (var sWYSIWYGEditor = new WYSIWYGEditor())
                        {
                            sWYSIWYGEditor.ID = arrFieldName[i];
                            sWYSIWYGEditor.Text = SepFunctions.ParseXML(arrFieldName[i], inidata);
                            FormShow.Controls.Add(sWYSIWYGEditor);
                        }

                        break;

                    case "State":
                        using (var cState = new StateDropdown())
                        {
                            cState.ID = arrFieldName[i];
                            FormShow.Controls.Add(cState);
                        }

                        break;

                    case "Country":
                        using (var cCountry = new CountryDropdown())
                        {
                            cCountry.ID = arrFieldName[i];
                            FormShow.Controls.Add(cCountry);
                        }

                        break;

                    case "EmailID":
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName FROM EmailTemplates ORDER BY EmailSubject", conn))
                            {
                                using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                                {
                                    str.Clear();
                                    str.Append("<select name=\"" + arrFieldName[i] + "\" style=\"width:200px\">");
                                    str.Append("<option value=\"\">" + SepFunctions.LangText("None") + "</option>");
                                    while (ApproveRS.Read())
                                    {
                                        str.Append("<option value=\"" + SepFunctions.openNull(ApproveRS["TemplateID"]) + "\"" + Strings.ToString(SepFunctions.ParseXML(arrFieldName[i], inidata) == SepFunctions.openNull(ApproveRS["TemplateID"]) ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(ApproveRS["TemplateName"]) + "</option>");
                                    }
                                    str.Append("</select>");
                                    using (var sSpan = new HtmlGenericControl("span"))
                                    {
                                        sSpan.InnerHtml = Strings.ToString(str);
                                        FormShow.Controls.Add(sSpan);
                                    }
                                }
                            }
                        }

                        break;

                    case "AlbumID":
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT AlbumID,AlbumName FROM PhotoAlbums WHERE UserID='" + SepFunctions.FixWord(SepFunctions.ParseXML("USERID", inidata)) + "'", conn))
                            {
                                str.Clear();
                                using (SqlDataReader ApproveRS = cmd.ExecuteReader())
                                {
                                    if (!ApproveRS.HasRows)
                                    {
                                        str.Append("<input type=\"hidden\" name=\"AlbumID\" value=\"\" />");
                                    }
                                    else
                                    {
                                        str.Append("<label>Additional Photo's</label>");
                                        str.Append("<select name=\"AlbumID\" class=\"Dropdown\">");
                                        str.Append("<option value=\"0\">" + SepFunctions.LangText("--- Select a Photo Album ---") + "</option>");
                                        while (ApproveRS.Read())
                                        {
                                            str.Append("<option value=\"" + SepFunctions.openNull(ApproveRS["AlbumID"]) + "\"" + Strings.ToString(Strings.ToString(SepFunctions.ParseXML(arrFieldName[i], inidata)) == SepFunctions.openNull(ApproveRS["AlbumID"]) ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.openNull(ApproveRS["AlbumName"]) + "</option>");
                                        }
                                        str.Append("</select>");
                                    }
                                }
                                using (var sSpan = new HtmlGenericControl("span"))
                                {
                                    sSpan.InnerHtml = Strings.ToString(str);
                                    FormShow.Controls.Add(sSpan);
                                }
                            }
                        }

                        break;

                    case "MenuID":
                        str.Clear();
                        var GetMenuID = SepFunctions.toLong(SepFunctions.ParseXML(arrFieldName[i], inidata));
                        str.Append("<select name=\"" + arrFieldName[i] + "\" class=\"Dropdown\">");
                        str.Append("<option value=\"1\"" + Strings.ToString(GetMenuID == 1 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu1Text") + "</option>");
                        str.Append("<option value=\"2\"" + Strings.ToString(GetMenuID == 2 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu2Text") + "</option>");
                        str.Append("<option value=\"3\"" + Strings.ToString(GetMenuID == 3 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu3Text") + "</option>");
                        str.Append("<option value=\"4\"" + Strings.ToString(GetMenuID == 4 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu4Text") + "</option>");
                        str.Append("<option value=\"5\"" + Strings.ToString(GetMenuID == 5 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu5Text") + "</option>");
                        str.Append("<option value=\"6\"" + Strings.ToString(GetMenuID == 6 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu6Text") + "</option>");
                        str.Append("<option value=\"7\"" + Strings.ToString(GetMenuID == 7 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.Setup(993, "Menu7Text") + "</option>");
                        str.Append("<option value=\"8\"" + Strings.ToString(GetMenuID == 8 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Top Menu") + "</option>");
                        str.Append("<option value=\"9\"" + Strings.ToString(GetMenuID == 9 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Account Info Main Menu") + "</option>");
                        str.Append("<option value=\"0\"" + Strings.ToString(GetMenuID == 0 ? " selected=\"selected\"" : string.Empty) + ">" + SepFunctions.LangText("Not on a Menu") + "</option>");
                        str.Append("</select>");
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = Strings.ToString(str);
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "Enable":
                        str.Clear();
                        if (SepFunctions.ParseXML(arrFieldName[i], inidata) == "Enabled" || SepFunctions.toLong(SepFunctions.ParseXML(arrFieldName[i], inidata)) == 1)
                        {
                            str.Append("<select name=\"" + arrFieldName[i] + "\" id=\"" + arrFieldName[i] + "\">");
                            str.Append("<option value=\"yes\" selected=\"selected\">" + SepFunctions.LangText("Yes") + "</option>");
                            str.Append("<option value=\"no\">" + SepFunctions.LangText("No") + "</option>");
                            str.Append("</select>");
                        }
                        else
                        {
                            str.Append("<select name=\"" + arrFieldName[i] + "\" id=\"" + arrFieldName[i] + "\">");
                            str.Append("<option value=\"yes\">" + SepFunctions.LangText("Yes") + "</option>");
                            str.Append("<option value=\"no\" selected=\"selected\">" + SepFunctions.LangText("No") + "</option>");
                            str.Append("</select>");
                        }

                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = Strings.ToString(str);
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    case "AccessKeys":
                        using (var cKeys = new AccessKeyDropdown())
                        {
                            cKeys.ID = arrFieldName[i];
                            FormShow.Controls.Add(cKeys);
                        }

                        break;

                    case "UserID":
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<input type=\"text\" id=\"" + arrFieldName[i] + "\" name=\"" + arrFieldName[i] + "\" class=\"form-control\" value=\"" + SepFunctions.GetUserInformation("UserName", SepFunctions.ParseXML(arrFieldName[i], inidata)) + "\" />";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;

                    default:
                        using (var sSpan = new HtmlGenericControl("span"))
                        {
                            sSpan.InnerHtml = "<input type=\"text\" id=\"" + arrFieldName[i] + "\" name=\"" + arrFieldName[i] + "\" class=\"form-control\" value=\"" + SepFunctions.ParseXML(arrFieldName[i], inidata) + "\" />";
                            FormShow.Controls.Add(sSpan);
                        }

                        break;
                }

                using (var sDiv = new HtmlGenericControl("span"))
                {
                    sDiv.InnerHtml = "<div></div>";
                    FormShow.Controls.Add(sDiv);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var intModuleID = SepFunctions.toInt(ModuleID.Value);

            SepFunctions.Approval_Chain_Check(intModuleID, ModuleUniqueID.Value, false);

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Content has been successfully approved.") + "</div>";
            ModFormDiv.Visible = false;
        }
    }
}