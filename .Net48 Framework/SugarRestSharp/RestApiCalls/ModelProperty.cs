// ***********************************************************************
// Assembly         : SugarRestSharp
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="ModelProperty.cs" company="SugarCRM + PocoGen + REST">
//     Copyright (c) SugarCRM + PocoGen + REST. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************-

namespace SugarRestSharp
{
    using System;

    /// <summary>
    /// This class represents ModelProperty class.
    /// </summary>
    internal class ModelProperty
    {
        /// <summary>
        /// Gets or sets the C# property name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property name in JSON.
        /// </summary>
        /// <value>The name of the json.</value>
        public string JSONName { get; set; }

        /// <summary>
        /// Gets or sets property C# object type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether value is numeric or not.
        /// </summary>
        /// <value><c>true</c> if this instance is numeric; otherwise, <c>false</c>.</value>
        public bool IsNumeric
        {
            get
            {
                if (Type == null)
                {
                    return false;
                }

                string typeName = Type.Name.ToLower();
                switch (typeName)
                {
                    case "int32":
                    case "sbyte":
                        return true;

                    case "string":
                    case "datetime":
                        return false;
                }

                return false;
            }
        }
    }
}