// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="StockQuotes.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Controls
{
    using System.Data.SqlClient;
    using System.Text;

    /// <summary>
    /// Class StockQuotes.
    /// </summary>
    public class StockQuotes
    {
        /// <summary>
        /// Renders the contents.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Render()
        {
            StringBuilder output = new StringBuilder();

            var StocksUserQuotes = string.Empty;

            var GetStockSymbols = SepFunctions.Setup(15, "StocksSymbols");

            var sInstallFolder = SepFunctions.GetInstallFolder();

            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                using (SqlConnection conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Symbols FROM Stocks WHERE UserID=@UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SepFunctions.Session_User_ID());
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                while (RS.Read())
                                {
                                    if (!string.IsNullOrWhiteSpace(SepFunctions.openNull(RS["Symbols"])))
                                    {
                                        StocksUserQuotes = SepFunctions.openNull(RS["Symbols"]);
                                    }
                                    else
                                    {
                                        StocksUserQuotes = GetStockSymbols;
                                    }
                                }
                            }
                            else
                            {
                                StocksUserQuotes = GetStockSymbols;
                            }
                        }
                    }
                }
            }
            else
            {
                StocksUserQuotes = GetStockSymbols;
            }

            if (!string.IsNullOrWhiteSpace(StocksUserQuotes))
            {
                output.AppendLine("<iframe width=\"160\" height=\"120\" scrolling=\"no\" frameborder=\"0\" style=\"border:none;\" src=\"http://widgets.freestockcharts.com/WidgetServer/WatchListWidget.aspx?sym=" + StocksUserQuotes + "&style=WLBlueStyle&w=160\"></iframe>");
            }

            output.AppendLine("<br/><a href=\"" + sInstallFolder + "stocks.aspx\">" + SepFunctions.LangText("Customize Stocks") + "</a>");

            return output.ToString();
        }
    }
}