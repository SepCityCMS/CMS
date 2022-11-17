// ***********************************************************************
// Assembly         : SepCommon.Core
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-05-2020
// ***********************************************************************
// <copyright file="Modules.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCityCMS.Server.DAL.Globals
{
    using SepCore;
    using System.Collections.Generic;

    /// <summary>
    /// Class Modules.
    /// </summary>
    public static class Modules
    {
        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <param name="excludeIDs">The exclude i ds.</param>
        /// <param name="includeAdditional">The include additional.</param>
        /// <returns>List&lt;Models.Modules&gt;.</returns>
        public static List<Models.Modules> GetModules(string excludeIDs = "", string includeAdditional = "")
        {
            var lModules = new List<Models.Modules>();

            var dModules = new Models.Modules();
            var iPortalID = SepFunctions.Get_Portal_ID();

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdsAdmin"), true) && iPortalID == 0 && Strings.InStr(excludeIDs, "|2|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 2 };
                dModules.ModuleReplace = "[[advertising]]";
                dModules.ModuleName = SepFunctions.LangText("Advertising");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ArticlesAdmin"), true) && SepFunctions.ModuleActivated(35) && Strings.InStr(excludeIDs, "|35|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 35 };
                dModules.ModuleReplace = "[[articles]]";
                dModules.ModuleName = SepFunctions.LangText("Articles");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AuctionAdmin"), true) && SepFunctions.ModuleActivated(31) && Strings.InStr(excludeIDs, "|31|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 31 };
                dModules.ModuleReplace = "[[auctions]]";
                dModules.ModuleName = SepFunctions.LangText("Auctions");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("BlogsAdmin"), true) && SepFunctions.ModuleActivated(61) && Strings.InStr(excludeIDs, "|61|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 61 };
                dModules.ModuleReplace = "[[blogs]]";
                dModules.ModuleName = SepFunctions.LangText("Blogs");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("BusinessAdmin"), true) && SepFunctions.ModuleActivated(20) && Strings.InStr(excludeIDs, "|20|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 20 };
                dModules.ModuleReplace = "[[businesses]]";
                dModules.ModuleName = SepFunctions.LangText("Business Directory");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ClassifiedAdmin"), true) && SepFunctions.ModuleActivated(44) && Strings.InStr(excludeIDs, "|44|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 44 };
                dModules.ModuleReplace = "[[classifieds]]";
                dModules.ModuleName = SepFunctions.LangText("Classified Ads");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("AdminAccess"), true) && iPortalID == 0 && Strings.InStr(excludeIDs, "|1|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 1 };
                dModules.ModuleReplace = "[[contentrotator]]";
                dModules.ModuleName = SepFunctions.LangText("Content Rotator");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("DiscountsAdmin"), true) && SepFunctions.ModuleActivated(5) && Strings.InStr(excludeIDs, "|5|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 5 };
                dModules.ModuleReplace = "[[discounts]]";
                dModules.ModuleName = SepFunctions.LangText("Discount Coupons");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("LibraryAdmin"), true) && SepFunctions.ModuleActivated(10) && Strings.InStr(excludeIDs, "|10|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 10 };
                dModules.ModuleReplace = "[[libraries]]";
                dModules.ModuleName = SepFunctions.LangText("Downloads");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ELearningAdmin"), true) && SepFunctions.ModuleActivated(37) && Strings.InStr(excludeIDs, "|37|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 37 };
                dModules.ModuleReplace = "[[elearning]]";
                dModules.ModuleName = SepFunctions.LangText("E-Learning");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("EventsAdmin"), true) && SepFunctions.ModuleActivated(46) && Strings.InStr(excludeIDs, "|46|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 46 };
                dModules.ModuleReplace = "[[events]]";
                dModules.ModuleName = SepFunctions.LangText("Event Calendar");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("FAQAdmin"), true) && SepFunctions.ModuleActivated(9) && Strings.InStr(excludeIDs, "|9|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 9 };
                dModules.ModuleReplace = "[[faqs]]";
                dModules.ModuleName = SepFunctions.LangText("FAQ's");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("FormsAdmin"), true) && SepFunctions.ModuleActivated(13) && Strings.InStr(excludeIDs, "|13|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 13 };
                dModules.ModuleReplace = "[[forms]]";
                dModules.ModuleName = SepFunctions.LangText("Forms");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ForumsAdmin"), true) && SepFunctions.ModuleActivated(12) && Strings.InStr(excludeIDs, "|12|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 12 };
                dModules.ModuleReplace = "[[forums]]";
                dModules.ModuleName = SepFunctions.LangText("Forums");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("GuestbookAdmin"), true) && SepFunctions.ModuleActivated(14) && Strings.InStr(excludeIDs, "|14|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 14 };
                dModules.ModuleReplace = "[[guestbook]]";
                dModules.ModuleName = SepFunctions.LangText("Guestbook");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("HotNotAdmin"), true) && SepFunctions.ModuleActivated(40) && Strings.InStr(excludeIDs, "|40|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 40 };
                dModules.ModuleReplace = "[[hotornot]]";
                dModules.ModuleName = SepFunctions.LangText("Hot or Not");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("LinksAdmin"), true) && SepFunctions.ModuleActivated(19) && Strings.InStr(excludeIDs, "|19|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 19 };
                dModules.ModuleReplace = "[[links]]";
                dModules.ModuleName = SepFunctions.LangText("Link Directory");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("MatchAdmin"), true) && SepFunctions.ModuleActivated(18) && Strings.InStr(excludeIDs, "|18|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 18 };
                dModules.ModuleReplace = "[[matchmaker]]";
                dModules.ModuleName = SepFunctions.LangText("Match Maker");
                lModules.Add(dModules);
            }

            if (Strings.InStr(includeAdditional, "Members") > 0)
            {
                dModules = new Models.Modules { ModuleID = 0 };
                dModules.ModuleReplace = "[[members]]";
                dModules.ModuleName = SepFunctions.LangText("Members");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("NewsAdmin"), true) && SepFunctions.ModuleActivated(23) && Strings.InStr(excludeIDs, "|23|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 23 };
                dModules.ModuleReplace = "[[news]]";
                dModules.ModuleName = SepFunctions.LangText("News");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("NewsletAdmin"), true) && iPortalID == 0 && Strings.InStr(excludeIDs, "|24|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 24 };
                dModules.ModuleReplace = "[[newsletters]]";
                dModules.ModuleName = SepFunctions.LangText("Newsletters");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("GamesAdmin"), true) && SepFunctions.ModuleActivated(47) && Strings.InStr(excludeIDs, "|47|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 47 };
                dModules.ModuleReplace = "[[games]]";
                dModules.ModuleName = SepFunctions.LangText("Online Games");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PhotosAdmin"), true) && SepFunctions.ModuleActivated(28) && Strings.InStr(excludeIDs, "|28|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 28 };
                dModules.ModuleReplace = "[[albums]]";
                dModules.ModuleName = SepFunctions.LangText("Photo Albums");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PollsAdmin"), true) && SepFunctions.ModuleActivated(25) && Strings.InStr(excludeIDs, "|25|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 25 };
                dModules.ModuleReplace = "[[polls]]";
                dModules.ModuleName = SepFunctions.LangText("Polls");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("PortalsAdmin"), true) && SepFunctions.ModuleActivated(60) && iPortalID == 0 && Strings.InStr(excludeIDs, "|60|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 60 };
                dModules.ModuleReplace = "[[portals]]";
                dModules.ModuleName = SepFunctions.LangText("Portals");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("RStateAdmin"), true) && SepFunctions.ModuleActivated(32) && Strings.InStr(excludeIDs, "|32|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 32 };
                dModules.ModuleReplace = "[[realestate]]";
                dModules.ModuleName = SepFunctions.LangText("Real Estate");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ShopMallAdmin"), true) && SepFunctions.ModuleActivated(41) && (Strings.InStr(excludeIDs, "|41|") == 0))
            {
                dModules = new Models.Modules { ModuleID = 41 };
                dModules.ModuleReplace = "[[shopping]]";
                dModules.ModuleName = SepFunctions.LangText("Shopping Mall");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("SpeakerAdmin"), true) && SepFunctions.ModuleActivated(50) && Strings.InStr(excludeIDs, "|50|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 50 };
                dModules.ModuleReplace = "[[speakers]]";
                dModules.ModuleName = SepFunctions.LangText("Speakers Bureau");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("UPagesManage"), true) && SepFunctions.ModuleActivated(7) && Strings.InStr(excludeIDs, "|7|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 7 };
                dModules.ModuleReplace = "[[userpages]]";
                dModules.ModuleName = SepFunctions.LangText("User Pages");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ProfilesAdmin"), true) && SepFunctions.ModuleActivated(63) && Strings.InStr(excludeIDs, "|63|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 63 };
                dModules.ModuleReplace = "[[profiles]]";
                dModules.ModuleName = SepFunctions.LangText("User Profiles");
                lModules.Add(dModules);
            }

            if (SepFunctions.CompareKeys(SepFunctions.Security("ReviewsAdmin"), true) && SepFunctions.ModuleActivated(36) && Strings.InStr(excludeIDs, "|36|") == 0)
            {
                dModules = new Models.Modules { ModuleID = 36 };
                dModules.ModuleReplace = "[[reviews]]";
                dModules.ModuleName = SepFunctions.LangText("User Reviews");
                lModules.Add(dModules);
            }

            return lModules;
        }
    }
}