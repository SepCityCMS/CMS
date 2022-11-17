// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="image_thumbnail.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot.spadmin.ImageManager
{
    using SepCommon;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Web.UI;

    /// <summary>
    /// Class image_thumbnail.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class image_thumbnail : Page
    {
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                var nSizeThumb = 0;
                var nNewWidth = 0;
                var nNewHeight = 0;
                var imgOri = default(Image);
                var nJpegQuality = 0;
                string sFixWidth = null;
                nSizeThumb = SepFunctions.toInt(Request.QueryString["Size"]);
                nJpegQuality = SepFunctions.toInt(Request.QueryString["Quality"]);

                if (string.IsNullOrWhiteSpace(Request.QueryString["Quality"])) nJpegQuality = 90;
                sFixWidth = Request.QueryString["fixw"];

                var sFile = Server.MapPath(Request.QueryString["file"]);

                imgOri = Image.FromFile(sFile);
                nNewWidth = imgOri.Size.Width;
                nNewHeight = imgOri.Size.Height;

                if ((nNewWidth < nSizeThumb) & (nNewHeight < nSizeThumb))
                {
                    // noop
                }
                else if (nNewWidth > nNewHeight)
                {
                    nNewHeight /= (nNewWidth / nSizeThumb);
                    nNewWidth = nSizeThumb;
                }
                else if (nNewWidth < nNewHeight)
                {
                    nNewWidth /= (nNewHeight / nSizeThumb);
                    nNewHeight = nSizeThumb;
                }
                else
                {
                    nNewWidth = nSizeThumb;
                    nNewHeight = nSizeThumb;
                }

                if (sFixWidth == "Y")
                {
                    nNewHeight = nNewHeight * nSizeThumb / nNewWidth;
                    nNewWidth = nSizeThumb;
                }

                Image imgThumb = new Bitmap(nNewWidth, nNewHeight);
                var gr = Graphics.FromImage(imgThumb);
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.DrawImage(imgOri, 0, 0, nNewWidth, nNewHeight);

                var info = ImageCodecInfo.GetImageEncoders();
                dynamic ePars = new EncoderParameters(1);
                ePars.Param[0] = new EncoderParameter(Encoder.Quality, nJpegQuality);
                Response.ContentType = "image/jpeg";
                imgThumb.Save(Response.OutputStream, info[1], ePars);
                imgThumb.Dispose();
                imgOri.Dispose();
            }
            catch (Exception ex)
            {
                Response.ContentType = "image/jpeg";
                Response.Write(ex.Message);
            }
        }
    }
}