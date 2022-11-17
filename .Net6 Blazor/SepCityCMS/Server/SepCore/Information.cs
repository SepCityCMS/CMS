// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Information.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.SepCore
{
    using System;

    /// <summary>
    /// An information.
    /// </summary>
    public static class Information
    {
        /// <summary>
        /// Query if 'Expression' is date.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns>True if date, false if not.</returns>
        public static bool IsDate(object Expression)
        {
            if (Strings.InStr(Strings.ToString(Expression), "1/1/0001") > 0)
            {
                return false;
            }

            if (Expression != null)
            {
                if (DateTime.TryParseExact(Strings.ToString(Expression), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime d))
                {
                    return true;
                }
                else
                {
                    if (Expression is DateTime)
                    {
                        return true;
                    }
                    if (Expression is string)
                    {
                        return DateTime.TryParse((string)Expression, out DateTime time1);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Query if 'Expression' is database null.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns>True if database null, false if not.</returns>
        public static bool IsDBNull(object Expression)
        {
            if (Expression == DBNull.Value)
            {
                return true;
            }

            IConvertible convertible = Expression as IConvertible;
            return convertible != null ? convertible.GetTypeCode() == TypeCode.DBNull : false;
        }

        /// <summary>
        /// Query if 'Expression' is numeric.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns>True if numeric, false if not.</returns>
        public static bool IsNumeric(object Expression)
        {
            if (double.TryParse(Strings.ToString(Expression), out double myNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Bounds the given array.
        /// </summary>
        /// <param name="Array">The array.</param>
        /// <returns>An int.</returns>
        public static int LBound(Array Array)
        {
            try
            {
                if (Array != null)
                {
                    return Array.GetLowerBound(0);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Bounds the given array.
        /// </summary>
        /// <param name="Array">The array.</param>
        /// <returns>An int.</returns>
        public static int UBound(Array Array)
        {
            try
            {
                if (Array != null)
                {
                    return Array.GetUpperBound(0);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}