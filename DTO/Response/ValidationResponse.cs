using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace food_order_dotnet.DTO.Response
{
    public class ValidationResponse : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();

                context.Result = new BadRequestObjectResult(new MessageResponse
                {
                    Message = message ?? "Invalid Request",
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.BadRequest)
                });
            }

            base.OnResultExecuting(context);
        }
    }
}