// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="IP2Location.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using Newtonsoft.Json;
    using SepCommon.Core.SepCore;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// An IP 2 location.
    /// </summary>
    public class IP2Location
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>The IP address.</value>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP end.
        /// </summary>
        /// <value>The IP end.</value>
        public string IPEnd { get; set; }

        /// <summary>
        /// Gets or sets the IP start.
        /// </summary>
        /// <value>The IP start.</value>
        public string IPStart { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>The province.</value>
        public string Province { get; set; }
    }

    /// <summary>
    /// Information about the access token.
    /// </summary>
    public class AccessTokenInfo
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public string StatusCode { get; set; }
    }

    /// <summary>
    /// A province in country.
    /// </summary>
    public class ProvinceInCountry
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the provinces.
        /// </summary>
        /// <value>The provinces.</value>
        public List<string> Provinces { get; set; }
    }

    /// <summary>
    /// A postal codes in distance.
    /// </summary>
    public class PostalCodesInDistance
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the kilometers.
        /// </summary>
        /// <value>The kilometers.</value>
        public string Kilometers { get; set; }

        /// <summary>
        /// Gets or sets the miles.
        /// </summary>
        /// <value>The miles.</value>
        public string Miles { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }
    }

    /// <summary>
    /// A postal codes in distance response.
    /// </summary>
    public class PostalCodesInDistanceResponse
    {
        /// <summary>
        /// Gets or sets the distances.
        /// </summary>
        /// <value>The distances.</value>
        public List<string> Distances { get; set; }

        /// <summary>
        /// Gets or sets the postal codes.
        /// </summary>
        /// <value>The postal codes.</value>
        public List<string> PostalCodes { get; set; }
    }

    /// <summary>
    /// A postal to distance.
    /// </summary>
    public class PostalToDistance
    {
        /// <summary>
        /// Gets or sets from postal code.
        /// </summary>
        /// <value>from postal code.</value>
        public string FromPostalCode { get; set; }

        /// <summary>
        /// Gets or sets to postal code.
        /// </summary>
        /// <value>to postal code.</value>
        public string ToPostalCode { get; set; }
    }

    /// <summary>
    /// The radius distance.
    /// </summary>
    public class RadiusDistance
    {
        /// <summary>
        /// Gets or sets the kilometers.
        /// </summary>
        /// <value>The kilometers.</value>
        public string Kilometers { get; set; }

        /// <summary>
        /// Gets or sets the miles.
        /// </summary>
        /// <value>The miles.</value>
        public string Miles { get; set; }
    }

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Ignore bad certificates.
        /// </summary>
        public static void IgnoreBadCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        /// <summary>
        /// IP 2 location.
        /// </summary>
        /// <param name="strIPAddress">The IP address.</param>
        /// <param name="City">[in,out] The city.</param>
        /// <param name="State">[in,out] The state.</param>
        /// <param name="Country">[in,out] The country.</param>
        public static void IP2Location(string strIPAddress, ref string City, ref string State, ref string Country)
        {
            var sSiteName = Setup(992, "WebSiteName");

            try
            {
                var pcrSession = Strings.Replace(Session.getSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepIP2Location"), "'", string.Empty);
                if (!string.IsNullOrWhiteSpace(pcrSession))
                {
                    if (pcrSession == "Error")
                    {
                        State = string.Empty;
                        Country = string.Empty;
                        City = string.Empty;
                    }
                    else
                    {
                        var arrSession = Strings.Split(pcrSession, "||");
                        State = arrSession[0];
                        Country = arrSession[1];
                        City = arrSession[2];
                    }
                }
                else
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    IgnoreBadCertificates();

                    var IP2L = new IP2Location();
                    IP2L.IPAddress = strIPAddress;

                    var postData = JsonConvert.SerializeObject(IP2L);

                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var url = "https://www.sepcity.com/api/IP2Location";
                    if (DebugMode)
                    {
                        url = "https://new.sepcity.com/api/IP2Location";
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(url);
                    WRequest.Headers.Add("Authorization", "BEARER " + SepCityToken());
                    WRequest.Method = "POST";
                    WRequest.ContentLength = byteArray.Length;
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";
                    using (var dataStream = WRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var sResults = JsonConvert.DeserializeObject<IP2Location>(jsonString);
                    Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepIP2Location", sResults.Province + "||" + sResults.CountryCode + "||" + sResults.City);
                    State = sResults.Province;
                    Country = sResults.CountryCode;
                    City = sResults.City;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Postal code distance.
        /// </summary>
        /// <param name="FromPostalCode">from postal code.</param>
        /// <param name="ToPostalCode">to postal code.</param>
        /// <returns>A string.</returns>
        public static string PostalCodeDistance(string FromPostalCode, string ToPostalCode)
        {
            try
            {
                var token = SepCityToken();

                if (token != "Error")
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    IgnoreBadCertificates();

                    var IP2L = new PostalToDistance();
                    IP2L.FromPostalCode = FromPostalCode;
                    IP2L.ToPostalCode = ToPostalCode;

                    var postData = JsonConvert.SerializeObject(IP2L);

                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var url = "https://www.sepcity.com/api/Radius/Distance";
                    if (DebugMode)
                    {
                        url = "https://new.sepcity.com/api/Radius/Distance";
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(url);
                    WRequest.Headers.Add("Authorization", "BEARER " + token);
                    WRequest.Method = "POST";
                    WRequest.ContentLength = byteArray.Length;
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";
                    using (var dataStream = WRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var sResults = JsonConvert.DeserializeObject<RadiusDistance>(jsonString);
                    if (GetUserCountry() == "us")
                    {
                        return sResults.Miles;
                    }

                    return sResults.Kilometers;
                }
            }
            catch
            {
            }

            return "0";
        }

        /// <summary>
        /// Postal codes in distance.
        /// </summary>
        /// <param name="Country">The country.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="Miles">The miles.</param>
        /// <param name="Kilometers">The kilometers.</param>
        /// <returns>A PostalCodesInDistanceResponse.</returns>
        public static PostalCodesInDistanceResponse PostalCodesInDistance(string Country, string PostalCode, string Miles, string Kilometers)
        {
            try
            {
                var token = SepCityToken();

                if (token != "Error")
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    IgnoreBadCertificates();

                    var IP2L = new PostalCodesInDistance();
                    IP2L.CountryCode = Country;
                    IP2L.PostalCode = PostalCode;
                    IP2L.Miles = Miles;
                    IP2L.Kilometers = Kilometers;

                    var postData = JsonConvert.SerializeObject(IP2L);

                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var url = "https://www.sepcity.com/api/PostalCodesInDistance";
                    if (DebugMode)
                    {
                        url = "https://new.sepcity.com/api/PostalCodesInDistance";
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(url);
                    WRequest.Headers.Add("Authorization", "BEARER " + token);
                    WRequest.Method = "POST";
                    WRequest.ContentLength = byteArray.Length;
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";
                    using (var dataStream = WRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var sResults = JsonConvert.DeserializeObject<PostalCodesInDistanceResponse>(jsonString);
                    return sResults;
                }
            }
            catch
            {
            }

            return null;
        }

        /// <summary>
        /// Provinces in country.
        /// </summary>
        /// <param name="Country">The country.</param>
        /// <returns>A ProvinceInCountry.</returns>
        public static ProvinceInCountry ProvincesInCountry(string Country)
        {
            try
            {
                var token = SepCityToken();

                if (token != "Error")
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    IgnoreBadCertificates();

                    var IP2L = new ProvinceInCountry();
                    IP2L.CountryCode = Country;

                    var postData = JsonConvert.SerializeObject(IP2L);

                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var url = "https://www.sepcity.com/api/ProvincesInCountry";
                    if (DebugMode)
                    {
                        url = "https://new.sepcity.com/api/ProvincesInCountry";
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(url);
                    WRequest.Headers.Add("Authorization", "BEARER " + token);
                    WRequest.Method = "POST";
                    WRequest.ContentLength = byteArray.Length;
                    WRequest.ContentType = "application/json";
                    WRequest.Accept = "application/json";
                    using (var dataStream = WRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WResponse = (HttpWebResponse)WRequest.GetResponse();
                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var sResults = JsonConvert.DeserializeObject<ProvinceInCountry>(jsonString);
                    return sResults;
                }
            }
            catch
            {
            }

            return null;
        }

        /// <summary>
        /// Separator city token.
        /// </summary>
        /// <returns>A string.</returns>
        public static string SepCityToken()
        {
            var sSession = string.Empty;
            var sSiteName = Setup(992, "WebSiteName");

            try
            {
                var pcrSession = Strings.Replace(Strings.ToString(Session.getSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepToken")), "'", string.Empty);
                if (!string.IsNullOrWhiteSpace(pcrSession))
                {
                    sSession = pcrSession;
                }
                else
                {
                    HttpWebRequest WRequest = null;
                    HttpWebResponse WResponse = null;
                    StreamReader WReader = null;

                    var sDomain = GetMasterDomain(false).Replace("://www.", "://");

                    IgnoreBadCertificates();

                    var apiKey = string.Empty;
                    if (isHosted())
                    {
                        apiKey = AES_Encrypt(sDomain + "||" + GetEncryptionKey(), "8695vGPjDlbG5vGP");
                    }
                    else
                    {
                        apiKey = AES_Encrypt(sDomain + "||" + GetEncryptionKey() + "||" + Setup(70, "SepCityAPIKey") + "||" + Setup(70, "SepCityUser") + "||" + Setup(70, "SepCityPassword"), "8695vGPjDlbG5vGP");
                    }

                    var url = "https://www.sepcity.com/api/AccessToken?apikey=" + UrlEncode(apiKey);
                    if (DebugMode)
                    {
                        url = "https://new.sepcity.com/api/AccessToken?apikey=" + UrlEncode(apiKey);
                    }

                    WRequest = (HttpWebRequest)WebRequest.Create(url);
                    WResponse = (HttpWebResponse)WRequest.GetResponse();

                    WReader = new StreamReader(WResponse.GetResponseStream());
                    var jsonString = WReader.ReadToEnd();
                    WReader.Dispose();

                    var sResults = JsonConvert.DeserializeObject<AccessTokenInfo>(jsonString);

                    if (sResults.StatusCode == "200")
                    {
                        Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepToken", sResults.Message);
                        sSession = sResults.Message;
                    }
                    else
                    {
                        Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepToken", "Error");
                    }
                }
            }
            catch
            {
                Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "SepToken", "Error");
            }

            return sSession;
        }

        /// <summary>
        /// Accept all certifications.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="certification">The certification.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The ssl policy errors.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}