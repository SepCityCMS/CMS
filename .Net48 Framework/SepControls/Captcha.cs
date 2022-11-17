// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="Captcha.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    /// <summary>
    /// Class GoogleVerificationResponseOutput.
    /// </summary>
    public class GoogleVerificationResponseOutput
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GoogleVerificationResponseOutput"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool success { get; set; }
    }
}

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class Captcha.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Captcha runat=server></{0}:Captcha>")]
    public class Captcha : WebControl
    {
        /// <summary>
        /// The validate response field
        /// </summary>
        private readonly string ValidateResponseField = "g-recaptcha-response";

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                var s = Context.Request.Form[ID];
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Validate()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey")))
            {
                var EncodedResponse = Page.Request.Form[ValidateResponseField];

                if (string.IsNullOrWhiteSpace(EncodedResponse))
                {
                    return false;
                }

                // by pass certificate validation
                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;

                var client = new WebClient();

                var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey"), EncodedResponse));

                var serializer = new JavaScriptSerializer();
                var gOutput = serializer.Deserialize<GoogleVerificationResponseOutput>(GoogleReply);

                client.Dispose();

                return gOutput.success;
            }

            if (Strings.LCase(Request.Item("Captcha" + ID)) == Strings.LCase(Session.getSession("CaptchaText")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey")))
            {
                var reCaptchaHTML = string.Format("<div class='g-recaptcha' data-sitekey='{0}'></div>", SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey"));

                var scriptToRenderCaptcha = " <script src=\"https://www.google.com/recaptcha/api.js\" async defer></script>";
                output.Write(reCaptchaHTML);
                Page.ClientScript.RegisterStartupScript(GetType(), "reCapctchaScript", scriptToRenderCaptcha, false);
            }
            else
            {
                output.WriteLine("<div id=\"" + ID + "\">");
                output.WriteLine("<img src=\"" + sInstallFolder + "captchaimage.aspx?guid=" + SepFunctions.Generate_GUID() + "\" border=\"0\" />");
                output.WriteLine("<div>" + SepFunctions.LangText("Enter below the text that you see above.") + "</div>");
                output.WriteLine("<div><input type=\"text\" name=\"Captcha" + ID + "\" id=\"Captcha" + ID + "\" autocomplete=\"false\" /></div>");
                output.WriteLine("</div>");
            }
        }

        /// <summary>
        /// Accepts all certifications.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="certification">The certification.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}