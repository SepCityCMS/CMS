// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="TwilioTokenController.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.Controllers
{
    using Faker;
    using Faker.Extensions;
    using SepCommon;
    using System.IO;
    using System.Web.Mvc;
    using System.Xml;
    using Twilio;

    /// <summary>
    /// Class TwilioTokenController.
    /// Implements the <see cref="System.Web.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TwilioTokenController : Controller
    {
        // GET: TwilioToken
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), false) == false)
            {
                return null;
            }

            var accountSid = string.Empty;
            var authToken = string.Empty;
            var appSid = string.Empty;

            XmlDocument doc = new XmlDocument() { XmlResolver = null };
            using (StreamReader sreader = new StreamReader(SepFunctions.GetDirValue("app_data") + "settings.xml"))
            {
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    doc.Load(reader);

                    XmlElement root = doc.DocumentElement;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID") != null) accountSid = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAccountSID").InnerText;

                    if (root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken") != null) authToken = root.SelectSingleNode("/ROOTLEVEL/MODULE989/TwilioAuthToken").InnerText;

                    if (System.IO.File.Exists(SepFunctions.GetDirValue("app_data") + "twilio_app.xml"))
                    {
                        XmlDocument doc2 = new XmlDocument() { XmlResolver = null };
                        StringReader sreader2 = new StringReader(SepFunctions.GetDirValue("app_data") + "twilio_app.xml");
                        using (XmlReader reader2 = XmlReader.Create(sreader2, new XmlReaderSettings() { XmlResolver = null }))
                        {
                            doc2.Load(reader2);
                            var root2 = doc2.DocumentElement;
                            if (root2.SelectSingleNode("/Root/AppSID") != null)
                            {
                                appSid = root2.SelectSingleNode("/Root/AppSID").InnerText;
                            }
                        }
                    }
                }
            }

            // Create a random identity for the client
            var identity = Internet.UserName().AlphanumericOnly();

            // Create an Access Token generator
            var capability = new TwilioCapability(accountSid, authToken);
            capability.AllowClientOutgoing(appSid);
            capability.AllowClientIncoming(identity);
            var token = capability.GenerateToken();

            return Json(new { identity, token }, JsonRequestBehavior.AllowGet);
        }
    }
}