// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="RequestType.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    /// <summary>
    /// Represents RequestType enum class
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// SugarCRM get by id method call
        /// </summary>
        ReadById,

        /// <summary>
        /// SugarCRM get all method call.
        /// </summary>
        BulkRead,

        /// <summary>
        /// SugarCRM get paged method call.
        /// </summary>
        PagedRead,

        /// <summary>
        /// SugarCRM create method call.
        /// </summary>
        Create,

        /// <summary>
        /// SugarCRM bulk create method call.
        /// </summary>
        BulkCreate,

        /// <summary>
        /// SugarCRM update method call.
        /// </summary>
        Update,

        /// <summary>
        /// SugarCRM bulk update method call.
        /// </summary>
        BulkUpdate,

        /// <summary>
        /// SugarCRM delete method call.
        /// </summary>
        Delete,

        /// <summary>
        /// SugarCRM get by id method call - this gets associated linked objects serialized into a known custom type.
        /// </summary>
        LinkedReadById,

        /// <summary>
        /// SugarCRM get all method call - this gets associated linked objects serialized into a known custom type.
        /// </summary>
        LinkedBulkRead
    }
}