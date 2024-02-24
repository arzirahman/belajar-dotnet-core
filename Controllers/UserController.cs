using System.Security.Claims;
using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("user-management/users")]
    public class UserController(AppDbContext dbContext, UserService userService, JwtService jwtService) : ApiController 
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserService _userService = userService;
        private readonly JwtService _jwtService = jwtService;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUser()
        {
            Console.WriteLine(_jwtService.GetUserData().Username);
            var users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

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