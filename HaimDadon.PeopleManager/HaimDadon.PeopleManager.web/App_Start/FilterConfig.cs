using System.Web;
using System.Web.Mvc;

namespace HaimDadon.PeopleManager.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
