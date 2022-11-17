// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="ShoppingCart.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class ShoppingCart.
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            long TotalItems = 0;
            var TotalPrice = SepFunctions.Format_Currency(0);

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_Invoice_ID()))
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Count(INV.InvoiceID) AS TotalItems,Sum(Cast(RecurringPrice AS Money) + Cast(UnitPrice as Money) * Cast(Quantity as Int)) AS TotalPrice FROM Invoices_Products AS IP,Invoices AS INV WHERE INV.InvoiceID=IP.InvoiceID AND INV.InvoiceID='" + SepFunctions.FixWord(SepFunctions.Session_Invoice_ID()) + "' AND INV.Status='0' AND INV.inCart='1'", conn))
                    {
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                RS.Read();
                                if (SepFunctions.toDouble(SepFunctions.openNull(RS["TotalItems"])) > 0)
                                {
                                    TotalItems = SepFunctions.toLong(SepFunctions.openNull(RS["TotalItems"]));
                                    TotalPrice = SepFunctions.Format_Currency(SepFunctions.openNull(RS["TotalPrice"]));
                                }
                            }
                        }
                    }
                }
            }

            output.AppendLine("<div class=\"ViewCartDiv\">");
            output.AppendLine("<div class=\"ViewCartTitle\">" + SepFunctions.LangText("Your Cart") + "</div>");
            output.Append(SepFunctions.LangText("~~" + TotalItems + "~~ total item(s).") + "<br/>");
            output.Append(SepFunctions.LangText("Cost: ~~" + TotalPrice + "~~") + "<br/>");
            output.AppendLine("<a href=\"" + sInstallFolder + "viewcart.aspx\">" + SepFunctions.LangText("View Cart") + "</a><br/>");
            output.AppendLine("</div>");

            return output.ToString();
        }
    }
}