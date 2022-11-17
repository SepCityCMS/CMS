// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="polls_display.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class polls_display.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class polls_display : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The pn choices
        /// </summary>
        private Panel pnChoices;

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
                if (pnChoices != null)
                {
                    pnChoices.Dispose();
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
            var str = new StringBuilder();

            GlobalVars.ModuleID = 25;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "PollsEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("PollsAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PollID")))
            {
                long aCount = 0;

                var jPolls = SepCommon.DAL.Polls.Poll_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PollID")));

                if (jPolls.PollID == 0)
                {
                    PollQuestion.InnerHtml = SepFunctions.LangText("Poll Error");
                    PollOptions.InnerHtml = SepFunctions.LangText("~~Poll~~ does not exist.");
                    resultsLink.Visible = false;
                }
                else
                {
                    resultsLink.NavigateUrl = "javascript:showResults('','" + SepCommon.SepCore.Request.Item("PollID") + "');";
                    Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jPolls.Question);
                    PollQuestion.InnerHtml = jPolls.Question;

                    // Show Images
                    PollPHoto.ContentUniqueID = Strings.ToString(jPolls.PollID);
                    PollPHoto.ModuleID = GlobalVars.ModuleID;

                    // Dynamic TextBox Panel
                    pnChoices = new Panel
                    {
                        ID = "pnChoices"
                    };
                    PollOptions.Controls.Add(pnChoices);

                    string[] arrOptionIds = Strings.Split(jPolls.OptionIds, "||");
                    string[] arrOptionValues = Strings.Split(jPolls.OptionValues, "||");
                    if (arrOptionIds != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrOptionIds); i++)
                        {
                            aCount += 1;

                            using (var para = new HtmlGenericControl("p"))
                            {
                                using (var voteLink = new HyperLink())
                                {
                                    voteLink.ID = arrOptionIds[i];
                                    voteLink.Text = arrOptionValues[i];
                                    if (SepFunctions.CompareKeys(SepFunctions.Security("PollsVote")))
                                        voteLink.NavigateUrl = "javascript:castVote('" + jPolls.PollID + "', '" + arrOptionIds[i] + "', '" + SepFunctions.Get_Portal_ID() + "', '');";
                                    else
                                        voteLink.NavigateUrl = sInstallFolder + "login.aspx";
                                    para.Controls.Add(voteLink);
                                }

                                pnChoices.Controls.Add(para);
                            }
                        }
                    }

                    long acount = 0;

                    str.AppendLine("<script type=\"text/javascript\">");
                    str.AppendLine("Morris.Bar({");
                    str.AppendLine("element: 'PollResults',");
                    str.AppendLine("xkey: 'option',");
                    str.AppendLine("ykeys: ['value'],");
                    str.AppendLine("labels: ['Count'],");
                    str.AppendLine("xLabelAngle: 25,");
                    str.AppendLine("hideHover: 'auto',");
                    str.AppendLine("lineColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],");
                    str.AppendLine("resize: false,");
                    str.AppendLine("data: [");
                    var sResults = SepCommon.DAL.Polls.PollResults(jPolls.PollID);

                    for (var i = 0; i <= sResults.Count - 1; i++)
                    {
                        acount += 1;
                        if (acount > 1)
                            str.AppendLine(",");
                        str.AppendLine("{ option: unescape('" + SepFunctions.EscQuotes(sResults[i].PollOption) + "'), value: " + SepFunctions.toInt(Strings.ToString(sResults[i].Percentage)) + " }");
                    }

                    str.AppendLine("]");
                    str.AppendLine("});");
                    str.AppendLine("</script>");

                    TrendData.InnerHtml = Strings.ToString(str);
                }
            }
            else
            {
                PollQuestion.InnerHtml = SepFunctions.LangText("Invalid Poll");
                PollOptions.InnerHtml = SepFunctions.LangText("Poll does not exist on our site or has expired.");
                resultsLink.Visible = false;
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