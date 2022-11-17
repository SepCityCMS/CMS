using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SepCommon
{
    public static class PayPal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="referenceId"></param>
        /// <param name="itemTotal"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static Task<HttpResponse> CreateOrder(string toUserId, string referenceId, string itemTotal, string itemName)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(BuildRequestBody(toUserId, referenceId, itemTotal, itemName));
            var response = PayPalClient.client().Execute(request);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="referenceId"></param>
        /// <param name="itemTotal">99.99</param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        private static OrderRequest BuildRequestBody(string toUserId, string referenceId, string itemTotal, string itemName)
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",

                ApplicationContext = new ApplicationContext
                {
                    BrandName = SepFunctions.Setup(991, "CompanyName"),
                    LandingPage = "BILLING",
                    UserAction = "CONTINUE",
                    ShippingPreference = "SET_PROVIDED_ADDRESS"
                },
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                  new PurchaseUnitRequest{
                    ReferenceId =  referenceId,
                    CustomId = toUserId,
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                      CurrencyCode = "USD",
                      Value = itemTotal,
                      AmountBreakdown = new AmountBreakdown
                      {
                        ItemTotal = new Money
                        {
                          CurrencyCode = "USD",
                          Value = itemTotal
                        }
                      }
                    },
                    Items = new List<Item>
                    {
                      new Item
                      {
                        Name = itemName,
                        UnitAmount = new Money
                        {
                          CurrencyCode = "USD",
                          Value = itemTotal
                        },
                        Quantity = "1",
                        Category = "PHYSICAL_GOODS"
                      }
                    },
                    ShippingDetail = new ShippingDetail
                    {
                      Name = new Name
                      {
                        FullName = SepFunctions.GetUserInformation("FirstName", toUserId) + " " + SepFunctions.GetUserInformation("LastName", toUserId)
                      },
                      AddressPortable = new AddressPortable
                      {
                        AddressLine1 = SepFunctions.GetUserInformation("StreetAddress", toUserId),
                        AdminArea2 = SepFunctions.GetUserInformation("City", toUserId),
                        AdminArea1 = SepFunctions.GetUserInformation("State", toUserId),
                        PostalCode = SepFunctions.GetUserInformation("ZipCode", toUserId),
                        CountryCode = SepCore.Strings.UCase(SepFunctions.GetUserInformation("Country", toUserId))
                      }
                    }
                  }
                }
            };

            return orderRequest;
        }
    }

    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment(SepFunctions.Setup(989, "PayPalClientID"), SepFunctions.Decrypt(SepFunctions.Setup(989, "PayPalSecret")));
        }

        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}
