// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Twilio.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon.Core
{
    using global::Twilio;
    using global::Twilio.Rest.Api.V2010.Account;
    using global::Twilio.Types;

    /// <summary>
    /// The twilio.
    /// </summary>
    public class TwilioGlobal
    {
        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="toPhone">to phone.</param>
        /// <param name="msgBody">The message body.</param>
        public void Send_SMS(string toPhone, string msgBody)
        {
            try
            {
                TwilioClient.Init(SepFunctions.Setup(989, "TwilioAccountSID"), SepFunctions.Setup(989, "TwilioAuthToken"));

                MessageResource.Create(new PhoneNumber(SepFunctions.FormatPhone(toPhone)), from: new PhoneNumber(SepFunctions.FormatPhone(SepFunctions.Setup(989, "TwilioPhoneNumber"))), body: msgBody);
            }
            catch
            {
                // Do not send SMS
            }
        }

    }
}