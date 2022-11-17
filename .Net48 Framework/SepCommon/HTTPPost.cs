// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="HTTPPost.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Send this message.
        /// </summary>
        /// <param name="URL">URL of the resource.</param>
        /// <param name="PostData">(Optional) Information describing the post.</param>
        /// <returns>A string.</returns>
        public static string Send(string URL, string PostData = "")
        {
            // ERROR: Not supported in C#: OnErrorStatement
            var request = WebRequest.Create(URL);
            var response = request.GetResponse();
            var writer = new StreamWriter(request.GetRequestStream());

            response.ContentType = "text/xml";
            request.Method = "POST";
            request.ContentType = "text/xml";

            writer.Write(PostData);
            writer.Dispose();

            var reader = new StreamReader(response.GetResponseStream());
            var sReturn = reader.ReadToEnd();
            reader.Dispose();

            return sReturn;
        }

        /// <summary>
        /// Sends a get.
        /// </summary>
        /// <param name="URL">URL of the resource.</param>
        /// <returns>A string.</returns>
        public static string Send_Get(string URL)
        {
            // ERROR: Not supported in C#: OnErrorStatement
            var request = WebRequest.Create(URL);
            var response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var sReturn = reader.ReadToEnd();
            reader.Dispose();

            return sReturn;
        }

        /// <summary>
        /// Sends an XML.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        /// <param name="PostData">Information describing the post.</param>
        /// <param name="contentType">(Optional) Type of the content.</param>
        /// <returns>A string.</returns>
        public static string Send_XML(string url, string PostData, string contentType = "text/xml")
        {
            string responseFromServer = string.Empty;

            var Request = WebRequest.Create(url);
            Request.Method = "POST";

            var ByteArray = Encoding.UTF8.GetBytes(PostData);

            // Set the ContentType property of the WebRequest.
            Request.ContentType = contentType;

            // Set the ContentLength property of the WebRequest.
            Request.ContentLength = ByteArray.Length;

            // Get the Request stream.
            var dataStream = Request.GetRequestStream();

            // Write the data to the Request stream.
            dataStream.Write(ByteArray, 0, ByteArray.Length);
            using (var response = Request.GetResponse())
            {

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                using (var reader = new StreamReader(dataStream))
                {
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                }
            }

            return responseFromServer;
        }
    }
}