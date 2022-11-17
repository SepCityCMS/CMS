// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_browse_candidates.aspx.cs" company="SepCity, Inc.">
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
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class careers_browse_candidates.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_browse_candidates : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The c PCR
        /// </summary>
        private readonly PCRecruiter cPCR = new PCRecruiter();

        /// <summary>
        /// The current page
        /// </summary>
        private int currentPage;

        /// <summary>
        /// The page query
        /// </summary>
        private string pageQuery = string.Empty;

        /// <summary>
        /// The s industry
        /// </summary>
        private string sIndustry = string.Empty;

        /// <summary>
        /// The s keywords
        /// </summary>
        private string sKeywords = string.Empty;

        /// <summary>
        /// The s location
        /// </summary>
        private string sLocation = string.Empty;

        /// <summary>
        /// The s title
        /// </summary>
        private string sTitle = string.Empty;

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
        public void BindData()
        {
            var wClause = string.Empty;
            var noResults = false;

            if (!string.IsNullOrWhiteSpace(sLocation))
            {
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
                if (!string.IsNullOrWhiteSpace(sTitle))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "Title co " + Strings.Trim(sTitle);
                }

                if (!string.IsNullOrWhiteSpace(sIndustry))
                {
                    if (!string.IsNullOrWhiteSpace(wClause))
                        wClause += " AND ";
                    wClause += "Industry eq " + Strings.Trim(sIndustry);
                }

                if (!string.IsNullOrWhiteSpace(wClause))
                    wClause += " AND ";
                wClause += "Status eq C";

                var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Candidates, "CanSearch", "searchDefault");
                var getFields = string.Empty;
                var arrCount = 0;

                foreach (var item in arrData)
                {
                    if (arrCount > 0) getFields += ",";
                    getFields += Strings.ToString(item);
                    arrCount += 1;
                }

                var sessionId = cPCR.GetSessionId();
                var sQuery = "Fields=CompanyId,CandidateId," + getFields + "&query=" + wClause;

                try
                {
                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "candidates?" + sQuery + "&ResultsPerPage=" + SepFunctions.toLong(SepFunctions.Setup(992, "RecPerAPage")) + "&Page=" + currentPage + Strings.ToString(!string.IsNullOrWhiteSpace(sKeywords) ? "&Keywords=" + sKeywords : string.Empty) + "&Order=FirstName ASC");
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
                                            ItemTemplate = new AddTemplateCandidate(ListItemType.Item, Strings.ToString(item)),
                                            HeaderText = root.SelectSingleNode("/pcrfields/CandidateFields/field[@name='" + item + "']").InnerText
                                        };
                                        ManageGridView.Columns.Add(tempField);
                                    }
                                    catch
                                    {
                                    }
                            }
                        }
                    }

                    var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCandidateResults>(jsonString);
                    ManageGridView.DataSource = pcrResults.Results.ToList();
                    ManageGridView.DataBind();

                    var pcrTotalResults = JsonConvert.DeserializeObject<PCRecruiterTotalRecords>(jsonString);

                    if (pcrTotalResults.TotalRecords > 0)
                    {
                        PagingPanel.Visible = true;
                        var totalPages = Math.Ceiling(SepFunctions.toDouble(Strings.ToString(pcrTotalResults.TotalRecords)) / SepFunctions.toDouble(SepFunctions.Setup(992, "RecPerAPage")));

                        litPageNumber.InnerHtml = Strings.ToString(currentPage);
                        litTotalPages.InnerHtml = Strings.ToString(totalPages);

                        if (currentPage == 1)
                        {
                            btnFirst.Visible = false;
                            btnPrevious.Visible = false;
                            btnNext.Visible = true;
                            btnNext.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + (currentPage + 1) + "&Total=" + totalPages + pageQuery + "';return false;";
                            btnLast.Visible = true;
                            btnLast.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + totalPages + "&Total=" + totalPages + pageQuery + "';return false;";
                        }

                        if (currentPage == totalPages)
                        {
                            btnFirst.Visible = true;
                            btnFirst.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=1&Total=" + totalPages + pageQuery + "';return false;";
                            btnPrevious.Visible = true;
                            btnPrevious.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + (currentPage - 1) + "&Total=" + totalPages + pageQuery + "';return false;";
                            btnNext.Visible = false;
                            btnLast.Visible = false;
                        }
                    }

                    if (ManageGridView.Rows.Count == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("No results found in your search.") + "</div>";
                        ManageGridView.Visible = false;
                        PagingPanel.Visible = false;
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Browse Candidates");
                    NameTitleLabel.InnerText = SepFunctions.LangText("Title:");
                    KeywordsLabel.InnerText = SepFunctions.LangText("Keywords:");
                    IndustryLabel.InnerText = SepFunctions.LangText("Industry/Specialty:");
                    LocationLabel.InnerText = SepFunctions.LangText("City, State or Zip/Postal Code:");
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
            SepFunctions.RequireLogin(SepFunctions.Security("PCRBrowse"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            cPCR.Members2PCR();

            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("Page")) == 0) currentPage = 1;
            else currentPage = SepFunctions.toInt(SepCommon.SepCore.Request.Item("Page"));

            if (!string.IsNullOrWhiteSpace(Location.Value)) sLocation = Strings.Trim(Location.Value);
            else sLocation = Strings.Trim(SepCommon.SepCore.Request.Item("Location"));

            if (!string.IsNullOrWhiteSpace(sLocation))
            {
                pageQuery += "&Location=" + SepFunctions.UrlEncode(sLocation);
                Location.Value = sLocation;
            }

            if (!string.IsNullOrWhiteSpace(NameTitle.Value)) sTitle = Strings.Trim(NameTitle.Value);
            else sTitle = Strings.Trim(SepCommon.SepCore.Request.Item("Title"));

            if (!string.IsNullOrWhiteSpace(sTitle))
            {
                pageQuery += "&Title=" + SepFunctions.UrlEncode(sTitle);
                NameTitle.Value = sTitle;
            }

            if (!string.IsNullOrWhiteSpace(Keywords.Value)) sKeywords = Strings.Trim(Keywords.Value);
            else sKeywords = Strings.Trim(SepCommon.SepCore.Request.Item("Keywords"));

            if (!string.IsNullOrWhiteSpace(sKeywords))
            {
                pageQuery += "&Keywords=" + SepFunctions.UrlEncode(sKeywords);
                Keywords.Value = sKeywords;
            }

            if (!string.IsNullOrWhiteSpace(Industry.Value)) sIndustry = Strings.Trim(Industry.Value);
            else sIndustry = Strings.Trim(SepCommon.SepCore.Request.Item("Industry"));

            if (!string.IsNullOrWhiteSpace(sIndustry))
            {
                pageQuery += "&Industry=" + SepFunctions.UrlEncode(sIndustry);
                Industry.Value = sIndustry;
            }

            btnFirst.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=1&Total=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("Total")) + pageQuery + "';return false;";
            btnPrevious.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + (currentPage - 1) + "&Total=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("Total")) + pageQuery + "';return false;";
            btnNext.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + (currentPage + 1) + "&Total=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("Total")) + pageQuery + "';return false;";
            btnLast.OnClientClick = "document.location.href='careers_browse_candidates.aspx?Page=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("Total")) + "&Total=" + SepFunctions.toLong(SepCommon.SepCore.Request.Item("Total")) + pageQuery + "';return false;";

            if (!IsPostBack)
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("Page")))
                    BindData();
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
            currentPage = 1;

            sLocation = Location.Value;
            sTitle = NameTitle.Value;
            sKeywords = Keywords.Value;
            sIndustry = Industry.Value;

            BindData();
        }
    }

    /// <summary>
    /// Class AddTemplateCandidate.
    /// Implements the <see cref="System.Web.UI.ITemplate" />
    /// </summary>
    /// <seealso cref="System.Web.UI.ITemplate" />
    public class AddTemplateCandidate : ITemplate
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
        /// Initializes a new instance of the <see cref="AddTemplateCandidate" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="colname">The colname.</param>
        public AddTemplateCandidate(ListItemType type, string colname)
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
                            case "CompanyName":
                                var companyNameLink = new HyperLink();
                                companyNameLink.DataBinding += ht_DataBinding;
                                container.Controls.Add(companyNameLink);
                                break;

                            case "FirstName":
                                var firstNameLink = new HyperLink();
                                firstNameLink.DataBinding += ht_DataBinding;
                                container.Controls.Add(firstNameLink);
                                break;

                            case "LastName":
                                var lastNameLink = new HyperLink();
                                lastNameLink.DataBinding += ht_DataBinding;
                                container.Controls.Add(lastNameLink);
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
                    case "CompanyName":
                        var companyNameLink = (HyperLink)sender;
                        var companyNameContainer = (GridViewRow)companyNameLink.NamingContainer;
                        var companyNameValue = DataBinder.Eval(companyNameContainer.DataItem, _colName);
                        if (companyNameValue != DBNull.Value)
                        {
                            companyNameLink.Text = Strings.ToString(companyNameValue);
                            companyNameLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_company.aspx?CompanyId=" + DataBinder.Eval(companyNameContainer.DataItem, "CompanyId");
                        }

                        break;

                    case "FirstName":
                        var firstNameLink = (HyperLink)sender;
                        var firstNameContainer = (GridViewRow)firstNameLink.NamingContainer;
                        var firstNameValue = DataBinder.Eval(firstNameContainer.DataItem, _colName);
                        if (firstNameValue != DBNull.Value)
                        {
                            firstNameLink.Text = Strings.ToString(firstNameValue);
                            firstNameLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_view_candidate.aspx?CandidateId=" + DataBinder.Eval(firstNameContainer.DataItem, "CandidateId");
                        }

                        break;

                    case "LastName":
                        var lastNameLink = (HyperLink)sender;
                        var lastNameContainer = (GridViewRow)lastNameLink.NamingContainer;
                        var lastNameValue = DataBinder.Eval(lastNameContainer.DataItem, _colName);
                        if (lastNameValue != DBNull.Value)
                        {
                            lastNameLink.Text = Strings.ToString(lastNameValue);
                            lastNameLink.NavigateUrl = SepFunctions.GetInstallFolder() + "careers_view_candidate.aspx?CandidateId=" + DataBinder.Eval(lastNameContainer.DataItem, "CandidateId");
                        }

                        break;

                    case "DateEntered":
                        Format_Date_Field(sender);
                        break;

                    case "LastActivity":
                        Format_Date_Field(sender);
                        break;

                    case "LastModified":
                        Format_Date_Field(sender);
                        break;

                    case "Available":
                        Format_Date_Field(sender);
                        break;

                    case "CurrentSalary":
                        Format_Currency_Field(sender);
                        break;

                    case "DesiredSalary":
                        Format_Currency_Field(sender);
                        break;

                    case "BillRate":
                        Format_Currency_Field(sender);
                        break;

                    case "PayRate":
                        Format_Currency_Field(sender);
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