using System.Web;
using System.Web.Optimization;

namespace ClubMember
{
    public class BundleConfig
    {
        public int TemplateNumber;
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862

        public void SetTemplate(int TempNumber)
        {
            TemplateNumber = TempNumber;
        }

        public int GetTemplateNumber()
        {
            return TemplateNumber;
        }
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleConfig Bconfig = new BundleConfig();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            if(Bconfig.GetTemplateNumber() == 1 || Bconfig.GetTemplateNumber() == 0)
                bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap3-slate.css",
                      "~/Content/site.css"));
            else
                bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap3-Darkly.css",
                      "~/Content/site.css"));
        }
    }
}
