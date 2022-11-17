using System;

namespace wwwroot
{
    public partial class uber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SepCommon.Uber cUber = new SepCommon.Uber();
            Response.Write(cUber.GetPricing(SepCommon.SepCore.Request.Item("code")));

        }
    }
}