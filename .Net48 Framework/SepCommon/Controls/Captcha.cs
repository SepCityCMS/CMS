// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Captcha.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    /// <summary>
    /// Class GoogleVerificationResponseOutput.
    /// </summary>
    public class GoogleVerificationResponseOutput
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GoogleVerificationResponseOutput" /> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool success { get; set; }
    }
}

namespace SepCityControls
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.SepCore;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Class Captcha.
    /// </summary>
    public class Captcha
    {
        /// <summary>
        /// The validate response field
        /// </summary>
        private readonly string ValidateResponseField = "g-recaptcha-response";

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string ID { get; set; }

        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey")))
            {
                var reCaptchaHTML = string.Format("<div class='g-recaptcha' data-sitekey='{0}'></div>", SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey"));

                var scriptToRenderCaptcha = " <script src=\"https://www.google.com/recaptcha/api.js\" async defer></script>";
                output.Append(reCaptchaHTML);
                output.AppendLine(scriptToRenderCaptcha);
            }
            else
            {
                output.AppendLine("<div id=\"" + ID + "\">");
                output.AppendLine("<img src=\"" + sInstallFolder + "captchaimage.aspx?guid=" + SepFunctions.Generate_GUID() + "\" border=\"0\" />");
                output.AppendLine("<div>" + SepFunctions.LangText("Enter below the text that you see above.") + "</div>");
                output.AppendLine("<div><input type=\"text\" name=\"Captcha" + ID + "\" id=\"Captcha" + ID + "\" autocomplete=\"false\" /></div>");
                output.AppendLine("</div>");
            }

            return output.ToString();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Validate()
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPublicKey")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey")))
            {
                var EncodedResponse = Request.Form(ValidateResponseField);

                if (string.IsNullOrWhiteSpace(EncodedResponse))
                {
                    return false;
                }

                // by pass certificate validation
                ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;

                var client = new WebClient();

                var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", SepFunctions.Setup(989, "GooglereCAPTCHAPrivateKey"), EncodedResponse));

                var gOutput = JsonConvert.DeserializeObject<GoogleVerificationResponseOutput>(GoogleReply);

                client.Dispose();

                return gOutput.success;
            }

            if (Strings.LCase(Request.Item("Captcha" + ID)) == Strings.LCase(Session.getSession("CaptchaText")))
            {
                return true;
            }
            else
            {
                return false;
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