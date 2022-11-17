// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_job_modify.aspx.cs" company="SepCity, Inc.">
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
    using SepControls;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class careers_job_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_job_modify : Page
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
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Position");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
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

            cPCR.Members2PCR();

            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PCRCompanyId")))
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            var root = doc.DocumentElement;

                            var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, "JobPost", "postDefault");
                            foreach (var item in arrData)
                            {
                                var objP = new HtmlGenericControl("p");

                                var objLabel = new HtmlGenericControl("label")
                                {
                                    ID = item + "Label",
                                    InnerText = root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText
                                };
                                objLabel.Attributes.Add("for", Strings.ToString(item));
                                objP.Controls.Add(objLabel);

                                if (Strings.ToString(item) == "JobType")
                                {
                                    var objText = new DropDownList
                                    {
                                        ID = Strings.ToString(item)
                                    };
                                    objP.Controls.Add(objText);

                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Full Time"), "FTP"));
                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Part time"), "PTP"));
                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Full Time (Temporary)"), "FTT"));
                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Part time (Temporary)"), "PTT"));
                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Contractor (Full Time)"), "CFT"));
                                    objText.Items.Add(new ListItem(SepFunctions.LangText("Contractor (Part Time)"), "CPT"));
                                }
                                else if (Strings.ToString(item) == "State")
                                {
                                    var objText = new StateDropdown
                                    {
                                        ID = Strings.ToString(item)
                                    };
                                    objP.Controls.Add(objText);
                                }
                                else if (Strings.ToString(item) == "Industry")
                                {
                                    var objIndustry = new DropDownList();
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Not Specified"), string.Empty));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Automotive"), "Automotive"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Business Services"), "Business Services"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Computers/Eletronic"), "Computers/Eletronic"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Construction"), "Construction"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Education"), "Education"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Entertainment"), "Entertainment"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Food & Dining"), "Food & Dining"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Health & Medicine"), "Health & Medicine"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Home & Garden"), "Home & Garden"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Legal & Financial"), "Legal & Financial"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Manufacturing/Wholesale"), "Manufacturing/Wholesale"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Merchants (Retail)"), "Merchants (Retail)"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Miscellaneous"), "Miscellaneous"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Personal Services"), "Personal Services"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Real Estate"), "Real Estate"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Travel/Transport"), "Travel/Transport"));
                                    objIndustry.Items.Add(new ListItem(SepFunctions.LangText("Other"), "Other"));
                                    objIndustry.ID = Strings.ToString(item);
                                    objIndustry.ClientIDMode = ClientIDMode.Static;
                                    objIndustry.CssClass = "form-control";
                                    objP.Controls.Add(objIndustry);
                                }
                                else if (Strings.ToString(item) == "Status")
                                {
                                    var objStatus = new DropDownList();
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Available/Open"), "Available"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Filled"), "Filled"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Internal Only"), "Internal"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Pending"), "Pending"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Hold"), "Hold"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Expired"), "Expired"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Closed"), "Closed"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Offer Accepted"), "Offer"));
                                    objStatus.Items.Add(new ListItem(SepFunctions.LangText("Not Available"), "Not"));
                                    objStatus.ID = Strings.ToString(item);
                                    objStatus.ClientIDMode = ClientIDMode.Static;
                                    objStatus.CssClass = "form-control";
                                    objP.Controls.Add(objStatus);
                                }
                                else if (Strings.ToString(item) == "Country")
                                {
                                    var objText = new CountryDropdown
                                    {
                                        ID = Strings.ToString(item)
                                    };
                                    objP.Controls.Add(objText);
                                }
                                else if (Strings.ToString(item) == "JobDescription")
                                {
                                    var objJobDescription = new WYSIWYGEditor
                                    {
                                        ID = Strings.ToString(item)
                                    };
                                    objP.Controls.Add(objJobDescription);
                                }
                                else if (Strings.ToString(item) == "InternalJobDescription")
                                {
                                    var objInternalJobDescription = new WYSIWYGEditor
                                    {
                                        ID = Strings.ToString(item)
                                    };
                                    objP.Controls.Add(objInternalJobDescription);
                                }
                                else
                                {
                                    var objText = new TextBox
                                    {
                                        ID = Strings.ToString(item)
                                    };

                                    if (Strings.ToString(item) == "JobDescription" || Strings.ToString(item) == "Benefits" || Strings.ToString(item) == "Notes" || Strings.ToString(item) == "WebNotes" || Strings.ToString(item) == "InternalJobDescription" || Strings.ToString(item) == "Keywords") objText.TextMode = TextBoxMode.MultiLine;

                                    objText.CssClass = "form-control";
                                    objP.Controls.Add(objText);
                                }

                                ScreenHTML.Controls.Add(objP);
                            }
                        }
                    }
                }
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

            PCRecruiterJobs pcrResults = null;

            if (!string.IsNullOrWhiteSpace(SepFunctions.GetUserInformation("PCRCompanyId")))
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")))
                    {
                        var sessionId = cPCR.GetSessionId();
                        var sQuery = "?Fields=JobId,CompanyId,JobTitle,Industry,ContactName,ContactPhone,ContactEmail,Country,City,State,PostalCode,JobDescription,EducationAid,JobType,MinSalary,MaxSalary,PositionId";

                        WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions/" + SepCommon.SepCore.Request.Item("JobId") + sQuery);
                        WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                        WRequest.Method = "GET";
                        WRequest.ContentType = "application/json";
                        WRequest.Accept = "application/json";

                        WResponse = (HttpWebResponse)WRequest.GetResponse();
                        WReader = new StreamReader(WResponse.GetResponseStream());
                        var jsonString = WReader.ReadToEnd();

                        pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobs>(jsonString);

                        if (pcrResults.CompanyId == SepFunctions.GetUserInformation("PCRCompanyId"))
                        {
                            ModifyLegend.InnerText = SepFunctions.LangText("Edit Position");
                            JobId.Value = SepCommon.SepCore.Request.Item("JobId");
                            PostPricing.Visible = false;
                        }
                        else
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You can not modify this job.") + "</div>";
                            ModFormDiv.Visible = false;
                        }
                    }
                    else
                    {
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostPostJobs", "GetPostJobs", SepFunctions.GetUserInformation("PCRCandidateId"), true) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must add your company before you can manage your positions.") + "</div>";
                    ModFormDiv.Visible = false;
                }

                if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            var root = doc.DocumentElement;

                            var arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Positions, "JobPost", "postDefault");
                            foreach (var item in arrData)
                            {
                                var objP = new HtmlGenericControl("p");

                                var objLabel = new HtmlGenericControl("label")
                                {
                                    ID = item + "Label",
                                    InnerText = root.SelectSingleNode("/pcrfields/PositionFields/field[@name='" + item + "']").InnerText
                                };
                                objLabel.Attributes.Add("for", Strings.ToString(item));
                                objP.Controls.Add(objLabel);

                                if (Strings.ToString(item) == "JobType")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var d1 = (HtmlSelect)ScreenHTML.FindControl(Strings.ToString(item));
                                            d1.Value = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }
                                else if (Strings.ToString(item) == "State")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var d1 = (StateDropdown)ScreenHTML.FindControl(Strings.ToString(item));
                                            d1.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }
                                else if (Strings.ToString(item) == "Industry")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var d2 = (HtmlSelect)ScreenHTML.FindControl(Strings.ToString(item));
                                            d2.Value = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }
                                else if (Strings.ToString(item) == "Status")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var d3 = (HtmlSelect)ScreenHTML.FindControl(Strings.ToString(item));
                                            d3.Value = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }
                                else if (Strings.ToString(item) == "Country")
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var d1 = (CountryDropdown)ScreenHTML.FindControl(Strings.ToString(item));
                                            d1.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) && !IsPostBack)
                                        if (pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null) != null)
                                        {
                                            var t = (TextBox)ScreenHTML.FindControl(Strings.ToString(item));
                                            t.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                        }
                                }

                                objP.Dispose();
                            }
                        }
                    }
                }
            }

            PostPricing.PriceUniqueID = Strings.ToString(SepFunctions.GetIdentity());
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sessionId = cPCR.GetSessionId();

            var intModuleID = 66;

            var expireDays = SepFunctions.toInt(SepFunctions.Setup(intModuleID, "PCRExpireDays"));
            if (expireDays == 0)
                expireDays = 30;

            // Write Company Record
            var cJobs = new SepCommon.Models.PCRecruiterJobs
            {
                CompanyId = SepFunctions.GetUserInformation("PCRCompanyId"),
                Status = "Available"
            };

            foreach (Control ctr in ScreenHTML.Controls)
                if (ctr is HtmlGenericControl)
                    foreach (Control contr in ctr.Controls)
                        if (contr is TextBox || contr is StateDropdown || contr is CountryDropdown || contr is DropDownList)
                        {
                            string value;
                            if (contr is HtmlSelect select)
                                value = select.Value;
                            else if (contr is StateDropdown dropdown)
                                value = dropdown.Text;
                            else if (contr is CountryDropdown dropdown1)
                                value = dropdown1.Text;
                            else
                                value = ((TextBox)contr).Text;
                            switch (contr.ID)
                            {
                                case "MinSalary":
                                    var jMinSalary = new SepCommon.Models.PCRSalaryField
                                    {
                                        CurrencyCode = "USD",
                                        Value = Strings.ToString(SepFunctions.toDouble(value))
                                    };
                                    cJobs.MinSalary = jMinSalary;
                                    break;

                                case "MaxSalary":
                                    var jMaxSalary = new SepCommon.Models.PCRSalaryField
                                    {
                                        CurrencyCode = "USD",
                                        Value = Strings.ToString(SepFunctions.toDouble(value))
                                    };
                                    cJobs.MaxSalary = jMaxSalary;
                                    break;

                                case "BillRate":
                                    var jBillRate = new SepCommon.Models.PCRSalaryField
                                    {
                                        CurrencyCode = "USD",
                                        Value = Strings.ToString(SepFunctions.toDouble(value))
                                    };
                                    cJobs.BillRate = jBillRate;
                                    break;

                                case "PayRate":
                                    var jPayRate = new SepCommon.Models.PCRSalaryField
                                    {
                                        CurrencyCode = "USD",
                                        Value = Strings.ToString(SepFunctions.toDouble(value))
                                    };
                                    cJobs.PayRate = jPayRate;
                                    break;

                                default:
                                    var propertyInfo = cJobs.GetType().GetProperty(contr.ID);
                                    propertyInfo.SetValue(cJobs, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                                    break;
                            }
                        }

            cJobs.DatePosted = DateTime.Now;
            cJobs.BeginDate = DateTime.Now;
            cJobs.EndDate = DateAndTime.DateAdd(DateAndTime.DateInterval.Day, expireDays, DateTime.Now);
            cJobs.UserName = "EMPLOYER";

            var jCustom = new SepCommon.Models.PCRPositionCustom();
            var jCollection = new Collection<PCRPositionCustom>();
            jCustom.FieldName = "Highlighted Listing";
            jCustom.FieldType = "2";
            string[] arrList = null;
            Array.Resize(ref arrList, 1);
            arrList[0] = "0";
            jCustom.Values = arrList;
            jCollection.Add(jCustom);
            jCustom = new SepCommon.Models.PCRPositionCustom
            {
                FieldName = "Bold Listing",
                FieldType = "17"
            };
            Array.Resize(ref arrList, 1);
            arrList[0] = "0";
            jCustom.Values = arrList;
            jCollection.Add(jCustom);
            jCustom = new PCRPositionCustom
            {
                FieldName = "Feature Listing",
                FieldType = "17"
            };
            Array.Resize(ref arrList, 1);
            arrList[0] = "0";
            jCustom.Values = arrList;
            jCollection.Add(jCustom);
            cJobs.CustomFields = jCollection;

            var postData = JsonConvert.SerializeObject(cJobs);

            var byteArray = Encoding.UTF8.GetBytes(postData);
            var sJobId = !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")) ? "/" + SepCommon.SepCore.Request.Item("JobId") : string.Empty;
            WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "positions" + sJobId);
            WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId"))) WRequest.Method = "PUT";
            else WRequest.Method = "POST";
            WRequest.ContentLength = byteArray.Length;
            WRequest.ContentType = "application/json";
            WRequest.Accept = "application/json";
            using (var dataStream = WRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WResponse = (HttpWebResponse)WRequest.GetResponse();
            WReader = new StreamReader(WResponse.GetResponseStream());

            var jsonString = WReader.ReadToEnd();

            var pcrResults = JsonConvert.DeserializeObject<PCRecruiterJobs>(jsonString);

            // End Write Company
            JobId.Value = pcrResults.JobId;

            PostPricing.PriceUniqueID = JobId.Value;
            SepFunctions.Pricing_Options_Save(66, JobId.Value);

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("JobId")))
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Updated", "Job: " + pcrResults.JobId);
            else
                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Added", "Job: " + pcrResults.JobId);
            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Job has been successfully saved.") + "</div>";
        }
    }
}