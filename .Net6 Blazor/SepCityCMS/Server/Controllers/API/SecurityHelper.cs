using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;
using System.Security.Claims;

namespace SepCityCMS.Server.Controllers
{

    public class CheckOptionAttribute : TypeFilterAttribute
    {
        public CheckOptionAttribute(string claimType, string claimValue) : base(typeof(CheckOptionFilter))
        {
            claimValue = Server.SepFunctions.Security(claimValue);
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class CheckOptionFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public CheckOptionFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = false;

            try
            {
                hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && Server.SepFunctions.CompareKeys(_claim.Value, userKeys: c.Value));
            }
            catch
            {
                hasClaim = false;
            }

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class AuthHandler : IAuthenticationHandler
    {
        public const string SchemeName = "default scheme";

        AuthenticationScheme? _scheme;
        HttpContext _context;

        /// <summary>
        ///Initialize authentication
        /// </summary>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }

        /// <summary>
        ///Authentication processing
        /// </summary>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            //check if user is logged in
            //var req = _context.Request.Form;
            var req = _context.Request.Query;
            //var isLogin = req["isLogin"].FirstOrDefault();
            AuthenticationTicket ticket = null;

            string sResponse = Server.DAL.Members.Login(req["Username"], req["Password"], "", "", "", Server.SepFunctions.Get_Portal_ID(), false, "");

            if (Server.SepCore.Strings.Left(sResponse, 7) == "USERID:")
            {
                sResponse = Server.SepCore.Strings.Replace(sResponse, "USERID:", string.Empty);
                string UserID = Server.SepCore.Strings.Split(sResponse, "||")[0];
                string Username = Server.SepCore.Strings.Split(sResponse, "||")[1];
                string Password = Server.SepCore.Strings.Split(sResponse, "||")[2];
                string AccessKeys = Server.SepCore.Strings.Split(sResponse, "||")[3];
                ticket = GetAuthTicket("username", AccessKeys);

                using (var conn = new SqlConnection(Server.SepFunctions.Database_Connection()))
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
                                long UserStatus = Server.SepFunctions.toInt(Server.SepFunctions.openNull(RS["Status"]));
                                if (UserStatus == 0)
                                {
                                    return Task.FromResult(AuthenticateResult.Fail("Account is not yet activated."));
                                }
                            }
                            else
                            {
                                return Task.FromResult(AuthenticateResult.Fail("Invalid Username and/or Password."));
                            }
                        }
                    }

                    // Check if user has access to login to a subPortal
                    if (Server.SepFunctions.Get_Portal_ID() > 0 && Server.SepFunctions.ModuleActivated(60))
                        using (var cmd = new SqlCommand("SELECT LoginKeys FROM Portals WHERE PortalID=@PortalID AND Status <> -1", conn))
                        {
                            cmd.Parameters.AddWithValue("@PortalID", Server.SepFunctions.Get_Portal_ID());
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    RS.Read();
                                    if (Server.SepFunctions.CompareKeys(Server.SepFunctions.openNull(RS["LoginKeys"]), true, UserID) == false)
                                    {
                                        return Task.FromResult(AuthenticateResult.Fail("Invalid Username and/or Password."));
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
                        cmd.Parameters.AddWithValue("@IPAddress", Server.SepFunctions.GetUserIP());
                        cmd.ExecuteNonQuery();
                    }

                    // Write activity
                    var sActDesc = Server.SepFunctions.LangText("[[Username]] successfully logged into your web site.") + Environment.NewLine;
                    Server.SepFunctions.Activity_Write("LOGIN", sActDesc, 21, string.Empty, UserID);

                }
            }

            //if (isLogin != "true")
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("not logged in"));
            //}

            if (ticket == null)
            {
                ticket = GetAuthTicket("username", "|1|");
            }
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        AuthenticationTicket GetAuthTicket(string clainType, string claimValue)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(clainType, claimValue)
            }, "SepCity");

            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }

        /// <summary>
        ///Handling of insufficient authority
        /// </summary>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        /// <summary>
        ///Processing when not logged in
        /// </summary>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }
    }

}
