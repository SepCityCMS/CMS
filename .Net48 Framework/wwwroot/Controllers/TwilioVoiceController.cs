// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="TwilioVoiceController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using SepCommon;
    using System.IO;
    using System.Web.Mvc;
    using System.Xml;
    using Twilio.Mvc;
    using Twilio.TwiML;

    /// <summary>
    /// Class TwilioVoiceController.
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TwilioVoiceController : Controller
    {
        /// <summary>
        /// Indexes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        [Route("TwilioVoice")]
        [HttpPost]
        public string Index(VoiceRequest request)
        {
            try
            {
                string callerId = string.Empty;

                if (System.IO.File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                {
                    XmlDocument doc = new XmlDocument() { XmlResolver = null };
                    using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc.Load(reader);
                            var root = doc.DocumentElement;
                            if (root.SelectSingleNode("/Root/CallerId") != null && !string.IsNullOrWhiteSpace(root.SelectSingleNode("/Root/CallerId").InnerText))
                            {
                                callerId = root.SelectSingleNode("/Root/CallerId").InnerText;
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(callerId))
                {
                    var response = new TwilioResponse();
                    if (!string.IsNullOrWhiteSpace(request.To))
                    {
                        response.Dial(new Number(request.To), new { callerId });
                    }
                    else
                    {
                        response.Say("Thank you for calling!");
                    }

                    return SepCommon.SepCore.Strings.ToString(response);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                var response = new TwilioResponse();
                response.Say("Application Error");
                return SepCommon.SepCore.Strings.ToString(response);
            }
        }
    }
}