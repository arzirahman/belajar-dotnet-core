using System.Net;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

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
            catch (DbUpdateException ex)
            {
                return BadRequest(new MessageResponse
                {
                    Message = ex.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Status = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new MessageResponse
                {
                    Message = ValidationMessages.ServerError,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Status = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError)
                });
            }
        }
    }
}
