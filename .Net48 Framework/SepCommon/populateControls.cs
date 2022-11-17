// ***********************************************************************
// Assembly         : SepCommon
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="populateControls.cs" company="SepCity, Inc.">
//     Copyright © SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepCommon
{
    using System.Data.SqlClient;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    /// <summary>
    /// A separator functions.
    /// </summary>
    public static partial class SepFunctions
    {
        /// <summary>
        /// Populates the access classes.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Access_Classes(HtmlSelect toDropdown, string selected = "")
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ClassID,ClassName FROM AccessClasses WHERE ClassID <> '2' ORDER BY ClassName", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            while (RS.Read())
                            {
                                toDropdown.Items.Add(new ListItem(openNull(RS["ClassName"]), openNull(RS["ClassID"])));
                            }
                        }
                    }
                }
            }

            toDropdown.Value = selected;
        }

        /// <summary>
        /// Populates the countries.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Countries(HtmlSelect toDropdown, string selected = "")
        {
            toDropdown.Items.Add(new ListItem("Afghanistan", "af"));
            toDropdown.Items.Add(new ListItem("Albania", "al"));
            toDropdown.Items.Add(new ListItem("Algeria", "dz"));
            toDropdown.Items.Add(new ListItem("American Samoa", "as"));
            toDropdown.Items.Add(new ListItem("Andorra", "ad"));
            toDropdown.Items.Add(new ListItem("Angola", "ao"));
            toDropdown.Items.Add(new ListItem("Anguilla", "ai"));
            toDropdown.Items.Add(new ListItem("Antarctica", "aq"));
            toDropdown.Items.Add(new ListItem("Antigua and Barbuda", "ag"));
            toDropdown.Items.Add(new ListItem("Argentina", "ar"));
            toDropdown.Items.Add(new ListItem("Armenia", "am"));
            toDropdown.Items.Add(new ListItem("Aruba", "aw"));
            toDropdown.Items.Add(new ListItem("Australia", "au"));
            toDropdown.Items.Add(new ListItem("Austria", "at"));
            toDropdown.Items.Add(new ListItem("Azerbaijan", "az"));
            toDropdown.Items.Add(new ListItem("Bahamas", "bs"));
            toDropdown.Items.Add(new ListItem("Bahrain", "bh"));
            toDropdown.Items.Add(new ListItem("Bangladesh", "bd"));
            toDropdown.Items.Add(new ListItem("Barbados", "bb"));
            toDropdown.Items.Add(new ListItem("Belarus", "by"));
            toDropdown.Items.Add(new ListItem("Belgium", "be"));
            toDropdown.Items.Add(new ListItem("Belize", "bz"));
            toDropdown.Items.Add(new ListItem("Benin", "bj"));
            toDropdown.Items.Add(new ListItem("Bermuda", "bm"));
            toDropdown.Items.Add(new ListItem("Bhutan", "bt"));
            toDropdown.Items.Add(new ListItem("Bolivia", "bo"));
            toDropdown.Items.Add(new ListItem("Bosnia and Herzegovina", "ba"));
            toDropdown.Items.Add(new ListItem("Botswana", "bw"));
            toDropdown.Items.Add(new ListItem("Bouvet Island", "bv"));
            toDropdown.Items.Add(new ListItem("Brazil", "br"));
            toDropdown.Items.Add(new ListItem("British Indian Ocean Territory", "io"));
            toDropdown.Items.Add(new ListItem("British Virgin Islands", "vg"));
            toDropdown.Items.Add(new ListItem("Brunei", "bn"));
            toDropdown.Items.Add(new ListItem("Bulgaria", "bg"));
            toDropdown.Items.Add(new ListItem("Burkina Faso", "bf"));
            toDropdown.Items.Add(new ListItem("Burundi", "bi"));
            toDropdown.Items.Add(new ListItem("Cambodia", "kh"));
            toDropdown.Items.Add(new ListItem("Cameroon", "cm"));
            toDropdown.Items.Add(new ListItem("Canada", "ca"));
            toDropdown.Items.Add(new ListItem("Cape Verde", "cv"));
            toDropdown.Items.Add(new ListItem("Cayman Islands", "ky"));
            toDropdown.Items.Add(new ListItem("Central African Republic", "cf"));
            toDropdown.Items.Add(new ListItem("Chad", "td"));
            toDropdown.Items.Add(new ListItem("Chile", "cl"));
            toDropdown.Items.Add(new ListItem("China", "cn"));
            toDropdown.Items.Add(new ListItem("Christmas Island", "cx"));
            toDropdown.Items.Add(new ListItem("Cocos Islands", "cc"));
            toDropdown.Items.Add(new ListItem("Colombia", "co"));
            toDropdown.Items.Add(new ListItem("Comoros", "km"));
            toDropdown.Items.Add(new ListItem("Congo", "cg"));
            toDropdown.Items.Add(new ListItem("Cook Islands", "ck"));
            toDropdown.Items.Add(new ListItem("Costa Rica", "cr"));
            toDropdown.Items.Add(new ListItem("Croatia", "hr"));
            toDropdown.Items.Add(new ListItem("Cuba", "cu"));
            toDropdown.Items.Add(new ListItem("Cyprus", "cy"));
            toDropdown.Items.Add(new ListItem("Czech Republic", "cz"));
            toDropdown.Items.Add(new ListItem("Denmark", "dk"));
            toDropdown.Items.Add(new ListItem("Djibouti", "dj"));
            toDropdown.Items.Add(new ListItem("Dominica", "dm"));
            toDropdown.Items.Add(new ListItem("Dominican Republic", "do"));
            toDropdown.Items.Add(new ListItem("East Timor", "tp"));
            toDropdown.Items.Add(new ListItem("Ecuador", "ec"));
            toDropdown.Items.Add(new ListItem("Egypt", "eg"));
            toDropdown.Items.Add(new ListItem("El Salvador", "sv"));
            toDropdown.Items.Add(new ListItem("Equatorial Guinea", "gq"));
            toDropdown.Items.Add(new ListItem("Eritrea", "er"));
            toDropdown.Items.Add(new ListItem("Estonia", "ee"));
            toDropdown.Items.Add(new ListItem("Ethiopia", "et"));
            toDropdown.Items.Add(new ListItem("Falkland Islands", "fk"));
            toDropdown.Items.Add(new ListItem("Faroe Islands", "fo"));
            toDropdown.Items.Add(new ListItem("Fiji", "fj"));
            toDropdown.Items.Add(new ListItem("Finland", "fi"));
            toDropdown.Items.Add(new ListItem("France", "fr"));
            toDropdown.Items.Add(new ListItem("French Guiana", "gf"));
            toDropdown.Items.Add(new ListItem("French Polynesia", "pf"));
            toDropdown.Items.Add(new ListItem("French Southern Territories", "tf"));
            toDropdown.Items.Add(new ListItem("Gabon", "ga"));
            toDropdown.Items.Add(new ListItem("Gambia", "gm"));
            toDropdown.Items.Add(new ListItem("Georgia", "ge"));
            toDropdown.Items.Add(new ListItem("Germany", "de"));
            toDropdown.Items.Add(new ListItem("Ghana", "gh"));
            toDropdown.Items.Add(new ListItem("Gibraltar", "gi"));
            toDropdown.Items.Add(new ListItem("Greece", "gr"));
            toDropdown.Items.Add(new ListItem("Greenland", "gl"));
            toDropdown.Items.Add(new ListItem("Grenada", "gd"));
            toDropdown.Items.Add(new ListItem("Guadeloupe", "gd"));
            toDropdown.Items.Add(new ListItem("Guam", "gu"));
            toDropdown.Items.Add(new ListItem("Guatemala", "gt"));
            toDropdown.Items.Add(new ListItem("Guinea", "gn"));
            toDropdown.Items.Add(new ListItem("Guinea-Bissau", "gw"));
            toDropdown.Items.Add(new ListItem("Guyana", "gy"));
            toDropdown.Items.Add(new ListItem("Haiti", "ht"));
            toDropdown.Items.Add(new ListItem("Heard and McDonald Islands", "hm"));
            toDropdown.Items.Add(new ListItem("Honduras", "hn"));
            toDropdown.Items.Add(new ListItem("Hong Kong", "hk"));
            toDropdown.Items.Add(new ListItem("Hungary", "hu"));
            toDropdown.Items.Add(new ListItem("Iceland", "is"));
            toDropdown.Items.Add(new ListItem("India", "in"));
            toDropdown.Items.Add(new ListItem("Indonesia", "id"));
            toDropdown.Items.Add(new ListItem("Iran", "ir"));
            toDropdown.Items.Add(new ListItem("Iraq", "iq"));
            toDropdown.Items.Add(new ListItem("Ireland", "ie"));
            toDropdown.Items.Add(new ListItem("Israel", "il"));
            toDropdown.Items.Add(new ListItem("Italy", "it"));
            toDropdown.Items.Add(new ListItem("Ivory Coast", "ci"));
            toDropdown.Items.Add(new ListItem("Jamaica", "jm"));
            toDropdown.Items.Add(new ListItem("Japan", "jp"));
            toDropdown.Items.Add(new ListItem("Jordan", "jo"));
            toDropdown.Items.Add(new ListItem("Kazakhstan", "kz"));
            toDropdown.Items.Add(new ListItem("Kenya", "ke"));
            toDropdown.Items.Add(new ListItem("Kiribati", "ki"));
            toDropdown.Items.Add(new ListItem("Korea, North", "kp"));
            toDropdown.Items.Add(new ListItem("Korea, South", "kr"));
            toDropdown.Items.Add(new ListItem("Kuwait", "kw"));
            toDropdown.Items.Add(new ListItem("Kyrgyzstan", "kg"));
            toDropdown.Items.Add(new ListItem("Laos", "la"));
            toDropdown.Items.Add(new ListItem("Latvia", "lv"));
            toDropdown.Items.Add(new ListItem("Lebanon", "lb"));
            toDropdown.Items.Add(new ListItem("Lesotho", "ls"));
            toDropdown.Items.Add(new ListItem("Liberia", "lr"));
            toDropdown.Items.Add(new ListItem("Libya", "ly"));
            toDropdown.Items.Add(new ListItem("Liechtenstein", "li"));
            toDropdown.Items.Add(new ListItem("Lithuania", "lt"));
            toDropdown.Items.Add(new ListItem("Luxembourg", "lu"));
            toDropdown.Items.Add(new ListItem("Macau", "mo"));
            toDropdown.Items.Add(new ListItem("Macedonia, Former Yugoslav Republic", "mk"));
            toDropdown.Items.Add(new ListItem("Madagascar", "mg"));
            toDropdown.Items.Add(new ListItem("Malawi", "mw"));
            toDropdown.Items.Add(new ListItem("Malaysia", "my"));
            toDropdown.Items.Add(new ListItem("Maldives", "mv"));
            toDropdown.Items.Add(new ListItem("Mali", "ml"));
            toDropdown.Items.Add(new ListItem("Malta", "mt"));
            toDropdown.Items.Add(new ListItem("Marshall Islands", "mh"));
            toDropdown.Items.Add(new ListItem("Martinique", "mq"));
            toDropdown.Items.Add(new ListItem("Mauritania", "mr"));
            toDropdown.Items.Add(new ListItem("Mauritius", "mu"));
            toDropdown.Items.Add(new ListItem("Mayotte", "yt"));
            toDropdown.Items.Add(new ListItem("Mexico", "mx"));
            toDropdown.Items.Add(new ListItem("Micronesia, Federated States of", "fm"));
            toDropdown.Items.Add(new ListItem("Moldova", "md"));
            toDropdown.Items.Add(new ListItem("Monaco", "mc"));
            toDropdown.Items.Add(new ListItem("Mongolia", "mn"));
            toDropdown.Items.Add(new ListItem("Montserrat", "ms"));
            toDropdown.Items.Add(new ListItem("Morocco", "ma"));
            toDropdown.Items.Add(new ListItem("Mozambique", "mz"));
            toDropdown.Items.Add(new ListItem("Myanmar", "mm"));
            toDropdown.Items.Add(new ListItem("Namibia", "na"));
            toDropdown.Items.Add(new ListItem("Nauru", "nr"));
            toDropdown.Items.Add(new ListItem("Nepal", "np"));
            toDropdown.Items.Add(new ListItem("Netherlands", "nl"));
            toDropdown.Items.Add(new ListItem("Netherlands Antilles", "an"));
            toDropdown.Items.Add(new ListItem("New Caledonia", "nc"));
            toDropdown.Items.Add(new ListItem("New Zealand", "nz"));
            toDropdown.Items.Add(new ListItem("Nicaragua", "ni"));
            toDropdown.Items.Add(new ListItem("Niger", "ne"));
            toDropdown.Items.Add(new ListItem("Nigeria", "ng"));
            toDropdown.Items.Add(new ListItem("Niue", "nu"));
            toDropdown.Items.Add(new ListItem("Norfolk Island", "nf"));
            toDropdown.Items.Add(new ListItem("Northern Mariana Islands", "mp"));
            toDropdown.Items.Add(new ListItem("Norway", "no"));
            toDropdown.Items.Add(new ListItem("Oman", "om"));
            toDropdown.Items.Add(new ListItem("Pakistan", "pk"));
            toDropdown.Items.Add(new ListItem("Palau", "pw"));
            toDropdown.Items.Add(new ListItem("Panama", "pa"));
            toDropdown.Items.Add(new ListItem("Papua New Guinea", "pg"));
            toDropdown.Items.Add(new ListItem("Paraguay", "py"));
            toDropdown.Items.Add(new ListItem("Peru", "pe"));
            toDropdown.Items.Add(new ListItem("Philippines", "ph"));
            toDropdown.Items.Add(new ListItem("Pitcairn Island", "pn"));
            toDropdown.Items.Add(new ListItem("Poland", "pl"));
            toDropdown.Items.Add(new ListItem("Portugal", "pt"));
            toDropdown.Items.Add(new ListItem("Puerto Rico", "pr"));
            toDropdown.Items.Add(new ListItem("Qatar", "qa"));
            toDropdown.Items.Add(new ListItem("Reunion", "re"));
            toDropdown.Items.Add(new ListItem("Romania", "ro"));
            toDropdown.Items.Add(new ListItem("Russia", "ru"));
            toDropdown.Items.Add(new ListItem("Rwanda", "rw"));
            toDropdown.Items.Add(new ListItem("S. Georgia and S. Sandwich Isls.", "gs"));
            toDropdown.Items.Add(new ListItem("Saint Kitts & Nevis", "kn"));
            toDropdown.Items.Add(new ListItem("Saint Lucia", "lc"));
            toDropdown.Items.Add(new ListItem("Saint Vincent and The Grenadines", "vc"));
            toDropdown.Items.Add(new ListItem("Samoa", "ws"));
            toDropdown.Items.Add(new ListItem("San Marino", "sm"));
            toDropdown.Items.Add(new ListItem("Sao Tome and Principe", "st"));
            toDropdown.Items.Add(new ListItem("Saudi Arabia", "sa"));
            toDropdown.Items.Add(new ListItem("Senegal", "sn"));
            toDropdown.Items.Add(new ListItem("Seychelles", "sc"));
            toDropdown.Items.Add(new ListItem("Sierra Leone", "sl"));
            toDropdown.Items.Add(new ListItem("Singapore", "sg"));
            toDropdown.Items.Add(new ListItem("Slovakia", "sk"));
            toDropdown.Items.Add(new ListItem("Slovenia", "si"));
            toDropdown.Items.Add(new ListItem("Somalia", "so"));
            toDropdown.Items.Add(new ListItem("South Africa", "za"));
            toDropdown.Items.Add(new ListItem("Spain", "es"));
            toDropdown.Items.Add(new ListItem("Sri Lanka", "lk"));
            toDropdown.Items.Add(new ListItem("St. Helena", "sh"));
            toDropdown.Items.Add(new ListItem("St. Pierre and Miquelon", "pm"));
            toDropdown.Items.Add(new ListItem("Sudan", "sd"));
            toDropdown.Items.Add(new ListItem("Suriname", "sr"));
            toDropdown.Items.Add(new ListItem("Svalbard and Jan Mayen Islands", "sj"));
            toDropdown.Items.Add(new ListItem("Swaziland", "sz"));
            toDropdown.Items.Add(new ListItem("Sweden", "se"));
            toDropdown.Items.Add(new ListItem("Switzerland", "ch"));
            toDropdown.Items.Add(new ListItem("Syria", "sy"));
            toDropdown.Items.Add(new ListItem("Taiwan", "tw"));
            toDropdown.Items.Add(new ListItem("Tajikistan", "tj"));
            toDropdown.Items.Add(new ListItem("Tanzania", "tz"));
            toDropdown.Items.Add(new ListItem("Thailand", "th"));
            toDropdown.Items.Add(new ListItem("Togo", "tg"));
            toDropdown.Items.Add(new ListItem("Tokelau", "tk"));
            toDropdown.Items.Add(new ListItem("Tonga", "to"));
            toDropdown.Items.Add(new ListItem("Trinidad and Tobago", "tt"));
            toDropdown.Items.Add(new ListItem("Tunisia", "tn"));
            toDropdown.Items.Add(new ListItem("Turkey", "tr"));
            toDropdown.Items.Add(new ListItem("Turkmenistan", "tm"));
            toDropdown.Items.Add(new ListItem("Turks and Caicos Islands", "tc"));
            toDropdown.Items.Add(new ListItem("Tuvalu", "tv"));
            toDropdown.Items.Add(new ListItem("U.S. Minor Outlying Islands", "um"));
            toDropdown.Items.Add(new ListItem("Uganda", "ug"));
            toDropdown.Items.Add(new ListItem("Ukraine", "ua"));
            toDropdown.Items.Add(new ListItem("United Arab Emirates", "ae"));
            toDropdown.Items.Add(new ListItem("United Kingdom", "uk"));
            toDropdown.Items.Add(new ListItem("United States of America", "us"));
            toDropdown.Items.Add(new ListItem("Uruguay", "uy"));
            toDropdown.Items.Add(new ListItem("Uzbekistan", "uz"));
            toDropdown.Items.Add(new ListItem("Vanuatu", "vu"));
            toDropdown.Items.Add(new ListItem("Vatican City", "va"));
            toDropdown.Items.Add(new ListItem("Venezuela", "ve"));
            toDropdown.Items.Add(new ListItem("Vietnam", "vn"));
            toDropdown.Items.Add(new ListItem("Virgin Islands", "vi"));
            toDropdown.Items.Add(new ListItem("Wallis and Futuna Islands", "wf"));
            toDropdown.Items.Add(new ListItem("Western Sahara", "eh"));
            toDropdown.Items.Add(new ListItem("Yemen", "ye"));
            toDropdown.Items.Add(new ListItem("Yugoslavia (Former)", "yu"));
            toDropdown.Items.Add(new ListItem("Zaire", "zr"));
            toDropdown.Items.Add(new ListItem("Zambia", "zm"));
            toDropdown.Items.Add(new ListItem("Zimbabwe", "zw"));

            if (string.IsNullOrWhiteSpace(selected))
            {
                selected = "us";
            }

            toDropdown.Value = selected;
        }

        /// <summary>
        /// Populates an email templates.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Email_Templates(HtmlSelect toDropdown, string selected = "")
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT TemplateID,TemplateName FROM EmailTemplates ORDER BY EmailSubject", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            toDropdown.Items.Add(new ListItem(openNull(RS["TemplateName"]), openNull(RS["TemplateID"])));
                        }
                    }
                }
            }

            toDropdown.Value = selected;
        }

        /// <summary>
        /// Populates the forms.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Forms(HtmlSelect toDropdown, string selected = "")
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT FormID,Title FROM Forms WHERE Available='1' ORDER BY Title", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        while (RS.Read())
                        {
                            toDropdown.Items.Add(new ListItem(openNull(RS["Title"]), "FORM:" + openNull(RS["FormID"])));
                        }
                    }
                }
            }

            toDropdown.Value = selected;
        }

        /// <summary>
        /// Populates a web pages.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Web_Pages(HtmlSelect toDropdown, string selected = "")
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ModuleID,UniqueID,PageID,UserPageName,LinkText FROM ModulesNPages WHERE UserPageName <> '' AND Status=1 AND Activated='1' ORDER BY LinkText", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            toDropdown.Items.Add(new ListItem(LangText("Disabled"), string.Empty));
                            toDropdown.Items.Add(new ListItem(LangText("Account Info"), "~/account.aspx"));
                            while (RS.Read())
                            {
                                if (ModuleActivated(toLong(openNull(RS["ModuleID"]))))
                                {
                                    string strPage;
                                    if (toLong(openNull(RS["PageID"])) == 200)
                                    {
                                        strPage = "~/viewpage.aspx?UniqueID=" + openNull(RS["UniqueID"]);
                                    }
                                    else if (toLong(openNull(RS["PageID"])) == 201)
                                    {
                                        strPage = openNull(RS["UserPageName"]);
                                    }
                                    else
                                    {
                                        strPage = "~/" + openNull(RS["UserPageName"]);
                                    }

                                    toDropdown.Items.Add(new ListItem(openNull(RS["LinkText"]), strPage));
                                }
                            }
                        }
                    }
                }
            }

            toDropdown.Value = selected;
        }

        /// <summary>
        /// Populates a web pages 2.
        /// </summary>
        /// <param name="toDropdown">to dropdown.</param>
        /// <param name="selected">(Optional) The selected.</param>
        public static void Populate_Web_Pages2(HtmlSelect toDropdown, string selected = "")
        {
            using (var conn = new SqlConnection(Database_Connection()))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ModuleID,PageID,UserPageName,LinkText FROM ModulesNPages WHERE UserPageName <> '' AND PageID < 200 AND PageID <> '16' AND Status=1 AND Activated='1' ORDER BY LinkText", conn))
                {
                    using (SqlDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows)
                        {
                            toDropdown.Items.Add(new ListItem(LangText("Home Page"), string.Empty));
                            while (RS.Read())
                            {
                                if (ModuleActivated(toLong(openNull(RS["ModuleID"]))))
                                {
                                    if (toDouble(openNull(RS["PageID"])) < 200)
                                    {
                                        toDropdown.Items.Add(new ListItem(openNull(RS["LinkText"]), openNull(RS["UserPageName"])));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            toDropdown.Value = selected;
        }
    }
}