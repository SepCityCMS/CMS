// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ModulePropertyAttribute.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using System;

    /// <summary>
    /// SugarCRM module attributes [ModuleName - name of module, Tablename - name of associated table]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class ModulePropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets module name
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets table name
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }
    }
}