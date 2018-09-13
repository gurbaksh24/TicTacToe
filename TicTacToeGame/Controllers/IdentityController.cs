using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlDatabase;
namespace TicTacToeGame.Controllers
{
    [Route("api/Identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ApiResponse AddANewUser([FromBody] Users users)
        {
            Database database = new Database();
            string accessToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bool result = database.InsertUsers(users.FirstName, users.LastName, users.Username, accessToken);
            if (result)
            {
                return new ApiResponse()
                {
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Success,
                        StatusCode = 200
                    }
                };
            }
            else
            {
                return new ApiResponse()
                {
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        StatusCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
            }
        }

       
    }
}
