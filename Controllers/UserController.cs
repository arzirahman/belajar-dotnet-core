using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("user-management/users")]
    public class UserController : ControllerBase 
    {
        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] SignInRequest request)
        {
            Console.WriteLine("Request Body:");
            Console.WriteLine(request.Username);
            return Ok(new MessageResponse { Message = "Login success", StatusCode = 200, Status = "OK" });
        }
    }
}