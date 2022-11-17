// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="bgProcess.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server
{
    using FileHelpers;
    using SepCityCMS.Models;
    using Server.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.Net;
    using System.Threading;

    /// <summary>
    /// Class clsbgProcess.
    /// </summary>
    public class BackgroundProcesses
    {
        /// <summary>
        /// Sends the newsletters.
        /// </summary>
        /// <param name="IntervalSeconds">The interval seconds.</param>
        /// <param name="ProcessID">The process identifier.</param>
        public static void Send_Newsletters(int IntervalSeconds, string ProcessID)
        {
            try
            {
                using (var EmailConn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    EmailConn.Open();

                    using (var Emailcmd = new SqlCommand("SELECT * FROM BG_Emails WHERE ProcessID=@ProcessID", EmailConn))
                    {
                        Emailcmd.Parameters.AddWithValue("@ProcessID", ProcessID);
                        using (SqlDataReader RS = Emailcmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                SepFunctions.Send_Email(SepFunctions.openNull(RS["To_Email_Address"]), SepFunctions.openNull(RS["From_Email_Address"]), SepFunctions.openNull(RS["Email_Subject"]), SepFunctions.openNull(RS["Email_Body"]), 24, SepFunctions.openNull(RS["Email_Attachment"]));
                                Thread.Sleep(IntervalSeconds * 1000);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>
        /// Imports the image from URL.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="imageURL">The image URL.</param>
        public void Import_Image_From_URL(long productId, string imageURL)
        {
            if (!string.IsNullOrWhiteSpace(imageURL))
                try
                {
                    var webClient = new WebClient();
                    var imageBytes = webClient.DownloadData(imageURL);

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("DELETE FROM Uploads WHERE UniqueID=@UniqueID AND ModuleID=@ModuleID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UniqueID", productId);
                            cmd.Parameters.AddWithValue("@ModuleID", 41);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("INSERT INTO Uploads (UploadID, UniqueID, UserID, ModuleID, FileName, FileSize, ContentType, isTemp, Approved, DatePosted, PortalID, FileData, ControlID, UserRates, TotalRates, Weight) VALUES(@UploadID, @UniqueID, @UserID, @ModuleID, @FileName, @FileSize, @ContentType, @isTemp, @Approved, @DatePosted, @PortalID, @FileData, @ControlID, '0', '0', '99')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UploadID", SepFunctions.GetIdentity());
                            cmd.Parameters.AddWithValue("@UniqueID", productId);
                            cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                            cmd.Parameters.AddWithValue("@ModuleID", 41);
                            cmd.Parameters.AddWithValue("@FileName", Strings.Split(imageURL, "/")[Information.UBound(Strings.Split(imageURL, "/"))]);
                            cmd.Parameters.AddWithValue("@FileSize", imageBytes.Length);
                            cmd.Parameters.AddWithValue("@ContentType", Strings.LCase("image/png"));
                            cmd.Parameters.AddWithValue("@isTemp", false);
                            cmd.Parameters.AddWithValue("@Approved", true);
                            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                            cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                            cmd.Parameters.AddWithValue("@FileData", imageBytes);
                            cmd.Parameters.AddWithValue("@ControlID", string.Empty);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    webClient.Dispose();
                }
                catch
                {
                    // Do Nothing
                }
        }

        /// <summary>
        /// Imports the wholesale2b.
        /// </summary>
        /// <param name="IntervalSeconds">The interval seconds.</param>
        /// <param name="ProcessID">The process identifier.</param>
        public void Import_Wholesale2b(int IntervalSeconds, string ProcessID)
        {
            try
            {
                using (var FeedConn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    FeedConn.Open();

                    using (var Feedcmd = new SqlCommand("SELECT * FROM Wholesale2b_Feeds WHERE FeedID=@FeedID ORDER BY LastRunTime DESC", FeedConn))
                    {
                        Feedcmd.Parameters.AddWithValue("@FeedID", ProcessID);
                        using (SqlDataReader RS = Feedcmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                Import_Wholesale2b_URL(SepFunctions.openNull(RS["FeedURL"]), SepFunctions.toLong(ProcessID), SepFunctions.openNull(RS["AccessKeys"]), SepFunctions.openBoolean(RS["AccessHide"]), SepFunctions.openBoolean(RS["ExcPortalSecurity"]), SepFunctions.openBoolean(RS["Sharing"]), SepFunctions.openNull(RS["PortalIDs"]));
                                Thread.Sleep(IntervalSeconds * 1000);
                            }
                        }
                    }

                    using (var Feedcmd = new SqlCommand("UPDATE Wholesale2b_Feeds SET LastRunTime=@LastRunTime WHERE FeedID=@FeedID", FeedConn))
                    {
                        Feedcmd.Parameters.AddWithValue("@LastRunTime", DateTime.Now);
                        Feedcmd.Parameters.AddWithValue("@FeedID", ProcessID);
                    }
                }
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>
        /// Imports the wholesale2b URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="FeedID">The feed identifier.</param>
        /// <param name="AccessKeys">The access keys.</param>
        /// <param name="AccessHide">if set to <c>true</c> [access hide].</param>
        /// <param name="ExcPortalSecurity">if set to <c>true</c> [exc portal security].</param>
        /// <param name="Sharing">if set to <c>true</c> [sharing].</param>
        /// <param name="PortalIDs">The portal i ds.</param>
        public void Import_Wholesale2b_URL(string url, long FeedID, string AccessKeys, bool AccessHide, bool ExcPortalSecurity, bool Sharing, string PortalIDs)
        {
            try
            {
                var engine = new DelimitedFileEngine(typeof(Models.Wholesale2b));
                engine.Options.Delimiter = ",";
                var csvData = SepFunctions.Send_Get(url);
                var records = engine.ReadString(csvData);

                foreach (Wholesale2b record in records)
                {
                    var Category = Server.DAL.Categories.Category_Get_By_Name(record.category_1, FeedID);
                    if (Category.CategoryName != record.category_1)
                        try
                        {
                            Server.DAL.Categories.Category_Save(SepFunctions.GetIdentity(), "0", record.category_1, string.Empty, "|41|", AccessKeys, AccessHide, "|2|", false, "|2|", string.Empty, string.Empty, string.Empty, PortalIDs, string.Empty, string.Empty, Sharing, ExcPortalSecurity, 0, string.Empty, string.Empty, FeedID);
                        }
                        catch
                        {
                            // Do Nothing
                        }

                    var Product = Server.DAL.ShoppingMall.Product_Get_By_ImportID(record.Item, FeedID);
                    long sProductId = 0;
                    if (Product.ImportID != record.Item)
                    {
                        sProductId = SepFunctions.GetIdentity();
                        Import_Image_From_URL(sProductId, record.URL_to_large_image);
                        Import_Image_From_URL(sProductId, record.Extra_Img_1);
                        Import_Image_From_URL(sProductId, record.Extra_Img_2);
                        Import_Image_From_URL(sProductId, record.Extra_Img_3);
                        Import_Image_From_URL(sProductId, record.Extra_Img_4);
                        Import_Image_From_URL(sProductId, record.Extra_Img_5);
                        Import_Image_From_URL(sProductId, record.Extra_Img_6);
                    }
                    else
                    {
                        sProductId = Product.ProductID;
                    }

                    try
                    {
                        Server.DAL.ShoppingMall.Product_Save(sProductId, Category.CatID, string.Empty, 0, record.Product_Name, record.description_no_html, record.Product_description_orignal_html, 0, SepFunctions.toDecimal(record.Retail_Price), 0, "1m", 0, string.Empty, string.Empty, SepFunctions.toDecimal(record.Weight), "lbs", SepFunctions.toDecimal(record.shipping_height), SepFunctions.toDecimal(record.shipping_width), SepFunctions.toDecimal(record.shipping_length), true, SepFunctions.toInt(record.Qty_in_stock), 0, SepFunctions.toInt(record.Qty_in_stock), "Shipping", false, SepFunctions.toDecimal(record.Supplier_handling_fee), record.manufacturer, Strings.Left(record.supplier_name_item_number, 25), string.Empty, 0, 0, false, 41, string.Empty, string.Empty, string.Empty, record.Item, record.In_Stock == "Y" ? 1 : 0, FeedID);
                    }
                    catch
                    {
                        // Do Nothing
                    }
                }
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>
        /// Processes the nightly news.
        /// </summary>
        public void Process_NightlyNews()
        {
            if (SepFunctions.Setup(24, "NewsletNightlyEnable") != "Yes")
            {
                return;
            }

            var LetterID = SepFunctions.toLong(SepFunctions.Setup(24, "NewsletNightlyNews"));
            if (LetterID == 0)
            {
                return;
            }

            var GetEmailBody = SepFunctions.Setup(24, "NewsletNightlyBody");
            var EmailSubject = SepFunctions.Setup(24, "NewsletNightlySubject");
            var GetNewsRemoveText = SepFunctions.Setup(24, "NewsletRemoveText");
            var GetAdminEmail = SepFunctions.Setup(991, "AdminEmailAddress");

            if (string.IsNullOrWhiteSpace(GetEmailBody) || string.IsNullOrWhiteSpace(EmailSubject) ||
                string.IsNullOrWhiteSpace(GetNewsRemoveText) || string.IsNullOrWhiteSpace(GetAdminEmail))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(SepFunctions.Setup(24, "NewsletFromEmail")))
            {
                var arrEmails = Strings.Split(SepFunctions.Setup(24, "NewsletFromEmail"), Environment.NewLine);
                GetAdminEmail = arrEmails[0];
            }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                // Send Newsletters or Index Database
                using (var cmd = new SqlCommand("SELECT DISTINCT EmailAddress,UserID FROM NewslettersUsers WHERE EmailAddress <> '' AND LetterID='" + SepFunctions.FixWord(Strings.ToString(LetterID)) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            try
                            {
                                GetEmailBody = SepFunctions.Replace_Widgets(GetEmailBody, 24, true);
                                if (!string.IsNullOrWhiteSpace(GetEmailBody))
                                {
                                    GetEmailBody += "<br/><br/><p align=\"center\"><a href=\"" + SepFunctions.GetMasterDomain(true) + "default.aspx?DoAction=NewsletterRemove&EmailAddress=" + SepFunctions.UrlEncode(SepFunctions.openNull(RS["EmailAddress"])) + "\" target=\"_blank\">" + Strings.ToString(!string.IsNullOrWhiteSpace(GetNewsRemoveText) ? GetNewsRemoveText : SepFunctions.LangText("Click here to unsubscribe to our newsletters.")) + "</a></p>";
                                    SepFunctions.Send_Email(SepFunctions.openNull(RS["EmailAddress"]), GetAdminEmail, EmailSubject, GetEmailBody, 24);
                                }
                                Thread.Sleep(10 * 1000);
                            }
                            catch
                            {
                                // Do Nothing
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            do
            {
                try
                {
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();

                        // Send Newsletters or Index Database
                        using (var cmd = new SqlCommand("SELECT ProcessID, ProcessName, IntervalSeconds, RecurringDays FROM BG_Processes WHERE ((ProcessName='Email' OR ProcessName='IndexDB') AND Status=1) OR (ProcessName='NightlyNews' AND (DATEADD(day, 1, DateEnded) < GETDATE() OR DateEnded IS NULL)) OR (ProcessName='Wholesale2b' AND (DATEADD(day, 1, DateEnded) < GETDATE() OR DateEnded IS NULL))", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                    using (var cmd2 = new SqlCommand("UPDATE BG_Processes SET DateStarted=@DateStarted WHERE Status=1", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@DateStarted", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }

                                while (RS.Read())
                                {
                                    switch (SepFunctions.openNull(RS["ProcessName"]))
                                    {
                                        case "Email":
                                            Send_Newsletters(SepFunctions.toInt(SepFunctions.openNull(RS["IntervalSeconds"])), SepFunctions.openNull(RS["ProcessID"]));
                                            using (var cmd2 = new SqlCommand("DELETE FROM BG_Emails WHERE ProcessID=@ProcessID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@ProcessID", SepFunctions.openNull(RS["ProcessID"]));
                                                cmd2.ExecuteNonQuery();
                                            }

                                            break;

                                        case "IndexDB":
                                            Server.LuceneNet.SepFunctions.ReindexDatabase();
                                            break;

                                        case "NightlyNews":
                                            Process_NightlyNews();
                                            break;

                                        case "Wholesale2b":
                                            Import_Wholesale2b(SepFunctions.toInt(SepFunctions.openNull(RS["IntervalSeconds"])), SepFunctions.openNull(RS["ProcessID"]));
                                            break;
                                    }

                                    using (var cmd2 = new SqlCommand("UPDATE BG_Processes SET Status='0',DateEnded=@DateEnded WHERE ProcessID=@ProcessID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@ProcessID", SepFunctions.openNull(RS["ProcessID"]));
                                        cmd2.Parameters.AddWithValue("@DateEnded", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        // Send Schedule SMS Messages
                        using (var cmd = new SqlCommand("SELECT BG.ProcessID,BG.ProcessName,BG.IntervalSeconds,SMS.To_Phone,SMS.From_Phone,SMS.Message_Body FROM BG_Processes AS BG, BG_SMS AS SMS WHERE BG.ProcessID=SMS.ProcessID AND SMS.Send_Date <= GETDATE() AND BG.ProcessName='SMS' AND BG.Status=1", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                    using (var cmd2 = new SqlCommand("UPDATE BG_Processes SET DateStarted=@DateStarted WHERE Status=1", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@DateStarted", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }

                                while (RS.Read())
                                {
                                    switch (SepFunctions.openNull(RS["ProcessName"]))
                                    {
                                        case "SMS":
                                            var cTwilio = new Server.Integrations.TwilioGlobal();
                                            cTwilio.Send_SMS(SepFunctions.openNull(RS["To_Phone"]), SepFunctions.openNull(RS["Message_Body"]));
                                            break;
                                    }

                                    using (var cmd2 = new SqlCommand("UPDATE BG_Processes SET Status='0',DateEnded=@DateEnded WHERE ProcessID=@ProcessID", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@ProcessID", SepFunctions.openNull(RS["ProcessID"]));
                                        cmd2.Parameters.AddWithValue("@DateEnded", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }

                                    switch (SepFunctions.openNull(RS["ProcessName"]))
                                    {
                                        case "SMS":
                                            using (var cmd2 = new SqlCommand("DELETE FROM BG_SMS WHERE ProcessID=@ProcessID", conn))
                                            {
                                                cmd2.Parameters.AddWithValue("@ProcessID", SepFunctions.openNull(RS["ProcessID"]));
                                                cmd2.ExecuteNonQuery();
                                            }

                                            break;
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch
                {
                    // Do Nothing
                }

                // run every minute
            }
            while (true);
        }
    }
}