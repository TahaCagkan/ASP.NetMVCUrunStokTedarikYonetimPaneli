using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCYonetimPaneli
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            if(Application["ActiveUser"] == null)
            {
                int cnt = 1;
                Application["ActiveUser"] = cnt;
            }
            else
            {
                int cnt = (int)Application["ActiveUser"];
                cnt++;
                Application["ActiveUser"] = cnt;
            }
            if (Application["TotalVisiter"] == null)
            {
                int cnt = 1;
                Application["TotalVisiter"] = cnt;
            }
            else
            {
                int cnt = (int)Application["TotalVisiter"];
                cnt++;
                Application["TotalVisiter"] = cnt;
            }
        }

        protected void Session_End()
        {
            int cnt = (int)Application["ActiveUser"];
            cnt--;
            Application["ActiveUser"]= cnt;
        }
    }
}
