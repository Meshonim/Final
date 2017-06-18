using MvcPL.Filters;
using System.Web;
using System.Web.Mvc;

namespace MvcPL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionCatchAttribute());
            filters.Add(new HandleErrorAttribute());          
        }
    }
}
