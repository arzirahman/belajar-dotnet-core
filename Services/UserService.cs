using System.Net;
using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Models;
using food_order_dotnet.Resources;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Services
{
    public class UserService(AppDbContext dbContext, JwtService jwtService)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly JwtService _jwtService = jwtService;

        public async Task<MessageResponse> SignUp(SignUpRequest user){
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                throw new DbUpdateException(string.Format(ValidationMessages.UsernameExist ?? "", user.Username));
            }
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Fullname = user.Fullname
            };
            newUser.HashPassword();
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return new MessageResponse{
                Message = string.Format(ValidationMessages.RegisterSuccess ?? "", user.Username),
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK)
            };
        }

        public async Task<SignInResponse> SignIn(SignInRequest user){
            var userData = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username) 
                ?? throw new DbUpdateException(ValidationMessages.LoginFailed ?? "");
            if (!userData.VerifyPassword(user.Password ?? "")) {
                throw new DbUpdateException(ValidationMessages.LoginFailed ?? "");
            }
            return new SignInResponse{
                Data = new SignInDTO
                {
                    Id = userData.UserId,
                    Token = _jwtService.GenerateToken(userData),
                    Type = "Bearer",
                    Username = user.Username,
                },
                Message = ValidationMessages.LoginSuccess,
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK)
            };
        }
    }
}