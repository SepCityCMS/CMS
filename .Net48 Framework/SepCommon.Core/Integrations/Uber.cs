using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace SepCommon.Core
{
    public class Uber
    {

        public void Login()
        {
            SepFunctions.Redirect("https://login.uber.com/oauth/v2/authorize?response_type=code&client_id=aJQZXH_RvNPD1Gt8tbAJOKdH5jXsZV4A&scope=profile&redirect_uri=https://demo.sepcity.com/uber.aspx");
        }

        /// async Task<string> 
        public string GetToken(string code)
        {

            WebClient webClient = new WebClient();

            NameValueCollection formData = new NameValueCollection();
            formData["client_secret"] = "gsRcJbg9UbtbMlFju3jFnwpFhxMg1AmPKw4kbCX-";
            formData["client_id"] = "aJQZXH_RvNPD1Gt8tbAJOKdH5jXsZV4A";
            formData["grant_type"] = "authorization_code";
            formData["redirect_uri"] = "https://demo.sepcity.com/uber.aspx";
            formData["code"] = code;

            byte[] responseBytes = webClient.UploadValues("https://login.uber.com/oauth/v2/token", "POST", formData);
            string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();

            UberResponse responseJSON = new UberResponse();

            responseJSON = JsonConvert.DeserializeObject<UberResponse>(resultAuthTicket);

            return responseJSON.access_token;

        }

        public string GetPricing(string code)
        {

            var accessToken = GetToken(code);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                SepFunctions.Debug_Log("Access Token: " + accessToken);

                var request = WebRequest.Create("https://api.uber.com/v1.2/estimates/price?start_latitude=37.7752315&start_longitude=-122.418075&end_latitude=37.7752415&end_longitude=-122.518075");

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                request.Headers.Add("Accept-Language", "en_US");

                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var sReturn = reader.ReadToEnd();
                response.Dispose();
                reader.Dispose();

                SepFunctions.Debug_Log("Pricing: " + sReturn);

                return sReturn;
            }
            return "";

        }
    }
    public class UberResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}
