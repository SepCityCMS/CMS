// ***********************************************************************
// Assembly         : SepControls
// Author           : spink
// Created          : 02-08-2019
//
// Last Modified By : spink
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="CountryDropdown.cs" company="SepCity, Inc.">
//     Copyright ? SepCity, Inc. 2019
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SepControls
{
    using SepCommon;
    using SepCommon.SepCore;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Class CountryDropdown.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebControl" />
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CountryDropdown runat=server></{0}:CountryDropdown>")]
    public class CountryDropdown : WebControl
    {
        /// <summary>
        /// The m allow blank selection
        /// </summary>
        private bool m_AllowBlankSelection;

        /// <summary>
        /// The m state dropdown identifier
        /// </summary>
        private string m_StateDropdownID;

        /// <summary>
        /// The m text
        /// </summary>
        private string m_Text;

        /// <summary>
        /// Gets or sets a value indicating whether [allow blank selection].
        /// </summary>
        /// <value><c>true</c> if [allow blank selection]; otherwise, <c>false</c>.</value>
        public bool AllowBlankSelection
        {
            get
            {
                var s = m_AllowBlankSelection;
                return s;
            }

            set => m_AllowBlankSelection = value;
        }

        /// <summary>
        /// Gets or sets the state dropdown identifier.
        /// </summary>
        /// <value>The state dropdown identifier.</value>
        public string StateDropdownID
        {
            get => Strings.ToString(m_StateDropdownID);

            set => m_StateDropdownID = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                var s = Context.Request.Form[ID];
                if (s == null)
                {
                    s = Strings.ToString(m_Text);
                }

                return s;
            }

            set => m_Text = value;
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // -V3013
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(string.Empty);
        }

        /// <summary>
        /// Renders the specified output.
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void Render(HtmlTextWriter output)
        {
            var sImageFolder = SepFunctions.GetInstallFolder(true);

            var sCity = string.Empty;
            var sState = string.Empty;
            var sCountry = string.Empty;

            if (string.IsNullOrWhiteSpace(Text))
            {
                if (string.IsNullOrWhiteSpace(SepFunctions.Session_User_ID()))
                {
                    SepFunctions.IP2Location(SepFunctions.GetUserIP(), ref sCity, ref sState, ref sCountry);
                    Text = sCountry;
                }
                else
                {
                    Text = SepFunctions.GetUserInformation("Country");
                }
            }

            if (AllowBlankSelection == false)
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    Text = SepFunctions.Setup(991, "CompanyCountry");
                }

                if (string.IsNullOrWhiteSpace(Text))
                {
                    Text = "us";
                }
            }

            output.WriteLine("<script type=\"text/javascript\" src=\"" + sImageFolder + "js/country.js\"></script>");

            if (string.IsNullOrWhiteSpace(Text))
            {
            }

            Text = Strings.LCase(Text);

            var sStateBox = !string.IsNullOrWhiteSpace(StateDropdownID) ? "try{StateBox('" + SepFunctions.SepCityToken() + "', $('#" + ID + "').val(), $('#" + StateDropdownID + "'));}catch(e){};" : string.Empty;
            var sDisabled = Enabled == false ? " disabled=\"disabled\"" : string.Empty;

            output.WriteLine("<select name=\"" + ID + "\" id=\"" + ID + "\" onchange=\"" + sStateBox + "\" class=\"" + CssClass + "\"" + sDisabled + ">");
            if (AllowBlankSelection)
            {
                output.WriteLine("<option value=\"\">-------- " + SepFunctions.LangText("Select a Country") + " --------</option>");
            }

            output.WriteLine("<option value=\"af\"" + Strings.ToString(Text == "af" ? " selected=\"selected\"" : string.Empty) + ">Afghanistan</option>");
            output.WriteLine("<option value=\"al\"" + Strings.ToString(Text == "al" ? " selected=\"selected\"" : string.Empty) + ">Albania</option>");
            output.WriteLine("<option value=\"dz\"" + Strings.ToString(Text == "dz" ? " selected=\"selected\"" : string.Empty) + ">Algeria</option>");
            output.WriteLine("<option value=\"as\"" + Strings.ToString(Text == "as" ? " selected=\"selected\"" : string.Empty) + ">American Samoa</option>");
            output.WriteLine("<option value=\"ad\"" + Strings.ToString(Text == "ad" ? " selected=\"selected\"" : string.Empty) + ">Andorra</option>");
            output.WriteLine("<option value=\"ao\"" + Strings.ToString(Text == "ao" ? " selected=\"selected\"" : string.Empty) + ">Angola</option>");
            output.WriteLine("<option value=\"ai\"" + Strings.ToString(Text == "ai" ? " selected=\"selected\"" : string.Empty) + ">Anguilla</option>");
            output.WriteLine("<option value=\"aq\"" + Strings.ToString(Text == "aq" ? " selected=\"selected\"" : string.Empty) + ">Antarctica</option>");
            output.WriteLine("<option value=\"ag\"" + Strings.ToString(Text == "ag" ? " selected=\"selected\"" : string.Empty) + ">Antigua and Barbuda</option>");
            output.WriteLine("<option value=\"ar\"" + Strings.ToString(Text == "ar" ? " selected=\"selected\"" : string.Empty) + ">Argentina</option>");
            output.WriteLine("<option value=\"am\"" + Strings.ToString(Text == "am" ? " selected=\"selected\"" : string.Empty) + ">Armenia</option>");
            output.WriteLine("<option value=\"aw\"" + Strings.ToString(Text == "aw" ? " selected=\"selected\"" : string.Empty) + ">Aruba</option>");
            output.WriteLine("<option value=\"au\"" + Strings.ToString(Text == "au" ? " selected=\"selected\"" : string.Empty) + ">Australia</option>");
            output.WriteLine("<option value=\"at\"" + Strings.ToString(Text == "at" ? " selected=\"selected\"" : string.Empty) + ">Austria</option>");
            output.WriteLine("<option value=\"az\"" + Strings.ToString(Text == "az" ? " selected=\"selected\"" : string.Empty) + ">Azerbaijan</option>");
            output.WriteLine("<option value=\"bs\"" + Strings.ToString(Text == "bs" ? " selected=\"selected\"" : string.Empty) + ">Bahamas</option>");
            output.WriteLine("<option value=\"bh\"" + Strings.ToString(Text == "bh" ? " selected=\"selected\"" : string.Empty) + ">Bahrain</option>");
            output.WriteLine("<option value=\"bd\"" + Strings.ToString(Text == "bd" ? " selected=\"selected\"" : string.Empty) + ">Bangladesh</option>");
            output.WriteLine("<option value=\"bb\"" + Strings.ToString(Text == "bb" ? " selected=\"selected\"" : string.Empty) + ">Barbados</option>");
            output.WriteLine("<option value=\"by\"" + Strings.ToString(Text == "by" ? " selected=\"selected\"" : string.Empty) + ">Belarus</option>");
            output.WriteLine("<option value=\"be\"" + Strings.ToString(Text == "be" ? " selected=\"selected\"" : string.Empty) + ">Belgium</option>");
            output.WriteLine("<option value=\"bz\"" + Strings.ToString(Text == "bz" ? " selected=\"selected\"" : string.Empty) + ">Belize</option>");
            output.WriteLine("<option value=\"bj\"" + Strings.ToString(Text == "bj" ? " selected=\"selected\"" : string.Empty) + ">Benin</option>");
            output.WriteLine("<option value=\"bm\"" + Strings.ToString(Text == "bm" ? " selected=\"selected\"" : string.Empty) + ">Bermuda</option>");
            output.WriteLine("<option value=\"bt\"" + Strings.ToString(Text == "bt" ? " selected=\"selected\"" : string.Empty) + ">Bhutan</option>");
            output.WriteLine("<option value=\"bo\"" + Strings.ToString(Text == "bo" ? " selected=\"selected\"" : string.Empty) + ">Bolivia</option>");
            output.WriteLine("<option value=\"ba\"" + Strings.ToString(Text == "ba" ? " selected=\"selected\"" : string.Empty) + ">Bosnia and Herzegovina</option>");
            output.WriteLine("<option value=\"bw\"" + Strings.ToString(Text == "bw" ? " selected=\"selected\"" : string.Empty) + ">Botswana</option>");
            output.WriteLine("<option value=\"bv\"" + Strings.ToString(Text == "bv" ? " selected=\"selected\"" : string.Empty) + ">Bouvet Island</option>");
            output.WriteLine("<option value=\"br\"" + Strings.ToString(Text == "br" ? " selected=\"selected\"" : string.Empty) + ">Brazil</option>");
            output.WriteLine("<option value=\"io\"" + Strings.ToString(Text == "io" ? " selected=\"selected\"" : string.Empty) + ">British Indian Ocean Territory</option>");
            output.WriteLine("<option value=\"vg\"" + Strings.ToString(Text == "vg" ? " selected=\"selected\"" : string.Empty) + ">British Virgin Islands</option>");
            output.WriteLine("<option value=\"bn\"" + Strings.ToString(Text == "bn" ? " selected=\"selected\"" : string.Empty) + ">Brunei</option>");
            output.WriteLine("<option value=\"bg\"" + Strings.ToString(Text == "bg" ? " selected=\"selected\"" : string.Empty) + ">Bulgaria</option>");
            output.WriteLine("<option value=\"bf\"" + Strings.ToString(Text == "bf" ? " selected=\"selected\"" : string.Empty) + ">Burkina Faso</option>");
            output.WriteLine("<option value=\"bi\"" + Strings.ToString(Text == "bi" ? " selected=\"selected\"" : string.Empty) + ">Burundi</option>");
            output.WriteLine("<option value=\"kh\"" + Strings.ToString(Text == "kh" ? " selected=\"selected\"" : string.Empty) + ">Cambodia</option>");
            output.WriteLine("<option value=\"cm\"" + Strings.ToString(Text == "cm" ? " selected=\"selected\"" : string.Empty) + ">Cameroon</option>");
            output.WriteLine("<option value=\"ca\"" + Strings.ToString(Text == "ca" ? " selected=\"selected\"" : string.Empty) + ">Canada</option>");
            output.WriteLine("<option value=\"cv\"" + Strings.ToString(Text == "cv" ? " selected=\"selected\"" : string.Empty) + ">Cape Verde</option>");
            output.WriteLine("<option value=\"ky\"" + Strings.ToString(Text == "ky" ? " selected=\"selected\"" : string.Empty) + ">Cayman Islands</option>");
            output.WriteLine("<option value=\"cf\"" + Strings.ToString(Text == "cf" ? " selected=\"selected\"" : string.Empty) + ">Central African Republic</option>");
            output.WriteLine("<option value=\"td\"" + Strings.ToString(Text == "td" ? " selected=\"selected\"" : string.Empty) + ">Chad</option>");
            output.WriteLine("<option value=\"cl\"" + Strings.ToString(Text == "cl" ? " selected=\"selected\"" : string.Empty) + ">Chile</option>");
            output.WriteLine("<option value=\"cn\"" + Strings.ToString(Text == "cn" ? " selected=\"selected\"" : string.Empty) + ">China</option>");
            output.WriteLine("<option value=\"cx\"" + Strings.ToString(Text == "cx" ? " selected=\"selected\"" : string.Empty) + ">Christmas Island</option>");
            output.WriteLine("<option value=\"cc\"" + Strings.ToString(Text == "cc" ? " selected=\"selected\"" : string.Empty) + ">Cocos Islands</option>");
            output.WriteLine("<option value=\"co\"" + Strings.ToString(Text == "co" ? " selected=\"selected\"" : string.Empty) + ">Colombia</option>");
            output.WriteLine("<option value=\"km\"" + Strings.ToString(Text == "km" ? " selected=\"selected\"" : string.Empty) + ">Comoros</option>");
            output.WriteLine("<option value=\"cg\"" + Strings.ToString(Text == "cg" ? " selected=\"selected\"" : string.Empty) + ">Congo</option>");
            output.WriteLine("<option value=\"ck\"" + Strings.ToString(Text == "ck" ? " selected=\"selected\"" : string.Empty) + ">Cook Islands</option>");
            output.WriteLine("<option value=\"cr\"" + Strings.ToString(Text == "cr" ? " selected=\"selected\"" : string.Empty) + ">Costa Rica</option>");
            output.WriteLine("<option value=\"hr\"" + Strings.ToString(Text == "hr" ? " selected=\"selected\"" : string.Empty) + ">Croatia</option>");
            output.WriteLine("<option value=\"cu\"" + Strings.ToString(Text == "cu" ? " selected=\"selected\"" : string.Empty) + ">Cuba</option>");
            output.WriteLine("<option value=\"cy\"" + Strings.ToString(Text == "cy" ? " selected=\"selected\"" : string.Empty) + ">Cyprus</option>");
            output.WriteLine("<option value=\"cz\"" + Strings.ToString(Text == "cz" ? " selected=\"selected\"" : string.Empty) + ">Czech Republic</option>");
            output.WriteLine("<option value=\"dk\"" + Strings.ToString(Text == "dk" ? " selected=\"selected\"" : string.Empty) + ">Denmark</option>");
            output.WriteLine("<option value=\"dj\"" + Strings.ToString(Text == "dj" ? " selected=\"selected\"" : string.Empty) + ">Djibouti</option>");
            output.WriteLine("<option value=\"dm\"" + Strings.ToString(Text == "dm" ? " selected=\"selected\"" : string.Empty) + ">Dominica</option>");
            output.WriteLine("<option value=\"do\"" + Strings.ToString(Text == "do" ? " selected=\"selected\"" : string.Empty) + ">Dominican Republic</option>");
            output.WriteLine("<option value=\"tp\"" + Strings.ToString(Text == "tp" ? " selected=\"selected\"" : string.Empty) + ">East Timor</option>");
            output.WriteLine("<option value=\"ec\"" + Strings.ToString(Text == "ec" ? " selected=\"selected\"" : string.Empty) + ">Ecuador</option>");
            output.WriteLine("<option value=\"eg\"" + Strings.ToString(Text == "eg" ? " selected=\"selected\"" : string.Empty) + ">Egypt</option>");
            output.WriteLine("<option value=\"sv\"" + Strings.ToString(Text == "sv" ? " selected=\"selected\"" : string.Empty) + ">El Salvador</option>");
            output.WriteLine("<option value=\"gq\"" + Strings.ToString(Text == "gq" ? " selected=\"selected\"" : string.Empty) + ">Equatorial Guinea</option>");
            output.WriteLine("<option value=\"er\"" + Strings.ToString(Text == "er" ? " selected=\"selected\"" : string.Empty) + ">Eritrea</option>");
            output.WriteLine("<option value=\"ee\"" + Strings.ToString(Text == "ee" ? " selected=\"selected\"" : string.Empty) + ">Estonia</option>");
            output.WriteLine("<option value=\"et\"" + Strings.ToString(Text == "et" ? " selected=\"selected\"" : string.Empty) + ">Ethiopia</option>");
            output.WriteLine("<option value=\"fk\"" + Strings.ToString(Text == "fk" ? " selected=\"selected\"" : string.Empty) + ">Falkland Islands</option>");
            output.WriteLine("<option value=\"fo\"" + Strings.ToString(Text == "fo" ? " selected=\"selected\"" : string.Empty) + ">Faroe Islands</option>");
            output.WriteLine("<option value=\"fj\"" + Strings.ToString(Text == "fj" ? " selected=\"selected\"" : string.Empty) + ">Fiji</option>");
            output.WriteLine("<option value=\"fi\"" + Strings.ToString(Text == "fi" ? " selected=\"selected\"" : string.Empty) + ">Finland</option>");
            output.WriteLine("<option value=\"fr\"" + Strings.ToString(Text == "fr" ? " selected=\"selected\"" : string.Empty) + ">France</option>");
            output.WriteLine("<option value=\"gf\"" + Strings.ToString(Text == "gf" ? " selected=\"selected\"" : string.Empty) + ">French Guiana</option>");
            output.WriteLine("<option value=\"pf\"" + Strings.ToString(Text == "pf" ? " selected=\"selected\"" : string.Empty) + ">French Polynesia</option>");
            output.WriteLine("<option value=\"tf\"" + Strings.ToString(Text == "tf" ? " selected=\"selected\"" : string.Empty) + ">French Southern Territories</option>");
            output.WriteLine("<option value=\"ga\"" + Strings.ToString(Text == "ga" ? " selected=\"selected\"" : string.Empty) + ">Gabon</option>");
            output.WriteLine("<option value=\"gm\"" + Strings.ToString(Text == "gm" ? " selected=\"selected\"" : string.Empty) + ">Gambia</option>");
            output.WriteLine("<option value=\"ge\"" + Strings.ToString(Text == "ge" ? " selected=\"selected\"" : string.Empty) + ">Georgia</option>");
            output.WriteLine("<option value=\"de\"" + Strings.ToString(Text == "de" ? " selected=\"selected\"" : string.Empty) + ">Germany</option>");
            output.WriteLine("<option value=\"gh\"" + Strings.ToString(Text == "gh" ? " selected=\"selected\"" : string.Empty) + ">Ghana</option>");
            output.WriteLine("<option value=\"gi\"" + Strings.ToString(Text == "gi" ? " selected=\"selected\"" : string.Empty) + ">Gibraltar</option>");
            output.WriteLine("<option value=\"gr\"" + Strings.ToString(Text == "gr" ? " selected=\"selected\"" : string.Empty) + ">Greece</option>");
            output.WriteLine("<option value=\"gl\"" + Strings.ToString(Text == "gl" ? " selected=\"selected\"" : string.Empty) + ">Greenland</option>");
            output.WriteLine("<option value=\"gd\"" + Strings.ToString(Text == "gd" ? " selected=\"selected\"" : string.Empty) + ">Grenada</option>");
            output.WriteLine("<option value=\"gp\"" + Strings.ToString(Text == "gp" ? " selected=\"selected\"" : string.Empty) + ">Guadeloupe</option>");
            output.WriteLine("<option value=\"gu\"" + Strings.ToString(Text == "gu" ? " selected=\"selected\"" : string.Empty) + ">Guam</option>");
            output.WriteLine("<option value=\"gt\"" + Strings.ToString(Text == "gt" ? " selected=\"selected\"" : string.Empty) + ">Guatemala</option>");
            output.WriteLine("<option value=\"gn\"" + Strings.ToString(Text == "gn" ? " selected=\"selected\"" : string.Empty) + ">Guinea</option>");
            output.WriteLine("<option value=\"gw\"" + Strings.ToString(Text == "gw" ? " selected=\"selected\"" : string.Empty) + ">Guinea-Bissau</option>");
            output.WriteLine("<option value=\"gy\"" + Strings.ToString(Text == "gy" ? " selected=\"selected\"" : string.Empty) + ">Guyana</option>");
            output.WriteLine("<option value=\"ht\"" + Strings.ToString(Text == "ht" ? " selected=\"selected\"" : string.Empty) + ">Haiti</option>");
            output.WriteLine("<option value=\"hm\"" + Strings.ToString(Text == "hm" ? " selected=\"selected\"" : string.Empty) + ">Heard and McDonald Islands</option>");
            output.WriteLine("<option value=\"hn\"" + Strings.ToString(Text == "hn" ? " selected=\"selected\"" : string.Empty) + ">Honduras</option>");
            output.WriteLine("<option value=\"hk\"" + Strings.ToString(Text == "hk" ? " selected=\"selected\"" : string.Empty) + ">Hong Kong</option>");
            output.WriteLine("<option value=\"hu\"" + Strings.ToString(Text == "hu" ? " selected=\"selected\"" : string.Empty) + ">Hungary</option>");
            output.WriteLine("<option value=\"is\"" + Strings.ToString(Text == "is" ? " selected=\"selected\"" : string.Empty) + ">Iceland</option>");
            output.WriteLine("<option value=\"in\"" + Strings.ToString(Text == "in" ? " selected=\"selected\"" : string.Empty) + ">India</option>");
            output.WriteLine("<option value=\"id\"" + Strings.ToString(Text == "id" ? " selected=\"selected\"" : string.Empty) + ">Indonesia</option>");
            output.WriteLine("<option value=\"ir\"" + Strings.ToString(Text == "ir" ? " selected=\"selected\"" : string.Empty) + ">Iran</option>");
            output.WriteLine("<option value=\"iq\"" + Strings.ToString(Text == "iq" ? " selected=\"selected\"" : string.Empty) + ">Iraq</option>");
            output.WriteLine("<option value=\"ie\"" + Strings.ToString(Text == "ie" ? " selected=\"selected\"" : string.Empty) + ">Ireland</option>");
            output.WriteLine("<option value=\"il\"" + Strings.ToString(Text == "il" ? " selected=\"selected\"" : string.Empty) + ">Israel</option>");
            output.WriteLine("<option value=\"it\"" + Strings.ToString(Text == "it" ? " selected=\"selected\"" : string.Empty) + ">Italy</option>");
            output.WriteLine("<option value=\"ci\"" + Strings.ToString(Text == "ci" ? " selected=\"selected\"" : string.Empty) + ">Ivory Coast</option>");
            output.WriteLine("<option value=\"jm\"" + Strings.ToString(Text == "jm" ? " selected=\"selected\"" : string.Empty) + ">Jamaica</option>");
            output.WriteLine("<option value=\"jp\"" + Strings.ToString(Text == "jp" ? " selected=\"selected\"" : string.Empty) + ">Japan</option>");
            output.WriteLine("<option value=\"jo\"" + Strings.ToString(Text == "jo" ? " selected=\"selected\"" : string.Empty) + ">Jordan</option>");
            output.WriteLine("<option value=\"kz\"" + Strings.ToString(Text == "kz" ? " selected=\"selected\"" : string.Empty) + ">Kazakhstan</option>");
            output.WriteLine("<option value=\"ke\"" + Strings.ToString(Text == "ke" ? " selected=\"selected\"" : string.Empty) + ">Kenya</option>");
            output.WriteLine("<option value=\"ki\"" + Strings.ToString(Text == "ki" ? " selected=\"selected\"" : string.Empty) + ">Kiribati</option>");
            output.WriteLine("<option value=\"kp\"" + Strings.ToString(Text == "kp" ? " selected=\"selected\"" : string.Empty) + ">Korea, North</option>");
            output.WriteLine("<option value=\"kr\"" + Strings.ToString(Text == "kr" ? " selected=\"selected\"" : string.Empty) + ">Korea, South</option>");
            output.WriteLine("<option value=\"kw\"" + Strings.ToString(Text == "kw" ? " selected=\"selected\"" : string.Empty) + ">Kuwait</option>");
            output.WriteLine("<option value=\"kg\"" + Strings.ToString(Text == "kg" ? " selected=\"selected\"" : string.Empty) + ">Kyrgyzstan</option>");
            output.WriteLine("<option value=\"la\"" + Strings.ToString(Text == "la" ? " selected=\"selected\"" : string.Empty) + ">Laos</option>");
            output.WriteLine("<option value=\"lv\"" + Strings.ToString(Text == "lv" ? " selected=\"selected\"" : string.Empty) + ">Latvia</option>");
            output.WriteLine("<option value=\"lb\"" + Strings.ToString(Text == "lb" ? " selected=\"selected\"" : string.Empty) + ">Lebanon</option>");
            output.WriteLine("<option value=\"ls\"" + Strings.ToString(Text == "ls" ? " selected=\"selected\"" : string.Empty) + ">Lesotho</option>");
            output.WriteLine("<option value=\"lr\"" + Strings.ToString(Text == "lr" ? " selected=\"selected\"" : string.Empty) + ">Liberia</option>");
            output.WriteLine("<option value=\"ly\"" + Strings.ToString(Text == "ly" ? " selected=\"selected\"" : string.Empty) + ">Libya</option>");
            output.WriteLine("<option value=\"li\"" + Strings.ToString(Text == "li" ? " selected=\"selected\"" : string.Empty) + ">Liechtenstein</option>");
            output.WriteLine("<option value=\"lt\"" + Strings.ToString(Text == "lt" ? " selected=\"selected\"" : string.Empty) + ">Lithuania</option>");
            output.WriteLine("<option value=\"lu\"" + Strings.ToString(Text == "lu" ? " selected=\"selected\"" : string.Empty) + ">Luxembourg</option>");
            output.WriteLine("<option value=\"mo\"" + Strings.ToString(Text == "mo" ? " selected=\"selected\"" : string.Empty) + ">Macau</option>");
            output.WriteLine("<option value=\"mk\"" + Strings.ToString(Text == "mk" ? " selected=\"selected\"" : string.Empty) + ">Macedonia, Former Yugoslav Republic</option>");
            output.WriteLine("<option value=\"mg\"" + Strings.ToString(Text == "mg" ? " selected=\"selected\"" : string.Empty) + ">Madagascar</option>");
            output.WriteLine("<option value=\"mw\"" + Strings.ToString(Text == "mw" ? " selected=\"selected\"" : string.Empty) + ">Malawi</option>");
            output.WriteLine("<option value=\"my\"" + Strings.ToString(Text == "my" ? " selected=\"selected\"" : string.Empty) + ">Malaysia</option>");
            output.WriteLine("<option value=\"mv\"" + Strings.ToString(Text == "mv" ? " selected=\"selected\"" : string.Empty) + ">Maldives</option>");
            output.WriteLine("<option value=\"ml\"" + Strings.ToString(Text == "ml" ? " selected=\"selected\"" : string.Empty) + ">Mali</option>");
            output.WriteLine("<option value=\"mt\"" + Strings.ToString(Text == "mt" ? " selected=\"selected\"" : string.Empty) + ">Malta</option>");
            output.WriteLine("<option value=\"mh\"" + Strings.ToString(Text == "mh" ? " selected=\"selected\"" : string.Empty) + ">Marshall Islands</option>");
            output.WriteLine("<option value=\"mq\"" + Strings.ToString(Text == "mq" ? " selected=\"selected\"" : string.Empty) + ">Martinique</option>");
            output.WriteLine("<option value=\"mr\"" + Strings.ToString(Text == "mr" ? " selected=\"selected\"" : string.Empty) + ">Mauritania</option>");
            output.WriteLine("<option value=\"mu\"" + Strings.ToString(Text == "mu" ? " selected=\"selected\"" : string.Empty) + ">Mauritius</option>");
            output.WriteLine("<option value=\"yt\"" + Strings.ToString(Text == "yt" ? " selected=\"selected\"" : string.Empty) + ">Mayotte</option>");
            output.WriteLine("<option value=\"mx\"" + Strings.ToString(Text == "mx" ? " selected=\"selected\"" : string.Empty) + ">Mexico</option>");
            output.WriteLine("<option value=\"fm\"" + Strings.ToString(Text == "fm" ? " selected=\"selected\"" : string.Empty) + ">Micronesia, Federated States of</option>");
            output.WriteLine("<option value=\"md\"" + Strings.ToString(Text == "md" ? " selected=\"selected\"" : string.Empty) + ">Moldova</option>");
            output.WriteLine("<option value=\"mc\"" + Strings.ToString(Text == "mc" ? " selected=\"selected\"" : string.Empty) + ">Monaco</option>");
            output.WriteLine("<option value=\"mn\"" + Strings.ToString(Text == "mn" ? " selected=\"selected\"" : string.Empty) + ">Mongolia</option>");
            output.WriteLine("<option value=\"ms\"" + Strings.ToString(Text == "ms" ? " selected=\"selected\"" : string.Empty) + ">Montserrat</option>");
            output.WriteLine("<option value=\"ma\"" + Strings.ToString(Text == "ma" ? " selected=\"selected\"" : string.Empty) + ">Morocco</option>");
            output.WriteLine("<option value=\"mz\"" + Strings.ToString(Text == "mz" ? " selected=\"selected\"" : string.Empty) + ">Mozambique</option>");
            output.WriteLine("<option value=\"mm\"" + Strings.ToString(Text == "mn" ? " selected=\"selected\"" : string.Empty) + ">Myanmar</option>");
            output.WriteLine("<option value=\"na\"" + Strings.ToString(Text == "na" ? " selected=\"selected\"" : string.Empty) + ">Namibia</option>");
            output.WriteLine("<option value=\"nr\"" + Strings.ToString(Text == "nr" ? " selected=\"selected\"" : string.Empty) + ">Nauru</option>");
            output.WriteLine("<option value=\"np\"" + Strings.ToString(Text == "np" ? " selected=\"selected\"" : string.Empty) + ">Nepal</option>");
            output.WriteLine("<option value=\"nl\"" + Strings.ToString(Text == "nl" ? " selected=\"selected\"" : string.Empty) + ">Netherlands</option>");
            output.WriteLine("<option value=\"an\"" + Strings.ToString(Text == "an" ? " selected=\"selected\"" : string.Empty) + ">Netherlands Antilles</option>");
            output.WriteLine("<option value=\"nc\"" + Strings.ToString(Text == "nc" ? " selected=\"selected\"" : string.Empty) + ">New Caledonia</option>");
            output.WriteLine("<option value=\"nz\"" + Strings.ToString(Text == "nz" ? " selected=\"selected\"" : string.Empty) + ">New Zealand</option>");
            output.WriteLine("<option value=\"ni\"" + Strings.ToString(Text == "ni" ? " selected=\"selected\"" : string.Empty) + ">Nicaragua</option>");
            output.WriteLine("<option value=\"ne\"" + Strings.ToString(Text == "ne" ? " selected=\"selected\"" : string.Empty) + ">Niger</option>");
            output.WriteLine("<option value=\"ng\"" + Strings.ToString(Text == "ng" ? " selected=\"selected\"" : string.Empty) + ">Nigeria</option>");
            output.WriteLine("<option value=\"nu\"" + Strings.ToString(Text == "nu" ? " selected=\"selected\"" : string.Empty) + ">Niue</option>");
            output.WriteLine("<option value=\"nf\"" + Strings.ToString(Text == "nf" ? " selected=\"selected\"" : string.Empty) + ">Norfolk Island</option>");
            output.WriteLine("<option value=\"mp\"" + Strings.ToString(Text == "mp" ? " selected=\"selected\"" : string.Empty) + ">Northern Mariana Islands</option>");
            output.WriteLine("<option value=\"no\"" + Strings.ToString(Text == "no" ? " selected=\"selected\"" : string.Empty) + ">Norway</option>");
            output.WriteLine("<option value=\"om\"" + Strings.ToString(Text == "om" ? " selected=\"selected\"" : string.Empty) + ">Oman</option>");
            output.WriteLine("<option value=\"pk\"" + Strings.ToString(Text == "pk" ? " selected=\"selected\"" : string.Empty) + ">Pakistan</option>");
            output.WriteLine("<option value=\"pw\"" + Strings.ToString(Text == "pw" ? " selected=\"selected\"" : string.Empty) + ">Palau</option>");
            output.WriteLine("<option value=\"pa\"" + Strings.ToString(Text == "pa" ? " selected=\"selected\"" : string.Empty) + ">Panama</option>");
            output.WriteLine("<option value=\"pg\"" + Strings.ToString(Text == "pg" ? " selected=\"selected\"" : string.Empty) + ">Papua New Guinea</option>");
            output.WriteLine("<option value=\"py\"" + Strings.ToString(Text == "py" ? " selected=\"selected\"" : string.Empty) + ">Paraguay</option>");
            output.WriteLine("<option value=\"pe\"" + Strings.ToString(Text == "pe" ? " selected=\"selected\"" : string.Empty) + ">Peru</option>");
            output.WriteLine("<option value=\"ph\"" + Strings.ToString(Text == "ph" ? " selected=\"selected\"" : string.Empty) + ">Philippines</option>");
            output.WriteLine("<option value=\"pn\"" + Strings.ToString(Text == "pn" ? " selected=\"selected\"" : string.Empty) + ">Pitcairn Island</option>");
            output.WriteLine("<option value=\"pl\"" + Strings.ToString(Text == "pl" ? " selected=\"selected\"" : string.Empty) + ">Poland</option>");
            output.WriteLine("<option value=\"pt\"" + Strings.ToString(Text == "pt" ? " selected=\"selected\"" : string.Empty) + ">Portugal</option>");
            output.WriteLine("<option value=\"pr\"" + Strings.ToString(Text == "pr" ? " selected=\"selected\"" : string.Empty) + ">Puerto Rico</option>");
            output.WriteLine("<option value=\"qa\"" + Strings.ToString(Text == "qa" ? " selected=\"selected\"" : string.Empty) + ">Qatar</option>");
            output.WriteLine("<option value=\"re\"" + Strings.ToString(Text == "re" ? " selected=\"selected\"" : string.Empty) + ">Reunion</option>");
            output.WriteLine("<option value=\"ro\"" + Strings.ToString(Text == "ro" ? " selected=\"selected\"" : string.Empty) + ">Romania</option>");
            output.WriteLine("<option value=\"ru\"" + Strings.ToString(Text == "ru" ? " selected=\"selected\"" : string.Empty) + ">Russia</option>");
            output.WriteLine("<option value=\"rw\"" + Strings.ToString(Text == "rw" ? " selected=\"selected\"" : string.Empty) + ">Rwanda</option>");
            output.WriteLine("<option value=\"gs\"" + Strings.ToString(Text == "gs" ? " selected=\"selected\"" : string.Empty) + ">S. Georgia and S. Sandwich Isls.</option>");
            output.WriteLine("<option value=\"kn\"" + Strings.ToString(Text == "kn" ? " selected=\"selected\"" : string.Empty) + ">Saint Kitts & Nevis</option>");
            output.WriteLine("<option value=\"lc\"" + Strings.ToString(Text == "lc" ? " selected=\"selected\"" : string.Empty) + ">Saint Lucia</option>");
            output.WriteLine("<option value=\"vc\"" + Strings.ToString(Text == "vc" ? " selected=\"selected\"" : string.Empty) + ">Saint Vincent and The Grenadines</option>");
            output.WriteLine("<option value=\"ws\"" + Strings.ToString(Text == "ws" ? " selected=\"selected\"" : string.Empty) + ">Samoa</option>");
            output.WriteLine("<option value=\"sm\"" + Strings.ToString(Text == "sm" ? " selected=\"selected\"" : string.Empty) + ">San Marino</option>");
            output.WriteLine("<option value=\"st\"" + Strings.ToString(Text == "st" ? " selected=\"selected\"" : string.Empty) + ">Sao Tome and Principe</option>");
            output.WriteLine("<option value=\"sa\"" + Strings.ToString(Text == "sa" ? " selected=\"selected\"" : string.Empty) + ">Saudi Arabia</option>");
            output.WriteLine("<option value=\"sn\"" + Strings.ToString(Text == "sn" ? " selected=\"selected\"" : string.Empty) + ">Senegal</option>");
            output.WriteLine("<option value=\"sc\"" + Strings.ToString(Text == "sc" ? " selected=\"selected\"" : string.Empty) + ">Seychelles</option>");
            output.WriteLine("<option value=\"sl\"" + Strings.ToString(Text == "sl" ? " selected=\"selected\"" : string.Empty) + ">Sierra Leone</option>");
            output.WriteLine("<option value=\"sg\"" + Strings.ToString(Text == "sg" ? " selected=\"selected\"" : string.Empty) + ">Singapore</option>");
            output.WriteLine("<option value=\"sk\"" + Strings.ToString(Text == "sk" ? " selected=\"selected\"" : string.Empty) + ">Slovakia</option>");
            output.WriteLine("<option value=\"si\"" + Strings.ToString(Text == "si" ? " selected=\"selected\"" : string.Empty) + ">Slovenia</option>");
            output.WriteLine("<option value=\"so\"" + Strings.ToString(Text == "sc" ? " selected=\"selected\"" : string.Empty) + ">Somalia</option>");
            output.WriteLine("<option value=\"za\"" + Strings.ToString(Text == "za" ? " selected=\"selected\"" : string.Empty) + ">South Africa</option>");
            output.WriteLine("<option value=\"es\"" + Strings.ToString(Text == "es" ? " selected=\"selected\"" : string.Empty) + ">Spain</option>");
            output.WriteLine("<option value=\"lk\"" + Strings.ToString(Text == "lk" ? " selected=\"selected\"" : string.Empty) + ">Sri Lanka</option>");
            output.WriteLine("<option value=\"sh\"" + Strings.ToString(Text == "sh" ? " selected=\"selected\"" : string.Empty) + ">St. Helena</option>");
            output.WriteLine("<option value=\"pm\"" + Strings.ToString(Text == "pm" ? " selected=\"selected\"" : string.Empty) + ">St. Pierre and Miquelon</option>");
            output.WriteLine("<option value=\"sd\"" + Strings.ToString(Text == "sd" ? " selected=\"selected\"" : string.Empty) + ">Sudan</option>");
            output.WriteLine("<option value=\"sr\"" + Strings.ToString(Text == "sr" ? " selected=\"selected\"" : string.Empty) + ">Suriname</option>");
            output.WriteLine("<option value=\"sj\"" + Strings.ToString(Text == "sj" ? " selected=\"selected\"" : string.Empty) + ">Svalbard and Jan Mayen Islands</option>");
            output.WriteLine("<option value=\"sz\"" + Strings.ToString(Text == "sz" ? " selected=\"selected\"" : string.Empty) + ">Swaziland</option>");
            output.WriteLine("<option value=\"se\"" + Strings.ToString(Text == "se" ? " selected=\"selected\"" : string.Empty) + ">Sweden</option>");
            output.WriteLine("<option value=\"ch\"" + Strings.ToString(Text == "ch" ? " selected=\"selected\"" : string.Empty) + ">Switzerland</option>");
            output.WriteLine("<option value=\"sy\"" + Strings.ToString(Text == "sy" ? " selected=\"selected\"" : string.Empty) + ">Syria</option>");
            output.WriteLine("<option value=\"tw\"" + Strings.ToString(Text == "tw" ? " selected=\"selected\"" : string.Empty) + ">Taiwan</option>");
            output.WriteLine("<option value=\"tj\"" + Strings.ToString(Text == "tj" ? " selected=\"selected\"" : string.Empty) + ">Tajikistan</option>");
            output.WriteLine("<option value=\"tz\"" + Strings.ToString(Text == "tz" ? " selected=\"selected\"" : string.Empty) + ">Tanzania</option>");
            output.WriteLine("<option value=\"th\"" + Strings.ToString(Text == "th" ? " selected=\"selected\"" : string.Empty) + ">Thailand</option>");
            output.WriteLine("<option value=\"tg\"" + Strings.ToString(Text == "tg" ? " selected=\"selected\"" : string.Empty) + ">Togo</option>");
            output.WriteLine("<option value=\"tk\"" + Strings.ToString(Text == "tk" ? " selected=\"selected\"" : string.Empty) + ">Tokelau</option>");
            output.WriteLine("<option value=\"to\"" + Strings.ToString(Text == "to" ? " selected=\"selected\"" : string.Empty) + ">Tonga</option>");
            output.WriteLine("<option value=\"tt\"" + Strings.ToString(Text == "tt" ? " selected=\"selected\"" : string.Empty) + ">Trinidad and Tobago</option>");
            output.WriteLine("<option value=\"tn\"" + Strings.ToString(Text == "tn" ? " selected=\"selected\"" : string.Empty) + ">Tunisia</option>");
            output.WriteLine("<option value=\"tr\"" + Strings.ToString(Text == "tr" ? " selected=\"selected\"" : string.Empty) + ">Turkey</option>");
            output.WriteLine("<option value=\"tm\"" + Strings.ToString(Text == "tm" ? " selected=\"selected\"" : string.Empty) + ">Turkmenistan</option>");
            output.WriteLine("<option value=\"tc\"" + Strings.ToString(Text == "tc" ? " selected=\"selected\"" : string.Empty) + ">Turks and Caicos Islands</option>");
            output.WriteLine("<option value=\"tv\"" + Strings.ToString(Text == "tv" ? " selected=\"selected\"" : string.Empty) + ">Tuvalu</option>");
            output.WriteLine("<option value=\"um\"" + Strings.ToString(Text == "um" ? " selected=\"selected\"" : string.Empty) + ">U.S. Minor Outlying Islands</option>");
            output.WriteLine("<option value=\"ug\"" + Strings.ToString(Text == "ug" ? " selected=\"selected\"" : string.Empty) + ">Uganda</option>");
            output.WriteLine("<option value=\"ua\"" + Strings.ToString(Text == "ua" ? " selected=\"selected\"" : string.Empty) + ">Ukraine</option>");
            output.WriteLine("<option value=\"ae\"" + Strings.ToString(Text == "ae" ? " selected=\"selected\"" : string.Empty) + ">United Arab Emirates</option>");
            output.WriteLine("<option value=\"uk\"" + Strings.ToString(Text == "uk" ? " selected=\"selected\"" : string.Empty) + ">United Kingdom</option>");
            output.WriteLine("<option value=\"us\"" + Strings.ToString(Text == "us" ? " selected=\"selected\"" : string.Empty) + ">United States of America</option>");
            output.WriteLine("<option value=\"uy\"" + Strings.ToString(Text == "uy" ? " selected=\"selected\"" : string.Empty) + ">Uruguay</option>");
            output.WriteLine("<option value=\"uz\"" + Strings.ToString(Text == "uz" ? " selected=\"selected\"" : string.Empty) + ">Uzbekistan</option>");
            output.WriteLine("<option value=\"vu\"" + Strings.ToString(Text == "vu" ? " selected=\"selected\"" : string.Empty) + ">Vanuatu</option>");
            output.WriteLine("<option value=\"va\"" + Strings.ToString(Text == "va" ? " selected=\"selected\"" : string.Empty) + ">Vatican City</option>");
            output.WriteLine("<option value=\"ve\"" + Strings.ToString(Text == "ve" ? " selected=\"selected\"" : string.Empty) + ">Venezuela</option>");
            output.WriteLine("<option value=\"vn\"" + Strings.ToString(Text == "vn" ? " selected=\"selected\"" : string.Empty) + ">Vietnam</option>");
            output.WriteLine("<option value=\"vi\"" + Strings.ToString(Text == "vi" ? " selected=\"selected\"" : string.Empty) + ">Virgin Islands</option>");
            output.WriteLine("<option value=\"wf\"" + Strings.ToString(Text == "wf" ? " selected=\"selected\"" : string.Empty) + ">Wallis and Futuna Islands</option>");
            output.WriteLine("<option value=\"eh\"" + Strings.ToString(Text == "eh" ? " selected=\"selected\"" : string.Empty) + ">Western Sahara</option>");
            output.WriteLine("<option value=\"ye\"" + Strings.ToString(Text == "ye" ? " selected=\"selected\"" : string.Empty) + ">Yemen</option>");
            output.WriteLine("<option value=\"yu\"" + Strings.ToString(Text == "yu" ? " selected=\"selected\"" : string.Empty) + ">Yugoslavia (Former)</option>");
            output.WriteLine("<option value=\"zr\"" + Strings.ToString(Text == "zr" ? " selected=\"selected\"" : string.Empty) + ">Zaire</option>");
            output.WriteLine("<option value=\"zm\"" + Strings.ToString(Text == "zm" ? " selected=\"selected\"" : string.Empty) + ">Zambia</option>");
            output.WriteLine("<option value=\"zw\"" + Strings.ToString(Text == "zw" ? " selected=\"selected\"" : string.Empty) + ">Zimbabwe</option>");
            output.WriteLine("</select>");
        }
    }
}