using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("food-order")]
    public class FoodController(FoodService foodService) : ApiController 
    {
        private readonly FoodService _foodService = foodService;

        [HttpGet("foods")]
        [Authorize]
        public async Task<ActionResult<FoodListResponse>> GetFoods([FromQuery] FoodListRequest param)
        {
            return await ExecuteAction(async () => await _foodService.GetFoods(param));
        }

        [HttpPost("cart")]
        [Authorize]
        public async Task<ActionResult<CartResponse>> AddCart(CartRequest request)
        {
            return await ExecuteAction(async () => await _foodService.AddCart(request));
        }

        [HttpDelete("cart/{foodId}")]
        [Authorize]
        public async Task<ActionResult<CartResponse>> AddCart(int foodId)
        {
            return await ExecuteAction(async () => await _foodService.DeleteCart(foodId));
        }
    }
}