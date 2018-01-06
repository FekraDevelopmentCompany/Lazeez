using System.Net;
using System.Web.Mvc;
using Elmah;
using Lazeez.Helper;
using Lazeez.Resource.Shared;

namespace Lazeez.Web.Helpers.Filters
{
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new PartialViewResult
                {
                    ViewName = "Dialogs/_Message",
                    ViewData = new ViewDataDictionary(new MessageDialog
                    {
                        Title = Resources.Res_Error,
                        Message = filterContext.Exception.GetBaseException().Message,
                        Type = MessageDialog.ButtonType.Error
                    })
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Code (500) To Excute OnFailure
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }

            // Signal ELMAH to log the exception
            ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}