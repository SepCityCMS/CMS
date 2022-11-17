// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="HotOrNot.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.DAL
{
    using SepCommon.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class HotOrNot.
    /// </summary>
    public static class HotOrNot
    {
        /// <summary>
        /// Gets the hot or not pictures.
        /// </summary>
        /// <param name="SortExpression">The sort expression.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="searchWords">The search words.</param>
        /// <returns>List&lt;Models.HotOrNotPictures&gt;.</returns>
        public static List<HotOrNotPictures> GetHotOrNotPictures(string SortExpression = "TotalRates", string SortDirection = "DESC", string searchWords = "")
        {
            var lHotOrNotPictures = new List<HotOrNotPictures>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "TotalRates";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (M.Username LIKE '%" + SepFunctions.FixWord(searchWords) + "%' OR M.FirstName LIKE '%" + SepFunctions.FixWord(searchWords) + "%' OR M.LastName LIKE '%" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UL.UserRates,UL.TotalRates,M.Username,M.FirstName,M.LastName,M.City,M.State FROM Profiles AS P,Uploads AS UL,Members AS M WHERE M.UserID=UL.UserID AND P.UserID=UL.UserID" + SepCore.Strings.ToString(SepFunctions.Setup(60, "PortalProfiles") == "Yes" ? string.Empty : " AND P.PortalID=@PortalID") + " AND UL.isTemp='0' AND UL.Approved='1' AND UL.ModuleID='63' AND M.Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                for (var i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows.Count == i)
                    {
                        break;
                    }

                    var dHotOrNotPictures = new Models.HotOrNotPictures { Username = SepFunctions.openNull(ds.Tables[0].Rows[i]["Username"]) };
                    dHotOrNotPictures.FirstName = SepFunctions.openNull(ds.Tables[0].Rows[i]["FirstName"]);
                    dHotOrNotPictures.LastName = SepFunctions.openNull(ds.Tables[0].Rows[i]["LastName"]);
                    dHotOrNotPictures.City = SepFunctions.openNull(ds.Tables[0].Rows[i]["City"]);
                    dHotOrNotPictures.State = SepFunctions.openNull(ds.Tables[0].Rows[i]["State"]);
                    dHotOrNotPictures.TotalRates = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["TotalRates"]));
                    dHotOrNotPictures.UserRates = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["UserRates"]));
                    lHotOrNotPictures.Add(dHotOrNotPictures);
                }
            }

            return lHotOrNotPictures;
        }

        /// <summary>
        /// Pictures the save.
        /// </summary>
        /// <param name="UploadID">The upload identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int Picture_Save(long UploadID, string UserID)
        {
            int intReturn = SepFunctions.Additional_Data_Save(false, 40, SepCore.Strings.ToString(UploadID), UserID, SepFunctions.GetUserInformation("UserName", UserID), "HotOrNot", "Hot or Not", string.Empty);
            if (intReturn == 0)
            {
                return 3;
            }
            else
            {
                return intReturn;
            }
        }

        /// <summary>
        /// Randoms the picture.
        /// </summary>
        /// <param name="ShowSex">The show sex.</param>
        /// <returns>Models.HotOrNotPictures.</returns>
        public static HotOrNotPictures Random_Picture(string ShowSex)
        {
            var returnXML = new Models.HotOrNotPictures();

            var wClause = string.Empty;
            long RecordCounter = 0;
            switch (ShowSex)
            {
                case "Male":
                    wClause = " AND M.Male='1'";
                    break;

                case "Female":
                    wClause = " AND M.Male='0'";
                    break;
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT UL.UploadID,UL.FileName,M.Username,M.UserID FROM Uploads AS UL,Members AS M WHERE Cast(UL.UploadID AS Varchar) NOT IN (SELECT UniqueID FROM Activities WHERE IPAddress='" + SepFunctions.FixWord(SepFunctions.GetUserIP()) + "' AND ModuleID='40') AND (UL.ModuleID='63' AND UL.UserID IN (SELECT UserID FROM Profiles WHERE HotOrNot='1' AND UserID=UL.UserID) OR UL.ModuleID='40') AND UL.Approved='1' AND UL.isTemp='0' AND UL.UserID=M.UserID AND UL.PortalID=@PortalID" + wClause, conn))
                    {
                        cmd.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }

                RecordCounter = ds.Tables[0].Rows.Count - 1;
                if (RecordCounter > 0)
                {
                    int rndNumber;
                    if (RecordCounter == 1)
                    {
                        rndNumber = 0;
                    }
                    else
                    {
                        var random = new Random();
                        rndNumber = random.Next(0, Convert.ToInt32(RecordCounter));
                    }

                    if (rndNumber == -1)
                    {
                        rndNumber = 0;
                    }

                    returnXML.UploadID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[rndNumber]["UploadID"]));
                    returnXML.Username = SepFunctions.openNull(ds.Tables[0].Rows[rndNumber]["Username"]);
                    returnXML.UserID = SepFunctions.openNull(ds.Tables[0].Rows[rndNumber]["UserID"]);
                }
            }

            return returnXML;
        }

        /// <summary>
        /// Rates the picture.
        /// </summary>
        /// <param name="Rating">The rating.</param>
        /// <param name="UploadID">The upload identifier.</param>
        /// <returns>System.String.</returns>
        public static string Rate_Picture(int Rating, long UploadID)
        {
            double strAdverage = 0;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Uploads SET UserRates=UserRates+@UserRates, TotalRates=TotalRates+1 WHERE (ModuleID='40' OR ModuleID='63') AND UploadID=@UploadID AND Cast(UploadID AS Varchar) NOT IN (SELECT UniqueID FROM Activities WHERE IPAddress='" + SepFunctions.FixWord(SepFunctions.GetUserIP()) + "' AND ModuleID='40')", conn))
                {
                    cmd.Parameters.AddWithValue("@UserRates", Rating);
                    cmd.Parameters.AddWithValue("@UploadID", UploadID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("SELECT UniqueID FROM Activities WHERE IPAddress='" + SepFunctions.FixWord(SepFunctions.GetUserIP()) + "' AND ModuleID='40' AND UniqueID='" + SepFunctions.FixWord(SepCore.Strings.ToString(UploadID)) + "'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            SepFunctions.Activity_Write("HOTRATE", "Hot or not rating", 40, SepCore.Strings.ToString(UploadID), SepFunctions.Session_User_ID(), SepFunctions.GetUserIP());
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT UL.UserRates,UL.TotalRates,UL.FileName,M.UserName FROM Uploads AS UL,Members AS M WHERE UL.UserID=M.UserID AND UL.UploadID='" + SepFunctions.FixWord(SepCore.Strings.ToString(UploadID)) + "' AND UL.PortalID='" + SepFunctions.Get_Portal_ID() + "' AND (UL.ModuleID='63' OR UL.ModuleID='40') AND UL.isTemp='0' AND UL.Approved='1'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            strAdverage = Math.Round(SepFunctions.toDouble(SepFunctions.openNull(RS["UserRates"])) / SepFunctions.toDouble(SepFunctions.openNull(RS["TotalRates"])), 1);
                        }
                    }
                }
            }

            return SepCore.Strings.ToString(strAdverage);
        }

        /// <summary>
        /// Resets the ratings.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Reset_Ratings()
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Uploads SET UserRates='0', TotalRates='0' WHERE (ModuleID='40' OR ModuleID='63')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn;

            sReturn = SepFunctions.LangText("Ratings have been successfully reset.");

            return sReturn;
        }
    }
}