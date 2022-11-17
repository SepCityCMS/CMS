// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_my_jobs.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class careers_my_jobs.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_my_jobs : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The c PCR
        /// </summary>
        private readonly PCRecruiter cPCR = new PCRecruiter();

        /// <summary>
        /// The w reader
        /// </summary>
        private StreamReader WReader;

        /// <summary>
        /// The w request
        /// </summary>
        private HttpWebRequest WRequest;

        /// <summary>
        /// The w response
        /// </summary>
        private HttpWebResponse WResponse;

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="StartPage">The start page.</param>
        public void BindData(long StartPage)
        {
            var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, "JobSearch", "searchDefault");
            var getFields = string.Empty;
            var arrCount = 0;

            foreach (var item in arrData)
            {
                if (arrCount > 0) getFields += ",";
                getFields += Strings.ToString(item);
                arrCount += 1;
            }

            var sessionId = cPCR.GetSessionId();
            var sQuery = "OnJobBoard=External&Fields=CompanyId,JobId," + getFields + "&Page=" + (StartPage + 1) + "&ResultsPerPage=" + SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage")) + "&query=CompanyId eq " + SepFunctions.GetUserInformation("PCRCompanyId");

            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions?" + sQuery);
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            WRequest.Method = "GET";
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";

            WResponse = (HttpWebResponse)WRequest.GetResponse();
            WReader = new StreamReader(WResponse.GetResponseStream());
            var jsonString = WReader.ReadToEnd();

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;

                        ListContent.Columns.Clear();

                        foreach (var item in arrData)
                            try
                            {
                                var tempField = new TemplateField
                                {
                                    ItemTemplate = new AddTemplateMyPositions(ListItemType.Item, Strings.ToString(item)),
                                    HeaderText = root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText
                                };
                                ListContent.Columns.Add(tempField);
                            }
                            catch
                            {
                            }
                    }
                }
            }

            var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobResults>(jsonString.Replace("null", "\"\""));
            ListContent.DataSource = pcrResults.Results.ToList();
            ListContent.DataBind();

            var pcrTotalResults = JsonConvert.DeserializeObject<PCRecruiterTotalRecords>(jsonString);

            ListContent.VirtualItemCount = pcrTotalResults.TotalRecords;

            if (ListContent.Rows.Count == 0) ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("You currently do not have any jobs listed.") + "</div>";
        }

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
        /// Formats the ISAPI.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public string Format_ISAPI(object sText)
        {
            return SepFunctions.Format_ISAPI(Strings.ToString(sText));
        }

        /// <summary>
        /// Formats the type of the job.
        /// </summary>
        /// <param name="JobType">Type of the job.</param>
        /// <returns>System.String.</returns>
        public string FormatJobType(string JobType)
        {
            switch (JobType)
            {
                case "CFT":
                    return SepFunctions.LangText("Contractor");

                case "PTP":
                    return SepFunctions.LangText("Part Time");

                default:
                    return SepFunctions.LangText("Full Time");
            }
        }

        /// <summary>
        /// Formats the location.
        /// </summary>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <returns>System.String.</returns>
        public string FormatLocation(string City, string State, string PostalCode)
        {
            var sReturn = City;

            if (!string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(State))
                sReturn += ", ";
            sReturn += State;
            sReturn += " " + PostalCode;

            return sReturn;
        }

        /// <summary>
        /// Gets the install folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetInstallFolder()
        {
            return SepFunctions.GetInstallFolder();
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                }
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
                if (cPCR != null)
                {
                    cPCR.Dispose();
                }

                if (WReader != null)
                {
                    WReader.Dispose();
                }

                if (WResponse != null)
                {
                    WResponse.Dispose();
                }
            }
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the ListContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs" /> instance containing the event data.</param>
        protected void ListContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListContent.PageIndex = e.NewPageIndex;
            BindData(ListContent.PageIndex);
        }

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
            var sInstallFolder = SepFunctions.GetInstallFolder();

            TranslatePage();

            GlobalVars.ModuleID = 66;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PCREnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PCREmployer"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            cPCR.Members2PCR();

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PCRCompanyId"))) BindData(0);
                else ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must add your company before you can manage your positions.") + "</div>";
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }

    /// <summary>
    /// Class AddTemplateMyPositions.
    /// Implements the <see cref="System.Web.UI.ITemplate" />
    /// </summary>
    /// <seealso cref="System.Web.UI.ITemplate" />
    public class AddTemplateMyPositions : ITemplate // -V3072
    {
        // Types that own disposable fields should be disposable
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The col name
        /// </summary>
        private readonly string _colName;

        /// <summary>
        /// The type
        /// </summary>
        private readonly ListItemType _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTemplateMyPositions" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="colname">The colname.</param>
        public AddTemplateMyPositions(ListItemType type, string colname)
        {
            _type = type;

            _colName = colname;
        }

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
        }

        /// <summary>
        /// When implemented by a class, defines the <see cref="System.Web.UI.Control" /> object that child controls and templates belong to. These child controls are in turn defined within an inline template.
        /// </summary>
        /// <param name="container">The <see cref="System.Web.UI.Control" /> object to contain the instances of controls from the inline template.</param>
        void ITemplate.InstantiateIn(Control container)
        {
            // Interface methods should be callable by child types
            try
            {
                switch (_type)
                {
                    case ListItemType.Item:
                        switch (_colName)
                        {
                            case "JobTitle":
                                var jobTitleLink = new HyperLink();
                                jobTitleLink.DataBinding += ht_DataBinding;
                                container.Controls.Add(jobTitleLink);
                                break;

                            case "CompanyName":
                                var CompanyLink = new HyperLink();
                                CompanyLink.DataBinding += ht_DataBinding;
                                container.Controls.Add(CompanyLink);
                                break;

                            default:
                                var defaultLiteral = new Literal();
                                defaultLiteral.DataBinding += ht_DataBinding;
                                container.Controls.Add(defaultLiteral);
                                break;
                        }

                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// IDisposable.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Formats the currency field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void Format_Currency_Field(object sender)
        {
            try
            {
                var defaultLiteral = (Literal)sender;
                var defaultContainer = (GridViewRow)defaultLiteral.NamingContainer;
                var defaultValue = DataBinder.Eval(defaultContainer.DataItem, _colName);
                if (defaultValue != DBNull.Value) defaultLiteral.Text = Strings.ToString(SepFunctions.Format_Currency(defaultValue));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Formats the date field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void Format_Date_Field(object sender)
        {
            try
            {
                var defaultLiteral = (Literal)sender;
                var defaultContainer = (GridViewRow)defaultLiteral.NamingContainer;
                var defaultValue = DataBinder.Eval(defaultContainer.DataItem, _colName);
                if (defaultValue != DBNull.Value) defaultLiteral.Text = Strings.FormatDateTime(SepFunctions.toDate(Strings.ToString(defaultValue)), Strings.DateNamedFormat.ShortDate);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the DataBinding event of the ht control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ht_DataBinding(object sender, EventArgs e)
        {
            try
            {
                switch (_colName)
                {
                    case "JobTitle":
                        var jobTitleLink = (HyperLink)sender;
                        var jobTitleContainer = (GridViewRow)jobTitleLink.NamingContainer;
                        var jobTitleValue = DataBinder.Eval(jobTitleContainer.DataItem, _colName);
                        if (jobTitleValue != DBNull.Value)
                        {
                            jobTitleLink.Text = Strings.ToString(jobTitleValue);
                            jobTitleLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_job_modify.aspx?JobId=" + DataBinder.Eval(jobTitleContainer.DataItem, "JobId");
                        }

                        break;

                    case "CompanyName":
                        var CompanyLink = (HyperLink)sender;
                        var CompanyContainer = (GridViewRow)CompanyLink.NamingContainer;
                        var CompanyValue = DataBinder.Eval(CompanyContainer.DataItem, _colName);
                        if (CompanyValue != DBNull.Value)
                        {
                            CompanyLink.Text = Strings.ToString(CompanyValue);
                            CompanyLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_my_company.aspx";
                        }

                        break;

                    case "JobType":
                        var jobTypeLiteral = (Literal)sender;
                        var jobTypeContainer = (GridViewRow)jobTypeLiteral.NamingContainer;
                        var jobTypeValue = DataBinder.Eval(jobTypeContainer.DataItem, _colName);
                        if (jobTypeValue != DBNull.Value) jobTypeLiteral.Text = SepFunctions.formatJobType(Strings.ToString(jobTypeValue));
                        break;

                    case "MinSalary":
                        Format_Currency_Field(sender);
                        break;

                    case "MaxSalary":
                        Format_Currency_Field(sender);
                        break;

                    case "BillRate":
                        Format_Currency_Field(sender);
                        break;

                    case "PayRate":
                        Format_Currency_Field(sender);
                        break;

                    case "BeginDate":
                        Format_Date_Field(sender);
                        break;

                    case "EndDate":
                        Format_Date_Field(sender);
                        break;

                    case "DatePosted":
                        Format_Date_Field(sender);
                        break;

                    default:
                        var defaultLiteral = (Literal)sender;
                        var defaultContainer = (GridViewRow)defaultLiteral.NamingContainer;
                        var defaultValue = DataBinder.Eval(defaultContainer.DataItem, _colName);
                        if (defaultValue != DBNull.Value) defaultLiteral.Text = Strings.ToString(defaultValue);
                        break;
                }
            }
            catch
            {
            }
        }
    }
}