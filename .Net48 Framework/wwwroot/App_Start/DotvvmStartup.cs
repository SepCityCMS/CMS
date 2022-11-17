using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace wwwroot
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config);
            ConfigureControls();
            ConfigureResources();
        }

        private void ConfigureRoutes(DotvvmConfiguration config)
        {
            //config.RouteTable.Add("Default", "", "Views/default.dothtml", new { });
            //config.RouteTable.Add("DefaultAspx", "default.aspx", "Views/default.dothtml", new { });
            config.RouteTable.Add("AdminArticles", "spadmin/articles", "Views/admin/articles/articles.dothtml", new { });
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls()
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources()
        {
            // register custom resources and adjust paths to the built-in resources
            //config.Resources.Register("Styles", new StylesheetResource()
            //{
            //    Location = new UrlResourceLocation("~/Styles/styles.css")
            //});
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
