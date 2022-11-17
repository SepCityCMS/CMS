// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="AccountMenu.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityControls
{
    using SepCommon;
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class AccountMenu.
    /// </summary>
    public class AccountMenu
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                var cMenu = new SiteMenu
                {
                    MenuID = 10,
                    CssClass = "flex-column"
                };
                output.Append("<div class=\"col-12\">" + cMenu.Render() + "</div>");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey")))
                {
                    output.AppendLine("<p class=\"FacebookImgRow\" id=\"FacebookRow\">");
                    output.AppendLine("<a onclick=\"fbAccountLogin();\" href=\"javascript:void(0)\" class=\"fb_button fb_button_medium\"><span class=\"fb_button_text\">" + SepFunctions.LangText("Sign in with Facebook") + "</span></a>");
                    output.AppendLine("</p>");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInAPI")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInSecret")))
                {
                    output.AppendLine("<p class=\"LinkedInImgRow\" id=\"LinkedInRow\">");
                    output.AppendLine("<script type=\"in/Login\"></script>");
                    output.AppendLine("</p>");
                }

                output.AppendLine("<form action=\"" + sInstallFolder + "login.aspx\" name=\"frmAccForm\" method=\"post\">");
                output.AppendLine(System.Web.Helpers.AntiForgery.GetHtml().ToString());
                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey")))
                {
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_Token2\" id=\"Facebook_Token2\" value=\"\" />");
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_Id2\" id=\"Facebook_Id2\" value=\"\" />");
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_User2\" id=\"Facebook_User2\" value=\"\" />");
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_Email2\" id=\"Facebook_Email2\" value=\"\" />");
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_FName2\" id=\"Facebook_FName2\" value=\"\" />");
                    output.AppendLine("<input type=\"hidden\" name=\"Facebook_LName2\" id=\"Facebook_LName2\" value=\"\" />");
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInAPI")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInSecret")))
                {
                    output.AppendLine("<input type=\"hidden\" name=\"LinkedInId2\" id=\"LinkedInId2\" value=\"\" />");
                }

                output.AppendLine("<input type=\"hidden\" name=\"Activate2\" value=\"" + SepCommon.SepCore.Request.Item("Activate") + "\" />");
                foreach (var Item in SepCommon.SepCore.Request.Form())
                {
                    if (SepCommon.SepCore.Strings.ToString(Item) != "Activate2" && SepCommon.SepCore.Strings.ToString(Item) != "UserName2" && SepCommon.SepCore.Strings.ToString(Item) != "Password2")
                    {
                        output.AppendLine("<input type=\"hidden\" name=\"" + SepCommon.SepCore.Strings.ToString(Item) + "\" value=\"" + SepFunctions.HTMLEncode(SepCommon.SepCore.Request.Form(SepCommon.SepCore.Strings.ToString(Item))) + "\" />");
                    }
                }

                output.AppendLine("<div>");
                if (SepFunctions.Setup(997, "LoginEmail") == "Yes")
                {
                    output.Append(SepFunctions.LangText("Email Address"));
                }
                else
                {
                    output.Append(SepFunctions.LangText("Username"));
                }

                output.AppendLine("<br/>");
                output.AppendLine("<input type=\"text\" name=\"UserName2\" id=\"UserName2\" /><br/>");
                output.Append(SepFunctions.LangText("Password") + "<br/>");
                output.AppendLine("<input type=\"password\" name=\"Password2\" id=\"Password2\" autocomplete=\"off\" /><br/>");
                output.AppendLine("<input type=\"checkbox\" name=\"RememberMe2\" id=\"RememberMe2\" value=\"true\" /><label for=\"RememberMe2\">" + SepFunctions.LangText("Keep me logged in ") + "</label><br/>");
                output.AppendLine("<div align=\"right\"><input type=\"submit\" class=\"btn btn-primary\" value=\"" + SepFunctions.LangText("Login") + "\" /></div><br/>");

                if (SepFunctions.ModuleActivated(68) == false)
                {
                    output.AppendLine("<div align=\"center\" id=\"idAccReg\"><a href=\"" + sInstallFolder + "signup.aspx\">" + SepFunctions.LangText("Register Now") + "</a><br/>");
                    output.AppendLine("<a href=\"" + sInstallFolder + "forgot_password.aspx\">" + SepFunctions.LangText("Forgot Password?") + "</a></div>");
                    output.AppendLine("</div>");
                    output.AppendLine("</form>");
                }
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()) || !string.IsNullOrWhiteSpace(SepFunctions.Session_Invoice_ID()))
            {
                string CartDisplayStatement;
                if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    CartDisplayStatement = "UserID='" + SepFunctions.Session_User_ID() + "'";
                }
                else
                {
                    CartDisplayStatement = "InvoiceID='" + SepFunctions.FixWord(SepFunctions.Session_Invoice_ID()) + "'";
                }

                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE " + CartDisplayStatement + " AND Status='0' AND inCart='1'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                output.AppendLine("<div class=\"col-12\"><button type=\"button\" class=\"btn btn-success btn-block\" onclick=\"document.location.href='" + sInstallFolder + "viewcart.aspx';\">" + SepFunctions.LangText("View Cart") + "</button></div>");
                            }
                        }
                    }
                }
            }

            return output.ToString();
        }
    }
}