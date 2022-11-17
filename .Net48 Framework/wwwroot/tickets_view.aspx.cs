// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="tickets_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class tickets_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class tickets_view : Page
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
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("TicketID")))
                {
                    var cCRM = new CRM();
                    var rootNodes = cCRM.Get_Ticket(SepCommon.SepCore.Request.Item("TicketID"));

                    if (cCRM.Software_Enabled() == CRM.SoftwareEnabled.SmarterTrack)
                    {
                        Subject.InnerHtml = "<br/>" + Strings.Replace(rootNodes[0].InnerText, "Subject=", string.Empty);
                        DateReceived.InnerHtml = "<br/>" + Strings.Replace(rootNodes[2].InnerText, "DateReceivedUTC=", string.Empty);
                        switch (Strings.Replace(rootNodes[4].InnerText, "TicketStatusID=", string.Empty))
                        {
                            case "1":
                                Status.InnerHtml = "<br/>Waiting";
                                break;

                            case "2":
                                Status.InnerHtml = "<br/>Closed";
                                break;

                            case "3":
                                Status.InnerHtml = "<br/>Closed and Locked";
                                break;

                            default:
                                Status.InnerHtml = "<br/>Active";
                                break;
                        }

                        TicketNumber.InnerHtml = "<br/>" + SepCommon.SepCore.Request.Item("TicketID");
                        MessageBody.InnerHtml = "<br/>" + Strings.Replace(rootNodes[1].InnerText, "BodyHTML=", string.Empty);
                    }
                    else if (cCRM.Software_Enabled() == CRM.SoftwareEnabled.SugarCRM || cCRM.Software_Enabled() == CRM.SoftwareEnabled.SuiteCRM)
                    {
                        Subject.InnerHtml = "<br/>" + SepFunctions.ParseXML("name", rootNodes[0].OuterXml);
                        DateReceived.InnerHtml = "<br/>" + SepFunctions.ParseXML("date_entered", rootNodes[0].OuterXml);
                        Status.InnerHtml = "<br/>" + SepFunctions.ParseXML("status", rootNodes[0].OuterXml);
                        TicketNumber.InnerHtml = "<br/>" + SepFunctions.ParseXML("case_number", rootNodes[0].OuterXml);
                        MessageBody.InnerHtml = "<br/>" + SepFunctions.ParseXML("description", rootNodes[0].OuterXml);
                    }

                    cCRM.Dispose();
                }
                else
                {
                    ContactDiv.Visible = false;
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Error loading ticket. (" + SepFunctions.LangText("Ticket does not exist") + ")</div>";
                }
            }
            catch
            {
                ContactDiv.Visible = false;
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Error loading ticket. (" + SepFunctions.LangText("Ticket does not exist") + ")</div>";
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