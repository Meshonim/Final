using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Filters
{
    public class ExceptionCatchAttribute : FilterAttribute, IExceptionFilter
    {
        public IExceptionObjectService ExceptionObjectService
        {
            get { return (IExceptionObjectService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IExceptionObjectService)); }
        }
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionObjectEntity exceptionObject = new ExceptionObjectEntity()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                Action = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            ExceptionObjectService.Create(exceptionObject);
            filterContext.Result = filterContext.HttpContext.Response.StatusCode == 404 ?
                new ViewResult { ViewName = "~/Views/Error/NotFound.cshtml" } : new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };

            filterContext.ExceptionHandled = true;
        }
    }
}