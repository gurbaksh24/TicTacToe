using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlDatabase;
namespace TicTacToeGame
{
    public class LoggerAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string accessToken = context.HttpContext.Request.Headers["accessToken"].ToString();
            Database database = new Database();
            string userName = database.GetUsername(accessToken);
            var request = userName + " " + context.RouteData.Values["action"].ToString();
            var response = "Success " + context.RouteData.Values["action"].ToString() + " by " + userName;
            database.InsertLog(request, response, "", "");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Headers["accessToken"].ToString();
            Database database = new Database();
            string userName = database.GetUsername(accessToken);
            var request = userName + " executing " + context.RouteData.Values["action"].ToString();
            var response = "Executing " + context.RouteData.Values["action"].ToString() + " by " + userName;
            database.InsertLog(request, response, "", "");
        }
    }
}
