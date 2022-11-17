namespace wwwroot
{
    using SepCommon;
    using System;
    using System.Web.UI;

    /// <summary>
    /// Class sq_checkout.
    /// Implements the <see cref="System.Web.UI.Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class sq_checkout : Page
    {
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
            ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">" + SepFunctions.LangText("Your credit card has been declined") + "</div>";

            if (!string.IsNullOrWhiteSpace(SepCommon.SepCore.Request.Item("OrderGUID")) && !string.IsNullOrWhiteSpace(SepCommon.SepCore.Session.getSession("OrderGUID")) && !string.IsNullOrWhiteSpace(SepFunctions.Session_Invoice_ID()))
            {
                if (SepCommon.SepCore.Request.Item("OrderGUID") == SepCommon.SepCore.Session.getSession("OrderGUID"))
                {
                    SepCommon.DAL.Invoices.Invoice_Mark_Paid(SepFunctions.Session_Invoice_ID());
                    SepCommon.SepCore.Session.setSession("OrderGUID", string.Empty);
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-success\" role=\"alert\">" + SepFunctions.LangText("Your credit card has been successfully processed") + "</div>";
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