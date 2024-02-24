using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("user-management/users")]
    public class UserController(AppDbContext dbContext, UserService userService) : ApiController 
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserService _userService = userService;

        [HttpPost("sign-up")]
        public async Task<ActionResult<MessageResponse>> SignUp(SignUpRequest request)
        {
            return await ExecuteAction(async () => await _userService.SignUp(request));
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
        {
            return await ExecuteAction(async () => await _userService.SignIn(request));
        }
    }
}