using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlDatabase;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeGame
{
    public class LogExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string request = context.HttpContext.Request.Method;
            string response = context.RouteData.Values["action"].ToString();
            string exception = context.Exception.Message;
            string comment = context.Exception.StackTrace.Substring(0, 49);
            Database database = new Database();
            database.InsertLog(request, response, exception, comment);
            context.Result = new JsonResult(exception);
        }
    }
}
