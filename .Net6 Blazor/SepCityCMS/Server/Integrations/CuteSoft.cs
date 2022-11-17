// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CuteSoft.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Integrations
{
    using CuteChat;

    /// <summary>
    /// A chat provider.
    /// </summary>
    public class ChatProvider : CuteChat.ChatProvider // -V3072
    {
        // Types that own disposable fields should be disposable
        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
        }

        /// <summary>
        /// Searches for the first user login name.
        /// </summary>
        /// <param name="nickName">Name of the nick.</param>
        /// <returns>The found user login name.</returns>
        public override string FindUserLoginName(string nickName)
        {
            return Server.SepFunctions.Session_User_Name();
        }

        /// <summary>
        /// Gets connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        public override string GetConnectionString()
        {
            return Server.SepFunctions.Database_Connection();
        }

        /// <summary>
        /// Gets logon identity.
        /// </summary>
        /// <returns>The logon identity.</returns>
        public override AppChatIdentity GetLogonIdentity()
        {
            var loginname = Server.SepFunctions.Session_User_Name();
            var nickname = Server.SepFunctions.Session_User_Name();
            return new AppChatIdentity(nickname, false, ToUserId(loginname), SepCore.Request.UserHostAddress());
        }

        /// <summary>
        /// Gets user information.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="nickName">[in,out] Name of the nick.</param>
        /// <param name="isAdmin">[in,out] True if is admin, false if not.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public override bool GetUserInfo(string loginName, ref string nickName, ref bool isAdmin)
        {
            nickName = Server.SepFunctions.Session_User_Name();

            if (Server.SepFunctions.CompareKeys("|2|"))
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }

            if (!string.IsNullOrWhiteSpace(nickName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public override bool ValidateUser(string loginName, string password)
        {
            if (!string.IsNullOrWhiteSpace(Server.SepFunctions.Session_User_Name()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The common.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }
    }
}