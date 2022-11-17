// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Conversions.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using iTextSharp.tool.xml;
    using Novacode;
    using System.IO;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Document x coordinate 2 text.
        /// </summary>
        /// <param name="filename">Filename of the file.</param>
        /// <returns>A string.</returns>
        public static string DocX2Text(string filename)
        {
            var sText = string.Empty;

            try
            {
                DocX doc = DocX.Load(filename);
                sText = doc.Text;
                doc.Dispose();
            }
            catch
            {
                // Do Nothing
            }

            return sText;
        }

        /// <summary>
        /// HTML to PDF.
        /// </summary>
        /// <param name="HTMLCode">The HTML code.</param>
        /// <returns>A string.</returns>
        public static string HTMLToPDF(string HTMLCode)
        {
            var sText = string.Empty;

            var tempID = SepCore.Strings.ToString(GetIdentity());

            // Create PDF document
            using (var doc = new Document(PageSize.A4))
            {
                var writer = PdfWriter.GetInstance(doc, new FileStream(GetDirValue("App_Data") + "temp/" + tempID + ".pdf", FileMode.Create));
                doc.Open();

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, new StringReader(HTMLCode));
            }

            if (File.Exists(GetDirValue("App_Data") + "temp/" + tempID + ".pdf"))
            {
                using (var fileData = new StreamReader(GetDirValue("App_Data") + "temp/" + tempID + ".pdf"))
                {
                    sText = fileData.ReadToEnd();
                }

                File.Delete(GetDirValue("App_Data") + "temp/" + tempID + ".pdf");
            }

            return sText;
        }

        /// <summary>
        /// PDF 2 text.
        /// </summary>
        /// <param name="pdffile">The pdffile.</param>
        /// <returns>A string.</returns>
        public static string PDF2Text(string pdffile)
        {
            if (File.Exists(pdffile))
            {
                try
                {
                    var text = new StringBuilder();
                    using (var pdfReader = new PdfReader(pdffile))
                    {
                        for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                            var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                            currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text.Append(currentText);
                        }
                    }

                    return SepCore.Strings.ToString(text);
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}