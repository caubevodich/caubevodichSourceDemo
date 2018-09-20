﻿using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
             

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
             

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                     
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.easing.min.js", 

                      "~/Scripts/pace.min.js",
                                                              
                      "~/Scripts/apps/app.js"));

 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                    "~/Content/font-awesome.css",
                      "~/Content/pace.css",
                     
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/account").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/font-awesome.css",
                      "~/Content/pace.css" ));



            BundleTable.EnableOptimizations = false;
        }
        
    }
}
