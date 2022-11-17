// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="LDAP.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using SepCommon.Core.SepCore;
    using System;
    using System.Data.SqlClient;
    using System.DirectoryServices;
    using System.Text;

    /// <summary>
    /// The cls ldap.
    /// </summary>
    public class LDAP : IDisposable
    {
        /// <summary>
        /// The uf accountdisable.
        /// </summary>
        private const int UF_ACCOUNTDISABLE = 0x0002;

        /// <summary>
        /// The domain.
        /// </summary>
        private readonly string _domain;

        /// <summary>
        /// The common.
        /// </summary>
        private string _filterAttribute;

        /// <summary>
        /// Full pathname of the file.
        /// </summary>
        private string _path;

        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LDAP()
        {
            _path = SepFunctions.Setup(68, "LDAPPath");
            _domain = SepFunctions.Setup(68, "LDAPDomain");
        }

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// First time install.
        /// </summary>
        public void First_Time_Install()
        {
            // Import LDAP Groups
            var arrGroups = Strings.Split(GetGroups(), "||");

            var bUpdate = false;

            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM AccessKeys", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM AccessClasses", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("DELETE FROM LDAPGroups", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            if (arrGroups != null)
            {
                for (var i = 1; i < Information.UBound(arrGroups); i++)
                {
                    var arrSplitGroups = Strings.Split(arrGroups[i], "$$");
                    Array.Resize(ref arrSplitGroups, 2);
                    bUpdate = false;
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT KeyID FROM LDAPGroups WHERE LDAP_GUID='" + arrSplitGroups[1] + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    bUpdate = true;
                                }
                            }
                        }

                        if (bUpdate)
                        {
                            using (var cmd = new SqlCommand("UPDATE LDAPGroups SET Group_Name=@Group_Name, DateUpdated=@DateUpdated WHERE LDAP_GUID=@LDAP_GUID", conn))
                            {
                                cmd.Parameters.AddWithValue("@Group_Name", arrSplitGroups[0]);
                                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                                cmd.Parameters.AddWithValue("@LDAP_GUID", arrSplitGroups[1]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            var KeyId = Strings.ToString(SepFunctions.GetIdentity());

                            switch (arrSplitGroups[0])
                            {
                                case "Administrators":
                                    KeyId = "2";
                                    break;

                                case "Users":
                                    KeyId = "4";
                                    break;

                                case "Everyone":
                                    KeyId = "1";
                                    break;

                                default:
                                    KeyId = Strings.ToString(SepFunctions.GetIdentity());
                                    break;
                            }

                            using (var cmd = new SqlCommand("INSERT INTO LDAPGroups (KeyID, LDAP_GUID, Group_Name, DateAdded, DateUpdated) VALUES(@KeyID, @LDAP_GUID, @Group_Name, @DateAdded, @DateUpdated)", conn))
                            {
                                cmd.Parameters.AddWithValue("@KeyID", KeyId);
                                cmd.Parameters.AddWithValue("@LDAP_GUID", arrSplitGroups[1]);
                                cmd.Parameters.AddWithValue("@Group_Name", arrSplitGroups[0]);
                                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES(@KeyID, @KeyName, '1')", conn))
                            {
                                cmd.Parameters.AddWithValue("@KeyID", KeyId);
                                cmd.Parameters.AddWithValue("@KeyName", arrSplitGroups[0]);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("INSERT INTO AccessClasses (ClassID, ClassName, KeyIDs, PrivateClass, PortalIDs, Status) VALUES(@ClassID, @ClassName, @KeyIDs, 0, '|-1|', '1')", conn))
                            {
                                cmd.Parameters.AddWithValue("@ClassID", KeyId);
                                cmd.Parameters.AddWithValue("@ClassName", arrSplitGroups[0]);
                                cmd.Parameters.AddWithValue("@KeyIDs", "|" + KeyId + "|");
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            // Add default keys if not found in groups
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT KeyID FROM AccessKeys WHERE KeyID='1'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            using (var cmd2 = new SqlCommand("INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES('1', 'Everyone', '1')", conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT KeyID FROM AccessKeys WHERE KeyID='2'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            using (var cmd2 = new SqlCommand("INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES('2', 'Administrator', '1')", conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT KeyID FROM AccessKeys WHERE KeyID='4'", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (!RS.HasRows)
                        {
                            using (var cmd2 = new SqlCommand("INSERT INTO AccessKeys (KeyID, KeyName, Status) VALUES('4', 'Users', '1')", conn))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            // Import users
            using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Members", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            var arrUsers = Strings.Split(GetUsers(), "||");

            bUpdate = false;

            if (arrUsers != null)
            {
                for (var i = 1; i < Information.UBound(arrUsers); i++)
                {
                    var arrSplitUsers = Strings.Split(arrUsers[i], "$$");
                    Array.Resize(ref arrSplitUsers, 7);
                    bUpdate = false;
                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserID='" + arrSplitUsers[0] + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows)
                                {
                                    bUpdate = true;
                                }
                            }
                        }

                        var AccessKeys = string.Empty;
                        var KeyCount = 0;
                        var isAdmin = false;
                        var arrGroupNames = Strings.Split(arrSplitUsers[6], "][");
                        if (arrGroupNames != null)
                        {
                            for (var j = 0; j <= Information.UBound(arrGroupNames); j++)
                            {
                                using (var cmd = new SqlCommand("SELECT KeyID FROM LDAPGroups WHERE Group_Name='" + arrGroupNames[j] + "'", conn))
                                {
                                    using (SqlDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows)
                                        {
                                            RS.Read();
                                            if (SepFunctions.openNull(RS["KeyID"]) == "2")
                                            {
                                                isAdmin = true;
                                            }

                                            if (KeyCount > 0)
                                            {
                                                AccessKeys += ",";
                                            }

                                            AccessKeys += "|" + SepFunctions.openNull(RS["KeyID"]) + "|";
                                            KeyCount += 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (string.IsNullOrWhiteSpace(AccessKeys))
                        {
                            AccessKeys = "|1|";
                        }

                        if (bUpdate)
                        {
                            using (var cmd = new SqlCommand("UPDATE Members SET UserName=@UserName, FirstName=@FirstName, LastName=@LastName, EmailAddress=@EmailAddress, Password=@Password, AccessClass=@AccessClass, AccessKeys=@AccessKeys, Status=@Status WHERE UserID=@UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserName", Strings.Left(arrSplitUsers[1], 25));
                                cmd.Parameters.AddWithValue("@FirstName", Strings.Left(arrSplitUsers[2], 50));
                                cmd.Parameters.AddWithValue("@LastName", Strings.Left(arrSplitUsers[3], 50));
                                cmd.Parameters.AddWithValue("@EmailAddress", Strings.Left(arrSplitUsers[4], 100));
                                cmd.Parameters.AddWithValue("@Password", Strings.Left(SepFunctions.Save_Password(arrSplitUsers[1]), 100));
                                if (isAdmin)
                                {
                                    cmd.Parameters.AddWithValue("@AccessClass", "2");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@AccessClass", "4");
                                }

                                cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                                if (arrSplitUsers[5] == "enabled")
                                {
                                    cmd.Parameters.AddWithValue("@Status", 1);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Status", 0);
                                }

                                cmd.Parameters.AddWithValue("@UserID", arrSplitUsers[0]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (var cmd = new SqlCommand("INSERT INTO Members (UserID, UserName, FirstName, LastName, EmailAddress, Password, AccessClass, AccessKeys, Status, CreateDate, PortalID, UserPoints, ApproveFriends, ClassChanged) VALUES(@UserID, @UserName, @FirstName, @LastName, @EmailAddress, @Password, @AccessClass, @AccessKeys, @Status, @CreateDate, '0', '0', 'Yes', @ClassChanged)", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserName", Strings.Left(arrSplitUsers[1], 25));
                                cmd.Parameters.AddWithValue("@FirstName", Strings.Left(arrSplitUsers[2], 50));
                                cmd.Parameters.AddWithValue("@LastName", Strings.Left(arrSplitUsers[3], 50));
                                cmd.Parameters.AddWithValue("@EmailAddress", Strings.Left(arrSplitUsers[4], 100));
                                cmd.Parameters.AddWithValue("@Password", Strings.Left(SepFunctions.Save_Password(arrSplitUsers[1]), 100));
                                if (isAdmin)
                                {
                                    cmd.Parameters.AddWithValue("@AccessClass", "2");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@AccessClass", "4");
                                }

                                cmd.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                                if (arrSplitUsers[5] == "enabled")
                                {
                                    cmd.Parameters.AddWithValue("@Status", 1);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Status", 0);
                                }

                                cmd.Parameters.AddWithValue("@UserID", arrSplitUsers[0]);
                                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@ClassChanged", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <returns>The groups.</returns>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        public string GetGroups()
        {
            var deSearch = new DirectorySearcher(_path);

            SearchResultCollection results;
            var groupNames = new StringBuilder();
            try
            {
                deSearch.Filter = "(&(objectCategory=group))";
                deSearch.SearchScope = SearchScope.Subtree;
                deSearch.PageSize = 1000;
                results = deSearch.FindAll();

                groupNames.Append("||");
                foreach (SearchResult result in results)
                {
                    groupNames.Append(result.Properties["cn"][0]);
                    groupNames.Append("$$" + new Guid((byte[])result.Properties["objectguid"][0]));
                    groupNames.Append("||");
                }
            }
            catch
            {
            }
            finally
            {
                deSearch.Dispose();
            }

            return Strings.ToString(groupNames);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>The users.</returns>
        /// <exception cref="Exception">Error obtaining users. " + ex.Message</exception>
        public string GetUsers()
        {
            var userNames = new StringBuilder();
            try
            {
                var searchRoot = new DirectoryEntry(_path);
                var search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectCategory=Person)(objectClass=user))";
                search.PropertiesToLoad.Add("objectguid");
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("displayname"); // first name
                search.PropertiesToLoad.Add("sn"); // last name
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("userAccountControl");
                search.PropertiesToLoad.Add("memberOf");
                SearchResult result;
                var resultCol = search.FindAll();
                userNames.Append("||");
                for (var counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname"))
                    {
                        userNames.Append(new Guid((byte[])result.Properties["objectguid"][0]).ToString());
                        userNames.Append("$$");
                        userNames.Append((string)result.Properties["samaccountname"][0]);
                        userNames.Append("$$");
                        if (result.Properties.Contains("displayname"))
                        {
                            userNames.Append((string)result.Properties["displayname"][0]);
                        }

                        userNames.Append("$$");
                        if (result.Properties.Contains("sn"))
                        {
                            userNames.Append((string)result.Properties["sn"][0]);
                        }

                        userNames.Append("$$");
                        if (result.Properties.Contains("mail"))
                        {
                            userNames.Append((string)result.Properties["mail"][0]);
                        }

                        userNames.Append("$$");
                        if (result.Properties.Contains("userAccountControl"))
                        {
                            var flags = (int)result.Properties["userAccountControl"][0];
                            if (Convert.ToBoolean(flags & UF_ACCOUNTDISABLE))
                            {
                                userNames.Append("disabled");
                            }
                            else
                            {
                                userNames.Append("enabled");
                            }
                        }
                        else
                        {
                            userNames.Append("enabled");
                        }

                        // GET GROUPS
                        var propertyCount = result.Properties["memberOf"].Count;
                        string dn;
                        int equalsIndex, commaIndex;
                        var groupNames = new StringBuilder();
                        for (var propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                        {
                            dn = (string)result.Properties["memberOf"][propertyCounter];

                            equalsIndex = dn.IndexOf("=", 1, StringComparison.OrdinalIgnoreCase);
                            commaIndex = dn.IndexOf(",", 1, StringComparison.OrdinalIgnoreCase);
                            if (-1 == equalsIndex)
                            {
                                search.Dispose();
                                searchRoot.Dispose();
                                return null;
                            }

                            if (propertyCounter > 0)
                            {
                                groupNames.Append("][");
                            }

                            groupNames.Append(dn.Substring(equalsIndex + 1, commaIndex - equalsIndex - 1));
                        }

                        userNames.Append("$$" + groupNames);

                        // END GROUPS
                        userNames.Append("||");
                    }
                }

                search.Dispose();
                searchRoot.Dispose();

                return Strings.ToString(userNames);
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining users. " + ex.Message);
            }
        }

        /// <summary>
        /// Query if 'domain' is authenticated.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="username">The username.</param>
        /// <param name="pwd">The password.</param>
        /// <returns>True if authenticated, false if not.</returns>
        /// <exception cref="Exception">Error authenticating user. " + ex.Message</exception>
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            var domainAndUsername = domain + @"\" + username;
            var entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                var search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                var result = search.FindOne();
                search.Dispose();

                if (null == result)
                {
                    entry.Dispose();
                    return false;
                }

                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                entry.Dispose();
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            entry.Dispose();

            return true;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="pass">The pass.</param>
        /// <returns>A string.</returns>
        public string Login(string user, string pass)
        {
            try
            {
                if (IsAuthenticated(_domain, user, pass))
                {
                    var search = new DirectorySearcher(_path);
                    search.Filter = "(cn=" + _filterAttribute + ")";
                    search.PropertiesToLoad.Add("objectguid");
                    search.PropertiesToLoad.Add("samaccountname");
                    search.PropertiesToLoad.Add("displayname"); // first name
                    search.PropertiesToLoad.Add("sn"); // last name
                    search.PropertiesToLoad.Add("mail");
                    search.PropertiesToLoad.Add("userAccountControl");
                    search.PropertiesToLoad.Add("memberOf");
                    var result = search.FindOne();
                    var userId = new Guid((byte[])result.Properties["objectguid"][0]).ToString();

                    search.Dispose();

                    // GET GROUPS
                    var propertyCount = result.Properties["memberOf"].Count;
                    string dn;
                    int equalsIndex, commaIndex;
                    var groupNames = new StringBuilder();
                    for (var propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                    {
                        dn = (string)result.Properties["memberOf"][propertyCounter];

                        equalsIndex = dn.IndexOf("=", 1, StringComparison.OrdinalIgnoreCase);
                        commaIndex = dn.IndexOf(",", 1, StringComparison.OrdinalIgnoreCase);
                        if (-1 == equalsIndex)
                        {
                            return null;
                        }

                        if (propertyCounter > 0)
                        {
                            groupNames.Append("][");
                        }

                        groupNames.Append(dn.Substring(equalsIndex + 1, commaIndex - equalsIndex - 1));
                    }

                    var groups = Strings.ToString(groupNames);
                    var AccessKeys = string.Empty;

                    using (var conn = new SqlConnection(SepFunctions.Database_Connection()))
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT UserID FROM Members WHERE UserID='" + userId + "'", conn))
                        {
                            using (SqlDataReader RS = cmd.ExecuteReader())
                            {
                                if (!RS.HasRows)
                                {
                                    var KeyCount = 0;
                                    var isAdmin = false;
                                    var arrGroupNames = Strings.Split(groups, "][");

                                    if (arrGroupNames != null)
                                    {
                                        for (var j = 0; j <= Information.UBound(arrGroupNames); j++)
                                        {
                                            using (var cmd2 = new SqlCommand("SELECT KeyID FROM LDAPGroups WHERE Group_Name='" + arrGroupNames[j] + "'", conn))
                                            {
                                                using (SqlDataReader RS2 = cmd2.ExecuteReader())
                                                {
                                                    if (RS2.HasRows)
                                                    {
                                                        RS2.Read();
                                                        if (SepFunctions.openNull(RS2["KeyID"]) == "2")
                                                        {
                                                            isAdmin = true;
                                                        }

                                                        if (KeyCount > 0)
                                                        {
                                                            AccessKeys += ",";
                                                        }

                                                        AccessKeys += "|" + SepFunctions.openNull(RS2["KeyID"]) + "|";
                                                        KeyCount += 1;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (string.IsNullOrWhiteSpace(AccessKeys))
                                    {
                                        AccessKeys = "|1|";
                                    }

                                    using (var cmd2 = new SqlCommand("INSERT INTO Members (UserID, UserName, FirstName, LastName, EmailAddress, Password, AccessClass, AccessKeys, Status, CreateDate, PortalID, UserPoints, ApproveFriends, ClassChanged) VALUES(@UserID, @UserName, @FirstName, @LastName, @EmailAddress, @Password, @AccessClass, @AccessKeys, @Status, @CreateDate, '0', '0', 'Yes', @ClassChanged)", conn))
                                    {
                                        cmd2.Parameters.AddWithValue("@UserName", Strings.Left(user, 25));
                                        if (result.Properties.Contains("displayname"))
                                        {
                                            cmd2.Parameters.AddWithValue("@FirstName", Strings.Left((string)result.Properties["displayname"][0], 50));
                                        }
                                        else
                                        {
                                            cmd2.Parameters.AddWithValue("@FirstName", string.Empty);
                                        }

                                        if (result.Properties.Contains("sn"))
                                        {
                                            cmd2.Parameters.AddWithValue("@LastName", Strings.Left((string)result.Properties["sn"][0], 50));
                                        }
                                        else
                                        {
                                            cmd2.Parameters.AddWithValue("@LastName", string.Empty);
                                        }

                                        if (result.Properties.Contains("mail"))
                                        {
                                            cmd2.Parameters.AddWithValue("@EmailAddress", Strings.Left((string)result.Properties["mail"][0], 50));
                                        }
                                        else
                                        {
                                            cmd2.Parameters.AddWithValue("@EmailAddress", string.Empty);
                                        }

                                        cmd2.Parameters.AddWithValue("@Password", Strings.Left(SepFunctions.Save_Password(pass), 100));
                                        if (isAdmin)
                                        {
                                            cmd2.Parameters.AddWithValue("@AccessClass", "2");
                                        }
                                        else
                                        {
                                            cmd2.Parameters.AddWithValue("@AccessClass", "4");
                                        }

                                        cmd2.Parameters.AddWithValue("@AccessKeys", AccessKeys);
                                        if (result.Properties.Contains("userAccountControl"))
                                        {
                                            var flags = (int)result.Properties["userAccountControl"][0];
                                            if (Convert.ToBoolean(flags & UF_ACCOUNTDISABLE))
                                            {
                                                cmd2.Parameters.AddWithValue("@Status", 0);
                                            }
                                            else
                                            {
                                                cmd2.Parameters.AddWithValue("@Status", 1);
                                            }
                                        }
                                        else
                                        {
                                            cmd2.Parameters.AddWithValue("@Status", 1);
                                        }

                                        cmd2.Parameters.AddWithValue("@UserID", userId);
                                        cmd2.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                                        cmd2.Parameters.AddWithValue("@ClassChanged", DateTime.Now);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    return "USERID:" + userId + "||" + user + "||" + pass + "||" + AccessKeys + "||" + DateTime.Now;
                }

                return SepFunctions.LangText("Invalid user name / password.");
            }
            catch
            {
                return SepFunctions.LangText("Invalid user name / password.");
            }
        }

        /// <summary>
        /// IDisposable.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposedValue = true;
                }
            }
        }
    }
}