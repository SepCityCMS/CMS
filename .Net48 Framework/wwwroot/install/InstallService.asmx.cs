// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="InstallService.asmx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using Newtonsoft.Json;
    using SepCommon;
    using SepCommon.SepActivation;
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.Script.Services;
    using System.Web.Services;

    /// <summary>
    /// Summary description for InstallService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class InstallService : WebService
    {
        /// <summary>
        /// Gets the percentage.
        /// </summary>
        /// <returns>System.String.</returns>
        [WebMethod]
        public string GetPercentage()
        {
            dynamic categories = GetPercent();

            return JsonConvert.SerializeObject(categories);
        }

        /// <summary>
        /// Runs the install.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="DBAddress">The database address.</param>
        /// <param name="DBName">Name of the database.</param>
        /// <param name="DBUser">The database user.</param>
        /// <param name="DBPass">The database pass.</param>
        /// <param name="InstallCategories">The install categories.</param>
        /// <param name="InstallSampleData">The install sample data.</param>
        /// <param name="SMTPServer">The SMTP server.</param>
        /// <param name="SMTPUser">The SMTP user.</param>
        /// <param name="SMTPPass">The SMTP pass.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="Male">The male.</param>
        /// <param name="BirthDate">The birth date.</param>
        /// <param name="StreetAddress">The street address.</param>
        /// <param name="City">The city.</param>
        /// <param name="State">The state.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <param name="PhoneNumber">The phone number.</param>
        /// <param name="Country">The country.</param>
        /// <param name="Secret_Answer">The secret answer.</param>
        /// <param name="Secret_Question">The secret question.</param>
        /// <param name="LicenseUser">The license user.</param>
        /// <param name="LicensePass">The license pass.</param>
        /// <param name="LicenseKey">The license key.</param>
        /// <returns>System.String.</returns>
        [WebMethod]
        public string RunInstall(string UserID, string DBAddress, string DBName, string DBUser, string DBPass, string InstallCategories, string InstallSampleData, string SMTPServer, string SMTPUser, string SMTPPass, string UserName, string Password, string EmailAddress, string FirstName, string LastName, string Male, string BirthDate, string StreetAddress, string City, string State, string ZipCode, string PhoneNumber, string Country, string Secret_Answer, string Secret_Question, string LicenseUser, string LicensePass, string LicenseKey)
        {
            if (string.IsNullOrWhiteSpace(UserID))
            {
                SepFunctions.Debug_Log("Missing UserID");
                return "Missing UserID";
            }

            if (string.IsNullOrWhiteSpace(DBAddress))
            {
                SepFunctions.Debug_Log("Missing DBAddress");
                return "Missing DBAddress";
            }

            if (string.IsNullOrWhiteSpace(DBName))
            {
                SepFunctions.Debug_Log("Missing DBName");
                return "Missing DBName";
            }

            if (string.IsNullOrWhiteSpace(DBUser))
            {
                SepFunctions.Debug_Log("Missing DBUser");
                return "Missing DBUser";
            }

            if (string.IsNullOrWhiteSpace(DBPass))
            {
                SepFunctions.Debug_Log("Missing DBPass");
                return "Missing DBPass";
            }

            if (string.IsNullOrWhiteSpace(SMTPServer))
            {
                SepFunctions.Debug_Log("Missing SMTPServer");
                return "Missing SMTPServer";
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                SepFunctions.Debug_Log("Missing UserName");
                return "Missing UserName";
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                SepFunctions.Debug_Log("Missing Password");
                return "Missing Password";
            }

            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                SepFunctions.Debug_Log("Missing EmailAddress");
                return "Missing EmailAddress";
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                SepFunctions.Debug_Log("Missing FirstName");
                return "Missing FirstName";
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                SepFunctions.Debug_Log("Missing LastName");
                return "Missing LastName";
            }

            if (string.IsNullOrWhiteSpace(Male))
            {
                SepFunctions.Debug_Log("Missing Male");
                return "Missing Male";
            }

            if (string.IsNullOrWhiteSpace(BirthDate))
            {
                SepFunctions.Debug_Log("Missing BirthDate");
                return "Missing BirthDate";
            }

            if (string.IsNullOrWhiteSpace(StreetAddress))
            {
                SepFunctions.Debug_Log("Missing StreetAddress");
                return "Missing StreetAddress";
            }

            if (string.IsNullOrWhiteSpace(City))
            {
                SepFunctions.Debug_Log("Missing City");
                return "Missing City";
            }

            if (string.IsNullOrWhiteSpace(ZipCode))
            {
                SepFunctions.Debug_Log("Missing ZipCode");
                return "Missing ZipCode";
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                SepFunctions.Debug_Log("Missing PhoneNumber");
                return "Missing PhoneNumber";
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                SepFunctions.Debug_Log("Missing Country");
                return "Missing Country";
            }

            if (string.IsNullOrWhiteSpace(Secret_Answer))
            {
                SepFunctions.Debug_Log("Missing Secret_Answer");
                return "Missing Secret_Answer";
            }

            if (string.IsNullOrWhiteSpace(Secret_Question))
            {
                SepFunctions.Debug_Log("Missing Secret_Question");
                return "Missing Secret_Question";
            }

            if (string.IsNullOrWhiteSpace(LicenseUser))
            {
                SepFunctions.Debug_Log("Missing LicenseUser");
                return "Missing LicenseUser";
            }

            if (string.IsNullOrWhiteSpace(LicensePass))
            {
                SepFunctions.Debug_Log("Missing LicensePass");
                return "Missing LicensePass";
            }

            if (string.IsNullOrWhiteSpace(LicenseKey))
            {
                SepFunctions.Debug_Log("Missing LicenseKey");
                return "Missing LicenseKey";
            }

            var sInstallFolder = SepFunctions.GetInstallFolder();

            var connStr = "DATABASE=" + DBName + ";SERVER=" + DBAddress + ";user id=" + DBUser + ";PASSWORD=" + DBPass + ";MultipleActiveResultSets=true;";
            jActivation sActivation = checkValidInstall(LicenseUser, LicensePass, LicenseKey, connStr);

            if (sActivation == null)
            {
                SepFunctions.Debug_Log("Software is already installed.");
                return "Software is already installed.";
            }

            if (sActivation.Status != "Active")
            {
                SepFunctions.Debug_Log("Invalid License Information.");
                return "Invalid License Information";
            }

            SepFunctions.Debug_Log("Got successful license.");

            long iMaxNum = 0;

            dynamic SqlStr = string.Empty;

            long resetCount = 0;
            long currentCount = 0;
            string[] arrTables = null;
            string[] arrData = null;
            string[] arrEventTypes = null;
            string[] arrCategories = null;
            string[] arrSample = null;


            StreamReader objTables;
            using (objTables = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\create_tables.sql"))
            {
                arrTables = Strings.Split(objTables.ReadToEnd(), Environment.NewLine);
                iMaxNum += Information.UBound(arrTables);
            }

            StreamReader objData;
            using (objData = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\default_data.sql"))
            {
                arrData = Strings.Split(objData.ReadToEnd(), Environment.NewLine);
                iMaxNum += Information.UBound(arrData);
            }

            StreamReader objEventTypes;
            using (objEventTypes = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\event_types.sql"))
            {
                arrEventTypes = Strings.Split(objEventTypes.ReadToEnd(), Environment.NewLine);
                iMaxNum += Information.UBound(arrEventTypes);
            }

            StreamReader objCategories;
            using (objCategories = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\categories.sql"))
            {
                arrCategories = Strings.Split(objCategories.ReadToEnd(), Environment.NewLine);
            }

            if (InstallCategories == "Yes" || InstallSampleData == "Yes") iMaxNum += Information.UBound(arrCategories);

            using (var objSample = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\sql\\sample_data.sql"))
            {
                arrSample = Strings.Split(objSample.ReadToEnd(), Environment.NewLine);
            }

            if (InstallSampleData == "Yes") iMaxNum += Information.UBound(arrSample);

            StreamWriter outfile;
            double percentNum;
            // Create Tables
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    if (arrTables != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrTables); i++)
                        {
                            if (Strings.Trim(arrTables[i]) == "GO")
                            {
                                try
                                {
                                    using (SqlCommand cmd = new SqlCommand(SqlStr + ";", conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                catch
                                {
                                    SepFunctions.Debug_Log("Error running SQL Statement. (" + SqlStr + ")");
                                }

                                SqlStr = string.Empty;
                            }
                            else
                            {
                                SqlStr += arrTables[i] + " ";
                            }

                            if (resetCount == 50)
                            {
                                percentNum = Percent(currentCount, iMaxNum);
                                try
                                {
                                    using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                                    {
                                        outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>" + percentNum + "</percentage>" + Environment.NewLine + "</root>");
                                    }
                                }
                                catch
                                {
                                }

                                resetCount = 0;
                            }

                            resetCount += 1;
                            currentCount += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error creating the default tables. (" + ex.Message + ")");
                SepFunctions.Debug_Log("Connection String (" + connStr + ")");
            }

            // Add default data
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    if (arrData != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrData); i++)
                        {
                            if (Strings.Trim(arrData[i]) == "GO")
                            {
                                using (SqlCommand cmd = new SqlCommand(SqlStr + ";", conn))
                                {
                                    cmd.CommandTimeout = 999999;
                                    cmd.ExecuteNonQuery();
                                }

                                SqlStr = string.Empty;
                            }
                            else
                            {
                                SqlStr += arrData[i] + " ";
                            }

                            if (resetCount == 50)
                            {
                                percentNum = Percent(currentCount, iMaxNum);
                                try
                                {
                                    using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                                    {
                                        outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>" + percentNum + "</percentage>" + Environment.NewLine + "</root>");
                                    }
                                }
                                catch
                                {
                                }

                                resetCount = 0;
                            }

                            resetCount += 1;
                            currentCount += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error writing the default data to the database. (" + ex.Message + ")");
            }

            // Add event types
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    if (arrEventTypes != null)
                    {
                        for (var i = 0; i <= Information.UBound(arrEventTypes); i++)
                        {
                            if (Strings.Trim(arrEventTypes[i]) == "GO")
                            {
                                using (SqlCommand cmd = new SqlCommand(SqlStr + ";", conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                SqlStr = string.Empty;
                            }
                            else
                            {
                                SqlStr += arrEventTypes[i] + " ";
                            }

                            if (resetCount == 50)
                            {
                                percentNum = Percent(currentCount, iMaxNum);
                                try
                                {
                                    using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                                    {
                                        outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>" + percentNum + "</percentage>" + Environment.NewLine + "</root>");
                                    }
                                }
                                catch
                                {
                                }

                                resetCount = 0;
                            }

                            resetCount += 1;
                            currentCount += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error writing the event types to the database. (" + ex.Message + ")");
            }

            if (Strings.LCase(InstallCategories) == "yes" || Strings.LCase(InstallSampleData) == "yes")
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        if (arrCategories != null)
                        {
                            for (var i = 0; i <= Information.UBound(arrCategories); i++)
                            {
                                if (Strings.Trim(arrCategories[i]) == "GO")
                                {
                                    using (SqlCommand cmd = new SqlCommand(SqlStr + ";", conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                    SqlStr = string.Empty;
                                }
                                else
                                {
                                    SqlStr += arrCategories[i] + " ";
                                }

                                if (resetCount == 50)
                                {
                                    percentNum = Percent(currentCount, iMaxNum);
                                    try
                                    {
                                        using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                                        {
                                            outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>" + percentNum + "</percentage>" + Environment.NewLine + "</root>");
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    resetCount = 0;
                                }

                                resetCount += 1;
                                currentCount += 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing the categories to the database. (" + ex.Message + ")");
                }

            if (Strings.LCase(InstallSampleData) == "yes")
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        if (arrSample != null)
                        {
                            for (var i = 0; i <= Information.UBound(arrSample); i++)
                            {
                                if (Strings.Trim(arrSample[i]) == "GO")
                                {
                                    using (SqlCommand cmd = new SqlCommand(SqlStr + ";", conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                    SqlStr = string.Empty;
                                }
                                else
                                {
                                    SqlStr += arrSample[i] + " ";
                                }

                                if (resetCount == 50)
                                {
                                    percentNum = Percent(currentCount, iMaxNum);
                                    try
                                    {
                                        using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                                        {
                                            outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>" + percentNum + "</percentage>" + Environment.NewLine + "</root>");
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    resetCount = 0;
                                }

                                resetCount += 1;
                                currentCount += 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing the sample data to the database. (" + ex.Message + ")");
                }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837451','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner1.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 1', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837452','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner2.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 2', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837453','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner3.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 3', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837454','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner4.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 4', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837455','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner5.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 5', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("insert into Advertisements (AdID, MaxClicks, MaxExposures, TotalClicks, TotalExposures, ImageURL, SiteURL, UserID, UseHTML, StartDate, ZoneID, CatIDs, PortalIDs, EndDate, Status, DatePosted, PageIDs, Description, HTMLCode) VALUES('192837456','100000', '100000', '1', '1', '" + sInstallFolder + "images/banner6.gif', '" + sInstallFolder + "advertising.aspx', '" + UserID + "', '0', '" + DateTime.Now + "', '192837456', '|0|', '|0|','" + DateAndTime.DateAdd(DateAndTime.DateInterval.Year, 4, DateTime.Now) + "','1','" + DateTime.Now + "', '|-1|', 'Advertise Here 6', '')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing advertisements to the database. (" + ex.Message + ")");
                }

                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Members (UserID, UserName, Password, EmailAddress, FirstName, LastName, Male, BirthDate, AccessClass, AccessKeys, Status, LastLogin, CreateDate, PortalID, UserNotes, StreetAddress, City, State, ZipCode, PhoneNumber, Country, ApproveFriends, IPAddress, PayPal, ClassChanged, UserPoints, AffiliateID, Secret_Answer, Secret_Question, HideTips) VALUES('" + UserID + "',@UserName, @Password, @EmailAddress, @FirstName, @LastName, @Male, @BirthDate, '2', '|1|,|2|,|3|,|4|', '1', '" + DateTime.Now + "', '" + DateTime.Now + "', '0', '', @StreetAddress, @City, @State, @ZipCode, @PhoneNumber, @Country, 'Yes', '', '', '" + DateTime.Now + "', '0', '" + SepFunctions.GetIdentity() + "', @Secret_Answer, @Secret_Question, '0')", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Password", SepFunctions.Save_Password(Password));
                        cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@Male", Male);
                        cmd.Parameters.AddWithValue("@BirthDate", DateTime.Today);
                        cmd.Parameters.AddWithValue("@StreetAddress", StreetAddress);
                        cmd.Parameters.AddWithValue("@City", City);
                        cmd.Parameters.AddWithValue("@State", State);
                        cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        cmd.Parameters.AddWithValue("@Country", Country);
                        cmd.Parameters.AddWithValue("@Secret_Answer", Secret_Answer);
                        cmd.Parameters.AddWithValue("@Secret_Question", Secret_Question);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing Administrator to the database. (" + ex.Message + ")");
                }
            }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>100</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            // Copy default XML files
            File.Copy(HostingEnvironment.MapPath("~/install/") + "\\xml\\points.xml", SepFunctions.GetDirValue("app_data") + "points.xml");
            File.Copy(HostingEnvironment.MapPath("~/install/") + "\\xml\\security.xml", SepFunctions.GetDirValue("app_data") + "security.xml");
            File.Copy(HostingEnvironment.MapPath("~/install/") + "\\xml\\image_sizes.xml", SepFunctions.GetDirValue("app_data") + "image_sizes.xml");
            File.Copy(HostingEnvironment.MapPath("~/install/") + "\\xml\\pcrfields.xml", SepFunctions.GetDirValue("app_data") + "pcrfields.xml");
            try
            {
                StreamReader readSettings;
                using (readSettings = new StreamReader(HostingEnvironment.MapPath("~/install/") + "\\xml\\settings.xml"))
                {
                    StreamWriter writeSettings;
                    using (writeSettings = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings.xml"))
                    {
                        // Write admin information to general setup
                        dynamic sSettings = readSettings.ReadToEnd();
                        sSettings = Strings.Replace(sSettings, "<AdminFullName>Your Name</AdminFullName>", "<AdminFullName>" + SepFunctions.HTMLEncode(FirstName + " " + LastName) + "</AdminFullName>");
                        sSettings = Strings.Replace(sSettings, "<AdminEmailAddress>username@domain.com</AdminEmailAddress>", "<AdminEmailAddress>" + SepFunctions.HTMLEncode(EmailAddress) + "</AdminEmailAddress>");
                        sSettings = Strings.Replace(sSettings, "<CompanyPhone></CompanyPhone>", "<CompanyPhone>" + SepFunctions.HTMLEncode(PhoneNumber) + "</CompanyPhone>");
                        sSettings = Strings.Replace(sSettings, "<CompanyAddressLine1></CompanyAddressLine1>", "<CompanyAddressLine1>" + SepFunctions.HTMLEncode(StreetAddress) + "</CompanyAddressLine1>");
                        sSettings = Strings.Replace(sSettings, "<CompanyCity></CompanyCity>", "<CompanyCity>" + SepFunctions.HTMLEncode(City) + "</CompanyCity>");
                        sSettings = Strings.Replace(sSettings, "<CompanyState></CompanyState>", "<CompanyState>" + SepFunctions.HTMLEncode(State) + "</CompanyState>");
                        sSettings = Strings.Replace(sSettings, "<CompanyZipCode></CompanyZipCode>", "<CompanyZipCode>" + SepFunctions.HTMLEncode(ZipCode) + "</CompanyZipCode>");
                        sSettings = Strings.Replace(sSettings, "<CompanyCountry>us</CompanyCountry>", "<CompanyCountry>" + SepFunctions.HTMLEncode(Country) + "</CompanyCountry>");
                        writeSettings.Write(sSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error writing settings.xml file. (" + ex.Message + ")");
            }

            // Create system.xml file
            dynamic sXML;
            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "system.xml"))
                {
                    sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                    sXML += "<ROOTLEVEL>" + Environment.NewLine;
                    sXML += "<EncryptionKey></EncryptionKey>" + Environment.NewLine;
                    sXML += "<ConnectionString>" + SepFunctions.HTMLEncode(connStr) + "</ConnectionString>" + Environment.NewLine;
                    sXML += "<MailServerIP>" + SepFunctions.HTMLEncode(SMTPServer) + "</MailServerIP>" + Environment.NewLine;
                    sXML += "<MailServerUser>" + SepFunctions.HTMLEncode(SMTPUser) + "</MailServerUser>" + Environment.NewLine;
                    sXML += "<MailServerPass>" + SepFunctions.HTMLEncode(SMTPPass) + "</MailServerPass>" + Environment.NewLine;
                    sXML += "<MailServerPort>25</MailServerPort>" + Environment.NewLine;
                    sXML += "</ROOTLEVEL>" + Environment.NewLine;
                    outfile.Write(sXML);
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error writing system.xml file. (" + ex.Message + ")");
            }

            // Create License File
            try
            {
                if (sActivation.Status == "Active")
                {
                    // -V3022
                    sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                    sXML += "<ROOTLEVEL>" + Environment.NewLine;
                    sXML += "<Username>" + SepFunctions.HTMLEncode(LicenseUser) + "</Username>" + Environment.NewLine;
                    sXML += "<Password>" + SepFunctions.HTMLEncode(LicensePass) + "</Password>" + Environment.NewLine;
                    sXML += "<LicenseKey>" + SepFunctions.HTMLEncode(LicenseKey) + "</LicenseKey>" + Environment.NewLine;
                    sXML += "<LastChecked>" + Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.ShortDate) + "</LastChecked>" + Environment.NewLine;
                    sXML += "<ModuleList>" + sActivation.ModuleList + "</ModuleList>" + Environment.NewLine;
                    sXML += "<Version>" + sActivation.SoftwareEdition + "</Version>" + Environment.NewLine;
                    sXML += "</ROOTLEVEL>";

                    using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "license.xml"))
                    {
                        outfile.Write(sXML);
                    }
                }
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error writing license.xml file. (" + ex.Message + ")");
            }

            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "version.txt"))
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "version.txt"))
                {
                    outfile.Write(SepFunctions.GetVersion());
                }

            try
            {
                if (Directory.Exists(HostingEnvironment.MapPath("~/install/"))) Directory.Delete(HostingEnvironment.MapPath("~/install/"), true);
            }
            catch
            {
            }

            try
            {
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "install_temp.xml")) File.Delete(SepFunctions.GetDirValue("app_data") + "install_temp.xml");
            }
            catch
            {
            }

            return "Done";

        }

        /// <summary>
        /// Runs the upgrade.
        /// </summary>
        /// <param name="DBAddress">The database address.</param>
        /// <param name="DBName">Name of the database.</param>
        /// <param name="DBUser">The database user.</param>
        /// <param name="DBPass">The database pass.</param>
        /// <param name="SMTPServer">The SMTP server.</param>
        /// <param name="SMTPUser">The SMTP user.</param>
        /// <param name="SMTPPass">The SMTP pass.</param>
        /// <param name="LicenseUser">The license user.</param>
        /// <param name="LicensePass">The license pass.</param>
        /// <param name="LicenseKey">The license key.</param>
        /// <returns>System.String.</returns>
        [WebMethod]
        public string RunUpgrade(string DBAddress, string DBName, string DBUser, string DBPass, string SMTPServer, string SMTPUser, string SMTPPass, string LicenseUser, string LicensePass, string LicenseKey)
        {
            var connStr = "DATABASE=" + DBName + ";SERVER=" + DBAddress + ";user id=" + DBUser + ";PASSWORD=" + DBPass + ";MultipleActiveResultSets=true;";

            StreamWriter outfile;
            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>1</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            var sEncryptionKey = SepFunctions.Generate_GUID();


            // Create system.xml file
            dynamic sXML;
            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "system.xml"))
                try
                {
                    using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "system.xml"))
                    {
                        sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        sXML += "<ROOTLEVEL>" + Environment.NewLine;
                        sXML += "<EncryptionKey>" + sEncryptionKey + "</EncryptionKey>" + Environment.NewLine;
                        sXML += "<ConnectionString>" + SepFunctions.HTMLEncode(SepFunctions.AES_Encrypt(connStr, sEncryptionKey)) + "</ConnectionString>" + Environment.NewLine;
                        sXML += "<MailServerIP>" + SepFunctions.HTMLEncode(SMTPServer) + "</MailServerIP>" + Environment.NewLine;
                        sXML += "<MailServerUser>" + SepFunctions.HTMLEncode(SepFunctions.AES_Encrypt(SMTPUser, sEncryptionKey)) + "</MailServerUser>" + Environment.NewLine;
                        sXML += "<MailServerPass>" + SepFunctions.HTMLEncode(SepFunctions.AES_Encrypt(SMTPPass, sEncryptionKey)) + "</MailServerPass>" + Environment.NewLine;
                        sXML += "<MailServerPort>25</MailServerPort>" + Environment.NewLine;
                        sXML += "</ROOTLEVEL>" + Environment.NewLine;
                        outfile.Write(sXML);
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing system.xml file. (" + ex.Message + ")");
                }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>25</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            // Create License File
            if (!File.Exists(SepFunctions.GetDirValue("app_data") + "license.xml"))
                try
                {
                    var sActivation = default(jActivation);
                    using (var soapClient = new activationSoapClient("activationSoap"))
                    {
                        SepFunctions.Debug_Log("LicenseUser: " + LicenseUser);
                        SepFunctions.Debug_Log("LicensePass: " + LicensePass);
                        SepFunctions.Debug_Log("LicenseKey: " + LicenseKey);
                        sActivation = soapClient.Get_License_Details(LicenseUser, LicensePass, LicenseKey);
                    }

                    if (sActivation.Status == "Active")
                    {
                        sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                        sXML += "<ROOTLEVEL>" + Environment.NewLine;
                        sXML += "<Username>" + SepFunctions.HTMLEncode(LicenseUser) + "</Username>" + Environment.NewLine;
                        sXML += "<Password>" + SepFunctions.HTMLEncode(LicensePass) + "</Password>" + Environment.NewLine;
                        sXML += "<LicenseKey>" + SepFunctions.HTMLEncode(LicenseKey) + "</LicenseKey>" + Environment.NewLine;
                        sXML += "<LastChecked>" + Strings.FormatDateTime(DateTime.Now, Strings.DateNamedFormat.ShortDate) + "</LastChecked>" + Environment.NewLine;
                        sXML += "<ModuleList>" + sActivation.ModuleList + "</ModuleList>" + Environment.NewLine;
                        sXML += "<Version>" + sActivation.SoftwareEdition + "</Version>" + Environment.NewLine;
                        sXML += "</ROOTLEVEL>";

                        using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "license.xml"))
                        {
                            outfile.Write(sXML);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SepFunctions.Debug_Log("Error writing license.xml file. (" + ex.Message + ")");
                }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>40</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT PortalID,(SELECT TOP 1 ScriptText FROM PortalScripts WHERE ScriptType='SETTINGS' AND PortalID=Portals.PortalID) AS ScriptText FROM Portals", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                            {
                                var sSettings = SepFunctions.openNull(RS["ScriptText"]);
                                sXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
                                sXML += "<ROOTLEVEL>" + Environment.NewLine;
                                sXML += "<FULLNAME>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("FULLNAME", sSettings)) + "</FULLNAME>" + Environment.NewLine;
                                sXML += "<EMAILADDRESS>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("EMAILADDRESS", sSettings)) + "</EMAILADDRESS>" + Environment.NewLine;
                                sXML += "<COMPANYNAME>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("COMPANYNAME", sSettings)) + "</COMPANYNAME>" + Environment.NewLine;
                                sXML += "<STREET>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("STREET", sSettings)) + "</STREET>" + Environment.NewLine;
                                sXML += "<CITY>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("CITY", sSettings)) + "</CITY>" + Environment.NewLine;
                                sXML += "<STATE>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("STATE", sSettings)) + "</STATE>" + Environment.NewLine;
                                sXML += "<POSTALCODE>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("POSTALCODE", sSettings)) + "</POSTALCODE>" + Environment.NewLine;
                                sXML += "<SITECOUNTER>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITECOUNTER", sSettings)) + "</SITECOUNTER>" + Environment.NewLine;
                                sXML += "<SITEMENU1>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU1", sSettings)) + "</SITEMENU1>" + Environment.NewLine;
                                sXML += "<SITEMENU2>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU2", sSettings)) + "</SITEMENU2>" + Environment.NewLine;
                                sXML += "<SITEMENU3>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU3", sSettings)) + "</SITEMENU3>" + Environment.NewLine;
                                sXML += "<SITEMENU4>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU4", sSettings)) + "</SITEMENU4>" + Environment.NewLine;
                                sXML += "<SITEMENU5>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU5", sSettings)) + "</SITEMENU5>" + Environment.NewLine;
                                sXML += "<SITEMENU6>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU6", sSettings)) + "</SITEMENU6>" + Environment.NewLine;
                                sXML += "<SITEMENU7>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITEMENU7", sSettings)) + "</SITEMENU7>" + Environment.NewLine;
                                sXML += "<SITELANG>" + SepFunctions.HTMLEncode(SepFunctions.ParseXML("SITELANG", sSettings)) + "</SITELANG>" + Environment.NewLine;
                                sXML += "<WEBSITELAYOUT>938475665938400</WEBSITELAYOUT>" + Environment.NewLine;
                                sXML += "<WEBSITELAYOUTMOBILE>938475665938400</WEBSITELAYOUTMOBILE>" + Environment.NewLine;
                                sXML += "</ROOTLEVEL>" + Environment.NewLine;
                                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings-" + SepFunctions.openNull(RS["PortalID"]) + ".xml"))
                                {
                                    outfile.Write(sXML);
                                }
                            }

                    }
                }
            }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>50</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "settings.xml"))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='SETUP' AND ModuleIDs LIKE '%|0|%' AND PortalIDs LIKE '%|0|%' AND UserID='0' AND CatIDs LIKE '%|0|%'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + SepFunctions.openNull(RS["ScriptText"]));
                            }

                        }
                    }
                }
            }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>75</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "security.xml"))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ScriptText FROM Scripts WHERE ScriptType='SECURITY' AND ModuleIDs LIKE '%|0|%' AND PortalIDs LIKE '%|0|%' AND UserID='0' AND CatIDs LIKE '%|0|%'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + SepFunctions.openNull(RS["ScriptText"]));
                            }

                        }
                    }
                }
            }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>90</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT UploadID,ModuleID,FileName,UserID FROM Uploads WHERE Approved='1' AND isTemp='0'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                            while (RS.Read())
                                if (File.Exists(SepFunctions.GetDirValue("images") + "large_" + SepFunctions.openNull(RS["FileName"])))
                                {
                                    dynamic sUniqueID = string.Empty;
                                    var data = File.ReadAllBytes(SepFunctions.GetDirValue("images") + "large_" + SepFunctions.openNull(RS["FileName"]));
                                    if (SepFunctions.toLong(SepFunctions.openNull(RS["ModuleID"])) == 63)
                                    {
                                        if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["UserID"])))
                                        {
                                            using (SqlCommand cmd2 = new SqlCommand("SELECT ProfileID FROM Profiles WHERE UserID=@UserID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@UserID", SepFunctions.openNull(RS["UserID"]));
                                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                {
                                                    if (RS2.HasRows)
                                                    {
                                                        RS2.Read();
                                                        sUniqueID = SepFunctions.openNull(RS2["ProfileID"]);
                                                    }
                                                }
                                            }

                                            if (!string.IsNullOrWhiteSpace(sUniqueID))
                                                using (SqlCommand cmd2 = new SqlCommand("UPDATE Uploads SET UniqueID=@UniqueID, FileData=@FileData, FileSize=@FileSize WHERE UploadID=@UploadID", conn))
                                                {
                                                    cmd2.Parameters.AddWithValue("@UploadID", SepFunctions.openNull(RS["UploadID"]));
                                                    cmd2.Parameters.AddWithValue("@FileData", Convert.ToByte(data));
                                                    cmd2.Parameters.AddWithValue("@FileSize", data.Length);
                                                    cmd2.Parameters.AddWithValue("@UniqueID", sUniqueID);
                                                    cmd2.ExecuteNonQuery();
                                                }
                                        }
                                    }
                                    else
                                    {
                                        using (SqlCommand cmd2 = new SqlCommand("UPDATE Uploads SET FileData=@FileData, FileSize=@FileSize WHERE UploadID=@UploadID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@UploadID", SepFunctions.openNull(RS["UploadID"]));
                                            cmd2.Parameters.AddWithValue("@FileData", Convert.ToByte(data));
                                            cmd2.Parameters.AddWithValue("@FileSize", data.Length);
                                            cmd2.ExecuteNonQuery();
                                        }
                                    }
                                }

                    }
                }
            }

            try
            {
                using (outfile = new StreamWriter(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    outfile.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine + "<root>" + Environment.NewLine + "<percentage>100</percentage>" + Environment.NewLine + "</root>");
                }
            }
            catch
            {
            }

            return "Done";
        }

        /// <summary>
        /// Gets the percent.
        /// </summary>
        /// <returns>List&lt;jPercentage&gt;.</returns>
        private static List<jPercentage> GetPercent()
        {
            dynamic lPercentage = new List<jPercentage>();

            try
            {
                if (File.Exists(SepFunctions.GetDirValue("app_data") + "install.xml"))
                {
                    using (var objReader = new StreamReader(SepFunctions.GetDirValue("app_data") + "install.xml"))
                    {
                        var iPercent = SepFunctions.toLong(SepFunctions.ParseXML("percentage", objReader.ReadToEnd()));

                        dynamic dPercentage = new jPercentage { Percentage = iPercent };
                        lPercentage.Add(dPercentage);
                    }
                }
                else
                {
                    dynamic dPercentage = new jPercentage { Percentage = 0 };
                    lPercentage.Add(dPercentage);
                }
            }
            catch
            {
                dynamic dPercentage = new jPercentage { Percentage = 0 };
                lPercentage.Add(dPercentage);
            }

            return lPercentage;
        }

        /// <summary>
        /// Checks the valid install.
        /// </summary>
        /// <param name="LicenseUser">The license user.</param>
        /// <param name="LicensePass">The license pass.</param>
        /// <param name="LicenseKey">The license key.</param>
        /// <param name="connStr">The connection string.</param>
        /// <returns>jActivation.</returns>
        private jActivation checkValidInstall(string LicenseUser, string LicensePass, string LicenseKey, string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT UserID FROM Members", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                            }

                        }
                    }
                }

                return null;
            }
            catch
            {
            }

            try
            {
                var sReturn = default(jActivation);
                using (var soapClient = new activationSoapClient("activationSoap"))
                {
                    SepFunctions.Debug_Log("LicenseUser: " + LicenseUser);
                    SepFunctions.Debug_Log("LicensePass: " + LicensePass);
                    SepFunctions.Debug_Log("LicenseKey: " + LicenseKey);
                    sReturn = soapClient.Get_License_Details(LicenseUser, LicensePass, LicenseKey);
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                SepFunctions.Debug_Log("Error running CheckValidInstall() (" + ex.Message + ")");

                return null;
            }
        }

        /// <summary>
        /// Percents the specified curr value.
        /// </summary>
        /// <param name="CurrVal">The curr value.</param>
        /// <param name="MaxVal">The maximum value.</param>
        /// <returns>System.Double.</returns>
        private double Percent(double CurrVal, double MaxVal)
        {
            return CurrVal / MaxVal * 100 + 0.5;
        }
    }

    /// <summary>
    /// Class jPercentage.
    /// </summary>
    public class jPercentage
    {
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public long Percentage { get; set; }
    }
}