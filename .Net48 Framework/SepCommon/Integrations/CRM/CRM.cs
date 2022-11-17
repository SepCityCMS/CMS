// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="CRM.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System;
    using System.Xml;

    /// <summary>
    /// Class CRM.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class CRM : IDisposable
    {
        /// <summary>
        /// The module identifier
        /// </summary>
        private const int ModuleID = 67;

        // To detect redundant calls
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Enum SoftwareEnabled
        /// </summary>
        public enum SoftwareEnabled
        {
            /// <summary>
            /// The disabled
            /// </summary>
            Disabled = 0,

            /// <summary>
            /// The smarter track
            /// </summary>
            SmarterTrack = 1,

            /// <summary>
            /// The sugar CRM
            /// </summary>
            SugarCRM = 2,

            /// <summary>
            /// The suite CRM
            /// </summary>
            SuiteCRM = 3,

            /// <summary>
            /// The WHMCS
            /// </summary>
            WHMCS = 4
        }

        /// <summary>
        /// Enum WhenToWriteUser
        /// </summary>
        public enum WhenToWriteUser
        {
            /// <summary>
            /// The disabled
            /// </summary>
            Disabled = 0,

            /// <summary>
            /// Creates new signup.
            /// </summary>
            NewSignup = 1,

            /// <summary>
            /// Creates new order.
            /// </summary>
            NewOrder = 2,

            /// <summary>
            /// Creates new ticket.
            /// </summary>
            NewTicket = 3
        }

        /// <summary>
        /// Creates the ticket.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="email">The email.</param>
        public void Create_Ticket(long departmentId, string subject, string body, string email)
        {
            switch (Software_Enabled())
            {
                case SoftwareEnabled.SmarterTrack:
                    ST_Create_Ticket(departmentId, subject, body, email);
                    break;

                case SoftwareEnabled.SugarCRM:
                    Sugar_Create_Ticket(subject, body);
                    break;

                case SoftwareEnabled.SuiteCRM:
                    Suite_Create_Ticket(subject, body);
                    break;

                case SoftwareEnabled.WHMCS:
                    WHMCS_Create_Ticket(subject, body);
                    break;

                case SoftwareEnabled.Disabled:
                    break;
            }
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="WhenToWrite">The when to write.</param>
        public void Create_User(WhenToWriteUser WhenToWrite)
        {
            switch (WhenToWrite)
            {
                case WhenToWriteUser.NewSignup:
                    switch (Software_Enabled())
                    {
                        case SoftwareEnabled.SmarterTrack:
                            if (SepFunctions.Setup(67, "STDatabase") == "NewSignup")
                            {
                                ST_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SugarCRM:
                            if (SepFunctions.Setup(67, "SugarCRMDatabase") == "NewSignup")
                            {
                                Sugar_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SuiteCRM:
                            if (SepFunctions.Setup(67, "SuiteCRMDatabase") == "NewSignup")
                            {
                                Suite_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.WHMCS:
                            if (SepFunctions.Setup(67, "WHMCSDatabase") == "NewSignup")
                            {
                                WHMCS_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.Disabled:

                            // Do Nothing (CRM Disabled)
                            break;
                    }

                    break;

                case WhenToWriteUser.NewOrder:
                    switch (Software_Enabled())
                    {
                        case SoftwareEnabled.SmarterTrack:
                            if (SepFunctions.Setup(67, "STDatabase") == "NewOrder")
                            {
                                ST_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SugarCRM:
                            if (SepFunctions.Setup(67, "SugarCRMDatabase") == "NewOrder")
                            {
                                Sugar_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SuiteCRM:
                            if (SepFunctions.Setup(67, "SuiteCRMDatabase") == "NewOrder")
                            {
                                Suite_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.WHMCS:
                            if (SepFunctions.Setup(67, "WHMCSDatabase") == "NewOrder")
                            {
                                WHMCS_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.Disabled:

                            // Do Nothing (CRM Disabled)
                            break;
                    }

                    break;

                case WhenToWriteUser.NewTicket:
                    switch (Software_Enabled())
                    {
                        case SoftwareEnabled.SmarterTrack:
                            if (SepFunctions.Setup(67, "STDatabase") == "NewTicket")
                            {
                                ST_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SugarCRM:
                            if (SepFunctions.Setup(67, "SugarCRMDatabase") == "NewTicket")
                            {
                                Sugar_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.SuiteCRM:
                            if (SepFunctions.Setup(67, "SuiteCRMDatabase") == "NewTicket")
                            {
                                Suite_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.WHMCS:
                            if (SepFunctions.Setup(67, "WHMCSDatabase") == "NewTicket")
                            {
                                WHMCS_Create_Account();
                            }

                            break;

                        case SoftwareEnabled.Disabled:

                            // Do Nothing (CRM Disabled)
                            break;
                    }

                    break;

                case WhenToWriteUser.Disabled:

                    // Do Nothing (CRM Disabled)
                    break;
            }
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList Get_Ticket(string ticketId)
        {
            switch (Software_Enabled())
            {
                case SoftwareEnabled.SmarterTrack:
                    return ST_Get_Ticket(ticketId);

                case SoftwareEnabled.SugarCRM:
                    return Sugar_Get_Ticket(ticketId);

                case SoftwareEnabled.SuiteCRM:
                    return Suite_Get_Ticket(ticketId);

                case SoftwareEnabled.WHMCS:
                    return WHMCS_Get_Ticket(ticketId);
            }

            return null;
        }

        /// <summary>
        /// Lists the tickets.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList List_Tickets()
        {
            switch (Software_Enabled())
            {
                case SoftwareEnabled.SmarterTrack:
                    return ST_List_Tickets();

                case SoftwareEnabled.SugarCRM:
                    return Sugar_List_Tickets();

                case SoftwareEnabled.SuiteCRM:
                    return Suite_List_Tickets();

                case SoftwareEnabled.WHMCS:
                    return WHMCS_List_Tickets();
            }

            return null;
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <returns>SoftwareEnabled.</returns>
        public SoftwareEnabled Software_Enabled()
        {
            switch (SepFunctions.Setup(67, "CRMVersion"))
            {
                case "SmarterTrack":
                    return SoftwareEnabled.SmarterTrack;

                case "SugarCRM":
                    return SoftwareEnabled.SugarCRM;

                case "SuiteCRM":
                    return SoftwareEnabled.SuiteCRM;

                case "WHMCS":
                    return SoftwareEnabled.WHMCS;

                default:
                    return SoftwareEnabled.Disabled;
            }
        }

        // IDisposable
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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