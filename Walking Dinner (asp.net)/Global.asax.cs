using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Walking_Dinner__asp.net_.Controllers;
using Walking_Dinner__asp.net_.Models;


namespace Walking_Dinner__asp.net_
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer<AdminparallelDB>(new DropCreateDatabaseIfModelChanges<AdminparallelDB>());
            //Database.SetInitializer<AdminDB>(new DropCreateDatabaseIfModelChanges<AdminDB>());

            //            Database.SetInitializer<persoonDB>(new DropCreateDatabaseIfModelChanges<persoonDB>());

            //          Database.SetInitializer<persoonparallelDB>(new DropCreateDatabaseIfModelChanges<persoonparallelDB>());


            persoondatasController.StartupClass startupobject = new persoondatasController.StartupClass();
            startupobject.Init();



        }
    }
}
