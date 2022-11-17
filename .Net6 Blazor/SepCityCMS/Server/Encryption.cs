// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="Encryption.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Aes decrypt.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="pass">The pass.</param>
        /// <returns>A string.</returns>
        public static string AES_Decrypt(string input, string pass)
        {
            var AES = new RijndaelManaged();
            var Hash_AES = new MD5CryptoServiceProvider();
            var decrypted = string.Empty;

            try
            {
                var hash = new byte[32];
                var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);
                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                var DESDecrypter = AES.CreateDecryptor();
                var Buffer = Convert.FromBase64String(input);
                decrypted = Encoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch
            {
            }

            Hash_AES.Dispose();
            AES.Dispose();

            return decrypted;
        }

        /// <summary>
        /// Aes encrypt.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="pass">The pass.</param>
        /// <returns>A string.</returns>
        public static string AES_Encrypt(string input, string pass)
        {
            var AES = new RijndaelManaged();
            var Hash_AES = new MD5CryptoServiceProvider();
            var encrypted = string.Empty;

            try
            {
                var hash = new byte[32];
                var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);
                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                var DESEncrypter = AES.CreateEncryptor();
                var Buffer = Encoding.ASCII.GetBytes(input);
                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch
            {
            }

            Hash_AES.Dispose();
            AES.Dispose();

            return encrypted;
        }

        /// <summary>
        /// Decrypts.
        /// </summary>
        /// <param name="cipherString">The cipher string.</param>
        /// <param name="forceKey">(Optional) The force key.</param>
        /// <returns>A string.</returns>
        public static string Decrypt(string cipherString, string forceKey = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cipherString))
                {
                    return string.Empty;
                }

                var key = string.Empty;

                if (!string.IsNullOrWhiteSpace(forceKey))
                {
                    key = forceKey;
                }
                else
                {
                    key = GetEncryptionKey();
                }

                byte[] keyArray;
                var toEncryptArray = Convert.FromBase64String(cipherString);

                keyArray = Encoding.UTF8.GetBytes(key);

                var tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                tdes.Dispose();
                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Encrypts.
        /// </summary>
        /// <param name="toEncrypt">to encrypt.</param>
        /// <param name="forceKey">(Optional) The force key.</param>
        /// <returns>A string.</returns>
        public static string Encrypt(string toEncrypt, string forceKey = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toEncrypt))
                {
                    return string.Empty;
                }

                var key = string.Empty;

                if (!string.IsNullOrWhiteSpace(forceKey))
                {
                    key = forceKey;
                }
                else
                {
                    key = GetEncryptionKey();
                }

                byte[] keyArray;
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                keyArray = Encoding.UTF8.GetBytes(key);

                var tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                tdes.Dispose();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Generates a salt.
        /// </summary>
        /// <returns>The salt.</returns>
        public static string GenerateSalt()
        {
            var random = new Random();
            var legalCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            var builder = new StringBuilder();
            var ch = '\0';

            for (var i = 0; i <= 5; i++)
            {
                ch = legalCharacters[random.Next(0, legalCharacters.Length)];
                builder.Append(ch);
            }

            return SepCore.Strings.ToString(builder);
        }

        /// <summary>
        /// Md 5 hash encrypt.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A string.</returns>
        public static string MD5Hash_Encrypt(string input)
        {
            // Create a new instance of the MD5 object.
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (var i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            md5Hasher.Dispose();

            // Return the hexadecimal string.
            return SepCore.Strings.ToString(sBuilder);
        }

        /// <summary>
        /// Saves a password.
        /// </summary>
        /// <param name="userPassword">The user password.</param>
        /// <returns>A string.</returns>
        public static string Save_Password(string userPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(userPassword + "^Y8~JJ", BCrypt.Net.BCrypt.GenerateSalt());
        }

        /// <summary>
        /// Verify password.
        /// </summary>
        /// <param name="databasePassword">The database password.</param>
        /// <param name="userPassword">The user password.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool Verify_Password(string databasePassword, string userPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(userPassword + "^Y8~JJ", databasePassword);
            }
            catch
            {
                return false;
            }
        }
    }
}