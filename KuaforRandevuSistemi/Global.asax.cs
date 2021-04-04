using KuaforRandevuSistemi.Ayar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KuaforRandevuSistemi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Bütün sayfalarda çalýþ
            GlobalFilters.Filters.Add(new _SecurityFilter());
        }
    }
}
