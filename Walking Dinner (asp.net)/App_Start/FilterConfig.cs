using System.Web;
using System.Web.Mvc;

namespace Walking_Dinner__asp.net_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
