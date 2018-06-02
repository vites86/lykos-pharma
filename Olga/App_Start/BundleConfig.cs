using System.Web;
using System.Web.Optimization;

namespace Olga
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/plugins/DataTable/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/umega/css").Include(
               "~/Content/umega/plugins/PACE/themes/blue/pace-theme-flash.css",
               "~/Content/umega/plugins/themify-icons/themify-icons.css",
               "~/Content/umega/plugins/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
               "~/Content/umega/plugins/toastr/toastr.min.css",
               "~/Content/umega/plugins/datatables.net-bs/css/dataTables.bootstrap.min.css",
               "~/Content/umega/plugins/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
               "~/Content/umega/plugins/datatables.net-colreorder-bs/css/colReorder.bootstrap.min.css",
               "~/Content/umega/plugins/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
               "~/Content/umega/build/css/fourth-layout.css",
               "~/Content/umega/pligins/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.min.css",
               "~/Content/umega/plugins/animo.js/animate-animo.min.css",
               "~/Content/umega/plugins/flag-icon-css/css/flag-icon.min.css",
               "~/Content/umega/plugins/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/umegajs").Include(
                "~/Content/umega/plugins/jquery/dist/jquery.min.js",
                "~/Content/umega/plugins/PACE/pace.min.js",
                "~/Content/umega/plugins/toastr/toastr.min.js",
                "~/Content/umega/plugins/moment/min/moment.min.js",
                "~/Content/umega/plugins/datatables.net/js/jquery.dataTables.min.js",
                "~/Content/umega/plugins/datatables.net-bs/js/dataTables.bootstrap.min.js",
                // "~/Content/umega/plugins/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/Content/umega/plugins/datatables.net-buttons/js/buttons.print.min.js",
                "~/Content/umega/plugins/datatables.net-buttons/js/buttons.html5.min.js",
                "~/Content/umega/plugins/datatables.net-colreorder/js/dataTables.colReorder.min.js",
                "~/Content/umega/plugins/datatables.net-responsive/js/dataTables.responsive.min.js",
                // "~/Content/umega/plugins/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                //  "~/Content/umega/build/js/fourth-layout/app.js",
                //  "~/Content/umega/build/js/fourth-layout/demo.js"

                "~/Content/umega/plugins/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                "~/Content/umega/plugins/animo.js/animo.min.js",
                "~/Content/umega/plugins/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/Content/umega/plugins/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js",
                "~/Content/umega/plugins/moment/locale/ru.js",
                "~/Content/umega/plugins/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Content/umega/build/js/page-content/pickers/datetime-picker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/plugins/DataTable/jquery.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/bundles/Ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery-3.3.1.min.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                "~/Scripts/dropzone/dropzone-amd-module.min.js",
                "~/Scripts/dropzone/dropzone.js"
            ));

           
        }
    }
}
