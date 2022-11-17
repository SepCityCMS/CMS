// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="careers_my_company.aspx.cs" company="SepCity, Inc.">
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
    using System.Collections;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class careers_my_company.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class careers_my_company : Page
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
                    ModifyLegend.InnerHtml = SepFunctions.LangText("My Company");
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

            var UserCoId = SepFunctions.GetUserInformation("PCRCompanyId");

            ArrayList arrData;

            if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
            {
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        var root = doc.DocumentElement;

                        arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, "ComPost", "postDefault");
                        foreach (var item in arrData)
                        {
                            var objP = new HtmlGenericControl("p");

                            var objLabel = new HtmlGenericControl("label")
                            {
                                ID = item + "Label",
                                InnerText = root.SelectSingleNode("/pcrfields/CompanyFields/field[@name='" + item + "']").InnerText
                            };
                            objLabel.Attributes.Add("for", Strings.ToString(item));
                            objP.Controls.Add(objLabel);

                            try
                            {
                                switch (Strings.ToString(item))
                                {
                                    case "NumberOfEmployees":
                                        var objEmployees = new DropDownList();
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("1-10 Employees"), "1"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("11-25 Employees"), "11"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("26-50 Employees"), "26"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("51-100 Employees"), "51"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("101-200 Employees"), "101"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("201-500 Employees"), "201"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("Over 500 Employees"), "501"));
                                        objEmployees.Items.Add(new ListItem(SepFunctions.LangText("1-10 Employees"), "1"));
                                        objEmployees.ID = Strings.ToString(item);
                                        objEmployees.ClientIDMode = ClientIDMode.Static;
                                        objEmployees.CssClass = "form-control";
                                        objP.Controls.Add(objEmployees);
                                        break;

                                    case "Industry":
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
                                        break;

                                    case "Keywords":
                                        var objKeywords = new TextBox
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            TextMode = TextBoxMode.MultiLine,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objKeywords);
                                        break;

                                    case "Notes":
                                        var objNotes = new TextBox
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            TextMode = TextBoxMode.MultiLine,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objNotes);
                                        break;

                                    case "Summary":
                                        var objSummary = new TextBox
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            TextMode = TextBoxMode.MultiLine,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objSummary);
                                        break;

                                    case "Logo":
                                        var objLogo = new UploadFiles
                                        {
                                            ID = Strings.ToString(item),
                                            ModuleID = 66,
                                            Mode = UploadFiles.EInputMode.SingleFile,
                                            ContentID = UserCoId,
                                            UserID = SepFunctions.Session_User_ID()
                                        };
                                        objP.Controls.Add(objLogo);
                                        break;

                                    case "State":
                                        var objState = new StateDropdown
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objState);
                                        break;

                                    case "Country":
                                        var objCountry = new CountryDropdown
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objCountry);
                                        break;

                                    default:
                                        var objText = new TextBox
                                        {
                                            ID = Strings.ToString(item),
                                            ClientIDMode = ClientIDMode.Static,
                                            CssClass = "form-control"
                                        };
                                        objP.Controls.Add(objText);
                                        break;
                                }
                            }
                            catch
                            {
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

            if (!Page.IsPostBack)
            {
                cPCR.Members2PCR();

                var UserCoId = SepFunctions.GetUserInformation("PCRCompanyId");
                ArrayList arrData;

                arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, "ComPost", "postDefault");
                var getFields = string.Empty;
                var arrCount = 0;

                foreach (var item in arrData)
                {
                    if (arrCount > 0) getFields += ",";
                    getFields += Strings.ToString(item);
                    arrCount += 1;
                }

                var sessionId = cPCR.GetSessionId();

                WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "companies/" + UserCoId + "?Fields=" + getFields);
                WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                WRequest.Method = "GET";
                WRequest.ContentType = "application/json";
                WRequest.Accept = "application/json";

                WResponse = (HttpWebResponse)WRequest.GetResponse();
                WReader = new StreamReader(WResponse.GetResponseStream());
                var jsonString = WReader.ReadToEnd();

                PCRecruiterCompanies pcrResults = JsonConvert.DeserializeObject<PCRecruiterCompanies>(jsonString);
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "pcrfields.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);

                            arrData = SepCommon.DAL.JobBoard.SettingToArray(PCRecruiter.PCR_RecordType.Companies, "ComPost", "postDefault");
                            foreach (var item in arrData)
                                try
                                {
                                    switch (Strings.ToString(item))
                                    {
                                        case "NumberOfEmployees":
                                            var d1 = (HtmlSelect)ScreenHTML.FindControl(Strings.ToString(item));
                                            d1.Value = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                            break;

                                        case "Industry":
                                            var d2 = (HtmlSelect)ScreenHTML.FindControl(Strings.ToString(item));
                                            d2.Value = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                            break;

                                        case "State":
                                            var s = (StateDropdown)ScreenHTML.FindControl(Strings.ToString(item));
                                            s.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                            break;

                                        case "Country":
                                            var c = (CountryDropdown)ScreenHTML.FindControl(Strings.ToString(item));
                                            c.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                            break;

                                        case "Logo":
                                            break;

                                        default:
                                            var t = (TextBox)ScreenHTML.FindControl(Strings.ToString(item));
                                            t.Text = Strings.ToString(pcrResults.GetType().GetProperty(Strings.ToString(item)).GetValue(pcrResults, null));
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                        }
                    }
                }
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var sessionId = cPCR.GetSessionId();
            var UserCoId = SepFunctions.GetUserInformation("PCRCompanyId");

            // Write Company Record
            var cCompanies = new SepCommon.Models.PCRecruiterCompanies();
            if (!string.IsNullOrWhiteSpace(UserCoId) && UserCoId != "0") cCompanies.CompanyId = UserCoId;

            foreach (Control ctr in ScreenHTML.Controls)
                if (ctr is HtmlGenericControl)
                    foreach (Control contr in ctr.Controls)
                        if (contr is TextBox || contr is StateDropdown || contr is CountryDropdown || contr is UploadFiles || contr is DropDownList)
                        {
                            var value = string.Empty;
                            if (contr is HtmlSelect select)
                            {
                                value = select.Value;
                            }
                            else if (contr is StateDropdown dropdown1)
                            {
                                value = dropdown1.Text;
                            }
                            else if (contr is DropDownList list)
                            {
                                value = list.SelectedValue;
                            }
                            else if (contr is CountryDropdown dropdown)
                            {
                                value = dropdown.Text;
                            }
                            else if (contr is UploadFiles)
                            {
                                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                                {
                                    conn.Open();
                                    using (var cmd = new SqlCommand("SELECT FileData FROM Uploads WHERE ModuleID=@ModuleID AND UserID=@UserID AND isTemp='1' AND UniqueID=@UniqueID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ModuleID", 66);
                                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                        cmd.Parameters.AddWithValue("@UniqueID", UserCoId);
                                        using (SqlDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows)
                                            {
                                                RS.Read();
                                                var bits = (byte[])RS["FileData"];
                                                value = SepFunctions.Base64Encode(SepFunctions.BytesToString(bits));
                                            }

                                        }
                                    }

                                    using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE ModuleID=@ModuleID AND UserID=@UserID AND isTemp='1' AND UniqueID=@UniqueID", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@ModuleID", 66);
                                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                                        cmd.Parameters.AddWithValue("@UniqueID", UserCoId);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                value = ((TextBox)contr).Text;
                            }

                            switch (contr.ID)
                            {
                                case "AnnualRevenue":
                                    var jAnnualRevenue = new SepCommon.Models.PCRSalaryField
                                    {
                                        CurrencyCode = "USD",
                                        Value = Strings.ToString(SepFunctions.toDouble(value))
                                    };
                                    cCompanies.AnnualRevenue = jAnnualRevenue;
                                    break;

                                default:
                                    var propertyInfo = cCompanies.GetType().GetProperty(contr.ID);
                                    propertyInfo.SetValue(cCompanies, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                                    break;
                            }
                        }

            cCompanies.UserName = "EMPLOYER";

            try
            {
                var postData = JsonConvert.SerializeObject(cCompanies);
                var byteArray = Encoding.UTF8.GetBytes(postData);

                try
                {
                    WRequest = (HttpWebRequest)WebRequest.Create(cPCR.GetPCRequiterURL() + "companies" + Strings.ToString(!string.IsNullOrWhiteSpace(UserCoId) && UserCoId != "0" ? "/" + UserCoId : string.Empty));
                    WRequest.Headers.Add("Authorization", "BEARER " + sessionId);
                    if (!string.IsNullOrWhiteSpace(UserCoId) && UserCoId != "0") WRequest.Method = "PUT";
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
                }
                catch
                {
                }

                var jsonString = WReader.ReadToEnd();

                var pcrResults = JsonConvert.DeserializeObject<PCRecruiterCompanies>(jsonString);

                // End Write Company

                // Save Company ID
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Members SET PCRCompanyId=@PCRCompanyId WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        cmd.Parameters.AddWithValue("@PCRCompanyId", pcrResults.CompanyId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // End Save
                if (!string.IsNullOrWhiteSpace(UserCoId) && UserCoId != "0")
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Updated", "Company: " + pcrResults.CompanyId);
                else
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Added", "Company: " + pcrResults.CompanyId);
                ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Company has been successfully saved.") + "</div>";
                ModFormDiv.Visible = false;
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error: " + ex.Message);
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Error connecting to PCRecruiter API.") + "</div>";
                ModFormDiv.Visible = false;
            }
        }
    }
}