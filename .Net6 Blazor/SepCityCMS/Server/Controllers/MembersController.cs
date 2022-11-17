
namespace SepCityCMS.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using SepCityCMS.Server;

    public class MembersController : ControllerBase
    {

        //[CheckOption("username", "Everyone")]
        [Route("api/members/authenticate")]
        [HttpPost]
        public Models.API.RecordResponse<Models.LoginResponse> Authenticate([FromBody] Models.LoginRequest account)
        {
            Models.API.RecordResponse<Models.LoginResponse> response = new Models.API.RecordResponse<Models.LoginResponse>();
            Models.LoginResponse record = new Models.LoginResponse();
            response.Status = 200;

            var LoginField = "UserName";
            var tryDatabase = false;
            var sUserID = string.Empty;
            var sAccessKeys = string.Empty;

            if (SepFunctions.ModuleActivated(68))
            {
                using (var LD = new SepCityCMS.Server.Integrations.LDAP())
                {
                    var LDLogin = LD.Login(account.UserName, account.Password);
                    if (Server.SepCore.Strings.Left(LDLogin, 7) == "USERID:")
                    {
                        record.Token = Server.SepCore.Strings.Split(Server.SepCore.Strings.Replace(LDLogin, "USERID:", ""), "||")[0];
                        record.UserID = Server.SepCore.Strings.Split(Server.SepCore.Strings.Replace(LDLogin, "USERID:", ""), "||")[0];
                        record.UserName = account.UserName;
                    }
                    else
                    {
                        tryDatabase = true;
                    }
                }
            }
            else
            {
                tryDatabase = true;
            }

            if (tryDatabase)
            {
                if (SepFunctions.Setup(997, "LoginEmail") == "Yes")
                {
                    LoginField = "EmailAddress";
                }

                using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT UserID,UserName,EmailAddress,Password,AccessKeys,Status FROM Members WHERE " + LoginField + "=@Username AND Status <> -1", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", account.UserName);
                        using (SqlDataReader RS = cmd.ExecuteReader())
                        {
                            if (!RS.HasRows)
                            {
                                sUserID = string.Empty;
                                sAccessKeys = string.Empty;
                                response.Status = 500;
                                response.Message = "Invalid Username and/or Password.";
                            } 
                            else
                            {
                                RS.Read();
                                sUserID = SepFunctions.openNull(RS["UserID"]);
                                sAccessKeys = SepFunctions.openNull(RS["AccessKeys"]);
                                if (SepFunctions.toLong(SepFunctions.openNull(RS["Status"])) == 0)
                                {
                                    sUserID = string.Empty;
                                    sAccessKeys = string.Empty;
                                    response.Status = 500;
                                    response.Message = "Error: Account Not Activated";
                                } else
                                {
                                    if (SepFunctions.Get_Portal_ID() > 0)
                                    {
                                        using (var cmd2 = new SqlCommand("SELECT LoginKeys FROM Portals WHERE PortalID=@PortalID", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@PortalID", SepFunctions.Get_Portal_ID());
                                            using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                            {
                                                if (RS2.HasRows)
                                                {
                                                    RS2.Read();
                                                    if (SepFunctions.CompareKeys(SepFunctions.openNull(RS2["LoginKeys"]), true, sUserID) == false)
                                                    {
                                                        sUserID = string.Empty;
                                                        sAccessKeys = string.Empty;
                                                        response.Status = 500;
                                                        response.Message = "Access denied to login to this portal.";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(sUserID))
                    {
                        // Write to online users table
                        using (var cmd = new SqlCommand("INSERT INTO OnlineUsers(UserID, Location, LoginTime, LastActive, isChatting) VALUES (@UserID, 'Login', @LoginTime, @LastActive, '0')", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", sUserID);
                            cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@LastActive", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("UPDATE Members SET LastLogin=@LastLogin, IPAddress=@IPAddress WHERE UserID=@UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", sUserID);
                            cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                            cmd.Parameters.AddWithValue("@IPAddress", Server.SepFunctions.GetUserIP());
                            cmd.ExecuteNonQuery();
                        }

                        // Write activity
                        var sActDesc = Server.SepFunctions.LangText("[[Username]] successfully logged into your web site.") + Environment.NewLine;
                        Server.SepFunctions.Activity_Write("LOGIN", sActDesc, 21, string.Empty, sUserID);

                        record.UserID = sUserID;
                        record.UserName = account.UserName;

                        var token = new SepCityCMS.Server.Security.Bearer.Helpers.JwtTokenBuilder()
                                            .AddSecurityKey(SepCityCMS.Server.Security.Bearer.Helpers.JwtSecurityKey.Create("fiver-secret-key"))
                                            .AddSubject(account.UserName + " Login Token")
                                            .AddIssuer("SepCity.Security.Bearer")
                                            .AddAudience("SepCity.Security.Bearer")
                                            .AddClaim("UserId", sUserID)
                                            .AddClaim("UserName", account.UserName)
                                            .AddClaim("AccessKeys", sAccessKeys)
                                            .AddExpiry(1)
                                            .Build();
                        record.Token = token.Value;
                    }
                }
            }

            response.Record = record;
            return response;
        }

        [CheckOption("username", "AdminUserMan")]
        [Route("api/members/dailysignups")]
        [HttpGet]
        public List<Models.ChartData> DailySignups()
        {
            return Server.DAL.Members.DailySignups();
        }

        [CheckOption("username", "AdminUserMan")]
        [Route("api/members/monthlysignups")]
        [HttpGet]
        public List<Models.ChartData> MonthlySignups()
        {
            return Server.DAL.Members.MonthlySignups();
        }
    }
}