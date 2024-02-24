using System.Net;
using System.Text.Json;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Resources;
using Microsoft.AspNetCore.WebUtilities;

namespace food_order_dotnet.Middlewares
{
    public class AuthorizeException(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";

                var response = new MessageResponse
                {
                    Message = ValidationMessages.Unauthorized,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Status = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Unauthorized)
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}