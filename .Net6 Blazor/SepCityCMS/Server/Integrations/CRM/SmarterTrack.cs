// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="SmarterTrack.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.Integrations.CRM
{
    using SepCityCMS.Server;
    using SepCore;
    using System;
    using System.Xml;

    /// <summary>
    /// Class CRM.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial class CRM
    {
        /// <summary>
        /// Sts the API URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ST_API_URL()
        {
            var sURL = SepFunctions.Format_URL(SepFunctions.Setup(ModuleID, "STURL"));
            if (Strings.Right(sURL, 1) != "/")
            {
                sURL += "/";
            }

            return sURL;
        }

        /// <summary>
        /// Sts the create account.
        /// </summary>
        /// <returns>XmlDocument.</returns>
        public XmlDocument ST_Create_Account()
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <CreateUser xmlns=\"http://www.smartertools.com/SmarterTrack/Services2/svcOrganization.asmx\">";
                sSOAP += "      <authUserName>" + STUser + "</authUserName>";
                sSOAP += "      <authPassword>" + STPass + "</authPassword>";
                sSOAP += "      <username>string</username>";
                sSOAP += "      <password>string</password>";
                sSOAP += "      <email>string</email>";
                sSOAP += "      <isEmailVerified>true</isEmailVerified>";
                sSOAP += "      <displayName>string</displayName>";
                sSOAP += "    </CreateUser>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcOrganization.asmx?op=CreateUser", sSOAP);

                var Doc = new XmlDocument();
                Doc.LoadXml(XMLString);
                return Doc;
            }

            return null;
        }

        /// <summary>
        /// Sts the create ticket.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="fromEmail">From email.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_Create_Ticket(long departmentId, string subject, string body, string fromEmail)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <CreateTicket xmlns=\"http://www.smartertools.com/SmarterTrack/Services2/svcTickets.asmx\">";
                sSOAP += "      <authUserName>" + STUser + "</authUserName>";
                sSOAP += "      <authPassword>" + STPass + "</authPassword>";
                sSOAP += "      <departmentID>" + departmentId + "</departmentID>";
                sSOAP += "      <groupId>0</groupId>";
                sSOAP += "      <userIdOfAgent>0</userIdOfAgent>";
                sSOAP += "      <toAddress>" + fromEmail + "</toAddress>";
                sSOAP += "      <subject>" + subject + "</subject>";
                sSOAP += "      <body>" + body + "</body>";
                sSOAP += "      <isHtml>true</isHtml>";
                sSOAP += "      <setWaiting>true</setWaiting>";
                sSOAP += "      <sendEmail>true</sendEmail>";
                sSOAP += "    </CreateTicket>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcTickets.asmx?op=CreateTicket", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetAllDepartmentsResponse/ab:GetAllDepartmentsResult/ab:Departments/ab:DepartmentInfo";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/SmarterTrack/Services2/svcOrganization.asmx");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the get departments.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_Get_Departments()
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <GetAllDepartments xmlns=\"http://www.smartertools.com/SmarterTrack/Services2/svcOrganization.asmx\">";
                sSOAP += "      <authUserName>" + STUser + "</authUserName>";
                sSOAP += "      <authPassword>" + STPass + "</authPassword>";
                sSOAP += "    </GetAllDepartments>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcOrganization.asmx?op=GetAllDepartments", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetAllDepartmentsResponse/ab:GetAllDepartmentsResult/ab:Departments/ab:DepartmentInfo";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/SmarterTrack/Services2/svcOrganization.asmx");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the get ticket.
        /// </summary>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_Get_Ticket(string ticketNumber)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <GetTicketProperties xmlns=\"http://www.smartertools.com/SmarterTrack/Services2/svcTickets.asmx\">";
                sSOAP += "      <authUserName>" + STUser + "</authUserName>";
                sSOAP += "      <authPassword>" + STPass + "</authPassword>";
                sSOAP += "      <ticketNumber>" + ticketNumber + "</ticketNumber>";
                sSOAP += "      <requestedValues>";
                sSOAP += "         <string>Subject</string>";
                sSOAP += "         <string>BodyHTML</string>";
                sSOAP += "         <string>DateReceivedUTC</string>";
                sSOAP += "         <string>DepartmentID</string>";
                sSOAP += "         <string>TicketStatusID</string>";
                sSOAP += "      </requestedValues>";
                sSOAP += "    </GetTicketProperties>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcTickets.asmx?op=GetTicketProperties", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetTicketPropertiesResponse/ab:GetTicketPropertiesResult/ab:RequestResults/ab:string";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/SmarterTrack/Services2/svcTickets.asmx");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the knowledge base folders.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_KnowledgeBase_Folders(long parentId)
        {
            var STUser = SepFunctions.Setup(67, "STUser");
            var STPass = SepFunctions.Decrypt(SepFunctions.Setup(67, "STPass"));

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;
                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <GetFolders xmlns=\"http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB\">";
                sSOAP += "      <adminUsername>" + STUser + "</adminUsername>";
                sSOAP += "      <adminPassword>" + STPass + "</adminPassword>";
                sSOAP += "      <parentFolderID>" + parentId + "</parentFolderID>";
                sSOAP += "    </GetFolders>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcKB.asmx?op=GetFolders", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetFoldersResponse/ab:GetFoldersResult/ab:Folders/ab:KBFolderResultPart";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the knowledge base list articles.
        /// </summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_KnowledgeBase_List_Articles(long folderId)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <GetArticles xmlns=\"http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB\">";
                sSOAP += "      <adminUsername>" + STUser + "</adminUsername>";
                sSOAP += "      <adminPassword>" + STPass + "</adminPassword>";
                sSOAP += "      <folderID>" + folderId + "</folderID>";
                sSOAP += "      <includePrivate>false</includePrivate>";
                sSOAP += "    </GetArticles>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcKB.asmx?op=GetArticles", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetArticlesResponse/ab:GetArticlesResult/ab:Articles/ab:KBArticlesResultPart";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the knowledge base search.
        /// </summary>
        /// <param name="Keywords">The keywords.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_KnowledgeBase_Search(string Keywords)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <SearchKBArticles xmlns=\"http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB\">";
                sSOAP += "      <adminUsername>" + STUser + "</adminUsername>";
                sSOAP += "      <adminPassword>" + STPass + "</adminPassword>";
                sSOAP += "      <searchTerms>" + Keywords + "</searchTerms>";
                sSOAP += "      <maxResults>20</maxResults>";
                sSOAP += "      <includePrivate>false</includePrivate>";
                sSOAP += "    </SearchKBArticles>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcKB.asmx?op=SearchKBArticles", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:SearchKBArticlesResponse/ab:SearchKBArticlesResult/ab:Articles/ab:KBArticlesResultPart";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the knowledge base view item.
        /// </summary>
        /// <param name="ItemId">The item identifier.</param>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_KnowledgeBase_View_Item(long ItemId)
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">";
                sSOAP += "  <soap:Body>";
                sSOAP += "    <GetArticle xmlns=\"http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB\">";
                sSOAP += "      <adminUsername>" + STUser + "</adminUsername>";
                sSOAP += "      <adminPassword>" + STPass + "</adminPassword>";
                sSOAP += "      <articleID>" + ItemId + "</articleID>";
                sSOAP += "    </GetArticle>";
                sSOAP += "  </soap:Body>";
                sSOAP += "</soap:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcKB.asmx?op=GetArticle", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetArticleResponse/ab:GetArticleResult";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/Products/SmarterTrack/Services2/svcKB");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the list tickets.
        /// </summary>
        /// <returns>XmlNodeList.</returns>
        public XmlNodeList ST_List_Tickets()
        {
            var STUser = string.Empty;
            var STPass = string.Empty;

            var isLoaded = ST_Load_Config(ref STUser, ref STPass);

            if (isLoaded)
            {
                var sSOAP = string.Empty;

                sSOAP += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                sSOAP += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
                sSOAP += "  <soap12:Body>";
                sSOAP += "    <GetTicketsBySearch xmlns=\"http://www.smartertools.com/SmarterTrack/Services2/svcTickets.asmx\">";
                sSOAP += "      <authUserName>" + STUser + "</authUserName>";
                sSOAP += "      <authPassword>" + STPass + "</authPassword>";
                sSOAP += "      <searchCriteria>";
                sSOAP += "         <string>EmailAddress=" + SepFunctions.GetUserInformation("EmailAddress") + "</string>";
                sSOAP += "      </searchCriteria>";
                sSOAP += "    </GetTicketsBySearch>";
                sSOAP += "  </soap12:Body>";
                sSOAP += "</soap12:Envelope>";

                var XMLString = SepFunctions.Send_XML(ST_API_URL() + "Services2/svcTickets.asmx?op=GetTicketsBySearch", sSOAP);

                var xmlRoot = new XmlDocument();
                xmlRoot.LoadXml(XMLString);

                var xmlSelect = "//soap:Envelope/soap:Body/ab:GetTicketsBySearchResponse/ab:GetTicketsBySearchResult/ab:Tickets/ab:TicketInfo";
                var nsmgr = new XmlNamespaceManager(xmlRoot.NameTable);
                nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                nsmgr.AddNamespace("ab", "http://www.smartertools.com/SmarterTrack/Services2/svcTickets.asmx");
                var rootNodes = xmlRoot.SelectNodes(xmlSelect, nsmgr);

                xmlRoot = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return rootNodes;
            }

            return null;
        }

        /// <summary>
        /// Sts the load configuration.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool ST_Load_Config(ref string UserName, ref string Password)
        {
            UserName = SepFunctions.Setup(67, "STUser");
            Password = SepFunctions.Decrypt(SepFunctions.Setup(67, "STPass"));

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }
    }
}