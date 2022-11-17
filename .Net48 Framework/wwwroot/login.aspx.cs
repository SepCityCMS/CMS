// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="login.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class login.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class login : Page
    {
        /// <summary>
        /// Checks the facebook user.
        /// </summary>
        /// <param name="Facebook_Email">The facebook email.</param>
        /// <param name="Facebook_FName">Name of the facebook f.</param>
        /// <param name="Facebook_LName">Name of the facebook l.</param>
        public void Check_Facebook_User(string Facebook_Email, string Facebook_FName, string Facebook_LName)
        {
            if (!string.IsNullOrWhiteSpace(Facebook_Email) && !string.IsNullOrWhiteSpace(Facebook_FName) && !string.IsNullOrWhiteSpace(Facebook_LName))
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT UserName FROM Members WHERE EmailAddress=@EmailAddress AND FirstName=@FirstName AND LastName=@LastName AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@EmailAddress", Facebook_Email);
                        cmd.Parameters.AddWithValue("@FirstName", Facebook_FName);
                        cmd.Parameters.AddWithValue("@LastName", Facebook_LName);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                idLoginErrorMsg.InnerHtml = SepFunctions.LangText("Please login with your account password below to associate FaceBook with your account.");
                                if (SepFunctions.Setup(997, "LoginEmail") == "Yes") UserName.Value = SepFunctions.openNull(RS["EmailAddress"]);
                                else UserName.Value = SepFunctions.openNull(RS["UserName"]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the facebook application identifier.
        /// </summary>
        /// <returns>System.String.</returns>
        public string getFacebookAppId()
        {
            return SepFunctions.Setup(989, "FacebookAPIKey");
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Facebook_Token">The facebook token.</param>
        /// <param name="Facebook_Id">The facebook identifier.</param>
        /// <param name="Facebook_User">The facebook user.</param>
        /// <param name="RememberUser">if set to <c>true</c> [remember user].</param>
        /// <param name="Activate">The activate.</param>
        /// <param name="LinkedInId">The linked in identifier.</param>
        /// <returns>System.String.</returns>
        public string Login_User(string Username, string Password, string Facebook_Token, string Facebook_Id, string Facebook_User, bool RememberUser, string Activate, string LinkedInId)
        {
            long GetAccessClass = 0;
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            string sResponse = SepCommon.DAL.Members.Login(Username, Password, Facebook_Token, Facebook_Id, Facebook_User, SepFunctions.Get_Portal_ID(), false, LinkedInId);
            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (Strings.Left(sResponse, 7) == "USERID:")
            {
                sResponse = Strings.Replace(sResponse, "USERID:", string.Empty);
                string UserID = Strings.Split(sResponse, "||")[0];
                Username = Strings.Split(sResponse, "||")[1];
                Password = Strings.Split(sResponse, "||")[2];
                string AccessKeys = Strings.Split(sResponse, "||")[3];
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", UserID);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", Username);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", Password);
                SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", AccessKeys);

                if (RememberUser)
                {
                    Response.Cookies["UserInfo"].Value = UserID + "||" + Username + "||" + Password + "||" + AccessKeys;
                    Response.Cookies["UserInfo"].Expires = DateAndTime.DateAdd(DateAndTime.DateInterval.Month, 1, DateTime.Today);
                }

                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Login", Username);

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT Status,AccessClass FROM Members WHERE UserID=@UserID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                long UserStatus = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                                GetAccessClass = SepFunctions.toLong(SepFunctions.openNull(RS["AccessClass"]));
                                if (UserStatus == 0)
                                {
                                    return sInstallFolder + "login.aspx?Action=NotActive&Username=" + SepFunctions.UrlEncode(Username);
                                }
                            }
                            else
                            {
                                return sInstallFolder + "login.aspx?LoginError=True&Username=" + SepFunctions.UrlEncode(Username);
                            }
                        }
                    }

                    // Check if user has access to login to a subPortal
                    if (SepFunctions.Get_Portal_ID() > 0 && SepFunctions.ModuleActivated(60))
                        using (var cmd = new SqlCommand("SELECT LoginKeys FROM Portals WHERE PortalID=@PortalID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (SepFunctions.CompareKeys(SepFunctions.openNull(RS["LoginKeys"]), true, UserID) == false)
                                    {
                                        return sInstallFolder + "login.aspx?LoginError=True&Username=" + SepFunctions.UrlEncode(Username);
                                    }
                                }
                            }
                        }

                    // Write to online users table
                    using (var cmd = new SqlCommand("INSERT INTO OnlineUsers(UserID, Location, LoginTime, LastActive, isChatting) VALUES (@UserID, 'Login', @LoginTime, @LastActive, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@LastActive", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("UPDATE Members SET LastLogin=@LastLogin, IPAddress=@IPAddress, Facebook_Token=@Facebook_Token, Facebook_Id=@Facebook_Id, Facebook_User=@Facebook_User, LinkedInId=@LinkedInId WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IPAddress", SepFunctions.GetUserIP());
                        cmd.Parameters.AddWithValue("@Facebook_Token", !string.IsNullOrWhiteSpace(Facebook_Token) ? Facebook_Token : string.Empty);
                        cmd.Parameters.AddWithValue("@Facebook_Id", !string.IsNullOrWhiteSpace(Facebook_Id) ? Facebook_Id : string.Empty);
                        cmd.Parameters.AddWithValue("@Facebook_User", !string.IsNullOrWhiteSpace(Facebook_User) ? Facebook_User : string.Empty);
                        cmd.Parameters.AddWithValue("@LinkedInId", !string.IsNullOrWhiteSpace(LinkedInId) ? LinkedInId : string.Empty);
                        cmd.ExecuteNonQuery();
                    }

                    // Update invoices to a username if a user has purchases products before logging in
                    using (var cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE UserID=@UserID AND Status='0' AND inCart='1'", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                SepCommon.SepCore.Session.setSession(Strings.Left(sSiteName, 5) + "InvoiceID", SepFunctions.openNull(RS["InvoiceID"]));
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Session.getSession(Strings.Left(sSiteName, 5) + "InvoiceID")))
                        using (var cmd = new SqlCommand("UPDATE Invoices SET UserID=@UserID WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@InvoiceID", SepCommon.SepCore.Session.getSession(Strings.Left(sSiteName, 5) + "InvoiceID"));
                            cmd.ExecuteNonQuery();
                        }

                    // Write activity
                    var sActDesc = SepFunctions.LangText("[[Username]] successfully logged into your web site.") + Environment.NewLine;
                    SepFunctions.Activity_Write("LOGIN", sActDesc, GlobalVars.ModuleID, string.Empty, UserID);

                    // Go to purchase a membership if a user is in the Everyone class and there are
                    // available memberships
                    if (GetAccessClass == 1 && SepFunctions.isProfessionalEdition())
                        using (var cmd = new SqlCommand("SELECT ModelNumber FROM ShopProducts WHERE ModuleID='38' ORDER BY UnitPrice", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    return sInstallFolder + "memberships.aspx?DoAction=SelectPlan&UserID=" + SepFunctions.Session_User_ID() + "&";
                                }
                            }
                        }
                }

                if (Activate == "Yes" && !string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupPage"))) return SepFunctions.GetMasterDomain(false) + "/" + SepFunctions.Setup(997, "SignupPage");

                if (!string.IsNullOrWhiteSpace(SepFunctions.Cookie_ReturnUrl()))
                {
                    if (Strings.InStr(SepFunctions.Cookie_ReturnUrl(), "FavoritePageURL=") > 0)
                    {
                        var posa = Strings.InStr(SepFunctions.Cookie_ReturnUrl(), "FavoritePageURL=");
                        return Strings.Replace(SepFunctions.UrlDecode(Strings.Mid(SepFunctions.Cookie_ReturnUrl(), 1, posa + 15)) + SepFunctions.UrlEncode(Strings.Mid(SepFunctions.Cookie_ReturnUrl(), posa + 16, Strings.Len(SepFunctions.Cookie_ReturnUrl()))), "FavoritePageURL=", "PageURL=");
                    }

                    return Strings.Replace(SepFunctions.Cookie_ReturnUrl(), "//", "/");
                }

                if (SepFunctions.Setup(63, "ProfileRequired") == "Yes" && SepFunctions.Setup(997, "SignupAdminApp") != "Yes" && SepFunctions.Setup(63, "ProfilesEnable") == "Enable")
                {
                    var sProfileId = SepCommon.DAL.UserProfiles.Profile_UserID_To_ProfileID(SepFunctions.Session_User_ID());
                    if (sProfileId == 0) return sInstallFolder + "profiles_modify.aspx";
                }

                var sLoginPage = Strings.Trim(SepFunctions.Setup(33, "LoginPage"));
                if (!string.IsNullOrWhiteSpace(sLoginPage))
                {
                    if (Strings.Left(sLoginPage, 5) == "FORM:" && SepFunctions.ModuleActivated(13))
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT FormID FROM Forms WHERE FormID=@FormID AND Available='1' AND FormID NOT IN (SELECT FormID FROM FormAnswers WHERE FormID=@FormID AND UserID=@UserID)", conn))
                            {
                                cmd.Parameters.AddWithValue("@FormID", Strings.Replace(sLoginPage, "FORM:", string.Empty));
                                cmd.Parameters.AddWithValue("@UserID", UserID);
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        return sInstallFolder + "forms.aspx?DoAction=Display&FormID=" + Strings.Replace(sLoginPage, "FORM:", string.Empty);
                                    }
                                }

                                return sInstallFolder + "default.aspx";
                            }
                        }

                    if (SepFunctions.Get_Portal_ID() > 0)
                        using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("SELECT PortalID FROM PortalPages WHERE UserPageName=@UserPageName AND PortalID=@PortalID AND Status=1", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserPageName", Strings.Replace(sLoginPage, "~/", string.Empty));
                                cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    if (RS.HasRows)
                                    {
                                        return sInstallFolder + Strings.Replace(sLoginPage, "~/", string.Empty);
                                    }
                                }

                                return sInstallFolder + "default.aspx";
                            }
                        }

                    return sLoginPage;
                }

                return sInstallFolder + "default.aspx";
            }

            return "NOREDIRECT:" + "<div class=\"alert alert-danger\" role=\"alert\">" + sResponse + "</div>";
        }

        /// <summary>
        /// Translates the page.
        /// </summary>
        public void TranslatePage()
        {
            if (!Page.IsPostBack)
            {
                var sSiteLang = Strings.UCase(SepFunctions.Setup(992, "SiteLang"));
                if (SepFunctions.DebugMode || (sSiteLang != "EN-US" && !string.IsNullOrWhiteSpace(sSiteLang)))
                {
                    UserNameLabel.InnerText = SepFunctions.LangText("User Name:");
                    UserPasswordLabel.InnerText = SepFunctions.LangText("Password:");
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the LoginButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Facebook_Id.Value) && string.IsNullOrWhiteSpace(Facebook_Token.Value) && string.IsNullOrWhiteSpace(Facebook_User.Value) && string.IsNullOrWhiteSpace(LinkedInId.Value))
            {
                if (string.IsNullOrWhiteSpace(UserName.Value))
                {
                    idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("User Name is required.") + "</div>";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Password.Value))
                {
                    idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password is required.") + "</div>";
                    return;
                }
            }

            var sRedirPage = Login_User(UserName.Value, Password.Value, Facebook_Token.Value, Facebook_Id.Value, Facebook_User.Value, RememberMe.Checked, SepCommon.SepCore.Request.Item("Activate"), LinkedInId.Value);
            if (Strings.Left(sRedirPage, 11) != "NOREDIRECT:")
            {
                var returnCookie = SepCommon.SepCore.Session.getCookie("returnUrl");
                if (returnCookie != null && !string.IsNullOrWhiteSpace(returnCookie))
                {
                    SepCommon.SepCore.Session.removeCookie("returnUrl");
                    Response.Redirect(returnCookie);
                }
                else
                {
                    SepFunctions.Redirect(SepFunctions.GetInstallFolder() + "default.aspx");
                }
            }
            else
            {
                idLoginErrorMsg.InnerHtml = Strings.Replace(sRedirPage, "NOREDIRECT:", string.Empty);
                Check_Facebook_User(Facebook_Email.Value, Facebook_FName.Value, Facebook_LName.Value);
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
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
            TranslatePage();

            GlobalVars.ModuleID = 21;

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (SepFunctions.ModuleActivated(68)) SignupPassDiv.Visible = false;

            if (!string.IsNullOrWhiteSpace(Request.Form["UserName"]) && !string.IsNullOrWhiteSpace(Request.Form["Password"]))
            {
                var sRedirPage = Login_User(Request.Form["UserName"], Request.Form["Password"], string.Empty, string.Empty, string.Empty, false, Request.Form["Activate"], string.Empty);
                if (Strings.Left(sRedirPage, 11) != "NOREDIRECT:")
                {
                    var returnCookie = SepCommon.SepCore.Session.getCookie("returnUrl");
                    if (returnCookie != null && !string.IsNullOrWhiteSpace(returnCookie))
                    {
                        SepCommon.SepCore.Session.removeCookie("returnUrl");
                        Response.Redirect(returnCookie);
                    }

                    return;
                }

                idLoginErrorMsg.InnerHtml = Strings.Replace(sRedirPage, "NOREDIRECT:", string.Empty);
            }

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.Setup(997, "LoginEmail") == "Yes") UserNameLabel.InnerText = SepFunctions.LangText("Email Address:");

            // Comes from template.master AccountMenu control
            if (!string.IsNullOrWhiteSpace(Request.Form["UserName2"]) && !string.IsNullOrWhiteSpace(Request.Form["Password2"]) || !string.IsNullOrWhiteSpace(Request.Form["Facebook_Token2"]) && !string.IsNullOrWhiteSpace(Request.Form["Facebook_Id2"]) && !string.IsNullOrWhiteSpace(Request.Form["Facebook_User2"]) || !string.IsNullOrWhiteSpace(Request.Form["LinkedInId2"]))
            {
                var sRedirPage = Login_User(Request.Form["UserName2"], Request.Form["Password2"], Request.Form["Facebook_Token2"], Request.Form["Facebook_Id2"], Request.Form["Facebook_User2"], SepFunctions.toBoolean(Request.Form["RememberMe2"]), Request.Form["Activate2"], Request.Form["LinkedInId2"]);
                if (Strings.Left(sRedirPage, 11) != "NOREDIRECT:")
                {
                    var returnCookie = SepCommon.SepCore.Session.getCookie("returnUrl");
                    if (returnCookie != null && !string.IsNullOrWhiteSpace(returnCookie))
                    {
                        SepCommon.SepCore.Session.removeCookie("returnUrl");
                        Response.Redirect(returnCookie);
                    }
                    else
                    {
                        SepFunctions.Redirect(sInstallFolder + "default.aspx");
                    }

                    return;
                }

                var cReplace = new Replace();

                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

                cReplace.Dispose();

                idLoginErrorMsg.InnerHtml = Strings.Replace(sRedirPage, "NOREDIRECT:", string.Empty);
                Facebook_Token.Value = Request.Form["Facebook_Token2"];
                Facebook_Id.Value = Request.Form["Facebook_Id2"];
                Facebook_User.Value = Request.Form["Facebook_User2"];
                Facebook_Email.Value = Request.Form["Facebook_Email2"];
                Facebook_FName.Value = Request.Form["Facebook_FName2"];
                Facebook_LName.Value = Request.Form["Facebook_LName2"];
                Check_Facebook_User(Facebook_Email.Value, Facebook_FName.Value, Facebook_LName.Value);
                return;
            }

            if (IsPostBack)
            {
                if ((!string.IsNullOrWhiteSpace(Facebook_Token.Value) || string.IsNullOrWhiteSpace(LinkedInId.Value)) && string.IsNullOrWhiteSpace(UserName.Value) && string.IsNullOrWhiteSpace(Password.Value))
                {
                    var sRedirPage = Login_User(string.Empty, string.Empty, Facebook_Token.Value, Facebook_Id.Value, Facebook_User.Value, false, SepCommon.SepCore.Request.Item("Activate"), LinkedInId.Value);
                    if (Strings.Left(sRedirPage, 11) != "NOREDIRECT:")
                    {
                        var returnCookie = SepCommon.SepCore.Session.getCookie("returnUrl");
                        if (returnCookie != null && !string.IsNullOrWhiteSpace(returnCookie))
                        {
                            SepCommon.SepCore.Session.removeCookie("returnUrl");
                            Response.Redirect(returnCookie);
                        }
                        else
                        {
                            SepFunctions.Redirect(sInstallFolder + "default.aspx");
                        }
                    }
                    else
                    {
                        idLoginErrorMsg.InnerHtml = Strings.Replace(sRedirPage, "NOREDIRECT:", string.Empty);
                        Check_Facebook_User(Facebook_Email.Value, Facebook_FName.Value, Facebook_LName.Value);
                    }
                }
            }
            else
            {
                var cReplace = new Replace();

                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

                cReplace.Dispose();

                if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey")))
                {
                    FacebookRow.Visible = false;
                    Facebook_Token.Visible = false;
                    Facebook_Id.Visible = false;
                    Facebook_User.Visible = false;
                    Facebook_Email.Visible = false;
                    Facebook_FName.Visible = false;
                    Facebook_LName.Visible = false;
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInAPI")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "LinkedInSecret")))
                {
                    LinkedInRow.Visible = false;
                    LinkedInId.Visible = false;
                }

                if (SepCommon.SepCore.Request.Item("Activate") == "Yes" && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserID")))
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        using (var cmd = new SqlCommand("UPDATE Members SET Status=1 WHERE UserID=@UserID AND Status = '0'", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", SepCommon.SepCore.Request.Item("UserID"));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    idLoginErrorMsg.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("You have successfully activated your account. Please login below") + "</div>";
                }
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }
}