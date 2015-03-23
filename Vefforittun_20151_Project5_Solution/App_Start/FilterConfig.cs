using System.Web;
using System.Web.Mvc;

namespace Vefforittun_20151_Project5_Solution
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
