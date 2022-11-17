// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_company.aspx.cs" company="SepCity, Inc.">
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
    /// Class careers_company.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_company : Page
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
        /// <param name="CompanyName">Name of the company.</param>
        public void Bind_Data(string CompanyName)
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
            var sQuery = "Fields=CompanyId,JobId," + getFields + "&query=CompanyId eq " + SepCommon.SepCore.Request.Item("CompanyId");

            try
            {
                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions?" + sQuery + "&Order=DatePosted DESC");
                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                WRequest.Method = "GET";
                WRequest.ContentType = "application/json";
                WRequest.Accept = "application/json";

                WResponse = (HttpWebResponse)WRequest.GetResponse();
                WReader = new StreamReader(WResponse.GetResponseStream());
                var jsonString = WReader.ReadToEnd();

                if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    var doc = new XmlDocument();
                    doc.Load(SepFunctions.GetDirValue("app_data") + "pcrfields.xml");

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

                    doc = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobResults>(jsonString);
                ManageGridView.DataSource = pcrResults.Results.ToList();
                ManageGridView.DataBind();

                if (ManageGridView.Rows.Count == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-info\" role=\"alert\">" + SepFunctions.LangText("This company does not have any positions available.") + "</div>";
                    ManageGridView.Visible = false;
                }
                else
                {
                    ManageGridView.Caption = SepFunctions.LangText("Positions posted by ~~" + CompanyName + "~~");
                    ErrorMessage.InnerHtml = string.Empty;
                }
            }
            catch
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("There has been an error connecting to the PCRecruiter API.") + "</div>";
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
            SepFunctions.RequireLogin(SepFunctions.Security("PCRAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            cPCR.Members2PCR();

            var sessionId = cPCR.GetSessionId();

            var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, "ComDetail", "detailsDefault");
            var getFields = string.Empty;
            var arrCount = 0;

            foreach (var item in arrData)
            {
                if (arrCount > 0) getFields += ",";
                getFields += Strings.ToString(item);
                arrCount += 1;
            }

            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "companies/" + SepCommon.SepCore.Request.Item("CompanyId") + "?Fields=" + getFields);
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            WRequest.Method = "GET";
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";

            WResponse = (HttpWebResponse)WRequest.GetResponse();
            WReader = new StreamReader(WResponse.GetResponseStream());
            var jsonString = WReader.ReadToEnd();

            var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCompanies>(jsonString);
            if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
            {
                var doc = new XmlDocument();
                doc.Load(SepFunctions.GetDirValue("app_data") + "pcrfields.xml");

                var root = doc.DocumentElement;

                foreach (var item in arrData)
                {

                    string drawScreenHTML;
                    try
                    {
                        var ret = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                        drawScreenHTML = "<p class=\"CandidateRow\">";
                        drawScreenHTML += "  <label class=\"CandidateInfoLabel\">" + root.SelectSingleNode("/pcrfields/CompanyFields/field[@name='" + item + "']").InnerText + "</label>";
                        if (!string.IsNullOrWhiteSpace(ret))
                            switch (Strings.ToString(item))
                            {
                                case "CompanyName":
                                    CompanyName.InnerText += pcrResults.CompanyName + " ";
                                    drawScreenHTML = string.Empty;
                                    break;

                                case "AnnualRevenue":
                                    drawScreenHTML += SepFunctions.Format_Currency(pcrResults.AnnualRevenue.Value);
                                    break;

                                case "NumberOfEmployees":
                                    if (!string.IsNullOrWhiteSpace(pcrResults.NumberOfEmployees))
                                        switch (SepFunctions.toInt(pcrResults.NumberOfEmployees))
                                        {
                                            case 1:
                                                drawScreenHTML += SepFunctions.LangText("1-10 employees");
                                                break;

                                            case 11:
                                                drawScreenHTML += SepFunctions.LangText("11-25 employees");
                                                break;

                                            case 26:
                                                drawScreenHTML += SepFunctions.LangText("26-50 employees");
                                                break;

                                            case 51:
                                                drawScreenHTML += SepFunctions.LangText("51-100 employees");
                                                break;

                                            case 101:
                                                drawScreenHTML += SepFunctions.LangText("101-200 employees");
                                                break;

                                            case 201:
                                                drawScreenHTML += SepFunctions.LangText("201-500 employees");
                                                break;

                                            case 501:
                                                drawScreenHTML += SepFunctions.LangText("501+ employees");
                                                break;

                                            default:
                                                drawScreenHTML += pcrResults.NumberOfEmployees + " " + SepFunctions.LangText("employees");
                                                break;
                                        }
                                    else
                                        drawScreenHTML = string.Empty;

                                    break;

                                case "EmailWWWAddress":
                                    drawScreenHTML += "<a href=\"mailto:" + ret + "\">" + ret + "</a>";
                                    break;

                                default:
                                    if (root.SelectSingleNode("/pcrfields/CompanyFields/field[@name='" + item + "']").Attributes["type"].InnerText == "date")
                                        drawScreenHTML += SepFunctions.toDate(ret);
                                    else
                                        drawScreenHTML += ret;
                                    break;
                            }
                        else
                            drawScreenHTML += SepFunctions.LangText("Not Specified");

                        if (!string.IsNullOrWhiteSpace(drawScreenHTML)) drawScreenHTML += "</p>";
                    }
                    catch
                    {
                        drawScreenHTML = string.Empty;
                    }

                    ScreenHTML.InnerHtml += drawScreenHTML;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            Bind_Data(pcrResults.CompanyName);
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
    /// Class AddTemplateCompany.
    /// Implements the <see cref="System.Web.UI.ITemplate" />
    /// </summary>
    /// <seealso cref="System.Web.UI.ITemplate" />
    public class AddTemplateCompany : ITemplate // -V3072
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
        /// Initializes a new instance of the <see cref="AddTemplateCompany" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="colname">The colname.</param>
        public AddTemplateCompany(ListItemType type, string colname)
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
                                jobTitleLink.Dispose();
                                break;

                            default:
                                var defaultLiteral = new Literal();
                                defaultLiteral.DataBinding += ht_DataBinding;
                                container.Controls.Add(defaultLiteral);
                                defaultLiteral.Dispose();
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