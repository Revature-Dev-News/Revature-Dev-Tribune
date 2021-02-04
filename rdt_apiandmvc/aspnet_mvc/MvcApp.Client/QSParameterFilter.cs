using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace MvcApp.Client
{
    public class QSParameterFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var QSParameter = context.HttpContext.Request.Query["parameter"];
            ((Controller)context.Controller).ViewBag.QSParameter = QSParameter;
        }
        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}