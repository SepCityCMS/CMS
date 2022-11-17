// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.spadmin
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Collections;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class careers.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers : Page
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
        /// Populates the fields.
        /// </summary>
        public void PopulateFields()
        {
            var sSettings = SepCommon.DAL.JobBoard.CustomizeGetSetting(Customize.Value);
            var hasSetting = false;

            selectedFields.InnerHtml = string.Empty;
            availableFields.InnerHtml = string.Empty;

            var cPCR = new PCRecruiter();
            if (!string.IsNullOrWhiteSpace(sSettings)) hasSetting = true;

            if (hasSetting)
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;
                        var xTag = string.Empty;

                        ArrayList arrData;
                        switch (Customize.Value)
                        {
                            case "ComDetail":
                                xTag = "CompanyFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, Customize.Value, "detailsDefault");
                                break;

                            case "ComPost":
                                xTag = "CompanyFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, Customize.Value, "postDefault");
                                break;

                            case "ComSearch":
                                xTag = "CompanyFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, Customize.Value, "searchDefault");
                                break;

                            case "JobDetail":
                                xTag = "PositionFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, Customize.Value, "detailsDefault");
                                break;

                            case "JobPost":
                                xTag = "PositionFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, Customize.Value, "postDefault");
                                break;

                            case "JobSearch":
                                xTag = "PositionFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, Customize.Value, "searchDefault");
                                break;

                            case "CanPost":
                                xTag = "CandidateFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Candidates, Customize.Value, "postDefault");
                                break;

                            case "CanSearch":
                                xTag = "CandidateFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Candidates, Customize.Value, "searchDefault");
                                break;

                            default:
                                xTag = "CandidateFields";
                                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Candidates, Customize.Value, "detailsDefault");
                                break;
                        }

                        foreach (var item in arrData) selectedFields.InnerHtml += "<li class=\"list-group-item\" data-value=\"" + item + "\">" + root.SelectSingleNode("/pcrfields/" + xTag + "/ field[@name='" + item + "']").InnerText + "</li>";
                    }
                }
            }
            else
            {
                XmlNodeList selectedNodes;
                switch (Customize.Value)
                {
                    case "ComDetail":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "detailsDefault", "Y", false);
                        break;

                    case "ComPost":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "postDefault", "Y", false);
                        break;

                    case "ComSearch":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "searchDefault", "Y", false);
                        break;

                    case "JobDetail":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "detailsDefault", "Y", false);
                        break;

                    case "JobPost":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "postDefault", "Y", false);
                        break;

                    case "JobSearch":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "searchDefault", "Y", false);
                        break;

                    case "CanPost":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "postDefault", "Y", false);
                        break;

                    case "CanSearch":
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "searchDefault", "Y", false);
                        break;

                    default:
                        selectedNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "detailsDefault", "Y", false);
                        break;
                }

                foreach (XmlNode node in selectedNodes)
                    if (Strings.InStr(sSettings, node.Attributes["name"].InnerText) > 0 || hasSetting == false) // -V3063 //-V3022
                        selectedFields.InnerHtml += "<li class=\"list-group-item\" data-value=\"" + node.Attributes["name"].InnerText + "\">" + node.InnerText + "</li>";
            }

            XmlNodeList availableNodes;
            switch (SepCommon.SepCore.Request.Item("DoAction"))
            {
                case "ComDetail":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "detailsDefault", "N", hasSetting);
                    break;

                case "ComPost":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "postDefault", "N", hasSetting);
                    break;

                case "ComSearch":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Companies, "searchDefault", "N", hasSetting);
                    break;

                case "JobDetail":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "detailsDefault", "N", hasSetting);
                    break;

                case "JobPost":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "postDefault", "N", hasSetting);
                    break;

                case "JobSearch":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Positions, "searchDefault", "N", hasSetting);
                    break;

                case "CanPost":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "postDefault", "N", hasSetting);
                    break;

                case "CanSearch":
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "searchDefault", "N", hasSetting);
                    break;

                default:
                    availableNodes = cPCR.GetFieldNodes(PCRecruiter.PCR_RecordType.Candidates, "detailsDefault", "N", hasSetting);
                    break;
            }

            foreach (XmlNode node in availableNodes)
                if (Strings.InStr(sSettings, node.Attributes["name"].InnerText) == 0 || hasSetting == false)
                    availableFields.InnerHtml += "<li class=\"list-group-item\" data-value=\"" + node.Attributes["name"].InnerText + "\">" + node.InnerText + "</li>";
            cPCR.Dispose();
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
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

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("PCRAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PCRAdmin"), true) == false)
            {
                UpdatePanel.Visible = false;
                var idErrorMsg = (Literal)Master.FindControl("idPublicErrorMsg");
                idErrorMsg.Visible = true;
                idErrorMsg.Text = "<div align=\"center\" style=\"margin-top:50px\">";
                idErrorMsg.Text += "<h1>" + SepFunctions.LangText("Oops! Access denied...") + "</h1><br/>";
                idErrorMsg.Text += SepFunctions.LangText("You do not have access to this page.") + "<br/><br/>";
                idErrorMsg.Text += "</div>";
                return;
            }

            if (!Page.IsPostBack)
            {
                Customize.Value = SepCommon.SepCore.Request.Item("DoAction");

                if (string.IsNullOrWhiteSpace(Customize.Value)) Customize.Value = "CanDetail";

                switch (Customize.Value)
                {
                    case "ComDetail":
                        PageHeader.InnerHtml = SepFunctions.LangText("Company Detail Fields");
                        break;

                    case "ComPost":
                        PageHeader.InnerHtml = SepFunctions.LangText("Company Posting Fields");
                        break;

                    case "ComSearch":
                        PageHeader.InnerHtml = SepFunctions.LangText("Company Search Result Fields");
                        break;

                    case "JobDetail":
                        PageHeader.InnerHtml = SepFunctions.LangText("Job Detail Fields");
                        break;

                    case "JobPost":
                        PageHeader.InnerHtml = SepFunctions.LangText("Job Posting Fields");
                        break;

                    case "JobSearch":
                        PageHeader.InnerHtml = SepFunctions.LangText("Job Search Result Fields");
                        break;

                    case "CanPost":
                        PageHeader.InnerHtml = SepFunctions.LangText("Candidate Posting Fields");
                        break;

                    case "CanSearch":
                        PageHeader.InnerHtml = SepFunctions.LangText("Candidate Search Result Fields");
                        break;

                    default:
                        PageHeader.InnerHtml = SepFunctions.LangText("Candidate Detail Fields");
                        break;
                }

                PopulateFields();
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SepCommon.DAL.JobBoard.CustomizeSave(Customize.Value, SelectedFIeldValues.Value);

            PopulateFields();

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Changes have been successfully saved.") + "</div>";
        }
    }
}