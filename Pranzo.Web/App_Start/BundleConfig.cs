﻿using System.Web.Optimization;

namespace Lazeez.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*",
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatablegrid").Include(
                      "~/Content/plugins/datatables/media/js/jquery.dataTables.min.js",
                      "~/Content/plugins/datatables/media/js/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pluginscripts").Include(
                     "~/Content/plugins/bootstrap-clockpicker/js/bootstrap-clockpicker.min.js",
                     "~/Content/plugins/moment/moment.min.js",
                     "~/Content/plugins/moment/moment-with-locales.min.js",
                     "~/Content/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                     "~/Content/plugins/chartjs/Chart.min.js",
                     "~/Content/plugins/bootstrap-progressbar/js/bootstrap-progressbar.min.js",
                     "~/Content/plugins/skycons/skycons.js",
                     "~/Content/plugins/jqvmap/dist/jquery.vmap.js",
                     "~/Content/plugins/jqvmap/dist/maps/jquery.vmap.world.js",
                     "~/Content/plugins/jqvmap/dist/maps/jquery.vmap.usa.js",
                     "~/Content/plugins/jqvmap/examples/js/jquery.vmap.sampledata.js",
                     "~/Content/plugins/bootstrap-duallistbox/js/jquery.bootstrap-duallistbox.min.js",
                     "~/Content/plugins/lobibox/js/lobibox.min.js",
                     //"~/Content/plugins/lobibox/js/notifications.js",
                     "~/Content/plugins/bootstrap-fileinput/plugins/canvas-to-blob.min.js",
                     "~/Content/plugins/bootstrap-fileinput/js/fileinput.min.js",
                     "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customscripts").Include(
                     "~/Scripts/custom/master.js",
                     "~/Scripts/custom/datepicker.js",
                     "~/Scripts/custom/datatable-grid.js",
                     "~/Scripts/custom/custom.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/plugins/bootstrap/css/bootstrap.min.css",
                      //"~/Content/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/plugins/bootstrap-clockpicker/css/bootstrap-clockpicker.min.css",
                      "~/Content/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css",
                      "~/Content/plugins/datatables/media/css/dataTables.bootstrap.min.css",
                      "~/Content/plugins/datatables/extensions/Select/css/select.bootstrap.min.css",
                      "~/Content/plugins/bootstrap-duallistbox/css/bootstrap-duallistbox.min.css",
                      "~/Content/plugins/lobibox/css/lobibox.min.css",
                      "~/Content/plugins/bootstrap-fileinput/css/fileinput.min.css",
                      "~/Content/plugins/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/plugins/jqvmap/jqvmap.min.css",
                      //"~/Content/custom/custom.css",
                      "~/Content/Site.css"));
        }
    }
}
