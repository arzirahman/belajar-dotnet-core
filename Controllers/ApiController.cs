using System.Net;
using food_order_dotnet.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace food_order_dotnet.Controllers
{
    public class ApiController : ControllerBase
    {
        protected async Task<ActionResult<T>> ExecuteAction<T>(Func<Task<T>> action) where T : class
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new MessageResponse
                {
                    Message = ex.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Status = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest)
                });
            }
        }
    }
}
