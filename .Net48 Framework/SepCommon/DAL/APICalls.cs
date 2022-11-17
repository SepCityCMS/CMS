namespace SepCommon.DAL
{
    using SepCommon.SepCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public static class APICalls
    {
        public static string APICallDelete(string APIIDs)
        {
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                var arrAPIIDs = Strings.Split(APIIDs, ",");

                if (arrAPIIDs != null)
                {
                    for (var i = 0; i <= Information.UBound(arrAPIIDs); i++)
                    {
                        using (var cmd = new SqlCommand("UPDATE APICalls SET Status='-1', DateDeleted=@DateDeleted WHERE APIID=@APIID", conn))
                        {
                            cmd.Parameters.AddWithValue("@APIID", arrAPIIDs[i]);
                            cmd.Parameters.AddWithValue("@DateDeleted", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            return SepFunctions.LangText("API Calls(s) has been successfully deleted.");
        }

        public static Models.APICalls APICallGet(long APIID)
        {
            var returnXML = new Models.APICalls();

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM APICalls WHERE APIID=@APIID AND Status <> -1", conn))
                {
                    cmd.Parameters.AddWithValue("@APIID", APIID);
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            RS.Read();
                            returnXML.APIID = SepFunctions.toLong(SepFunctions.openNull(RS["APIID"]));
                            returnXML.Method = SepFunctions.openNull(RS["Method"]);
                            returnXML.ApiURL = SepFunctions.openNull(RS["ApiURL"]);
                            returnXML.ApiHeaders = SepFunctions.openNull(RS["ApiHeaders"]);
                            returnXML.ApiBody = SepFunctions.openNull(RS["ApiBody"]);
                            returnXML.Status = SepFunctions.toInt(SepFunctions.openNull(RS["Status"]));
                        }
                    }
                }
            }

            return returnXML;
        }

        public static string APICallSave(long APIID, string Method, string ApiURL, string ApiHeaders, string ApiBody)
        {
            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();

                if (APIID > 0)
                {
                    using (var cmd = new SqlCommand("SELECT APIID FROM APICalls WHERE APIID=@APIID", conn))
                    {
                        cmd.Parameters.AddWithValue("@APIID", APIID);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows)
                            {
                                bUpdate = true;
                            }
                        }
                    }
                }
                else
                {
                    APIID = SepFunctions.GetIdentity();
                }

                var SqlStr = string.Empty;
                if (bUpdate)
                {
                    SqlStr = "UPDATE APICalls SET Method=@Method, ApiURL=@ApiURL, ApiHeaders=@ApiHeaders, ApiBody=@ApiBody WHERE APIID=@APIID";
                }
                else
                {
                    SqlStr = "INSERT INTO APICalls (APIID, Method, ApiURL, ApiHeaders, ApiBody, Status) VALUES (@APIID, @Method, @ApiURL, @ApiHeaders, @ApiBody, '1')";
                }

                using (var cmd = new SqlCommand(SqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@APIID", APIID);
                    cmd.Parameters.AddWithValue("@Method", Method);
                    cmd.Parameters.AddWithValue("@ApiURL", ApiURL);
                    cmd.Parameters.AddWithValue("@ApiHeaders", ApiHeaders);
                    cmd.Parameters.AddWithValue("@ApiBody", ApiBody);
                    cmd.ExecuteNonQuery();
                }
            }

            string sReturn = SepFunctions.LangText("API Call has been successfully added.");

            if (bUpdate)
            {
                sReturn = SepFunctions.LangText("API call has been successfully updated.");
            }

            return sReturn;
        }

        public static List<Models.APICalls> GetAPICalls(string SortExpression = "Method", string SortDirection = "DESC", string searchWords = "")
        {
            var lAPICalls = new List<Models.APICalls>();

            var wClause = string.Empty;

            if (string.IsNullOrWhiteSpace(SortExpression))
            {
                SortExpression = "Method";
            }

            if (string.IsNullOrWhiteSpace(SortDirection))
            {
                SortDirection = "DESC";
            }

            if (!string.IsNullOrWhiteSpace(searchWords))
            {
                wClause = " AND (ApiURL LIKE '" + SepFunctions.FixWord(searchWords) + "%')";
            }

            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    using (var cmd = new SqlCommand("SELECT APIID,Method,ApiURL FROM APICalls WHERE Status <> -1" + wClause + " ORDER BY " + SortExpression + " " + SortDirection, conn))
                    {
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

                    var dAPICalls = new Models.APICalls { APIID = SepFunctions.toLong(SepFunctions.openNull(ds.Tables[0].Rows[i]["APIID"])) };
                    dAPICalls.Method = SepFunctions.openNull(ds.Tables[0].Rows[i]["Method"]);
                    dAPICalls.ApiURL = SepFunctions.openNull(ds.Tables[0].Rows[i]["ApiURL"]);
                    lAPICalls.Add(dAPICalls);
                }
            }

            return lAPICalls;
        }
    }
}
