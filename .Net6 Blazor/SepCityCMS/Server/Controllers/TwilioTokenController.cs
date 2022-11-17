
namespace SepCityCMS.Server.Controllers
{
    using Faker;
    using Faker.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.IO;
    using System.Xml;

    public class TwilioTokenController : Controller
    {
        [CheckOption("username", "AdminAccess")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
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

            var identity = Internet.UserName().AlphanumericOnly();

            var capability = new Twilio.Jwt.ClientCapability(accountSid, authToken);
            var token = capability.ToJwt();

            return Json(new { identity, token });
        }
    }
}