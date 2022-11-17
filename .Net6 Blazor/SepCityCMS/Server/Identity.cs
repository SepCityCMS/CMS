// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Identity.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server
{
    using SepCore;
    using System;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// The CRC 32 table.
        /// </summary>
        public static long[] CRC32Table = new long[256];

        /// <summary>
        /// / Turn on error trapping / Declare counter variable iBytes, counter variable iBits,
        /// value variables lCrc32 and lTempCrc32.
        /// </summary>
        private static uint static_InitCrc32_LastSeed;

        /// <summary>
        /// Generates a unique identifier.
        /// </summary>
        /// <returns>The unique identifier.</returns>
        public static string Generate_GUID()
        {
            var MyGuid = Guid.NewGuid();
            return Strings.ToString(MyGuid);
        }

        /// <summary>
        /// Generates a CRC 32.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The CRC 32.</returns>
        public static long GenerateCRC32(string str)
        {
            long lCrc32Value = 0;

            lCrc32Value = InitCrc32();
            lCrc32Value = AddCrc32(str, lCrc32Value);
            return GetCrc32(lCrc32Value);
        }

        /// <summary>
        /// Gets an identity.
        /// </summary>
        /// <param name="word">(Optional) The word.</param>
        /// <returns>The identity.</returns>
        public static long GetIdentity(string word = "")
        {
            var returnFunction = string.Empty;
            returnFunction = Strings.ToString(Math.Abs(GenerateCRC32(IdentityJet(word))));
            do
            {
                if (Strings.Len(returnFunction) >= 15)
                {
                    break;
                }

                var random = new Random();
                returnFunction += random.Next(0, Convert.ToInt32(9));
            }
            while (true);

            return Convert.ToInt64(returnFunction.Substring(0, 15));
        }

        /// <summary>
        /// Adds the CRC32.
        /// </summary>
        /// <param name="Item">The item.</param>
        /// <param name="crc32">The CRC 32.</param>
        /// <returns>A long.</returns>
        private static long AddCrc32(string Item, long crc32)
        {
            // ERROR: Not supported in C#: OnErrorStatement
            var bCharValue = 0;
            var iCounter = 0;
            long lIndex = 0;
            long lAccValue = 0;
            long lTableValue = 0;

            for (iCounter = 1; iCounter <= Strings.Len(Item); iCounter++)
            {
                bCharValue = Strings.Asc(Strings.Mid(Item, iCounter, 1));
                lAccValue = crc32 & 0xffffff00;
                lAccValue = lAccValue / 0x100;
                lAccValue = lAccValue & 0xffffff;
                lIndex = crc32 & 0xff;
                lIndex = lIndex ^ bCharValue;
                lTableValue = CRC32Table[lIndex];
                crc32 = lAccValue ^ lTableValue;
            }

            return crc32;
        }

        /// <summary>
        /// Gets CRC 32.
        /// </summary>
        /// <param name="crc32">The CRC 32.</param>
        /// <returns>The CRC 32.</returns>
        private static long GetCrc32(long crc32)
        {
            // ERROR: Not supported in C#: OnErrorStatement
            return crc32 ^ 0xffffffff;
        }

        /// <summary>
        /// Identity jet.
        /// </summary>
        /// <param name="word">(Optional) The word.</param>
        /// <returns>A string.</returns>
        private static string IdentityJet(string word = "")
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                return word;
            }

            return Strings.ToString(Guid.NewGuid());
        }

        /// <summary>
        /// Initializes the CRC 32.
        /// </summary>
        /// <returns>An uint.</returns>
        private static uint InitCrc32()
        {
            // ERROR: Not supported in C#: OnErrorStatement
            var iBytes = 0;
            long lCrc32 = 0;
            long lTempCrc32 = 0;
            var Seed = 0xedb88320;
            var Precondition = 0xffffffff;

            if (static_InitCrc32_LastSeed != Seed)
            {
                //// Iterate 256 times
                for (iBytes = 0; iBytes <= 255; iBytes++)
                {
                    //// Initiate lCrc32 to counter variable
                    lCrc32 = iBytes;

                    //// Now iterate through each bit in counter byte
                    for (var iBits = 0; iBits <= 7; iBits++)
                    {
                        //// Right shift unsigned Integer 1 bit
                        lTempCrc32 = lCrc32 & 0xfffffffe;
                        lTempCrc32 = lTempCrc32 / 0x2;
                        lTempCrc32 = lTempCrc32 & 0x7fffffff;

                        //// Now check if temporary is less than zero and then mix Crc32 checksum with Seed value
                        if ((lCrc32 & 0x1) != 0)
                        {
                            lCrc32 = lTempCrc32 ^ Seed;
                        }
                        else
                        {
                            lCrc32 = lTempCrc32;
                        }
                    }

                    //// Put Crc32 checksum value in the holding array
                    CRC32Table[iBytes] = lCrc32;
                }

                static_InitCrc32_LastSeed = Seed;
            }

            //// After this is done, set function value to the precondition value
            return Precondition;
        }
    }
}