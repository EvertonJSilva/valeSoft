using System.Web.Optimization;

namespace ProjetoModeloDDD.MVC
{
    public class RegisterBundlesTeste
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-1.10.2.intellisense.js",
                    "~/Scripts/jquery-1.10.2.js",
                    "~/Scripts/jquery-1.10.2.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    // "~/Content/SiteB/js/jquery-ui-1.9.2.custom.min.js",
                    //"~/Content/SiteB/js/jquery.js",
                    //"~/Content/SiteB/js/jquery.maskedinput.js",

                    "~/Content/SiteB/js/jquery.ui.touch-punch.min.js",
                    "~/Content/SiteB/js/jquery.dcjqaccordion.2.7.js",
                    "~/Content/SiteB/js/jquery.scrollTo.min.js",
                    "~/Content/SiteB/js/jquery.nicescroll.js",
                    "~/Content/SiteB/js/jquery.*",
                    //"~/Content/SiteB/js/jquery.scrollTo.min.js",
                    ////"~/Content/SiteB/js/jquery.nicescroll.js",
                    //"~/Scripts/jquery.validate.vsdoc.js",
                    //"~/Scripts/jquery.validate.js",
                    //"~/Scripts/jquery.validae.min.js",
                    //"~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Content/SiteB/js/bootstrap-switch.js",
                    "~/Content/SiteB/js/bootstrap-toggle.js",
                    "~/Content/SiteB/js/bootstrap2-toggle.js",
                    "~/Content/SiteB/js/bootstrap-toggle.min.js",
                    "~/Content/SiteB/js/bootstrap2-toggle.min.js",
                    "~/Content/SiteB/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts1").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/gridmvc.min.js",
                    "~/Content/SiteB/js/gritter-conf.js",
                    "~/Content/SiteB/lineicons/Ite-ie7.js",
                    "~/Scripts/gridmvc.lan.ru.js",
                    "~/Content/SiteB/js/common-scripts.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/Scripts2").Include(

                    "~/Content/SiteB/js/zabuto_calendar.js",
                    "~/Content/SiteB/js/chartjs-scripts.js",
                    "~/Content/SiteB/js/easy-pie-chart.js",
                    "~/Content/SiteB/js/fullcalendar/fullcalendar.min.js",
                    "~/Content/SiteB/js/calendar-conf-events.js",
                    "~/Content/SiteB/js/Mascara.js",
                    "~/Content/SiteB/js/Tasks.js",


                    "~/Content/SiteB/js/form-component.js"
                  )
                    );


            bundles.Add(new ScriptBundle("~/bundles/Fonts").Include(
                    "~/Content/SiteB/font-awesome/fonts/fontawesome.webfont.eot",
                    "~/Content/SiteB/font-awesome/fonts/fontawesome.webfont.svg",
                    "~/Content/SiteB/font-awesome/fonts/fontawesome.webfont.ttf",
                    "~/Content/SiteB/font-awesome/fonts/fontawesome.webfont.woff"));
                    

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap.min.css",
                        "~/Content/PagedList.css",
                        "~/Content/SiteB/css/bootstrap-toggle.css",
                      "~/Content/SiteB/css/bootstrap2-toggle.css",
                      "~/Content/SiteB/css/bootstrap-toggle.min.css",
                      "~/Content/SiteB/css/bootstrap2-toggle.min.css",
                      "~/Content/SiteB/css/bootstrap2-toggle.css",
                      "~/Content/SiteB/font-awesome/css/font-awesome.css",
                      "~/Content/SiteB/css/style.css",
                    "~/Content/SiteB/js/fullcalendar/bootstrap-fullcalendar.css",
                      "~/Content/SiteB/lineicons/style.css",
                      "~/Content/Gridmvc.css",
                      "~/Content/SiteB/css/style-responsive.css"));


            
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
