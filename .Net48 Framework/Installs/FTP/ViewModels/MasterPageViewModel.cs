using DotVVM.Framework.ViewModel;
using SepCommon;

namespace wwwroot.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        public string SiteLogo { get; set; }
        public string WebsiteName { get; set; }
        public string CompanySlogan { get; set; }
        public string Banners { get; set; }
        public string BreadCrumbs { get; set; }
        public string UserTopMenu { get; set; }
        public string AccountMenu { get; set; }
        public string Sponsors { get; set; }
        public string RandomPoll { get; set; }
        public string Newsletters { get; set; }
        public string SearchBox { get; set; }
        public string WhosOnline { get; set; }
        public string FlashMessenger { get; set; }
        public string SiteMenu1 { get; set; }
        public string SiteMenu2 { get; set; }
        public string SiteMenu3 { get; set; }
        public string SiteMenu4 { get; set; }
        public string SiteMenu5 { get; set; }
        public string SiteMenu6 { get; set; }
        public string SiteMenu7 { get; set; }
        public string PoweredBySepCity { get; set; }

        public MasterPageViewModel()
        {

            var cSiteMenu1 = new SepCityControls.SiteMenu
            {
                MenuID = 1
            };
            SiteMenu1 = cSiteMenu1.Render();

            SepFunctions.Page_Load();
            //Page.MasterPageFile = SepFunctions.GetMasterPage();
            //Globals.LoadSiteTheme(Master);
        }
    }
}

