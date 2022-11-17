// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="payments.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;

    /// <summary>
    /// Class payments.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class payments : Page
    {
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
            var str = new StringBuilder();

            var doAct = false;
            var skipNew = false;

            var GetResponse = string.Empty;

            var GetTransactionID = string.Empty;
            double GetAmount = 0;
            var GetInvoiceID = string.Empty;
            var GetCustId = string.Empty;
            var GetEmail = string.Empty;
            var GetPaymentMethod = string.Empty;

            var sActDesc = string.Empty;

            var GetUserID = string.Empty;

            var sendstr = Context.Request.Form + "&cmd=_notify-validate";

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("x_response_code")))
                GetResponse = SepCommon.SepCore.Request.Item("x_response_code");
            else
                try
                {
                    var objRequest = (HttpWebRequest)WebRequest.Create("https://www.paypal.com/spadmin/webscr");
                    objRequest.Method = "POST";
                    objRequest.ContentType = "application/x-www-form-urlencoded";
                    using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                    {
                        myWriter.Write(sendstr);
                    }
                    var objResponse = (HttpWebResponse)objRequest.GetResponse();
                    using (var sr = new StreamReader(objResponse.GetResponseStream()))
                    {
                        GetResponse = sr.ReadToEnd();
                    }
                }
                catch
                {
                }

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (GetResponse == "1" || GetResponse == "VERIFIED")
                {
                    if (GetResponse == "1")
                    {
                        GetEmail = SepCommon.SepCore.Request.Item("x_email");

                        GetPaymentMethod = "Authorize.Net";
                    }
                    else
                    {
                        GetTransactionID = Context.Request.Form["txn_id"];

                        GetInvoiceID = Context.Request.Form["invoice"];
                        GetCustId = Context.Request.Form["item_number"];

                        GetAmount = SepFunctions.toLong(Context.Request.Form["mc_gross"]);

                        GetEmail = Context.Request.Form["payer_email"];

                        GetPaymentMethod = "PayPal";
                    }

                    sActDesc = SepFunctions.LangText("Your transaction has been approved.") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Payment Method: ~~" + GetPaymentMethod + "~~") + Environment.NewLine;

                    if (!string.IsNullOrWhiteSpace(GetCustId))
                        using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE CustomerID='" + SepFunctions.FixWord(GetCustId) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    GetUserID = SepFunctions.openNull(RS["UserID"]);
                                }

                            }
                        }

                    if (!string.IsNullOrWhiteSpace(GetTransactionID))
                        using (var cmd = new SqlCommand("SELECT InvoiceID FROM Invoices WHERE TransactionID='" + SepFunctions.FixWord(GetTransactionID) + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows) skipNew = true;
                            }
                        }
                    else
                        skipNew = true;

                    if (skipNew == false)
                    {
                        if (!string.IsNullOrWhiteSpace(GetInvoiceID))
                            using (var cmd = new SqlCommand("SELECT InvoiceID,TransactionID FROM Invoices WHERE InvoiceID='" + SepFunctions.FixWord(GetInvoiceID) + "' AND Status='0'", conn))
                            {
                                using (SqlDataReader RS = cmd.ExecuteReader())
                                {
                                    while (RS.Read())
                                    {
                                        if (string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["TransactionID"])))
                                        {
                                            SepFunctions.PUB_Mark_Order_Paid(SepFunctions.openNull(RS["InvoiceID"]), GetTransactionID, GetPaymentMethod, GetEmail);
                                        }

                                        doAct = true;
                                    }

                                }
                            }

                        sActDesc += SepFunctions.LangText("Amount Paid: ~~" + SepCommon.SepCore.Strings.ToString(SepFunctions.Format_Currency(GetAmount)) + "~~") + Environment.NewLine;

                        if (doAct && !string.IsNullOrWhiteSpace(sActDesc))
                            SepFunctions.Activity_Write("PAYMENT", sActDesc, 995, GetInvoiceID, GetUserID);
                    }
                }
                else if (GetResponse == "2")
                {
                    sActDesc = SepFunctions.LangText("Your transaction has been declined.") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Payment Method: Unknown") + Environment.NewLine;

                    SepFunctions.Activity_Write("PAYMENTERROR", sActDesc, 995, GetInvoiceID);
                }
                else if (GetResponse == "3")
                {
                    sActDesc = SepFunctions.LangText("There has been an error processing this transaction.") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Code ~~" + SepCommon.SepCore.Request.Item("x_response_code") + "~~") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Description ~~" + SepCommon.SepCore.Request.Item("x_response_reason_text") + "~~") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Payment Method: Unknown") + Environment.NewLine;

                    SepFunctions.Activity_Write("PAYMENTERROR", sActDesc, 995, GetInvoiceID, GetUserID);
                }
                else
                {
                    sActDesc = SepFunctions.LangText("Transaction is being held for review.") + Environment.NewLine;
                    sActDesc += SepFunctions.LangText("Payment Method: Unknown") + Environment.NewLine;

                    SepFunctions.Activity_Write("PAYMENTERROR", sActDesc, 995, GetInvoiceID, GetUserID);
                }
            }

            PageContent.InnerHtml = SepCommon.SepCore.Strings.ToString(str);
        }
    }
}