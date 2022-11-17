// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="captchaimage.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web.UI;

    /// <summary>
    /// Class captchaimage.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class captchaimage : Page
    {
        /// <summary>
        /// The i BMP height
        /// </summary>
        private readonly int iBMPHeight = 50;

        /// <summary>
        /// The i BMP width
        /// </summary>
        private readonly int iBMPWidth = 220;

        /// <summary>
        /// The s top margin
        /// </summary>
        private readonly float sTopMargin = 10;

        /// <summary>
        /// The BMP captcha
        /// </summary>
        private Bitmap bmpCaptcha;

        /// <summary>
        /// The g
        /// </summary>
        private Graphics g;

        /// <summary>
        /// The i angle
        /// </summary>
        private int iAngle;

        /// <summary>
        /// The rf letter
        /// </summary>
        private RectangleF rfLetter;

        /// <summary>
        /// The sf letter
        /// </summary>
        private SizeF sfLetter;

        /// <summary>
        /// The s left margin
        /// </summary>
        private float sLeftMargin = 20;

        /// <summary>
        /// The s letter
        /// </summary>
        private string sLetter;

        /// <summary>
        /// The s offset
        /// </summary>
        private float sOffset;

        /// <summary>
        /// The s temporary
        /// </summary>
        private float sTemp;

        /// <summary>
        /// The s x1
        /// </summary>
        private float sX1;

        /// <summary>
        /// The s x2
        /// </summary>
        private float sX2;

        /// <summary>
        /// The s y1
        /// </summary>
        private float sY1;

        /// <summary>
        /// The s y2
        /// </summary>
        private float sY2;

        /// <summary>
        /// Generates the fake word.
        /// </summary>
        /// <param name="len">The length.</param>
        /// <returns>System.String.</returns>
        public static string GenerateFakeWord(int len)
        {
            var r = new Random();
            string[] consonants = { "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X" };
            string[] vowels = { "A", "E", "I", "O", "U", "Y" };
            var Name = string.Empty;
            Name += SepCommon.SepCore.Strings.UCase(consonants[r.Next(consonants.Length)]);
            Name += vowels[r.Next(vowels.Length)];
            var b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        /// <summary>
        /// Generates the captcha.
        /// </summary>
        /// <param name="sWord">The s word.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap GenerateCaptcha(string sWord)
        {
            bmpCaptcha = new Bitmap(iBMPWidth, iBMPHeight, PixelFormat.Format16bppRgb555);
            g = Graphics.FromImage(bmpCaptcha);
            var drawBackground = new SolidBrush(Color.Silver);
            g.FillRectangle(drawBackground, new Rectangle(0, 0, iBMPWidth, iBMPHeight));
            drawBackground.Dispose();

            // Create font and brush.
            var drawFont = new Font("Times New Roman", 20);
            var drawBrush = new SolidBrush(Color.DarkBlue);
            var strFormat = new StringFormat(StringFormatFlags.FitBlackBox);
            sfLetter = new SizeF(30, 30);

            int ixr;
            // Draw string to screen.
            for (ixr = 0; ixr <= sWord.Length - 1; ixr++)
            {
                sLetter = sWord.Substring(ixr, 1);
                g = Graphics.FromImage(bmpCaptcha);
                rfLetter = new RectangleF(sLeftMargin, sTopMargin, 30, 30);
                sfLetter = g.MeasureString(sLetter, drawFont, sfLetter, strFormat);
                iAngle = (int)Math.Ceiling(RndInterval(0, 20) - 10);
                var _with1 = rfLetter;
                sOffset = Convert.ToInt64(sLeftMargin * Math.Tan(iAngle * Math.PI / 180));
                _with1.Y = sTopMargin - sOffset;
                _with1.Width = sfLetter.Width + 10;
                _with1.Height = sfLetter.Height;
                g.RotateTransform(iAngle);
                g.DrawString(sLetter, drawFont, drawBrush, rfLetter);
                sLeftMargin += sfLetter.Width + 2;
            }

            drawFont.Dispose();
            drawBrush.Dispose();
            strFormat.Dispose();

            var drawPen = new Pen(Color.Crimson, 1);

            for (ixr = 0; ixr <= 3; ixr++)
            {
                sX1 = sX2;
                while (Math.Abs(sX1 - sX2) < iBMPWidth * 0.5)
                {
                    sX1 = RndInterval(2, iBMPWidth - 2);
                    sX2 = RndInterval(2, iBMPWidth - 2);
                }

                sY1 = sY2;
                while (Math.Abs(sY1 - sY2) < iBMPHeight * 0.5)
                {
                    sY1 = RndInterval(2, iBMPHeight - 2);
                    sY2 = RndInterval(2, iBMPHeight - 2);
                }

                if (RndInterval(0, 2) > 1)
                {
                    sTemp = sX1;
                    sX1 = sX2;
                    sX2 = sTemp;
                    sTemp = sY1;
                    sY1 = sY2;
                    sY2 = sTemp;
                }

                g.DrawLine(drawPen, sX1, sY1, sX2, sY2);
            }

            drawPen.Dispose();
            g.Dispose();

            return bmpCaptcha;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bmpCaptcha != null)
                {
                    bmpCaptcha.Dispose();
                }

                if (g != null)
                {
                    g.Dispose();
                }
            }
        }

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
            Response.Clear();
            Response.ContentType = "image/png";

            var CaptchaWord = GenerateFakeWord(6);

            SepCommon.SepCore.Session.setSession("CaptchaText", CaptchaWord);

            using (var image = new Bitmap(GenerateCaptcha(CaptchaWord)))
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    ms.WriteTo(Context.Response.OutputStream);
                }
            }

            Response.End();
        }

        /// <summary>
        /// Randoms the interval.
        /// </summary>
        /// <param name="iMin">The i minimum.</param>
        /// <param name="iMax">The i maximum.</param>
        /// <returns>System.Single.</returns>
        private float RndInterval(int iMin, int iMax)
        {
            var random = new Random();
            return random.Next(0, iMax - iMin + 1 + iMin);
        }
    }
}