using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("user-management/users")]
    public class UserController(AppDbContext dbContext, UserService userService) : ApiController 
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            var users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<MessageResponse>> SignUp(SignUpRequest request)
        {
            return await ExecuteAction(async () => await _userService.SignUp(request));
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(SignInRequest request)
        {
            Console.WriteLine("Request Body:");
            Console.WriteLine(request.Username);
            return Ok(new MessageResponse { Message = "Login success", StatusCode = 200, Status = "OK" });
        }
    }
}