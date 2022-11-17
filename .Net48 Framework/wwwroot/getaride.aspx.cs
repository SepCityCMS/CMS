using System;

namespace wwwroot
{
    public partial class getaride : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SepCommon.Uber cUber = new SepCommon.Uber();
            cUber.Login();

        }
    }
}