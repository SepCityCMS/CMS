// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="FedEx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Integrations.Shipping
{
    using RateWebReference;
    using System;

    /// <summary>
    /// A fed ex.
    /// </summary>
    public class FedEx : IDisposable //-V3073
    {
        /// <summary>
        /// True to enable debug mode, false to disable it.
        /// </summary>
        public bool DebugMode = false;

        /// <summary>
        /// The common.
        /// </summary>
        private readonly string m_accountNumber;

        /// <summary>
        /// Destination city.
        /// </summary>
        private readonly string m_destinationCity;

        /// <summary>
        /// Destination country code.
        /// </summary>
        private readonly string m_destinationCountryCode;

        /// <summary>
        /// State of the destination.
        /// </summary>
        private readonly string m_destinationState;

        /// <summary>
        /// Destination street.
        /// </summary>
        private readonly string m_destinationStreet;

        /// <summary>
        /// Destination zip code.
        /// </summary>
        private readonly string m_destinationZipCode;

        /// <summary>
        /// The meter number.
        /// </summary>
        private readonly string m_meterNumber;

        /// <summary>
        /// The service key.
        /// </summary>
        private readonly string m_serviceKey;

        /// <summary>
        /// The service password.
        /// </summary>
        private readonly string m_servicePassword;

        /// <summary>
        /// The shipper city.
        /// </summary>
        private readonly string m_shipperCity;

        /// <summary>
        /// The shipper country code.
        /// </summary>
        private readonly string m_shipperCountryCode;

        /// <summary>
        /// State of the shipper.
        /// </summary>
        private readonly string m_shipperState;

        /// <summary>
        /// The shipper street.
        /// </summary>
        private readonly string m_shipperStreet;

        /// <summary>
        /// The shipper zip code.
        /// </summary>
        private readonly string m_shipperZipCode;

        /// <summary>
        /// The weight.
        /// </summary>
        private string m_weight;

        /// <summary>
        /// Type of the weight.
        /// </summary>
        private string m_weightType;

        /// <summary>
        /// The total shipping.
        /// </summary>
        private decimal totalShipping;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="meterNumber">The meter number.</param>
        /// <param name="serviceKey">The service key.</param>
        /// <param name="servicePassword">The service password.</param>
        /// <param name="shipperStreet">The shipper street.</param>
        /// <param name="shipperCity">The shipper city.</param>
        /// <param name="shipperState">State of the shipper.</param>
        /// <param name="shipperZipCode">The shipper zip code.</param>
        /// <param name="shipperCountryCode">The shipper country code.</param>
        /// <param name="destinationStreet">Destination street.</param>
        /// <param name="destinationCity">Destination city.</param>
        /// <param name="destinationState">State of the destination.</param>
        /// <param name="destinationZipCode">Destination zip code.</param>
        /// <param name="destinationCountryCode">Destination country code.</param>
        public FedEx(string accountNumber, string meterNumber, string serviceKey, string servicePassword, string shipperStreet, string shipperCity, string shipperState, string shipperZipCode, string shipperCountryCode, string destinationStreet, string destinationCity, string destinationState, string destinationZipCode, string destinationCountryCode)
        {
            m_accountNumber = accountNumber;
            m_meterNumber = meterNumber;
            m_serviceKey = serviceKey;
            m_servicePassword = servicePassword;

            m_shipperStreet = shipperStreet;
            m_shipperCity = shipperCity;
            m_shipperState = shipperState;
            m_shipperZipCode = shipperZipCode;
            m_shipperCountryCode = shipperCountryCode;
            m_destinationStreet = destinationStreet;
            m_destinationCity = destinationCity;
            m_destinationState = destinationState;
            m_destinationZipCode = destinationZipCode;
            m_destinationCountryCode = destinationCountryCode;
        }

        /// <summary>
        /// Creates rate request.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns>The new rate request.</returns>
        public RateRequest CreateRateRequest(string weight, string weightType)
        {
            // Build a RateRequest
            var request = new RateRequest();

            request.WebAuthenticationDetail = SetWebAuthenticationDetail();

            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = m_accountNumber;
            request.ClientDetail.MeterNumber = m_meterNumber;

            // Replace "XXX" with client's meter number

            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "*** Rate Request using VB.NET ***";

            // This is a reference field for the customer. Any value can be used and will be provided
            // in the response.

            request.Version = new VersionId();

            // WSDL version information, value is automatically set from wsdl

            request.ReturnTransitAndCommit = true;
            request.ReturnTransitAndCommitSpecified = true;

            SetShipmentDetails(weight, weightType, ref request);

            return request;
        }

        /// <summary>
        /// This code added by Visual Basic to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a rate.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns>The rate.</returns>
        public decimal GetRate(string weight, string weightType)
        {
            m_weight = weight;
            m_weightType = weightType;

            var request = CreateRateRequest(m_weight, m_weightType);
            // TODO
            //var service = new RateService();

            //// Initialize the service

            //service.Url = "https://ws.fedex.com:443/web-services/rate";

            //try
            //{
            //    // Call the web service passing in a RateRequest and returning a RateReply
            //    var reply = service.getRates(request);

            //    // check if the call was successful
            //    if (!(reply.HighestSeverity == NotificationSeverityType.ERROR) && !(reply.HighestSeverity == NotificationSeverityType.FAILURE))
            //    {
            //        ShowRateReply(ref reply);
            //    }
            //    else
            //    {
            //        ShowNotifications(ref reply);
            //    }
            //}
            //catch (Exception e)
            //{
            //    if (DebugMode)
            //    {
            //        Server.SepFunctions.Debug_Log("FedEx Error: " + e.Message);
            //    }
            //}

            //service.Dispose();

            return totalShipping;
        }

        /// <summary>
        /// Sets a destination.
        /// </summary>
        /// <param name="request">[in,out] The request.</param>
        public void SetDestination(ref RateRequest request)
        {
            request.RequestedShipment.Recipient = new Party();
            request.RequestedShipment.Recipient.Address = new Address();
            request.RequestedShipment.Recipient.Address.StreetLines = new string[1] { m_destinationStreet };
            request.RequestedShipment.Recipient.Address.City = m_destinationCity;
            request.RequestedShipment.Recipient.Address.StateOrProvinceCode = m_destinationState;
            request.RequestedShipment.Recipient.Address.PostalCode = m_destinationZipCode;
            request.RequestedShipment.Recipient.Address.CountryCode = m_destinationCountryCode;

            if (DebugMode)
            {
                Server.SepFunctions.Debug_Log("m_destinationStreet: " + m_destinationStreet);
                Server.SepFunctions.Debug_Log("m_destinationCity: " + m_destinationCity);
                Server.SepFunctions.Debug_Log("m_destinationState: " + m_destinationState);
                Server.SepFunctions.Debug_Log("m_destinationZipCode: " + m_destinationZipCode);
                Server.SepFunctions.Debug_Log("m_destinationCountryCode: " + m_destinationCountryCode);
            }
        }

        /// <summary>
        /// Sets an origin.
        /// </summary>
        /// <param name="request">[in,out] The request.</param>
        public void SetOrigin(ref RateRequest request)
        {
            request.RequestedShipment.Shipper = new Party();
            request.RequestedShipment.Shipper.Address = new Address();
            request.RequestedShipment.Shipper.Address.StreetLines = new string[1] { m_shipperStreet };
            request.RequestedShipment.Shipper.Address.City = m_shipperCity;
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = m_shipperState;
            request.RequestedShipment.Shipper.Address.PostalCode = m_shipperZipCode;
            request.RequestedShipment.Shipper.Address.CountryCode = m_shipperCountryCode;

            if (DebugMode)
            {
                Server.SepFunctions.Debug_Log("m_shipperStreet: " + m_shipperStreet);
                Server.SepFunctions.Debug_Log("m_shipperCity: " + m_shipperCity);
                Server.SepFunctions.Debug_Log("m_shipperState: " + m_shipperState);
                Server.SepFunctions.Debug_Log("m_shipperZipCode: " + m_shipperZipCode);
                Server.SepFunctions.Debug_Log("m_shipperCountryCode: " + m_shipperCountryCode);
            }
        }

        /// <summary>
        /// Sets package line items.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <param name="request">[in,out] The request.</param>
        public void SetPackageLineItems(string weight, string weightType, ref RateRequest request)
        {
            request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[2];
            request.RequestedShipment.RequestedPackageLineItems[0] = new RequestedPackageLineItem();
            request.RequestedShipment.RequestedPackageLineItems[0].SequenceNumber = "1";

            // package sequence number
            request.RequestedShipment.RequestedPackageLineItems[0].GroupPackageCount = "1";

            // Package weight information
            request.RequestedShipment.RequestedPackageLineItems[0].Weight = new Weight();
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.Units = WeightUnits.LB;
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.UnitsSpecified = true;
            if (Server.SepFunctions.toDecimal(weight) > 0 && weightType != "LBS")
            {
                request.RequestedShipment.RequestedPackageLineItems[0].Weight.Value = Server.SepFunctions.toDecimal(weight) / 16;
            }
            else
            {
                request.RequestedShipment.RequestedPackageLineItems[0].Weight.Value = Server.SepFunctions.toDecimal(weight);
            }

            request.RequestedShipment.RequestedPackageLineItems[0].Weight.ValueSpecified = true;

            // package dimensions
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions = new Dimensions();
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Length = "10";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Width = "13";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Height = "4";
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Units = LinearUnits.IN;
            request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.UnitsSpecified = true;

            // insured value
            request.RequestedShipment.RequestedPackageLineItems[0].InsuredValue = new Money();
            request.RequestedShipment.RequestedPackageLineItems[0].InsuredValue.Amount = 100;
            request.RequestedShipment.RequestedPackageLineItems[0].InsuredValue.Currency = "USD";
        }

        /// <summary>
        /// Sets shipment details.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <param name="request">[in,out] The request.</param>
        public void SetShipmentDetails(string weight, string weightType, ref RateRequest request)
        {
            request.RequestedShipment = new RequestedShipment();
            request.RequestedShipment.ShipTimestamp = DateTime.Now;

            // Ship date and time
            request.RequestedShipment.ShipTimestampSpecified = true;
            request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP;

            //Drop off types are BUSINESS_SERVICE_CENTER, DROP_BOX, REGULAR_PICKUP, REQUEST_COURIER, STATION
            request.RequestedShipment.ServiceType = ServiceType.INTERNATIONAL_PRIORITY;

            // Service types are STANDARD_OVERNIGHT, PRIORITY_OVERNIGHT, FEDEX_GROUND ...
            request.RequestedShipment.ServiceTypeSpecified = true;
            request.RequestedShipment.PackagingType = PackagingType.YOUR_PACKAGING;

            // Packaging type FEDEX_BOK, FEDEX_PAK, FEDEX_TUBE, YOUR_PACKAGING, ...
            request.RequestedShipment.PackagingTypeSpecified = true;

            SetOrigin(ref request);

            SetDestination(ref request);

            SetPackageLineItems(weight, weightType, ref request);

            request.RequestedShipment.TotalInsuredValue = new Money();
            request.RequestedShipment.TotalInsuredValue.Amount = 100;
            request.RequestedShipment.TotalInsuredValue.Currency = "USD";

            request.RequestedShipment.PackageCount = "2";
        }

        /// <summary>
        /// Sets web authentication detail.
        /// </summary>
        /// <returns>A WebAuthenticationDetail.</returns>
        public WebAuthenticationDetail SetWebAuthenticationDetail()
        {
            var wad = new WebAuthenticationDetail();

            wad.UserCredential = new WebAuthenticationCredential();
            wad.ParentCredential = new WebAuthenticationCredential();
            wad.UserCredential.Key = m_serviceKey;
            wad.UserCredential.Password = m_servicePassword;
            wad.ParentCredential.Key = m_serviceKey;
            wad.ParentCredential.Password = m_servicePassword;

            return wad;
        }

        /// <summary>
        /// Shows the delivery details.
        /// </summary>
        /// <param name="rateReplyDetail">[in,out] The rate reply detail.</param>
        public void ShowDeliveryDetails(ref RateReplyDetail rateReplyDetail)
        {
            if (rateReplyDetail.DeliveryTimestampSpecified)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Delivery timestamp: " + rateReplyDetail.DeliveryTimestamp);
                }
            }

            if (rateReplyDetail.TransitTimeSpecified)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Transit time: " + rateReplyDetail.TransitTime);
                }
            }
        }

        /// <summary>
        /// Shows the notifications.
        /// </summary>
        /// <param name="reply">[in,out] The reply.</param>
        public void ShowNotifications(ref RateReply reply)
        {
            if (DebugMode)
            {
                Server.SepFunctions.Debug_Log("Notifications");
            }

            for (var i = 0; i <= reply.Notifications.Length - 1; i++)
            {
                var notification = reply.Notifications[i];
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Notification no. " + i);
                    Server.SepFunctions.Debug_Log(" Severity: " + notification.Severity);
                    Server.SepFunctions.Debug_Log(" Code: " + notification.Code);
                    Server.SepFunctions.Debug_Log(" Message: " + notification.Message);
                    Server.SepFunctions.Debug_Log(" Source: " + notification.Source);
                }
            }
        }

        /// <summary>
        /// Shows the rate reply.
        /// </summary>
        /// <param name="reply">[in,out] The reply.</param>
        public void ShowRateReply(ref RateReply reply)
        {
            if (reply.RateReplyDetails == null)
            {
                return;
            }

            if (DebugMode)
            {
                Server.SepFunctions.Debug_Log("RateReply details:");
            }

            for (var i = 0; i <= reply.RateReplyDetails.Length - 1; i++)
            {
                var rateReplyDetail = reply.RateReplyDetails[i];
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Rate Reply Detail for Service " + (i + 1));
                }

                if (rateReplyDetail.ServiceTypeSpecified)
                {
                    if (DebugMode)
                    {
                        Server.SepFunctions.Debug_Log("Service Type: " + rateReplyDetail.ServiceType);
                    }
                }

                if (rateReplyDetail.PackagingTypeSpecified)
                {
                    if (DebugMode)
                    {
                        Server.SepFunctions.Debug_Log("Packaging Type: " + rateReplyDetail.PackagingType);
                    }
                }

                if (rateReplyDetail.RatedShipmentDetails != null)
                {
                    for (var j = 0; j <= rateReplyDetail.RatedShipmentDetails.Length - 1; j++)
                    {
                        var shipmentDetail = rateReplyDetail.RatedShipmentDetails[j];
                        if (DebugMode)
                        {
                            Server.SepFunctions.Debug_Log("---Rated Shipment Detail for Rate Type " + (j + 1) + "---");
                        }

                        ShowShipmentRateDetails(ref shipmentDetail);
                    }
                }

                ShowDeliveryDetails(ref rateReplyDetail);
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("**********************************************************");
                }
            }
        }

        /// <summary>
        /// Shows the shipment rate details.
        /// </summary>
        /// <param name="shipmentDetail">[in,out] The shipment detail.</param>
        public void ShowShipmentRateDetails(ref RatedShipmentDetail shipmentDetail)
        {
            if (shipmentDetail == null)
            {
                return;
            }

            if (shipmentDetail.ShipmentRateDetail == null)
            {
                return;
            }

            var rateDetail = shipmentDetail.ShipmentRateDetail;
            if (DebugMode)
            {
                Server.SepFunctions.Debug_Log("--- Shipment Rate Detail ---");
                Server.SepFunctions.Debug_Log("RateType: " + rateDetail.RateType);
            }

            if (rateDetail.TotalBillingWeight != null)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Total Billing Weight: " + rateDetail.TotalBillingWeight.Value + " " + rateDetail.TotalBillingWeight.Units);
                }
            }

            if (rateDetail.TotalBaseCharge != null)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Total Base Charge: " + rateDetail.TotalBaseCharge.Amount + " " + rateDetail.TotalBaseCharge.Currency);
                }
            }

            if (rateDetail.TotalFreightDiscounts != null)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Total Freight Discounts: " + rateDetail.TotalFreightDiscounts.Amount + " " + rateDetail.TotalFreightDiscounts.Currency);
                }
            }

            if (rateDetail.TotalSurcharges != null)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Total Surcharges: " + rateDetail.TotalSurcharges.Amount + " " + rateDetail.TotalSurcharges.Currency);
                }
            }

            if (rateDetail.Surcharges != null)
            {
                foreach (var surcharge in rateDetail.Surcharges)
                {
                    if (DebugMode)
                    {
                        Server.SepFunctions.Debug_Log(" " + surcharge.SurchargeType + " surcharge " + surcharge.Amount.Amount + " " + surcharge.Amount.Currency);
                    }
                }
            }

            if (rateDetail.TotalNetCharge != null)
            {
                if (DebugMode)
                {
                    Server.SepFunctions.Debug_Log("Total Net Charge: " + rateDetail.TotalNetCharge.Amount + " " + rateDetail.TotalNetCharge.Currency);
                }

                totalShipping += rateDetail.TotalNetCharge.Amount;
            }
        }

        /// <summary>
        /// IDisposable.
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