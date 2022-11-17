// ***********************************************************************
// Assembly         : wwwroot
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 10-30-2019
// ***********************************************************************
// <copyright file="realestate_property_view.aspx.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace wwwroot
{
    using SepCommon;
    using SepCommon.SepCore;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Class realestate_property_view.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class realestate_property_view : Page
    {
        /// <summary>
        /// The s address
        /// </summary>
        public static string sAddress = string.Empty;

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// The c common
        /// </summary>
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
            {
                ViewStateUserKey = SepFunctions.Session_User_ID();
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            var sInstallFolder = SepFunctions.GetInstallFolder();

            GlobalVars.ModuleID = 32;

            if (SepFunctions.Setup(GlobalVars.ModuleID, "RStateEnable") != "Enable" && Response.IsClientConnected)
                SepFunctions.Redirect(sInstallFolder + "error.aspx?ErrorID=404");
            SepFunctions.RequireLogin(SepFunctions.Security("RStateAccess"));

            var arrHeader = Strings.Split(SepFunctions.PageHeader(GlobalVars.ModuleID), "|$$|");
            Array.Resize(ref arrHeader, 3);
            Page.Title = arrHeader[0];
            Page.MetaDescription = arrHeader[1];
            Page.MetaKeywords = arrHeader[2];

            var aspnetForm = Master.FindControl("aspnetForm") as HtmlForm;
            aspnetForm.Action = Context.Request.RawUrl;

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("PropertyID")))
                {
                    var sPropertyID = SepFunctions.toLong(SepCommon.SepCore.Request.Item("PropertyID"));

                    var jProperty = SepCommon.DAL.RealEstate.Property_Get(sPropertyID);

                    if (jProperty.PropertyID == 0)
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Property~~ does not exist.") + "</div>";
                        DisplayContent.Visible = false;
                    }
                    else
                    {
                        if (SepFunctions.Check_User_Points(GlobalVars.ModuleID, "PostViewProperty", "GetViewProperty", Strings.ToString(sPropertyID), false) == false)
                        {
                            SepFunctions.Redirect(sInstallFolder + "buy_credits.aspx?DoAction=Error");
                            return;
                        }

                        Globals.LogGoogleAnalytics(Master, GlobalVars.ModuleID, "View", jProperty.Title);
                        Pictures.ContentUniqueID = SepCommon.SepCore.Request.Item("PropertyID");
                        PropertyTitle.InnerText = jProperty.Title;
                        if (jProperty.ForSale)
                        {
                            PricingTitle.InnerText = SepFunctions.Format_Currency(jProperty.Price);
                        }
                        else
                        {
                            PricingTitle.InnerText = SepFunctions.Format_Currency(jProperty.Price);
                            switch (jProperty.RecurringCycle)
                            {
                                case "3m":
                                    PricingTitle.InnerText += " / Every 3 Months";
                                    break;

                                case "6m":
                                    PricingTitle.InnerText += " / Every 6 Months";
                                    break;

                                case "1y":
                                    PricingTitle.InnerText += " / Yearly";
                                    break;

                                default:
                                    PricingTitle.InnerText += " / Monthly";
                                    break;
                            }
                        }

                        var sLocationSeperator = !string.IsNullOrWhiteSpace(jProperty.City) && !string.IsNullOrWhiteSpace(jProperty.State) ? ", " : string.Empty;
                        var sMLSNumber = !string.IsNullOrWhiteSpace(jProperty.MLSNumber) ? " (" + jProperty.MLSNumber + ")" : string.Empty;

                        Location.InnerHtml = jProperty.City + sLocationSeperator + jProperty.State + " " + jProperty.PostalCode;
                        if (!string.IsNullOrWhiteSpace(jProperty.MLSNumber)) MLSNumber.InnerHtml = SepFunctions.LangText("MLS Number:") + " " + jProperty.MLSNumber;
                        if (!string.IsNullOrWhiteSpace(jProperty.UserID)) PostedBy.InnerHtml = SepFunctions.LangText("Posted By:") + " <a href=\"" + sInstallFolder + "userinfo.aspx?UserID=" + jProperty.UserID + "\">" + SepFunctions.GetUserInformation("UserName", jProperty.UserID) + "</a><br /><a href=\"" + sInstallFolder + "messenger_compose.aspx?UserID=" + jProperty.UserID + "&Subject=" + SepFunctions.UrlEncode(jProperty.Title + sMLSNumber) + "\">" + SepFunctions.LangText("Contact Agent") + "</a><br />";
                        if (jProperty.NumRooms > 0) NumRooms.InnerHtml = "<label>" + SepFunctions.LangText("Total Rooms:") + "</label> " + jProperty.NumRooms + "<br/>";
                        if (jProperty.NumBedrooms > 0) NumBedrooms.InnerHtml = "<label>" + SepFunctions.LangText("Total Bedrooms:") + "</label> " + jProperty.NumBedrooms + "<br/>";
                        if (jProperty.NumBathrooms > 0) NumBathrooms.InnerHtml = "<label>" + SepFunctions.LangText("Total Bathrooms:") + "</label> " + jProperty.NumBathrooms + "<br/>";
                        if (!string.IsNullOrWhiteSpace(jProperty.SizeDiningRoom)) DiningRoom.InnerHtml = "<label>" + SepFunctions.LangText("Has Dining Room:") + "</label> " + jProperty.SizeDiningRoom + "<br/>";
                        if (!string.IsNullOrWhiteSpace(jProperty.SizeLot)) SizeLot.InnerHtml = "<label>" + SepFunctions.LangText("Lot Size:") + "</label> " + jProperty.SizeLot + "<br/>";
                        if (!string.IsNullOrWhiteSpace(jProperty.YearBuilt)) YearBuilt.InnerHtml = "<label>" + SepFunctions.LangText("Year Built:") + "</label> " + jProperty.YearBuilt + "<br/>";
                        if (!string.IsNullOrWhiteSpace(jProperty.Description)) Description.InnerHtml = "<h3>" + SepFunctions.LangText("Basic Features:") + "</h3> " + jProperty.Description;
                        if (!string.IsNullOrWhiteSpace(jProperty.FeatureInterior)) FeatureInterior.InnerHtml = "<h3>" + SepFunctions.LangText("Interior Features:") + "</h3> " + jProperty.FeatureInterior;
                        if (!string.IsNullOrWhiteSpace(jProperty.FeatureExterior)) FeatureExterior.InnerHtml = "<h3>" + SepFunctions.LangText("Exterior Features:") + "</h3> " + jProperty.FeatureExterior;
                        if (string.IsNullOrWhiteSpace(SepFunctions.Setup(989, "GoogleMapsAPI")))
                        {
                            MapCol.Visible = false;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(jProperty.StreetAddress) && !string.IsNullOrWhiteSpace(jProperty.City) && !string.IsNullOrWhiteSpace(jProperty.State) && !string.IsNullOrWhiteSpace(jProperty.PostalCode))
                            {
                                sAddress = jProperty.StreetAddress + ", " + jProperty.City + ", " + jProperty.State + " " + jProperty.PostalCode;
                                GoogleMap.InnerHtml = "<script async defer src=\"https://maps.googleapis.com/maps/api/js?key=" + SepFunctions.Setup(989, "GoogleMapsAPI") + "&callback=initMap\"></script>";
                            }
                            else
                            {
                                MapCol.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("~~Sorry, this property~~ does not exist.") + "</div>";
                    DisplayContent.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            SepFunctions.Page_Load();
            Page.MasterPageFile = SepFunctions.GetMasterPage();
            Globals.LoadSiteTheme(Master);
        }

        /// <summary>
        /// Handles the UnLoad event of the Page control.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            SepFunctions.Page_Unload();
        }
    }
}