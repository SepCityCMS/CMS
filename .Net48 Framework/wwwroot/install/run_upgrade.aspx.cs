using SepCommon;
using System;
using System.IO;
using System.Web.UI;
using wwwroot.SepActivation;

namespace wwwroot
{
    public partial class run_upgrade : Page
    {
        private readonly SepFunctions cCommon = new SepFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(SepCommon.SepCore.HostingEnvironment.MapPath("~/app_data/") + "\\system.xml")) cCommon.Redirect("installed.aspx");

            if (Session["DBAddress"] == null || Session["DBName"] == null || Session["DBUser"] == null || Session["DBPass"] == null) contentupgrade.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Missing Database Information, please run the install again.</div>";
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            cCommon.Redirect("default.aspx");
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            using (var soapClient = new activationSoapClient("activationSoap"))
            {
                var jActivation = soapClient.Get_License_Details(UserName.Value, cCommon.MD5Hash_Encrypt(Password.Value), LicenseKey.Value);

                try
                {
                    if (jActivation.Status == "Active")
                    {
                        Session["DBCategories"] = "No";
                        Session["DBSampleData"] = "No";

                        Session["SMTPServer"] = SMTPServer.Value;
                        Session["SMTPUser"] = SMTPUser.Value;
                        Session["SMTPPass"] = SMTPPass.Value;

                        Session["LicenseUser"] = UserName.Value;
                        Session["LicensePass"] = cCommon.MD5Hash_Encrypt(Password.Value);
                        Session["LicenseKey"] = LicenseKey.Value;

                        Session["PUserName"] = "";
                        Session["PPassword"] = "";
                        Session["PSecretQuestion"] = "";
                        Session["PSecretAnswer"] = "";
                        Session["PEmailAddress"] = "";
                        Session["PFirstName"] = "";
                        Session["PLastName"] = "";
                        Session["PCountry"] = "";
                        Session["PStreetAddress"] = "";
                        Session["PCity"] = "";
                        Session["PState"] = "";
                        Session["PPostalCode"] = "";
                        Session["PGender"] = "";
                        Session["PBirthDate"] = "";
                        Session["PPhoneNumber"] = "";

                        cCommon.Redirect("runinstall.aspx?DoAction=Upgrade");
                    }
                    else
                    {
                        ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Invalid license information.</div>";
                    }
                }
                catch
                {
                    ErrorMessage.InnerHtml = "<div class=\"alert alert-danger\" role=\"alert\">Invalid license information.</div>";
                }
            }
        }
    }
}