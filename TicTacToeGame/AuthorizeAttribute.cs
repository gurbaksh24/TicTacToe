using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlDatabase;
namespace TicTacToeGame
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        [LogException]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["accessToken"].ToString();
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new UnauthorizedAccessException("accessToken is not passed");
            }
            else
            {
                Database database = new Database();
                bool checkAccessToken = database.CheckAccessTokenExist(accessToken);
                if (checkAccessToken == false)
                    throw new UnauthorizedAccessException("Access Token doesn't exist");
            }
        }
    }
}
