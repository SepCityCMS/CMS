// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="custom_reports_print.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;

    /// <summary>
    /// Class custom_reports_print.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class custom_reports_print : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The s XML
        /// </summary>
        private string sXML = string.Empty;

        /// <summary>
        /// Adms the select additional array.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string ADM_Select_Additional_Array()
        {
            var arrValues = new string[4];

            arrValues[0] = "{NonPaidSales}";
            arrValues[1] = "{PaidSales}";
            arrValues[2] = "{TotalLogins}";
            arrValues[3] = "{TotalActivities}";

            return Strings.Join(arrValues, ",");
        }

        /// <summary>
        /// Adms the SQL replace.
        /// </summary>
        /// <param name="sSQL">The s SQL.</param>
        /// <returns>System.String.</returns>
        public static string ADM_SQL_Replace(string sSQL)
        {
            var str = sSQL;
            string[] arrAdditional = Strings.Split(ADM_Select_Additional_Array(), ",");
            if (arrAdditional != null)
            {
                for (var i = 0; i <= Information.UBound(arrAdditional); i++)
                {
                    str = Strings.Replace(str, "," + arrAdditional[i], string.Empty);
                    str = Strings.Replace(str, arrAdditional[i] + ",", string.Empty);
                }
            }

            return str;
        }

        /// <summary>
        /// Adms the replace fields.
        /// </summary>
        /// <param name="myData3">My data3.</param>
        /// <param name="sFieldName">Name of the s field.</param>
        /// <param name="conn">The connection.</param>
        /// <returns>System.String.</returns>
        public string ADM_Replace_Fields(SqlDataReader myData3, string sFieldName, SqlConnection conn)
        {
            var str = string.Empty;
            var iValue = 1;

            sFieldName = Strings.Replace(Strings.Replace(sFieldName, "[[", string.Empty), "]]", string.Empty);

            switch (Strings.LCase(sFieldName))
            {
                case "accessclass":
                    using (var cmd2 = new SqlCommand("SELECT * FROM AccessClasses WHERE ClassID='" + SepFunctions.openNull(myData3["AccessCkass"], true) + "'", conn))
                    {
                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                        {
                            if (RS2.HasRows)
                            {
                                RS2.Read();
                                str = SepFunctions.openNull(RS2["ClassName"]);
                            }
                        }
                    }

                    break;

                case "accesskeys":
                    string[] arrKeys = Strings.Split(SepFunctions.openNull(myData3["AccessKeys"]), ",");
                    str = string.Empty;
                    if (arrKeys != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrKeys); i++)
                            using (var cmd2 = new SqlCommand("SELECT * FROM AccessKeys WHERE KeyID='" + SepFunctions.FixWord(Strings.Replace(arrKeys[i], "|", string.Empty)) + "'", conn))
                            {
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    if (RS2.HasRows)
                                    {
                                        RS2.Read();
                                        str += SepFunctions.openNull(RS2["KeyName"]) + Environment.NewLine;
                                    }
                                }
                            }
                    }

                    break;

                case "portalid":
                    if (SepFunctions.openNull(myData3["PortalID"]) == "0")
                        str = SepFunctions.LangText("Main Portal");
                    else
                        using (var cmd2 = new SqlCommand("SELECT PortalTitle FROM Portals WHERE PortalID='" + SepFunctions.openNull(myData3["PortalID"], true) + "'", conn))
                        {
                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                            {
                                if (RS2.HasRows)
                                {
                                    RS2.Read();
                                    str = SepFunctions.openNull(RS2["PortalTitle"]);
                                }
                            }
                        }

                    break;

                case "referralid":
                    if (SepFunctions.openNull(myData3["ReferralID"]) == "0" || string.IsNullOrWhiteSpace(SepFunctions.openNull(myData3["ReferralID"])))
                        str = SepFunctions.LangText("N/A");
                    else
                        using (var cmd2 = new SqlCommand("SELECT Username FROM Members WHERE AffiliateID='" + SepFunctions.openNull(myData3["ReferralID"], true) + "'", conn))
                        {
                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                            {
                                if (RS2.HasRows)
                                {
                                    RS2.Read();
                                    str = SepFunctions.openNull(RS2["Username"]);
                                }
                            }
                        }

                    break;

                case "unitprice":
                case "recurringprice":
                case "unitprice1":
                case "recurringprice1":
                    str = SepFunctions.Format_Currency(SepFunctions.openNull(myData3[sFieldName]));
                    break;

                case "{nonpaidsales}":
                    using (var cmd2 = new SqlCommand("SELECT Sum(Cast(UnitPrice As Double) + Cast(RecurringPrice As Double)) AS TotalAmount AS Counter FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND INV.UserID='" + SepFunctions.openNull(myData3["UserID"], true) + "' AND INV.Status='0' AND INV.inCart='0'", conn))
                    {
                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                        {
                            if (RS2.HasRows)
                            {
                                RS2.Read();
                                str = SepFunctions.Format_Currency(SepFunctions.openNull(RS2["TotalAmount"]));
                            }
                            else
                            {
                                str = SepFunctions.Format_Currency("0");
                            }
                        }
                    }

                    break;

                case "{paidsales}":
                    using (var cmd2 = new SqlCommand("SELECT Sum(Cast(UnitPrice As Double) + Cast(RecurringPrice As Double)) AS TotalAmount AS Counter FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND INV.UserID='" + SepFunctions.openNull(myData3["UserID"], true) + "' AND INV.Status > '0' AND INV.inCart='0'", conn))
                    {
                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                        {
                            if (RS2.HasRows)
                            {
                                RS2.Read();
                                str = SepFunctions.Format_Currency(SepFunctions.openNull(RS2["TotalAmount"]));
                            }
                            else
                            {
                                str = SepFunctions.Format_Currency("0");
                            }
                        }
                    }

                    break;

                case "{totallogins}":
                    using (var cmd2 = new SqlCommand("SELECT Count(ActivityID) AS Counter FROM Activities WHERE UserID='" + SepFunctions.openNull(myData3["UserID"], true) + "' AND ActType='LOGIN'", conn))
                    {
                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                        {
                            if (RS2.HasRows)
                            {
                                RS2.Read();
                                str = SepFunctions.Format_Currency(SepFunctions.openNull(RS2["Counter"]));
                            }
                            else
                            {
                                str = SepFunctions.Format_Currency("0");
                            }
                        }
                    }

                    break;

                case "{totalactivities}":
                    using (var cmd2 = new SqlCommand("SELECT Count(ActivityID) AS Counter FROM Activities WHERE UserID='" + SepFunctions.openNull(myData3["UserID"], true) + "'", conn))
                    {
                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                        {
                            if (RS2.HasRows)
                            {
                                RS2.Read();
                                str = SepFunctions.Format_Currency(SepFunctions.openNull(RS2["Counter"]));
                            }
                            else
                            {
                                str = SepFunctions.Format_Currency("0");
                            }
                        }
                    }

                    break;

                default:
                    str = SepFunctions.openNull(myData3[sFieldName]);
                    break;
            }

            if (SepCommon.SepCore.Request.Item("DoAction") == "Print") str = Strings.Replace(str, Environment.NewLine, "<br/>");

            if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML(str, sXML)))
            {
                string xOldXML = "<" + Strings.UCase(str) + ">" + Environment.NewLine;
                xOldXML += "<VALUE>" + SepFunctions.toLong(SepFunctions.ParseXML("VALUE", SepFunctions.ParseXML(str, sXML))) + "</VALUE>" + Environment.NewLine;
                xOldXML += "</" + Strings.UCase(str) + ">" + Environment.NewLine;

                iValue = Convert.ToInt32(SepFunctions.toDouble(SepFunctions.ParseXML("VALUE", SepFunctions.ParseXML(str, sXML)))) + 1;

                string sNewXML = "<" + Strings.UCase(str) + ">" + Environment.NewLine;
                sNewXML += "<VALUE>" + Strings.ToString(iValue) + "</VALUE>" + Environment.NewLine;
                sNewXML += "</" + Strings.UCase(str) + ">" + Environment.NewLine;

                sXML = Strings.Replace(sXML, xOldXML, sNewXML);
            }
            else
            {
                sXML += "<" + Strings.UCase(str) + ">" + Environment.NewLine;
                sXML += "<VALUE>" + Strings.ToString(iValue) + "</VALUE>" + Environment.NewLine;
                sXML += "</" + Strings.UCase(str) + ">" + Environment.NewLine;
            }

            return str;
        }

        /// <summary>
        /// Adms the report print.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ADM_Report_Print()
        {
            var str = new StringBuilder();
            long iRecordCount = 0;
            string tr1;
            string tr2;
            string td2;
            string td1;

            string starttable;
            string endtable;
            if (SepCommon.SepCore.Request.Item("DoAction") == "Print")
            {
                starttable = "<table width=\"640\">";
                endtable = "</table>";
                tr1 = "<tr>";
                tr2 = "</tr>";
                td1 = "<td valign=\"top\">";
                td2 = "</td>";
            }
            else
            {
                starttable = string.Empty;
                endtable = string.Empty;
                tr1 = string.Empty;
                tr2 = string.Empty;
                td1 = string.Empty;
                td2 = ",";
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Scripts WHERE ScriptType='REPORT' AND ID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("ReportID")) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows == false)
                        {
                            str.Append("<p class=\"errorMsg\">" + SepFunctions.LangText("No valid report found") + "</p>");
                            str.Append("<p align=\"center\"><input type=\"button\" onclick=\"window.close()\" value=\"" + SepFunctions.LangText("Close") + "\" /></p>");
                        }
                        else
                        {
                            RS.Read();
                            string GetScriptText = SepFunctions.openNull(RS["ScriptText"]);
                            if (string.IsNullOrWhiteSpace(SepFunctions.ParseXML("SQLSTATEMENT", GetScriptText)))
                            {
                                str.Append("<p class=\"errorMsg\">" + SepFunctions.LangText("SQL Statement is missing from the report.") + "</p>");
                                str.Append("<p align=\"center\"><input type=\"button\" onclick=\"window.close()\" value=\"" + SepFunctions.LangText("Close") + "\" /></p>");
                                return Strings.ToString(str);
                            }

                            str.Append(starttable);
                            str.Append(tr1);
                            for (var i = 1; i <= 6; i++)
                                if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("HEAD" + i, GetScriptText)))
                                    str.Append(td1 + SepFunctions.ParseXML("HEAD" + i, GetScriptText) + td2);
                            str.Append(tr2 + tr1);
                            for (var i = 7; i <= 12; i++)
                                if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("HEAD" + i, GetScriptText)))
                                    str.Append(td1 + SepFunctions.ParseXML("HEAD" + i, GetScriptText) + td2);
                            str.Append(tr2 + tr1);
                            for (var i = 13; i <= 18; i++)
                                if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("HEAD" + i, GetScriptText)))
                                    str.Append(td1 + SepFunctions.ParseXML("HEAD" + i, GetScriptText) + td2);
                            str.Append(tr2 + Environment.NewLine);
                            if (SepCommon.SepCore.Request.Item("DoAction") == "Print") str.Append("<tr><td colspan=\"6\"><hr height=\"1\" width=\"100%\"/></td></tr>");

                            using (var cmd2 = new SqlCommand(ADM_SQL_Replace(SepFunctions.HTMLDecode(SepFunctions.ParseXML("SQLSTATEMENT", GetScriptText))), conn))
                            {
                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                {
                                    while (RS2.Read())
                                    {
                                        str.Append(tr1);
                                        for (var i = 1; i <= 6; i++)
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("BODY" + i, GetScriptText)))
                                                str.Append(td1 + ADM_Replace_Fields(RS2, SepFunctions.ParseXML("BODY" + i, GetScriptText), conn) + td2);
                                        str.Append(tr2 + tr1);
                                        for (var i = 7; i <= 12; i++)
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("BODY" + i, GetScriptText)))
                                                str.Append(td1 + ADM_Replace_Fields(RS2, SepFunctions.ParseXML("BODY" + i, GetScriptText), conn) + td2);
                                        str.Append(tr2 + tr1);
                                        for (var i = 13; i <= 18; i++)
                                            if (!string.IsNullOrWhiteSpace(SepFunctions.ParseXML("BODY" + i, GetScriptText)))
                                                str.Append(td1 + ADM_Replace_Fields(RS2, SepFunctions.ParseXML("BODY" + i, GetScriptText), conn) + td2);
                                        str.Append(tr2);
                                        str.Append(Environment.NewLine);
                                        if (SepCommon.SepCore.Request.Item("DoAction") == "Print") str.Append("<tr><td colspan=\"6\"><hr height=\"1\" width=\"100%\"/></td></tr>");
                                        iRecordCount += 1;
                                    }
                                }
                            }

                            str.Append(endtable);

                            if (SepCommon.SepCore.Request.Item("DoAction") == "Print" && SepFunctions.toInt(SepCommon.SepCore.Request.Item("ShowTotals")) == 1)
                            {
                                str.Append("<table width=\"250\">");
                                str.Append("<tr><td width=\"200\">" + SepFunctions.LangText("Total Records:") + "</td><td width=\"50\">" + iRecordCount + "</td></tr>");
                                string[] arrXMLLines = Strings.Split(sXML, Environment.NewLine);
                                if (arrXMLLines != null)
                                {
                                    for (var i = 1; i <= Information.UBound(arrXMLLines); i++)
                                    {
                                        string sFieldName = string.Empty;
                                        if (!string.IsNullOrWhiteSpace(arrXMLLines[i]))
                                            if (Strings.InStr(arrXMLLines[i], "<VALUE>") == 0 && Strings.InStr(arrXMLLines[i], "</") == 0)
                                                sFieldName = Strings.Mid(arrXMLLines[i], 2, Strings.Len(arrXMLLines[i]) - 2);
                                        if (!string.IsNullOrWhiteSpace(sFieldName)) str.Append("<tr><td>" + sFieldName + "</td><td>" + SepFunctions.ParseXML("VALUE", SepFunctions.ParseXML(sFieldName, sXML)) + "</td></tr>");
                                    }
                                }

                                str.Append("</table>");
                            }
                        }
                    }
                }
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
            if (SepCommon.SepCore.Request.Item("DoAction") != "Print")
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("ContentType", "text/plain");
                Response.AddHeader("content-disposition", "attachment;filename=export.csv");

                Response.Write(ADM_Report_Print());

                Response.End();
            }
            else
            {
                ReportText.InnerHtml = ADM_Report_Print();
            }
        }
    }
}