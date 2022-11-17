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

namespace wwwroot
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.Models;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class careers.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The c PCR
        /// </summary>
        private readonly PCRecruiter cPCR = new PCRecruiter();

        /// <summary>
        /// The page query
        /// </summary>
        private string pageQuery = string.Empty;

        /// <summary>
        /// The s company name
        /// </summary>
        private string sCompanyName = string.Empty;

        /// <summary>
        /// The s employment type
        /// </summary>
        private string sEmploymentType = string.Empty;

        /// <summary>
        /// The s job category
        /// </summary>
        private string sJobCategory = string.Empty;

        /// <summary>
        /// The s job title
        /// </summary>
        private string sJobTitle = string.Empty;

        /// <summary>
        /// The s keywords
        /// </summary>
        private string sKeywords = string.Empty;

        /// <summary>
        /// The s location
        /// </summary>
        private string sLocation = string.Empty;

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
            ErrorMessage.InnerHtml = string.Empty;
            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            var wClause = string.Empty;
            var noResults = false;

            if (!string.IsNullOrWhiteSpace(sLocation))
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    var rx = new Regex("^(.+)[,\\\\s]+(.+?)\\s*(\\d{5})?$", RegexOptions.Compiled);
                    var m = rx.Match(sLocation);
                    if (!string.IsNullOrWhiteSpace(m.Groups[1].Value))
                    {
                        if (!string.IsNullOrWhiteSpace(wClause)) // -V3022
                            wClause += " AND ";
                        wClause += "City eq " + Strings.Trim(m.Groups[1].Value);
                    }

                    if (!string.IsNullOrWhiteSpace(m.Groups[2].Value))
                    {
                        if (!string.IsNullOrWhiteSpace(wClause))
                            wClause += " AND ";
                        wClause += "State eq " + Strings.Trim(m.Groups[2].Value);
                    }

                    if (string.IsNullOrWhiteSpace(wClause))
                        noResults = true;
                }

            if (noResults == false)
            {
                if (!string.IsNullOrWhiteSpace(sJobTitle))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "JobTitle co " + Strings.Trim(sJobTitle);
                }

                if (!string.IsNullOrWhiteSpace(sCompanyName))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "CompanyName co " + Strings.Trim(sCompanyName);
                }

                if (!string.IsNullOrWhiteSpace(sKeywords))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "Keywords co " + Strings.Trim(sKeywords);
                }

                if (!string.IsNullOrWhiteSpace(sJobCategory))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "Keywords co " + Strings.Trim(sJobCategory);
                }

                if (!string.IsNullOrWhiteSpace(sEmploymentType))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "JobType eq " + sEmploymentType;
                }

                if (!string.IsNullOrWhiteSpace(wClause))
                    wClause += " AND ";
                wClause += "EndDate ge " + DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss") + ".840";

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
                var sQuery = "Fields=CompanyId,JobId," + getFields + "&query=" + SepFunctions.UrlEncode(wClause);

                try
                {
                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions?" + sQuery + "&ResultsPerPage=" + SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage")) + "&Page=" + (StartPage + 1) + "&OnJobBoard eq External&Order=" + SepFunctions.UrlEncode("Feature Listing DESC,DatePosted DESC"));
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

                                ManageGridView.Columns.Clear();

                                foreach (var item in arrData)
                                    try
                                    {
                                        var tempField = new TemplateField
                                        {
                                            ItemTemplate = new AddTemplatePositions(ListItemType.Item, Strings.ToString(item)),
                                            HeaderText = root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText
                                        };
                                        ManageGridView.Columns.Add(tempField);
                                    }
                                    catch
                                    {
                                    }
                            }
                        }
                    }

                    var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobResults>(jsonString);
                    ManageGridView.DataSource = pcrResults.Results.ToList();
                    ManageGridView.DataBind();

                    var pcrTotalResults = JsonConvert.DeserializeObject<PCRecruiterTotalRecords>(jsonString);

                    ManageGridView.VirtualItemCount = pcrTotalResults.TotalRecords;

                    if (ManageGridView.Rows.Count == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No results found in your search.") + "</div>";
                        ManageGridView.Visible = false;
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = string.Empty;
                    }
                }
                catch
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error connecting to the PCRecruiter API.") + "</div>";
                }
            }
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
        /// Formats the date.
        /// </summary>
        /// <param name="sDate">The s date.</param>
        /// <returns>System.Object.</returns>
        public object FormatDate(string sDate)
        {
            try
            {
                return Strings.FormatDateTime(SepFunctions.toDate(sDate), Strings.DateNamedFormat.ShortDate);
            }
            catch
            {
                return Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Search for a Job");
                    JobTitleLabel.InnerText = SepFunctions.LangText("Job Title:");
                    KeywordsLabel.InnerText = SepFunctions.LangText("Keywords:");
                    LocationLabel.InnerText = SepFunctions.LangText("City, State or Zip/Postal Code:");
                    JobCategoryLabel.InnerText = SepFunctions.LangText("Job Category:");
                    CompanyNameLabel.InnerText = SepFunctions.LangText("Company Name:");
                    EmploymentTypeLabel.InnerText = SepFunctions.LangText("Employment Type:");
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
            ManageGridView.PageIndex = e.NewPageIndex;
            BindData(ManageGridView.PageIndex);
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
            SepFunctions.RequireLogin(SepFunctions.Security("PCRAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            // Check PCRecruiter Settings
            if (string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "PCRAPIURL")) || string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "PCRUserName")) || string.IsNullOrWhiteSpace(SepFunctions.Decrypt(SepFunctions.Setup(GlobalVars.ModuleID, "PCRPassword"))) || string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "PCRDatabaseId")))
            {
                ModFormDiv.Visible = false;
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("The Administrator does not have PCRecruiter setup currently.") + " " + SepFunctions.LangText("If you are the Administrator please go to the admin console and check your settings under General Setup.") + "</div>";
                return;
            }

            ErrorMessage.InnerHtml = string.Empty;

            if (Page.IsPostBack) BindData(0);

            if (!string.IsNullOrWhiteSpace(Location.Value)) sLocation = Strings.Trim(Location.Value);
            else sLocation = Strings.Trim(SepCommon.SepCore.Request.Item("Location"));

            if (!string.IsNullOrWhiteSpace(sLocation))
            {
                pageQuery += "&Location=" + SepFunctions.UrlEncode(sLocation);
                Location.Value = sLocation;
            }

            if (!string.IsNullOrWhiteSpace(JobTitle.Value)) sJobTitle = Strings.Trim(JobTitle.Value);
            else sJobTitle = Strings.Trim(SepCommon.SepCore.Request.Item("JobTitle"));

            if (!string.IsNullOrWhiteSpace(sJobTitle))
            {
                pageQuery += "&JobTitle=" + SepFunctions.UrlEncode(sJobTitle);
                JobTitle.Value = sJobTitle;
            }

            if (!string.IsNullOrWhiteSpace(CompanyName.Value)) sCompanyName = Strings.Trim(CompanyName.Value);
            else sCompanyName = Strings.Trim(SepCommon.SepCore.Request.Item("CompanyName"));

            if (!string.IsNullOrWhiteSpace(sCompanyName))
            {
                pageQuery += "&CompanyName=" + SepFunctions.UrlEncode(sCompanyName);
                CompanyName.Value = sCompanyName;
            }

            if (!string.IsNullOrWhiteSpace(Keywords.Value)) sKeywords = Strings.Trim(Keywords.Value);
            else sKeywords = Strings.Trim(SepCommon.SepCore.Request.Item("Keywords"));

            if (!string.IsNullOrWhiteSpace(sKeywords))
            {
                pageQuery += "&Keywords=" + SepFunctions.UrlEncode(sKeywords);
                Keywords.Value = sKeywords;
            }

            if (!string.IsNullOrWhiteSpace(JobCategory.Value)) sJobCategory = Strings.Trim(JobCategory.Value);
            else sJobCategory = Strings.Trim(SepCommon.SepCore.Request.Item("JobCategory"));

            if (!string.IsNullOrWhiteSpace(sJobCategory))
            {
                pageQuery += "&JobCategory=" + SepFunctions.UrlEncode(sJobCategory);
                JobCategory.Value = sJobCategory;
            }

            if (!string.IsNullOrWhiteSpace(EmploymentType.Value)) sEmploymentType = Strings.Trim(EmploymentType.Value);
            else sEmploymentType = Strings.Trim(SepCommon.SepCore.Request.Item("EmploymentType"));

            if (!string.IsNullOrWhiteSpace(sEmploymentType))
            {
                pageQuery += "&EmploymentType=" + SepFunctions.UrlEncode(sEmploymentType);
                EmploymentType.Value = sEmploymentType;
            }

            if (!IsPostBack && string.IsNullOrWhiteSpace(pageQuery))
            {
                var cReplace = new Replace();

                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

                cReplace.Dispose();

                cPCR.Members2PCR();
                ManageGridView.Caption = SepFunctions.LangText("Latest Job Postings");
                ManageGridView.PageSize = 10;
                try
                {
                    var sessionId = cPCR.GetSessionId();

                    var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, "JobSearch", "searchDefault");
                    var getFields = string.Empty;
                    var arrCount = 0;

                    foreach (var item in arrData)
                    {
                        if (arrCount > 0) getFields += ",";
                        getFields += Strings.ToString(item);
                        arrCount += 1;
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions?Fields=CompanyId,JobId," + getFields + "&ResultsPerPage=10&query=EndDate ge " + DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss") + ".840&OnJobBoard eq External&Order=DatePosted DESC");
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

                                ManageGridView.Columns.Clear();

                                foreach (var item in arrData)
                                    try
                                    {
                                        var tempField = new TemplateField
                                        {
                                            ItemTemplate = new AddTemplatePositions(ListItemType.Item, Strings.ToString(item)),
                                            HeaderText = root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText
                                        };
                                        ManageGridView.Columns.Add(tempField);
                                    }
                                    catch
                                    {
                                    }
                            }
                        }
                    }

                    var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobResults>(jsonString.Replace("null", "\"\""));
                    ManageGridView.DataSource = pcrResults.Results.ToList();
                    ManageGridView.DataBind();
                }
                catch
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error connecting to PCRecruiter's API.") + "</div>";
                }
            }
            else
            {
                ManageGridView.Caption = SepFunctions.LangText("Position Search Results");
                BindData(0);
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

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ErrorMessage.InnerHtml = string.Empty;

            ManageGridView.Caption = SepFunctions.LangText("Position Search Results");
            ManageGridView.PageSize = SepFunctions.toInt(SepFunctions.Setup(992, "RecPerAPage"));

            sLocation = Location.Value;
            sJobTitle = JobTitle.Value;
            sCompanyName = CompanyName.Value;
            sKeywords = Keywords.Value;
            sJobCategory = JobCategory.Value;
            sEmploymentType = EmploymentType.Value;

            BindData(0);
        }
    }

    /// <summary>
    /// Class AddTemplatePositions.
    /// Implements the <see cref="System.Web.UI.ITemplate" />
    /// </summary>
    /// <seealso cref="System.Web.UI.ITemplate" />
    public class AddTemplatePositions : ITemplate
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
        /// Initializes a new instance of the <see cref="AddTemplatePositions" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="colname">The colname.</param>
        public AddTemplatePositions(ListItemType type, string colname)
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
                            jobTitleLink.NavigateUrl = SepFunctions.GetInstallFolder() + "position/" + DataBinder.Eval(jobTitleContainer.DataItem, "JobId") + "/" + SepFunctions.Format_ISAPI(Strings.ToString(DataBinder.Eval(jobTitleContainer.DataItem, _colName)));
                        }

                        break;

                    case "CompanyName":
                        var CompanyLink = (HyperLink)sender;
                        var CompanyContainer = (GridViewRow)CompanyLink.NamingContainer;
                        var CompanyValue = DataBinder.Eval(CompanyContainer.DataItem, _colName);
                        if (CompanyValue != DBNull.Value)
                        {
                            CompanyLink.Text = Strings.ToString(CompanyValue);
                            CompanyLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_company.aspx?CompanyId=" + DataBinder.Eval(CompanyContainer.DataItem, "CompanyId");
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