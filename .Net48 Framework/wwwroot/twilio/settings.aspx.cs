// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="settings.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.twilio
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    /// <summary>
    /// Class settings.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class settings : Page
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
        /// Resets the database.
        /// </summary>
        public void Reset_Database()
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM TwilioNumbers", conn))
                {
                    cmd.ExecuteNonQuery();
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
            if (SepFunctions.Admin_Login_Required(SepFunctions.Security("AdminAccess")))
            {
                var MainContent = (ContentPlaceHolder)Master.FindControl("MainContent");
                MainContent.Visible = false;
                var idLogin = (UpdatePanel)Master.FindControl("idLogin");
                idLogin.Visible = true;
                return;
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
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
                XmlDocument doc = new XmlDocument() { XmlResolver = null };
                using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings.xml"))
                {
                    using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                    {
                        doc.Load(reader);

                        XmlElement root = doc.DocumentElement;

                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID") != null)
                        {
                            OldTwilioSID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID").InnerText;
                            TwilioSID.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID").InnerText;
                        }

                        if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken") != null)
                        {
                            OldTwilioToken.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken").InnerText;
                            TwilioToken.Value = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken").InnerText;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    doc.Load(reader);

                    XmlElement root = doc.DocumentElement;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID") == null)
                    {
                        makeXPath(doc, "/ROOTLEVEL/MODULE989/TwilioAccountSID");
                    }

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken") == null)
                    {
                        makeXPath(doc, "/ROOTLEVEL/MODULE989/TwilioAuthToken");
                    }

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID") != null)
                    {
                        root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID").InnerText = TwilioSID.Value;
                    }

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken") != null)
                    {
                        root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken").InnerText = TwilioToken.Value;
                    }
                }
            }

            doc.Save(SepFunctions.GetDirValue("app_data") + "settings.xml");

            if (OldTwilioSID.Value != TwilioSID.Value || OldTwilioToken.Value != TwilioToken.Value)
            {
                Reset_Database();
            }

            ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Settings has been successfully saved.") + "</div>";
        }

        /// <summary>
        /// Makes the x path.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns>XmlNode.</returns>
        static private XmlNode makeXPath(XmlDocument doc, string xpath)
        {
            return makeXPath(doc, doc, xpath);
        }

        /// <summary>
        /// Makes the x path.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns>XmlNode.</returns>
        static private XmlNode makeXPath(XmlDocument doc, XmlNode parent, string xpath)
        {
            // grab the next node name in the xpath; or return parent if empty
            string[] partsOfXPath = xpath.Trim('/').Split('/');
            string nextNodeInXPath = partsOfXPath.First();
            if (string.IsNullOrWhiteSpace(nextNodeInXPath))
                return parent;

            // get or create the node from the name
            XmlNode node = parent.SelectSingleNode(nextNodeInXPath);
            if (node == null)
                node = parent.AppendChild(doc.CreateElement(nextNodeInXPath));

            // rejoin the remainder of the array as an xpath expression and recurse
            string rest = string.Join("/", partsOfXPath.Skip(1).ToArray());
            return makeXPath(doc, node, rest);
        }
    }
}