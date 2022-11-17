// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="signup.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using ASPSnippets.LinkedInAPI;
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using wwwroot.BusinessObjects;

    /// <summary>
    /// Class usersignup.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class usersignup : Page
    {
        /// <summary>
        /// Classes the selection.
        /// </summary>
        public void Class_Selection()
        {
            var sStartClass = SepFunctions.toLong(SepFunctions.Setup(997, "StartupClass"));

            long aa = 0;
            string[] arrClassID = null;

            var sClassID = string.Empty;
            var sClassName = string.Empty;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                string sSel;
                if (SepFunctions.Setup(997, "SignupSelMem") == "Yes")
                    using (var cmd = new SqlCommand("SELECT ClassID,ClassName,Description FROM AccessClasses WHERE ClassID <> '1' AND ClassID <> '2' AND PrivateClass='0' AND (PortalIDs LIKE '%" + SepFunctions.Get_Portal_ID() + "%' OR PortalIDs LIKE '%|-1|%') AND Status <> '-1' ORDER BY ClassName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                MembershipSelection.InnerHtml += "<p>";
                                MembershipSelection.InnerHtml += "<label>" + SepFunctions.LangText("Select a Membership:") + "</label>";
                                MembershipSelection.InnerHtml += "<table>";
                                while (RS.Read())
                                {
                                    using (var cmd2 = new SqlCommand("SELECT UnitPrice,RecurringPrice,RecurringCycle FROM ShopProducts WHERE ModuleID='38' AND ModelNumber LIKE '%-" + SepFunctions.FixWord(SepFunctions.openNull(RS["ClassID"])) + "' ORDER BY UnitPrice", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (!RS2.HasRows)
                                            {
                                                aa += 1;
                                                if (aa == 1)
                                                    sSel = " checked=\"checked\"";
                                                else
                                                    sSel = string.Empty;
                                                MembershipSelection.InnerHtml += "<tr>";
                                                MembershipSelection.InnerHtml += "<td><input type=\"radio\" name=\"UserClass\" value=\"" + SepFunctions.openNull(RS["ClassID"]) + "\"" + sSel + " /></td>";
                                                MembershipSelection.InnerHtml += "<td>" + SepFunctions.openNull(RS["ClassName"]) + "</td>";
                                                MembershipSelection.InnerHtml += "<td style=\"padding-left: 20px;\">" + SepFunctions.LangText("FREE") + "</td>";
                                                MembershipSelection.InnerHtml += "</tr>";
                                            }
                                        }
                                    }
                                }

                                MembershipSelection.InnerHtml += "</table>";
                                MembershipSelection.InnerHtml += "</p>";
                                if (aa == 0)
                                {
                                    MembershipSelection.InnerHtml = "";
                                }
                            }

                        }
                    }
                else
                    using (var cmd = new SqlCommand("SELECT AC.ClassID,SP.ProductID,SP.ModelNumber,SP.ProductName,SP.UnitPrice,SP.RecurringPrice,SP.RecurringCycle,SP.Description FROM AccessClasses AS AC,ShopProducts AS SP WHERE SP.ModuleID='38' AND SP.ModelNumber LIKE '%-'+Cast(AC.ClassID AS varchar) AND SP.ProductName LIKE 'Class Name%' AND (AC.PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR AC.PortalIDs LIKE '%|-1|%') AND AC.Status <> '-1' ORDER BY SP.ProductName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                MembershipSelection.InnerHtml += "<p>";
                                MembershipSelection.InnerHtml += "<label>" + SepFunctions.LangText("Select a Membership:") + "</label>";
                                MembershipSelection.InnerHtml += "<table>";

                                // =================== Find Free Membership ==================
                                using (var cmd2 = new SqlCommand("SELECT ClassID,ClassName,Description FROM AccessClasses WHERE ClassID='" + SepFunctions.toLong(Strings.ToString(sStartClass)) + "' AND (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR PortalIDs LIKE '%|-1|%')", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        while (RS2.Read())
                                        {
                                            sClassID = SepFunctions.openNull(RS2["ClassID"]);
                                            sClassName = SepFunctions.openNull(RS2["ClassName"]);
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(sClassName))
                                    using (var cmd2 = new SqlCommand("SELECT AC.ClassID FROM AccessClasses AS AC,ShopProducts AS SP WHERE SP.ModuleID='38' AND SP.ModelNumber LIKE '%-'+Cast(AC.ClassID AS varchar) AND SP.ProductName LIKE 'Class Name%' AND AC.ClassID='" + SepFunctions.toLong(Strings.ToString(sStartClass)) + "' AND (AC.PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR AC.PortalIDs LIKE '%|-1|%')", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows == false)
                                            {
                                                aa += 1;
                                                sSel = " checked=\"checked\"";
                                                MembershipSelection.InnerHtml += "<tr>";
                                                MembershipSelection.InnerHtml += "<td><input type=\"radio\" name=\"UserClass\" value=\"" + sClassID + "\"" + sSel + " /></td>";
                                                MembershipSelection.InnerHtml += "<td>" + sClassName + "</td>";
                                                MembershipSelection.InnerHtml += "<td style=\"padding-left: 20px;\">" + SepFunctions.LangText("FREE") + "</td>";
                                                MembershipSelection.InnerHtml += "</tr>";
                                            }
                                        }
                                    }

                                // ===========================================================
                                while (RS.Read())
                                {
                                    arrClassID = Strings.Split(SepFunctions.openNull(RS["ModelNumber"]), "-");
                                    Array.Resize(ref arrClassID, 2);
                                    using (var cmd2 = new SqlCommand("SELECT ClassID,ClassName,Description FROM AccessClasses WHERE ClassID='" + SepFunctions.FixWord(arrClassID[1]) + "' AND PrivateClass='0'", conn))
                                    {
                                        using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                        {
                                            if (RS2.HasRows)
                                            {
                                                RS2.Read();
                                                aa += 1;
                                                if (aa == 1)
                                                    sSel = " checked=\"checked\"";
                                                else
                                                    sSel = string.Empty;
                                                MembershipSelection.InnerHtml += "<tr>";
                                                MembershipSelection.InnerHtml += "<td><input type=\"radio\" name=\"UserClass\" value=\"" + SepFunctions.openNull(RS2["ClassID"]) + "\"" + sSel + " /></td>";
                                                MembershipSelection.InnerHtml += "<td>" + SepFunctions.openNull(RS2["ClassName"]) + "</td>";
                                                MembershipSelection.InnerHtml += "<td style=\"padding-left: 20px;\">" + Memberships_Pricing(SepFunctions.toDecimal(SepFunctions.openNull(RS["UnitPrice"])), SepFunctions.toDecimal(SepFunctions.openNull(RS["RecurringPrice"])), SepFunctions.openNull(RS["RecurringCycle"])) + "</td>";
                                                MembershipSelection.InnerHtml += "</tr>";
                                            }
                                        }
                                    }
                                }

                                MembershipSelection.InnerHtml += "</table>";
                                if (aa == 0)
                                {
                                    MembershipSelection.InnerHtml = "";
                                }
                            }

                        }
                    }
            }
        }

        /// <summary>
        /// Credits the selection.
        /// </summary>
        public void Credit_Selection()
        {
            if (SepFunctions.Setup(1, "ShowCreditsSignup") != "No")
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ProductID,Inventory,UnitPrice FROM ShopProducts WHERE ModuleID='973' ORDER BY ProductName", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                CreditSelection.InnerHtml += "<p>";
                                CreditSelection.InnerHtml += "<label>" + SepFunctions.LangText("Purchase Credits:") + "</label>";
                                CreditSelection.InnerHtml += "<table>";
                                CreditSelection.InnerHtml += "<tr>";
                                CreditSelection.InnerHtml += "<td><input type=\"radio\" name=\"UserCredits\" value=\"0\" checked=\"checked\" /></td>";
                                CreditSelection.InnerHtml += "<td>" + SepFunctions.LangText("None") + "</td>";
                                CreditSelection.InnerHtml += "<td>(" + SepFunctions.LangText("FREE") + ")</td>";
                                CreditSelection.InnerHtml += "</tr>";
                                while (RS.Read())
                                {
                                    CreditSelection.InnerHtml += "<tr>";
                                    CreditSelection.InnerHtml += "<td><input type=\"radio\" name=\"UserCredits\" value=\"" + SepFunctions.openNull(RS["ProductID"]) + "\" /></td>";
                                    CreditSelection.InnerHtml += "<td>" + SepFunctions.toLong(SepFunctions.openNull(RS["Inventory"])) + " " + SepFunctions.LangText("Credits") + "</td>";
                                    CreditSelection.InnerHtml += "<td>(" + SepFunctions.Format_Currency(SepFunctions.openNull(RS["UnitPrice"])) + ")</td>";
                                    CreditSelection.InnerHtml += "</tr>";
                                }

                                CreditSelection.InnerHtml += "</table>";
                                CreditSelection.InnerHtml += "</p>";
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
        public void Login_User(string Username, string Password)
        {
            var sResponse = SepCommon.DAL.Members.Login(Username, Password, string.Empty, string.Empty, string.Empty, SepFunctions.Get_Portal_ID(), false, string.Empty);

            if (Strings.Left(sResponse, 7) == "USERID:")
            {
                var sSiteName = SepFunctions.Setup(992, "WebSiteName");
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    sResponse = Strings.Replace(sResponse, "USERID:", string.Empty);
                    string UserID = Strings.Split(sResponse, "||")[0];
                    Username = Strings.Split(sResponse, "||")[1];
                    Password = Strings.Split(sResponse, "||")[2];
                    string AccessKeys = Strings.Split(sResponse, "||")[3];
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "UserID", UserID);
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Username", Username);
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "Password", Password);
                    SepCommon.SepCore.Session.setSession(Strings.Left(Strings.Replace(sSiteName, " ", string.Empty), 5) + "AccessKeys", AccessKeys);

                    // Write to online users table
                    using (var cmd = new SqlCommand("INSERT INTO OnlineUsers(UserID, Location, LoginTime, LastActive, isChatting) VALUES (@UserID, 'Login', @LoginTime, @LastActive, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@LastActive", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    // Update invoices to a username if a user has purchases products before logging in
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Session.getSession("InvoiceID")))
                        using (var cmd = new SqlCommand("UPDATE Invoices SET UserID=@UserID WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.Parameters.AddWithValue("@InvoiceID", SepCommon.SepCore.Session.getSession("InvoiceID"));
                            cmd.ExecuteNonQuery();
                        }

                    // Write activity
                    var sActDesc = SepFunctions.LangText("[[Username]] successfully logged into your web site.") + Environment.NewLine;
                    SepFunctions.Activity_Write("LOGIN", sActDesc, GlobalVars.ModuleID, string.Empty, UserID);
                }
            }
        }

        /// <summary>
        /// Membershipses the pricing.
        /// </summary>
        /// <param name="strUnitPrice">The string unit price.</param>
        /// <param name="strRecurringPrice">The string recurring price.</param>
        /// <param name="strRecurringCycle">The string recurring cycle.</param>
        /// <returns>System.String.</returns>
        public string Memberships_Pricing(decimal strUnitPrice, decimal strRecurringPrice, string strRecurringCycle)
        {
            var str = new StringBuilder();

            if (strUnitPrice != 0)
            {
                if (strRecurringPrice != 0)
                    str.Append(SepFunctions.LangText("Setup Fee:") + " ");
                else
                    str.Append(SepFunctions.LangText("Price:") + " ");
                str.Append(SepFunctions.Format_Currency(strUnitPrice) + "<br/>");
            }

            if (strRecurringPrice != 0 && strUnitPrice != 0) str.Append(SepFunctions.LangText("Recurring Fee:") + " ");
            if (strRecurringPrice != 0)
                switch (strRecurringCycle)
                {
                    case "3m":
                        str.Append(SepFunctions.LangText("~~" + SepFunctions.Format_Currency(strRecurringPrice) + "~~/3 months"));
                        break;

                    case "6m":
                        str.Append(SepFunctions.LangText("~~" + SepFunctions.Format_Currency(strRecurringPrice) + "~~/6 months"));
                        break;

                    case "1y":
                        str.Append(SepFunctions.LangText("~~" + SepFunctions.Format_Currency(strRecurringPrice) + "~~/year"));
                        break;

                    default:
                        str.Append(SepFunctions.LangText("~~" + SepFunctions.Format_Currency(strRecurringPrice) + "~~/month"));
                        break;
                }

            return Strings.ToString(str);
        }

        /// <summary>
        /// Saves the member.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="PostalCode">The postal code.</param>
        /// <param name="State">The state.</param>
        /// <param name="Country">The country.</param>
        /// <param name="Gender">The gender.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="Birthdate">The birthdate.</param>
        /// <param name="PayPalEmail">The pay pal email.</param>
        /// <param name="ReferralLookupField">The referral lookup field.</param>
        /// <param name="Referral">The referral.</param>
        /// <param name="SecretQuestion">The secret question.</param>
        /// <param name="SecretAnswer">The secret answer.</param>
        /// <param name="Facebook_Token">The facebook token.</param>
        /// <param name="Facebook_Id">The facebook identifier.</param>
        /// <param name="Facebook_User">The facebook user.</param>
        /// <param name="InviteID">The invite identifier.</param>
        /// <param name="SiteID">The site identifier.</param>
        public void Save_Member(string UserName, string Password, string EmailAddress, string FirstName, string LastName, string StreetAddress, string City, string PostalCode, string State, string Country, int Gender, string PhoneNumber, DateTime Birthdate, string PayPalEmail, string ReferralLookupField, string Referral, string SecretQuestion, string SecretAnswer, string Facebook_Token, string Facebook_Id, string Facebook_User, string InviteID, long SiteID)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var sUserID = SepFunctions.Generate_GUID();
            var sUserName = UserName;
            var sUserPrefix = string.Empty;
            var sReferralID = Referral;

            var sStartClass = SepFunctions.toLong(SepFunctions.Setup(997, "StartupClass"));
            var sStartKeys = "|1|";
            var sProductID = string.Empty;

            var GoToShopCart = false;
            var JoinLetters = string.Empty;
            var foundInviteId = false;
            var sSiteName = SepFunctions.Setup(992, "WebSiteName");

            // Get the start up class
            if (SepFunctions.Get_Portal_ID() > 0)
                if (SepFunctions.toLong(SepFunctions.PortalSetup("StartClass")) > 0)
                    sStartClass = SepFunctions.toLong(SepFunctions.PortalSetup("StartClass"));
            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("UserClass"))) sStartClass = SepFunctions.toLong(SepCommon.SepCore.Request.Item("UserClass"));

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Invite
                if (!string.IsNullOrWhiteSpace(InviteID))
                    using (var cmd = new SqlCommand("SELECT ClassID FROM MembersInvite WHERE InviteID=@InviteID", conn))
                    {
                        cmd.Parameters.AddWithValue("@InviteID", InviteID);
                        using (SqlDataReader RS3 = cmd.ExecuteReader())
                        {
                            if (RS3.HasRows)
                            {
                                RS3.Read();
                                foundInviteId = true;
                                sStartClass = SepFunctions.toLong(SepFunctions.openNull(RS3["ClassID"]));
                                using (var cmd2 = new SqlCommand("DELETE FROM MembersInvite WHERE InviteID=@InviteID", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@InviteID", InviteID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                string FromEmailAddress;
                // Get referrer (Affiliate) information
                if (!string.IsNullOrWhiteSpace(ReferralLookupField))
                    using (var cmd = new SqlCommand("SELECT AffiliateID,Username,EmailAddress,UserID FROM Members WHERE " + SepFunctions.FixWord(ReferralLookupField) + "='" + SepFunctions.FixWord(Referral) + "' AND Status=1", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == false)
                            {
                                if (SepFunctions.Setup(39, "AffiliateSignup") == "Yes" && SepFunctions.Setup(39, "AffiliateEnable") == "Enable" && SepFunctions.Setup(39, "AffiliateIDReq") == "Yes")
                                {
                                    ErrorMessage.InnerHtml += "<p class=\"errorMsg\">" + SepFunctions.LangText("You have entered an invalid affiliate.") + "</p>";
                                    return;
                                }
                            }
                            else
                            {
                                RS.Read();
                                sReferralID = SepFunctions.openNull(RS["AffiliateID"]);
                                FromEmailAddress = SepFunctions.Setup(GlobalVars.ModuleID, "AdminEmailAddress");
                                if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
                                {
                                    FromEmailAddress = SepFunctions.PortalSetup("EmailAddress");
                                }

                                SepFunctions.Send_Email(SepFunctions.openNull(RS["EmailAddress"]), FromEmailAddress, UserName + " has joined under your affiliate ID.", UserName + " has joined under your affiliate ID.", GlobalVars.ModuleID);
                                SepFunctions.Activity_Write("AFFILIATEREFERRAL", SepFunctions.LangText("~~" + SepFunctions.openNull(RS["Username"]) + "~~ has referred user to your web site.") + Environment.NewLine, GlobalVars.ModuleID, sReferralID, SepFunctions.openNull(RS["UserID"]));
                            }

                        }
                    }

                if (!string.IsNullOrWhiteSpace(Strings.ToString(Context.Session["ReferralID"])))
                    sReferralID = Context.Session["ReferralID"].ToString();

                if (foundInviteId == false)
                    using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ModuleID='38' AND ModelNumber LIKE '%-" + SepFunctions.FixWord(Strings.ToString(sStartClass)) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                GoToShopCart = true;
                                sProductID = SepFunctions.openNull(RS["ProductID"]);
                            }

                        }
                    }

                int sUserStatus;
                if (GoToShopCart)
                {
                    sUserStatus = 0;
                    sStartKeys = "|1|";
                    sStartClass = 1;
                }
                else
                {
                    sUserStatus = 1;
                    using (var cmd = new SqlCommand("SELECT KeyIDs FROM AccessClasses WHERE ClassID='" + SepFunctions.FixWord(Strings.ToString(sStartClass)) + "'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == false)
                            {
                                sStartKeys = "|1|";
                            }
                            else
                            {
                                RS.Read();
                                sStartKeys = SepFunctions.openNull(RS["KeyIDs"]);
                            }

                        }
                    }

                    if (sStartClass == 2)
                    {
                        sStartClass = 1;
                        sStartKeys = "|1|";
                    }
                }

                if (foundInviteId == false)
                {
                    if (SepFunctions.Setup(997, "SignupAdminApp") == "Yes") sUserStatus = 0;

                    if (SepFunctions.Setup(GlobalVars.ModuleID, "SignupVerify") == "Yes") sUserStatus = 0;
                }

                if (SepFunctions.Setup(63, "ProfileRequired") == "Yes" && SepFunctions.Setup(997, "SignupAdminApp") != "Yes" && SepFunctions.Setup(63, "ProfilesEnable") == "Enable") sUserStatus = 1;

                SepFunctions.Activity_Write("SIGNUP", SepFunctions.LangText("Account successfully created") + Environment.NewLine, GlobalVars.ModuleID, string.Empty, sUserID);

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "AutoUser")))
                {
                    using (var cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='SignupAutoUser'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == false)
                            {
                                using (var cmd2 = new SqlCommand("INSERT INTO Scripts (ScriptType,Description,ScriptText,DatePosted,ModuleIDs,CatIDs,UserID,PortalIDs) VALUES('SignupAutoUser','Signup Auto-Generated User','1',GETDATE(),'0','0','0','0')", conn))
                                {
                                    cmd2.ExecuteNonQuery();
                                }

                                sUserPrefix = "0001";
                            }
                            else
                            {
                                RS.Read();
                                sUserPrefix = Strings.ToString(SepFunctions.toLong(SepFunctions.openNull(RS["ScriptText"])) + 1);
                                using (var cmd2 = new SqlCommand("UPDATE Scripts SET ScriptText=@ScriptText WHERE ScriptType='SignupAutoUser'", conn))
                                {
                                    cmd2.Parameters.AddWithValue("@ScriptText", sUserPrefix);
                                    cmd2.ExecuteNonQuery();
                                }

                                if (Strings.Len(sUserPrefix) == 1)
                                    sUserPrefix = "000" + sUserPrefix;
                                if (Strings.Len(sUserPrefix) == 2)
                                    sUserPrefix = "00" + sUserPrefix;
                                if (Strings.Len(sUserPrefix) == 3)
                                    sUserPrefix = "0" + sUserPrefix;
                            }

                        }
                    }

                    sUserName = SepFunctions.Setup(997, "AutoUser") + Strings.Right(Strings.ToString(DateAndTime.Year(DateTime.Today)), 2) + sUserPrefix;
                }
                else
                {
                    if (SepFunctions.Setup(997, "LoginEmail") == "Yes") sUserName = Strings.Trim(Strings.Left(Strings.Split(EmailAddress, "@")[0] + Strings.Left(Strings.ToString(SepFunctions.GetIdentity()), 5), 25));
                    sUserName = Strings.Trim(Strings.Left(sUserName, 25));
                }

                if (SepFunctions.isUserPage())
                {
                    using (var cmd = new SqlCommand("SELECT SiteID FROM UPagesSites WHERE UserID=@UserID AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.GetUserID(SepCommon.SepCore.Request.Item("UserName")));
                        using (SqlDataReader SiteRS = cmd.ExecuteReader())
                        {
                            if (SiteRS.HasRows)
                            {
                                SiteRS.Read();
                                SiteID = SepFunctions.toLong(SepFunctions.openNull(SiteRS["SiteID"]));
                            }
                        }
                    }
                }

                SepCommon.DAL.Members.Member_Save(sUserID, sUserName, Password, SecretQuestion, SecretAnswer, FirstName, LastName, StreetAddress, City, State, PostalCode, Country, EmailAddress, PhoneNumber, PayPalEmail, SepFunctions.Points_Setup("GetCreateAccount"), sStartClass, sStartKeys, Birthdate, Gender, sReferralID, string.Empty, SepFunctions.Get_Portal_ID(), Friends.Value, Request.Form["LetterIDs"], sUserStatus, Facebook_Token, Facebook_Id, Facebook_User, false, SiteID, LinkedInID.Value);

                // Save Custom Fields
                var customXML = string.Empty;
                using (var cmd = new SqlCommand("SELECT FieldID FROM CustomFields WHERE ModuleIDs LIKE '%|29|%' AND (PortalIDs LIKE '%|" + SepFunctions.Get_Portal_ID() + "|%' OR datalength(PortalIDs) = 0)", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read()) customXML += "<Custom" + SepFunctions.openNull(RS["FieldID"]) + ">" + SepFunctions.HTMLEncode(SepCommon.SepCore.Request.Item("Custom" + SepFunctions.openNull(RS["FieldID"]))) + "</Custom" + SepFunctions.openNull(RS["FieldID"]) + ">";
                    }
                }

                SepCommon.DAL.CustomFields.Answers_Save(sUserID, 29, 29, 0, customXML);

                // End Custom Fields
                using (var cCRM = new CRM())
                {
                    cCRM.Create_User(CRM.WhenToWriteUser.NewSignup);
                }

                using (var cmd = new SqlCommand("SELECT LetterID FROM Newsletters ORDER BY NewsletName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                            if (SepFunctions.toLong(SepCommon.SepCore.Request.Item(SepFunctions.openNull(RS["LetterID"]))) == 1)
                                using (var cmd2 = new SqlCommand("SELECT * FROM NewslettersUsers WHERE EmailAddress='" + SepFunctions.FixWord(EmailAddress) + "' AND LetterID='" + SepFunctions.openNull(RS["LetterID"], true) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows == false)
                                        {
                                            using (var cmd3 = new SqlCommand("INSERT INTO NewslettersUsers (NUserID, LetterID, UserID, EmailAddress, Status) VALUES('" + SepFunctions.GetIdentity() + "','" + SepFunctions.openNull(RS["LetterID"], true) + "','" + SepFunctions.FixWord(sUserID) + "','" + SepFunctions.FixWord(EmailAddress) + "', '0')", conn))
                                            {
                                                cmd3.ExecuteNonQuery();
                                            }

                                            JoinLetters += SepFunctions.openNull(RS["NewsletName"]) + "<br/>";
                                        }
                                    }
                                }

                    }
                }

                if (foundInviteId == false)
                    if (SepFunctions.toLong(SepCommon.SepCore.Request.Item("UserCredits")) > 0)
                        using (var cmd = new SqlCommand("SELECT ProductID FROM ShopProducts WHERE ModuleID='973' AND ProductID='" + SepFunctions.FixWord(SepCommon.SepCore.Request.Item("UserCredits")) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GoToShopCart = true;
                                    sProductID = SepFunctions.openNull(RS["ProductID"]);
                                }

                            }
                        }

                Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "Signup", sUserName);

                if (SepFunctions.Cookie_ReturnUrl() == "viewcart.aspx")
                {
                    if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Session.getSession(Strings.Left(sSiteName, 5) + "InvoiceID")))
                        using (var cmd = new SqlCommand("UPDATE Invoices SET UserID=@UserID WHERE InvoiceID=@InvoiceID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", sUserID);
                            cmd.Parameters.AddWithValue("@InvoiceID", SepCommon.SepCore.Session.getSession(Strings.Left(sSiteName, 5) + "InvoiceID"));
                            cmd.ExecuteNonQuery();
                        }

                    Login_User(sUserName, Password);
                    SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
                    return;
                }

                if (GoToShopCart)
                {
                    Login_User(sUserName, Password);

                    SepCommon.DAL.Invoices.Invoice_Save(SepFunctions.toLong(SepFunctions.Session_Invoice_ID()), SepFunctions.Session_User_ID(), 0, DateTime.Now, 38, sProductID, "1", string.Empty, string.Empty, false, string.Empty, "0", string.Empty, string.Empty, string.Empty, 0, 0, SepFunctions.Get_Portal_ID());

                    SepFunctions.Redirect(sInstallFolder + "viewcart.aspx");
                    return;
                }


                string ToEmailAddress;
                string EmailBody;
                string EmailSubject;
                if (foundInviteId == false)
                {
                    if (SepFunctions.Setup(997, "SignupAdminApp") == "Yes")
                    {
                        ToEmailAddress = SepFunctions.Setup(GlobalVars.ModuleID, "AdminEmailAddress");
                        if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
                        {
                            ToEmailAddress = SepFunctions.PortalSetup("EmailAddress");
                        }

                        EmailSubject = SepFunctions.LangText("Account waiting for approval at ~~" + sSiteName + "~~");
                        EmailBody = SepFunctions.LangText("The following account information is waiting approval.") + "<br/><br/>" + "<b>" + SepFunctions.LangText("Username") + "</b> " + sUserName + "<br/><br/>" + SepFunctions.LangText("If you have any questions regarding this email or your account, please feel free to email us by responding to this message.");
                        SepFunctions.Send_Email(ToEmailAddress, EmailAddress, EmailSubject, EmailBody, GlobalVars.ModuleID);
                    }

                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupAREmail")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupARSubject")) && !string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupARBody")) && SepFunctions.Setup(997, "SignupVerify") != "Yes") SepFunctions.Send_Email(EmailAddress, SepFunctions.Setup(997, "SignupAREmail"), SepFunctions.Replace_Fields(SepFunctions.Setup(997, "SignupARSubject"), sUserID), SepFunctions.Replace_Fields(SepFunctions.Setup(997, "SignupARBody"), sUserID), GlobalVars.ModuleID);
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(GlobalVars.ModuleID, "EmailAdminNew")))
                {
                    ToEmailAddress = SepFunctions.Setup(GlobalVars.ModuleID, "EmailAdminNew");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
                    {
                        ToEmailAddress = SepFunctions.PortalSetup("EmailAddress");
                    }

                    FromEmailAddress = EmailAddress;
                    EmailSubject = SepFunctions.LangText("New user has signed up at") + " " + sSiteName;
                    EmailBody = SepFunctions.LangText("A new user has signed up with the following information") + "<br/><b>" + SepFunctions.LangText("Username") + "</b> " + sUserName + "<br/><b>" + SepFunctions.LangText("Email Address") + "</b> " + EmailAddress + "<br/><b>" + SepFunctions.LangText("Full Name") + "</b> " + FirstName + " " + LastName;
                    SepFunctions.Send_Email(ToEmailAddress, FromEmailAddress, EmailSubject, EmailBody, GlobalVars.ModuleID);
                }

                if (SepFunctions.Setup(GlobalVars.ModuleID, "SignupVerify") == "Yes" && SepFunctions.Setup(997, "SignupAdminApp") != "Yes" && foundInviteId == false && sUserStatus == 0)
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "EmailAdminNew")))
                        FromEmailAddress = SepFunctions.Setup(997, "EmailAdminNew");
                    else
                        FromEmailAddress = SepFunctions.Setup(991, "AdminEmailAddress");
                    if (!string.IsNullOrWhiteSpace(SepFunctions.PortalSetup("EmailAddress")) && SepFunctions.Get_Portal_ID() > 0)
                    {
                        FromEmailAddress = SepFunctions.PortalSetup("EmailAddress");
                    }

                    EmailSubject = SepFunctions.LangText("Login information for") + " " + sSiteName;
                    var siteDomain = SepFunctions.GetSiteDomain();
                    string strRegisterURL = siteDomain + "login.aspx?Activate=Yes&UserID=" + SepFunctions.UrlEncode(sUserID);
                    if (!string.IsNullOrWhiteSpace(JoinLetters)) JoinLetters = "By cliking the link below you agree that you will be signing up for the following newsletters.<br/><br/>" + JoinLetters + "<br/>";
                    EmailBody = SepFunctions.LangText("Thank you for registering with us! The process is almost complete - all you need to do is click the link below. " + JoinLetters + "If you cannot click links within your email, you may need to copy and paste the link to the address bar of your web browser. This process will confirm your registration with our website, after which you will be able to login.") + "<br/><br/><a href=\"" + strRegisterURL + "\" target=\"_blank\">" + strRegisterURL + "</a><br/><br/>";
                    if (SepFunctions.Setup(991, "LoginEmail") == "Yes")
                        EmailBody += "<b>" + SepFunctions.LangText("Email Address") + "</b> " + EmailAddress;
                    else
                        EmailBody += "<b>" + SepFunctions.LangText("Username") + "</b> " + sUserName;
                    EmailBody += "<br/><br/>" + SepFunctions.LangText("If you have any questions regarding this email or your account, please feel free to email us by responding to this message.");
                    SepFunctions.Send_Email(EmailAddress, FromEmailAddress, EmailSubject, EmailBody, GlobalVars.ModuleID);
                }
                else
                {
                    Login_User(sUserName, Password);
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Cookie_ReturnUrl()))
                    {
                        SepFunctions.Redirect(SepFunctions.Cookie_ReturnUrl());
                        return;
                    }
                }

                if (SepFunctions.Setup(13, "FormsEnable") == "Enable" && !string.IsNullOrWhiteSpace(SepFunctions.Setup(13, "FormsSignup")) && SepFunctions.Get_Portal_ID() == 0)
                    using (var cmd = new SqlCommand("SELECT FormID,Title FROM Forms WHERE FormID='" + SepFunctions.FixWord(Strings.Replace(SepFunctions.Setup(13, "FormsSignup"), "FORM:", string.Empty)) + "' AND Available='1'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                using (var cmd2 = new SqlCommand("SELECT FormID FROM FormAnswers WHERE FormID='" + SepFunctions.FixWord(Strings.Replace(SepFunctions.Setup(13, "FormsSignup"), "FORM:", string.Empty)) + "' AND UserID='" + SepFunctions.FixWord(sUserID) + "'", conn))
                                {
                                    using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                    {
                                        if (RS2.HasRows) SepFunctions.Redirect(sInstallFolder + "forms/" + SepFunctions.openNull(RS["FormID"]) + "/" + SepFunctions.Format_ISAPI(SepFunctions.openNull(RS["Title"])) + "/");
                                    }
                                }
                            }

                        }
                    }

                if (SepFunctions.Setup(63, "ProfileRequired") == "Yes" && SepFunctions.Setup(997, "SignupAdminApp") != "Yes" && SepFunctions.Setup(63, "ProfilesEnable") == "Enable")
                {
                    Login_User(sUserName, Password);
                    SepFunctions.Redirect(sInstallFolder + "profiles_modify.aspx");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupPage")) && SepFunctions.Setup(GlobalVars.ModuleID, "SignupVerify") != "Yes")
            {
                SepFunctions.Redirect(SepFunctions.GetMasterDomain(false) + SepFunctions.Setup(997, "SignupPage").Replace("~/", string.Empty));
            }
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
                    SiteId.Items[0].Text = SepFunctions.LangText("Select a User Site");
                    SecretQuestion.Items[0].Text = SepFunctions.LangText("Name of your favorite pet?");
                    SecretQuestion.Items[1].Text = SepFunctions.LangText("In what city were you born?");
                    SecretQuestion.Items[2].Text = SepFunctions.LangText("What high school did you attend?");
                    SecretQuestion.Items[3].Text = SepFunctions.LangText("Your favorite movie?");
                    SecretQuestion.Items[4].Text = SepFunctions.LangText("Your mother's maiden name?");
                    SecretQuestion.Items[5].Text = SepFunctions.LangText("What street did you grow up on?");
                    SecretQuestion.Items[6].Text = SepFunctions.LangText("Make of your first car?");
                    SecretQuestion.Items[7].Text = SepFunctions.LangText("When is your anniversary?");
                    SecretQuestion.Items[8].Text = SepFunctions.LangText("What is your favorite color?");
                    Gender.Items[0].Text = SepFunctions.LangText("Male");
                    Gender.Items[1].Text = SepFunctions.LangText("Female");
                    Friends.Items[0].Text = SepFunctions.LangText("Yes");
                    Friends.Items[1].Text = SepFunctions.LangText("No");
                    ReferralOptions.Items[0].Text = SepFunctions.LangText("Referral Username");
                    ReferralOptions.Items[1].Text = SepFunctions.LangText("Referral ID Number");
                    ReferralOptions.Items[2].Text = SepFunctions.LangText("Referral Email Address");
                    ModifyLegend.InnerHtml = SepFunctions.LangText("Register Now");
                    SiteIdLabel.InnerText = SepFunctions.LangText("Select a Member Group:");
                    UserNameLabel.InnerText = SepFunctions.LangText("User Name:");
                    PasswordLabel.InnerText = SepFunctions.LangText("Enter a Password:");
                    RePasswordLabel.InnerText = SepFunctions.LangText("Re-enter a Password:");
                    SecretQuestionLabel.InnerText = SepFunctions.LangText("Secret Question:");
                    SecretAnswerLabel.InnerText = SepFunctions.LangText("Secret Answer:");
                    EmailAddressLabel.InnerText = SepFunctions.LangText("Email Address:");
                    FirstNameLabel.InnerText = SepFunctions.LangText("First Name:");
                    LastNameLabel.InnerText = SepFunctions.LangText("Last Name:");
                    CountryLabel.InnerText = SepFunctions.LangText("Country:");
                    StreetAddressLabel.InnerText = SepFunctions.LangText("Street Address:");
                    CityLabel.InnerText = SepFunctions.LangText("City:");
                    StateLabel.InnerText = SepFunctions.LangText("State/Province:");
                    PostalCodeLabel.InnerText = SepFunctions.LangText("Zip/Postal Code:");
                    GenderLabel.InnerText = SepFunctions.LangText("Gender:");
                    BirthDateLabel.InnerText = SepFunctions.LangText("Birth Date:");
                    PhoneNumberLabel.InnerText = SepFunctions.LangText("Phone Number:");
                    FriendsLabel.InnerText = SepFunctions.LangText("Allow your authorization before others can add you to their friend list:");
                    PayPalLabel.InnerText = SepFunctions.LangText("PayPal Email Address (In case you intent buyers to pay you with credit card):");
                    ReferralLabel.InnerText = SepFunctions.LangText("Referral Options:");
                    NewslettersLabel.InnerText = SepFunctions.LangText("Select the Newsletters you wish to join:");
                    UserNameRequired.ErrorMessage = SepFunctions.LangText("~~User Name~~ is required.");
                    RePasswordRequired.ErrorMessage = SepFunctions.LangText("~~Password~~ is required.");
                    SecretQuestionRequired.ErrorMessage = SepFunctions.LangText("~~Secret Question~~ is required.");
                    SecretAnswerRequired.ErrorMessage = SepFunctions.LangText("~~Secret Answer~~ is required.");
                    EmailAddressRequired.ErrorMessage = SepFunctions.LangText("~~Email Address~~ is required.");
                    FirstNameRequired.ErrorMessage = SepFunctions.LangText("~~First Name~~ is required.");
                    LastNameRequired.ErrorMessage = SepFunctions.LangText("~~Last Name~~ is required.");
                    StreetAddressRequired.ErrorMessage = SepFunctions.LangText("~~Street Address~~ is required.");
                    CityRequired.ErrorMessage = SepFunctions.LangText("~~City~~ is required.");
                    PostalCodeRequired.ErrorMessage = SepFunctions.LangText("~~Zip/Postal Code~~ is required.");
                    PhoneNumberRequired.ErrorMessage = SepFunctions.LangText("~~Phone Number~~ is required.");
                    PayPalRequired.ErrorMessage = SepFunctions.LangText("~~PayPal Email Address~~ is required.");
                    ReferralRequired.ErrorMessage = SepFunctions.LangText("~~Referral~~ is required.");
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

            var foundInviteId = false;
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 29;

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (SepFunctions.ModuleActivated(68))
            {
                PageText.InnerHtml = SepFunctions.SendGenericError(404);
                SignupFormDiv.Visible = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID())) SepFunctions.Redirect(sInstallFolder + "account_edit.aspx");

            if (!IsPostBack)
            {
                var cReplace = new Replace();

                PageText.InnerHtml += cReplace.GetPageText(GlobalVars.ModuleID, GlobalVars.ModuleID);

                cReplace.Dispose();

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupSecTitle"))) SignupFormText.InnerHtml += SepFunctions.HTMLDecode(SepFunctions.Setup(997, "SignupSecTitle"));
                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupSecDesc")))
                {
                    if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupSecTitle"))) SignupFormText.InnerHtml += "<br/>";
                    SignupFormText.InnerHtml += SepFunctions.HTMLDecode(SepFunctions.Setup(997, "SignupSecDesc"));
                }

                if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupSecTitle")) || !string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupSecDesc"))) SignupFormText.InnerHtml += "<br/><br/>";

                if (!string.IsNullOrWhiteSpace(Strings.ToString(Context.Session["ReferralID"])))
                {
                    ReferralOptions.Value = "AffiliateID";
                    Referral.Value = Strings.ToString(Context.Session["ReferralID"]);
                }

                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("InviteID")))
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT * FROM MembersInvite WHERE InviteID=@InviteID", conn))
                        {
                            cmd.Parameters.AddWithValue("@InviteID", SepCommon.SepCore.Request.Item("InviteID"));
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    foundInviteId = true;
                                    EmailAddress.Value = SepFunctions.openNull(RS["EmailAddress"]);
                                    FirstName.Value = SepFunctions.openNull(RS["FirstName"]);
                                    LastName.Value = SepFunctions.openNull(RS["LastName"]);
                                }

                            }
                        }
                    }

                if (foundInviteId == false)
                {
                    Class_Selection();
                    Credit_Selection();
                }

                if (SepFunctions.Setup(997, "AutoUser") == "Yes")
                {
                    UserNameRow.Visible = false;
                }

                if (SepFunctions.Setup(997, "LoginEmail") == "Yes")
                {
                    UserNameRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskCountry") != "Yes")
                {
                    CountryRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskStreetAddress") != "Yes")
                {
                    StreetAddressRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(994, "ReqAddress") != "Yes")
                    {
                        StreetAddressRequired.Enabled = false;
                    }
                }

                if (SepFunctions.Setup(994, "AskCity") != "Yes")
                {
                    CityRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(994, "ReqAddress") != "Yes")
                    {
                        CityRequired.Enabled = false;
                    }
                }

                if (SepFunctions.Setup(994, "AskState") != "Yes")
                {
                    StateRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskZipCode") != "Yes")
                {
                    PostalCodeRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(994, "ReqAddress") != "Yes")
                    {
                        PostalCodeRequired.Enabled = false;
                    }
                }

                if (SepFunctions.Setup(994, "AskGender") != "Yes")
                {
                    GenderRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskBirthDate") != "Yes")
                {
                    BirthDateRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskPhoneNumber") != "Yes")
                {
                    PhoneNumberRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(994, "ReqPhoneNumber") != "Yes")
                    {
                        PhoneNumberRequired.Enabled = false;
                    }
                }

                if (SepFunctions.Setup(33, "FriendsEnable") != "Yes" || SepFunctions.Setup(994, "AskFriends") != "Yes")
                {
                    FriendsRow.Visible = false;
                }

                if (SepFunctions.Setup(994, "AskPayPal") != "Yes")
                {
                    PayPalRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(994, "ReqPayPal") != "Yes")
                    {
                        PayPalRequired.Enabled = false;
                    }
                }

                if (SepFunctions.Setup(7, "UPagesSignupProcess") != "Allow" && SepFunctions.Setup(7, "UPagesSignupProcess") != "Require" || SepFunctions.isUserPage())
                {
                    SiteIdRow.Visible = false;
                }
                else
                {
                    var cUserPages = SepCommon.DAL.UserPages.GetUserPages();
                    for (var i = 0; i <= cUserPages.Count - 1; i++) SiteId.Items.Add(new ListItem(cUserPages[i].SiteName, Strings.ToString(cUserPages[i].SiteID)));
                }

                if (string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupAgreement"))) AgreementRow.Visible = false;
                if (SepFunctions.isProfessionalEdition() == false)
                {
                    ReferralRow.Visible = false;
                }
                else
                {
                    if (SepFunctions.Setup(39, "AffiliateEnable") != "Enable" || SepFunctions.Setup(39, "AffiliateSignup") == "No")
                    {
                        ReferralRow.Visible = false;
                    }
                    else
                    {
                        if (SepFunctions.Setup(994, "AffiliateIDReq") != "Yes")
                        {
                            ReferralRequired.Enabled = false;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "FacebookAPIKey"))) FacebookRow.Visible = false;

                var LinkedInAPI = SepFunctions.Setup(989, "LinkedInAPI");
                var LinkedInSecret = SepFunctions.Setup(989, "LinkedInSecret");

                if (!string.IsNullOrWhiteSpace(LinkedInAPI) && !string.IsNullOrWhiteSpace(LinkedInSecret))
                {
                    LinkedInConnect.APIKey = LinkedInAPI;
                    LinkedInConnect.APISecret = LinkedInSecret;
                    LinkedInConnect.RedirectUrl = Request.Url.AbsoluteUri.Split('?')[0];
                }
                else
                {
                    LinkedInRow.Visible = false;
                }
            }

            var sLetters = SepFunctions.Show_Newsletters(string.Empty);
            if (string.IsNullOrWhiteSpace(sLetters))
            {
                NewslettersRow.Visible = false;
            }
            else
            {
                Newsletters.InnerHtml = sLetters;
                NewslettersRow.Visible = true;
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

        /// <summary>
        /// Handles the Click event of the SignupButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SignupButton_Click(object sender, EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            var RequiredCustomField = SepFunctions.Validate_Custom_Fields(GlobalVars.ModuleID);
            if (!string.IsNullOrWhiteSpace(RequiredCustomField))
            {
                ErrorMessage.InnerHtml = RequiredCustomField;
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(997, "SignupAgreement")))
                if (SignupAgreement.Checked == false)
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must agree to our signup agreement.") + "</div>";
                    return;
                }

            if (Password.Value != RePassword.Value)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Passwords do not match.") + "</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, ".*[@#$%^&*/!].*") == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must contain one of @#$%^&*/!.") + "</div>";
                return;
            }

            if (Regex.IsMatch(Password.Value, "[^\\s]{4,20}") == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Password must be between 4-20 characters.") + "</div>";
                return;
            }

            if (SepFunctions.blockedUser(UserName.Value))
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Invalid user name") + "</div>";
                return;
            }

            if (SepFunctions.Setup(7, "UPagesSignupProcess") == "Require")
                if (string.IsNullOrWhiteSpace(SiteId.Value))
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You must select a user page.") + "</div>";
                    return;
                }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserName=@UserName", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName.Value);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("User Name already exists in our database.") + "</div>";
                            return;
                        }

                    }
                }
            }

            if (Recaptcha1.Validate() == false)
            {
                ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("You have entered an invalid reCaptcha.") + "</div>";
                return;
            }

            if (String.IsNullOrWhiteSpace(BirthDate.Value))
            {
                BirthDate.Value = Strings.ToString(DateTime.Now);
            }

            Save_Member(UserName.Value, Password.Value, EmailAddress.Value, FirstName.Value, LastName.Value, StreetAddress.Value, City.Value, PostalCode.Value, SepCommon.SepCore.Request.Item("State"), SepCommon.SepCore.Request.Item("Country"), SepFunctions.toInt(Gender.Value), PhoneNumber.Value, SepFunctions.toDate(BirthDate.Value), PayPalEmail.Value, ReferralOptions.Value, Referral.Value, SecretQuestion.Value, SecretAnswer.Value, FacebookToken.Value, FacebookId.Value, FacebookUser.Value, SepCommon.SepCore.Request.Item("InviteID"), SepFunctions.toLong(SiteId.Value));

            ErrorMessage.InnerHtml = "<p class=\"successMsg\">" + SepFunctions.LangText("Thank you for joining our website.") + "</p>";
            if (string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("InviteID")))
            {
                if (SepFunctions.Setup(997, "SignupAdminApp") == "Yes")
                {
                    ErrorMessage.InnerHtml += "<p align=\"center\">" + SepFunctions.LangText("You will receive an email once your account has been approved by the administrator.") + "</p>";
                }
                else
                {
                    if (SepFunctions.Setup(GlobalVars.ModuleID, "SignupVerify") == "Yes") ErrorMessage.InnerHtml += "<p align=\"center\">" + SepFunctions.LangText("We have sent a message to the email address you provided. After checking your email, you will need to confirm your email address by clicking the link at the bottom of the email message before you can login.") + "<br/><br/><b>" + SepFunctions.LangText("Note:") + "</b> " + SepFunctions.LangText("If you do not receive our email then please check your spam folder.") + "</p>";
                }
            }

            if (SepFunctions.Setup(997, "SignupAdminApp") != "Yes")
            {
                if (SepFunctions.Setup(63, "ProfilesEnable") == "Enable" && SepFunctions.Setup(GlobalVars.ModuleID, "ProfilesAskSignup") == "Yes" && SepFunctions.CompareKeys(SepFunctions.Security("ProfilesModify"), true)) ErrorMessage.InnerHtml += "<p align=\"center\"><a href=\"" + sInstallFolder + "profiles_modify.aspx\">" + SepFunctions.LangText("Don't forget to fill out your profile.") + "</a></p>";
            }

            SignupFormDiv.Visible = false;
        }
    }
}