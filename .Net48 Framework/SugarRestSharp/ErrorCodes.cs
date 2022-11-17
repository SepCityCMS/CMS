// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ErrorCodes.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    /// <summary>
    /// Represents ErrorCodes class
    /// </summary>
    internal class ErrorCodes
    {
        /// <summary>
        /// Invalid url error code
        /// </summary>
        public static string UrlInvalid = "Url is not valid or not provided.";

        /// <summary>
        /// Invalid username error code
        /// </summary>
        public static string UsernameInvalid = "Username is not valid or not provided.";

        /// <summary>
        /// Invalid password error code
        /// </summary>
        public static string PasswordInvalid = "Password is not valid or not provided.";

        /// <summary>
        /// Invalid entity type error code
        /// </summary>
        public static string ModulenameInvalid = "Generic type T provided is not a valid EntityBase Type. Must be valid SugarCRM model.";

        /// <summary>
        /// Invalid identifier error code
        /// </summary>
        public static string IdInvalid = "Identifier is not valid or not provided.";

        /// <summary>
        /// Invalid entity or entities data error code
        /// </summary>
        public static string DataInvalid = "Entity or entities data object provided is not valid.";

        /// <summary>
        /// Invalid linked field information missing.
        /// </summary>
        public static string LinkedFieldsInfoMissing = "Entity or entities data object provided is not valid.";
    }
}