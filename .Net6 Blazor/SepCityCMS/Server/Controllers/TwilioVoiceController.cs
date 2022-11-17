
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SepCityCMS.Server;
    using System.IO;
    using System.Xml;
    using Twilio.AspNet.Common;
    using Twilio.TwiML;

    public class TwilioVoiceController : Controller
    {
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
                    var response = new VoiceResponse();
                    if (!string.IsNullOrWhiteSpace(request.To))
                    {
                        response.Dial(request.To);
                    }
                    else
                    {
                        response.Say("Thank you for calling!");
                    }

                    return Server.SepCore.Strings.ToString(response);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                var response = new VoiceResponse();
                response.Say("Application Error");
                return Server.SepCore.Strings.ToString(response);
            }
        }
    }
}