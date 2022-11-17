// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="import_utility.aspx.cs" company="SepCity, Inc.">
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
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class import_utility.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class import_utility : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The arr dup options
        /// </summary>
        private string[] arrDupOptions;

        /// <summary>
        /// The string table
        /// </summary>
        private string strTable = string.Empty;

        /// <summary>
        /// The with events field import button
        /// </summary>
        private Button withEventsField_ImportButton;

        /// <summary>
        /// Gets or sets the import button.
        /// </summary>
        /// <value>The import button.</value>
        protected Button ImportButton
        {
            get => withEventsField_ImportButton;

            set
            {
                if (withEventsField_ImportButton != null) withEventsField_ImportButton.Click -= ImportButton_Click;
                withEventsField_ImportButton = value;
                if (withEventsField_ImportButton != null) withEventsField_ImportButton.Click += ImportButton_Click;
            }
        }

        /// <summary>
        /// Does the not skip.
        /// </summary>
        /// <param name="sField">The s field.</param>
        /// <param name="strTable">The string table.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool doNotSkip(string sField, string strTable)
        {
            if (strTable == "Members")
            {
                if (sField == "AFFILIATEPAID" || sField == "REFERRALID" || sField == "AFFILIATEID" || sField == "DELETED" || sField == "DATEDELETED")
                    return false;
                return true;
            }

            return true;
        }

        /// <summary>
        /// Adms the get module tables.
        /// </summary>
        /// <param name="iModuleID">The i module identifier.</param>
        public void ADM_Get_Module_Tables(int iModuleID)
        {
            switch (iModuleID)
            {
                case 35:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "Articles";
                    arrDupOptions[0] = "Title";
                    break;

                case 20:
                    Array.Resize(ref arrDupOptions, 2);
                    strTable = "BusinessListings";
                    arrDupOptions[0] = "BusinessName";
                    arrDupOptions[1] = "PhoneNumber";
                    break;

                case 46:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "EventCalendar";
                    arrDupOptions[0] = "Subject";
                    break;

                case 9:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "FAQ";
                    arrDupOptions[0] = string.Empty;
                    break;

                case 12:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "ForumsMessages";
                    arrDupOptions[0] = "Subject";
                    break;

                case 19:
                    Array.Resize(ref arrDupOptions, 2);
                    strTable = "LinksWebsites";
                    arrDupOptions[0] = "LinkName";
                    arrDupOptions[1] = "LinkURL";
                    break;

                case 986:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "Members";
                    arrDupOptions[0] = "Username";
                    break;

                case 23:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "News";
                    arrDupOptions[0] = "Topic";
                    break;

                case 24:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "NewslettersUsers";
                    arrDupOptions[0] = "EmailAddress";
                    break;

                case 41:
                    Array.Resize(ref arrDupOptions, 1);
                    strTable = "ShopProducts";
                    arrDupOptions[0] = "ProductName";
                    break;
            }
        }

        /// <summary>
        /// Adms the import map fields.
        /// </summary>
        /// <param name="sFileName">Name of the s file.</param>
        /// <returns>System.String.</returns>
        public string ADM_Import_Map_Fields(string sFileName)
        {
            var str = new StringBuilder();
            var inidata = string.Empty;
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) == false) return string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                ADM_Get_Module_Tables(SepFunctions.toInt(ModuleID.Value));

                string SqlStr = "SELECT * FROM " + strTable;
                using (var sr = new StreamReader(SepFunctions.GetDirValue("App_Data") + sFileName))
                {
                    inidata = sr.ReadToEnd();
                }

                str.Append("<script type=\"text/javascript\" language=\"JavaScript\">");
                str.Append("function SelectFields(strField, fieldName){");
                str.Append("var atext = '';");
                int aa = 0;
                string[] arrRows = Strings.Split(inidata, Environment.NewLine);
                string[] arrCols = Strings.Split(Strings.Replace(arrRows[0], "\"", string.Empty), ",");
                str.Append("atext = atext + '<select name=\"input'+strField+'\" class=\"form-control\">';");
                str.Append("atext = atext + unescape('<option value=\"\">" + SepFunctions.EscQuotes("--- " + SepFunctions.LangText("Select Field") + " ---") + "</option>');");
                str.Append("if(fieldName == 'PASSWORD') {");
                str.Append("  atext = atext + unescape('<option value=\"[RANDOM]\">" + SepFunctions.EscQuotes("--- " + SepFunctions.LangText("Random Password") + " ---") + "</option>');");
                str.Append("}");
                if (arrCols != null)
                {
                    for (var i = 0; i <= Information.UBound(arrCols); i++)
                        if (!string.IsNullOrWhiteSpace(arrCols[i]))
                        {
                            aa += 1;
                            str.Append("atext = atext + unescape('<option value=\"" + aa + "\">" + SepFunctions.EscQuotes(arrCols[i]) + "</option>');");
                        }
                }

                str.Append("atext = atext + unescape('" + SepFunctions.EscQuotes("</select>") + "');");
                str.Append("document.write(atext);");
                str.Append("}");
                str.Append("function ChangeClass(){");
                using (var cmd = new SqlCommand("SELECT * FROM AccessClasses ORDER BY ClassName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            str.Append("if(document.frmMapFields.AccessClass.value=='" + SepFunctions.openNull(RS["ClassID"]) + "'){");
                            str.Append("document.getElementById('AccessKeys').innerHTML='" + SepFunctions.openNull(RS["KeyIDs"]) + "'");
                            str.Append("}");
                        }
                    }
                }

                str.Append("}");
                str.Append("</script>");

                str.Append("<table width=\"440\" cellpadding=\"1\" cellspacing=\"0\" border=\"0\">");
                str.Append("<tr>");
                str.Append("<td align=\"center\"><b>" + SepFunctions.LangText("Map to Fields") + "</b></td>");
                str.Append("</tr><tr>");
                str.Append("<td valign=\"top\" width=\"440\">" + SepFunctions.LangText("Dublicate Check") + ": ");
                str.Append("<select name=\"dubs\" class=\"form-control\">");
                str.Append("<option value=\"\">" + SepFunctions.LangText("No Dublicate Checking") + "</option>");
                if (arrDupOptions != null)
                {
                    for (var i = 0; i <= Information.UBound(arrDupOptions); i++)
                        if (!string.IsNullOrWhiteSpace(arrDupOptions[i]))
                            str.Append("<option value=\"" + arrDupOptions[i] + "\">" + arrDupOptions[i] + "</option>");
                }

                str.Append("</select></td>");
                str.Append("</tr><tr>");
                str.Append("<td valign=\"top\" width=\"440\">");

                aa = 0;

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        RS.Read();
                        for (var i = 0; i <= RS.FieldCount - 1; i++)
                        {
                            string sField = SepFunctions.openNull(RS.GetName(i));
                            if (!string.IsNullOrWhiteSpace(sField) && doNotSkip(Strings.UCase(sField), strTable))
                            {
                                aa += 1;
                                str.Append("<table border=\"0\" width=\"440\" cellpadding=\"0\" cellspacing=\"0\">");
                                str.Append("<tr>");
                                if (Strings.UCase(sField) != "CATID" && Strings.UCase(sField) != "MODULEID" && Strings.UCase(sField) != "SHIPOPTION") str.Append("<td width=\"240\">" + sField + "<td>");
                                switch (Strings.UCase(sField))
                                {
                                    case "BUSINESSID":
                                    case "LINKID":
                                    case "FAQID":
                                    case "ARTICLEID":
                                    case "EVENTID":
                                    case "PRODUCTID":
                                    case "NUSERID":
                                    case "AFFILIATEID":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" />");
                                        break;

                                    case "CATID":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" />");
                                        str.Append("<td width=\"200\" height=\"19\" colspan=\"2\">");
                                        using (var cCat = new CategoryDropdown())
                                        {
                                            using (var sw = new StringWriter())
                                            {
                                                cCat.ModuleID = SepFunctions.toInt(ModuleID.Value);
                                                cCat.ID = "input" + aa;
                                                var htw = new HtmlTextWriter(sw);
                                                cCat.RenderControl(htw);
                                                str.Append(sw);
                                                htw.Dispose();
                                            }
                                        }

                                        str.Append("</td>");
                                        break;

                                    case "APPROVED":
                                    case "ACTIVE":
                                    case "APPROVEFRIENDS":
                                    case "MALE":
                                    case "SHARED":
                                    case "EXCLUDEAFFILIATE":
                                    case "USEINVENTORY":
                                        str.Append("<td width=\"200\" height=\"19\"><select name=\"input" + aa + "\" id=\"input" + aa + "\"><option value=\"Yes\">" + SepFunctions.LangText("Yes") + "</option><option value=\"No\">" + SepFunctions.LangText("No") + "</option></select></td>");
                                        break;

                                    case "STATUS":
                                        str.Append("<td width=\"200\" height=\"19\"><select name=\"input" + aa + "\" id=\"input" + aa + "\"><option value=\"1\">" + SepFunctions.LangText("Approved") + "</option><option value=\"0\">" + SepFunctions.LangText("Not Approved") + "</option></select></td>");
                                        break;

                                    case "PORTALID":
                                        using (var cPortal = new PortalDropdown())
                                        {
                                            using (var sw = new StringWriter())
                                            {
                                                cPortal.ID = "input" + aa;
                                                var htw = new HtmlTextWriter(sw);
                                                cPortal.RenderControl(htw);
                                                str.Append(sw);
                                                htw.Dispose();
                                            }
                                        }

                                        break;

                                    case "CLASSCHANGED":
                                        str.Append("<td width=\"200\" height=\"19\">" + DateTime.Now + "</td>");
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" value=\"" + Strings.ToString(DateTime.Now) + "\" />");
                                        break;

                                    case "ACCESSCLASS":
                                        str.Append("<td width=\"200\" height=\"19\"><select name=\"input" + aa + "\" onchange=\"ChangeClass()\" class=\"form-control\">");
                                        str.Append("<option value=\"\">--- " + SepFunctions.LangText("Select a Class") + " ---</option>");
                                        using (var cmd2 = new SqlCommand("SELECT * FROM AccessClasses ORDER BY ClassName", conn))
                                        {
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                while (RS2.Read())
                                                {
                                                    str.Append("<option value=\"" + SepFunctions.openNull(RS2["ClassID"]) + "\">" + SepFunctions.openNull(RS2["ClassName"]) + "</option>");
                                                }
                                            }
                                        }

                                        str.Append("</select></td>");
                                        break;

                                    case "ACCESSKEYS":
                                        str.Append("<td width=\"200\" height=\"19\"></td>");
                                        break;

                                    case "DELETED":
                                    case "USERPOINTS":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" value=\"0\" />");
                                        break;

                                    case "LANG":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" value=\"EN\" />");
                                        break;

                                    case "SKINFOLDER":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" value=\"Default\" />");
                                        break;

                                    case "NEWSLETID":
                                    case "LETTERID":
                                        str.Append("<td width=\"200\" height=\"19\"><select name=\"input" + aa + "\" onchange=\"ChangeClass()\" class=\"form-control\" style=\"width:200px;\">");
                                        str.Append("<option value=\"0\">--- " + SepFunctions.LangText("Select a Newsletter") + " ---</option>");
                                        using (var cmd2 = new SqlCommand("SELECT * FROM Newsletters ORDER BY NewsletName", conn))
                                        {
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                while (RS2.Read())
                                                {
                                                    str.Append("<option value=\"" + SepFunctions.openNull(RS2["LetterID"]) + "\">" + SepFunctions.openNull(RS2["NewsletName"]) + "</option>");
                                                }
                                            }
                                        }

                                        str.Append("</select></td>");
                                        break;

                                    case "RECURRINGCYCLE":
                                        str.Append("<td width=\"200\" height=\"19\"><select name=\"input" + aa + "\" onchange=\"ChangeClass()\" class=\"form-control\" style=\"width:200px;\">");
                                        str.Append("<option value=\"1m\">" + SepFunctions.LangText("1 Month") + "</option>");
                                        str.Append("<option value=\"3m\">" + SepFunctions.LangText("3 Months") + "</option>");
                                        str.Append("<option value=\"6m\">" + SepFunctions.LangText("6 Months") + "</option>");
                                        str.Append("<option value=\"1y\">" + SepFunctions.LangText("1 Year") + "</option>");
                                        str.Append("</select></td>");
                                        break;

                                    case "MODULEID":
                                        break;

                                    case "SHIPOPTION":
                                        str.Append("<input type=\"hidden\" name=\"input" + aa + "\" id=\"input" + aa + "\" value=\"shipping\" />");
                                        break;

                                    default:
                                        str.Append("<td width=\"200\" height=\"19\"><script type=\"text/javascript\" language=\"JavaScript\">SelectFields('" + aa + "', '" + Strings.UCase(sField) + "')</script></td>");
                                        break;
                                }

                                str.Append("</tr>");
                                str.Append("</table>");
                            }
                        }
                    }
                }

                if (SepFunctions.toInt(ModuleID.Value) == 41)
                {
                    aa = 0;
                    str.Append("<table border=\"0\" width=\"440\" cellpadding=\"0\" cellspacing=\"0\">");
                    str.Append("<tr>");
                    str.Append("<td width=\"240\">" + SepFunctions.LangText("Image URL") + "<td>");
                    str.Append("<td width=\"200\" height=\"19\">");
                    str.Append("<select name=\"ImageURL\" class=\"form-control\">");
                    str.Append("<option value=\"\">--- " + SepFunctions.LangText("Select Field") + " ---</option>");
                    if (arrCols != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrCols); i++)
                            if (!string.IsNullOrWhiteSpace(arrCols[i]))
                            {
                                aa += 1;
                                str.Append("<option value=\"" + aa + "\">" + SepFunctions.EscQuotes(arrCols[i]) + "</option>");
                            }
                    }

                    str.Append("</select>");
                    str.Append("</td>");
                    str.Append("</tr>");
                    str.Append("</table>");
                }

                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
            }

            return Strings.ToString(str);
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    Seperator.Items[0].Text = SepFunctions.LangText("Comma");
                    Seperator.Items[1].Text = SepFunctions.LangText("Tab");
                    Seperator.Items[2].Text = SepFunctions.LangText("Semicolon");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Import Utility");
                    ModuleIDLabel.InnerText = SepFunctions.LangText("Select a module to import data into:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal to map data to:");
                    SeperatorLabel.InnerText = SepFunctions.LangText("Seperator:");
                    FileNameLabel.InnerText = SepFunctions.LangText("Select a file to import:");
                    FileNameRequired.ErrorMessage = SepFunctions.LangText("~~File to import~~ is required.");
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
            }
        }

        /// <summary>
        /// Handles the Click event of the ImportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ImportButton_Click(object sender, EventArgs e)
        {
            // var parser = new TextFieldParser(SepFunctions.GetDirValue("App_Data") + TempFileName.Value);
            // parser.TextFieldType = FieldType.Delimited;
            // parser.SetDelimiters(";");

            // var strUniqueID = "";
            // var DBFieldName = "";
            // var colName = "";
            // var colValue = "";
            // long aa = 0;
            // var insStr = "";
            // var insFields = "";
            // long colInt = 0;

            // HttpContext.Current.Server.ScriptTimeout = 3600;

            // ADM_Get_Module_Tables(SepFunctions.toInt(ModuleID.Value));

            // using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            // {
            // conn.Open();
            // while (!parser.EndOfData)
            // {
            // var row = parser.ReadFields();

            // strUniqueID = SepCommon.SepCore.Strings.ToString(SepFunctions.GetIdentity()); insStr = ""; insFields = ""; aa = 0;
            // using (var cmd = new SqlCommand("SELECT * FROM " + strTable, conn)) { SqlDataReader RS
            // = cmd.ExecuteReader(); RS.Read(); for (var i = 0; i <= RS.FieldCount - 1; i++) {
            // DBFieldName = SepFunctions.openNull(RS.GetName(i)); if
            // (!string.IsNullOrWhiteSpace(DBFieldName) && doNotSkip(Strings.UCase(DBFieldName),
            // strTable)) { colValue = ""; colName = ""; colInt = 0; aa += 1; switch
            // (Strings.UCase(DBFieldName)) { case "LETTERID": insStr += "'" + SepCommon.SepCore.Request.Item("input" + aa)
            // + "'||"; break;

            // default: try { colInt = SepFunctions.toLong(SepCommon.SepCore.Request.Item("input" + aa)) - 1; } catch { colInt =
            // 0; } if (colInt >= 0) { var datecolumn = new DataColumn(row[colInt]); colName =
            // datecolumn.ColumnName; } if (!string.IsNullOrWhiteSpace(colName)) colValue =
            // row[colInt]; switch (Strings.UCase(DBFieldName)) { case "CATID": insStr += "'" +
            // SepCommon.SepCore.Request.Item("input" + aa) + "'||"; break;

            // case "MALE": case "ACTIVE": case "APPROVED": case "USEINVENTORY": insStr += "'" +
            // SepCommon.SepCore.Strings.ToString(colValue == "Yes" || string.IsNullOrWhiteSpace(colValue) ? "1" : "0")
            // + "'||"; break;

            // case "EXCLUDEAFFILIATE": insStr += "'" + SepCommon.SepCore.Strings.ToString(colValue == "No" ? "0" :
            // "1") + "'||"; break;

            // case "ACCESSCLASS": case "VISITS": case "UNITPRICE": case "RECURRINGPRICE": case
            // "AFFILIATEUNITPRICE": case "AFFILIATERECURRINGPRICE": case "SALEPRICE": insStr += "'"
            // + SepFunctions.toLong(colValue) + "'||"; break;

            // case "PORTALID": insStr += "'" +
            // SepFunctions.toLong(SepCommon.SepCore.Request.Item("PortalID")) + "'||"; break;

            // case "ITEMWEIGHT": case "DIMH": case "DIMW": case "DIML": insStr += "'" +
            // SepFunctions.FixWord(SepCommon.SepCore.Strings.ToString(SepFunctions.toLong(colValue))) + "'||"; break;

            // case "USERNAME": if (!string.IsNullOrWhiteSpace(colValue)) insStr += "'" +
            // SepFunctions.FixWord(colValue) + "'||"; else insStr += "'" +
            // SepFunctions.FixWord(SepFunctions.Session_User_Name()) + "'||"; break;

            // case "USERID": if (!string.IsNullOrWhiteSpace(colValue)) { insStr += "'" +
            // SepFunctions.FixWord(colValue) + "'||"; } else { if (strTable == "NewslettersUsers") insStr
            // += "''||"; else insStr += "'" + SepFunctions.FixWord(SepFunctions.Session_User_ID()) + "'||"; } break;

            // case "BUSINESSID": case "LINKID": case "FAQID": case "ARTICLEID": case "EVENTID": case
            // "PRODUCTID": case "NUSERID": insStr += "'" + SepFunctions.FixWord(strUniqueID) + "'||"; break;

            // case "DATEPOSTED": case "CLASSCHANGED": if (Information.IsDate(colValue)) insStr +=
            // "'" + SepFunctions.FixWord(colValue) + "'||"; else insStr += "'" +
            // SepFunctions.FixWord(SepCommon.SepCore.Strings.ToString(DateTime.Now)) + "'||"; break;

            // case "MODULEID": insStr += "'" + ModuleID.Value + "'||"; break;

            // case "AFFILIATEID": insStr += "'" + SepFunctions.GetIdentity() + "'||"; break;

            // case "DELETED": case "REFERRALID": case "USERPOINTS": insStr += "'0'||"; break;

            // case "STATE": insStr += "'" + SepFunctions.FixWord(colValue) + "'||"; break;

            // case "SHIPOPTION": insStr += "'shipping'||"; break;

            // default: insStr += "'" + SepFunctions.FixWord(colValue) + "'||"; break; } break; }
            // insFields += DBFieldName + "||"; } } }

            // try
            // {
            // using (var cmd = new SqlCommand("INSERT INTO " + strTable + " (" + Strings.Left(Strings.Replace(insFields, "||", ","), Strings.Len(Strings.Replace(insFields, "||", ",")) - 1) + ") VALUES(" + Strings.Left(Strings.Replace(insStr, "||", ","), Strings.Len(Strings.Replace(insStr, "||", ",")) - 1) + ")", conn))
            // {
            // cmd.ExecuteNonQuery();
            // }
            // }
            // catch
            // {
            // ImportFinished.Text += "ERROR EXECUTING:<br/>\"INSERT INTO " + strTable + " (" + Strings.Left(Strings.Replace(insFields, "||", ","), Strings.Len(Strings.Replace(insFields, "||", ",")) - 1) + ") VALUES(" + Strings.Left(Strings.Replace(insStr, "||", ","), Strings.Len(Strings.Replace(insStr, "||", ",")) - 1) + ")\"<br/><br/>";
            // }
            // }
            // }
            // parser.Dispose();
            if (File.Exists(SepFunctions.GetDirValue("App_Data") + TempFileName.Value)) File.Delete(SepFunctions.GetDirValue("App_Data") + TempFileName.Value);

            ImportFinished.InnerHtml += SepFunctions.LangText("Data Successfully Imported");
        }

        /// <summary>
        /// Handles the Click event of the NextButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void NextButton_Click(object sender, EventArgs e)
        {
            var sFileExt = Strings.LCase(Path.GetExtension(FileName.PostedFile.FileName));

            switch (sFileExt)
            {
                case ".csv":
                case ".txt":
                    var SaveFileName = SepFunctions.GetIdentity() + sFileExt;
                    var SaveLocation = SepFunctions.GetDirValue("App_Data") + SaveFileName;
                    FileName.PostedFile.SaveAs(SaveLocation);
                    Step1.Visible = false;
                    Step2.Visible = true;
                    MapFieldsDiv.InnerHtml = ADM_Import_Map_Fields(SaveFileName);
                    TempFileName.Value = SaveFileName;
                    break;

                default:
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid file format. (Only txt and csv files are supported)") + "</div>";
                    break;
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
            TranslatePage();

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAdvance")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAdvance"), false) == false)
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
                if (SepFunctions.ModuleActivated(35)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Articles"), "35"));
                if (SepFunctions.ModuleActivated(20)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Business Directory"), "20"));
                if (SepFunctions.ModuleActivated(46)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Event Calendar"), "46"));
                if (SepFunctions.ModuleActivated(9)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("FAQ"), "9"));
                if (SepFunctions.ModuleActivated(12)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Forums"), "12"));
                if (SepFunctions.ModuleActivated(19)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Link Directory"), "19"));
                ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Members"), "986"));
                if (SepFunctions.ModuleActivated(23)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("News"), "23"));
                ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Newsletters"), "24"));
                if (SepFunctions.ModuleActivated(41)) ModuleID.Items.Insert(ModuleID.Items.Count, new ListItem(SepFunctions.LangText("Shopping Mall"), "41"));
            }
        }
    }
}