// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="polls_modify.aspx.cs" company="SepCity, Inc.">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class polls_modify.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class polls_modify : Page
    {
        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// The pn choices
        /// </summary>
        private Panel pnChoices;

        /// <summary>
        /// Adds the choice.
        /// </summary>
        public void Add_Choice()
        {
            var cnt = FindOccurence("txtChoice");
            var iId = Strings.ToString(cnt + 1);

            using (var para = new HtmlGenericControl("p"))
            {
                using (var ChoiceLabel = new Label())
                {
                    ChoiceLabel.ID = "lblChoice" + iId + "Label";
                    ChoiceLabel.AssociatedControlID = "txtChoice" + iId;
                    ChoiceLabel.Text = "Choice " + iId;
                    para.Controls.Add(ChoiceLabel);
                }

                using (var ChoiceName = new TextBox())
                {
                    ChoiceName.ID = "txtChoice" + iId;
                    ChoiceName.CssClass = "form-control";
                    ChoiceName.ClientIDMode = ClientIDMode.Static;
                    para.Controls.Add(ChoiceName);
                }

                using (var OptionID = new HiddenField())
                {
                    OptionID.ID = "hdnChoiceID" + iId;
                    OptionID.ClientIDMode = ClientIDMode.Static;
                    OptionID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    para.Controls.Add(OptionID);
                }

                pnChoices.Controls.Add(para);
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
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Add Poll");
                    PollQuestionLabel.InnerText = SepFunctions.LangText("Poll Question:");
                    PortalLabel.InnerText = SepFunctions.LangText("Portal:");
                    StartDateLabel.InnerText = SepFunctions.LangText("Start Date:");
                    EndDateLabel.InnerText = SepFunctions.LangText("End Date:");
                    PollQuestionRequired.ErrorMessage = SepFunctions.LangText("~~Poll Question~~ is required.");
                    SaveButton.InnerText = SepFunctions.LangText("Save");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddChoice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnAddChoice_Click(object sender, EventArgs e)
        {
            Add_Choice();
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
            TranslatePage();

            GlobalVars.ModuleID = 25;

            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("PollsAdmin")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PollsAdmin"), true) == false)
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

            if (SepFunctions.Setup(60, "PortalsEnable") != "Enable" || SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), false) == false)
            {
                PortalsRow.Visible = false;
                Portal.Text = "|" + SepFunctions.Get_Portal_ID() + "|";
            }

            // Dynamic TextBox Panel
            pnChoices = new Panel
            {
                ID = "pnChoices"
            };
            ModFieldset.Controls.Add(pnChoices);

            // Button To add TextBoxes
            using (var btnAddChoice = new Button())
            {
                btnAddChoice.ID = "btnAddChoice";
                btnAddChoice.Text = "Add Choice";
                btnAddChoice.Click += btnAddChoice_Click;
                ModFieldset.Controls.Add(btnAddChoice);
            }

            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PollID")))
            {
                string[] arrOptionIds = null;
                string[] arrOptionValues = null;
                long aCount = 0;

                var jPolls = SepCommon.DAL.Polls.Poll_Get(SepFunctions.toLong(SepCommon.SepCore.Request.Item("PollID")));

                if (jPolls.PollID == 0)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Poll~~ does not exist.") + "</div>";
                    ModFieldset.Visible = false;
                }
                else
                {
                    ModifyLegend.InnerText = SepFunctions.LangText("Edit Poll");
                    PollID.Value = SepCommon.SepCore.Request.Item("PollID");
                    Photo.ContentID = SepCommon.SepCore.Request.Item("PollID");
                    PollQuestion.Value = jPolls.Question;
                    StartDate.Value = jPolls.StartDate.ToShortDateString();
                    EndDate.Value = jPolls.EndDate.ToShortDateString();

                    arrOptionIds = Strings.Split(jPolls.OptionIds, "||");
                    arrOptionValues = Strings.Split(jPolls.OptionValues, "||");
                    if (arrOptionIds != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrOptionIds); i++)
                        {
                            aCount += 1;

                            var para = new HtmlGenericControl("p");

                            var ChoiceLabel = new Label
                            {
                                ID = "lblChoice" + aCount + "Label",
                                AssociatedControlID = "txtChoice" + aCount,
                                Text = "Choice " + aCount
                            };
                            para.Controls.Add(ChoiceLabel);

                            var ChoiceName = new TextBox
                            {
                                ID = "txtChoice" + aCount,
                                CssClass = "form-control",
                                ClientIDMode = ClientIDMode.Static,
                                Text = arrOptionValues[i]
                            };
                            para.Controls.Add(ChoiceName);

                            var OptionID = new HiddenField
                            {
                                ID = "hdnChoiceID" + aCount,
                                ClientIDMode = ClientIDMode.Static,
                                Value = arrOptionIds[i]
                            };
                            para.Controls.Add(OptionID);

                            pnChoices.Controls.Add(para);
                        }
                    }
                }
            }
            else
            {
                if (IsPostBack)
                {
                    RecreateControls();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(PollID.Value)) PollID.Value = Strings.ToString(SepFunctions.GetIdentity());
                    StartDate.Value = Strings.FormatDateTime(DateTime.Today, Strings.DateNamedFormat.ShortDate);
                    EndDate.Value = Strings.FormatDateTime(DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, DateTime.Today), Strings.DateNamedFormat.ShortDate);

                    var para = new HtmlGenericControl("p");

                    var ChoiceLabel = new Label
                    {
                        ID = "lblChoice1Label",
                        AssociatedControlID = "txtChoice1",
                        Text = "Choice 1"
                    };
                    para.Controls.Add(ChoiceLabel);

                    var ChoiceName = new TextBox
                    {
                        ID = "txtChoice1",
                        CssClass = "form-control",
                        ClientIDMode = ClientIDMode.Static
                    };
                    para.Controls.Add(ChoiceName);

                    var OptionID = new HiddenField
                    {
                        ID = "hdnChoiceID1",
                        ClientIDMode = ClientIDMode.Static,
                        Value = Strings.ToString(SepFunctions.GetIdentity())
                    };
                    para.Controls.Add(OptionID);

                    pnChoices.Controls.Add(para);

                    para = new HtmlGenericControl("p");

                    ChoiceLabel = new Label
                    {
                        ID = "lblChoice2Label",
                        AssociatedControlID = "txtChoice2",
                        Text = "Choice 2"
                    };
                    para.Controls.Add(ChoiceLabel);

                    ChoiceName = new TextBox
                    {
                        ID = "txtChoice2",
                        CssClass = "form-control",
                        ClientIDMode = ClientIDMode.Static
                    };
                    para.Controls.Add(ChoiceName);

                    OptionID = new HiddenField
                    {
                        ID = "hdnChoiceID2",
                        ClientIDMode = ClientIDMode.Static,
                        Value = Strings.ToString(SepFunctions.GetIdentity())
                    };
                    para.Controls.Add(OptionID);

                    pnChoices.Controls.Add(para);
                }

                Photo.ContentID = PollID.Value;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var OptionIds = string.Empty;
            var OptionValues = string.Empty;

            var ctrls = Strings.ToString(Request.Form).Split('&');
            long aCount = 0;

            // Get Option Values
            var cnt = FindOccurence("txtChoice");

            if (cnt > 0)
                for (var k = 1; k <= cnt; k++)
                    for (var i = 0; i <= ctrls.Length - 1; i++)
                        if (ctrls[i].Contains("txtChoice" + k))
                        {
                            var ctrlValue = ctrls[i].Split('=')[1];

                            if (!string.IsNullOrWhiteSpace(ctrlValue))
                            {
                                aCount += 1;
                                if (aCount > 1)
                                    OptionValues += "||";
                                OptionValues += Server.UrlDecode(ctrlValue);
                            }

                            break;
                        }

            // Get Option Ids
            aCount = 0;
            cnt = FindOccurence("hdnChoiceID");

            if (cnt > 0)
                for (var k = 1; k <= cnt; k++)
                    for (var i = 0; i <= ctrls.Length - 1; i++)
                        if (ctrls[i].Contains("hdnChoiceID" + k))
                        {
                            var ctrlValue = ctrls[i].Split('=')[1];

                            if (!string.IsNullOrWhiteSpace(ctrlValue))
                            {
                                aCount += 1;
                                if (aCount > 1)
                                    OptionIds += "||";
                                OptionIds += ctrlValue;
                            }

                            break;
                        }

            if (string.IsNullOrWhiteSpace(OptionIds))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must create at lease one poll option.") + "</div>";
                return;
            }

            var intReturn = SepCommon.DAL.Polls.Poll_Save(SepFunctions.toLong(PollID.Value), SepFunctions.Session_User_ID(), PollQuestion.Value, SepFunctions.toDate(StartDate.Value), SepFunctions.toDate(EndDate.Value), Portal.Text, OptionIds, OptionValues);

            ErrorMessage.InnerHtml = Globals.GetSaveMessage(Master, GlobalVars.ModuleID, intReturn, string.Empty);
        }

        /// <summary>
        /// Finds the occurence.
        /// </summary>
        /// <param name="substr">The substr.</param>
        /// <returns>System.Int32.</returns>
        private int FindOccurence(string substr)
        {
            var reqstr = Strings.ToString(Request.Form);
            return (reqstr.Length - reqstr.Replace(substr, string.Empty).Length) / substr.Length;
        }

        /// <summary>
        /// Recreates the controls.
        /// </summary>
        private void RecreateControls()
        {
            var ctrls = Strings.ToString(Request.Form).Split('&');
            var cnt = FindOccurence("txtChoice");
            long iId = 0;

            if (cnt > 0)
                for (var k = 1; k <= cnt; k++)
                    for (var i = 0; i <= ctrls.Length - 1; i++)
                        if (ctrls[i].Contains("txtChoice" + k))
                        {
                            var ctrlValue = ctrls[i].Split('=')[1];

                            // Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            using (var para = new HtmlGenericControl("p"))
                            {
                                iId += 1;

                                using (var ChoiceLabel = new Label())
                                {
                                    ChoiceLabel.ID = "lblChoice" + iId + "Label";
                                    ChoiceLabel.AssociatedControlID = "txtChoice" + iId;
                                    ChoiceLabel.Text = "Choice " + iId;
                                    para.Controls.Add(ChoiceLabel);
                                }

                                using (var ChoiceName = new TextBox())
                                {
                                    ChoiceName.ID = "txtChoice" + iId;
                                    ChoiceName.CssClass = "form-control";
                                    ChoiceName.ClientIDMode = ClientIDMode.Static;
                                    ChoiceName.Text = ctrlValue;
                                    para.Controls.Add(ChoiceName);
                                }

                                using (var OptionID = new HiddenField())
                                {
                                    OptionID.ID = "hdnChoiceID" + iId;
                                    OptionID.ClientIDMode = ClientIDMode.Static;
                                    OptionID.Value = ctrlValue;
                                    para.Controls.Add(OptionID);
                                }

                                pnChoices.Controls.Add(para);
                            }

                            break;
                        }

            Add_Choice();
        }
    }
}