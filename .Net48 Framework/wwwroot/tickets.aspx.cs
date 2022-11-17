// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="tickets.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class tickets.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class tickets : Page
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
            GlobalVars.ModuleID = 67;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            try
            {
                var output = new StringBuilder();
                var cCRM = new CRM();

                var rootNodes = cCRM.List_Tickets();

                var ticketIdField = string.Empty;
                var subjectField = string.Empty;
                var lastActivityField = string.Empty;
                var statusField = string.Empty;
                var ticketNumberField = string.Empty;

                switch (cCRM.Software_Enabled())
                {
                    case CRM.SoftwareEnabled.SmarterTrack:
                        ticketIdField = "TicketNumber";
                        subjectField = "Subject";
                        lastActivityField = "LastReplyDateUtc";
                        statusField = "IsOpen";
                        ticketNumberField = "TicketNumber";
                        break;

                    case CRM.SoftwareEnabled.SugarCRM:
                        ticketIdField = "id";
                        subjectField = "name";
                        lastActivityField = "date_modified";
                        statusField = "status";
                        ticketNumberField = "case_number";
                        break;

                    case CRM.SoftwareEnabled.SuiteCRM:
                        ticketIdField = "id";
                        subjectField = "name";
                        lastActivityField = "date_modified";
                        statusField = "status";
                        ticketNumberField = "case_number";
                        break;

                    case CRM.SoftwareEnabled.WHMCS:
                        break;

                    case CRM.SoftwareEnabled.Disabled:
                        break;
                }

                if (rootNodes.Count > 0)
                {
                    output.AppendLine("<table class=\"GridViewStyle table table-striped table-bordered\" id=\"ManageGridView\">");
                    output.AppendLine("<tr>");
                    output.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Subject") + "</th>");
                    output.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Last Activity") + "</th>");
                    output.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Status") + "</th>");
                    output.AppendLine("<th scope=\"col\">" + SepFunctions.LangText("Ticket Number") + "</th>");
                    output.AppendLine("</tr>");
                    for (var i = 0; i <= rootNodes.Count - 1; i++)
                        try
                        {
                            var sValue = SepFunctions.ParseXML(subjectField, rootNodes[i].OuterXml);
                            if (!string.IsNullOrWhiteSpace(sValue))
                            {
                                output.AppendLine("<tr>");
                                output.AppendLine("<td><a href=\"tickets_view.aspx?TicketID=" + SepFunctions.ParseXML(ticketIdField, rootNodes[i].OuterXml) + "\">" + sValue + "</a></td>");
                                output.AppendLine("<td>" + SepFunctions.ParseXML(lastActivityField, rootNodes[i].OuterXml) + "</td>");
                                if (statusField == "IsOpen")
                                {
                                    if (SepFunctions.ParseXML(statusField, rootNodes[i].OuterXml) == "true")
                                        output.AppendLine("<td>" + SepFunctions.LangText("Opened") + "</td>");
                                    else
                                        output.AppendLine("<td>" + SepFunctions.LangText("Closed") + "</td>");
                                }
                                else
                                {
                                    output.AppendLine("<td>" + SepFunctions.ParseXML(statusField, rootNodes[i].OuterXml) + "</td>");
                                }

                                output.AppendLine("<td>" + SepFunctions.ParseXML(ticketNumberField, rootNodes[i].OuterXml) + "</td>");
                                output.AppendLine("</tr>");
                            }
                        }
                        catch
                        {
                        }

                    output.AppendLine("</table>");
                    KBContent.InnerHtml = Strings.ToString(output);
                }

                cCRM.Dispose();
            }
            catch
            {
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
}